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
    public class FakturyZakupuViewModel : WszystkieViewModel<FakturaZakupuForView>
    {
        #region Fields
        private FakturaZakupuForView _WybranaFaktura;
        
       
        #endregion
        #region Konstruktor
        public FakturyZakupuViewModel() : base()
        {
            base.DisplayName = "Fv zakupu";
        }
        #endregion
        #region Properties
        public FakturaZakupuForView WybranaFaktura
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
                var fv = db.FakturyZakupu.FirstOrDefault(x => x.Fvz_IdZakup == WybranaFaktura.Fvz_IdZakup);
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
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => item.Fvz_NrDok != null && item.Fvz_NrDok.StartsWith(FindTextBox)));
            }                                   
            if (FindField == "Kontrahent")      
            {                                   
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => item.Fvz_Kontrahent != null && item.Fvz_Kontrahent.StartsWith(FindTextBox)));
            }                                   
            if (FindField == "Kategoria")       
            {                                   
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => item.Fvz_Kategoria != null && item.Fvz_Kategoria.StartsWith(FindTextBox)));
            }                                   
            if (FindField == "Platnosc")        
            {                                   
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => item.Fvz_PlatnoscTyp != null && item.Fvz_PlatnoscTyp.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => item.FvzDataSprzedazy >= DataSprzedazyOd && item.FvzDataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => item.Fvz_DataWyst >= DataWystawieniaOd && item.Fvz_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.Where(item => Convert.ToInt32(item.Fvz_NrDok) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Fvz_NrDok) <= Convert.ToInt32(NumerDo)));
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
                List = new ObservableCollection<FakturaZakupuForView>(List.OrderBy(item => item.Fvz_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.OrderBy(item => item.Fvz_Kontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.OrderBy(item => item.Fvz_PlatnoscTyp));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.OrderBy(item => item.Fvz_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<FakturaZakupuForView>(List.OrderBy(item => item.Fvz_Magazyn));
            }
        }
        public override void load()
        {
            List = new ObservableCollection<FakturaZakupuForView>
              (
                
                 from fvZakupu in minimarketEntities.FakturyZakupu
                 where fvZakupu.CzyAktywne == true
                 select new FakturaZakupuForView
                 {
                     Fvz_IdZakup = fvZakupu.Fvz_IdZakup,
                     Fvz_NrDok = fvZakupu.Fvz_NrDok,
                     FvzDataOd = fvZakupu.FvzDataOd,
                     FvzDataSprzedazy = fvZakupu.FvzDataSprzedazy,
                     FvzDataZmian = fvZakupu.FvzDataZmian,
                     FvzRabat = fvZakupu.FvzRabat,
                     Fvz_DataWprow = fvZakupu.Fvz_DataWprow,
                     Fvz_DataWyst = fvZakupu.Fvz_DataWyst,
                     Fvz_DokNettoBrutto = fvZakupu.DokumentNettoBrutto.Nazwa,
                     Fvz_Kategoria = fvZakupu.Kategorie.K_Nazwa,
                     Fvz_Kontrahent = fvZakupu.Kontrahenci.Knt_Nazwa1 + " " + fvZakupu.Kontrahenci.Knt_Ulica + " " + fvZakupu.Kontrahenci.Knt_NrDomu + " " +
                     fvZakupu.Kontrahenci.Knt_KodPocztowy + " " + fvZakupu.Kontrahenci.Knt_Poczta + " " +
                     "NIP: " + fvZakupu.Kontrahenci.Knt_Nip,
                     Fvz_Magazyn = fvZakupu.Magazyny.M_Nazwa,
                     Fvz_PlatnoscTyp = fvZakupu.RodzajePlatnosci.RP_Nazwa,
                     Fvz_RodzajDokumentu = fvZakupu.RodzajeDokumentow.RD_Nazwa,
                     Fvz_Termin = fvZakupu.Fvz_Termin,


                 }
              ) ;
        }

       
        #endregion


    }
}
