namespace ShoeventoryAPI.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string ShoeType { get; set; }
        public string ShoeName { get; set; }
        public double ShoeSize { get; set; }
        public string ShoeColor { get; set; }
        public int ShoeQuantity { get; set; }
        public double ShoePrice { get; set; }
        public int ShoeCollectionId { get; set; }
        public ShoeCollection ShoeCollection { get; set; }
    }
}
