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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "home.html");
            return PhysicalFile(filePath, "text/html");
        }

        [HttpHead()]
        public IActionResult Head()
        {
            return Ok();
        }
    }
}