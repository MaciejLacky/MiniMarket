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
    public class ZamowieniaViewModel : WszystkieViewModel<ZamowieniaForView>
    {
        #region Fields
        private ZamowieniaForView _WybraneZamowienie;
        #endregion
        #region Constructor
        public ZamowieniaViewModel() : base()
        {
            base.DisplayName = "Zamówienia";
        }
        #endregion
        #region Properties
        public ZamowieniaForView WybraneZamowienie
        {
            get
            {
                return _WybraneZamowienie;
            }
            set
            {
                _WybraneZamowienie = value;
            }
        }
        #endregion

        public override void delete()
        {
            if (WybraneZamowienie != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var wz = db.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == WybraneZamowienie.Zam_IdZamowienia).Zam_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
        }

        public override void edit()
        {
            if (WybraneZamowienie != null)
            {

                Messenger.Default.Send(WybraneZamowienie);
            }
        }

        public override void find()
        {
            if (FindField == "Numer dokumentu")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => item.Zam_NrDok != null && item.Zam_NrDok.StartsWith(FindTextBox)));
            }
            if (FindField == "Kontrahent")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => item.Zam_Kontrahent != null && item.Zam_Kontrahent.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => item.Zam_Kategoria != null && item.Zam_Kategoria.StartsWith(FindTextBox)));
            }
            if (FindField == "Platnosc")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => item.Zam_PlatnoscTyp != null && item.Zam_PlatnoscTyp.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => item.Zam_DataSprzedazy >= DataSprzedazyOd && item.Zam_DataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => item.Zam_DataWyst >= DataWystawieniaOd && item.Zam_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<ZamowieniaForView>(List.Where(item => Convert.ToInt32(item.Zam_NrDok) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Zam_NrDok) <= Convert.ToInt32(NumerDo)));
            }
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria", "Platnosc" };
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria", "Platnosc", "Magazyn" };
        }
        public override void sort()
        {
            if (SortList == "Numer dokumentu")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.OrderBy(item => item.Zam_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.OrderBy(item => item.Zam_Kontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.OrderBy(item => item.Zam_PlatnoscTyp));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.OrderBy(item => item.Zam_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<ZamowieniaForView>(List.OrderBy(item => item.Zam_Magazyn));
            }
        }
        public override void load()
        {
            List = new ObservableCollection<ZamowieniaForView>
                (
                   from zam in minimarketEntities.Zamowienia
                   where zam.Zam_CzyAktywne == true
                   select new ZamowieniaForView
                   {
                       Zam_IdZamowienia = zam.Zam_IdZamowienia,                      
                       Zam_DataOd = zam.Zam_DataOd,
                       Zam_DataSprzedazy = zam.Zam_DataSprzedazy,
                       Zam_DataWprow = zam.Zam_DataWprow,
                       Zam_DataWyst = zam.Zam_DataWyst,
                       Zam_DataZmian = zam.Zam_DataZmian,
                       Zam_IdPozycji = zam.Zam_IdPozycji,
                       Zam_Kontrahent = zam.Kontrahenci.Knt_Nazwa1,
                       Zam_Kategoria = zam.Kategorie.K_Nazwa,
                       Zam_Magazyn = zam.Magazyny.M_Nazwa,
                       Zam_NrDok = zam.Zam_NrDok,
                       Zam_KontrahentOdbiorca = zam.Kontrahenci.Knt_Nazwa1,
                       Zam_CzyAktywne = zam.Zam_CzyAktywne,
                       Zam_PlatnoscTyp = zam.RodzajePlatnosci.RP_Nazwa,
                       Zam_Rabat = zam.Zam_Rabat,
                       Zam_Termin = zam.Zam_Termin,
                                             

                   }
                );
        }

       
    }
}
