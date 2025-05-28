namespace iNature.Models.DTOs
{
    public record NoticiaResponseDTO
    {
        public int Id { get; init; }
        public string Titulo { get; init; }
        public DateTime DataPublicacao { get; init; }
        public string Resumo { get; init; }
        public string Corpo { get; init; }
        public string NomeAutor { get; init; }

        public NoticiaResponseDTO(int id, string titulo, DateTime dataPublicacao, string resumo, string corpo, string nomeAutor)
        {
            Id = id;
            Titulo = titulo;
            DataPublicacao = dataPublicacao;
            Resumo = resumo;
            Corpo = corpo;
            NomeAutor = nomeAutor;
        }
    }
}