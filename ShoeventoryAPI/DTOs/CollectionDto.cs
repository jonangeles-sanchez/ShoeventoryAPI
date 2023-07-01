using System.ComponentModel.DataAnnotations;

namespace ShoeventoryAPI.DTOs
{
    public class CollectionDto
    {
        public string ShoeCollectionName { get; set; }
        public int MerchantId { get; set; }
    }
}
