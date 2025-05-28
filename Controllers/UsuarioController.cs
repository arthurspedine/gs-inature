using System.Security.Claims;
using iNature.Models.DTOs;
using iNature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iNature.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            return Ok(await _usuarioService.LoginUsuario(dto));
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarDTO dto)
        {
            await _usuarioService.RegistrarUsuario(dto);
            return Created(string.Empty, new { message = "Usuário registrado com sucesso." });
        }
    }
}