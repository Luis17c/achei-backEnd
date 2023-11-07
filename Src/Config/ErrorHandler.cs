using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Config {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandler : ControllerBase {
        
        [Route("/error")]
        public ActionResult Handle() => 
            Problem();

        [Route("/error-development")]
        public ActionResult HandleDevelopment( [FromServices] IHostEnvironment hostEnvironment ) {
            if (!hostEnvironment.IsDevelopment()) {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message
            );
        }
    };
}