﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTOS
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
