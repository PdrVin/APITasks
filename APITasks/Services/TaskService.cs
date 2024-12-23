using APITasks.Database;
using APITasks.DTO;
using APITasks.Models;
using APITasks.Models.Errors;
using APITasks.Services.Interfaces;

namespace APITasks.Services;

public class TaskService : ITaskService
{
    public TaskService(TaskContext db)
    {
        _db = db;
    }

    private TaskContext _db;

    public List<TaskModel> TaskList(int page = 1)
    {
        page = (page < 1) ? 1 : page;
        int limit = 10;
        int offset = (page - 1) * limit;
        return _db.Tasks.Skip(offset).Take(limit).ToList();
    }

    public TaskModel Include(TaskDto taskDto)
    {
        if (string.IsNullOrEmpty(taskDto.Title))
            throw new TaskError("Título Obrigatório!");

        var task = new TaskModel
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            IsCompleted = taskDto.IsCompleted,
        };

        _db.Tasks.Add(task);
        _db.SaveChanges();
        return task;
    }

    public TaskModel Update(int id, TaskDto taskDto)
    {
        if (string.IsNullOrEmpty(taskDto.Title))
            throw new TaskError("Título Obrigatório!");

        var taskDb = _db.Tasks.Find(id);
        if (taskDb == null)
            throw new TaskError("Tarefa Não Encontrada!");

        taskDb.Title = taskDto.Title;
        taskDb.Description = taskDto.Description;
        taskDb.IsCompleted = taskDto.IsCompleted;

        _db.Tasks.Update(taskDb);
        _db.SaveChanges();
        return taskDb;
    }

    public TaskModel SelectById(int id)
    {
        var taskDb = _db.Tasks.Find(id);
        if (taskDb == null)
            throw new TaskError("Tarefa Não Encontrada!");

        return taskDb;
    }

    public void Delete(int id)
    {
        var taskDb = _db.Tasks.Find(id);
        if (taskDb == null)
            throw new TaskError("Tarefa Não Encontrada!");

        _db.Tasks.Remove(taskDb);
        _db.SaveChanges();
    }
}
