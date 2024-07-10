using System.ComponentModel.DataAnnotations;


namespace ToDoWebApp.Interaction;


internal class GetToDoItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public int PriorityLevel { get; set; }
    public Guid UserId { get; set; }
}