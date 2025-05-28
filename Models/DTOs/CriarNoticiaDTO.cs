using System.ComponentModel.DataAnnotations;

namespace iNature.Models.DTOs
{
    public record CriarNoticiaDTO
    {
        [Required]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
        public required string Titulo { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "O resumo deve ter no máximo 500 caracteres.")]
        public required string Resumo { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "O corpo da notícia deve ter pelo menos 10 caracteres.")]
        public required string Corpo { get; set; }
    }
}