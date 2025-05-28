namespace iNature.Models.DTOs
{
    public record LoginResponseDTO
    {
        public string Token { get; init; }

        public LoginResponseDTO(string token)
        {
            Token = token;
        }
    }
}