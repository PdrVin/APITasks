using Microsoft.EntityFrameworkCore;
using APITasks.Models;

namespace APITasks.Database;

public class TaskContext : DbContext
{
    #nullable disable
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) {}
    public DbSet<TaskModel> Tasks { get; set; } 
}