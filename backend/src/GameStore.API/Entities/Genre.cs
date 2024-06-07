namespace GameStore.API.Entities
{
    public class Genre
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        private Genre() { }

        public static Genre Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            return new Genre()
            {
                Name = name
            };
        }

        public static Genre Seed(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentException($"'{nameof(id)}' cannot be less than or equal to zero.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            return new Genre()
            {
                Id = id,
                Name = name
            };
        }
    }
}