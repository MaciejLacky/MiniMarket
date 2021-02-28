using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class RezerwacjeForView
    {
        public int Rez_IdRezerwacji { get; set; }
        public string Rez_NrDok { get; set; }
        public string Rez_NazwaKontrahent { get; set; }
        public string Rez_KontrahentOdbiorca { get; set; }
        public string Rez_Kategoria { get; set; }
        public string Rez_Magazyn { get; set; }
        public Nullable<System.DateTime> Rez_DataWyst { get; set; }
        public Nullable<System.DateTime> Rez_DataSprzedazy { get; set; }
        public Nullable<System.DateTime> Rez_DataOd { get; set; }
        public Nullable<decimal> Rez_Rabat { get; set; }
        public string Rez_PlatnoscTyp { get; set; }
        public Nullable<System.DateTime> Rez_Termin { get; set; }
        public Nullable<int> Rez_IdPozycji { get; set; }
        public Nullable<int> Rez_WprowId { get; set; }
        public Nullable<System.DateTime> Rez_DataWprow { get; set; }
        public Nullable<int> Rez_ZmienId { get; set; }
        public Nullable<System.DateTime> Rez_DataZmian { get; set; }
        public Nullable<bool> Rez_CzyAktywne { get; set; }
        public string Rez_DokNettoBrutto { get; set; }
        public string Rez_RodzajDokumentu { get; set; }
        public string Rez_IdRodzjaPlatnosci { get; set; }
    }
}
