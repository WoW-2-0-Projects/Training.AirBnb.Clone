using Backend_Project.Domain.Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AirBnb.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var problemDetails = context.Exception switch
        {
            EntityNotFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Detail = context.Exception.Message
            },

            DuplicateEntityException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Detail = context.Exception.Message
            },

            EntityNotUpdatableException => new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden, 
                Detail = context.Exception.Message
            },

            EntityNotDeletableException => new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Detail = context.Exception.Message
            },

            EntityValidationException => new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = context.Exception.Message
            },

            ArgumentException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = context.Exception.Message
            },

            InvalidOperationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = context.Exception.Message
            },

            Exception => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = context.Exception.Message
            }
        };

        context.ExceptionHandled = true;

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }
}