using System.ComponentModel.DataAnnotations;

namespace ToDoWebApp.Interaction;


internal class CreateUserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}