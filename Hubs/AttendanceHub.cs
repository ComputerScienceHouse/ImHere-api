using ImHereAPI.Controllers;
using ImHereAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ImHereAPI.Hubs {
    public class AttendanceHub : Hub {
        private readonly ILogger<AttendanceHub> logger;

        public AttendanceHub(ILogger<AttendanceHub> logger) 
            => this.logger = logger;

        public static async Task DispatchMemberSignIn(IHubContext<AttendanceHub> hub, string hostID, Member member) {
            Console.WriteLine($"Dispatching \"{member.preferred_username} signed in\" to {hostID}"); // ill find a way to use loggeer here eventually
            await hub.Clients.All.SendAsync("MemberSignedIn", JsonSerializer.Serialize(member), hostID);
        }

        public void Takeown(uint attendanceID, string hostUID) {
            logger.LogInformation($"New attendance taker connected: {hostUID}");
            lock (AttendanceController.AttendancesLock)
                if (AttendanceController.Attendances.ContainsKey(attendanceID))
                    AttendanceController.Attendances[attendanceID].HostUID = hostUID;
        }

        public static event Action<uint, string> MemberDeleted = delegate { };
        public void MemberDelete(uint attendanceID, string username) {
            // invoke event
            MemberDeleted.Invoke(attendanceID, username);
        }
    }
}
