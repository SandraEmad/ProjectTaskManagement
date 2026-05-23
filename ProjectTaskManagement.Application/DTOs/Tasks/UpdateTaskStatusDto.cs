using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks
{
    public class UpdateTaskStatusDto
    {
        public ProjectTaskStatus Status { get; set; }
    }
}
