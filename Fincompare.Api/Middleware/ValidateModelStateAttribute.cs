using Fincompare.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fincompare.Api.Middleware
{
    public class ValidateModelStateAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var customValidationProblemDetails = new CustomValidationProblemDetails
                {
                    Success = false,
                    Errors = context.ModelState
                        .SelectMany(ms => ms.Value.Errors.Select(e => new CustomValidationError
                        {
                            Field = ms.Key,
                            Message = e.ErrorMessage
                        }))
                        .ToList()
                };

                context.Result = new JsonResult(customValidationProblemDetails)
                {
                    StatusCode = 400
                };
            }
        }
    }

}
