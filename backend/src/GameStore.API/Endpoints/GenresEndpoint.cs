using GameStore.API.Data;
using GameStore.API.Mapping;
using GameStore.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints
{
    public static class GenresEndpoint
    {
        public static RouteGroupBuilder MapGenresEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/genres");

            // GET /genres
            group.MapGet("/", async (GameStoreDbContext dbContext) =>
            {
                return await dbContext.Genres
                    .Select(genre => genre.ToGenreSummaryDto())
                    .AsNoTracking()
                    .ToListAsync();
            })
			.Produces<List<GenreSummaryDTO>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);

            return group;
        } 
    }
}
