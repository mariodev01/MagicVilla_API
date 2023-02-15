using Magic_Villa_API.Datos;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Repository.IRepository;

namespace Magic_Villa_API.Repository
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.numeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
