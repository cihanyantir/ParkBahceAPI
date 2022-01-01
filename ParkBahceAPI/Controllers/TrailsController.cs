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
    [Route("api/v{version:apiVersion}/trails")]
    //[ApiExplorerSettings(GroupName = "ParkBahceAPITrail")]
    [ApiController]
    public class TrailsController : Controller
    {
        private ITrailRepository _trailrepo;
        private readonly IMapper _mapper;
        public TrailsController(ITrailRepository mbrepo, IMapper mapper)
        {
            _trailrepo = mbrepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Trail Listesi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTrails()
        {
            var objList = _trailrepo.GetTrails();
            var objdto = new List<TrailDTO>();
            foreach (var obj in objList)
            {
                objdto.Add(_mapper.Map<TrailDTO>(obj));

            }
            return Ok(objdto);

        }
        /// <summary>
        /// ID'ye göre Trail
        /// </summary>
        /// <param name="TrailID">Seçilen ID</param>
        /// <returns></returns>
        [HttpGet("{TrailID:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int TrailID)
        {
            var obj = _trailrepo.GetTrail(TrailID);
            if (obj == null)
            {
                return NotFound();
            }
            var objdto = _mapper.Map<TrailDTO>(obj); //no foreach needed just 1 column
            return Ok(objdto);
        }


        [HttpGet("[action]/{milletbahcesiID:int}")]
        public IActionResult GetTrailInMilletBahcesi(int milletbahcesiID)
        {
            var obj = _trailrepo.GetTrailInMilletBahcesi(milletbahcesiID);
            if (obj == null)
            {
                return NotFound();
            }
            var objdto = new List<TrailDTO>();
                   
            foreach (var item in obj)
            {
                objdto.Add( _mapper.Map<TrailDTO>(item));
            }
         
            return Ok(objdto);
        }



        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailCreateDTO trailDTO)
        {
            if (trailDTO == null)
            { return BadRequest(ModelState); }
            if (_trailrepo.TrailExist(trailDTO.Name))
            {
                ModelState.AddModelError("", "Millet Bahcesi Exist!");
                return StatusCode(404, ModelState);
            }


            var dtoobj = _mapper.Map<Trail>(trailDTO); //dtoobj mapped (trailDTO=Trail)
            
            
            
            if (!_trailrepo.CreateTrail(dtoobj))//formdan gelen (maplanan) post created
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini kaydederken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { TrailID = dtoobj.Id }, dtoobj);

        }
        [HttpPatch("{TrailID:int}", Name = "UpdateTrail")]

        public IActionResult UpdateTrail(int TrailID, [FromBody] TrailUpdateDTO TrailDTO)
        {

            if (TrailDTO == null || TrailID != TrailDTO.Id)
            {
                return BadRequest(ModelState);
            }
            var dtoobj = _mapper.Map<Trail>(TrailDTO); //formdan geleni Trailne post
            if (!_trailrepo.UpdateTrail(dtoobj))
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini kaydederken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{TrailID:int}", Name = "DeleteTrail")]
        public IActionResult DeleteTrail(int TrailID)
        {
            if(!_trailrepo.TrailExist(TrailID))
            {
                return NotFound();
            }
            var dtoobj = _trailrepo.GetTrail(TrailID); 
            if (!_trailrepo.DeleteTrail(dtoobj)) //getile cagrilan veriyi sil
            {
                ModelState.AddModelError("", $"  {dtoobj.Name} objesini silerken hata oluştu");
                return StatusCode(500, ModelState);
            }
            return NoContent();
            

        }

    }
    
}
