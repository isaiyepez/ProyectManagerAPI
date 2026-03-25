using BusinessLogic.Contracts;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskService;

        public TaskItemController(ITaskItemService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemResponseDto>>> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemResponseDto>> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemResponseDto>> Create([FromBody] TaskItemCreateDto dto)
        {
            var result = await _taskService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemResponseDto>> Update(Guid id, [FromBody] TaskItemUpdateDto dto)
        {
            var result = await _taskService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<TaskItemResponseDto>> UpdateStatus(Guid id, [FromBody] StatusTask newStatus)
        {
            var result = await _taskService.UpdateStatusAsync(id, newStatus);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IEnumerable<TaskItemResponseDto>>> GetByPriority(PriorityTask priority)
        {
            var tasks = await _taskService.GetByPriorityAsync(priority);
            return Ok(tasks);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<TaskItemResponseDto>>> GetOverdue()
        {
            var tasks = await _taskService.GetOverdueTasksAsync();
            return Ok(tasks);
        }
    }
}