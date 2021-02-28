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
    public class WszystkiePZViewModel : WszystkieViewModel<PZForView>
    {
        #region Fields
        private PZForView _WybranyPZ;
        #endregion
        #region Constructor
        public WszystkiePZViewModel() : base()
        {
            base.DisplayName = "Wszystkie PZ";
        }
        #endregion
        #region Properties
        public PZForView WybranyPZ
        {
            get
            {
                return _WybranyPZ;
            }
            set
            {
                _WybranyPZ = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranyPZ != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var pz = db.PZ.FirstOrDefault(x => x.Pz_IdPZ == WybranyPZ.Pz_IdPZ).Pz_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
            
        }

        public override void edit()
        {
            if (WybranyPZ != null)
            {

                Messenger.Default.Send(WybranyPZ);
            }
        }

        public override void find()
        {
            if (FindField == "Numer dokumentu")
            {
                List = new ObservableCollection<PZForView>(List.Where(item => item.Pz_NrDok != null && item.Pz_NrDok.StartsWith(FindTextBox)));
            }
            if (FindField == "Kontrahent")
            {
                List = new ObservableCollection<PZForView>(List.Where(item => item.Pz_Kontrahent != null && item.Pz_Kontrahent.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<PZForView>(List.Where(item => item.Pz_Kategoria != null && item.Pz_Kategoria.StartsWith(FindTextBox)));
            }
            if (FindField == "Platnosc")
            {
                List = new ObservableCollection<PZForView>(List.Where(item => item.Pz_PlatnoscTyp != null && item.Pz_PlatnoscTyp.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<PZForView>(List.Where(item => item.Pz_DataSprzedazy >= DataSprzedazyOd && item.Pz_DataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<PZForView>(List.Where(item => item.Pz_DataWyst >= DataWystawieniaOd && item.Pz_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<PZForView>(List.Where(item => Convert.ToInt32(item.Pz_NrDok) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Pz_NrDok) <= Convert.ToInt32(NumerDo)));
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
                List = new ObservableCollection<PZForView>(List.OrderBy(item => item.Pz_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<PZForView>(List.OrderBy(item => item.Pz_Kontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<PZForView>(List.OrderBy(item => item.Pz_PlatnoscTyp));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<PZForView>(List.OrderBy(item => item.Pz_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<PZForView>(List.OrderBy(item => item.Pz_Magazyn));
            }
        }
        public override void load()
        {
            List = new ObservableCollection<PZForView>
               (
                  from zam in minimarketEntities.PZ
                  where zam.Pz_CzyAktywne == true
                  select new PZForView
                  {
                      Pz_IdPZ = zam.Pz_IdPZ,
                      Pz_DataSprzedazy = zam.Pz_DataSprzedazy,
                      Pz_DataOd = zam.Pz_DataOd,
                      Pz_DataWprow = zam.Pz_DataWprow,
                      Pz_DataWyst = zam.Pz_DataWyst,
                      Pz_DataZmian = zam.Pz_DataZmian,
                      Pz_IdPozycji = zam.Pz_IdPozycji,
                      Pz_Kontrahent = zam.Kontrahenci.Knt_Nazwa1,
                      Pz_Kategoria = zam.Kategorie.K_Nazwa,
                      Pz_Magazyn = zam.Magazyny.M_Nazwa,
                      Pz_NrDok = zam.Pz_NrDok,
                      Pz_KontrahentOdbiorca = zam.Kontrahenci.Knt_Nazwa1,
                      Pz_CzyAktywne = zam.Pz_CzyAktywne,
                      Pz_PlatnoscTyp = zam.RodzajePlatnosci.RP_Nazwa,
                      Pz_Rabat = zam.Pz_Rabat,
                      Pz_Termin = zam.Pz_Termin,
                      Pz_DokNettoBrutto = zam.DokumentNettoBrutto.Nazwa,
                      Pz_RodzajDokumentu = zam.RodzajeDokumentow.RD_Nazwa,



                  }
               );
        }

       
        #endregion


    }
}
