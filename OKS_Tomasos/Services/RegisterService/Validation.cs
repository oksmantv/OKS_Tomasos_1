using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OKS_Tomasos.Models;
using OKS_Tomasos.Services.RegisterService;
using OKS_Tomasos.ViewModels;

namespace OKS_Tomasos.Services.RegisterService
{
    public class Validation
    {
        public bool ValidateKund(Kunder K,List<Kund> Kunder)
        {
            //var Email = K.Kund.Email;
            var Anvandarnamn = K.Kund.AnvandarNamn;

            var AnvandarnamnCheck = Kunder.Where(k => k.AnvandarNamn == Anvandarnamn).SingleOrDefault();
            if(AnvandarnamnCheck == null)
            return true;

            //var EmailCheck = Kunder.Where(k => k.Email == Email).SingleOrDefault();
            //if (EmailCheck == null)
            //return true;

            return false;
        }

    }
}
