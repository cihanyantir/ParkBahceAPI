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
    [Route("api/v{version:apiVersion}/milletbahcesi")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkBahceAPIMilletBahcesi")]
    public class MilletBahcesiV2Controller : ControllerBase
    {
        private IMilletBahcesiRepository _mbRepo;
        private readonly IMapper _mapper;
        public MilletBahcesiV2Controller(IMilletBahcesiRepository mbrepo, IMapper mapper)
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
            var obj = _mbRepo.GetMilletBahcesis().FirstOrDefault();

       
            return Ok(_mapper.Map<MilletBahcesiDTO>(obj)); //bana maplanana eklenmiş veriyi getir.

        }
    }
    
}
