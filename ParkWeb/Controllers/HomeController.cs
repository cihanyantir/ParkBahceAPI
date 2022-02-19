using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMilletBahcesiRepository _mbrepo;
        private readonly IAccountRepository _acrepo;
        private readonly ISosyalTesisRepository _strepo;

        public HomeController(ILogger<HomeController> logger, IMilletBahcesiRepository mbrepo, ISosyalTesisRepository strepo, IAccountRepository acrepo)
        {
            _logger = logger;
            _mbrepo = mbrepo;
            _strepo = strepo;
            _acrepo = acrepo;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM listofmilletandsosyaltesis = new IndexVM()
            {
                MilletBahcesiList = await _mbrepo.GetAllAsync(SD.MilletBahcesiAPIPath,HttpContext.Session.GetString("JWToken")),
                SosyalTesisList = await _strepo.GetAllAsync(SD.SosyalTesisAPIPath, HttpContext.Session.GetString("JWToken")),
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



        [HttpGet]
        public IActionResult Register()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await _acrepo.RegisterAsync(SD.AccountAPIPath + "register/", obj);
            if (result==false)       //api url=Path/authenticate/obj
            {
                return View();
            }
        
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");

            return RedirectToAction("Index");
        }






        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _acrepo.LoginAsync(SD.AccountAPIPath + "authenticate/", obj); 
            if(objUser.Token==null )       //api url=Path/authenticate/obj
            {
                return View();
            }

            ////////////////////////////COOKİE AUTHENTICATION////////////////////////////
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            ////Kimlik principale atandı, Cookie scheme ile auth sağlanıyor//////// (startup configure)


            HttpContext.Session.SetString("JWToken", objUser.Token); //Like cookie, Set it on Startup.cs configure
            /*var test = HttpContext.Session.GetString("JWToken");  test=objuser.Token           */                                                //Session holding Token in JWTOKEN
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
           
            return View();
        }



    }
}
