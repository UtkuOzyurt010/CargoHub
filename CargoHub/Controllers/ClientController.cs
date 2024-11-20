using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CargoHub.Models;
using CargoHub.Services;


namespace CargoHub.Controllers
{

    [Route($"api/{Globals.Version}/client")]
    public class ClientController : Controller
    {
        ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _clientService.Get(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _clientService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() // custom filters can be added
        {
            var result = await _clientService.GetAll(); //filters can be added to get all
            return Ok(result);
        }

        [HttpPost()]
        protected async Task<IActionResult> Post([FromBody] Client client)
        {
            bool result = await _clientService.Post(client);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("batch")]
        protected async Task<IActionResult> PostBatch([FromBody] List<Client> clients)
        {
            var result = await _clientService.PostBatch(clients);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got successfully posted");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Client client)
        {
            bool result =  await _clientService.Update(client);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Client> clients)
        {
            var result = await _clientService.UpdateBatch(clients);
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
            bool result =  await _clientService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _clientService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
