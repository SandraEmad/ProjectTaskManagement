
using ProjectTaskManagement.Application.DTOs.Projects;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync(string userId);
        Task<ProjectDto> CreateAsync(CreateProjectDto dto, string userId);
        Task<ProjectDto> GetByIdAsync(Guid id, string userId);
        Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto dto, string userId);
        Task DeleteAsync(Guid id, string userId);
    }
}
