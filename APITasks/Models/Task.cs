using System.ComponentModel.DataAnnotations.Schema;

namespace APITasks.Models;

[Table]
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsCompleted { get; set; } = default!;
}