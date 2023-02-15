using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_Web.Models.DTOS
{
    public class NumeroVillaDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}
