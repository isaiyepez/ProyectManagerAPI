using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class TaskItemCreateDto
    {
        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Description { get; set; }

        [Required]
        public PriorityTask Priority { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}