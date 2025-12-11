using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly string _connectionString = "Server=localhost;Database=Test;User Id=sa;Password=YourPassword;";

    [HttpGet]
    public IActionResult GetUser(string username)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        // ❌ VULNERABLE: SQL Injection
        var sql = $"SELECT * FROM Users WHERE Username = '{username}'";

        using var command = new SqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        return Ok("Query executed");
    }
}
