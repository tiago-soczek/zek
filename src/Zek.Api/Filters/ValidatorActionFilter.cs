using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zek.Api.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var details = new ValidationProblemDetails(context.ModelState);

            context.Result = new BadRequestObjectResult(details);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed
        }
    }
}