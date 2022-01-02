using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkBahceAPI.Models.SosyalTesis;

namespace ParkBahceAPI.Models.DTOs
{
    public class SosyalTesisCreateDTO
    {
       
        [Required]
        public string Name { get; set; }


        public byte[] Picture { get; set; }
        public DifficultType Doluluk { get; set; }
        public int MilletBahcesiId { get; set; }
      
    }
}
