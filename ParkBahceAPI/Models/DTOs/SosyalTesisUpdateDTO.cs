using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkBahceAPI.Models.SosyalTesis;

namespace ParkBahceAPI.Models.DTOs
{
    public class SosyalTesisUpdateDTO
    {
     
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public byte[] Picture { get; set; }
        public DifficultType Difficulty { get; set; }
        public int MilletBahcesiId { get; set; }
    }
}
