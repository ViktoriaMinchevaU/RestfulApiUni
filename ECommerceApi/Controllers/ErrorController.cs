using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            // Log the exception
            // _logger.LogError(exception, "An unhandled exception occurred.");

            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred!",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception?.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}