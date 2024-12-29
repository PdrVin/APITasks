using APITasks.Database;
using APITasks.Interfaces;
using APITasks.Models;
using APITasks.Repository.Base;

namespace APITasks.Repository;

public class TaskRepository : Repository<TaskModel>, ITaskRepository
{
    public TaskRepository(TaskContext context) : base(context) { }
}