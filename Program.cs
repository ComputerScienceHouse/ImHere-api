namespace ImHere;
public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();

        /// Here are the routes that will be used
        /// POST /api/attendence create attendence using the json body containing the array of users, and the event info
        ///     Accessed by the event hoster
        /// GET  /api/rtc gets a signalR websocket to a new attendence session 
        ///     Accessed by the event hoster
        /// Viewing, adding, removing users from the attendence list is done through the signalR websocket
    }
}