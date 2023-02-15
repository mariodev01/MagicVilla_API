using Magic_Villa_API.Modelos;

namespace Magic_Villa_API.Repository.IRepository
{
    public interface INumeroVillaRepository : IRepository<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}