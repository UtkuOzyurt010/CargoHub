using Microsoft.AspNetCore.Mvc;
using CargoHub.Models;
using CargoHub.Services;

namespace CargoHub.Controllers
{
    [Route($"api/{Globals.Version}/transfers")]
    public class TransferController : Controller
    {
        private readonly ITransfer _transferService;
        private readonly IItemService _itemService;
        private readonly IInventoryService _inventoryService;

        public TransferController(ITransfer transferService, IItemService itemService, IInventoryService inventoryService)
        {
            _transferService = transferService;
            _itemService = itemService;
            _inventoryService = inventoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _transferService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetShipmentItems(int id)
        {
            var result = await _transferService.Get(id);
            if (result is not null)
            {
                return Ok(result.Items);
            }
            return NotFound(result);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _transferService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _transferService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Transfer transfer)
        {
            bool result = await _transferService.Post(transfer);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<Transfer> transfers)
        {
            var result = await _transferService.PostBatch(transfers);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Transfer transfer)
        {
            bool result =  await _transferService.Update(transfer);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}/commit")]
        public async Task<IActionResult> Commit(int id)
        {
            List<Transfer> transfers =  await _transferService.GetBatch([id]);
            var transfer = transfers.FirstOrDefault();
            if (transfer is null)
            {
                return BadRequest("No transfer with that id was found");
            }
            if (transfer is not null && transfer.Transfer_Status != "Completed")
            {
                if (transfer.Items is null)
                {
                    return BadRequest("No Items found to transfer in the Transfer.");
                }
                foreach (TransferItem item in transfer.Items)
                {
                    List<Inventory> inventories = await _inventoryService.GetItemInventory(item.Item_Id);
                    foreach (Inventory inv in inventories)
                    {
                        if (inv.Id == transfer.Transfer_From)
                        {
                            inv.Total_On_Hand -= item.Amount;
                            inv.Total_Expected = inv.Total_On_Hand + inv.Total_Ordered;
                            inv.Total_Available = inv.Total_On_Hand - inv.Total_Allocated;
                            bool fromresult = await _inventoryService.Update(inv);
                        }
                        if (inv.Id == transfer.Transfer_To)
                        {
                            inv.Total_On_Hand += item.Amount;
                            inv.Total_Expected = inv.Total_On_Hand + inv.Total_Ordered;
                            inv.Total_Available = inv.Total_On_Hand - inv.Total_Allocated;
                            bool toresult = await _inventoryService.Update(inv);
                        }
                    }
                }
                transfer.Transfer_Status = "Completed";
                bool result = await _transferService.Update(transfer);
                return Ok("Status updated to Completed");
            }
            
            if (transfer.Transfer_Status == "Completed")
            {
                return BadRequest("Transfer already executed");
            }
            return BadRequest($"Unknown error with transfer ID {id}. Transfer status: {transfer?.Transfer_Status}");
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Transfer> transfers)
        {
            var result = await _transferService.UpdateBatch(transfers);
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
            bool result =  await _transferService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _transferService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}