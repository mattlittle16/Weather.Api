using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Entities;
using Core.Enums;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Infrastructure.Logger;

public class DbLogger : ILogger  
{  
    private readonly DbLoggerProvider _dbLoggerProvider;   
    
    public DbLogger(DbLoggerProvider dbLoggerProvider)  
    {  
        _dbLoggerProvider = dbLoggerProvider;   
    }  

    public IDisposable BeginScope<TState>(TState state)  
    {  
        return null;  
    }  

    public bool IsEnabled(LogLevel logLevel)  
    {  
        return logLevel != LogLevel.None;  
    }  

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)  
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

            FieldInfo[] fieldsInfo = state.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo props = fieldsInfo.FirstOrDefault(o => o.Name == "_values");
            object[] values = (object[])props?.GetValue(state);

        
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

            if (formatter != null)
            {
                message += formatter(state, exception);
            }
            else 
            {
                if (exception != null)
                {
                    message += JsonSerializer.Serialize(exception);
                }
                else 
                {
                    message += JsonSerializer.Serialize(state);
                }
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