using iNature.Data;
using iNature.Models;
using Microsoft.EntityFrameworkCore;

namespace iNature.Repositories
{

    public class UsuarioRepository
    {
        private readonly OracleDbContext _context;

        public UsuarioRepository(OracleDbContext context)
        {
            _context = context;
        }
        public async Task SalvarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}