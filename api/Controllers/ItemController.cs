using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;

namespace api.Controllers
{
    [Route($"api/{Globals.Version}/Item")]
    public class ItemController : Controller
    {
        ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string uid)
        {
            var result = await _itemService.Get(uid);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<string> uids)
        {
            var result = await _itemService.GetBatch(uids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _itemService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        protected async Task<IActionResult> Post([FromBody] Item item)
        {
            bool result = await _itemService.Post(item);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        protected async Task<IActionResult> PostBatch([FromBody] List<Item> items)
        {
            var result = await _itemService.PostBatch(items);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Item item)
        {
            bool result =  await _itemService.Update(item);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Item> items)
        {
            var result = await _itemService.UpdateBatch(items);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully updated");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string uid)
        {  
            bool result =  await _itemService.Delete(uid);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<string> uids)
        {
            var result = await _itemService.DeleteBatch(uids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
