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
    public class WszystkieWZViewModel : WszystkieViewModel<WZForView>
    {
        #region Fields
        private WZForView _WybranyWZ;
        #endregion
        #region Constructor
        public WszystkieWZViewModel() : base()
        {
            base.DisplayName = "Wszystkie WZ";

        }
        #endregion
        #region Properties
        public WZForView WybranyWZ
        {
            get
            {
                return _WybranyWZ;
            }
            set
            {
                _WybranyWZ = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranyWZ != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var wz = db.WZ.FirstOrDefault(x => x.Wz_IdWZ == WybranyWZ.Wz_IdWZ).Wz_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
            
        }

        public override void edit()
        {
            if (WybranyWZ != null)
            {

                Messenger.Default.Send(WybranyWZ);
            }
        }

        public override void find()
        {
            if (FindField == "Numer dokumentu")
            {
                List = new ObservableCollection<WZForView>(List.Where(item => item.Wz_NrDok != null && item.Wz_NrDok.StartsWith(FindTextBox)));
            }
            if (FindField == "Kontrahent")
            {
                List = new ObservableCollection<WZForView>(List.Where(item => item.Wz_Kontrahent != null && item.Wz_Kontrahent.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<WZForView>(List.Where(item => item.Wz_Kategoria != null && item.Wz_Kategoria.StartsWith(FindTextBox)));
            }
            if (FindField == "Platnosc")
            {
                List = new ObservableCollection<WZForView>(List.Where(item => item.Wz_PlatnoscTyp != null && item.Wz_PlatnoscTyp.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<WZForView>(List.Where(item => item.Wz_DataSprzedazy >= DataSprzedazyOd && item.Wz_DataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<WZForView>(List.Where(item => item.Wz_DataWyst >= DataWystawieniaOd && item.Wz_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<WZForView>(List.Where(item => Convert.ToInt32(item.Wz_NrDok) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Wz_NrDok) <= Convert.ToInt32(NumerDo)));
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
                List = new ObservableCollection<WZForView>(List.OrderBy(item => item.Wz_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<WZForView>(List.OrderBy(item => item.Wz_Kontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<WZForView>(List.OrderBy(item => item.Wz_PlatnoscTyp));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<WZForView>(List.OrderBy(item => item.Wz_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<WZForView>(List.OrderBy(item => item.Wz_Magazyn));
            }
        }
        public override void load()
        {
            List = new ObservableCollection<WZForView>
               (
                  from zam in minimarketEntities.WZ
                  where zam.Wz_CzyAktywne == true
                  select new WZForView
                  {
                      
                      Wz_DataSprzedazy = zam.Wz_DataSprzedazy,
                      Wz_DataOd = zam.Wz_DataOd,
                      Wz_DataWprow = zam.Wz_DataWprow,
                      Wz_DataWyst = zam.Wz_DataWyst,
                      Wz_DataZmian = zam.Wz_DataZmian,
                      Wz_IdPozycji = zam.Wz_IdPozycji,
                      Wz_Kontrahent = zam.Kontrahenci.Knt_Nazwa1,
                      Wz_Kategoria = zam.Kategorie.K_Nazwa,
                      Wz_Magazyn = zam.Magazyny.M_Nazwa,
                      Wz_NrDok = zam.Wz_NrDok,
                      Wz_KontrahentOdbiorca = zam.Kontrahenci.Knt_Nazwa1,
                      Wz_CzyAktywne = zam.Wz_CzyAktywne,
                      Wz_PlatnoscTyp = zam.RodzajePlatnosci.RP_Nazwa,
                      Wz_Rabat = zam.Wz_Rabat,
                      Wz_Termin = zam.Wz_Termin,
                      Wz_DokNettoBrutto = zam.DokumentNettoBrutto.Nazwa,
                      Wz_RodzajDokumentu = zam.RodzajeDokumentow.RD_Nazwa,
                      Wz_IdWZ = zam.Wz_IdWZ


                  }
               );
        }

       
        #endregion

    }
}
