using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditoraBLL.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string? SobreNome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatório")]
        public DateTime? DataNascimento { get; set; }

        public AutorAsset? AutorAssets { get; set; } // Propriedade de navegação de REFERÊNCIA

        public IList<Quadrinho>? Quadrinhos { get; set; } // Propriedade de navegação de COLEÇÃO

    }
}
