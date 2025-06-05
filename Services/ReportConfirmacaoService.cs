using iNature.Repositories;
using iNature.Models;

namespace iNature.Services
{
    public class ReportConfirmacaoService
    {
        private readonly ReportConfirmacaoRepository _repository;
        private readonly ReportService _reportService;

        public ReportConfirmacaoService(ReportConfirmacaoRepository repository, ReportService reportService)
        {
            _repository = repository;
            _reportService = reportService;
        }

        public async Task ConfirmarAsync(int reportId, int usuarioId)
        {
            if (await _repository.ObterAsync(reportId, usuarioId) != null)
                throw new InvalidOperationException("Voce já confirmou este report.");
            if (await _reportService.BuscarPorId(reportId) == null)
                throw new InvalidOperationException("Report não encontrado.");
            var confirmacao = new ReportConfirmacao
            {
                ReportId = reportId,
                UsuarioId = usuarioId,
                DataConfirmacao = DateTime.UtcNow
            };

            await _repository.AdicionarAsync(confirmacao);
        }

        public async Task RemoverConfirmacaoAsync(int reportId, int usuarioId)
        {
            var confirmacao = await _repository.ObterAsync(reportId, usuarioId) 
                ?? throw new InvalidOperationException("Você não confirmou esse report.");
            await _repository.RemoverAsync(confirmacao);
        }
    }
}