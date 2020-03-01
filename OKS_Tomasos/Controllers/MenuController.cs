using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.Services.MenuService;
using OKS_Tomasos.Services.SessionService;
using OKS_Tomasos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OKS_Tomasos.Controllers
{
    public class MenuController : Controller
    {
        private IRepository _Connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private SessionData _Session;
        private MenuConnection _MenuConnection;
        public MenuController(IRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _Connection = repo;
            _httpContextAccessor = httpContextAccessor;
            _Session = new SessionData(_httpContextAccessor);
            _MenuConnection = new MenuConnection(_Connection, _httpContextAccessor);
        }


        [HttpGet]
        public IActionResult Menu()
        {
            return View(_MenuConnection.GetMatratter());
        }

        public IActionResult AddProduct(int id)
        {
            _Session.AddToCart(id,_Connection);
            return RedirectToAction("Menu");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(_MenuConnection.CreateBestallning());
        }

        [HttpGet]
        public IActionResult OrderDone()
        {
            _Connection.AddBestallning(_MenuConnection.CreateBestallning());
            HttpContext.Session.Remove("Cart");
            return View();
        }
    }
}
