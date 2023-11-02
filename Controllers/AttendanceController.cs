using Microsoft.AspNetCore.Mvc;

namespace ImHere_api.Controllers;

[ApiController]
public class AttendanceController : Controller {
    [HttpGet("/api")]
    public IActionResult GetSum() {
        return Ok("Bazinga");
    }
}
