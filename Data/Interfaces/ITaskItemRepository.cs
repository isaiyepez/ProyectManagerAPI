using Models;

namespace Data.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(TaskItem taskItem);
        Task UpdateAsync(TaskItem taskItem);
        Task DeleteAsync(TaskItem taskItem);
        Task<IEnumerable<TaskItem>> GetByPriorityAsync(PriorityTask priority);

        Task<IEnumerable<TaskItem>> GetOverdueTasksAsync();
    }
}
