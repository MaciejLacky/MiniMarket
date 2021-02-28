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
    public class FirmyViewModel : WszystkieViewModel<Firmy>
    {
        #region Fields
        private Firmy _WybranaFirma;
        #endregion
        #region Constructor
        public FirmyViewModel() : base()
        {
            base.DisplayName = "Spis firm";
        }
        #endregion
        #region Properties
        public Firmy WybranaFirma
        {
            get
            {
                return _WybranaFirma;
            }
            set
            {
                _WybranaFirma = value;
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranaFirma != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var fv = db.Firmy.FirstOrDefault(x => x.F_FirmaId == WybranaFirma.F_FirmaId);
                fv.F_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
            
        }

        public override void edit()
        {
            if (WybranaFirma != null)
            {

                Messenger.Default.Send(WybranaFirma);
            }
        }

        public override void find()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public override void load()
        {

            List = new ObservableCollection<Firmy>
                (
                   from firma in minimarketEntities.Firmy
                   where firma.F_CzyAktywne == true
                   select firma
                );
        }

        
        #endregion


    }
}
