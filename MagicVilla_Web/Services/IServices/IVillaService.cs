using MagicVilla_Web.Models.DTOS;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> Obtener<T>(int id, string token);
        Task<T> Crear<T>(VillaCreacionDto dto, string token);
        Task<T> Actualizar<T>(VillaActualizacionDto dto, string token);
        Task<T> Remover<T>(int id, string token);

    }
}
