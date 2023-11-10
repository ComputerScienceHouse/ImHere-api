namespace ImHere_api;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args); {
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
        }

        var app = builder.Build(); {
            app.UseHttpsRedirection();
            app.MapControllers();
            app.MapHub<AttendanceHub>("/attendanceHub");
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}