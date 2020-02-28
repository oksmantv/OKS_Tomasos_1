using OKS_Tomasos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OKS_Tomasos.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace OKS_Tomasos.Repositories
{
    public interface IRepository
    {
        void AddRegistration(Kunder K);
        void AddBestallning(Bestallning B);
        void UpdateKund(Kunder K,string Json);
        List<Kund> GetAllKunder();
        List<Matratt> GetMatratter();
        List<Produkt> GetProdukter();
        Kund GetKund(int id);
        Matratt GetMatratt(int id);
        MatrattTyp GetMatrattTyp(int id);
        List<MatrattTyp> GetMatrattTyper();
        MatrattProdukt GetMatrattProdukt(int id);

        List<MatrattProdukt> GetMatrattProdukter();

    }
    public class Repository : IRepository
    {

        // GET: /<controller>/
        private TomasosContext _Repository;
        public Repository(TomasosContext context)
        {
            _Repository = context;
        }

        public void AddBestallning(Bestallning B)
        {
            B.Kund = null;

            foreach (var BM in B.BestallningMatratt)
            {
                BM.Matratt = null;
               _Repository.BestallningMatratt.Add(BM);
            }

            B.BestallningMatratt = null;


            _Repository.Bestallning.Add(B);
            _Repository.SaveChanges();
        }
        public void AddRegistration(Kunder K)
        {

            _Repository.Kund.Add(K.Kund);
            _Repository.SaveChanges();
        }

        public void UpdateKund(Kunder K, string JSon)
        {
            var Model = new Kunder();
            Model.Kund = JsonConvert.DeserializeObject<Kund>(JSon);

            var Original = _Repository.Kund.SingleOrDefault(O => O.KundId == Model.Kund.KundId);
            K.Kund.KundId = Original.KundId;

            if (Original != null)
            {
                _Repository.Entry(Original).CurrentValues.SetValues(K.Kund);
                _Repository.SaveChanges();
            }
            
        }

        public List<Kund> GetAllKunder() 
        {
               return(_Repository.Kund.ToList());
        
        }

        public List<Matratt> GetMatratter()
        {
            return (_Repository.Matratt.ToList());

        }

        public Matratt GetMatratt(int id)
        {
            return (_Repository.Matratt.Where(x => x.MatrattId == id).SingleOrDefault());
        }

        public List<Produkt> GetProdukter()
        {
            return (_Repository.Produkt.ToList());

        }
        
        public Kund GetKund(int id)
        {
            return (_Repository.Kund.Where(x => x.KundId == id).SingleOrDefault());

        }

        public MatrattProdukt GetMatrattProdukt(int id)
        {
            return (_Repository.MatrattProdukt.Where(x => x.MatrattId == id).SingleOrDefault());
        }

        public List<MatrattProdukt> GetMatrattProdukter()
        {
            return (_Repository.MatrattProdukt.ToList());

        }

        public MatrattTyp GetMatrattTyp(int id)
        {
            return (_Repository.MatrattTyp.Where(x => x.MatrattTyp1 == id).SingleOrDefault());
        }

        public List<MatrattTyp> GetMatrattTyper()
        {
            return (_Repository.MatrattTyp.ToList());

        }

    }
}
