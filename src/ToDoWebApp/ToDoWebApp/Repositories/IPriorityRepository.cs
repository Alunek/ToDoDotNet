using ToDoWebApp.Domain;

namespace ToDoWebApp.Repositories;


internal interface IPriorityRepository
{
    Task<bool> HasItemByLevel(int level, CancellationToken cancellationToken);
}