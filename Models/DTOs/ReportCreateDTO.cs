using System.ComponentModel.DataAnnotations;

namespace iNature.Models.DTOs
{
    public class ReportCreateDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public required string Titulo { get; set; }
        [Required(ErrorMessage = "O corpo do report é obrigatório.")]
        public required string Corpo { get; set; }
        private TipoReport _type { get; set; }
        [Required]
        public string Tipo {
            get => _type.ToString();
            set
            {
                if (Enum.TryParse<TipoReport>(value, true, out TipoReport result))
                {
                    _type = result;
                }
                else
                {
                    throw new ArgumentException($"Tipo do report inválido. Valores aceitos: {string.Join(", ", Enum.GetNames(typeof(TipoReport)))}");
                }
            }
        }
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public required string Cidade { get; set; }
        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public required string Bairro { get; set; }
        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        public required string Logradouro { get; set; }
        public int Numero { get; set; }
    }
}