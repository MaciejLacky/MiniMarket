using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Helper;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public class MagazynViewModel : WszystkieViewModel<MagazynForView>
    {
        #region Fields
        private MagazynForView _WybranyTowar;
        private BaseCommand _KartotekaCommand;
        #endregion
        #region Construktor
        public MagazynViewModel()
        {
            base.DisplayName = "Magazyn";
        }
        #endregion

        #region Properties
        public MagazynForView WybranyTowar
        {
            get
            {
                return _WybranyTowar;
            }
            set
            {
                _WybranyTowar = value;
            }
        }
        public ICommand KartotekaCommand
        {
            get
            {
                if (_KartotekaCommand == null)
                {
                    _KartotekaCommand = new BaseCommand(() => edit());
                }
                return _KartotekaCommand;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            throw new NotImplementedException();
        }

        public override void edit()
        {
            if (WybranyTowar != null)
            {
                Messenger.Default.Send(WybranyTowar);
            }
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
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_WprUzytData >= DataSprzedazyOd && item.Twr_WprUzytData <= DataSprzedazyDo && item.Twr_ZakupIlosc == null));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<MagazynForView>(List.Where(item => item.Twr_WprUzytData >= DataWystawieniaOd && item.Twr_WprUzytData <= DataWystawieniaDo && item.Twr_SprzedazIlosc == null));
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
            ObservableCollection<MagazynForView> ListaIlosci;
            ListaIlosci = new ObservableCollection<MagazynForView>
                   (
                   from towar in minimarketEntities.Towary
                   where towar.Twr_CzyAktywne == true
                   select new MagazynForView
                   {
                       
                       Twr_IdTowaru = towar.Twr_IdTowaru,
                       Twr_DataZmian = towar.Twr_ZmiUzytData,
                       Twr_WprowData = towar.Twr_ZmiUzytData,
                       Twr_SprzedazIlosc = 0,
                       Twr_ZakupIlosc = 0,
                       Twr_Nazwa = towar.Twr_Nazwa,
                       Twr_JednPodst = towar.JednostkaPodstawowa.JedPd_Wartosc,
                       Twr_Kod = towar.Twr_Kod,
                       Twr_NumerKat = towar.Twr_NumerKat,
                       Twr_Grupa = towar.GrupyTowarowe.G_GRNazwa,
                       Twr_KatSprzedazy = towar.Kategorie.K_Nazwa,
                       Twr_KatZakupu = towar.Kategorie.K_Nazwa,
                       Twr_VatSpr = towar.VatSprzedaz.VatSpr_Wartosc,
                       Twr_VatZak = towar.VatZakup.VatZak_Wartosc,
                       Twr_CenaZakNetto = towar.Twr_CenaZakNetto,
                       Twr_CenaSprNetto = towar.Twr_CenaSprNetto,
                       Twr_WartoscCenaSprNetto = 0,
                       Twr_WartoscCenaZakNetto = 0,
                       Twr_StanIlosc = 0,

                   }
                   );
            decimal? y;
            decimal? z;
            foreach (var items in minimarketEntities.Towary.Where(x => x.Twr_CzyAktywne == true))
            {
                y = 0;
                z = 0;
                foreach (var item in minimarketEntities.TowaryIlosci.Where(x =>x.CzyAktywne==true))
                {
                    if (items.Twr_IdTowaru == item.Twr_IdTowaru)
                    {
                        if (item.Twr_SprzedazIlosc != null)
                        {
                            y += item.Twr_SprzedazIlosc;
                        }

                        if (item.Twr_ZakupIlosc != null)
                        {
                            z += item.Twr_ZakupIlosc;
                        }

                    }

                }
                ListaIlosci.FirstOrDefault(x => x.Twr_IdTowaru == items.Twr_IdTowaru).Twr_SprzedazIlosc = y;
                ListaIlosci.FirstOrDefault(x => x.Twr_IdTowaru == items.Twr_IdTowaru).Twr_ZakupIlosc = z;
            }
            List = new ObservableCollection<MagazynForView>
               (
                  from towar in ListaIlosci                 
                  select new MagazynForView
                  {
                      Twr_IdTowaru = towar.Twr_IdTowaru,
                      Twr_DataZmian = towar.Twr_ZmiUzytData,
                      Twr_WprowData = towar.Twr_ZmiUzytData,
                      Twr_SprzedazIlosc = towar.Twr_SprzedazIlosc,
                      Twr_ZakupIlosc = towar.Twr_ZakupIlosc,
                      Twr_Nazwa = towar.Twr_Nazwa,
                      Twr_JednPodst = towar.Twr_JednPodst,
                      Twr_Kod = towar.Twr_Kod,
                      Twr_NumerKat = towar.Twr_NumerKat,
                      Twr_Grupa = towar.Twr_Grupa,
                      Twr_KatSprzedazy = towar.Twr_KatSprzedazy,
                      Twr_KatZakupu = towar.Twr_KatZakupu,
                      Twr_VatSpr = towar.Twr_VatSpr,
                      Twr_VatZak = towar.Twr_VatZak,
                      Twr_CenaZakNetto = towar.Twr_CenaZakNetto,
                      Twr_CenaSprNetto = towar.Twr_CenaSprNetto,
                      Twr_WartoscCenaSprNetto = towar.Twr_SprzedazIlosc * towar.Twr_CenaSprNetto,
                      Twr_WartoscCenaZakNetto = towar.Twr_ZakupIlosc * towar.Twr_CenaZakNetto,
                      Twr_StanIlosc = towar.Twr_ZakupIlosc - towar.Twr_SprzedazIlosc,
                  }
                  );
        }
    }
        #endregion



    
}
