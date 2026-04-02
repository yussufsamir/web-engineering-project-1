namespace fitness_tracker.DTOs
{
    public class CoachDashboardDto
    {
        public int TotalAthletes { get; set; }
        public int TotalWods { get; set; }
        public int TotalAssignments { get; set; }
        public List<AssignmentResponseDto> RecentAssignments { get; set; }
    }
}