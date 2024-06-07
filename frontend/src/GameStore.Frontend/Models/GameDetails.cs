using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GameStore.Frontend.Converters;

namespace GameStore.Frontend.Models
{
    public class GameDetails
    {
        public int Id { get; set;}

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Genre field is required.")]
        [JsonConverter(typeof(JsonStringConverter))]
        public string GenreId { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "The Price must be a non-negative value.")]
        public decimal Price { get; set; }

        public DateOnly ReleaseDate { get; set; }
    }
}