using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        //Passwords must have at least one non letter or digit character.
        //Passwords must have at least one lowercase ('a'-'z').
        //Passwords must have at least one uppercase ('A'-'Z')."

        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senha informada deve ser igual a confirmação de senha.")]
        [Display(Name = "Confirmação de Senha")]
        public string ConfirmPassword { get; set; }
    }
}