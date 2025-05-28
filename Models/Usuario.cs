using System.ComponentModel.DataAnnotations;

namespace iNature.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string Senha { get; set; }

        [Required]
        public UsuarioRole Role { get; set; }

        public Usuario() { }
        
        public Usuario(string nome, string email, string senha, UsuarioRole role)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }
    }
}