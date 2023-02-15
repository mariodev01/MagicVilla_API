using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTOS
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Detalles { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}

/*los dtos son una forma de trabajar con los datos sin exponer directamente nuestas entidades*/
