using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Abstract
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailInMilletBahcesi(int mbID);
        Trail GetTrail(int TrailID);
        bool TrailExist(string name);
        bool TrailExist(int id);
        bool CreateTrail(Trail Trail);
        bool UpdateTrail(Trail Trail);
        bool DeleteTrail(Trail Trail);
        bool Save();

    }
}
