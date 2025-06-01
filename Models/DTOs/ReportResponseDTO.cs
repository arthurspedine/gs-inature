namespace iNature.Models.DTOs
{
    public class ReportResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Corpo { get; set; }
        public TipoReport Tipo { get; set; }
        public DateTime Data { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string CriadoPor { get; set; }
        public int Confirmacoes { get; set; }

        public static ReportResponseDTO FromReport(Report report)
        {
            return new ReportResponseDTO
            {
                Id = report.Id,
                Titulo = report.Titulo,
                Corpo = report.Corpo,
                Tipo = report.Tipo,
                Data = report.Data,
                Cidade = report.Cidade,
                Bairro = report.Bairro,
                Logradouro = report.Logradouro,
                Numero = report.Numero,
                CriadoPor = report.Usuario?.Nome ?? "Desconhecido",
                Confirmacoes = report.Confirmacoes?.Count ?? 0
            };
        }
    }
}