using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EditoraBLL.Models;

namespace EditoraDAL.Repositories
{
    public  class AutorRepository : IAutorRepository
    { 
        private readonly DbContext _context;

        public AutorRepository(DbContext context)
        {
            _context = context;
        }

        public bool EntityExists(int id)
        {
            return _context.Set<Autor>().Any(p => p.Id == id);
        }
        public async Task<Autor> GetById(int id)
        {

            return await _context.Set<Autor>()
                .Include(b => b.Quadrinhos)
                .SingleOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<Autor>> GetAll()
        {
            return await _context.Set<Autor>()
                .AsNoTracking()
                .Include(b => b.Quadrinhos)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }
        //public async Task<IEnumerable<Autor>> GetByCondition(Expression<Func<Autor, bool>> expression)
        //{
        //    return await _context.Set<Autor>().Where(expression)
        //        .Include(b => b.Quadrinhos)
        //        .OrderBy(p => p.Nome)
        //        .ToListAsync();
        //}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Create(Autor item)
        {
            _context.Set<Autor>().Add(item);
        }

        public void Update(Autor item)
        {
            _context.Set<Autor>().Update(item);
        }

        public void Delete(Autor item)
        {
            _context.Set<Autor>().Remove(item);
        }
    }
}
