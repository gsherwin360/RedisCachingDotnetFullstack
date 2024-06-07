using GameStore.API.Entities;

namespace GameStore.API.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync();
    }
}
