using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SomeCallsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { request = true });
        }
    }
}
