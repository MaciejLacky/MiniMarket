using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class PZForView
    {
        public int Pz_IdPZ { get; set; }
        public string Pz_NrDok { get; set; }
        public string Pz_Kontrahent { get; set; }
        public string Pz_KontrahentOdbiorca { get; set; }
        public string Pz_Kategoria { get; set; }
        public string Pz_Magazyn { get; set; }
        public Nullable<System.DateTime> Pz_DataWyst { get; set; }
        public Nullable<System.DateTime> Pz_DataSprzedazy { get; set; }
        public Nullable<System.DateTime> Pz_DataOd { get; set; }
        public Nullable<decimal> Pz_Rabat { get; set; }
        public string Pz_PlatnoscTyp { get; set; }
        public Nullable<System.DateTime> Pz_Termin { get; set; }
        public Nullable<int> Pz_IdPozycji { get; set; }
        public Nullable<int> Pz_WprowId { get; set; }
        public Nullable<System.DateTime> Pz_DataWprow { get; set; }
        public Nullable<int> Pz_ZmienId { get; set; }
        public Nullable<System.DateTime> Pz_DataZmian { get; set; }
        public Nullable<bool> Pz_CzyAktywne { get; set; }
        public string Pz_DokNettoBrutto { get; set; }
        public string Pz_RodzajDokumentu { get; set; }
        public string Pz_RodzajPlatnosci { get; set; }
    }
}
