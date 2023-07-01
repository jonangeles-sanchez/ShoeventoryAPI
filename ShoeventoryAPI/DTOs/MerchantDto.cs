using System.ComponentModel.DataAnnotations;

namespace ShoeventoryAPI.DTOs
{
    public class MerchantDto
    {
        public string MerchantName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
