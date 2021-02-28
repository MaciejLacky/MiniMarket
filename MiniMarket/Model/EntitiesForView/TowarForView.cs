using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class TowarForView
    {
        public int Twr_IdTowaru { get; set; }
        public string Twr_Kod { get; set; }
        public string Twr_NumerKat { get; set; }
        public string Twr_Grupa { get; set; }
        public string Twr_Typ { get; set; }
        public Nullable<bool> Twr_Opak_Kaucja { get; set; }
        public string Twr_EAN { get; set; }
        public string Twr_PKWiU { get; set; }
        public decimal? Twr_VatSpr { get; set; }
        public decimal? Twr_VatZak { get; set; }
        public Nullable<decimal> Twr_CenaDomysl { get; set; }
        public string Twr_Nazwa { get; set; }
        public string Twr_KatSprzedazy { get; set; }
        public string Twr_KatZakupu { get; set; }
        public string Twr_JednPodst { get; set; }
        public string Twr_JednPomoc { get; set; }
        public Nullable<decimal> Twr_CenaZakNetto { get; set; }
        public Nullable<decimal> Twr_CenaZakBrutto { get; set; }
        public Nullable<decimal> Twr_CenaSprNetto { get; set; }
        public Nullable<decimal> Twr_CenaSprBrutto { get; set; }
        public Nullable<decimal> Twr_Marza { get; set; }
        public string Twr_Waluta { get; set; }
        public string Twr_WprUzytk { get; set; }
        public Nullable<System.DateTime> Twr_WprUzytData { get; set; }
        public string Twr_ZmienUzyt { get; set; }
        public Nullable<System.DateTime> Twr_ZmiUzytData { get; set; }
        public Nullable<bool> Twr_CzyAktywne { get; set; }
  
        public Nullable<int> Twr_CenaDomyslna { get; set; }

    }
}
