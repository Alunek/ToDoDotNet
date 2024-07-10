using ToDoWebApp.Domain;

namespace ToDoWebApp.Repositories.Filters;


internal class ToDoItemPriorityFilter : IParameterFilter<ToDoItem>
{
    private readonly int[]? _priorities;


    public static ToDoItemPriorityFilter Default { get; } = new(null);


    private ToDoItemPriorityFilter(int[]? priorities)
    {
        _priorities = priorities;
    }


    public static ToDoItemPriorityFilter Create(int[]? priorities)
    {
        return priorities is null ? Default : new(priorities);
    }


    public IQueryable<ToDoItem> Apply(IQueryable<ToDoItem> query)
    {
        return _priorities is null || _priorities.Length == 0
            ? query
            : query.Where(x => _priorities.Contains(x.PriorityLevel));
    }
}