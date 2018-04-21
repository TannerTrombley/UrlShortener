using Common.DataModels.PublicDataModels.ApiResponses;
using Common.Exceptions.ApiExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UrlShortener.Controllers.Common
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ApiNotFoundException e:
                    context.Result = new ObjectResult(new ApiResponse(404, e.Message));
                    break;
                default:
                    return;
            }
            context.ExceptionHandled = true;
        }
    }
}
