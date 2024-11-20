using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [Route($"api/{Globals.Version}/warehouse")]
    public class WarehouseController : Controller
    {
        WarehouseService _warehouseService;

        public WarehouseController(IGenericService<Warehouse> warehouseService)
        {
            _warehouseService = (WarehouseService)warehouseService;
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
        protected async Task<IActionResult> Post([FromBody] Warehouse warehouse)
        {
            bool result = await _warehouseService.Post(warehouse);
            if (result)
            {
                return Ok();
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