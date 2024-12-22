using Microsoft.AspNetCore.Mvc;
using APITasks.Database;
using APITasks.DTO;
using APITasks.Models;
using APITasks.Views;

namespace APITasks.Controllers;

[ApiController]
[Route("/tasks")]
public class TasksController : ControllerBase
{
    private TaskContext _db;

    public TasksController(TaskContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var tasks = _db.Tasks.ToList();
        return StatusCode(200, tasks);
    }

    [HttpGet("{id}")]
    public IActionResult Select([FromRoute] int id)
    {
        var task = _db.Tasks.Find(id);
        if (task == null)
            return StatusCode(404, new ErrorView { Message = $"Id ({id}) Não Encontrado!" });

        return StatusCode(200, task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TaskDto taskDto)
    {
        if (string.IsNullOrEmpty(taskDto.Title))
            return StatusCode(400, new ErrorView { Message = "Título Obrigatório!" });

        var task = new TaskModel {
            Title = taskDto.Title,
            Description = taskDto.Description,
            IsCompleted = taskDto.IsCompleted,
        };

        _db.Tasks.Add(task);
        _db.SaveChanges();

        return StatusCode(201, task);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] TaskDto taskDto)
    {
        if (string.IsNullOrEmpty(taskDto.Title))
            return StatusCode(400, new ErrorView { Message = "Título Obrigatório!" });

        var taskDb = _db.Tasks.Find(id);
        if (taskDb == null)
            return StatusCode(404, new ErrorView { Message = $"Id ({id}) Não Encontrado!" });
        
        taskDb.Title = taskDto.Title;
        taskDb.Description = taskDto.Description;
        taskDb.IsCompleted = taskDto.IsCompleted;

        _db.Tasks.Update(taskDb);
        _db.SaveChanges();

        return StatusCode(200, taskDb);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var task = _db.Tasks.Find(id);
        if (task == null)
            return StatusCode(404, new ErrorView { Message = $"Id ({id}) Não Encontrado!" });

        _db.Tasks.Remove(task);
        _db.SaveChanges();

        return StatusCode(200, task);
    }
}


