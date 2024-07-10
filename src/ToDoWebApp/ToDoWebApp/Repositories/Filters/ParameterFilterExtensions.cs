namespace ToDoWebApp.Repositories.Filters;


internal static class ParameterFilterExtensions
{
    public static IQueryable<TEntity> ApplyFilters<TEntity>(
        this IQueryable<TEntity> source, IEnumerable<IParameterFilter<TEntity>> filters)
    {
        var newSource = source;

        foreach (var filter in filters)
        {
            newSource = filter.Apply(newSource);
        }

        return newSource;
    }
}