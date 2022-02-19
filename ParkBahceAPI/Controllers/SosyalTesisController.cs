using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v{version:apiVersion}/SosyalTesis")]
    //[ApiExplorerSettings(GroupName = "ParkBahceAPISosyalTesis")]
    [ApiController]
    public class SosyalTesisController : ControllerBase
    {
        private ISosyalTesisRepository _SosyalTesisrepo;
        private readonly IMapper _mapper;
        public SosyalTesisController(ISosyalTesisRepository mbrepo, IMapper mapper)
        {
            _SosyalTesisrepo = mbrepo;
            _mapper = mapper;
        }

        /// <summary>
        /// SosyalTesis Listesi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public IActionResult GetSosyalTesis()
        {
            var objList = _SosyalTesisrepo.GetSosyalTesiss();
            var objdto = new List<SosyalTesisDTO>();
            foreach (var obj in objList)
            {
                objdto.Add(_mapper.Map<SosyalTesisDTO>(obj));

            }
            return Ok(objdto);

        }
        /// <summary>
        /// ID'ye göre SosyalTesis
        /// </summary>
        /// <param name="SosyalTesisID">Seçilen ID</param>
        /// <returns></returns>
        [HttpGet("{SosyalTesisID:int}", Name = "GetSosyalTesis")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetSosyalTesis(int SosyalTesisID)
        {
            var obj = _SosyalTesisrepo.GetSosyalTesis(SosyalTesisID);
            if (obj == null)
            {
                return NotFound();
            }
            var objdto = _mapper.Map<SosyalTesisDTO>(obj); //no foreach needed just 1 column
            return Ok(objdto);
        }


        [HttpGet("[action]/{milletbahcesiID:int}")]
        
        public IActionResult GetSosyalTesisInMilletBahcesi(int milletbahcesiID)
        {
            var obj = _SosyalTesisrepo.GetSosyalTesisInMilletBahcesi(milletbahcesiID);
            if (obj == null)
            {
                return NotFound();
            }
            var objdto = new List<SosyalTesisDTO>();
                   
            foreach (var item in obj)
            {
                objdto.Add( _mapper.Map<SosyalTesisDTO>(item));
            }
         
            return Ok(objdto);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateSosyalTesis([FromBody] SosyalTesisCreateDTO SosyalTesisDTO)
        {
            if (SosyalTesisDTO == null)
            { return BadRequest(ModelState); }
            if (_SosyalTesisrepo.SosyalTesisExist(SosyalTesisDTO.Name))
            {
                ModelState.AddModelError("", "Millet Bahcesi Exist!");
                return StatusCode(404, ModelState);
            }


            var dtoobj = _mapper.Map<SosyalTesis>(SosyalTesisDTO); //dtoobj mapped (SosyalTesisDTO=SosyalTesis)
            
            
            
            if (!_SosyalTesisrepo.CreateSosyalTesis(dtoobj))//formdan gelen (maplanan) post created
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini kaydederken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSosyalTesis", new { SosyalTesisID = dtoobj.Id }, dtoobj);

        }
        [HttpPatch("{SosyalTesisID:int}", Name = "UpdateSosyalTesis")]
        [Authorize(Roles = "Admin")]

        public IActionResult UpdateSosyalTesis(int SosyalTesisID, [FromBody] SosyalTesisUpdateDTO SosyalTesisDTO)
        {

            if (SosyalTesisDTO == null || SosyalTesisID != SosyalTesisDTO.Id)
            {
                return BadRequest(ModelState);
            }
            var dtoobj = _mapper.Map<SosyalTesis>(SosyalTesisDTO); //formdan geleni SosyalTesisne post
            if (!_SosyalTesisrepo.UpdateSosyalTesis(dtoobj))
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini kaydederken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{SosyalTesisID:int}", Name = "DeleteSosyalTesis")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSosyalTesis(int SosyalTesisID)
        {
            if(!_SosyalTesisrepo.SosyalTesisExist(SosyalTesisID))
            {
                return NotFound();
            }
            var dtoobj = _SosyalTesisrepo.GetSosyalTesis(SosyalTesisID); 
            if (!_SosyalTesisrepo.DeleteSosyalTesis(dtoobj)) //getile cagrilan veriyi sil
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini silerken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return NoContent();
            

        }

    }
    
}
