using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        // ✅ Hardcoded employee list
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR", Salary = 50000 },
            new Employee { Id = 2, Name = "Bob", Department = "IT", Salary = 60000 },
            new Employee { Id = 3, Name = "Charlie", Department = "Finance", Salary = 55000 }
        };

        // ✅ PUT method to update employee
        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmp)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id");

            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return BadRequest("Invalid employee id");

            emp.Name = updatedEmp.Name;
            emp.Department = updatedEmp.Department;
            emp.Salary = updatedEmp.Salary;

            return Ok(emp);
        }
    }
}