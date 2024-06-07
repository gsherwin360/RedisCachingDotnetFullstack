using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Models
{
    public record class UpdateGameModel(
        [Required][StringLength(50)]string Name,
        int GenreId,
        decimal Price,
        DateOnly ReleaseDate);
}
