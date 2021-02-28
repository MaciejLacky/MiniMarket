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
    public class ParagonyViewModel : WszystkieViewModel<ParagonForView>
    {
        #region Fields
        private ParagonForView _WybranyParagon;
        #endregion
        #region Constructor
        public ParagonyViewModel() : base()
        {
            base.DisplayName = "Paragony";
        }
        #endregion

        #region Properties
        public ParagonForView WybranyParagon
        {
            get
            {
                return _WybranyParagon;
            }
            set
            {
                _WybranyParagon = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranyParagon != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var fv = db.Paragony.FirstOrDefault(x => x.Par_IdParagonu == WybranyParagon.Par_IdParagonu);
                fv.Par_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
            
        }

        public override void edit()
        {
            throw new NotImplementedException();
        }

        public override void find()
        {
            if (FindField == "Numer dokumentu")
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => item.Par_NrDok != null && item.Par_NrDok.StartsWith(FindTextBox)));
            }
            if (FindField == "Kontrahent")
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => item.Par_Kontrahent != null && item.Par_Kontrahent.StartsWith(FindTextBox)));
            }
            if (FindField == "Kategoria")
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => item.Par_Kategoria != null && item.Par_Kategoria.StartsWith(FindTextBox)));
            }
            if (FindField == "Platnosc")
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => item.Par_RodzajuPlatnosci != null && item.Par_RodzajuPlatnosci.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => item.Par_DataSprzedazy >= DataSprzedazyOd && item.Par_DataSprzedazy <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => item.Par_DataWyst >= DataWystawieniaOd && item.Par_DataWyst <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<ParagonForView>(List.Where(item => Convert.ToInt32(item.Par_NrDok) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Par_NrDok) <= Convert.ToInt32(NumerDo)));
            }
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria" };
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria", "Platnosc" };
        }
        public override void sort()
        {
            if (SortList == "Numer dokumentu")
            {
                List = new ObservableCollection<ParagonForView>(List.OrderBy(item => item.Par_NrDok));
            }
            if (SortList == "Kontrahent")
            {
                List = new ObservableCollection<ParagonForView>(List.OrderBy(item => item.Par_Kontrahent));
            }
            if (SortList == "Platnosc")
            {
                List = new ObservableCollection<ParagonForView>(List.OrderBy(item => item.Par_RodzajuPlatnosci));
            }
            if (SortList == "Kategoria")
            {
                List = new ObservableCollection<ParagonForView>(List.OrderBy(item => item.Par_Kategoria));
            }
            if (SortList == "Magazyn")
            {
                List = new ObservableCollection<ParagonForView>(List.OrderBy(item => item.Par_Magazyn));
            }
        }
        public override void load()
        {
            List = new ObservableCollection<ParagonForView>
                (
                   from zam in minimarketEntities.Paragony
                   where zam.Par_CzyAktywne == true
                   select new ParagonForView
                   {
                       Par_IdParagonu = zam.Par_IdParagonu,
                       Par_DataSprzedazy = zam.Par_DataSprzedazy,
                       Par_DataOd = zam.Par_DataOd,
                       Par_DataWprow = zam.Par_DataWprow,
                       Par_DataWyst = zam.Par_DataWyst,
                       Par_DataZmian = zam.Par_DataZmian,
                       Par_IdPozycji = zam.Par_IdPozycji,
                       Par_Kontrahent = zam.Kontrahenci.Knt_Nazwa1,
                       Par_Kategoria = zam.Kategorie.K_Nazwa,
                       Par_Magazyn = zam.Magazyny.M_Nazwa,
                       Par_NrDok = zam.Par_NrDok,
                       Par_KontrahentOdbiorca = zam.Kontrahenci.Knt_Nazwa1,
                       Par_CzyAktywne = zam.Par_CzyAktywne,
                       Par_Rabat = zam.Par_Rabat,
                       Par_Termin = zam.Par_Termin,
                       Par_NettoBrutto = zam.DokumentNettoBrutto.Nazwa,
                       Par_RodzajuDokumentu = zam.RodzajeDokumentow.RD_Nazwa,
                       Par_RodzajuPlatnosci = zam.RodzajePlatnosci.RP_Nazwa

                   }
                );
        }

       
        #endregion


    }
}
