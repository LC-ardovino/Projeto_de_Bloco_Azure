using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditoraBLL.Models
{
    public class Quadrinho
    {
        public int Id { get; set; } // Primary key
        [Required(ErrorMessage = "Título é obrigatório")]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "Editora é obrigatório")]
        public string? Editora  { get; set; }
        [Required(ErrorMessage = "Ano é obrigatório")]
        public string? Ano { get; set; }
        [Required(ErrorMessage = "Autor é obrigatório")]
        public int? AutorId { get; set; } // Foreign key
        public Autor? Autor { get; set; } // Reference navigation
    }
}
