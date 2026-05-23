using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.API.Extensions;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service) => _service = service;

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(Guid projectId)
        {
            var tasks = await _service.GetByProjectAsync(projectId, User.GetUserId());
            return Ok(ApiResponse<List<TaskDto>>.Ok(tasks));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            var task = await _service.CreateAsync(dto, User.GetUserId());
            return CreatedAtAction(nameof(GetByProject), new { projectId = task.ProjectId },
                ApiResponse<TaskDto>.Ok(task, "Task created"));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTaskStatusDto dto)
        {
            var task = await _service.UpdateStatusAsync(id, dto, User.GetUserId());
            return Ok(ApiResponse<TaskDto>.Ok(task));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id, User.GetUserId());
            return Ok(ApiResponse<object>.Ok(null!, "Deleted successfully"));
        }
    }
}