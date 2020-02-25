using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.Services.RegisterService
{
    public class DbConnection
    {

        private IRepository _Connection;
        public DbConnection(IRepository repo)
        {
            //Injectar repot (uppsatt i startup- configureservices dvs vår DI Container)
            _Connection = repo;
        }

        public List<Kund> GetAllKunder()
        {

            return _Connection.GetAllKunder();
        }

        public bool AddRegistration(Kunder K)
        {
            var Kunder = _Connection.GetAllKunder();
            var Validate = new Validation();
            if (Validate.ValidateKund(K, Kunder))
            {
                _Connection.AddRegistration(K);
                return true;
            }
            else
                return false;
            

        }

    }
}
