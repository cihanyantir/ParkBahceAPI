using ParkBahceAPI.Abstract;
using ParkBahceAPI.Data;
using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Repository
{
    public class MilletBahcesiRepository : IMilletBahcesiRepository
    {
        private readonly ApplicationDbContext _db;
        //List<MilletBahcesi> _millet = new List<MilletBahcesi>();

        public MilletBahcesiRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateMilletBahcesi(MilletBahcesi milletBahcesi)
        {
            _db.MilletBahcesis.Add(milletBahcesi);
            return Save();
        }

        public bool DeleteMilletBahcesi(MilletBahcesi milletBahcesi)
        {
            _db.MilletBahcesis.Remove(milletBahcesi);
            
            return Save();
        }
        

        //public List<MilletBahcesi> Delete(int id)
        //{
        //    _millet.RemoveAll(x => x.Id == id);
        //    return _millet;
        //}


        public MilletBahcesi GetMilletBahcesi(int milletbahcesiID)
        {
            return _db.MilletBahcesis.FirstOrDefault(x=>x.Id== milletbahcesiID);
        }

        public ICollection<MilletBahcesi> GetMilletBahcesis()
        {
            return _db.MilletBahcesis.OrderBy(x => x.Name).ToList();        }

        public bool MilletBahcesiExist(string name)
        {
            bool value = _db.MilletBahcesis.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool MilletBahcesiExist(int id)
        {
            return _db.MilletBahcesis.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMilletBahcesi(MilletBahcesi milletBahcesi)
        {
            _db.MilletBahcesis.Update(milletBahcesi);

            return Save();
        }
    }
}
