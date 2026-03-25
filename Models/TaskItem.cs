namespace Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public StatusTask Status { get; set; }
        public PriorityTask Priority { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Project? Project { get; set; }

    }
}
