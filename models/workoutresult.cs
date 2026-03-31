namespace fitness_tracker.models
{
    public class WorkoutResult
    {
        public int Id { get; set; }
        public int WorkoutAssignmentId { get; set; }
        public string Score { get; set; }
        public string TimeTaken { get; set; }
        public int RepsCompleted { get; set; }
        public string RxOrScaled { get; set; }
        public string CoachFeedback { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}