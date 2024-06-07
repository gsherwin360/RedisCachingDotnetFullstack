using GameStore.API.Mapping;
using GameStore.API.Models;
using GameStore.API.Models.DTOs;
using GameStore.API.Repositories;

namespace GameStore.API.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameByIdEndpointName = "GetGameById";

        public static RouteGroupBuilder MapGamesEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("games").WithParameterValidation();

            // GET /games
            group.MapGet("/", async (IGameRepository gameRepository) =>
            {
                var games = await gameRepository.GetAllAsync();
                return games.Select(game => game.ToGameSummaryDto()).ToList();
            })
            .Produces<List<GameSummaryDTO>>(StatusCodes.Status200OK)
            .Produces<GameDetailsDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // GET /games/1
            group.MapGet("/{id}", async (int id, IGameRepository gameRepository) =>
            {
                var game = await gameRepository.GetByIdAsync(id);
                return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
            })
            .WithName(GetGameByIdEndpointName)
            .Produces<GameDetailsDTO>(StatusCodes.Status201Created);

            // POST /games
            group.MapPost("/", async (CreateGameModel newGame, IGameRepository gameRepository) =>
            {
                var game = GameMapping.ToEntity(newGame);
                await gameRepository.AddAsync(game);

                return Results.CreatedAtRoute(GetGameByIdEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
            });

            // PUT /games
            group.MapPut("/{id}", async (int id, UpdateGameModel updatedGame, IGameRepository gameRepository) =>
            {
                var existingGame = await gameRepository.GetByIdAsync(id);

                if (existingGame is null) return Results.NotFound();

                updatedGame.UpdateEntity(existingGame);
                await gameRepository.UpdateAsync(existingGame);

                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /games/{id}
            group.MapDelete("/{id}", async (int id, IGameRepository gameRepository) =>
            {
                await gameRepository.DeleteAsync(id);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent);

            return group;
        }
    }
}