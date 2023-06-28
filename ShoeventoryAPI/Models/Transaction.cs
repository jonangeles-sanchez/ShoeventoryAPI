namespace ShoeventoryAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        public int ShoeCollectionId { get; set; }
        public ICollection<SoldShoe> SoldShoes { get; set; }

    }
}
