using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class ParagonForView
    {
        public int Par_IdParagonu { get; set; }
        public string Par_NrDok { get; set; }
        public string Par_Kontrahent { get; set; }
        public string Par_KontrahentOdbiorca { get; set; }
        public string Par_Kategoria { get; set; }
        public string Par_Magazyn { get; set; }
        public Nullable<System.DateTime> Par_DataWyst { get; set; }
        public Nullable<System.DateTime> Par_DataSprzedazy { get; set; }
        public Nullable<System.DateTime> Par_DataOd { get; set; }
        public Nullable<decimal> Par_Rabat { get; set; }
       
        public Nullable<System.DateTime> Par_Termin { get; set; }
        public Nullable<int> Par_IdPozycji { get; set; }
        public Nullable<int> Par_WprowId { get; set; }
        public Nullable<System.DateTime> Par_DataWprow { get; set; }
        public Nullable<int> Par_ZmienId { get; set; }
        public Nullable<System.DateTime> Par_DataZmian { get; set; }
        public Nullable<bool> Par_CzyAktywne { get; set; }
        public string Par_NettoBrutto { get; set; }
        public string Par_RodzajuDokumentu { get; set; }
        public string Par_RodzajuPlatnosci { get; set; }
    }
}
