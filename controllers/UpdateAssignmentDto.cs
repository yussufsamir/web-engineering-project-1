using System.ComponentModel.DataAnnotations;

namespace fitness_tracker.DTOs
{
    public class UpdateAssignmentDto
    {
        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; }

        public string? Notes { get; set; }
    }
}