using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Repos;
using EmployeeService.Services;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEmployeeService _empService;

    public EmployeesController(AppDbContext context, IEmployeeService empService)
    {
        _context = context;
        _empService = empService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        var result = await _context.Employees.ToListAsync();
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
        var employee = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
        return employee == null || employee.Deleted ? NotFound("Employee not found.") : Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee employee)
    {
        var result1 = await _context.Employees.Where(x => x.Email == employee.Email).ToListAsync();

        var result2 = await _context.Employees.Where(x => x.Email == employee.Email).AnyAsync();
        if (result2 == true)
        {
            return BadRequest("Email already assigned to existing Employee.");
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        if (id != employee.Id) return BadRequest();

        var result2 = await _context.Employees
            .Where(x => x.Email == employee.Email)
            .Where(x => x.Id != employee.Id)
            .AnyAsync();
        if (result2 == true)
        {
            return BadRequest("Email already assigned to existing Employee.");
        }
        await _empService.UpdateEmployee(employee);
        return NoContent();
    }

    [HttpPut("restore/{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await _empService.RestoreEmployee(id);
        if (result)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _empService.DeleteEmployee(id);
        if (result)
        {
            return NoContent();
        }
        return NotFound();
    }
}
