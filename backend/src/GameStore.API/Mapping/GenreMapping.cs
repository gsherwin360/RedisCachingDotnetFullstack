using GameStore.API.Entities;
using GameStore.API.Models.DTOs;

namespace GameStore.API.Mapping
{
    public static class GenreMapping
    {
        public static GenreSummaryDTO ToGenreSummaryDto(this Genre genre)
        {
            return new GenreSummaryDTO(genre.Id, genre.Name);
        }
    }
}
