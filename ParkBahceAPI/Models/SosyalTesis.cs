using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Models
{
    public class SosyalTesis
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public enum DifficultType { Boş, Orta, Yüksek, Dolu }
        public DifficultType Doluluk { get; set; }
        public int MilletBahcesiId { get; set; } 
        [ForeignKey("MilletBahcesiId")] 
        public MilletBahcesi MilletBahcesi { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Picture { get; set; }
    }
}
