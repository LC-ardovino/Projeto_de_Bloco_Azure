using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditoraBLL.Models;

namespace EditoraDAL.Repositories
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAll();
        Task<Autor> GetById(int id);
        bool EntityExists(int id);
        void Create(Autor item);
        void Update(Autor item);
        void Delete(Autor item);
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
