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
            //var valuesJSON = HttpContext.Session.GetString("Cart");
            //List<Matratt> MatrattList = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);

            //var accountJSON = HttpContext.Session.GetString("UserAccount");
            //Kund kund = JsonConvert.DeserializeObject<Kund>(accountJSON);

            //Bestallning B = new Bestallning();
            //int TotalSum = 0;

            //foreach (var V in MatrattList)
            //{
            //    var BM = new BestallningMatratt();


            //    if (B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault() != null)
            //    {
            //        B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault().Antal++;
            //    }
            //    else
            //    {       
            //        BM.Bestallning = B;
            //        BM.Matratt = V;
            //        BM.MatrattId = V.MatrattId;
            //        BM.Antal = 1;
            //        B.BestallningMatratt.Add(BM);
            //    }
                
            //    TotalSum += V.Pris;
            //}

            //B.KundId = kund.KundId;
            //B.Kund = kund;
            //B.BestallningDatum = DateTime.Now;
            //B.Levererad = false;
            //B.Totalbelopp = TotalSum;

            return View(_MenuConnection.CreateBestallning());
        }

        [HttpGet]
        public IActionResult OrderDone()
        {
            //var valuesJSON = HttpContext.Session.GetString("Cart");
            //var accountJSON = HttpContext.Session.GetString("UserAccount");

            //List<Matratt> MatrattList = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);
            //Kund kund = JsonConvert.DeserializeObject<Kund>(accountJSON);


            //Bestallning B = new Bestallning();
            //int TotalSum = 0;

            //foreach (var V in MatrattList)
            //{
            //    var BM = new BestallningMatratt();


            //    if (B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault() != null)
            //    {
            //        B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault().Antal++;
            //    }
            //    else
            //    {
            //        BM.Bestallning = B;
            //        BM.Matratt = V;
            //        BM.MatrattId = V.MatrattId;
            //        BM.Antal = 1;
            //        B.BestallningMatratt.Add(BM);
            //    }

            //    TotalSum += V.Pris;
            //}

            //B.KundId = kund.KundId;
            //B.Kund = kund;
            //B.BestallningDatum = DateTime.Now;
            //B.Levererad = false;
            //B.Totalbelopp = TotalSum;

           
            _Connection.AddBestallning(_MenuConnection.CreateBestallning());
            HttpContext.Session.Remove("Cart");
            return View();
        }
    }
}
