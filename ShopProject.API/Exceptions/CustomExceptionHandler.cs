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
            
            if (exception is ValidationException validationexception)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred.",
                    Extensions = { ["errors"] = validationexception.Errors.Select(e => e.ErrorMessage) }
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            if (exception.Message.Contains("Out of Stock") || exception.Message.Contains("out of stock"))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message 
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }


            if (exception.Message.Contains("not found"))
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var genericProblem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = "An unexpected error occurred."
            };
            await httpContext.Response.WriteAsJsonAsync(genericProblem, cancellationToken);

            return true; 
        }
    }
    }

