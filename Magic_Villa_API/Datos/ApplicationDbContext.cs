using Magic_Villa_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa_API.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Villa> villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(new Villa
            {
                Id= 1,
                Name = "villa la parra",
                Detalles = "la verdadera villa",
                ImagenUrl = "",
                Ocupantes = 4,
                MetrosCuadrados = 400,
                Tarifa = 1400,
                Amenidad = "",
                fechaCreacion = DateTime.Now,
                fechaActualizacion = DateTime.Now
            });

        }

    }
}
