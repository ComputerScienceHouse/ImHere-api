using ImHere_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ImHere_api.Controllers;

/// Here are the routes that will be used
/// POST /api/attendence sign into attendance
///     
/// GET  /api/rtc gets a signalR websocket to a new attendence session 
///     Accessed by the event hoster

[ApiController]
[Route("api/attendance")]
public class AttendanceController : Controller {
    private static int id = 0;
    private static int ID { get => id++; }
    private IHubContext<AttendanceHub> AttendanceRTC { get; init; }

    public AttendanceController(IHubContext<AttendanceHub> attendanceHub) 
        => AttendanceRTC = attendanceHub;

    [HttpPost("{id}")]
    public async Task<IActionResult> SignIn(string id, Member member) {
        // check if attendance exists
        if (AttendanceHub.AttendanceExists(id))
            return NotFound("Event does not Exist.");
        // if it does, send a message to the attendance hub
        await AttendanceRTC.Clients.User(id).SendAsync("SignIn", member);
        // wait for a response from the attendance hub
        // todo, implement this
        // in the meantime, the signin webpage displays a message:
        // see your name onscreen? If not try again, or ask the host to enter your name manually.
        return Ok();
    }

    [HttpPost("create/{name}")]
    public IActionResult CreateAttendance(Attendance attendance, string name) {
        attendance.Id = ID;
        attendance.Date = DateTime.Now;
        // add attendance to the attendance hub
        AttendanceHub.Attendances.Add(attendance.Id.ToString(), attendance);
        return Created(Url.Action("SignIn", new { id = attendance.Id }) ?? "URI n/a", attendance);
    }
    
}
