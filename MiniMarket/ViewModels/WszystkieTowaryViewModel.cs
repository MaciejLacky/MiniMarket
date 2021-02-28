using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;

namespace MiniMarket.ViewModels
{
    public class WszystkieTowaryViewModel : WszystkieViewModel<TowarForView>
    {
        #region Fields
        private TowarForView _WybranyTowar;
        private bool _selectedItem;
        private int _wybraneId;
        #endregion
        #region Constructor
        public WszystkieTowaryViewModel(bool selectedItem):base()
        {
            base.DisplayName = "Towary";
            this._selectedItem = selectedItem;

        }
        #endregion
        #region Properties
        public int WybraneId
        {
            get
            {
                return _wybraneId;
            }
            set
            {
                _wybraneId = value;
            }
        }
        public TowarForView WybranyTowar
        {
            get
            {
                return _WybranyTowar;
            }
            set
            {
                if (_WybranyTowar != value)
                {
                    _WybranyTowar = value;
                   
                    if (_selectedItem == true)
                    {
                        Messenger.Default.Send(_WybranyTowar);
                        OnRequestClose();
                    }
                }
            }
        }
        #endregion


        #region Helpers
        public override void delete()
        {
            if (WybranyTowar != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var towar = db.Towary.FirstOrDefault(x => x.Twr_IdTowaru == WybranyTowar.Twr_IdTowaru);
                towar.Twr_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
           
        }

        public override void edit()
        {
           WszystkieTowaryViewModel id = new WszystkieTowaryViewModel(false);
            if (WybranyTowar != null)
            {
                id.WybraneId = WybranyTowar.Twr_IdTowaru;
                Messenger.Default.Send(id);
            }
        }

        public override void find()
        {
            if (FindField == "Nazwa")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_Nazwa != null && item.Twr_Nazwa.StartsWith(FindTextBox)));
            }
            if (FindField == "Kod")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_Kod != null && item.Twr_Kod.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_KatSprzedazy != null && item.Twr_KatSprzedazy.StartsWith(FindTextBox)));
            }
            if (FindField == "Grupa")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_Grupa != null && item.Twr_Grupa.StartsWith(FindTextBox)));
            }
            if (FindField == "Cena zakupu netto")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_CenaZakNetto != null && item.Twr_CenaZakNetto == Convert.ToInt32( FindTextBox)));
            }
            if (FindField == "Vat zakupu")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_VatZak != null && item.Twr_VatZak == Convert.ToInt32(FindTextBox)));
            }
            if (FindField == "Jednostka")
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_JednPodst != null && item.Twr_JednPodst.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_WprUzytData >= DataSprzedazyOd && item.Twr_WprUzytData <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => item.Twr_ZmiUzytData >= DataWystawieniaOd && item.Twr_ZmiUzytData <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<TowarForView>(List.Where(item => Convert.ToInt32(item.Twr_Kod) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Twr_Kod) <= Convert.ToInt32(NumerDo)));
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
                List = new ObservableCollection<TowarForView>(List.OrderBy(item => item.Twr_Nazwa));
            }
            if (SortList == "Kod")
            {
                List = new ObservableCollection<TowarForView>(List.OrderBy(item => item.Twr_Kod));
            }
            if (SortList == "Grupa")
            {
                List = new ObservableCollection<TowarForView>(List.OrderBy(item => item.Twr_Grupa));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<TowarForView>(List.OrderBy(item => item.Twr_KatSprzedazy));
            }
        }
        public override void load()
        {

            List = new ObservableCollection<TowarForView>
               (
                  from towar in minimarketEntities.Towary
                  where towar.Twr_CzyAktywne == true
                  select new TowarForView
                  {
                      Twr_IdTowaru = towar.Twr_IdTowaru,
                      Twr_Kod = towar.Twr_Kod,
                      Twr_NumerKat = towar.Twr_NumerKat,
                      Twr_Grupa = towar.GrupyTowarowe.G_GRNazwa,
                      Twr_Typ = towar.TypTowaru.TypT_Wartosc,
                      Twr_Opak_Kaucja = towar.Twr_Opak_Kaucja,
                      Twr_EAN = towar.Twr_EAN,
                      Twr_PKWiU = towar.Twr_PKWiU,
                      Twr_VatSpr = towar.VatSprzedaz.VatSpr_Wartosc,
                      Twr_VatZak = towar.VatZakup.VatZak_Wartosc,
                      Twr_CenaDomysl = towar.CenaDomyslna.CenaDom_Wartosc,
                      Twr_Nazwa = towar.Twr_Nazwa,
                      Twr_KatSprzedazy = towar.Kategorie.K_Nazwa,
                      Twr_KatZakupu = towar.Kategorie.K_Nazwa,
                      Twr_JednPodst = towar.JednostkaPodstawowa.JedPd_Wartosc,
                      Twr_JednPomoc = towar.JednostkaPomocnicza.JednPc_Wartosc,
                      Twr_CenaZakNetto = towar.Twr_CenaZakNetto,
                      Twr_CenaZakBrutto = towar.Twr_CenaZakBrutto,
                      Twr_CenaSprNetto = towar.Twr_CenaSprNetto,
                      Twr_CenaSprBrutto = towar.Twr_CenaSprBrutto,
                      Twr_Marza = towar.Twr_Marza,
                      Twr_Waluta = towar.Twr_Waluta,
                      Twr_CzyAktywne = towar.Twr_CzyAktywne,

                  }
                  );
            
        }

       
        #endregion



    }
}
