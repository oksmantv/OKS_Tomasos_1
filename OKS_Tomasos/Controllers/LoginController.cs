using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OKS_Tomasos.Services.RegisterService;
using OKS_Tomasos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OKS_Tomasos.Controllers
{
    public class LoginController : Controller
    {
        private LoginConnection _Connection;
        public LoginController(LoginConnection conn)
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
            var Validate = new LoginValidation();
            var Kunder = _Connection.GetAllKunder();
            if (Validate.ValidateLogin(K, Kunder))

                return View(K);

            else return View(K);
        }
    }
}
