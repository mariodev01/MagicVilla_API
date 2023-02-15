using Magic_Villa_API.Modelos;

namespace Magic_Villa_API.Repository.IRepository
{
    public interface IVillaRepository: IRepository<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
