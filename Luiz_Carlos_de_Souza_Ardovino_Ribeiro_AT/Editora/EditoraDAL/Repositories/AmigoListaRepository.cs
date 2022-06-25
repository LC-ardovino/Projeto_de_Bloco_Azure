using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EditoraBLL.Models;

namespace EditoraDAL.Repositories
{
    public class AmigoListaRepository : IRepository
    {
        private readonly DbContext _context;

        public AmigoListaRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amigo>> GetAll()
        {

            List<Amigo> ListaAmigos = await _context.Set<Amigo>()
               .AsNoTracking()
               .OrderBy(p => p.Nome)
               .ToListAsync();

            if (ListaAmigos.Count == 0)
            {
                _context.Add(new Amigo() { Nome = "Jose", Sobrenome = "da Silva", Email = "jose@gmail.com", DataNascimento = DateTime.Now, Telefone = "9999-9999" });
                _context.Add(new Amigo() { Nome = "Antonio", Sobrenome = "Vieira", Email = "antonio@gmail.com", DataNascimento = DateTime.Now, Telefone = "9999-9999" });
                _context.Add(new Amigo() { Nome = "Maria", Sobrenome = "das Rosas", Email = "maria@gmail.com", DataNascimento = DateTime.Now, Telefone = "9999-9999" });
                _context.Add(new Amigo() { Nome = "Eduardo", Sobrenome = "Mello", Email = "eduardo@gmail.com", DataNascimento = DateTime.Now, Telefone = "9999-9999" });

                await _context.SaveChangesAsync();

                return await _context.Set<Amigo>()
                   .AsNoTracking()
                   .OrderBy(p => p.Nome)
                   .ToListAsync();

            }

            return ListaAmigos;
        }

        public async Task<Amigo> GetById(int id)
        {

            return await _context.Set<Amigo>().FirstOrDefaultAsync(b => b.Id == id);

        }
        public bool EntityExists(int id)
        {
            return _context.Set<Amigo>().Any(p => p.Id == id);
        }
        public void Create(Amigo item)
        {
            _context.Set<Amigo>().Add(item);
        }

        public void Update(Amigo item)
        {
            _context.Set<Amigo>().Update(item);
        }

        public  void Delete(Amigo item)
        {
            _context.Set<Amigo>().Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
