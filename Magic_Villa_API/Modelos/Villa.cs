using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_API.Modelos
{
    public class Villa
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Detalles { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
    }
}
