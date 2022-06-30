using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditoraBLL.Models;

namespace EditoraDAL.Repositories
{
    public interface IPerfilRepository
    { 
        Task<IEnumerable<Perfil>> GetAll();
        Task<Perfil> GetById(string username);
        bool EntityExists(string username);
        void Create(Perfil item);
        void Update(Perfil item);
        void Delete(Perfil item);
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
