using Microsoft.AspNetCore.Mvc;

namespace ImHereAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : Controller {
        [HttpPost("{id}")]
        public IActionResult SignIn(int id) {
            return Ok($"api/attendance/{id}: HELLO WORLD!");
        }
    }
}
