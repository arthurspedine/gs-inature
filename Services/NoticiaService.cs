using iNature.Models;
using iNature.Models.DTOs;
using iNature.Repositories;

namespace iNature.Services
{
    public class NoticiaService
    {
        private readonly NoticiaRepository _noticiaRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public NoticiaService(NoticiaRepository noticiaRepository, UsuarioRepository usuarioRepository)
        {
            _noticiaRepository = noticiaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<NoticiaResponseDTO>> GetAllNoticiasAsync()
        {
            var noticias = await _noticiaRepository.GetAllAsync();
            return noticias.Select(NoticiaResponseDTO.FromNoticia);
        }

        public async Task<NoticiaResponseDTO?> GetNoticiaByIdAsync(int id)
        {
            var noticia = await _noticiaRepository.GetByIdAsync(id);
            if (noticia == null) return null;

            return NoticiaResponseDTO.FromNoticia(noticia);
        }

        public async Task<IEnumerable<NoticiaResponseDTO>> GetNoticiasByUsuarioAsync(int usuarioId)
        {
            var noticias = await _noticiaRepository.GetByUsuarioIdAsync(usuarioId);
            return noticias.Select(NoticiaResponseDTO.FromNoticia);
        }

        public async Task<NoticiaResponseDTO?> CreateNoticiaAsync(NoticiaRequestDTO dto, int usuarioId)
        {  
            var noticia = new Noticia(usuarioId, dto.Titulo, dto.Resumo, dto.Corpo);
            var noticiaCreated = await _noticiaRepository.CreateAsync(noticia);

            return NoticiaResponseDTO.FromNoticia(noticiaCreated);
        }

        public async Task<NoticiaResponseDTO?> UpdateNoticiaAsync(int id, NoticiaRequestDTO dto, int usuarioId)
        {
            var noticia = await _noticiaRepository.GetByIdAsync(id);
            if (noticia == null || noticia.UsuarioId != usuarioId) return null;

            noticia.Titulo = dto.Titulo;
            noticia.Resumo = dto.Resumo;
            noticia.Corpo = dto.Corpo;

            var noticiaUpdated = await _noticiaRepository.UpdateAsync(noticia);
            if (noticiaUpdated == null) return null;

            return NoticiaResponseDTO.FromNoticia(noticiaUpdated);;
        }

        public async Task<bool> DeleteNoticiaAsync(int id, int usuarioId)
        {
            var noticia = await _noticiaRepository.GetByIdAsync(id);
            if (noticia == null || noticia.UsuarioId != usuarioId) return false;

            return await _noticiaRepository.DeleteAsync(id);
        }
    }
}