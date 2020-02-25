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
    public class RegisterValidation
    {
        public bool ValidateRegister(Kunder K,List<Kund> Kunder)
        {
            var Anvandarnamn = K.Kund.AnvandarNamn;

            var AnvandarnamnCheck = Kunder.Where(k => k.AnvandarNamn == Anvandarnamn).SingleOrDefault();
            if(AnvandarnamnCheck == null)
            return true;

            return false;
        }

    }
}
