using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditoraBLL.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Data de Nascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string? Telefone { get; set; }
    }
}
