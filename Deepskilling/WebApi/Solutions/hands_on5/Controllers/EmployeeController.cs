using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hands_on5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,POC")] // Only allow Admin or POC
    public class EmployeeController : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult Get()
        {
            return Ok("Access granted to protected resource!");
        }
    }
}