using Microsoft.AspNetCore.Mvc;
using ShoeventoryAPI.Services.InventoryService;
using ShoeventoryAPI.DTOs;
using System.Threading.Tasks;
using ShoeventoryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ShoeventoryAPI.Controllers
{
    [ApiController]
    [Route("api/merchants")]
    public class MerchantController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public MerchantController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public async Task<ActionResult<Merchant>> AddNewMerchant(MerchantDto merchantDto)
        {
            var newMerchant = await _inventoryService.AddNewMerchant(merchantDto);
            if (newMerchant is null)
            {
                return BadRequest("Failed to create a new merchant.");
            }
            return newMerchant;
        }

        [HttpPut("{merchantid}")]
        public async Task<ActionResult<Merchant>> EditMerchant(int merchantid, MerchantDto req)
        {
            var editedMerchant = await _inventoryService.EditMerchant(merchantid, req);
            if (editedMerchant is null)
            {
                return BadRequest("Failed to edit merchant");
            }
            return editedMerchant;
        }

        [HttpDelete("{merchantid}")]
        public async Task<ActionResult<Merchant>> DeleteMerchant(int merchantid)
        {
            var deletedMerchant = await _inventoryService.DeleteMerchant(merchantid);
            if(deletedMerchant is null)
            {
                return BadRequest("The merchant already doesn't exist.");
            }
            return deletedMerchant;
        }
    }

    [ApiController]
    [Route("api/collections")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("collections")]
        public async Task<ActionResult<ShoeCollection>> AddNewCollection(CollectionDto req)
        {
            var updatedCollection = await _inventoryService.AddNewCollection(req);
            if (updatedCollection is null)
            {
                return NotFound("There was an issue adding a new shoe collection.");
            }
            return updatedCollection;
        }

        [HttpGet("{collectionId}")]
        public async Task<ActionResult<ShoeCollection>> GetCollection(int collectionId)
        {
            var collection = await _inventoryService.GetCollection(collectionId);
            if (collection is null)
            {
                return NotFound("Shoe collection not found.");
            }
            return collection;
        }

        [HttpGet("{merchantId}/merchant")]
        public async Task<ActionResult<List<ShoeCollection>>> GetAllMerchantCollections(int merchantId)
        {
            var collections = await _inventoryService.GetAllMerchantCollections(merchantId);
            if (collections is null)
            {
                return NotFound("Merchant was not found.");
            }
            return collections;
        }

        [HttpPut("{collectionId}")]
        public async Task<ActionResult<ShoeCollection>> EditShoeCollection(int collectionId, CollectionDto collectionDto)
        {
            var updatedCollection = await _inventoryService.EditShoeCollection(collectionId, collectionDto);
            if (updatedCollection == null)
            {
                return NotFound("Shoe collection not found.");
            }

            return updatedCollection;
        }

        [HttpPost("{collectionId}/shoes")]
        public async Task<ActionResult<Shoe>> AddShoeToCollection(int collectionId, ShoeDto shoeDto)
        {
            var newShoe = await _inventoryService.AddShoeToCollection(collectionId, shoeDto);
            if (newShoe is null)
            {
                return BadRequest("Failed to add a new shoe to the collection.");
            }
            return newShoe;
        }

        [HttpGet("{collectionId}/shoes/{shoeId}")]
        public async Task<ActionResult<Shoe>> GetShoeFromCollection(int collectionId, int shoeId)
        {
            var shoe = await _inventoryService.GetShoeFromCollection(collectionId, shoeId);
            if (shoe is null)
            {
                return NotFound("Shoe not found in the specified collection.");
            }
            return shoe;
        }

        [HttpPut("{collectionId}/shoes/{shoeId}")]
        public async Task<ActionResult<Shoe>> EditShoe(int collectionId, int shoeId, ShoeDto shoeDto)
        {
            var updatedShoe = await _inventoryService.EditShoe(collectionId, shoeId, shoeDto);
            if (updatedShoe == null)
            {
                return NotFound("Shoe not found.");
            }

            return updatedShoe;
        }

        [HttpDelete("{collectionId}")]
        public async Task<ActionResult<ShoeCollection>> DeleteCollection(int collectionId)
        {
            var deletedCollection = await _inventoryService.DeleteCollection(collectionId);
            if(deletedCollection is null)
            {
                return NotFound("The collection you wish to delete already doesn't exist.");
            }

            return deletedCollection;
        }

        [HttpDelete("{collectionId}/shoes/{shoeId}")]
        public async Task<ActionResult<Shoe>> DeleteShoeFromCollection(int collectionId, int shoeId)
        {
            var deletedShoe = await _inventoryService.DeleteShoe(collectionId, shoeId);
            if(deletedShoe is null)
            {
                return NotFound("The shoe or collection does not exist.");
            }
            return deletedShoe;
        }

    }
}
