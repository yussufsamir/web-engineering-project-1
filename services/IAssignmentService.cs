using fitness_tracker.models;

namespace fitness_tracker.Services
{
    public interface IAssignmentService
    {
        Task<List<WorkoutAssignment>> GetAssignmentsByAthleteIdAsync(int athleteId);
        Task<(WorkoutAssignment? Assignment, string? ErrorMessage)> CreateAssignmentAsync(WorkoutAssignment newAssignment);
    }
}