using GameStore.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task MigrateDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
