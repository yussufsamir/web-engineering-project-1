using fitness_tracker.models;
using fitness_tracker.Services;
using Microsoft.EntityFrameworkCore;
using fitness_tracker.Data;

namespace fitness_tracker.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly AppDbContext _context;

        public AssignmentService(AppDbContext context)
        {
            _context = context;
        }

        public List<WorkoutAssignment> GetAssignmentsByAthleteId(int athleteId)
        {
            return _context.WorkoutAssignments
                .Include(a => a.Athlete)
                .Include(a => a.Wod)
                .Where(a => a.AthleteId == athleteId)
                .ToList();
        }

        public (WorkoutAssignment? Assignment, string? ErrorMessage) CreateAssignment(WorkoutAssignment newAssignment)
        {
            var athleteExists = _context.Athletes.Any(a => a.Id == newAssignment.AthleteId);
            if (!athleteExists)
                return (null, "Athlete does not exist.");

            var wodExists = _context.Wods.Any(w => w.Id == newAssignment.WodId);
            if (!wodExists)
                return (null, "WOD does not exist.");

            newAssignment.AssignedDate = DateTime.Now;

            _context.WorkoutAssignments.Add(newAssignment);
            _context.SaveChanges();

            var createdAssignment = _context.WorkoutAssignments
                .Include(a => a.Athlete)
                .Include(a => a.Wod)
                .FirstOrDefault(a => a.Id == newAssignment.Id);

            return (createdAssignment, null);
        }
    }
}