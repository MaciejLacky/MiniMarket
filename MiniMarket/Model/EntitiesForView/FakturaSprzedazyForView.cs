using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class FakturaSprzedazyForView
    {
        public int Fvs_IdZakup { get; set; }
        public string Fvs_NrDok { get; set; }
        public string Fvs_Kontrahent { get; set; }
        public string Fvs_Kategoria { get; set; }
        public string Fvs_Magazyn { get; set; }
        public Nullable<System.DateTime> Fvs_DataWyst { get; set; }
        public Nullable<System.DateTime> FvsDataSprzedazy { get; set; }
        public Nullable<System.DateTime> FvsDataOd { get; set; }
        public Nullable<decimal> FvsRabat { get; set; }
        public string Fvs_PlatnoscTyp { get; set; }
        public Nullable<System.DateTime> Fvs_Termin { get; set; }
        public Nullable<int> Fvs_IdPozycji { get; set; }
        public Nullable<int> Fvs_WprowId { get; set; }
        public Nullable<System.DateTime> Fvs_DataWprow { get; set; }
        public Nullable<int> Fvs_ZmienId { get; set; }
        public Nullable<System.DateTime> FvsDataZmian { get; set; }
        public Nullable<bool> CzyAktywne { get; set; }
        public Nullable<int> Fvs_IdTowaru { get; set; }
        public Nullable<int> Fvs_IdKontrahentOdb { get; set; }
        public string Fvs_DokNettoBrutto { get; set; }
        public string Fvs_RodzajDokumentu { get; set; }
        public string Fvs_RodzajPlatnosci { get; set; }
        public string Fvs_TowarNazwa { get; set; }
    }
}
