using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.EntitiesForView
{
    public class TowarSelected
    {
        public int Twr_IdTowaru { get; set; }
        public string Twr_Kod { get; set; }
        public string Twr_NumerKat { get; set; }
        public Nullable<int> Twr_IdGrupa { get; set; }
        public string Twr_Typ { get; set; }
        public Nullable<bool> Twr_Opak_Kaucja { get; set; }
        public string Twr_EAN { get; set; }
        public string Twr_PKWiU { get; set; }
        public Nullable<byte> Twr_VatSpr { get; set; }
        public Nullable<byte> Twr_VatZak { get; set; }
        public Nullable<decimal> Twr_CenaDomysl { get; set; }
        public string Twr_Nazwa { get; set; }
        public Nullable<int> Twr_IdKatSprzedazy { get; set; }
        public Nullable<int> Twr_IdKatZakupu { get; set; }
        public string Twr_JednPodst { get; set; }
        public string Twr_JednPomoc { get; set; }
        public Nullable<decimal> Twr_CenaZakNetto { get; set; }
        public Nullable<decimal> Twr_CenaZakBrutto { get; set; }
        public Nullable<decimal> Twr_CenaSprNetto { get; set; }
        public Nullable<decimal> Twr_CenaSprBrutto { get; set; }
        public Nullable<decimal> Twr_Marza { get; set; }
        public string Twr_Waluta { get; set; }
        public Nullable<int> Twr_WprUzytkId { get; set; }
        public Nullable<System.DateTime> Twr_WprUzytData { get; set; }
        public Nullable<int> Twr_ZmienUzytId { get; set; }
        public Nullable<System.DateTime> Twr_ZmiUzytData { get; set; }
        public Nullable<bool> Twr_CzyAktywne { get; set; }
        public Nullable<int> Twr_IdKategoria { get; set; }
        public string Twr_IdTowaryIlosci { get; set; }
        public Nullable<int> Twr_IdVatSprzedaz { get; set; }
        public Nullable<int> Twr_IdVatZakup { get; set; }
        public Nullable<int> Twr_IdJednostkaPodstawowa { get; set; }
        public Nullable<int> Twr_IdJednostkaPomocnicza { get; set; }
        public Nullable<int> Twr_IdTypTowaru { get; set; }
        public Nullable<int> Twr_CenaDomyslna { get; set; }
    }
}
