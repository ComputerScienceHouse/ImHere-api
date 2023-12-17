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
        private readonly ILogger<AttendanceController> logger;

        public AttendanceController(IHubContext<AttendanceHub> hub, ILogger<AttendanceController> logger)
        {
            Hub = hub;
            AttendanceHub.MemberDeleted += OnMemberDeleted;
            this.logger = logger;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SignIn(uint id, Member member) {
            logger.LogInformation($"PUT /api/attendance/{id} | {member.preferred_username}");
            if (!Attendances.ContainsKey(id))
                return NotFound("Attendance does not exist");
            Attendance response;
            string hostUID;
            lock (AttendancesLock) {
                // check if host is ready to recieve messages
                if (Attendances[id].HostUID == string.Empty)
                    return StatusCode(503, "Host is not ready to take attendance. This message shouldnt show up but if it does just refresh.");
                hostUID = Attendances[id].HostUID;

                if (Attendances[id].Members.ContainsKey(member.preferred_username))
                    return BadRequest("Already signed in");
                if (Attendances[id].Blacklist.Contains(member.preferred_username))
                    return StatusCode(418, "You are blacklisted for not being present");
                Attendances[id].Members.Add(member.preferred_username, member);
                response = new(Attendances[id].Name);
            }
            // dispatch message to host
            await AttendanceHub.DispatchMemberSignIn(Hub, hostUID, member);
            // wait for ack
            // todo
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateAttendance(Attendance attendance) {
            logger.LogInformation($"POST /api/attendance | {attendance.Name}");
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
