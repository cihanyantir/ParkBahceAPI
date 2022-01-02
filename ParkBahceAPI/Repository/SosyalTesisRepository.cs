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
    public class SosyalTesisRepository : ISosyalTesisRepository
    {
        private readonly ApplicationDbContext _db;
        //List<SosyalTesis> _millet = new List<SosyalTesis>();

        public SosyalTesisRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateSosyalTesis(SosyalTesis SosyalTesis)
        {
            _db.SosyalTesiss.Add(SosyalTesis);
            return Save();
        }

        public bool DeleteSosyalTesis(SosyalTesis SosyalTesis)
        {
            _db.SosyalTesiss.Remove(SosyalTesis);
            
            return Save();
        }
        

        //public List<SosyalTesis> Delete(int id)
        //{
        //    _millet.RemoveAll(x => x.Id == id);
        //    return _millet;
        //}


        public SosyalTesis GetSosyalTesis(int SosyalTesisID)
        {
            return _db.SosyalTesiss.Include(x => x.MilletBahcesi).FirstOrDefault(x=>x.Id== SosyalTesisID);
        }

        public ICollection<SosyalTesis> GetSosyalTesiss()
        {
            return _db.SosyalTesiss.Include(x => x.MilletBahcesi).OrderBy(x => x.Name).ToList();        }

        public bool SosyalTesisExist(string name)
        {
            bool value = _db.SosyalTesiss.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool SosyalTesisExist(int id)
        {
            return _db.SosyalTesiss.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateSosyalTesis(SosyalTesis SosyalTesis)
        {
            _db.SosyalTesiss.Update(SosyalTesis);

            return Save();
        }

        public ICollection<SosyalTesis> GetSosyalTesisInMilletBahcesi(int mbID)
        {
            return _db.SosyalTesiss.Include(x => x.MilletBahcesi).Where(x => x.MilletBahcesiId == mbID).ToList();
        }
    }
}
