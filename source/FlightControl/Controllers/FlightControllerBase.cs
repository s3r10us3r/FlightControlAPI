using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Controllers
{
    public abstract class FlightControllerBase : ControllerBase
    {
        private readonly ILogger _logger;
        protected ILogger Logger => _logger;

        public FlightControllerBase(ILogger<FlightControllerBase> logger)
        {
            _logger = logger;
        }

        protected IActionResult InternalServerError(Exception e)
        {
            _logger.LogError("Error occured while processing reequest {Error}", e.Message);
            return StatusCode(500, new { error = new { code = 500, nessage = "An internal server error occured." } });
        }

        protected IActionResult BadRequestWError(string message)
        {
            return BadRequest(new { error = new { code = 400, message = message } });
        }
    }
}
