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
using OKS_Tomasos.Services.UpdateService;
using OKS_Tomasos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OKS_Tomasos.Controllers
{
    public class LoginController : Controller
    {
        private IRepository _Connection;
        public LoginController(IRepository conn)
        {
            _Connection = conn;
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
            var Validate = new LoginValidation(_Connection);
            var Kunder = _Connection.GetAllKunder();

            if (Validate.ValidateLogin(K, Kunder) && Validate.ValidatePassword(K, Kunder))
            {
                Validate.CheckLogin(HttpContext.Session, K);
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
            var valuesJSON = HttpContext.Session.GetString("UserAccount");
            Model.Kund = JsonConvert.DeserializeObject<Kund>(valuesJSON);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Kunder K)
        {
            var UserID = HttpContext.Session.GetString("UserID");

            var Validate = new RegisterValidation();
            if (_Connection.GetAllKunder().Where(x => x.KundId == Convert.ToInt32(UserID)).SingleOrDefault().AnvandarNamn != K.Kund.AnvandarNamn)
            {
                if ((ModelState.IsValid || !ModelState.IsValid) && (!Validate.ValidateRegister(K, _Connection.GetAllKunder().ToList())))
                {
                    ModelState.AddModelError("Kund.AnvandarNamn", "Användarnamn taget");
                    return View(K);
                }
            }
            if (ModelState.IsValid)
            {
                var Json = HttpContext.Session.GetString("UserAccount");
                _Connection.UpdateKund(K,Json);
                var Validate2 = new LoginValidation(_Connection);
                Validate2.CheckLogin(HttpContext.Session, K);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(K);
            }

        }
    }
}
