using ToDoWebApp.Interaction;

namespace ToDoWebApp.Domain;


internal class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();


    public GetUserDto ToDto()
    {
        return new GetUserDto
        {
            Id = Id,
            Name = Name,
            ToDoItems = ToDoItems
                .Select(item => item.ToDto())
                .ToList()
        };
    }

    public static User CreateFromDto(CreateUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            ToDoItems = Array.Empty<ToDoItem>()
        };
    }

    public static User CreateFromDto(UpdateUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            ToDoItems = Array.Empty<ToDoItem>()
        };
    }
}