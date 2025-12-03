using CxC.UI.Data;
using CxC.UI.Models;
using Dapper;

namespace CxC.UI.Services;

public class UserRepository : IUserRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public UserRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<UserRecord?> GetUserAsync(string username)
    {
        const string query = @"SELECT TOP 1 Id, Usuario, Nombre, Contrasena FROM Usuarios WHERE Usuario = @Username";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<UserRecord>(query, new { Username = username });
    }
}
