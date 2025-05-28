using System.Security.Claims;
using iNature.Models;
using iNature.Models.DTOs;
using iNature.Repositories;

namespace iNature.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenService _tokenService;

        public UsuarioService(UsuarioRepository usuarioRepository, TokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDTO> LoginUsuario(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
            {
                throw new UnauthorizedAccessException("Senha incorreta.");
            }

            string token = _tokenService.GenerateJwtToken(usuario.Id, usuario.Role.ToString());

            return new LoginResponseDTO(
                token
            );
        }

        public async Task RegistrarUsuario(RegistrarDTO dto)
        {
            var existingUser = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Usuário já cadastrado.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            var cargoUsuario = Enum.Parse<UsuarioRole>(dto.Cargo, ignoreCase: true);
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = hashedPassword,
                Role = cargoUsuario
            };

            await _usuarioRepository.SalvarUsuarioAsync(usuario);
        }

        public async Task<Usuario> GetCurrentUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
            {
                throw new UnauthorizedAccessException("Token inválido ou usuário não autenticado");
            }
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("Token inválido ou usuário não autenticado");
                if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Token inválido");
            }

            // busca usuario por ID ou lança exceção se não encontrado
            var usuario = await _usuarioRepository.GetByIdAsync(userId) ?? throw new UnauthorizedAccessException("Usuário não encontrado");
                return usuario;
        }
    }
}