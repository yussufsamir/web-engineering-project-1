namespace fitness_tracker.models
{
    public class Athlete
    {
        public List<WorkoutAssignment> Assignments { get; set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}