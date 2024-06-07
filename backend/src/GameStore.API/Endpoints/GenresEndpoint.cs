using GameStore.API.Data;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints
{
    public static class GenresEndpoint
    {
        public static RouteGroupBuilder MapGenresEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/genres");

            // GET /genres
            group.Map("/", async (GameStoreDbContext dbContext) =>
            {
                return await dbContext.Genres
                    .Select(genre => genre.ToGenreSummaryDto())
                    .AsNoTracking()
                    .ToListAsync();
            });

            return group;
        } 
    }
}
