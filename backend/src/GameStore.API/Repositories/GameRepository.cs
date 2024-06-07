using GameStore.API.Data;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameStoreDbContext _dbContext;

        public GameRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Game>> GetAllAsync() 
            => await _dbContext.Games.Include(game => game.Genre).AsNoTracking().ToListAsync(); 

        public async Task<Game?> GetByIdAsync(int id)
            => await _dbContext.Games.AsNoTracking().FirstOrDefaultAsync(game => game.Id == id);

        public async Task AddAsync(Game game)
        {
            await _dbContext.Games.AddAsync(game);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            _dbContext.Games.Update(game);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var game = await _dbContext.Games.FindAsync(id);
            if (game is not null)
            {
                _dbContext.Games.Remove(game);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
