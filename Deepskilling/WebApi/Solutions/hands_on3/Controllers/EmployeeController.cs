using Microsoft.AspNetCore.Mvc;
using hands_on3.Models;
using hands_on3.Filters;

namespace hands_on3.Controllers;

[ApiController]
[Route("api/[controller]")]
[TypeFilter(typeof(CustomAuthFilter))]
[TypeFilter(typeof(CustomExceptionFilter))]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Employee>> GetStandard()
    {
        throw new Exception("This is a demo exception"); // to trigger CustomExceptionFilter
    }

    [HttpPost]
    public ActionResult<Employee> Post([FromBody] Employee emp)
    {
        return Ok(emp);
    }
}