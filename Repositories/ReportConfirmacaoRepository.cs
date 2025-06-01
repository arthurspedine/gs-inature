using iNature.Data;
using iNature.Models;
using Microsoft.EntityFrameworkCore;

namespace iNature.Repositories
{
    public class ReportConfirmacaoRepository
    {
        private readonly OracleDbContext _context;
        
        public ReportConfirmacaoRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<bool> JaConfirmouAsync(int reportId, int usuarioId)
        {
            return await _context.ReportConfirmacoes
                .AnyAsync(rc => rc.ReportId == reportId && rc.UsuarioId == usuarioId);
        }

        public async Task<ReportConfirmacao> AdicionarAsync(ReportConfirmacao confirmacao)
        {
            _context.ReportConfirmacoes.Add(confirmacao);
            await _context.SaveChangesAsync();
            return confirmacao;
        }

        public async Task<ReportConfirmacao?> ObterAsync(int reportId, int usuarioId)
        {
            return await _context.ReportConfirmacoes
                .FirstOrDefaultAsync(rc => rc.ReportId == reportId && rc.UsuarioId == usuarioId);
        }

        public async Task RemoverAsync(ReportConfirmacao confirmacao)
        {
            _context.ReportConfirmacoes.Remove(confirmacao);
            await _context.SaveChangesAsync();
        }
    }
}