namespace fitness_tracker.DTOs
{
    public class AssignmentResponseDto
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public string AthleteName { get; set; }
        public int WodId { get; set; }
        public string WodTitle { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}