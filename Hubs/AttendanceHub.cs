using ImHereAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ImHereAPI.Hubs {
    public class AttendanceHub : Hub {
        public static async Task DispatchMemberSignIn(IHubContext<AttendanceHub> hub, uint hostID, Member member) {
            await hub.Clients.User($"host_{hostID}").SendAsync("MemberSignedIn", JsonSerializer.Serialize(member));
        }

        public static event Action<uint, string> MemberDeleted = delegate { };
        public void MemberDelete(uint attendanceID, string username) {
            // invoke event
            MemberDeleted.Invoke(attendanceID, username);
        }
    }
}
