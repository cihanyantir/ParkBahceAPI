using Microsoft.AspNetCore.Mvc;
using ParkWeb.Models;
using ParkWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class MilletBahcesiController : Controller
    {
        private readonly IMilletBahcesiRepository _mbRepo;
        //MilletBahcesiRepository implemented.One more can be.We add it on Startup.cs
        //MilletBahcesiRepository:GenericRepository<MilletBahcesi>, IMilletBahcesiRepository
        //Aslında MilletBahcesiRepository = GenericRepository, so you can use _mbrepo.Create as general.

        public MilletBahcesiController(IMilletBahcesiRepository mbRepo)
        {
            _mbRepo = mbRepo;
        }

        public IActionResult Index()
        {
            return View(new MilletBahcesi() { });
        }
        public async Task<IActionResult> GetAllMilletBahcesi()
        {
            return Json(new { data = await _mbRepo.GetAllAsync(SD.MilletBahcesiAPIPath) });
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id) //one column
        {
            MilletBahcesi mlt = new MilletBahcesi();//id yoksa create, varsa update
            if(id==null)
            {
                return View(mlt);
            }
            mlt = await _mbRepo.GetAsync(SD.MilletBahcesiAPIPath, id.GetValueOrDefault());
            if(mlt==null)
            {
                return NotFound();
            }
            return View(mlt);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MilletBahcesi obj)
        { if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files; //for byte image
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }

                    }
                    obj.Picture = p1;
                }
                else
                {
                    if (obj.Id != 0)
                    {
                        var onjfromDb = await _mbRepo.GetAsync(SD.MilletBahcesiAPIPath, obj.Id);

                        obj.Picture = onjfromDb.Picture;
                    }
                    else
                    {
                        byte[] p1 = null; 
                        obj.Picture = p1; //bakılacak
                    }


                }
                if (obj.Id == 0)
                {
                    await _mbRepo.CreateAsync(SD.MilletBahcesiAPIPath, obj);
                }
                else
                {
                    await _mbRepo.UpdateAsync(SD.MilletBahcesiAPIPath + obj.Id, obj);
                }
                return RedirectToAction("Index"); 

            }
            else return View(obj);
                }
        
        [HttpDelete]
     
        public async Task<IActionResult> Delete(int id)
        {
          var success=await _mbRepo.DeleteAsync(SD.MilletBahcesiAPIPath, id);
            if(success)
            return Json(new { success = true, message = "Başarıyla Silindi" });
            return Json(new { success = false, message = "Silinemedi" });

        }
    }
}
