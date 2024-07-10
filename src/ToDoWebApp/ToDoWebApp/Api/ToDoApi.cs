using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using ToDoWebApp.Domain;
using ToDoWebApp.Interaction;
using ToDoWebApp.Repositories;
using ToDoWebApp.Repositories.Filters;

namespace ToDoWebApp.Api;


internal static class ToDoApi
{
    public static void MapToDoRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/todo")
            .WithTags("ToDoItems");

        group.MapGet("/", async Task<Results<Ok<IEnumerable<GetToDoItemDto>>, NotFound>> (
            [FromQuery] bool? isCompleted,
            [FromQuery] int[]? priorities,
            [FromServices] ILogger<Program> logger,
            [FromServices] IToDoRepository toDoRepository,
            CancellationToken cancellationToken) =>
        {
            var filters = new IParameterFilter<ToDoItem>[] {
                ToDoItemIsCompletedFilter.Create(isCompleted),
                ToDoItemPriorityFilter.Create(priorities)
            };

            var toDoItems = await toDoRepository.GetItemsByFilters(filters, cancellationToken);

            logger.LogTrace("Получен список задач. count: {}", toDoItems.Count);

            var dtos = toDoItems
                .Select(item => item.ToDto());

            return TypedResults.Ok(dtos);
        })
        .WithName("GetToDoItems")
        .WithOpenApi();


        group.MapPost("/", async Task<Results<Ok, BadRequest<string>>> (
            [FromBody] CreateToDoItemDto toDoItemDto,
            [FromServices] ILogger<Program> logger,
            [FromServices] IUserRepository userRepository,
            [FromServices] IPriorityRepository priorityRepository,
            [FromServices] IToDoRepository toDoRepository,
            CancellationToken cancellationToken) =>
        {
            if (!await userRepository.HasItemWithId(toDoItemDto.UserId, cancellationToken))
            {
                logger.LogError("Не найден пользователь. id: {}", toDoItemDto.UserId);

                return TypedResults.BadRequest($"Не найден пользователь. id: {toDoItemDto.UserId}");
            }

            if (!await priorityRepository.HasItemByLevel(toDoItemDto.PriorityLevel, cancellationToken))
            {
                logger.LogError("Уровня приоритета не существует. level: {}", toDoItemDto.PriorityLevel);

                return TypedResults.BadRequest($"Уровня приоритета не существует. level: {toDoItemDto.PriorityLevel}");
            }

            var newToDoItem = ToDoItem.CreateFromDto(toDoItemDto, toDoItemDto.PriorityLevel, toDoItemDto.UserId);

            await toDoRepository.Create(newToDoItem, cancellationToken);

            logger.LogDebug("Создана задача. toDoItemDto: {}", toDoItemDto);

            return TypedResults.Ok();
        })
        .WithName("CreateToDoItem")
        .WithOpenApi();


        group.MapPut("/{id:guid}", async Task<Results<Ok, BadRequest<string>>> (
            [FromRoute] Guid id,
            [FromBody] UpdateToDoItemDto toDoItemDto,
            [FromServices] ILogger<Program> logger,
            [FromServices] IUserRepository userRepository,
            [FromServices] IPriorityRepository priorityRepository,
            [FromServices] IToDoRepository toDoRepository,
            CancellationToken cancellationToken) =>
        {
            if (!await userRepository.HasItemWithId(toDoItemDto.UserId, cancellationToken))
            {
                logger.LogError("Не найден пользователь. id: {}", toDoItemDto.UserId);

                return TypedResults.BadRequest($"Не найден пользователь. id: {toDoItemDto.UserId}");
            }

            if (!await priorityRepository.HasItemByLevel(toDoItemDto.PriorityLevel, cancellationToken))
            {
                logger.LogError("Уровня приоритета не существует. level: {}", toDoItemDto.PriorityLevel);

                return TypedResults.BadRequest($"Уровня приоритета не существует. level: {toDoItemDto.PriorityLevel}");
            }

            var newToDoItemData = ToDoItem.CreateFromDto(toDoItemDto, toDoItemDto.PriorityLevel, toDoItemDto.UserId);

            await toDoRepository.Update(id, newToDoItemData, cancellationToken);

            logger.LogDebug("Обновлена информация о задаче. id: {} toDoItemDto: {}", id, toDoItemDto);

            return TypedResults.Ok();
        })
        .WithName("UpdateToDoItem")
        .WithOpenApi();


        group.MapDelete("/{id:guid}", async Task<Results<NoContent, NotFound>> (
            [FromRoute] Guid id,
            [FromServices] ILogger<Program> logger,
            [FromServices] IToDoRepository toDoRepository,
            CancellationToken cancellationToken) =>
        {
            if (!await toDoRepository.HasItemWithId(id, cancellationToken))
            {
                logger.LogWarning("Не найдена задача для удаления. id: {}", id);

                return TypedResults.NotFound();
            }

            await toDoRepository.Delete(id, cancellationToken);

            logger.LogDebug("Удалена задача. id: {}", id);

            return TypedResults.NoContent();
        })
        .WithName("DeleteToDoItem")
        .WithOpenApi();
    }
}