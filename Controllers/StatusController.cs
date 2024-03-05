using Microsoft.AspNetCore.Mvc;

namespace net_test_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase {
        [HttpGet("/status")]
        public IActionResult GetStatus() {
            return Ok("active");
        }
    }
}