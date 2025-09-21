using System.Text.Json;
using Core.Configuration;
using Core.Models;
using Core.ResponseModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (FriendlyException ex)
        {
            context.Response.StatusCode = ex.Status;
            context.Response.ContentType = "text/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                FriendlyExceptionResponse
                {
                    Title = ex.Title,
                    Status = ex.Status,
                    Details = ex.Details,
                    Instance = context.Request.Path + context.Request.QueryString
                }));
            return;
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/json";
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                FriendlyExceptionResponse
                {
                    Title = "System Error",
                    Status = 500,
                    Details = ex.Message,
                    Instance = context.Request.Path + context.Request.QueryString
                }));
            return;
        }
    }
}