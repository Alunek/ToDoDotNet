using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using ToDoWebApp.Interaction;

namespace ToDoWebApp.Domain;


internal class ToDoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public int PriorityLevel { get; set; }
    [ForeignKey(nameof(PriorityLevel))]
    public Priority Priority { get; set; } = null!;
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;


    public GetToDoItemDto ToDto()
    {
        return new GetToDoItemDto
        {
            Id = Id,
            Title = Title,
            Description = Description,
            IsCompleted = IsCompleted,
            DueDate = DueDate,
            PriorityLevel = PriorityLevel,
            UserId = UserId,
        };
    }

    public static ToDoItem CreateFromDto(CreateToDoItemDto dto, int priorityLevel, Guid userId)
    {
        return new ToDoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false,
            DueDate = dto.DueDate,
            PriorityLevel = priorityLevel,
            UserId = userId
        };
    }

    public static ToDoItem CreateFromDto(UpdateToDoItemDto dto, int priorityLevel, Guid userId)
    {
        return new ToDoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            DueDate = dto.DueDate,
            PriorityLevel = priorityLevel,
            UserId = userId
        };
    }
}