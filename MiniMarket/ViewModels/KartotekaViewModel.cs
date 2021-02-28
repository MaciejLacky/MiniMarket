using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class KartotekaViewModel : WszystkieViewModel<MagazynForView>
    {
        private int? _IdTowaru;
        public KartotekaViewModel(int? idTowaru)
        {
            this._IdTowaru = idTowaru;
            base.DisplayName = "Kartoteka";
        }

        public override void delete()
        {
            throw new NotImplementedException();
        }

        public override void edit()
        {

        }

        public override void find()
        {
            if (FindField == "Nazwa")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_Nazwa != null && item.Twr_Nazwa.StartsWith(FindTextBox)));
            }
            if (FindField == "Kod")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_Kod != null && item.Twr_Kod.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_KatSprzedazy != null && item.Twr_KatSprzedazy.StartsWith(FindTextBox)));
            }
            if (FindField == "Grupa")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_Grupa != null && item.Twr_Grupa.StartsWith(FindTextBox)));
            }
            if (FindField == "Cena zakupu netto")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_CenaZakNetto != null && item.Twr_CenaZakNetto == Convert.ToInt32(FindTextBox)));
            }
            if (FindField == "Vat zakupu")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_VatZak != null && item.Twr_VatZak == Convert.ToInt32(FindTextBox)));
            }
            if (FindField == "Jednostka")
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_JednPodst != null && item.Twr_JednPodst.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_WprUzytData >= DataSprzedazyOd && item.Twr_WprUzytData <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_WprUzytData >= DataWystawieniaOd && item.Twr_WprUzytData <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => Convert.ToInt32(item.Twr_Kod) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Twr_Kod) <= Convert.ToInt32(NumerDo)));
            }
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa", "Kod", "Kategoria", "Grupa", "Cena zakupu netto", "Vat", "Jednostka" };
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa", "Kod", "Kategoria", "Grupa" };
        }
        public override void sort()
        {
            if (SortList == "Nazwa")
            {
                List = new ObservableCollection<MagazynForView>(List.OrderBy(item => item.Twr_Nazwa));
            }
            if (SortList == "Kod")
            {
                List = new ObservableCollection<MagazynForView>(List.OrderBy(item => item.Twr_Kod));
            }
            if (SortList == "Grupa")
            {
                List = new ObservableCollection<MagazynForView>(List.OrderBy(item => item.Twr_Grupa));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<MagazynForView>(List.OrderBy(item => item.Twr_KatSprzedazy));
            }
        }

        public override void load()
        {

            List = new ObservableCollection<MagazynForView>
               (
                  from towar in minimarketEntities.TowaryIlosci
                  where towar.Twr_IdTowaru == _IdTowaru
                  select new MagazynForView
                  {
                      Twr_IdTowaru = towar.Twr_IdTowaru,
                      Twr_DataZmian = towar.Twr_DataZmian,
                      Twr_WprowData = towar.Twr_WprowData,
                      Twr_SprzedazIlosc = towar.Twr_SprzedazIlosc,
                      Twr_ZakupIlosc = towar.Twr_ZakupIlosc,
                      Twr_Nazwa = towar.Towary.Twr_Nazwa,
                      Twr_JednPodst = towar.Towary.JednostkaPodstawowa.JedPd_Wartosc,
                      Twr_Kod = towar.Towary.Twr_Kod,
                      Twr_NumerKat = towar.Towary.Twr_NumerKat,
                      Twr_Grupa = towar.Towary.GrupyTowarowe.G_GRNazwa,
                      Twr_KatSprzedazy = towar.Towary.Kategorie.K_Nazwa,
                      Twr_KatZakupu = towar.Towary.Kategorie.K_Nazwa,
                      Twr_VatSpr = towar.Towary.VatSprzedaz.VatSpr_Wartosc,
                      Twr_VatZak = towar.Towary.VatZakup.VatZak_Wartosc,
                      Twr_CenaZakNetto = towar.Towary.Twr_CenaZakNetto,
                      Twr_CenaSprNetto = towar.Towary.Twr_CenaSprNetto,
                      Twr_WartoscCenaSprNetto = towar.Twr_SprzedazIlosc * towar.Towary.Twr_CenaSprNetto,
                      Twr_WartoscCenaZakNetto = towar.Twr_ZakupIlosc * towar.Towary.Twr_CenaZakNetto,
                      Twr_Typ = towar.Twr_TypDokumentu,
                      NumerDokumentu = towar.Twr_NumerDokumentu,
                      Twr_WprUzytData = towar.Twr_DataSprzedaz == null ? towar.Twr_DataZakup : towar.Twr_DataSprzedaz


                  }
                  ) ;
        }
    }
}
