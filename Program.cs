using ImHereAPI.Hubs;

namespace ImHereAPI;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddControllers();
            builder.Services.AddSignalR();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
        }

        var app = builder.Build();
        {
            app.UseHttpsRedirection();
            app.UseCors();
            
            app.MapControllers();
            app.MapGet("/", () => "Nobody is home...");
            app.MapHub<AttendanceHub>("/rtc/hub");


            app.Run();
        }
    }
}