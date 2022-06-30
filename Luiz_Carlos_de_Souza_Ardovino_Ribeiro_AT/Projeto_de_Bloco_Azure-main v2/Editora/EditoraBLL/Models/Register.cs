using System.ComponentModel.DataAnnotations;

namespace EditoraBLL.Models.Auth;

public class Register
{
    [Required(ErrorMessage = "Informe o User Name")]
    public string Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Informe o Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Confirmação de Senha")]
    [Required(ErrorMessage = "Informe a Confirmação de senha")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}
