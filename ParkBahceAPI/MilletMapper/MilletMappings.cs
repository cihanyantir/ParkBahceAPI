using AutoMapper;
using ParkBahceAPI.Models;
using ParkBahceAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.MilletMapper
{
    public class MilletMappings : Profile
    {
        public MilletMappings()
        {
            CreateMap<MilletBahcesi, MilletBahcesiDTO>().ReverseMap();
            CreateMap<Trail, TrailDTO>().ReverseMap();
            CreateMap<Trail, TrailCreateDTO>().ReverseMap();
            CreateMap<Trail, TrailUpdateDTO>().ReverseMap();
            CreateMap<SosyalTesis, SosyalTesisDTO>().ReverseMap();
            CreateMap<SosyalTesis, SosyalTesisUpdateDTO>().ReverseMap();
            CreateMap<SosyalTesis, SosyalTesisCreateDTO>().ReverseMap();


        }
       
    }
}
