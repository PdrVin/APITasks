using APITasks.DTO;
using APITasks.Models;

namespace APITasks.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetAllTasksAsync(int page = 1);
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<TaskModel> AddTaskAsync(TaskDto taskDto);
    Task<TaskModel> UpdateTaskAsync(int id, TaskDto taskDto);
    Task DeleteTaskAsync(int id);
}
