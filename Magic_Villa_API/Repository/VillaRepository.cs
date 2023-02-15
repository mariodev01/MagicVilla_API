using Magic_Villa_API.Datos;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Repository.IRepository;

namespace Magic_Villa_API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.fechaActualizacion = DateTime.Now;
            _db.villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
