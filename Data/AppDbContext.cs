using Microsoft.EntityFrameworkCore;
using fitness_tracker.models;

namespace fitness_tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<AthleteProfile> AthleteProfiles { get; set; }
        public DbSet<Wod> Wods { get; set; }
        public DbSet<WorkoutAssignment> WorkoutAssignments { get; set; }
        public DbSet<WorkoutResult> WorkoutResults { get; set; }
    }
}