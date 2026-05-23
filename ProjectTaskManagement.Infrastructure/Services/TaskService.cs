using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Exceptions;
using ProjectTaskManagement.Infrastructure.Data;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context) => _context = context;

        public async Task<List<TaskDto>> GetByProjectAsync(Guid projectId, string userId) 
        {
            var projectExists = await _context.Projects
                .AnyAsync(p => p.Id == projectId && p.UserId == userId);

            if (!projectExists)
                throw new NotFoundException("Project", projectId);

            return await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Select(t => MapToDto(t))
                .ToListAsync();
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto, string userId)
        {
            var projectExists = await _context.Projects
                .AnyAsync(p => p.Id == dto.ProjectId && p.UserId == userId);

            if (!projectExists)
                throw new NotFoundException("Project", dto.ProjectId);

            var task = new ProjectTask
            {
         
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                ProjectId = dto.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return MapToDto(task);
        }

        public async Task<TaskDto> UpdateStatusAsync(Guid id, UpdateTaskStatusDto dto, string userId) 
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.UserId == userId)
                ?? throw new NotFoundException("Task", id);

            task.Status = dto.Status;
            await _context.SaveChangesAsync();
            return MapToDto(task);
        }

        public async Task DeleteAsync(Guid id, string userId) 
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.UserId == userId)
                ?? throw new NotFoundException("Task", id);

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        private static TaskDto MapToDto(ProjectTask t) => new()
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            DueDate = t.DueDate,
            CreatedAt = t.CreatedAt,
            ProjectId = t.ProjectId
        };
    }
}