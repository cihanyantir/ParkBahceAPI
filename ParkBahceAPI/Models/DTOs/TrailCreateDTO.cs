using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkBahceAPI.Models.Trail;

namespace ParkBahceAPI.Models.DTOs
{
    public class TrailCreateDTO
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }

        public DifficultType Difficulty { get; set; }
        public int MilletBahcesiId { get; set; }
      
    }
}
