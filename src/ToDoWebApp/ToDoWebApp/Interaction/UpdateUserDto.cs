using System.ComponentModel.DataAnnotations;

namespace ToDoWebApp.Interaction;


internal class UpdateUserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}