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

            var Model = new Matratter();
            Model.MatratterList = _Connection.GetMatratter();
            Model.Produkt = _Connection.GetProdukter();
            Model.MatrattTyper = _Connection.GetMatrattTyper();
            Model.MatrattProdukter = _Connection.GetMatrattProdukter();
            Model.BestallningList = new List<Matratt>();

            //Hämtar värden i varukorgen som JSON 
            var valuesJSON = HttpContext.Session.GetString("Cart");

            if (valuesJSON != null)
                Model.BestallningList = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);

            return View(Model);
        }

        [HttpPost]
        public IActionResult Menu(List<BestallningMatratt> Matratter)
        {
            return View();
        }

        public IActionResult AddProduct(int id)
        {

            List<Matratt> shoppingCart;

            //Vald produkt från vyn
            var selectedProduct = _Connection.GetMatratter().SingleOrDefault(p => p.MatrattId == id);

            //Kontroll om det är första produkten som läggs i varukorgen
            
            
            if (HttpContext.Session.GetString("Cart") == null)
            {
                //Eftersom det inte finns värden sedan tidigare, skapa en ny lista
                shoppingCart = new List<Matratt>();
            }
            else
            {
                //Det finns värden i varukorgen. Hämta dessa värden som JSON 
                var valuesJSON = HttpContext.Session.GetString("Cart");

                //Gör om JSON strängen till en lista med produkter
                shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);
            }

            //Lägg till den nya produkten till listan 
            shoppingCart.Add(selectedProduct);

            //Gör om listan till JSON 
            var jsonList = JsonConvert.SerializeObject(shoppingCart);

            //Lägg in JSON strängen i sessionsvariabeln
            HttpContext.Session.SetString("Cart", jsonList);

            //Gå tillbaka till produktlistan
            return RedirectToAction("Menu");
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            //Hämtar värden i varukorgen som JSON 
            var valuesJSON = HttpContext.Session.GetString("Cart");
            var accountJSON = HttpContext.Session.GetString("UserAccount");

            //Gör om JSON-strängen till en lista med produkter
            List<Matratt> MatrattList = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);
            Kund kund = JsonConvert.DeserializeObject<Kund>(accountJSON);


            Bestallning B = new Bestallning();
            int TotalSum = 0;

            foreach (var V in MatrattList)
            {
                var BM = new BestallningMatratt();


                if (B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault() != null)
                {
                    B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault().Antal++;
                }
                else
                {       
                    BM.Bestallning = B;
                    BM.Matratt = V;
                    BM.MatrattId = V.MatrattId;
                    BM.Antal = 1;
                    B.BestallningMatratt.Add(BM);
                }
                
                TotalSum += V.Pris;
            }

            B.KundId = kund.KundId;
            B.Kund = kund;
            B.BestallningDatum = DateTime.Now;
            B.Levererad = false;
            B.Totalbelopp = TotalSum;

            return View(B);
        }

        [HttpGet]
        public IActionResult OrderDone()
        {
            //Hämtar värden i varukorgen som JSON 
            var valuesJSON = HttpContext.Session.GetString("Cart");
            var accountJSON = HttpContext.Session.GetString("UserAccount");

            //Gör om JSON-strängen till en lista med produkter
            List<Matratt> MatrattList = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);
            Kund kund = JsonConvert.DeserializeObject<Kund>(accountJSON);


            Bestallning B = new Bestallning();
            int TotalSum = 0;

            foreach (var V in MatrattList)
            {
                var BM = new BestallningMatratt();


                if (B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault() != null)
                {
                    B.BestallningMatratt.Where(x => x.MatrattId == V.MatrattId).FirstOrDefault().Antal++;
                }
                else
                {
                    BM.Bestallning = B;
                    BM.Matratt = V;
                    BM.MatrattId = V.MatrattId;
                    BM.Antal = 1;
                    B.BestallningMatratt.Add(BM);
                }

                TotalSum += V.Pris;
            }

            B.KundId = kund.KundId;
            B.Kund = kund;
            B.BestallningDatum = DateTime.Now;
            B.Levererad = false;
            B.Totalbelopp = TotalSum;

            _Connection.AddBestallning(B);
            HttpContext.Session.Remove("Cart");
            return View();
        }
    }
}
