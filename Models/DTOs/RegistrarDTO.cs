using System.ComponentModel.DataAnnotations;

namespace iNature.Models.DTOs
{
    public record RegistrarDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Nome { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public required string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
        public required string Senha { get; set; }

        private UsuarioRole _role;
        [Required]
        public string Cargo {
            get => _role.ToString();
            set
            {
                if (Enum.TryParse<UsuarioRole>(value, true, out UsuarioRole result))
                {
                    _role = result;
                }
                else
                {
                    throw new ArgumentException($"Cargo do usuário inválido. Valores aceitos: {string.Join(", ", Enum.GetNames(typeof(UsuarioRole)))}");
                }
            }
        }
    }
}