namespace CxC.UI.Models;

public class UserRecord
{
    public int IdUsuario { get; set; }

    public string Usuario { get; set; } = string.Empty;

    public string Contrasena { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public bool Activo { get; set; }
}
