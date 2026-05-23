using ProjectTaskManagement.Application.DTOs.Tasks;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetByProjectAsync(Guid projectId, string userId);
        Task<TaskDto> CreateAsync(CreateTaskDto dto, string userId);
        Task<TaskDto> UpdateStatusAsync(Guid id, UpdateTaskStatusDto dto, string userId);
        Task DeleteAsync(Guid id, string userId);
    }
}