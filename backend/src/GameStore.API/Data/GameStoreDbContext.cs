using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameStore.API.Data
{
    public class GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) 
        : DbContext(options) 
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                Genre.Seed(1, "Fighting"),
                Genre.Seed(2, "Roleplaying"),
                Genre.Seed(3, "Sports"),
                Genre.Seed(4, "Racing"),
                Genre.Seed(5, "Kids and Family")
            );
        }
    }
}