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
    public class GrupyTowaroweViewModel : WszystkieViewModel<GrupyTowarowe>
    {
        #region Fields
        private bool _selectedMode;
        private GrupyTowarowe _WybranaGrupa;
        private int _wybraneId;
        #endregion
        public GrupyTowaroweViewModel(bool selectedMode):base()
        {
            base.DisplayName = "Grupy towarowe";
            this._selectedMode = selectedMode;
        }
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
        public GrupyTowarowe WybranaGrupa
        {
            get
            {
                return _WybranaGrupa;
            }
            set
            {
                if (_WybranaGrupa != value)
                {   //Po każdym kliknieciu w Kontrahenta ustawia się properties wybrany kontrahent
                    //wybrany kontrahent jest wysyłany messenegerem do fv i jest zamykany
                    _WybranaGrupa = value;
                    
                    if (_selectedMode == true)
                    {
                        Messenger.Default.Send(_WybranaGrupa);
                        OnRequestClose();
                    }


                }
            }
        }
        #endregion

        public override void delete()
        {
            if (WybranaGrupa != null)
            {
                var db = new MiniMarketEntities();
                var grupa = db.GrupyTowarowe.FirstOrDefault(m => m.G_GRId == WybranaGrupa.G_GRId);
                grupa.G_CzyAktywne = false;
                db.SaveChanges();
                load();
            }
           
        }

        public override void edit()
        {
            GrupyTowaroweViewModel id = new GrupyTowaroweViewModel(false);
            if (WybranaGrupa != null)
            {
                id.WybraneId = WybranaGrupa.G_GRId;
                Messenger.Default.Send(id);
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
            List = new ObservableCollection<GrupyTowarowe>
                ( 
                from grupa in minimarketEntities.GrupyTowarowe
                where grupa.G_CzyAktywne == true
                select grupa 
                );
        }

        
    }
}
