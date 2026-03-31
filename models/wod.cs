namespace fitness_tracker.models
{
    public class Wod
    {
        public List<WorkoutAssignment> Assignments { get; set; } //
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CoachId { get; set; }
    }
}