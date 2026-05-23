using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.API.Extensions;

namespace ProjectTaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _service.GetAllAsync(User.GetUserId());
            return Ok(ApiResponse<List<ProjectDto>>.Ok(projects));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _service.GetByIdAsync(id, User.GetUserId());
            return Ok(ApiResponse<ProjectDto>.Ok(project));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
        {
            var project = await _service.CreateAsync(dto, User.GetUserId());
            return CreatedAtAction(nameof(GetById), new { id = project.Id },
                ApiResponse<ProjectDto>.Ok(project, "Project created")); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto dto)
        {
            var project = await _service.UpdateAsync(id, dto, User.GetUserId());
            return Ok(ApiResponse<ProjectDto>.Ok(project));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id, User.GetUserId());
            return Ok(ApiResponse<object>.Ok(null!, "Deleted successfully"));
        }
    }
}