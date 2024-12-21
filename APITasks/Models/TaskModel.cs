using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APITasks.Models;

[Table("Tasks")]
public class TaskModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = default!;

    [Column(TypeName = "text")]
    public string Description { get; set; } = default!;
    public bool IsCompleted { get; set; } = default!;
}