using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class WZForView
    {
        public int Wz_IdWZ { get; set; }
        public string Wz_NrDok { get; set; }
        public string Wz_Kontrahent { get; set; }
        public string Wz_KontrahentOdbiorca { get; set; }
        public string Wz_Kategoria { get; set; }
        public string Wz_Magazyn { get; set; }
        public Nullable<System.DateTime> Wz_DataWyst { get; set; }
        public Nullable<System.DateTime> Wz_DataSprzedazy { get; set; }
        public Nullable<System.DateTime> Wz_DataOd { get; set; }
        public Nullable<decimal> Wz_Rabat { get; set; }
        public string Wz_PlatnoscTyp { get; set; }
        public Nullable<System.DateTime> Wz_Termin { get; set; }
        public Nullable<int> Wz_IdPozycji { get; set; }
        public Nullable<int> Wz_WprowId { get; set; }
        public Nullable<System.DateTime> Wz_DataWprow { get; set; }
        public Nullable<int> Wz_ZmienId { get; set; }
        public Nullable<System.DateTime> Wz_DataZmian { get; set; }
        public Nullable<bool> Wz_CzyAktywne { get; set; }
        public string Wz_DokNettoBrutto { get; set; }
        public string Wz_RodzajDokumentu { get; set; }
        public string Wz_RodzajPlatnosci { get; set; }
    }
}
