using AutoMapper;
using ParkWeb.Models;
using ParkWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Mapper
{
    public class WebMappings:Profile
    {
        public WebMappings()
        {
            CreateMap<SosyalTesis, SosyalTesisVM>().ReverseMap();
        }
    }
}
