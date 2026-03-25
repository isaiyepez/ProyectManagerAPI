using Models;

namespace Data.Interfaces
{
    public interface IProjectRepository
    {
        Task DeleteAsync(Project project);
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync(); 
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
    }
}
