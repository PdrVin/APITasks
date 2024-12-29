using Microsoft.AspNetCore.Mvc;
using Moq;
using APITasks.Controllers;
using APITasks.DTO;
using APITasks.Interfaces;
using APITasks.Models;
using APITasks.Models.Errors;


namespace APITasks.Test;

public class TestController
{
    [Fact(DisplayName = "Index - Ok")]
    public async Task IndexReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.GetAllTasksAsync(It.IsAny<int>()))
                .ReturnsAsync(GetSampleTasks());
        var controller = new TasksController(mockService.Object);

        // Act
        var result = await controller.Index(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var tasks = Assert.IsType<List<TaskModel>>(okResult.Value);
        Assert.Equal(2, tasks.Count);
    }

    [Fact(DisplayName = "Select - Ok")]
    public async Task SelectReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.GetTaskByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetSampleTask());
        var controller = new TasksController(mockService.Object);

        // Act
        var result = await controller.Select(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var task = Assert.IsType<TaskModel>(okResult.Value);
    }

    [Fact(DisplayName = "Select - NotFound")]
    public async Task SelectReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.GetTaskByIdAsync(It.IsAny<int>()))
                .Throws(new TaskError("Tarefa Não Encontrada!"));
        var controller = new TasksController(mockService.Object);

        // Act
        var result = await controller.Select(99);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact(DisplayName = "Create - Created")]
    public async Task CreateReturnsCreatedAtAction()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.AddTaskAsync(It.IsAny<TaskDto>()))
                .ReturnsAsync(GetSampleTask());
        var controller = new TasksController(mockService.Object);
        var newTaskDto = new TaskDto { Title = "New Task", Description = "Description", IsCompleted = false };

        // Act
        var result = await controller.Create(newTaskDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var task = Assert.IsType<TaskModel>(createdAtActionResult.Value);
    }

    [Fact(DisplayName = "Create - BadRequest")]
    public async Task CreateReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.AddTaskAsync(It.IsAny<TaskDto>()))
                .Throws(new TaskError("Título Obrigatório!"));
        var controller = new TasksController(mockService.Object);
        var newTaskDto = new TaskDto { Title = "", Description = "Description", IsCompleted = false };

        // Act
        var result = await controller.Create(newTaskDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact(DisplayName = "Update - Ok")]
    public async Task UpdateReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.UpdateTaskAsync(It.IsAny<int>(), It.IsAny<TaskDto>()))
                .ReturnsAsync(GetSampleTask());
        var controller = new TasksController(mockService.Object);
        var updateTaskDto = new TaskDto { Title = "Updated Task", Description = "Updated Description", IsCompleted = true };

        // Act
        var result = await controller.Update(1, updateTaskDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var task = Assert.IsType<TaskModel>(okResult.Value);
    }

    [Fact(DisplayName = "Update - BadRequest")]
    public async Task Update_ReturnsBadRequest_ForInvalidTask()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.UpdateTaskAsync(It.IsAny<int>(), It.IsAny<TaskDto>()))
                .Throws(new TaskError("Título Obrigatório!"));
        var controller = new TasksController(mockService.Object);
        var updateTaskDto = new TaskDto { Title = "", Description = "Updated Description", IsCompleted = true };

        // Act
        var result = await controller.Update(1, updateTaskDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact(DisplayName = "Delete - NoContent")]
    public async Task Delete_ReturnsNoContent_ForValidId()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.DeleteTaskAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);
        var controller = new TasksController(mockService.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact(DisplayName = "Delete - BadRequest")]
    public async Task Delete_ReturnsBadRequest_ForInvalidId()
    {
        // Arrange
        var mockService = new Mock<ITaskService>();
        mockService.Setup(service => service.DeleteTaskAsync(It.IsAny<int>()))
                .Throws(new TaskError("Tarefa Não Encontrada!"));
        var controller = new TasksController(mockService.Object);

        // Act
        var result = await controller.Delete(99);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
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
}