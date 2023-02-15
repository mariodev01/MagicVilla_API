using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_API.Modelos.DTOS
{
    public class NumeroVillaUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}
