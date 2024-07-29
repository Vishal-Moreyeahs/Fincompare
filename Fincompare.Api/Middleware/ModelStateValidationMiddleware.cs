using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Fincompare.Application.Models;

namespace Fincompare.Api.Middleware
{
    public class ModelStateValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ModelStateValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();
                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        var modelState = context.RequestServices.GetService(typeof(ModelStateDictionary)) as ModelStateDictionary;
                        if (modelState != null && !modelState.IsValid)
                        {
                            var errors = modelState
                                .Where(m => m.Value.Errors.Count > 0)
                                .Select(m => new
                                {
                                    Field = m.Key,
                                    Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                });

                            var problemDetails = new ValidationErrorResponse
                            {
                                Success = false,
                                Errors = errors.ToDictionary(e => e.Field, e => e.Errors)
                            };

                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsJsonAsync(problemDetails);
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }

    }
}
