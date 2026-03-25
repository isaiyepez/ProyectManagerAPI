using Microsoft.EntityFrameworkCore;

using Models;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.TaskItems)
                .WithOne(d => d.Project)
                .HasForeignKey(d => d.ProjectId);

           
            var projectId = Guid.NewGuid();

            modelBuilder.Entity<Project>().HasData(new Project
            {
                Id = projectId,
                Name = "Project Manager v1",
                Description = "Initial Project API",
                Status = StatusProject.Active,
                CreatedAt = DateTime.UtcNow
               
            });

           
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    Title = "Configure database",
                    Description = "Install packages and run migrations",
                    Status = StatusTask.Completed,
                    Priority = PriorityTask.High,
                    DueDate = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    Title = "Generate Repo",
                    Description = "Implementation of Repository Pattern for the Project",
                    Status = StatusTask.InProgress,
                    Priority = PriorityTask.Medium,
                    DueDate = DateTime.UtcNow.AddDays(10),
                    CreatedAt = DateTime.UtcNow
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    Title = "Tech documentation",
                    Description = "Generate README of the Project",
                    Status = StatusTask.Pending,
                    Priority = PriorityTask.Low,
                    DueDate = DateTime.UtcNow.AddDays(15),
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}