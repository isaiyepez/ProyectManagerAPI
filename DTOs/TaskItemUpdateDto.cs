using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class TaskItemUpdateDto
    {
        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Description { get; set; }

        [Required]
        public StatusTask Status { get; set; }

        [Required]
        public PriorityTask Priority { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}