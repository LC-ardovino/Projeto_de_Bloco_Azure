using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Confirmação de Senha")]
        public string ConfirmPassword { get; set; }
    }
}