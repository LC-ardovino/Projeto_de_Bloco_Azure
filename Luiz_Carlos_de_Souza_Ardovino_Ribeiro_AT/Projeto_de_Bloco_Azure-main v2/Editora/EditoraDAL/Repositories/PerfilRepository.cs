using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EditoraBLL.Models;

namespace EditoraDAL.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly DbContext _context;

        public PerfilRepository(DbContext context)
        {
            _context = context;
        }

        public bool EntityExists(string username)
        {
            return _context.Set<Perfil>().Any(p => p.Username == username);
        }
        public async Task<Perfil> GetById(string username)
        {

            return await _context.Set<Perfil>()
                .SingleOrDefaultAsync(b => b.Username == username);
        }
        public async Task<IEnumerable<Perfil>> GetAll()
        {
            return await _context.Set<Perfil>()
                .AsNoTracking()
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Create(Perfil item)
        {
            _context.Set<Perfil>().Add(item);
        }

        public void Update(Perfil item)
        {
            _context.Set<Perfil>().Update(item);
        }

        public void Delete(Perfil item)
        {
            _context.Set<Perfil>().Remove(item);
        }
    }
}
