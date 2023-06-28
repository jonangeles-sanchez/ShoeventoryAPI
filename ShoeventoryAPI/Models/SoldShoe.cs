namespace ShoeventoryAPI.Models
{
    public class SoldShoe
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public double ShoeSize { get; set; }
        public int ShoeQuantity { get; set; }
        public int ShoeId { get; set; }

    }
}
