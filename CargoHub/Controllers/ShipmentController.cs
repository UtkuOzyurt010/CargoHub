using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/shipments")]
    public class ShipmentController : Controller
    {
        private readonly IShipmentService _shipmentService;
        private readonly IItemService _itemService;
        private readonly IOrderService _orderService;

        public ShipmentController(IShipmentService shipmentService, IItemService itemService, IOrderService orderService)
        {
            _shipmentService = shipmentService;
            _itemService = itemService;
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _shipmentService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetShipmentItems(int id)
        {
            var result = await _shipmentService.Get(id);
            if (result is not null)
            {
                return Ok(result.Items);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetShipmentOrders(int id)
        {
            var result = default(Order);
            if (_orderService is OrderService concreteService)
            {
                result = await concreteService.GetShipmentOrder(id);
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
            var result = await _shipmentService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _shipmentService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Shipment shipment)
        {
            bool result = await _shipmentService.Post(shipment);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<Shipment> shipments)
        {
            var result = await _shipmentService.PostBatch(shipments);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Shipment shipment)
        {
            bool result =  await _shipmentService.Update(shipment);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Shipment> shipments)
        {
            var result = await _shipmentService.UpdateBatch(shipments);
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
            bool result =  await _shipmentService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _shipmentService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
