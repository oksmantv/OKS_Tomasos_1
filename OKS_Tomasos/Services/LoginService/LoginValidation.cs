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
using OKS_Tomasos.Services.SessionService;
using OKS_Tomasos.ViewModels;

namespace OKS_Tomasos.Services.LoginService
{

    public class LoginValidation
    {

        private IRepository _Connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _Session;
        public LoginValidation(IRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            //Injectar repot (uppsatt i startup- configureservices dvs vår DI Container)
            _Connection = repo;
            _httpContextAccessor = httpContextAccessor;
            _Session = _httpContextAccessor.HttpContext.Session;
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

        public void CheckLogin(Kunder K)
        {

            var Session = new SessionData(_httpContextAccessor);
            Session.SetSessionLoggedIn();
            Session.SetSessionKund(_Connection.FindKund(K.Kund.AnvandarNamn));
            Session.SetSessionKundId(_Connection.FindKund(K.Kund.AnvandarNamn).KundId);

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
