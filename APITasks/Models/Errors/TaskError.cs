namespace APITasks.Models.Errors;

public class TaskError(string message) : Exception(message) { }