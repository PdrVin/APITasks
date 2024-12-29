using Moq;
using APITasks.DTO;
using APITasks.Models;
using APITasks.Models.Errors;
using APITasks.Interfaces.Base;

namespace APITasks.Test;

public class TestRepository
{
    [Fact(DisplayName = "GetAllAsync - Ok")]
    public async Task GetAllAsyncReturnsPaginatedList()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();
        mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(GetSampleTasks());
        var repository = mockRepo.Object;
        
        // Act
        var tasks = await repository.GetAllAsync(1);
        
        // Assert
        Assert.Equal(2, tasks.Count());
    }

    [Fact(DisplayName = "GetByIdAsync - Ok")]
    public async Task GetByIdAsync_ReturnsEntity_ForValidId()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetSampleTask());

        var repository = mockRepo.Object;

        // Act
        var task = await repository.GetByIdAsync(1);

        // Assert
        Assert.Equal(GetSampleTask().Title, task.Title);
    }

    [Fact(DisplayName = "GetByIdAsync - NotFound")]
    public async Task GetByIdAsync_ThrowsTaskError_ForInvalidId()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new TaskError("Tarefa Não Encontrada!"));

        var repository = mockRepo.Object;

        // Act & Assert
        await Assert.ThrowsAsync<TaskError>(() => repository.GetByIdAsync(99));
    }

    [Fact(DisplayName = "AddAsync - Created")]
    public async Task AddAsync_AddsEntity()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();
        var newTask = GetSampleTask();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<TaskModel>()))
                .ReturnsAsync(newTask);

        var repository = mockRepo.Object;

        // Act
        var task = await repository.AddAsync(newTask);

        // Assert
        Assert.Equal("Sample Task", task.Title);
    }


    [Fact(DisplayName = "UpdateAsync - Ok")]
    public async Task UpdateAsync_UpdatesEntity()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();
        var existingTask = GetSampleTask();
        var updatedTaskDto = GetSampleDto();

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingTask);

        var repository = mockRepo.Object;

        // Act
        await repository.UpdateAsync(existingTask.Id, updatedTaskDto);

        // Assert
        mockRepo.Verify(repo => repo.UpdateAsync(existingTask.Id, updatedTaskDto), Times.Once);
        Assert.Equal("Title Test", updatedTaskDto.Title);
        Assert.Equal("Description Test", updatedTaskDto.Description);
        Assert.True(updatedTaskDto.IsCompleted);
    }

    [Fact(DisplayName = "DeleteAsync - Ok")]
    public async Task DeleteAsync_RemovesEntity_ForValidId()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();
        var existingTask = GetSampleTask();
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingTask);
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

        var repository = mockRepo.Object;

        // Act
        await repository.DeleteAsync(existingTask.Id);

        // Assert
        mockRepo.Verify(repo => repo.DeleteAsync(existingTask.Id), Times.Once);
    }

    [Fact(DisplayName = "DeleteAsync - NotFound")]
    public async Task DeleteAsync_ThrowsTaskError_ForInvalidId()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<TaskModel>>();
        
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((TaskModel?)null);
        mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    throw new TaskError("Tarefa Não Encontrada!");
                });

        var repository = mockRepo.Object;

        // Act & Assert
        await Assert.ThrowsAsync<TaskError>(() => repository.DeleteAsync(99));
    }


    private static List<TaskModel> GetSampleTasks()
    {
        return
        [
            new TaskModel { Id = 1, Title = "Task 1", Description = "Description 1", IsCompleted = false },
            new TaskModel { Id = 2, Title = "Task 2", Description = "Description 2", IsCompleted = true }
        ];
    }

    private static TaskModel GetSampleTask()
    {
        return new TaskModel { Id = 1, Title = "Sample Task", Description = "Sample Description", IsCompleted = false };
    }

    private static TaskDto GetSampleDto()
    {
        return new TaskDto { Title = "Title Test", Description = "Description Test", IsCompleted = true };
    }
}
