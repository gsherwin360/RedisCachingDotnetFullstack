using GameStore.API.Entities;
using GameStore.API.Models;
using GameStore.API.Models.DTOs;

namespace GameStore.API.Mapping
{
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameModel gameModel)
        {
            return Game.Create(
                gameModel.Name,
                gameModel.GenreId,
                gameModel.Price,
                gameModel.ReleaseDate);
        }

        public static void UpdateEntity(this UpdateGameModel gameModel, Game existingGame)
        {
            existingGame.Update(
                gameModel.Name,
                gameModel.GenreId,
                gameModel.Price,
                gameModel.ReleaseDate);
        }

        public static GameSummaryDTO ToGameSummaryDto(this Game game)
        {
            return new GameSummaryDTO(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate);
        }

        public static GameDetailsDTO ToGameDetailsDto(this Game game)
        {
            return new GameDetailsDTO(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate);
        }
    }
}
