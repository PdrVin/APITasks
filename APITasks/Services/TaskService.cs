using APITasks.DTO;
using APITasks.Models;
using APITasks.Interfaces;

namespace APITasks.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(int page = 1)
    {
        return await _repository.GetAllAsync(page);
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TaskModel> AddTaskAsync(TaskDto taskDto)
    {
        var taskModel = new TaskModel
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            IsCompleted = taskDto.IsCompleted
        };

        return await _repository.AddAsync(taskModel);
    }

    public async Task<TaskModel> UpdateTaskAsync(int id, TaskDto taskDto)
    {
        var task = new TaskModel
        {
            Id = id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            IsCompleted = taskDto.IsCompleted,
        };

        await _repository.UpdateAsync(id, taskDto);
        return task;
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
