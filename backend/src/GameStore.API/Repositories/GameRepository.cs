using GameStore.API.Caching;
using GameStore.API.Data;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using GameStore.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace GameStore.API.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameStoreDbContext _dbContext;
		private readonly ICacheService _cacheService;

		public GameRepository(GameStoreDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
		}

		public async Task<List<GameSummaryDTO>> GetAllAsync()
		{
			return await _cacheService.Get(
				this.GetCacheKeyForAllGames(),
				async () =>
				{
					var games = await _dbContext.Games.Include(game => game.Genre).AsNoTracking().ToListAsync();
					return games.Select(game => game.ToGameSummaryDto()).ToList();
				},
				new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.MaxValue });
		}

		public async Task<Game?> GetByIdAsync(int id)
		{
			return await _dbContext.Games.AsNoTracking().FirstOrDefaultAsync(game => game.Id == id);
		}

        public async Task AddAsync(Game game)
        {
            await _dbContext.Games.AddAsync(game);
            await _dbContext.SaveChangesAsync();
			await InvalidateCacheAsync();
		}

		public async Task UpdateAsync(Game game)
        {
            _dbContext.Games.Update(game);
            await _dbContext.SaveChangesAsync();
			await InvalidateCacheAsync();
		}

		public async Task DeleteAsync(int id)
        {
            var game = await _dbContext.Games.FindAsync(id);
            if (game is not null)
            {
                _dbContext.Games.Remove(game);
                await _dbContext.SaveChangesAsync();
				await InvalidateCacheAsync();
			}
        }

		private async Task InvalidateCacheAsync()
			=> await _cacheService.Remove(GetCacheKeyForAllGames());

		private string GetCacheKeyForAllGames() => "game_summary";
	}
}