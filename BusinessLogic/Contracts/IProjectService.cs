using DTOs;
namespace BusinessLogic.Contracts
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
        Task<ProjectResponseDto> GetByIdAsync(Guid id);
        Task<ProjectResponseDto> CreateAsync(ProjectCreateDto projectDto);
        Task<ProjectResponseDto> UpdateAsync(Guid id, ProjectUpdateDto projectDto);
        Task<bool> DeleteAsync(Guid id); 
        Task<double> GetProjectProgressAsync(Guid id);

    }
}
