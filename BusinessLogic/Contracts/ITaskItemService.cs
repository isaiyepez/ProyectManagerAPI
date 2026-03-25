using DTOs;

namespace BusinessLogic.Contracts
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();

        Task<TaskItemResponseDto?> GetByIdAsync(Guid id);

        Task<TaskItemResponseDto> CreateAsync(TaskItemCreateDto taskDto);

        Task<TaskItemResponseDto?> UpdateAsync(Guid id, TaskItemUpdateDto taskDto);

        Task<TaskItemResponseDto?> UpdateStatusAsync(Guid id, StatusTask newStatus);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<TaskItemResponseDto>> GetByPriorityAsync(PriorityTask priority);

        Task<IEnumerable<TaskItemResponseDto>> GetOverdueTasksAsync();
    }
}