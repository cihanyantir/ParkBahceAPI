using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Abstract
{
    public interface ISosyalTesisRepository
    {
        ICollection<SosyalTesis> GetSosyalTesiss();
        ICollection<SosyalTesis> GetSosyalTesisInMilletBahcesi(int mbID);
        SosyalTesis GetSosyalTesis(int SosyalTesisID);
        bool SosyalTesisExist(string name);
        bool SosyalTesisExist(int id);
        bool CreateSosyalTesis(SosyalTesis SosyalTesis);
        bool UpdateSosyalTesis(SosyalTesis SosyalTesis);
        bool DeleteSosyalTesis(SosyalTesis SosyalTesis);
        bool Save();

    }
}
