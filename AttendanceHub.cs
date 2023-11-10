using ImHere_api.Models;
using Microsoft.AspNetCore.SignalR;

namespace ImHere_api;
// TODO: Add functionality to spinlock (with a timeout ofc)
// waiting for the meeting host's reponse to a member joining a meeting
public class AttendanceHub : Hub {
    public static Dictionary<string, Attendance?> Attendances { get; } = new();

    public static bool AttendanceExists(string id)
        => Attendances.ContainsKey(id);
}