using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using MiniMarket.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class KategorieViewModel : WszystkieViewModel<Kategorie>
    {
        #region Fields
        private bool _selectedMode;
        private Kategorie _WybranaKategoria;
        private int _wybranaKategoriaId;
        
        
        #endregion
        #region Constructor
        public KategorieViewModel(bool selectedMode) : base()
        {
            base.DisplayName = "Kategorie";
            this._selectedMode = selectedMode;
            
        }
        #endregion
        #region Properties
        
        public int WybranaKategoriaId
        {
            get
            {
                return _wybranaKategoriaId;
            }
            set
            {
                _wybranaKategoriaId = value;
            }
        }
        public Kategorie WybranaKategoria
        {
            get
            {
                return _WybranaKategoria;
            }
            set
            {
                if (_WybranaKategoria != value)
                {   //Po każdym kliknieciu w Kontrahenta ustawia się properties wybrany kontrahent
                    //wybrany kontrahent jest wysyłany messenegerem do fv i jest zamykany
                    _WybranaKategoria = value;
                   
                    if (_selectedMode == true)
                    {
                        Messenger.Default.Send(_WybranaKategoria);
                        OnRequestClose();
                    }


                }
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            if (WybranaKategoria != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var kategoria = db.Kategorie.FirstOrDefault(x => x.K_IGKId == WybranaKategoria.K_IGKId).IGK_Aktywna = false;
                db.SaveChanges();
                load();
            }
            
        }
        public override void edit()
        {
            KategorieViewModel id = new KategorieViewModel(false);
            if (WybranaKategoria != null)
            {
                id.WybranaKategoriaId = WybranaKategoria.K_IGKId;
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
            return new List<string> { "Numer dokumentu", "Kontrahent", "Kategoria", "Platnosc" };
        }
        public override void sort()
        {
            throw new NotImplementedException();
        }
        public override void load()
        {
            List = new ObservableCollection<Kategorie>(
            from kategoria in minimarketEntities.Kategorie
            where kategoria.IGK_Aktywna == true
            select kategoria
        );
            #endregion

        }

        
    }
}
