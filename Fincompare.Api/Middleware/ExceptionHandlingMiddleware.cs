using Fincompare.Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace Fincompare.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid Token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ArgumentNullException ex:
                    if (ex.Message.Contains("not found"))
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case DbUpdateException dbEx:
                    // Check for foreign key violation
                    if (dbEx.InnerException?.Message.Contains("foreign key") ?? false)
                    {
                        var (tableName, fieldName) = ExtractTableAndFieldName(dbEx.InnerException.Message);
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = response.StatusCode,
                            Status = false,
                            Message = $"Foreign key violation occurred in table '{tableName}' on field '{fieldName}'."
                        }.ToString());
                        return;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = response.StatusCode,
                Status = false,
                Message = exception.Message
            }.ToString());
        }

        // Helper method to extract table name and field name from the exception message
        private (string TableName, string FieldName) ExtractTableAndFieldName(string errorMessage)
        {
            // Example: extract the table and field name using regex (depends on the error message format)
            // You might need to adjust this based on your actual database error message format
            var tableMatch = Regex.Match(errorMessage, @"table ""(?<TableName>\w+)""");
            var fieldMatch = Regex.Match(errorMessage, @"constraint ""(?<FieldName>\w+)""");

            string tableName = tableMatch.Groups["TableName"].Value;
            string fieldName = fieldMatch.Groups["FieldName"].Value;

            return (tableName, fieldName);
        }
    }
}
