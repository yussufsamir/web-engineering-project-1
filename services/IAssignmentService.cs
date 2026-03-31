using fitness_tracker.models;

namespace fitness_tracker.Services
{
    public interface IAssignmentService
    {
        List<WorkoutAssignment> GetAssignmentsByAthleteId(int athleteId);
        (WorkoutAssignment? Assignment, string? ErrorMessage) CreateAssignment(WorkoutAssignment newAssignment);
    }
}