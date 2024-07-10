using ToDoWebApp.Domain;

namespace ToDoWebApp.Repositories.Filters;


internal class ToDoItemIsCompletedFilter : IParameterFilter<ToDoItem>
{
    private readonly bool? _isCompleted;


    public static ToDoItemIsCompletedFilter Default { get; } = new(null);


    private ToDoItemIsCompletedFilter(bool? isCompleted)
    {
        _isCompleted = isCompleted;
    }


    public static ToDoItemIsCompletedFilter Create(bool? isCompleted)
    {
        return isCompleted is null ? Default : new(isCompleted);
    }


    public IQueryable<ToDoItem> Apply(IQueryable<ToDoItem> query)
    {
        return _isCompleted is null
            ? query
            : query.Where(x => x.IsCompleted == _isCompleted);
    }
}