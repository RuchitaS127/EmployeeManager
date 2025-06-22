using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using EmployeeService.Models;
using System.Threading.Tasks;
using EmployeeService.Services;

namespace EmployeeService.Repos
{
    public class EmployeeRepository : IEmployeeService
    {
        private static readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeDB;Trusted_Connection=True;";

        public EmployeeRepository()
        {
        }

        // CREATE
        public async Task<bool> AddEmployee(Employee employee)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            const string query = @"INSERT INTO [dbo].[Employees] (FirstName, LastName, Gender, Email) 
                                   VALUES (@FirstName, @LastName, @Gender, @Email)";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = employee.LastName;
            command.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = employee.Gender;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        // READ
        public async Task<List<Employee>> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM [dbo].[Employees] ";
            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Employee emp = new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FirstName = reader["FirstName"] != null ? reader["FirstName"].ToString() : string.Empty,
                    LastName = reader["LastName"] != null ? reader["LastName"].ToString() : string.Empty,
                    Gender = reader["Gender"] != null ? reader["Gender"].ToString() : string.Empty,
                    Email = reader["Email"] != null ? reader["Email"].ToString() : string.Empty
                };
                employees.Add(emp);
            }

            return employees;
        }

        // UPDATE
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            const string query = @"UPDATE [dbo].[Employees] 
                                   SET FirstName = @FirstName, LastName = @LastName, 
                                       Gender = @Gender, Email = @Email 
                                   WHERE Id = @Id";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = employee.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = employee.LastName;
            command.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = employee.Gender;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;
            command.Parameters.Add("@Id", SqlDbType.Int).Value = employee.Id;

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        // DELETE
        public async Task<bool> DeleteEmployee(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM [dbo].[Employees]  WHERE Id = @Id";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> RestoreEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}
