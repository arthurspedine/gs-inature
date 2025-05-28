using System.ComponentModel.DataAnnotations;

namespace iNature.Models.DTOs
{
    public record LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}