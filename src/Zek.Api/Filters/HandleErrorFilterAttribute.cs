using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Zek.Model;

namespace Zek.Api.Filters
{
    public class HandleErrorFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly string TraceIdentifierKey = "traceId";
        private static readonly string StackTraceKey = "stackTrace";

        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<HandleErrorFilterAttribute> logger;
        private readonly IClientErrorFactory clientErrorFactory;

        public HandleErrorFilterAttribute(IHostingEnvironment hostingEnvironment, ILogger<HandleErrorFilterAttribute> logger, IClientErrorFactory clientErrorFactory)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.clientErrorFactory = clientErrorFactory;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            logger.LogError(exception, "Unhandled Exception");

            var stackTrace = hostingEnvironment.IsDevelopment() ? exception.ToString() : null;

            var title = exception is ZekException ? exception.Message : "Internal Error";

            context.Result = GetClientError(context, title, stackTrace);
        }

        // See: ProblemDetailsClientErrorFactory.cs
        // https://github.com/aspnet/Mvc/blob/82a01a414dfd69de6c407951c917ba397b210be8/src/Microsoft.AspNetCore.Mvc.Core/Infrastructure/ProblemDetailsClientErrorFactory.cs
        public IActionResult GetClientError(ExceptionContext context, string title, string stackTrace)
        {
            var problemDetails = new ProblemDetails
            {
                Status = 500,
                Type = "about:blank",
                Title = title
            };

            SetTraceId(context, problemDetails);

            problemDetails.Extensions[StackTraceKey] = stackTrace;

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status,
                ContentTypes =
                {
                    "application/problem+json",
                    "application/problem+xml",
                },
            };
        }

        internal static void SetTraceId(ActionContext actionContext, ProblemDetails problemDetails)
        {
            var traceId = Activity.Current?.Id ?? actionContext.HttpContext.TraceIdentifier;

            problemDetails.Extensions[TraceIdentifierKey] = traceId;
        }
    }
}