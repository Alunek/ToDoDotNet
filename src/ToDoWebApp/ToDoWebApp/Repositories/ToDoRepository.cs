using Microsoft.EntityFrameworkCore;

using ToDoWebApp.Context;
using ToDoWebApp.Domain;
using ToDoWebApp.Repositories.Filters;

namespace ToDoWebApp.Repositories;


internal class ToDoRepository : IToDoRepository
{
    private readonly ApplicationToDoDbContext _context;


    public ToDoRepository(ApplicationToDoDbContext context)
    {
        _context = context;
    }


    public async Task<ICollection<ToDoItem>> GetItemsByFilters(
        IEnumerable<IParameterFilter<ToDoItem>> filters, CancellationToken cancellationToken)
    {
        return await _context.ToDoItems
            .AsNoTracking()
            .ApplyFilters(filters)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasItemWithId(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ToDoItems
            .AsNoTracking()
            .AnyAsync(item => item.Id == id, cancellationToken);
    }

    public async Task Create(ToDoItem newToDoItem, CancellationToken cancellationToken)
    {
        _context.ToDoItems.Add(newToDoItem);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Guid id, ToDoItem newToDoItem, CancellationToken cancellationToken)
    {
        await _context.ToDoItems
            .Where(item => item.Id == id)
            .ExecuteUpdateAsync(props => props
                .SetProperty(item => item.Title, newToDoItem.Title)
                .SetProperty(item => item.Description, newToDoItem.Description)
                .SetProperty(item => item.IsCompleted, newToDoItem.IsCompleted)
                .SetProperty(item => item.DueDate, newToDoItem.DueDate)
                .SetProperty(item => item.PriorityLevel, newToDoItem.PriorityLevel)
                .SetProperty(item => item.UserId, newToDoItem.UserId),
                cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        await _context.ToDoItems
            .Where(item => item.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}