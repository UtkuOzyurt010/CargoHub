using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/warehouses")]
    public class WarehouseController : Controller
    {
        private readonly IGenericService<Warehouse> _warehouseService;
        private readonly ILocationService _locationService;

        public WarehouseController(IGenericService<Warehouse> warehouseService, ILocationService locationService)
        {
            _warehouseService = warehouseService;
            _locationService = locationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _warehouseService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/locations")]
        public async Task<IActionResult> GetLocations(int id)
        {
            var result = default(List<Location>);
            if (_locationService is LocationService concreteService)
            {
                result = await concreteService.GetWarehouseLocations(id);
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
            var result = await _warehouseService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _warehouseService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Warehouse warehouse)
        {
            bool result = await _warehouseService.Post(warehouse);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        protected async Task<IActionResult> PostBatch([FromBody] List<Warehouse> warehouses)
        {
            var result = await _warehouseService.PostBatch(warehouses);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Warehouse warehouse)
        {
            bool result =  await _warehouseService.Update(warehouse);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Warehouse> warehouses)
        {
            var result = await _warehouseService.UpdateBatch(warehouses);
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
            bool result =  await _warehouseService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _warehouseService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}