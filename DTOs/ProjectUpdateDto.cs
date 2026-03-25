using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ProjectUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        public StatusProject Status { get; set; }
    }
}
