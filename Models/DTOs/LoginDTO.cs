using System.ComponentModel.DataAnnotations;

namespace iNature.Models.DTOs
{
    public record LoginDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public required string Senha { get; set; }
    }
}