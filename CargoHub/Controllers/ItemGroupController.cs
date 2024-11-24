using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/itemGroups")]
    public class ItemGroupController : Controller
    {
        private readonly IGenericService<ItemGroup> _itemGroupService;
        private readonly IItemService _itemService;

        public ItemGroupController(IGenericService<ItemGroup> itemGroupService, IItemService itemService)
        {
            _itemGroupService = itemGroupService;
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _itemGroupService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemGroupItems(int id)
        {
            var result = await _itemService.GetItemGroupItems(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _itemGroupService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _itemGroupService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ItemGroup itemGroup)
        {
            bool result = await _itemGroupService.Post(itemGroup);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<ItemGroup> itemGroups)
        {
            var result = await _itemGroupService.PostBatch(itemGroups);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ItemGroup itemGroup)
        {
            bool result =  await _itemGroupService.Update(itemGroup);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<ItemGroup> itemGroups)
        {
            var result = await _itemGroupService.UpdateBatch(itemGroups);
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
            bool result =  await _itemGroupService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _itemGroupService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
