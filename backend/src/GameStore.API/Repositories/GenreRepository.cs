using GameStore.API.Data;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GameStoreDbContext _dbContext;

        public GenreRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Genre>> GetAllAsync() 
            => await _dbContext.Genres.AsNoTracking().ToListAsync();
    }
}
