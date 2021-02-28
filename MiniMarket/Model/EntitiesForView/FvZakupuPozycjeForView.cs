using MiniMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class FvZakupuPozycjeForView 
    {
        public int Fvz_PozycjeId { get; set; }
        public string Fvz_PozycjeKod { get; set; }
        public string Fvz_PozycjeNazwa { get; set; }
        public Nullable<decimal> Fvz_PozycjeIlosc { get; set; }
        public string Fvz_PozycjeJm { get; set; }
        public Nullable<decimal> Fvz_Rabat { get; set; }
        public Nullable<decimal> Fvz_PozycjeCenaNetto { get; set; }
        public Nullable<decimal> Fvz_PozycjeVatZakup { get; set; }
        public Nullable<decimal> Fvz_PozycjeVatSprzedaz { get; set; }
        public Nullable<decimal> Fvz_PozycjeCenaBrutto { get; set; }
        public Nullable<decimal> Fvz_WartoscPozycjeCenaNetto { get; set; }
        public Nullable<decimal> Fvz_WartoscPozycjeCenaBrutto { get; set; }
        public Nullable<int> Fvz_PozycjeWprowId { get; set; }
        public Nullable<System.DateTime> Fvz_PozycjeDataWprow { get; set; }
        public Nullable<int> Fvz_PozycjeZmienId { get; set; }
        public Nullable<System.DateTime> Fvz_PozycjeDataZmien { get; set; }
        public Nullable<bool> Fvz_PozycjeCzyAktywne { get; set; }
        public Nullable<int> Fvz_PozycjeIdTowaru { get; set; }
        public Nullable<int> Fvz_PozycjeKategoriaId { get; set; }
        public Nullable<int> Fvz_IdFvZakup { get; set; }
    }
}
