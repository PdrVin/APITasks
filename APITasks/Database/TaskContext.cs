namespace APITasks.Database;

public class TaskContext : DbContext
{
    #nullable disable
    public TaskContext(DbContextOption<TaskContext> options) : base(options);
    public DbSet<Task> Tasks { get; set; } 
}