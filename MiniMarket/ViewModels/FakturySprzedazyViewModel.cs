using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MiniMarket.ViewModels
{
    class FakturySprzedazyViewModel :WszystkieViewModel<FakturaSprzedazyForView>
    {
        #region Fields
        private FakturaSprzedazyForView _WybranaFaktura;
        #endregion
        #region Constructor
        public FakturySprzedazyViewModel() : base()
        {
            base.DisplayName = "Fv Sprzedaż";
        }
        #endregion
        #region Properties
        public FakturaSprzedazyForView WybranaFaktura
        {
            get
            {
                return _WybranaFaktura;
            }
            set
            {                              
                _WybranaFaktura = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranaFaktura != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var fv = db.FakturySprzedazy.FirstOrDefault(x => x.Fvs_IdSprzedaz == WybranaFaktura.Fvs_IdZakup);
                fv.CzyAktywne = false;
                db.SaveChanges();
                load();
            }
           
        }

        public override void edit()
        {
           
            if (WybranaFaktura != null)
            {
                
                Messenger.Default.Send(WybranaFaktura);
            }
        }

        public override void find()
        {
            if (FindField == "Numer dokumentu")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => item.Fvs_NrDok != null && item.Fvs_NrDok.StartsWith(FindTextBox)));
            }
            if (FindField == "Kontrahent")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => item.Fvs_Kontrahent != null && item.Fvs_Kontrahent.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => item.Fvs_Kategoria != null && item.Fvs_Kategoria.StartsWith(FindTextBox)));
            }
            if (FindField == "Platnosc")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => item.Fvs_PlatnoscTyp != null && item.Fvs_PlatnoscTyp.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => item.FvsDataSprzedazy >= DataSprzedazyOd && item.FvsDataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => item.Fvs_DataWyst >= DataWystawieniaOd && item.Fvs_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.Where(item => Convert.ToInt32( item.Fvs_NrDok)  >= Convert.ToInt32( NumerOd) && Convert.ToInt32( item.Fvs_NrDok) <= Convert.ToInt32( NumerDo)));
            }
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent","Kategoria", "Platnosc" };
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent","Kategoria", "Platnosc","Magazyn" };
        }
        public override void sort()
        {           
            if (SortList == "Numer dokumentu")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.OrderBy(item => item.Fvs_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.OrderBy(item => item.Fvs_Kontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.OrderBy(item => item.Fvs_PlatnoscTyp));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.OrderBy(item => item.Fvs_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<FakturaSprzedazyForView>(List.OrderBy(item => item.Fvs_Magazyn));
            }
        }

        public override void load()
        {
            List = new ObservableCollection<FakturaSprzedazyForView>
               (
                  from fvs in minimarketEntities.FakturySprzedazy
                  where fvs.CzyAktywne == true
                  select new FakturaSprzedazyForView
                  {
                      Fvs_IdZakup = fvs.Fvs_IdSprzedaz,
                      Fvs_NrDok = fvs.Fvs_NrDok,
                      FvsDataOd = fvs.FvsDataOd,
                      FvsDataSprzedazy = fvs.FvsDataSprzedazy,
                      FvsDataZmian = fvs.FvsDataZmian,
                      FvsRabat = fvs.FvsRabat,
                      Fvs_DataWprow = fvs.Fvs_DataWprow,
                      Fvs_DataWyst = fvs.Fvs_DataWyst,
                      Fvs_DokNettoBrutto = fvs.DokumentNettoBrutto.Nazwa,
                      Fvs_Kategoria = fvs.Kategorie.K_Nazwa,
                      Fvs_Kontrahent = fvs.Kontrahenci.Knt_Nazwa1 + " " + fvs.Kontrahenci.Knt_Ulica + " " + fvs.Kontrahenci.Knt_NrDomu + " " +
                      fvs.Kontrahenci.Knt_KodPocztowy + " " + fvs.Kontrahenci.Knt_Poczta + " " +
                      " " + fvs.Kontrahenci.Knt_Nip,
                      Fvs_Magazyn = fvs.Magazyny.M_Nazwa,
                      Fvs_PlatnoscTyp = fvs.RodzajePlatnosci.RP_Nazwa,
                      Fvs_RodzajDokumentu = fvs.RodzajeDokumentow.RD_Nazwa,
                      Fvs_Termin = fvs.Fvs_Termin,


                  }
               ) ;
        }

        
        #endregion


    }
}
