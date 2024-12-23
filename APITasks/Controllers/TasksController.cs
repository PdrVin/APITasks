using Microsoft.AspNetCore.Mvc;
using APITasks.Database;
using APITasks.DTO;
using APITasks.Models;
using APITasks.Models.Errors;
using APITasks.Services.Interfaces;
using APITasks.Views;

namespace APITasks.Controllers;

[ApiController]
[Route("/tasks")]
public class TasksController : ControllerBase
{
    private ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(int page = 1)
    {
        var tasks = _service.TaskList(page);
        return StatusCode(200, tasks);
    }

    [HttpGet("{id}")]
    public IActionResult Select([FromRoute] int id)
    {
        try
        {
            var task = _service.SelectById(id);
            return StatusCode(200, task);
        }
        catch (TaskError error)
        {
            return StatusCode(404, new ErrorView { Message = error.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] TaskDto taskDto)
    {
        try
        {
            var task = _service.Include(taskDto);
            return StatusCode(201, task);
        }
        catch (TaskError error)
        {
            return StatusCode(400, new ErrorView { Message = error.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] TaskDto taskDto)
    {
        try
        {
            var task = _service.Update(id, taskDto);
            return StatusCode(200, task);
        }
        catch (TaskError error)
        {
            return StatusCode(400, new ErrorView { Message = error.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _service.Delete(id);
            return StatusCode(204);
        }
        catch (TaskError error) 
        {
            return StatusCode(400, new ErrorView { Message = error.Message });
        }
    }
}


