using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<MilletBahcesi> MilletBahcesiList { get; set; }
        public IEnumerable<SosyalTesis> SosyalTesisList { get; set; }
    }
}
