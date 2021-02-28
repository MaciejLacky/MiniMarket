using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class UzytkownicyViewModel : WszystkieViewModel<Uzytkownicy>
    {

        #region Fields
        private Uzytkownicy _WybranyUzytkownik;
        #endregion
        #region Constructor
        public UzytkownicyViewModel() : base()
        {
            base.DisplayName = "Użytkownicy";
        }
        #endregion

        #region Properties
        public Uzytkownicy WybranyUzytkownik
        {
            get
            {
                return _WybranyUzytkownik;
            }
            set
            {
                _WybranyUzytkownik = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranyUzytkownik != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var fv = db.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == WybranyUzytkownik.U_UzytkownikId);
                fv.U_UZCzyAktywna = false;
                db.SaveChanges();
                load();
            }
        }

        public override void edit()
        {
            if (WybranyUzytkownik != null)
            {

                Messenger.Default.Send(WybranyUzytkownik);
            }
        }

        public override void find()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria", "Platnosc", "Magazyn" };
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria", "Platnosc" };
        }
        public override void sort()
        {
            throw new NotImplementedException();
        }
        public override void load()
        {
            List = new ObservableCollection<Uzytkownicy>(from uzytkownik in minimarketEntities.Uzytkownicy select uzytkownik);

        }

       
        #endregion

    }
}
