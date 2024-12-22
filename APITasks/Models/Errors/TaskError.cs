namespace APITasks.Models.Errors;

public class TaskError : Exception
{
    public TaskError(string message) : base(message){}
}