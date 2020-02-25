using OKS_Tomasos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.ViewModels
{
    public class Kunder
    {

        public Kunder()
        {
            Kund = new Kund();
            Kunderna = new List<Kund>();
        }
        public Kund Kund { get; set; }
        public List<Kund> Kunderna { get; set; }
    }
}
