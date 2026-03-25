using BusinessLogic.Contracts;
using Data.Interfaces;
using DTOs;
using Models;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IProjectRepository _projectRepository; 

        public TaskItemService(ITaskItemRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(MapToResponseDto);
        }

        public async Task<TaskItemResponseDto?> GetByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            return MapToResponseDto(task);
        }

        public async Task<TaskItemResponseDto> CreateAsync(TaskItemCreateDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {dto.ProjectId} not found.");
            }

            if (dto.DueDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Due date cannot be in the past.");
            }

            var newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId,
                Status = StatusTask.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(newTask);

            return MapToResponseDto(newTask);
        }

        public async Task<TaskItemResponseDto?> UpdateAsync(Guid id, TaskItemUpdateDto dto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);

            if (existingTask == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            if (dto.DueDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Due date cannot be in the past.");
            }

            if (!IsValidStatusTransition(existingTask.Status, dto.Status))
            {
                throw new InvalidOperationException($"Invalid status transition from {existingTask.Status} to {dto.Status}. Tasks must go to InProgress before Completed.");
            }

            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.Priority = dto.Priority;
            existingTask.DueDate = dto.DueDate;
            existingTask.Status = dto.Status;

            await _taskRepository.UpdateAsync(existingTask);

            return MapToResponseDto(existingTask);
        }

        public async Task<TaskItemResponseDto?> UpdateStatusAsync(Guid id, StatusTask newStatus)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);

            if (existingTask == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            if (!IsValidStatusTransition(existingTask.Status, newStatus))
            {
                throw new InvalidOperationException($"Invalid status transition from {existingTask.Status} to {newStatus}.");
            }

            existingTask.Status = newStatus;

            await _taskRepository.UpdateAsync(existingTask);

            return MapToResponseDto(existingTask);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            await _taskRepository.DeleteAsync(task);
            return true;
        }

        public async Task<IEnumerable<TaskItemResponseDto>> GetByPriorityAsync(PriorityTask priority)
        {
            var tasks = await _taskRepository.GetByPriorityAsync(priority);
            return tasks.Select(MapToResponseDto);
        }
        public async Task<IEnumerable<TaskItemResponseDto>> GetOverdueTasksAsync()
        {
            var tasks = await _taskRepository.GetOverdueTasksAsync();
            return tasks.Select(MapToResponseDto);
        }

        private TaskItemResponseDto MapToResponseDto(TaskItem task)
        {
            return new TaskItemResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId
            };
        }

        private bool IsValidStatusTransition(StatusTask current, StatusTask next)
        {
            if (current == next) return true;

            if (current == StatusTask.Pending && next == StatusTask.Completed)
            {
                return false;
            }

            return true;
        }
    }
}
