namespace DTOs
{
    public class TaskItemResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProjectId { get; set; }
    }
}