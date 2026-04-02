using System.ComponentModel.DataAnnotations;

namespace fitness_tracker.DTOs
{
    public class UpdateWodDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

        [Required]
        public string Difficulty { get; set; }
    }
}