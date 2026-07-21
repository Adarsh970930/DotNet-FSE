using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Alice",
                Salary = 50000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "HR" },
                Skills = new List<Skill> { new Skill { Id = 1, Name = "Communication" } },
                DateOfBirth = new DateTime(1990, 01, 01)
            }
        };

        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            return Ok(_employees);
        }

        [HttpPost]
        public ActionResult<Employee> Create([FromBody] Employee newEmp)
        {
            newEmp.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(newEmp);
            return CreatedAtAction(nameof(GetAll), new { id = newEmp.Id }, newEmp);
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> Update(int id, [FromBody] Employee updatedEmp)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id");

            var existingEmp = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmp == null)
                return BadRequest("Invalid employee id");

            existingEmp.Name = updatedEmp.Name;
            existingEmp.Salary = updatedEmp.Salary;
            existingEmp.Permanent = updatedEmp.Permanent;
            existingEmp.Department = updatedEmp.Department;
            existingEmp.Skills = updatedEmp.Skills;
            existingEmp.DateOfBirth = updatedEmp.DateOfBirth;

            return Ok(existingEmp);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return BadRequest("Invalid employee id");

            _employees.Remove(emp);
            return Ok($"Employee with id {id} deleted.");
        }
    }
}