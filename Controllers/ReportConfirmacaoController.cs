using iNature.Models;
using iNature.Models.DTOs;
using iNature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iNature.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportConfirmacaoController : ControllerBase
    {
        private readonly ReportConfirmacaoService _reportConfirmacaoService;
        private readonly UsuarioService _usuarioService;

        public ReportConfirmacaoController(ReportConfirmacaoService service, UsuarioService usuarioService)
        {
            _reportConfirmacaoService = service;
            _usuarioService = usuarioService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ConfirmarConfirmacao(int id)
        {
            Usuario usuario = await _usuarioService.GetCurrentUserAsync();
            try
            {
                await _reportConfirmacaoService.ConfirmarAsync(id, usuario.Id);
                return Created(string.Empty, new { message = "Confirmação registrada." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverConfirmacao(int id)
        {
            Usuario usuario = await _usuarioService.GetCurrentUserAsync();
            try
            {
                await _reportConfirmacaoService.RemoverConfirmacaoAsync(id, usuario.Id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}