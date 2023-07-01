using System.ComponentModel.DataAnnotations;

namespace ShoeventoryAPI.Models
{
    public class Merchant
    {
        public int Id { get; set; }

        [Required]
        public string MerchantName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<ShoeCollection> ShoeCollections { get; set; }
    }
}
