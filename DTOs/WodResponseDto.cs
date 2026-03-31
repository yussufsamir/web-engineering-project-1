namespace fitness_tracker.DTOs
{
    public class WodResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}