using OKS_Tomasos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.Services.UpdateService
{
    public class UpdateValidation
    {

        private IRepository _Connection;

        public UpdateValidation()
        {
        }

        public UpdateValidation(IRepository repo)
        {
            //Injectar repot (uppsatt i startup- configureservices dvs vår DI Container)
            _Connection = repo;
        }



    }
}
