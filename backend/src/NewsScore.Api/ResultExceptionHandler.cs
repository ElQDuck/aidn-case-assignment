using NewsScore.BusinessLogic.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NewsScore.Api;

public class ResultExceptionHandler: IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ResultExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    /// <summary>
    /// Handles the exception of the result for the HTTP request.
    /// Only handles <see cref="ResultException"/> instances. If the exception is not a
    /// <see cref="ResultException"/>, the exception is left unhandled for the middleware.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> for the current HTTP request.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ResultException resultException)
        {
            return true;
        }

        var exceptionDetails = new ProblemDetails
        {
            Status = resultException.StatusCode,
            Title = resultException.Error,
            Detail = resultException.Message,
            Type = "Bad Request"
        };

        httpContext.Response.StatusCode = exceptionDetails.Status.Value;
        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = exceptionDetails
            });
    }
}