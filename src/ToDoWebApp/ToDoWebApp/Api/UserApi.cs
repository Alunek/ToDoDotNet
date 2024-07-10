using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using ToDoWebApp.Domain;
using ToDoWebApp.Interaction;
using ToDoWebApp.Repositories;

namespace ToDoWebApp.Api;


internal static class UserApi
{
    public static void MapUserRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/users")
            .WithTags("Users");

        group.MapGet("/", async Task<Results<Ok<IEnumerable<GetUserDto>>, NotFound>> (
            [FromServices] ILogger<Program> logger,
            [FromServices] IUserRepository userRepository,
            CancellationToken cancellationToken) =>
        {
            var userItems = await userRepository.GetItems(cancellationToken);

            logger.LogTrace("Получен список пользователей. count: {}", userItems.Count);

            var dtos = userItems
                .Select(item => item.ToDto());

            return TypedResults.Ok(dtos);
        })
        .WithName("GetUsers")
        .WithOpenApi();


        group.MapPost("/", async Task<Results<Ok, BadRequest>> (
            [FromBody] CreateUserDto userDto,
            [FromServices] ILogger<Program> logger,
            [FromServices] IUserRepository userRepository,
            CancellationToken cancellationToken) =>
        {
            var newUser = User.CreateFromDto(userDto);

            await userRepository.Create(newUser, cancellationToken);

            logger.LogDebug("Создан пользователь. userDto: {}", userDto);

            return TypedResults.Ok();
        })
        .WithName("CreateUser")
        .WithOpenApi();


        group.MapPut("/{id:guid}", async Task<Results<Ok, BadRequest<string>>> (
            [FromRoute] Guid id,
            [FromBody] UpdateUserDto userDto,
            [FromServices] ILogger<Program> logger,
            [FromServices] IUserRepository userRepository,
            CancellationToken cancellationToken) =>
        {
            if (!await userRepository.HasItemWithId(id, cancellationToken))
            {
                logger.LogError("Не найден пользователь. id: {}", id);

                return TypedResults.BadRequest($"Не найден пользователь. id: {id}");
            }

            var newUserData = User.CreateFromDto(userDto);

            await userRepository.Update(id, newUserData, cancellationToken);

            logger.LogDebug("Обновлена информация о пользователе. id: {} userDto: {}", id, userDto);

            return TypedResults.Ok();
        })
        .WithName("UpdateUser")
        .WithOpenApi();


        group.MapDelete("/{id:guid}", async Task<Results<NoContent, NotFound>> (
            [FromRoute] Guid id,
            [FromServices] ILogger<Program> logger,
            [FromServices] IUserRepository userRepository,
            CancellationToken cancellationToken) =>
        {
            if (!await userRepository.HasItemWithId(id, cancellationToken))
            {
                logger.LogWarning("Не найден пользователь для удаления. id: {}", id);

                return TypedResults.NotFound();
            }

            await userRepository.Delete(id, cancellationToken);

            logger.LogDebug("Удален пользователь. id: {}", id);

            return TypedResults.NoContent();
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}