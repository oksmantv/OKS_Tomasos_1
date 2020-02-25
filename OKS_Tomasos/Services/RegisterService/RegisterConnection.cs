using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.Services.RegisterService
{
    public class RegisterConnection
    {

        private IRepository _Connection;
        public RegisterConnection(IRepository repo)
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
            var Validate = new RegisterValidation();
            if (Validate.ValidateRegister(K, Kunder))
            {
                _Connection.AddRegistration(K);
                return true;
            }
            else
                return false;
            

        }

    }
}
