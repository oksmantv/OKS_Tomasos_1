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
            MatratterList = new List<Matratt>();
            MatrattTyper = new List<MatrattTyp>();
            MatrattProdukter = new List<MatrattProdukt>();
            Produkt = new List<Produkt>();
            BestallningList = new List<Matratt>();
            BestallningMatrattList = new List<BestallningMatratt>();
        }

        public List<MatrattProdukt> MatrattProdukter { get; set; }
        public List<Produkt> Produkt { get; set; }

        public List<Matratt> BestallningList { get; set; }

        public List<MatrattTyp> MatrattTyper { get; set; }
        public List<Matratt> MatratterList { get; set; }

        public List<BestallningMatratt> BestallningMatrattList { get; set; }

    }
}
