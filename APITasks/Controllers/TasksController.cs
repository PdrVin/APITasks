using Microsoft.AspNetCore.Mvc;
using APITasks.DTO;
using APITasks.Interfaces;
using APITasks.Models;
using APITasks.Models.Errors;
using APITasks.Views;

namespace APITasks.Controllers;

[ApiController]
[Route("/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        var tasks = await _service.GetAllTasksAsync(page);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Select([FromRoute] int id)
    {
        try
        {
            var task = await _service.GetTaskByIdAsync(id);
            return Ok(task);
        }
        catch (TaskError error)
        {
            return NotFound(new ErrorView { Message = error.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskDto taskDto)
    {
        try
        {
            var task = await _service.AddTaskAsync(taskDto);
            return CreatedAtAction(nameof(Select), new { id = task.Id }, task);
        }
        catch (TaskError error)
        {
            return BadRequest(new ErrorView { Message = error.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TaskDto taskDto)
    {
        try
        {
            var task = await _service.UpdateTaskAsync(id, taskDto);
            return Ok(task);
        }
        catch (TaskError error)
        {
            return BadRequest(new ErrorView { Message = error.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _service.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (TaskError error)
        {
            return BadRequest(new ErrorView { Message = error.Message });
        }
    }
}
