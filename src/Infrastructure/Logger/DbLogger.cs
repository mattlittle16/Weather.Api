using System.Reflection;
using System.Text.Json;
using Core.Enums;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System.Threading;

namespace Infrastructure.Logger;

public class DbLogger : ILogger  
{  
    private readonly DbLoggerProvider _dbLoggerProvider;   
    private static readonly AsyncLocal<string?> CurrentScope = new();
    
    public DbLogger(DbLoggerProvider dbLoggerProvider)  
    {  
        _dbLoggerProvider = dbLoggerProvider;   
    }  

    // Explicit interface implementation to match nullability constraints
    IDisposable ILogger.BeginScope<TState>(TState state)
    {
        var parentScope = CurrentScope.Value;
        CurrentScope.Value = state?.ToString();
        return new ScopeDisposable(() => CurrentScope.Value = parentScope);
    }

    private class ScopeDisposable : IDisposable
    {
        private readonly Action _onDispose;
        public ScopeDisposable(Action onDispose) => _onDispose = onDispose;
        public void Dispose() => _onDispose();
    }

    public bool IsEnabled(LogLevel logLevel)  
    {  
        return logLevel != LogLevel.None;  
    }  

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)  
    {  
        if (!IsEnabled(logLevel))  
        {   
            return;  
        }  

        var threadId = Thread.CurrentThread.ManagedThreadId; // Get the current thread ID to use in the log file.   

        // Store record.  
        using (var connection = new MySqlConnection(_dbLoggerProvider._ConString))  
        {  
            connection.Open();  

            FieldInfo[]? fieldsInfo = state?.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo? props = fieldsInfo?.FirstOrDefault(o => o.Name == "_values");
            object[]? values = props?.GetValue(state) as object[];
        
            var logType = LogTypeEnum.System;
            
            if (values?.Length > 0)
            {
                try 
                {
                    logType = (LogTypeEnum)values[0];
                }
                catch
                {}
            }

            var logTypeId = string.Empty;
            
            var message = "";

            var scope = CurrentScope.Value;
            if (!string.IsNullOrEmpty(scope))
            {
                message += $"[Scope: {scope}] ";
            }

            if (formatter != null)
            {
                message += formatter(state, exception);
            }
            else 
            {
                message += JsonSerializer.Serialize(state);
            }

            if (exception != null)
            {
                message += Environment.NewLine + exception.ToString();
            }

            try 
            {
                // Add to database.  
                using (var command = new MySqlCommand())  
                {  
                    command.Connection = connection;  
                    command.CommandType = System.Data.CommandType.Text;  
                    command.CommandText = string.Format("select Id from LogType where Type = @Type");
                    command.Parameters.Add(new MySqlParameter("@Type", logType));
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        logTypeId = reader[0].ToString();
                    }


                    command.Connection = connection;  
                    command.CommandType = System.Data.CommandType.Text;  
                    command.CommandText = string.Format(@"INSERT INTO Log (Id, LogTypeId, Message, CreatedDate, EventId, EventName) 
                    VALUES (@Id, @LogTypeId, @Message, @CreatedDate, @EventId, @EventName)");  

                    command.Parameters.Add(new MySqlParameter("@Id", Guid.NewGuid()));
                    command.Parameters.Add(new MySqlParameter("@LogTypeId", logTypeId));
                    command.Parameters.Add(new MySqlParameter("@Message", message));
                    command.Parameters.Add(new MySqlParameter("@CreatedDate", DateTimeOffset.UtcNow));
                    command.Parameters.Add(new MySqlParameter("@EventId", eventId.Id));
                    command.Parameters.Add(new MySqlParameter("@EventName", eventId.Name ?? "No Event Name"));

                    command.ExecuteNonQuery();  
                }  
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            connection.Close();  
        }  
    }  
}