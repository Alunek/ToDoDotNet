using ToDoWebApp.Domain;
using ToDoWebApp.Repositories.Filters;

namespace ToDoWebApp.Repositories;


internal interface IToDoRepository
{
    Task<ICollection<ToDoItem>> GetItemsByFilters(
        IEnumerable<IParameterFilter<ToDoItem>> filters, CancellationToken cancellationToken);
    Task<bool> HasItemWithId(Guid id, CancellationToken cancellationToken);
    Task Create(ToDoItem newToDoItem, CancellationToken cancellationToken);
    Task Update(Guid id, ToDoItem newToDoItem, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}