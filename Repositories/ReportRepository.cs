using iNature.Data;
using iNature.Models;
using Microsoft.EntityFrameworkCore;

namespace iNature.Repositories
{
    public class ReportRepository
    {
        private readonly OracleDbContext _context;
        public ReportRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<Report> AddAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.Confirmacoes)
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.Data)
                .ToListAsync();
        }

        public async Task<List<Report>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.Reports
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Confirmacoes)
                .ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(int id)
        {
            return await _context.Reports
                .Include(r => r.Confirmacoes)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}