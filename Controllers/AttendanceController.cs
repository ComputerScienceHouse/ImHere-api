using ImHereAPI.Hubs;
using ImHereAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ImHereAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : Controller {
        public static readonly object AttendancesLock = new();
        public static Dictionary<uint, Attendance> Attendances { get; } = new();
        private IHubContext<AttendanceHub> Hub { get; init; }

        public AttendanceController(IHubContext<AttendanceHub> hub) {
            Hub = hub;
            AttendanceHub.MemberDeleted += OnMemberDeleted;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SignIn(uint id, Member member) {
            if (!Attendances.ContainsKey(id))
                return NotFound("Attendance does not exist.");
            lock (AttendancesLock) {
                if (Attendances[id].Members.ContainsKey(member.preferred_username))
                    return BadRequest("Already signed in");
                if (Attendances[id].Blacklist.Contains(member.preferred_username))
                    return StatusCode(418, "You are blacklisted for not being present.");
                Attendances[id].Members.Add(member.preferred_username, member);
            }
            // dispatch message to host
            await AttendanceHub.DispatchMemberSignIn(Hub, id, member);
            // wait for ack
            // todo
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateAttendance(Attendance attendance) {
            Attendance newAttendance = new(attendance.Name);
            lock (AttendancesLock)
                Attendances.Add(newAttendance.ID, newAttendance);
            return Ok(newAttendance.ID);
        }

        private static void OnMemberDeleted(uint attendanceID, string username) {
            lock(AttendancesLock) {
                Attendances[attendanceID].Members.Remove(username);
                Attendances[attendanceID].Blacklist.Add(username);
            }
        }
    }
}
