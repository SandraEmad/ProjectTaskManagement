using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Exceptions;
using ProjectTaskManagement.Infrastructure.Data;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context) => _context = context;

        public async Task<List<ProjectDto>> GetAllAsync(string userId) 
            => await _context.Projects
                .Where(p => p.UserId == userId)
                .Select(p => MapToDto(p))
                .ToListAsync();

        public async Task<ProjectDto> GetByIdAsync(Guid id, string userId) 
        {
            var p = await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId)
                ?? throw new NotFoundException("Project", id);

            return MapToDto(p);
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, string userId)
        {
            var project = new Project
            {
               
                Name = dto.Name,
                Description = dto.Description,
                UserId = userId
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return MapToDto(project);
        }

        public async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto dto, string userId) 
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId)
                ?? throw new NotFoundException("Project", id);

            project.Name = dto.Name ?? project.Name;                  
            project.Description = dto.Description ?? project.Description; 

            await _context.SaveChangesAsync();
            return MapToDto(project);
        }

        public async Task DeleteAsync(Guid id, string userId) 
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId)
                ?? throw new NotFoundException("Project", id);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        private static ProjectDto MapToDto(Project p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CreatedAt = p.CreatedAt
        };
    }
}