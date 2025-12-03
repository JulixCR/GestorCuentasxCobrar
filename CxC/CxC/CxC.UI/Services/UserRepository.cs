using CxC.UI.Data;
using CxC.UI.Models;
using Dapper;
using System.Data;

namespace CxC.UI.Services;

public class UserRepository : IUserRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public UserRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<UserRecord?> GetUserAsync(string username, string password)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<UserRecord>(
            "PA_ValidarUsuario",
            new { Usuario = username, Contrasena = password },
            commandType: CommandType.StoredProcedure);
    }
}
