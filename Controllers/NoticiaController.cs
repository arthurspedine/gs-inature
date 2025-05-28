using iNature.Models;
using iNature.Models.DTOs;
using iNature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iNature.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // precisa ser autenticado para acessar os endpoints
    [Produces("application/json")]
    public class NoticiaController : ControllerBase
    {
        private readonly NoticiaService _noticiaService;
        private readonly UsuarioService _usuarioService;

        public NoticiaController(NoticiaService noticiaService, UsuarioService usuarioService)
        {
            _noticiaService = noticiaService;
            _usuarioService = usuarioService;
        }

        // GET - Listar todas as notícias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoticiaResponseDTO>>> GetAll()
        {
            try
            {
                var noticias = await _noticiaService.GetAllNoticiasAsync();
                return Ok(noticias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        // GET - busca notícia por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<NoticiaResponseDTO>> GetById(int id)
        {
            try
            {
                var noticia = await _noticiaService.GetNoticiaByIdAsync(id);
                if (noticia == null)
                    return NotFound(new { message = "Notícia não encontrada" });

                return Ok(noticia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        // GET - LIsta noticias do usuário autenticado
        [HttpGet("minhas")]
        [Authorize(Roles = "JORNALISTA")] // Apenas "JORNALISTA"
        public async Task<ActionResult<IEnumerable<NoticiaResponseDTO>>> GetMinhasNoticias()
        {
            try
            {
                Usuario usuario = await _usuarioService.GetCurrentUserAsync();
                var noticias = await _noticiaService.GetNoticiasByUsuarioAsync(usuario.Id);
                return Ok(noticias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        // POST - criar notícia
        [HttpPost]
        [Authorize(Roles = "JORNALISTA")] // Apenas "JORNALISTA"
        public async Task<ActionResult<NoticiaResponseDTO>> Create(NoticiaRequestDTO dto)
        {
            try
            {
                Usuario usuario = await _usuarioService.GetCurrentUserAsync();
                var noticia = await _noticiaService.CreateNoticiaAsync(dto, usuario.Id);

                if (noticia == null)
                    return BadRequest(new { message = "Erro ao criar notícia" });

                return CreatedAtAction(nameof(GetById), new { id = noticia.Id }, noticia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        // PUT - atualizar notícia
        [HttpPut("{id}")]
        [Authorize(Roles = "JORNALISTA")] // Apenas "JORNALISTA"
        public async Task<ActionResult<NoticiaResponseDTO>> Update(int id, NoticiaRequestDTO dto)
        {
            try
            {
                Usuario usuario = await _usuarioService.GetCurrentUserAsync();
                var noticia = await _noticiaService.UpdateNoticiaAsync(id, dto, usuario.Id);

                if (noticia == null)
                    return NotFound(new { message = "Notícia não encontrada ou você não tem permissão para editá-la" });

                return Ok(noticia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        // DELETE - excluir notícia
        [HttpDelete("{id}")]
        [Authorize(Roles = "JORNALISTA")] // Apenas "JORNALISTA"
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Usuario usuario = await _usuarioService.GetCurrentUserAsync();
                var deleted = await _noticiaService.DeleteNoticiaAsync(id, usuario.Id);

                if (!deleted)
                    return NotFound(new { message = "Notícia não encontrada ou você não tem permissão para excluí-la" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }
    }
}
