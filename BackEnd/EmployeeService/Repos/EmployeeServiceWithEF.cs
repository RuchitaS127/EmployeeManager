using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using EmployeeService.Models;
using System.Threading.Tasks;
using EmployeeService.Data;
using Microsoft.EntityFrameworkCore;


namespace EmployeeService.Services
{
    public interface IEmployeeService
    {
        Task<bool> AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployees();
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<bool> RestoreEmployee(int id);
    }

    public class EmployeeServiceWithEF : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeServiceWithEF(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<bool> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }


        // READ
        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _context.Employees.Where(e => e.Deleted == false).ToListAsync();
            return employees;
        }

        // UPDATE
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee != null)
            {
                employee.Deleted = true; // Soft delete
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreEmployee(int id)
        {
            var employee = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee != null)
            {
                employee.Deleted = false; // Soft delete
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
