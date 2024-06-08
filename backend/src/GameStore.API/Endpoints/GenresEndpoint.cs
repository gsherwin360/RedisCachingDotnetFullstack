using GameStore.API.Models.DTOs;
using GameStore.API.Repositories;

namespace GameStore.API.Endpoints
{
    public static class GenresEndpoint
    {
        public static RouteGroupBuilder MapGenresEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/genres");

            // GET /genres
            group.MapGet("/", async (IGenreRepository genreRepository) =>
            {
                return await genreRepository.GetAllAsync();
            })
            .Produces<List<GenreSummaryDTO>>(StatusCodes.Status200OK);

            return group;
        }
	}
}