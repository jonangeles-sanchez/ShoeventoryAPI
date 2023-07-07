using ShoeventoryAPI.DTOs;
using ShoeventoryAPI.Models;
using ShoeventoryAPI.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;

namespace ShoeventoryAPI.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly DataContext _context;

        public InventoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ShoeCollection> AddNewCollection(CollectionDto req)
        {
            var merchant = await _context.Merchants.FindAsync(req.MerchantId);

            if (merchant == null)
            {
                return null; 
            }

            var shoeCollection = new ShoeCollection
            {
                ShoeCollectionName = req.ShoeCollectionName,
                Merchant = merchant
            };

            _context.ShoeCollections.Add(shoeCollection);
            await _context.SaveChangesAsync();

            return shoeCollection;
        }
        public async Task<Merchant> AddNewMerchant(MerchantDto merchantDto)
        {
            var merchant = new Merchant
            {
                MerchantName = merchantDto.MerchantName,
                Password = merchantDto.Password,
                Email = merchantDto.Email
            };

            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();

            return merchant;
        }

        public async Task<Shoe> AddShoeToCollection(int collectionId, ShoeDto shoeDto)
        {
            var collection = await _context.ShoeCollections.FindAsync(collectionId);
            if (collection is null)
            {
                return null; // Collection not found
            }

            var shoe = new Shoe
            {
                Manufacturer = shoeDto.Manufacturer,
                ShoeType = shoeDto.ShoeType,
                ShoeName = shoeDto.ShoeName,
                ShoeSize = shoeDto.ShoeSize,
                ShoeColor = shoeDto.ShoeColor,
                ShoeQuantity = shoeDto.ShoeQuantity,
                ShoePrice = shoeDto.ShoePrice,
                ShoeCollection = collection
            };

            _context.Shoes.Add(shoe);
            await _context.SaveChangesAsync();

            return shoe;
        }

        public async Task<ShoeCollection> GetCollection(int collectionId)
        {
            var collection = await _context.ShoeCollections
                .Include(c => c.Shoes)
                .FirstOrDefaultAsync(c => c.Id == collectionId);

            return collection;
        }

        public async Task<List<ShoeCollection>> GetAllMerchantCollections(int merchantId)
        {
            /*
            var collections = await _context.ShoeCollections
                .Where(c => c.MerchantId == merchantId)
                .ToListAsync();
            */
            var collections = await _context.Merchants
                .Where(m => m.Id == merchantId)
                .SelectMany(m => m.ShoeCollections)
                .Include(c => c.Shoes)
                .ToListAsync();

            return collections;

        }

        public async Task<Shoe> GetShoeFromCollection(int collectionId, int shoeId)
        {
            var shoe = await _context.Shoes
                .FirstOrDefaultAsync(s => s.ShoeCollectionId == collectionId && s.Id == shoeId);

            return shoe;
        }

        public async Task<Shoe> EditShoe(int collectionId, int shoeId, ShoeDto shoeDto)
        {
            var collection = await _context.ShoeCollections.Include(c => c.Shoes)
                                                   .FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection is null)
            {
                return null;
            }

            var shoe = collection.Shoes.FirstOrDefault(s => s.Id == shoeId);
            if (shoe is null)
            {
                return null;
            }

            // Update the shoe properties
            shoe.Manufacturer = shoeDto.Manufacturer;
            shoe.ShoeType = shoeDto.ShoeType;
            shoe.ShoeName = shoeDto.ShoeName;
            shoe.ShoeSize = shoeDto.ShoeSize;
            shoe.ShoeColor = shoeDto.ShoeColor;
            shoe.ShoeQuantity = shoeDto.ShoeQuantity;
            shoe.ShoePrice = shoeDto.ShoePrice;

            await _context.SaveChangesAsync();

            return shoe;
        }

        public async Task<ShoeCollection> EditShoeCollection(int collectionId, CollectionDto collectionDto)
        {
            var collection = await _context.ShoeCollections.FindAsync(collectionId);
            if (collection == null)
                return null;

            // Update the shoe collection properties
            collection.ShoeCollectionName = collectionDto.ShoeCollectionName;

            await _context.SaveChangesAsync();

            return collection;
        }

        public async Task<Merchant> EditMerchant(int merchantId, MerchantDto req)
        {
            var merchant = await _context.Merchants.FindAsync(merchantId);
            if(merchant == null)
            {
                return null;
            }
            merchant.MerchantName = req.MerchantName;
            merchant.Password = req.Password;
            merchant.Email = req.Email;

            await _context.SaveChangesAsync();

            return merchant;
        }

        public async Task<Merchant> DeleteMerchant(int merchantId)
        {
            var merchant = await _context.Merchants.FindAsync(merchantId);
            if(merchant == null)
            {
                return null;
            }
            _context.Remove(merchant);
            _context.SaveChanges();

            return merchant;
        }

        public async Task<ShoeCollection> DeleteCollection(int collectionId)
        {
            var collection = await _context.ShoeCollections.FindAsync(collectionId);
            if(collection is null)
            {
                return null;
            }
            _context.Remove(collection);
            _context.SaveChanges();

            return collection;
        }

        public async Task<Shoe> DeleteShoe(int collectionId, int shoeId)
        {
            var collection = await _context.ShoeCollections.Include(c => c.Shoes)
                                                   .FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection is null)
            {
                return null;
            }

            var shoe = collection.Shoes.FirstOrDefault(s => s.Id == shoeId);
            if (shoe is null)
            {
                return null;
            }

            collection.Shoes.Remove(shoe);
            await _context.SaveChangesAsync();

            return shoe;
        }
    }
}
