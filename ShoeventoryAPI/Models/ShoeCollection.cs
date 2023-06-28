namespace ShoeventoryAPI.Models
{
    public class ShoeCollection
    {
        public int Id { get; set; }
        public string ShoeCollectionName { get; set; }
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
