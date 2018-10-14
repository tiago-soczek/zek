using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Zek.Api.Dtos;
using Zek.Model;

namespace Zek.Api.Filters
{
    public class HandleErrorFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<HandleErrorFilterAttribute> logger;

        public HandleErrorFilterAttribute(IHostingEnvironment hostingEnvironment, ILogger<HandleErrorFilterAttribute> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            logger.LogError(exception, "Unhandled Exception");

            var stackTrace = hostingEnvironment.IsDevelopment() ? exception.ToString() : null;

            var errorMessage = exception is ZekException ? exception.Message : "Internal Error";

            var friendlyErrorMessage = new ErrorResultDto
            {
                StackTrace = stackTrace,
                Error = errorMessage
            };

            context.Result = new ObjectResult(friendlyErrorMessage)
            {
                StatusCode = (int?) HttpStatusCode.InternalServerError
            };
        }
    }
}