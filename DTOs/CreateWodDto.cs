using System.ComponentModel.DataAnnotations;

namespace fitness_tracker.DTOs
{
    public class CreateWodDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(50)]
        public string Difficulty { get; set; }

        [Required]
        public int CoachId { get; set; }
    }
}