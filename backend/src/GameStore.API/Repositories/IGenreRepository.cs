using GameStore.API.Models.DTOs;

namespace GameStore.API.Repositories
{
    public interface IGenreRepository
    {
        Task<List<GenreSummaryDTO>> GetAllAsync();
    }
}