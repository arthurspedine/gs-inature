using System.ComponentModel.DataAnnotations;

namespace iNature.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Corpo { get; set; }
        [Required]
        public TipoReport Tipo { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ReportConfirmacao> Confirmacoes { get; set; } = [];

        public Report() { }

        public Report(string titulo, string corpo, TipoReport tipo, string cidade, string bairro, string logradouro, int numero, int usuarioId)
        {
            Titulo = titulo;
            Corpo = corpo;
            Tipo = tipo;
            Data = DateTime.Now;
            Cidade = cidade;
            Bairro = bairro;
            Logradouro = logradouro;
            Numero = numero;
            UsuarioId = usuarioId;
        }
    }
}