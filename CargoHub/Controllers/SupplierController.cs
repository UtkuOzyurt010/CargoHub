using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/suppliers")]
    public class SupplierController : Controller
    {
        private readonly IGenericService<Supplier> _supplierService;
        private readonly IItemService _itemService;

        public SupplierController(IGenericService<Supplier> supplierService, IItemService itemService)
        {
            _supplierService = supplierService;
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _supplierService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetSupplierItems(int id)
        {
            var result = await _itemService.GetSupplierItems(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _supplierService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _supplierService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Supplier supplier)
        {
            bool result = await _supplierService.Post(supplier);
            if (result)
            {
               return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<Supplier> suppliers)
        {
            var result = await _supplierService.PostBatch(suppliers);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Supplier supplier)
        {
            bool result =  await _supplierService.Update(supplier);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Supplier> suppliers)
        {
            var result = await _supplierService.UpdateBatch(suppliers);
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
            bool result =  await _supplierService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _supplierService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}