namespace ToDoWebApp.Api;


internal static class RootApiRoute
{
    public static void MapApiRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/api");

        group.MapToDoRoutes();
        group.MapUserRoutes();
    }
}