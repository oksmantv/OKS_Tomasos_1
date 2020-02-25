﻿using System;
using System.Collections.Generic;

namespace OKS_Tomasos.Models
{
    public partial class Bestallning
    {
        public Bestallning()
        {
            BestallningMatratt = new HashSet<BestallningMatratt>();
        }

        public int BestallningId { get; set; }
        public DateTime BestallningDatum { get; set; }
        public int Totalbelopp { get; set; }
        public bool Levererad { get; set; }
        public int KundId { get; set; }

        public virtual Kund Kund { get; set; }
        public virtual ICollection<BestallningMatratt> BestallningMatratt { get; set; }
    }
}
