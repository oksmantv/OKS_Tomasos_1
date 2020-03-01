using Microsoft.AspNetCore.Http;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.Services.SessionService;
using OKS_Tomasos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.Services.MenuService
{
    public class MenuConnection
    {

        private IRepository _Connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private SessionData _Session;
        public MenuConnection(IRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _Connection = repo;
            _httpContextAccessor = httpContextAccessor;
            _Session = new SessionData(_httpContextAccessor);
        }

        public Matratter GetMatratter()
        {

            var Model = new Matratter();
            Model.MatratterList = _Connection.GetMatratter();
            Model.Produkt = _Connection.GetProdukter();
            Model.MatrattTyper = _Connection.GetMatrattTyper();
            Model.MatrattProdukter = _Connection.GetMatrattProdukter();
            Model.BestallningList = _Session.GetCart();

            return Model;

        }

        public Bestallning CreateBestallning()
        {
            Bestallning B = new Bestallning();
            int TotalSum = 0;

            foreach (var V in _Session.GetCart())
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

            var kund = _Session.GetSessionKund();
            B.KundId = kund.KundId;
            B.Kund = kund;
            B.BestallningDatum = DateTime.Now;
            B.Levererad = false;
            B.Totalbelopp = TotalSum;

            return B;
        }
    }
}
