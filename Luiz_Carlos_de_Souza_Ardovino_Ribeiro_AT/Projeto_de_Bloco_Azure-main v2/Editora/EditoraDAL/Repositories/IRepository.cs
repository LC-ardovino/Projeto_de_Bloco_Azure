using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditoraBLL.Models;

namespace EditoraDAL.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Amigo>> GetAll();
        Task<Amigo> GetById(int id);
        bool EntityExists(int id);
        void Create(Amigo item);
        void Update(Amigo item);
        void Delete(Amigo item);
        Task SaveChangesAsync();
        void SaveChanges();

    }
}
