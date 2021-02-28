using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Helper;
using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        //polaczenie  z  baza  danych
        protected readonly MiniMarketEntities minimarketEntities;
        private BaseCommand _LoadCommand;
        private BaseCommand _AddCommand;
        private BaseCommand _UpdateCommand;
        private BaseCommand _DeleteCommand;
        private BaseCommand _EditCommand;
        private BaseCommand _SortCommand;
        private BaseCommand _FindCommand;
        private BaseCommand _SearchCommand;
        public string FindField { get; set; }
        public string SortList { get; set; }
        public DateTime DataSprzedazyOd { get; set; }
        public DateTime DataSprzedazyDo { get; set; }
        public DateTime DataWystawieniaOd { get; set; }
        public DateTime DataWystawieniaDo { get; set; }
        public string NumerOd { get; set; }
        public string NumerDo { get; set; }
        public bool CzyNumer { get; set; }
        public bool CzyDataWystawienia { get; set; }
        public bool CzyDataSprzedazy { get; set; }

        //lista  towarow  zaladowana  z  bazy  danych
        private ObservableCollection<T> _List;

        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null)
                {
                    _SearchCommand = new BaseCommand(() => find());
                }
                return _SearchCommand;
            }
        }
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                {
                    _SortCommand = new BaseCommand(() => sort());
                }
                return _SortCommand;
            }
        }
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                {
                    _FindCommand = new BaseCommand(() => find());
                }
                return _FindCommand;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new BaseCommand(() => edit());
                }
                return _EditCommand;
            }
        }
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => add());
                }
                return _AddCommand;
            }
        }
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => load());
                }
                return _LoadCommand;
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                if (_UpdateCommand == null)
                {
                    _UpdateCommand = new BaseCommand(() => load());
                }
                return _UpdateCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => delete());
                }
                return _DeleteCommand;
            }
        }
        public ObservableCollection<T> List
        {
            get
            {

                if (_List == null) load();
                return _List;

            }
            set
            {

                _List = value; OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Constructor
        public WszystkieViewModel()
        {

            this.minimarketEntities = new MiniMarketEntities();
            DataSprzedazyDo = DateTime.Now;
            DataSprzedazyOd = DateTime.Now;
            DataWystawieniaDo = DateTime.Now;
            DataWystawieniaOd = DateTime.Now;
        }
        #endregion
        #region Properties
        
        public List<string> SortComboboxItems
        {
            get
            {
                return GetComboboxSortList();
            }
        }
        
        public string FindTextBox { get; set; }
        public List<string> FindComboboxItems
        {
            get
            {
                return GetComboboxFindList();
            }
        }
        #endregion
        #region Helpers
        public abstract void sort();
        public abstract List<string> GetComboboxSortList();
        public abstract void find();
        public abstract List<string> GetComboboxFindList();
        public abstract void load();
        public abstract void edit();
        
        public abstract void delete();
        private void add()
        {
            //zadaniem tej funkcji będzie wywołanie okna do dodawania obiektu
            //messengerem z MVVmLight wyślemy komunikat do MainWindowViewModel któe ma wywołać
            Messenger.Default.Send(DisplayName + "Add");
        }

        #endregion

    }
}
