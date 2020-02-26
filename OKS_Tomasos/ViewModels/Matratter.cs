using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OKS_Tomasos.Models;

namespace OKS_Tomasos.ViewModels
{
    public class Matratter
    {
        public Matratter()
        {
            Kund = new Kund();
            Bestallning = new Bestallning();
            Matratt = new Matratt();
            MatratterList = new List<Matratt>();
            MatrattTyp = new MatrattTyp();
            MatrattTyper = new List<MatrattTyp>();
            MatrattProdukt = new MatrattProdukt();
            MatrattProdukter = new List<MatrattProdukt>();
            Produkt = new List<Produkt>();
        }
        public Kund Kund { get; set; }
        public Matratt Matratt { get; set; }
        public Bestallning Bestallning { get; set; }
        public MatrattTyp MatrattTyp { get; set; }
        public MatrattProdukt MatrattProdukt { get; set; }

        public List<MatrattProdukt> MatrattProdukter { get; set; }
        public List<Produkt> Produkt { get; set; }

        public List<MatrattTyp> MatrattTyper { get; set; }
        public List<Matratt> MatratterList { get; set; }

        public virtual MatrattTyp MatrattTypNavigation { get; set; }
    }
}
