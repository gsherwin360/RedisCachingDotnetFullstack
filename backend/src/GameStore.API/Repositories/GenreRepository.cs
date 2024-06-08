using GameStore.API.Caching;
using GameStore.API.Data;
using GameStore.API.Mapping;
using GameStore.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace GameStore.API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GameStoreDbContext _dbContext;
		private readonly ICacheService _cacheService;

		public GenreRepository(GameStoreDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
		}

		public async Task<List<GenreSummaryDTO>> GetAllAsync()
		{
			return await _cacheService.Get(
				this.GetCacheKeyForAllGenre(),
				async () =>
				{
                    var genres = await _dbContext.Genres.AsNoTracking().ToListAsync();
                    return genres.Select(genre => genre.ToGenreSummaryDto()).ToList();
				},
				new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.MaxValue });
		}

        private string GetCacheKeyForAllGenre() => "genre_summary";
    }
}
