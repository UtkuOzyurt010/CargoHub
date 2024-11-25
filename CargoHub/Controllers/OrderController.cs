using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;

        public OrderController(IOrderService orderService, IItemService itemService)
        {
            _orderService = orderService;
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _orderService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetOrderItems(int id)
        {
            var result = await _orderService.Get(id);
            var items = _itemService.GetOrderItems(result.ItemsJson);
            if (result is not null)
            {
                return Ok(items);
            }
            return NotFound(items);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _orderService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _orderService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            bool result = await _orderService.Post(order);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<Order> orders)
        {
            var result = await _orderService.PostBatch(orders);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Order order)
        {
            bool result =  await _orderService.Update(order);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Order> orders)
        {
            var result = await _orderService.UpdateBatch(orders);
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
            bool result =  await _orderService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _orderService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
