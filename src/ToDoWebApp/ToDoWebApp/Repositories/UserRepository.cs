using Microsoft.EntityFrameworkCore;

using ToDoWebApp.Context;
using ToDoWebApp.Domain;

namespace ToDoWebApp.Repositories;


internal class UserRepository : IUserRepository
{
    private readonly ApplicationToDoDbContext _context;


    public UserRepository(ApplicationToDoDbContext context)
    {
        _context = context;
    }


    public async Task<ICollection<User>> GetItems(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(item => item.ToDoItems)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasItemWithId(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(item => item.Id == id, cancellationToken);
    }

    public async Task Create(User newUser, CancellationToken cancellationToken)
    {
        _context.Users.Add(newUser);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Guid id, User newUser, CancellationToken cancellationToken)
    {
        await _context.Users
            .Where(item => item.Id == id)
            .ExecuteUpdateAsync(props => props
                .SetProperty(item => item.Name, newUser.Name),
                cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        await _context.Users
            .Where(item => item.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}