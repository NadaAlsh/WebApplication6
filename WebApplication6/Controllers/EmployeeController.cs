
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
  
        [Route("api/employee")]
        [ApiController]
        public class EmployeeController : ControllerBase
        {
            private readonly BankContext _context;

            public EmployeeController(BankContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
            {
                return await _context.Employees.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Employee>> GetEmployee(int id)
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return employee;
            }

            [HttpPost]
            public async Task<ActionResult<Employee>> Add(AddEmployeeRequest request)
            {
                var employee = new Employee
                {
                    Name = request.Name,
                    CivilId = request.CivilId,
                    Position = request.Position
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> EditEmployee(int id, AddEmployeeRequest request)
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.Name = request.Name;
                employee.CivilId = request.CivilId;
                employee.Position = request.Position;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEmployee(int id)
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool EmployeeExists(int id)
            {
                return _context.Employees.Any(e => e.Id == id);
            }
        }
    
}
