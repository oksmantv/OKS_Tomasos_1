using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OKS_Tomasos.Models;
using OKS_Tomasos.Services.RegisterService;
using OKS_Tomasos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OKS_Tomasos.Controllers
{
    public class RegisterController : Controller
    {
        private RegisterConnection _Connection;
        public RegisterController(RegisterConnection conn)
        {
            _Connection = conn;
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Kunder K)
          {
            var Validate = new RegisterValidation();
            if ((ModelState.IsValid || !ModelState.IsValid) && (!Validate.ValidateRegister(K, _Connection.GetAllKunder().ToList())))
            {
                ModelState.AddModelError("Kund.AnvandarNamn", "Användarnamn taget");
                return View(K);
            }
            if (ModelState.IsValid)
            {
                _Connection.AddRegistration(K);
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(K);
            }
        }
    }
}
