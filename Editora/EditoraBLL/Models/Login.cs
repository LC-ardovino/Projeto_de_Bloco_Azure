using System.ComponentModel.DataAnnotations;

namespace EditoraBLL.Models.Auth;

public class Login
{
    [Display(Name = "Usuário")]
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [Display(Name = "Senha")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}