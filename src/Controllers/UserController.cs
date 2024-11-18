using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DotnetDemoapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration["DatabaseConnection:ConnectionString"];
        }

        [HttpGet("search")]
        public IActionResult SearchUsers(string username)
        {
            // Intentionally vulnerable to SQL injection
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM Users WHERE Username LIKE '{username}'", connection);
                var reader = command.ExecuteReader();
                // Process results...
                return Ok("Search completed");
            }
        }
    }
}