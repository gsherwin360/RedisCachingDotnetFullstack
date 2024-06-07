using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Models
{
    public record class CreateGameModel(
        [Required][StringLength(50)]string Name,
        int GenreId,
        decimal Price,
        DateOnly ReleaseDate);
}