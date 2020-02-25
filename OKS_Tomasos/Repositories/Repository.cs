using OKS_Tomasos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OKS_Tomasos.ViewModels;

namespace OKS_Tomasos.Repositories
{
    public interface IRepository
    {
        void AddRegistration(Kunder K);
        List<Kund> GetAllKunder();
    }
    public class Repository : IRepository
    {

        // GET: /<controller>/
        private TomasosContext _Repository;
        public Repository(TomasosContext context)
        {
            _Repository = context;
        }

  
        public void AddRegistration(Kunder K)
        {

            _Repository.Kund.Add(K.Kund);
            _Repository.SaveChanges();
        }

        public List<Kund> GetAllKunder() 
        {
            return(_Repository.Kund.ToList());
        
        }

    }
}
