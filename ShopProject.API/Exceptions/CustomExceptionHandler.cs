using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ValidationException = FluentValidation.ValidationException;

namespace ShopProject.API.Exceptions
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationexception) {

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var proplemDetails = new ProblemDetails
                { 
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "Errors in Data",
                    Extensions = { ["errors"] = validationexception.Errors.Select(e => e.ErrorMessage) }

                };

                await httpContext.Response.WriteAsJsonAsync(proplemDetails, cancellationToken);
                return true;
            }
            return false;
        }
    }
}
