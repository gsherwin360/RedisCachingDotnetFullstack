using GameStore.API.Entities;
using GameStore.API.Models.DTOs;

namespace GameStore.API.Repositories
{
    public interface IGameRepository
    {
        Task<List<GameSummaryDTO>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(int id);
    }
}
