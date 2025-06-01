using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;
        private readonly UsuarioService _usuarioService;

        public ReportController(ReportService service, UsuarioService usuarioService)
        {
            _reportService = service;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ReportCreateDTO dto)
        {
            Usuario usuario = await _usuarioService.GetCurrentUserAsync();
            var report = await _reportService.CriarReportAsync(dto, usuario.Id);
            return CreatedAtAction(nameof(Criar), new { id = report.Id }, report);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReportResponseDTO>>> ListarTodos()
        {
            var reports = await _reportService.ListarTodosAsync();
            return Ok(reports);
        }

        [HttpGet("meus")]
        public async Task<ActionResult<List<ReportResponseDTO>>> ListarMeus()
        {
            Usuario usuario = await _usuarioService.GetCurrentUserAsync();
            var reports = await _reportService.ListarDoUsuarioAsync(usuario.Id);
            return Ok(reports);
        }
    }
}