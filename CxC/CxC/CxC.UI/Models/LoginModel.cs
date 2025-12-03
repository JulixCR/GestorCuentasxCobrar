using System.ComponentModel.DataAnnotations;

namespace CxC.UI.Models;

public class LoginModel
{
    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Contrasena { get; set; } = string.Empty;
}
