using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditoraBLL.Models;
using Microsoft.EntityFrameworkCore;

namespace EditoraDAL.Repositories
{
    public class QuadrinhoRepository : IQuadrinhoRepository
    {
        private readonly DbContext _context;

        public QuadrinhoRepository(DbContext context)
        {
            _context = context;
        }

        public bool EntityExists(int id)
        {
            return _context.Set<Quadrinho>().Any(p => p.Id == id);
        }
        public async Task<Quadrinho> GetById(int id)
        {

            return await _context.Set<Quadrinho>()
                .Include(b => b.Autor)
                .SingleOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<Quadrinho>> GetAll()
        {
            return await _context.Set<Quadrinho>()
                .AsNoTracking()
                .Include(b => b.Autor)
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }
        //public async Task<IEnumerable<Quadrinho>> GetByCondition(Expression<Func<Quadrinho, bool>> expression)
        //{
        //    return await _context.Set<Quadrinho>().Where(expression)
        //        //.Include(b => b.Autores)
        //        .OrderBy(p => p.Titulo)
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

        public void Create(Quadrinho item)
        {
            _context.Set<Quadrinho>().Add(item);
        }

        public void Update(Quadrinho item)
        {
            _context.Set<Quadrinho>().Update(item);

        }

        public void Delete(Quadrinho item)
        {
            _context.Set<Quadrinho>().Remove(item);
        }
    }
}
