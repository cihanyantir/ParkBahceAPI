using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkBahceAPI.Abstract;
using ParkBahceAPI.Models;
using ParkBahceAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilletBahcesiController : Controller
    {
        private IMilletBahcesiRepository _mbRepo;
        private readonly IMapper _mapper;
        public MilletBahcesiController(IMilletBahcesiRepository mbrepo, IMapper mapper)
        {
            _mbRepo = mbrepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Millet Bahcesi Listesi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMilletBahcesi()
        {
            var objList = _mbRepo.GetMilletBahcesis();
            var objdto = new List<MilletBahcesiDTO>();
            foreach (var obj in objList)
            {
                objdto.Add(_mapper.Map<MilletBahcesiDTO>(obj));

            }
            return Ok(objdto);

        }
        /// <summary>
        /// ID'ye göre millet bahcesi
        /// </summary>
        /// <param name="milletbahcesiID">Seçilen ID</param>
        /// <returns></returns>
        [HttpGet("{milletbahcesiID:int}", Name = "GetMilletBahcesi")]
        public IActionResult GetMilletBahcesi(int milletbahcesiID)
        {
            var obj = _mbRepo.GetMilletBahcesi(milletbahcesiID);
            if (obj == null)
            {
                return NotFound();
            }
            var objdto = _mapper.Map<MilletBahcesiDTO>(obj); //no foreach needed just 1 column
            return Ok(objdto);
        }
        [HttpPost]
        public IActionResult CreateMilletBahcesi([FromBody] MilletBahcesiDTO milletBahcesiDTO)
        {
            if (milletBahcesiDTO == null)
            { return BadRequest(ModelState); }
            if (_mbRepo.MilletBahcesiExist(milletBahcesiDTO.Name))
            {
                ModelState.AddModelError("", "Millet Bahcesi Exist!");
                return StatusCode(404, ModelState);
            }


            var dtoobj = _mapper.Map<MilletBahcesi>(milletBahcesiDTO); //formdan geleni MilletBahcesine post
            if (!_mbRepo.CreateMilletBahcesi(dtoobj))
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini kaydederken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetMilletBahcesi", new { milletbahcesiID = milletBahcesiDTO.Id }, dtoobj);

        }
        [HttpPatch("{milletbahcesiID:int}", Name = "UpdateMilletBahcesi")]

        public IActionResult UpdateMilletBahcesi(int milletbahcesiID, [FromBody] MilletBahcesiDTO milletBahcesiDTO)
        {

            if (milletBahcesiDTO == null || milletbahcesiID != milletBahcesiDTO.Id)
            {
                return BadRequest(ModelState);
            }
            var dtoobj = _mapper.Map<MilletBahcesi>(milletBahcesiDTO); //formdan geleni MilletBahcesine post
            if (!_mbRepo.UpdateMilletBahcesi(dtoobj))
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini kaydederken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{milletbahcesiID:int}", Name = "DeleteMilletBahcesi")]
        public IActionResult DeleteMilletBahcesi(int milletbahcesiID)
        {
            if(!_mbRepo.MilletBahcesiExist(milletbahcesiID))
            {
                return NotFound();
            }
            var dtoobj = _mbRepo.GetMilletBahcesi(milletbahcesiID); 
            if (!_mbRepo.DeleteMilletBahcesi(dtoobj)) //getile cagrilan veriyi sil
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini silerken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return NoContent();
            

        }

    }
    
}
