using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace CargoHub.Controllers
{

    [Route($"api/{Globals.Version}")]
    public class BaseController : Controller
    {

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok("優柔不断 Test");
        }

        [HttpHead()]
        public IActionResult Head()
        {
            return Ok();
        }
    }
}