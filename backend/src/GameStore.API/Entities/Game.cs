namespace GameStore.API.Entities
{
    public class Game
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public int GenreId { get; private set; }

        public Genre? Genre { get; private set; }

        public decimal Price { get; private set; }

        public DateOnly ReleaseDate { get; private set; }

        private Game() { }

        public static Game Create(string name, int genreId, decimal price, DateOnly releaseDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            if (genreId <= 0)
                throw new ArgumentException($"'{nameof(genreId)}' cannot be less than or equal to zero.", nameof(genreId));

            if (price < 0)
                throw new ArgumentException($"'{nameof(price)}' cannot be a non-negative value.", nameof(price));

            if (releaseDate == DateOnly.MinValue)
                throw new ArgumentException($"'{nameof(releaseDate)}' cannot be equal to {DateOnly.MinValue}. It must be set to a valid date.", nameof(releaseDate));

            return new Game()
            {
                Name = name,
                GenreId = genreId,
                Price = price,
                ReleaseDate = releaseDate
            };
        }

        public void Update(string name, int genreId, decimal price, DateOnly releaseDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            if (genreId <= 0)
                throw new ArgumentException($"'{nameof(genreId)}' cannot be less than or equal to zero.", nameof(genreId));

            if (price < 0)
                throw new ArgumentException($"'{nameof(price)}' cannot be a non-negative value.", nameof(price));

            if (releaseDate == DateOnly.MinValue)
                throw new ArgumentException($"'{nameof(releaseDate)}' cannot be equal to {DateOnly.MinValue}. It must be set to a valid date.", nameof(releaseDate));

            Name = name;
            GenreId = genreId;
            Price = price;
            ReleaseDate = releaseDate;
        }
    }
}