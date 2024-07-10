using ToDoWebApp.Domain;

namespace ToDoWebApp.Repositories;


internal interface IUserRepository
{
    Task<ICollection<User>> GetItems(CancellationToken cancellationToken);
    Task<bool> HasItemWithId(Guid id, CancellationToken cancellationToken);
    Task Create(User newUser, CancellationToken cancellationToken);
    Task Update(Guid id, User newUser, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}