namespace ToDoWebApp.Interaction;


internal class GetUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<GetToDoItemDto> ToDoItems { get; set; } = Array.Empty<GetToDoItemDto>();
}