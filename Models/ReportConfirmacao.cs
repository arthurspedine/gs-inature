using System.ComponentModel.DataAnnotations;

namespace iNature.Models
{
    public class ReportConfirmacao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReportId { get; set; }
        public Report Report { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataConfirmacao { get; set; }

        public ReportConfirmacao() { }

    }
}