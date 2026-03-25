namespace Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public StatusProject Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<TaskItem>? TaskItems { get; set; }
    }
}
