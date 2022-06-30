using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EditoraBLL.Models;
using System.Threading.Tasks;

namespace EditoraDAL.Repositories
{
    public interface IQuadrinhoRepository
    {
        Task<IEnumerable<Quadrinho>> GetAll();
        Task<Quadrinho> GetById(int id);
        bool EntityExists(int id);
        void Create(Quadrinho item);
        void Update(Quadrinho item);
        void Delete(Quadrinho item);
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
