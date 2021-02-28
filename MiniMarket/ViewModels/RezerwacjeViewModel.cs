using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class RezerwacjeViewModel : WszystkieViewModel<RezerwacjeForView>
    {
        #region Fields
        private RezerwacjeForView _WybranaRezerwacja;
        #endregion
        #region Construktor
        public RezerwacjeViewModel() : base()
        {
            base.DisplayName = "Rezerwacje";
        }
        #endregion
        #region Properties
        public RezerwacjeForView WybranaRezerwacja
        {
            get
            {
                return _WybranaRezerwacja;
            }
            set
            {
                _WybranaRezerwacja = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranaRezerwacja != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var fv = db.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == WybranaRezerwacja.Rez_IdRezerwacji);
                fv.Rez_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
           
        }

        public override void edit()
        {
            if (WybranaRezerwacja != null)
            {

                Messenger.Default.Send(WybranaRezerwacja);
            }
        }

        public override void find()
        {
            if (FindField == "Numer dokumentu")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => item.Rez_NrDok != null && item.Rez_NrDok.StartsWith(FindTextBox)));
            }
            if (FindField == "Kontrahent")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => item.Rez_NazwaKontrahent != null && item.Rez_NazwaKontrahent.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => item.Rez_Kategoria != null && item.Rez_Kategoria.StartsWith(FindTextBox)));
            }
            if (FindField == "Platnosc")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => item.Rez_PlatnoscTyp != null && item.Rez_PlatnoscTyp.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => item.Rez_DataSprzedazy >= DataSprzedazyOd && item.Rez_DataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => item.Rez_DataWyst >= DataWystawieniaOd && item.Rez_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<RezerwacjeForView>(List.Where(item => Convert.ToInt32(item.Rez_NrDok) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Rez_NrDok) <= Convert.ToInt32(NumerDo)));
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
                List = new ObservableCollection<RezerwacjeForView>(List.OrderBy(item => item.Rez_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.OrderBy(item => item.Rez_NazwaKontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.OrderBy(item => item.Rez_PlatnoscTyp));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.OrderBy(item => item.Rez_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<RezerwacjeForView>(List.OrderBy(item => item.Rez_Magazyn));
            }
        }
        public override void load()
        {
            List = new ObservableCollection<RezerwacjeForView>
                (
                   from zam in minimarketEntities.Rezerwacje
                   where zam.Rez_CzyAktywne == true
                   select new RezerwacjeForView
                   {
                       Rez_IdRezerwacji = zam.Rez_IdRezerwacji,
                       Rez_DataSprzedazy = zam.Rez_DataSprzedazy,
                       Rez_DataOd = zam.Rez_DataOd,
                       Rez_DataWprow = zam.Rez_DataWprow,
                       Rez_DataWyst = zam.Rez_DataWyst,
                       Rez_DataZmian = zam.Rez_DataZmian,
                       Rez_IdPozycji = zam.Rez_IdPozycji,
                       Rez_NazwaKontrahent = zam.Kontrahenci.Knt_Nazwa1,
                       Rez_Kategoria = zam.Kategorie.K_Nazwa,
                       Rez_Magazyn = zam.Magazyny.M_Nazwa,
                       Rez_NrDok = zam.Rez_NrDok,
                       Rez_KontrahentOdbiorca = zam.Kontrahenci.Knt_Nazwa1,
                       Rez_CzyAktywne = zam.Rez_CzyAktywne,
                       Rez_PlatnoscTyp = zam.RodzajePlatnosci.RP_Nazwa,
                       Rez_Rabat = zam.Rez_Rabat,
                       Rez_Termin = zam.Rez_Termin,
                       Rez_DokNettoBrutto = zam.DokumentNettoBrutto.Nazwa,
                       Rez_RodzajDokumentu = zam.RodzajeDokumentow.RD_Nazwa,


                   }
                );
        }

       
        #endregion


    }
}
