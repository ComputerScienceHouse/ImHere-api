using ImHere_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ImHere_api.InMemory;
public class AttendanceContext : DbContext {
    public AttendanceContext(DbContextOptions<AttendanceContext> options) : base(options) { }

    public DbSet<Attendance> Attendances { get; set; } = null!;
}
