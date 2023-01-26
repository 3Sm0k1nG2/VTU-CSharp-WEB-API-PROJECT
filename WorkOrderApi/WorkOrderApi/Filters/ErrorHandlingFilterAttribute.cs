using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using WorkOrders_DAL;
using WorkOrders_DAL.Errors;

namespace WorkOrderApi.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            ProblemDetails problemDetails;

            switch (exception.Message)
            {
                case Constants.ERROR_ENTITY_NOT_FOUND:
                    problemDetails = new ProblemDetails()
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Entity not found.",
                        Status = (int)HttpStatusCode.NotFound,
                    };

                    break;
                case Constants.ERROR_DUPLICATE_KEY_FOUND:
                    problemDetails = new ProblemDetails()
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Title = "Entity already exists.",
                        Status = (int)HttpStatusCode.BadRequest,
                    };

                    break;
                case Constants.ERROR_DTO_IS_EMPTY:
                    problemDetails = new ProblemDetails()
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.6",
                        Title = "Request body is empty.",
                        Status = (int)HttpStatusCode.NotAcceptable,
                        Detail = "The request body is required."
                    };

                    break;
                case Constants.ERROR_FIELD_IS_EMPTY:
                    problemDetails = new ProblemDetails()
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.6",
                        Title = "Request body not including all required fields.",
                        Status = (int)HttpStatusCode.NotAcceptable,
                        Detail = "There are missing required fields in the request body."
                    };
                    break;
                case Constants.ERROR_TECHS_COUNT_NOT_MATCHING:
                    problemDetails = new ProblemDetails()
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.6",
                        Title = "The request field 'techs' does not match any technicians count.",
                        Status = (int)HttpStatusCode.NotAcceptable,
                        Detail = "To fix this issue: Add the technicians' count and labor rate. ( endpoint: /api/LaborRate ) "
                    };
                    break;
                default:
                    problemDetails = new ProblemDetails()
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                        Title = "An unexpected error occured while processing your request.",
                        Status = (int)HttpStatusCode.InternalServerError,
                    };

                    break;
            }

            context.Result = new ObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }
    }
}
