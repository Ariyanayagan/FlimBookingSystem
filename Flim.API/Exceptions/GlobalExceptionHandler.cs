using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Flim.API.Exceptions
{
    /// <summary>
    /// Global Excebtion handler for internal server error.
    /// </summary>
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Log.Error(exception: exception, "Exception");
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = exception.Message,
                
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
