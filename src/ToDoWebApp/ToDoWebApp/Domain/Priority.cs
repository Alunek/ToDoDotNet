using System.ComponentModel.DataAnnotations;

namespace ToDoWebApp.Domain;


internal class Priority
{
    [Key]
    public int Level { get; set; }
    public IEnumerable<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
}