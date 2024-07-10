using Microsoft.EntityFrameworkCore;

using ToDoWebApp.Context;
using ToDoWebApp.Domain;

namespace ToDoWebApp.Repositories;


internal class PriorityRepository : IPriorityRepository
{
    private readonly ApplicationToDoDbContext _context;


    public PriorityRepository(ApplicationToDoDbContext context)
    {
        _context = context;
    }


    public async Task<bool> HasItemByLevel(int level, CancellationToken cancellationToken)
    {
        return await _context.Priorities
            .AsNoTracking()
            .AnyAsync(item => item.Level == level, cancellationToken);
    }
}