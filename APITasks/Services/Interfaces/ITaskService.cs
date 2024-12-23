using APITasks.Database;
using APITasks.DTO;
using APITasks.Models;
using APITasks.Models.Errors;

namespace APITasks.Services.Interfaces;

public interface ITaskService
{
    List<TaskModel> TaskList(int page);
    TaskModel Include(TaskDto taskDto);
    TaskModel Update(int id, TaskDto taskDto);
    TaskModel SelectById(int id);
    void Delete(int id);
}
