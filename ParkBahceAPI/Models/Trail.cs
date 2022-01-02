using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultType { Easy,Moderate,Difficult,Expert}
        public DifficultType Difficulty { get; set; }
        public int MilletBahcesiId { get; set; } //means x=>x.MilletBahcesi.MilletBahcesiId
        [ForeignKey("MilletBahcesiId")] //ef 5.0 can do it easily
        public MilletBahcesi MilletBahcesi { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
