namespace ShoeventoryAPI.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string MerchantName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<ShoeCollection> ShoeCollections { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
