using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoeventoryAPI.Models
{
    public class Shoe
    {
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string ShoeType { get; set; }

        [Required]
        public string ShoeName { get; set; }

        [Range(0.5, 20.0)]
        public double ShoeSize { get; set; }

        [Required]
        public string ShoeColor { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ShoeQuantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double ShoePrice { get; set; }

        public int ShoeCollectionId { get; set; }

        [JsonIgnore]
        public ShoeCollection ShoeCollection { get; set; }
    }
}
