using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/itemTypes")]
    public class ItemTypeController : Controller
    {
        private readonly IGenericService<ItemType> _itemTypeService;
        private readonly IItemService _itemService;

        public ItemTypeController(IGenericService<ItemType> itemTypeService, IItemService itemService)
        {
            _itemTypeService = itemTypeService;
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _itemTypeService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemTypeItems(int id)
        {
            var result = default(List<Item>);
            if (_itemService is ItemService concreteService)
            {
                result = await concreteService.GetItemTypeItems(id);
            }
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _itemTypeService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _itemTypeService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ItemType itemType)
        {
            bool result = await _itemTypeService.Post(itemType);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<ItemType> itemTypes)
        {
            var result = await _itemTypeService.PostBatch(itemTypes);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ItemType itemType)
        {
            bool result =  await _itemTypeService.Update(itemType);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<ItemType> itemTypes)
        {
            var result = await _itemTypeService.UpdateBatch(itemTypes);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully updated");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {  
            bool result =  await _itemTypeService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _itemTypeService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
