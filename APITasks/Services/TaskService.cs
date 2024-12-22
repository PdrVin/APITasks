using APITasks.Database;
using APITasks.Models;
using APITasks.Models.Errors;

namespace APITasks.Services;

public class TaskService
{
    public TaskService(TaskContext db)
    {
        _db = db;
    }

    private TaskContext _db;

    public List<TaskModel> TaskList()
    {
        return _db.Tasks.ToList();
    }

    public TaskModel Include(TaskModel task)
    {
        if (string.IsNullOrEmpty(task.Title))
            throw new TaskError("Título Obrigatório!");

        _db.Tasks.Add(task);
        _db.SaveChanges();
        return task;
    }

    public TaskModel Update(int id, TaskModel task)
    {
        if (string.IsNullOrEmpty(task.Title))
            throw new TaskError("Título Obrigatório!");

        var taskDb = _db.Tasks.Find(id);
        if (taskDb == null)
            throw new TaskError("Tarefa Não Encontrada!");

        taskDb.Title = task.Title;
        taskDb.Description = task.Description;
        taskDb.IsCompleted = task.IsCompleted;

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
