namespace APITasks.DTO;

public record TaskDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsCompleted { get; set; } = default!;
}