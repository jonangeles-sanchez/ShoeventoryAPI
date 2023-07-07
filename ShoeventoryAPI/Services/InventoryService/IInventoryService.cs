using ShoeventoryAPI.DTOs;
using ShoeventoryAPI.Models;
using System.Threading.Tasks;

namespace ShoeventoryAPI.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<Merchant> AddNewMerchant(MerchantDto merchantDto);
        Task<Merchant> EditMerchant(int merchantId, MerchantDto merchantDto);
        Task<ShoeCollection> AddNewCollection(CollectionDto collection);
        Task<List<ShoeCollection>> GetAllMerchantCollections(int merchantId);
        Task<ShoeCollection> GetCollection(int collectionId);
        Task<ShoeCollection> EditShoeCollection(int collectionId, CollectionDto collectionDto);
        Task<Shoe> AddShoeToCollection(int collectionId, ShoeDto shoeDto);
        Task<Shoe> GetShoeFromCollection(int collectionId, int shoeId);
        Task<Shoe> EditShoe(int collectionId, int shoeId, ShoeDto shoeDto);
        Task<Merchant> DeleteMerchant(int merchantId);
        Task<ShoeCollection> DeleteCollection(int collectionId);
        Task<Shoe> DeleteShoe(int collectionId, int shoeId);

    }
}
