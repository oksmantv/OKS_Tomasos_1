using Microsoft.AspNetCore.Http;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.Services.LoginService
{
    public class LoginConnection
    {
        private IRepository _Connection;
        public LoginConnection(IRepository repo)
        {
            //Injectar repot (uppsatt i startup- configureservices dvs vår DI Container)
            _Connection = repo;
        }

        public List<Kund> GetAllKunder()
        {

            return _Connection.GetAllKunder();
        }

    }
}
