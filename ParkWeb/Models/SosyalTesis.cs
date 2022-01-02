using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Models
{
    public class SosyalTesis
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public enum DifficultType { Boş, Orta, Yüksek, Dolu }
        public DifficultType Doluluk { get; set; }
        [Required]
        public int MilletBahcesiId { get; set; }
        public MilletBahcesi MilletBahcesi { get; set; }
        
        public byte[] Picture { get; set; }
        public IEnumerable<SelectListItem> MilletBahcesiList { get; set; }
    }
}
