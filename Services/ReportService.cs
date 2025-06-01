using iNature.Repositories;
using iNature.Models;
using iNature.Models.DTOs;

namespace iNature.Services
{
    public class ReportService
    {
        private readonly ReportRepository _repository;

        public ReportService(ReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReportResponseDTO> CriarReportAsync(ReportCreateDTO dto, int usuarioId)
        {
            var tipoReport = Enum.Parse<TipoReport>(dto.Tipo, ignoreCase: true);
            var report = new Report
            {
                Titulo = dto.Titulo,
                Corpo = dto.Corpo,
                Tipo = tipoReport,
                Cidade = dto.Cidade,
                Bairro = dto.Bairro,
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                UsuarioId = usuarioId
            };

            await _repository.AddAsync(report);

            return ReportResponseDTO.FromReport(report);
        }

        public async Task<IEnumerable<ReportResponseDTO>> ListarTodosAsync()
        {
            var reports = await _repository.GetAllAsync();
            return reports.Select(ReportResponseDTO.FromReport);
        }

        public async Task<IEnumerable<ReportResponseDTO>> ListarDoUsuarioAsync(int usuarioId)
        {
            var reports = await _repository.GetByUsuarioAsync(usuarioId);
            return reports.Select(ReportResponseDTO.FromReport);
        }

        public async Task<Report?> BuscarPorId(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}