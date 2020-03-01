using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.Services.LoginService;
using OKS_Tomasos.Services.RegisterService;
using OKS_Tomasos.Services.SessionService;
using OKS_Tomasos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OKS_Tomasos.Controllers
{
    public class LoginController : Controller
    {
        private IRepository _Connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(IRepository conn, IHttpContextAccessor httpContextAccessor)
        {
            _Connection = conn;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Kunder K) 
        {
            var Validate = new LoginValidation(_Connection,_httpContextAccessor);
            var Kunder = _Connection.GetAllKunder();

            if (Validate.ValidateLogin(K, Kunder) && Validate.ValidatePassword(K, Kunder))
            {
                Validate.CheckLogin(K);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                if (!Validate.ValidateLogin(K, Kunder))
                    ModelState.AddModelError("Kund.Anvandarnamn", "Fel Användarnamn");

                if(!Validate.ValidatePassword(K,Kunder))
                    ModelState.AddModelError("Kund.Losenord", "Fel Lösenord");

                return View(K);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Update()
        {
            var Model = new Kunder();
            var Session = new SessionData(_httpContextAccessor);
            Model.Kund = Session.GetSessionKund();
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Kunder K)
        {
            var Session = new SessionData(_httpContextAccessor);
            var UserID = Session.GetSessionKundId();

            var Validate = new RegisterValidation();

            if (_Connection.GetKund(UserID).AnvandarNamn != K.Kund.AnvandarNamn)
            {
                if (!Validate.ValidateRegister(K, _Connection.GetAllKunder().ToList()))
                {
                    ModelState.AddModelError("Kund.AnvandarNamn", "Användarnamn taget");
                    return View(K);
                }
            }
            if (ModelState.IsValid)
            {        
                _Connection.UpdateKund(K,Session.GetSessionKund());

                var LoginValidation = new LoginValidation(_Connection,_httpContextAccessor);
                LoginValidation.CheckLogin(K);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(K);
            }

        }
    }
}
