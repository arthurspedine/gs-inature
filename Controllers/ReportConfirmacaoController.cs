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

        [HttpPost]
        public async Task<IActionResult> ConfirmarConfirmacao(ReportConfirmacaoDTO dto)
        {
            Usuario usuario = await _usuarioService.GetCurrentUserAsync();
            try
            {
                await _reportConfirmacaoService.ConfirmarAsync(dto.ReportId, usuario.Id);
                return Created(string.Empty, new { message = "Confirmação registrada." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverConfirmacao(ReportConfirmacaoDTO dto)
        {
            Usuario usuario = await _usuarioService.GetCurrentUserAsync();
            try
            {
                await _reportConfirmacaoService.RemoverConfirmacaoAsync(dto.ReportId, usuario.Id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}