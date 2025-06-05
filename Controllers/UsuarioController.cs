using iNature.Models.DTOs;
using iNature.Services;
using Microsoft.AspNetCore.Mvc;

namespace iNature.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // POST - login do usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                return Ok(await _usuarioService.LoginUsuario(dto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }
        
        // POST - registrar novo usuário
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarDTO dto)
        {
            try
            {
                await _usuarioService.RegistrarUsuario(dto);
                return Created(string.Empty, new { message = "Usuário registrado com sucesso." });
            } catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }
    }
}