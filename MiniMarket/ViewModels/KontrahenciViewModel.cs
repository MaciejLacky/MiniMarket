using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace MiniMarket.ViewModels
{
    public class KontrahenciViewModel : WszystkieViewModel<Kontrahenci>
    {
        #region Fields
        private Kontrahenci _WybranyKontrahent;
        private bool _selectedMode;
        private int _wybraneId;
        
        #endregion
        #region Konstruktor
        public KontrahenciViewModel(bool selectedmode) : base()
        {
            
            base.DisplayName = "Kontrahenci";
            this._selectedMode = selectedmode;
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
        public Kontrahenci WybranyKontrahent
        {

            get
            {
                return _WybranyKontrahent;
            }
            set
            {
                Kontrahenci kont = new Kontrahenci();

                if (_WybranyKontrahent != value)
                {   //Po każdym kliknieciu w Kontrahenta ustawia się properties wybrany kontrahent
                    //wybrany kontrahent jest wysyłany messenegerem do fv i jest zamykany
                    _WybranyKontrahent = value;
                    
                    if (_selectedMode == true)
                    {
                        Messenger.Default.Send(_WybranyKontrahent);
                        OnRequestClose();
                    }


                }
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranyKontrahent != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var kontrahent = db.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == WybranyKontrahent.Knt_KntId).Knt_CzyAktywny = false;
                db.SaveChanges();
                load();
            }

        }

        public override void edit()
        {
            KontrahenciViewModel id = new KontrahenciViewModel(false);
            if (WybranyKontrahent != null)
            {
                id.WybraneId = WybranyKontrahent.Knt_KntId;
                Messenger.Default.Send(id);
            }
        }

        public override void find()
        {
            if (FindField == "Nazwa")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Nazwa1 != null && item.Knt_Nazwa1.StartsWith(FindTextBox)));
            }
            if (FindField == "Kraj")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Kraj != null && item.Knt_Kraj.StartsWith(FindTextBox)));
            }
            if (FindField == "Wojewodztwo")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Wojewodztwo != null && item.Knt_Wojewodztwo.StartsWith(FindTextBox)));
            }
            if (FindField == "Miasto")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Miasto != null && item.Knt_Miasto.StartsWith(FindTextBox)));
            }
            if (FindField == "Nip")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Nip != null && item.Knt_Nip.StartsWith(FindTextBox)));
            }
            if (FindField == "Regon")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Regon != null && item.Knt_Regon.StartsWith(FindTextBox)));
            }
            if (FindField == "Pesel")
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_Pesel != null && item.Knt_Pesel.StartsWith(FindTextBox)));
            }
            if (CzyDataSprzedazy)
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_DataWprowadzenia >= DataSprzedazyOd && item.Knt_DataWprowadzenia <= DataSprzedazyDo));
            }
            if (CzyDataWystawienia)
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => item.Knt_dataZmiany >= DataWystawieniaOd && item.Knt_dataZmiany <= DataWystawieniaDo));
            }
            if (CzyNumer)
            {
                List = new ObservableCollection<Kontrahenci>(List.Where(item => Convert.ToInt32(item.Knt_Kod) >= Convert.ToInt32(NumerOd) && Convert.ToInt32(item.Knt_Kod) <= Convert.ToInt32(NumerDo)));
            }
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa", "Kraj", "Wojewodztwo", "Miasto", "Nip", "Regon", "Pesel" };
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Kod", "Kraj", "Wojewodztwo", "Miasto","Nip"  };
        }
        public override void sort()
        {
            if (SortList == "Kod")
            {
                List = new ObservableCollection<Kontrahenci>(List.OrderBy(item => item.Knt_Kod));
            }
            if (SortList == "Kraj")
            {
                List = new ObservableCollection<Kontrahenci>(List.OrderBy(item => item.Knt_Kraj));
            }
            if (SortList == "Wojewodztwo")
            {
                List = new ObservableCollection<Kontrahenci>(List.OrderBy(item => item.Knt_Wojewodztwo));
            }
            if (SortList == "Miasto")
            {
                List = new ObservableCollection<Kontrahenci>(List.OrderBy(item => item.Knt_Miasto));
            }
            if (SortList == "Nip")
            {
                List = new ObservableCollection<Kontrahenci>(List.OrderBy(item => item.Knt_Nip));
            }
        }
        public override void load()
        {
            
            List = new ObservableCollection<Kontrahenci>
                (
                   from kontrahent in minimarketEntities.Kontrahenci
                   where kontrahent.Knt_CzyAktywny == true
                   select kontrahent
                );
            
                     
        }

       
        #endregion



    }
}
