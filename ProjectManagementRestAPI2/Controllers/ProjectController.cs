using DTOs;
using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetAll()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponseDto>> GetById(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponseDto>> Create(ProjectCreateDto dto)
        {
            var result = await _projectService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProjectUpdateDto dto)
        {
            var result = await _projectService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]                            
        public async Task<IActionResult> Delete(Guid id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/progress")]
        public async Task<ActionResult<double>> GetProgress(Guid id)
        {
            var progress = await _projectService.GetProjectProgressAsync(id);
            return Ok(progress);
        }
    }
}

