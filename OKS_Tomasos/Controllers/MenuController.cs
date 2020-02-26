using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OKS_Tomasos.Controllers
{
    public class MenuController : Controller
    {
        private IRepository _Connection;
        public MenuController(IRepository repo)
        {
            //Injectar repot (uppsatt i startup- configureservices dvs vår DI Container)
            _Connection = repo;
        }

        [HttpGet]
        public IActionResult Menu()
        {
            //Kund K;

            //if (HttpContext.Session.GetString("UserAccount") != null)
            //{
            //    var valuesJSON = HttpContext.Session.GetString("UserAccount");
            //    K = JsonConvert.DeserializeObject<Kund>(valuesJSON);
            //}

            var Model = new Matratter();
            Model.MatratterList = _Connection.GetMatratter();
            Model.Produkt = _Connection.GetProdukter();
            Model.MatrattTyper = _Connection.GetMatrattTyper();
            Model.MatrattProdukter = _Connection.GetMatrattProdukter();

            return View(Model);
        }

        [HttpPost]
        public IActionResult Menu(List<BestallningMatratt> Matratter)
        {
            return View();
        }
    }
}
