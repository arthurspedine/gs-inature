using iNature.Data;
using iNature.Models;
using Microsoft.EntityFrameworkCore;

namespace iNature.Repositories
{
    public class NoticiaRepository
    {
        private readonly OracleDbContext _context;

        public NoticiaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Noticia>> GetAllAsync()
        {
            return await _context.Noticias
                .Include(n => n.Usuario)
                // Ordens as notícias mais recentes primeiro
                .OrderByDescending(n => n.DataPublicacao)
                .ToListAsync();
        }

        public async Task<Noticia?> GetByIdAsync(int id)
        {
            return await _context.Noticias
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Noticia>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Noticias
                .Include(n => n.Usuario)
                .Where(n => n.UsuarioId == usuarioId)
                .OrderByDescending(n => n.DataPublicacao)
                .ToListAsync();
        }

        public async Task<Noticia> CreateAsync(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(noticia.Id);
        }

        public async Task<Noticia?> UpdateAsync(Noticia noticia)
        {
            _context.Noticias.Update(noticia);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(noticia.Id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null) return false;

            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}