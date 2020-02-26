using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.Services.RegisterService;
using OKS_Tomasos.ViewModels;

namespace OKS_Tomasos.Services.LoginService
{

    public class LoginValidation
    {

        private IRepository _Connection;
        public LoginValidation(IRepository repo)
        {
            //Injectar repot (uppsatt i startup- configureservices dvs vår DI Container)
            _Connection = repo;
        }

        public bool ValidateLogin(Kunder K,List<Kund> Kunder)
        {
            var Anvandarnamn = K.Kund.AnvandarNamn;
            var Losenord = K.Kund.Losenord;

            var AnvandarnamnCheck = Kunder.Where(k => k.AnvandarNamn == Anvandarnamn).SingleOrDefault();
            if(AnvandarnamnCheck == null)
            return false;

            return true;
        }

        public void CheckLogin(ISession Session, Kunder K)
        {
            var Kund = _Connection.GetAllKunder().SingleOrDefault(x => x.AnvandarNamn == K.Kund.AnvandarNamn);
            Session.SetString("UserLoggedIn", "1");

            var jsonList = JsonConvert.SerializeObject(Kund);

            //Lägg in JSON strängen i sessionsvariabeln
            Session.SetString("UserAccount", jsonList);
        }

        public bool ValidatePassword(Kunder K,List<Kund> Kunder)
        {
            var Anvandarnamn = K.Kund.AnvandarNamn;
            var Losenord = K.Kund.Losenord;
            var LosenordCheck = Kunder.Where(k => k.AnvandarNamn == Anvandarnamn).Where(p => p.Losenord == Losenord).SingleOrDefault();
            if (LosenordCheck == null)
                return false;

            return true;
        }

    }
}
