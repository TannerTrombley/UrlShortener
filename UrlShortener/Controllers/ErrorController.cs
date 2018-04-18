using Common.DataModels.PublicDataModels.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [Produces("application/json")]
    public class ErrorController : Controller
    {
        [Route("api/Error/{code}")]
        public IActionResult HandleError(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}