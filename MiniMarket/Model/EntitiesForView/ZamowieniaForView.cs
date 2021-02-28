using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class ZamowieniaForView
    {
        public int Zam_IdZamowienia { get; set; }
       
        public string Zam_NrDok { get; set; }
        public string Zam_Kontrahent { get; set; }
        public string Zam_KontrahentOdbiorca { get; set; }
        public string Zam_Kategoria { get; set; }
        public string Zam_Magazyn { get; set; }
        public Nullable<System.DateTime> Zam_DataWyst { get; set; }
        public Nullable<System.DateTime> Zam_DataSprzedazy { get; set; }
        public Nullable<System.DateTime> Zam_DataOd { get; set; }
        public Nullable<decimal> Zam_Rabat { get; set; }
        public string Zam_PlatnoscTyp { get; set; }
        public Nullable<System.DateTime> Zam_Termin { get; set; }
        public Nullable<int> Zam_IdPozycji { get; set; }
        public Nullable<int> Zam_WprowId { get; set; }
        public Nullable<System.DateTime> Zam_DataWprow { get; set; }
        public Nullable<int> Zam_ZmienId { get; set; }
        public Nullable<System.DateTime> Zam_DataZmian { get; set; }
        public Nullable<bool> Zam_CzyAktywne { get; set; }
        public string Fvs_DokNettoBrutto { get; set; }
        public string Fvs_RodzajDokumentu { get; set; }
        public string Fvs_RodzajPlatnosci { get; set; }
        public string Fvs_TowarNazwa { get; set; }
    }
}
