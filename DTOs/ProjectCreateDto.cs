using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ProjectCreateDto
    {
        [Required(ErrorMessage = "Project name is mandatory.")]
        [StringLength(100, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Description { get; set; }
    }
}
