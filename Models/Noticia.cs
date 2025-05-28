using System.ComponentModel.DataAnnotations;

namespace iNature.Models
{
    public class Noticia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        public DateTime DataPublicacao { get; set; }

        [Required]
        [StringLength(500)]
        public string Resumo { get; set; }

        [Required]
        public string Corpo { get; set; }

        public Usuario Usuario { get; set; }

        public Noticia()
        {
            DataPublicacao = DateTime.UtcNow;
        }
        
        public Noticia(int usuarioId, string titulo, string resumo, string corpo)
        {
            UsuarioId = usuarioId;
            Titulo = titulo;
            Resumo = resumo;
            Corpo = corpo;
            DataPublicacao = DateTime.UtcNow;
        }
    }
}