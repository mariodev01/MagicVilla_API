using MagicVilla_Web.Models.DTOS;

namespace MagicVilla_Web.Services
{
    public interface IUsuarioService
    {
        Task<T> Login<T>(LoginRequestDto dto);
        Task<T> Registrar<T>(RegistroRequestDto dto);

    }
}
