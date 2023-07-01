using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoeventoryAPI.Models
{
    public class ShoeCollection
    {
        public int Id { get; set; }

        [Required]
        public string ShoeCollectionName { get; set; }

        public int MerchantId { get; set; }

        [JsonIgnore]
        public Merchant Merchant { get; set; }

        public List<Shoe> Shoes { get; set; }

    }
}
