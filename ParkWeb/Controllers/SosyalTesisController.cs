using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkWeb.Models;
using ParkWeb.Models.ViewModel;
using ParkWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    [Authorize]
    public class SosyalTesisController : Controller
    {
       
            private readonly IMilletBahcesiRepository _mbRepo;
            private readonly ISosyalTesisRepository _stRepo;
            //MilletBahcesiRepository implemented.One more can be.We add it on Startup.cs
            //MilletBahcesiRepository:GenericRepository<MilletBahcesi>, IMilletBahcesiRepository
            //Aslında MilletBahcesiRepository = GenericRepository, so you can use _mbrepo.Create as general.

            public SosyalTesisController(IMilletBahcesiRepository mbRepo,ISosyalTesisRepository stRepo)
            {
                _mbRepo = mbRepo;
                _stRepo = stRepo;
            }

            public IActionResult Index()
            {
                return View(new SosyalTesis() { });
            }
            public async Task<IActionResult> GetAllSosyalTesis()
            {
                return Json(new { data = await _stRepo.GetAllAsync(SD.SosyalTesisAPIPath, HttpContext.Session.GetString("JWToken")) });
            }
            [HttpGet]
        
        public async Task<IActionResult> Upsert(int? id) //one column
            {   //All in mblist, so you can set text,value to MilletBahcesiList
                IEnumerable<MilletBahcesi> mblist = await _mbRepo.GetAllAsync(SD.MilletBahcesiAPIPath, HttpContext.Session.GetString("JWToken"));


            List<SelectListItem> categories = (from x in mblist
                                               select new SelectListItem
                                               { Text = x.Name, Value = x.Id.ToString() }).ToList();

            ViewBag.cv = categories;
            SosyalTesis objvs = new SosyalTesis();
            //{
            //    MilletBahcesiList = mblist.Select(x => new SelectListItem
            //    {
            //        Text = x.Name,
            //        Value = x.Id.ToString()
            //    })

            //};


            if (id == null)
            {
                return View(objvs);
            }
            //Can use sosyaltesis again by using SosyalTesis SosyalTesis {get;Set;}
            var objvm= await _stRepo.GetAsync(SD.SosyalTesisAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
                if (objvm == null)
                {
                    return NotFound();
                }
                return View(objvm);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Upsert(SosyalTesis obj)
            {
            
                if (ModelState.IsValid)
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
                    var onjfromDb = await _stRepo.GetAsync(SD.SosyalTesisAPIPath, obj.Id, HttpContext.Session.GetString("JWToken"));
                    if (obj.Id != 0)
                        obj.Picture = onjfromDb.Picture;


                }
                if (obj.Id == 0)
                    {
                        await _stRepo.CreateAsync(SD.SosyalTesisAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                    }
                    else
                    {
                        await _stRepo.UpdateAsync(SD.SosyalTesisAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                    } 
                return RedirectToAction("Index");

                }
            else
            {
                IEnumerable<MilletBahcesi> mblist = await _mbRepo.GetAllAsync(SD.MilletBahcesiAPIPath, HttpContext.Session.GetString("JWToken"));
                List<SelectListItem> categories = (from x in mblist
                                                   select new SelectListItem
                                                   { Text = x.Name, Value = x.Id.ToString() }).ToList();

                ViewBag.cv = categories;
                SosyalTesis objvs = new SosyalTesis();

                return View(objvs);
            }


        }
            [HttpDelete]
        
        public async Task<IActionResult> Delete(int id)
            {
                var success = await _mbRepo.DeleteAsync(SD.SosyalTesisAPIPath, id, HttpContext.Session.GetString("JWToken"));
                if (success)
                    return Json(new { success = true, message = "Başarıyla Silindi" });
                return Json(new { success = false, message = "Silinemedi" });

            }
        }
    }

