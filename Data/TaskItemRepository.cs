using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems
                .Include(t => t.Project) 
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems
                .Include(t => t.Project) 
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.TaskItems
                .Where(t => t.ProjectId == projectId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(TaskItem taskItem)
        {
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem taskItem)
        {
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TaskItem>> GetByPriorityAsync(PriorityTask priority)
        {
            return await _context.TaskItems
                .Where(t => t.Priority == priority)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<TaskItem>> GetOverdueTasksAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.TaskItems
                .Where(t => t.DueDate < now && t.Status != StatusTask.Completed)
                .AsNoTracking()
                .ToListAsync();
        }


    }
}