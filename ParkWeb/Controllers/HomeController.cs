using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkWeb.Models;
using ParkWeb.Models.ViewModel;
using ParkWeb.Repository;
using ParkWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMilletBahcesiRepository _mbrepo;
        private readonly ISosyalTesisRepository _strepo;

        public HomeController(ILogger<HomeController> logger, IMilletBahcesiRepository mbrepo, ISosyalTesisRepository strepo)
        {
            _logger = logger;
            _mbrepo = mbrepo;
            _strepo = strepo;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM listofmilletandsosyaltesis = new IndexVM()
            {
                MilletBahcesiList = await _mbrepo.GetAllAsync(SD.MilletBahcesiAPIPath),
                SosyalTesisList = await _strepo.GetAllAsync(SD.SosyalTesisAPIPath),
            };

            return View(listofmilletandsosyaltesis);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
