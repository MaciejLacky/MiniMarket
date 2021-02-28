using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class FakturaZakupuForView
    {
        public int Fvz_IdZakup { get; set; }
        public string Fvz_NrDok { get; set; }
        public string Fvz_Kontrahent { get; set; }
        public string Fvz_Kategoria { get; set; }
        public string Fvz_Magazyn { get; set; }
        public Nullable<System.DateTime> Fvz_DataWyst { get; set; }
        public Nullable<System.DateTime> FvzDataSprzedazy { get; set; }
        public Nullable<System.DateTime> FvzDataOd { get; set; }
        public Nullable<decimal> FvzRabat { get; set; }
        public string Fvz_PlatnoscTyp { get; set; }
        public Nullable<System.DateTime> Fvz_Termin { get; set; }
        public Nullable<int> Fvz_IdPozycji { get; set; }
        public Nullable<int> Fvz_WprowId { get; set; }
        public Nullable<System.DateTime> Fvz_DataWprow { get; set; }
        public Nullable<int> Fvz_ZmienId { get; set; }
        public Nullable<System.DateTime> FvzDataZmian { get; set; }
        public Nullable<bool> CzyAktywne { get; set; }
        public Nullable<int> Fvz_IdTowaru { get; set; }
        public Nullable<int> Fvz_IdKontrahentOdb { get; set; }
        public string Fvz_DokNettoBrutto { get; set; }
        public string Fvz_RodzajDokumentu { get; set; }
        public string Fvz_RodzajPlatnosci { get; set; }
        public string Fvz_TowarNazwa { get; set; }
        
    }
}
