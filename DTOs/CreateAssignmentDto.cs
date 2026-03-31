using System.ComponentModel.DataAnnotations;

namespace fitness_tracker.DTOs
{
    public class CreateAssignmentDto
    {
        [Required]
        public int AthleteId { get; set; }

        [Required]
        public int WodId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string Notes { get; set; }
    }
}