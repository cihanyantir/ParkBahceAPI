using Microsoft.EntityFrameworkCore;
using ParkBahceAPI.Abstract;
using ParkBahceAPI.Data;
using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;
        //List<Trail> _millet = new List<Trail>();

        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTrail(Trail Trail)
        {
            _db.Trails.Add(Trail);
            return Save();
        }

        public bool DeleteTrail(Trail Trail)
        {
            _db.Trails.Remove(Trail);
            
            return Save();
        }
        

        //public List<Trail> Delete(int id)
        //{
        //    _millet.RemoveAll(x => x.Id == id);
        //    return _millet;
        //}


        public Trail GetTrail(int TrailID)
        {
            return _db.Trails.Include(x => x.MilletBahcesi).FirstOrDefault(x=>x.Id== TrailID);
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.Include(x => x.MilletBahcesi).OrderBy(x => x.Name).ToList();        }

        public bool TrailExist(string name)
        {
            bool value = _db.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExist(int id)
        {
            return _db.Trails.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail Trail)
        {
            _db.Trails.Update(Trail);

            return Save();
        }

        public ICollection<Trail> GetTrailInMilletBahcesi(int mbID)
        {
            return _db.Trails.Include(x => x.MilletBahcesi).Where(x => x.MilletBahcesiId == mbID).ToList();
        }
    }
}
