using BusinessLogic.Contracts;
using Data.Interfaces;
using DTOs;
using Models;

namespace BusinessLogic.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                throw new KeyNotFoundException($"Project with ID {id} not found.");

            bool hasIncompleteTasks = project.TaskItems is not null &&
                                     project.TaskItems.Any(t => t.Status != StatusTask.Completed);

            if (hasIncompleteTasks)
            {
                throw new InvalidOperationException("Cannot delete project with incomplete tasks.");
            }

            await _projectRepository.DeleteAsync(project);
            return true;
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();

           
            return projects.Select(p => MapToResponseDto(p));
        }

        public async Task<ProjectResponseDto> GetByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                throw new KeyNotFoundException($"Project with ID {id} not found.");

            return MapToResponseDto(project);
        }

        public async Task<ProjectResponseDto> CreateAsync(ProjectCreateDto dto)
        {
           
            var newProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Status = StatusProject.Active, 
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(newProject);

      
            return MapToResponseDto(newProject);
        }

        public async Task<ProjectResponseDto> UpdateAsync(Guid id, ProjectUpdateDto dto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);

            if (existingProject is null)
                throw new KeyNotFoundException($"Project with ID {id} not found.");

            if (!IsValidStatusTransition(existingProject, dto.Status))
            {
                throw new InvalidOperationException("Invalid status transition: Cannot finish project with incomplete tasks.");
            }

            existingProject.Name = dto.Name;
            existingProject.Description = dto.Description;
            existingProject.Status = dto.Status;

            await _projectRepository.UpdateAsync(existingProject);
            return MapToResponseDto(existingProject);
        }

        public async Task<double> GetProjectProgressAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                throw new KeyNotFoundException($"Project with ID {id} not found.");

            return CalculateProgress(project.TaskItems);
        }

        private ProjectResponseDto MapToResponseDto(Project project)
        {
            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status.ToString(),
                CreatedAt = project.CreatedAt,
                CompletionPercentage = CalculateProgress(project.TaskItems)
            };
        }
        private double CalculateProgress(ICollection<TaskItem>? tasks)
        {
            if (tasks == null || !tasks.Any()) return 0;

            var completedTasks = tasks.Count(t => t.Status == StatusTask.Completed);
            return Math.Round((double)completedTasks / tasks.Count() * 100, 2);
        }


        private bool IsValidStatusTransition(Project previousProject, StatusProject updatedProjectStatus)
        {
            if (previousProject.Status == updatedProjectStatus) return true; 

            if(updatedProjectStatus == StatusProject.Finished && 
                (previousProject.TaskItems is not null && previousProject.TaskItems.Any(t => t.Status != StatusTask.Completed))) { 
               
                return false;
            }

            return true;
        }

    }
}
