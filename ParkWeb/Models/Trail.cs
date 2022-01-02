using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultType { Easy, Moderate, Difficult, Expert }
        public DifficultType Difficulty { get; set; }
        public int MilletBahcesiId { get; set; }
        public MilletBahcesi MilletBahcesi { get; set; }
    }
}
