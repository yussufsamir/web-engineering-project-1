using fitness_tracker.models;
using fitness_tracker.Data;
using Microsoft.EntityFrameworkCore;

namespace fitness_tracker.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly AppDbContext _context;

        public AssignmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutAssignment>> GetAssignmentsByAthleteIdAsync(int athleteId)
        {
            return await _context.WorkoutAssignments
                .AsNoTracking()
                .Include(a => a.Athlete)
                .Include(a => a.Wod)
                .Where(a => a.AthleteId == athleteId)
                .ToListAsync();
        }

        public async Task<(WorkoutAssignment? Assignment, string? ErrorMessage)> CreateAssignmentAsync(WorkoutAssignment newAssignment)
        {
            var athleteExists = await _context.Athletes.AnyAsync(a => a.Id == newAssignment.AthleteId);
            if (!athleteExists)
                return (null, "Athlete does not exist.");

            var wodExists = await _context.Wods.AnyAsync(w => w.Id == newAssignment.WodId);
            if (!wodExists)
                return (null, "WOD does not exist.");

            newAssignment.AssignedDate = DateTime.Now;

            _context.WorkoutAssignments.Add(newAssignment);
            await _context.SaveChangesAsync();

            var createdAssignment = await _context.WorkoutAssignments
                .AsNoTracking()
                .Include(a => a.Athlete)
                .Include(a => a.Wod)
                .FirstOrDefaultAsync(a => a.Id == newAssignment.Id);

            return (createdAssignment, null);
        }

        public async Task<WorkoutAssignment?> UpdateAssignmentAsync(int id, WorkoutAssignment updatedAssignment)
        {
            var assignment = await _context.WorkoutAssignments
                .Include(a => a.Athlete)
                .Include(a => a.Wod)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assignment == null)
                return null;

            assignment.DueDate = updatedAssignment.DueDate;
            assignment.Status = updatedAssignment.Status;
            assignment.Notes = updatedAssignment.Notes;

            await _context.SaveChangesAsync();
            return assignment;
        }
    }
}