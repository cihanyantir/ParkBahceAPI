using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Abstract
{
    public interface IMilletBahcesiRepository
    {
        ICollection<MilletBahcesi> GetMilletBahcesis();
        MilletBahcesi GetMilletBahcesi(int milletbahcesiID);
        bool MilletBahcesiExist(string name);
        bool MilletBahcesiExist(int id);
        bool CreateMilletBahcesi(MilletBahcesi milletBahcesi);
        bool UpdateMilletBahcesi(MilletBahcesi milletBahcesi);
        bool DeleteMilletBahcesi(MilletBahcesi milletBahcesi);
        bool Save();

    }
}
