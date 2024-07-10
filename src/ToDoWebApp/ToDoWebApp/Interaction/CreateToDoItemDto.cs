using System.ComponentModel.DataAnnotations;

namespace ToDoWebApp.Interaction;


internal class CreateToDoItemDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; }


    [Required]
    public int PriorityLevel { get; set; }


    [Required]
    public Guid UserId { get; set; }
}