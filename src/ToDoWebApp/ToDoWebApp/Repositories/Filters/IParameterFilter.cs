namespace ToDoWebApp.Repositories.Filters;


internal interface IParameterFilter<TModel>
{
    IQueryable<TModel> Apply(IQueryable<TModel> query);
}