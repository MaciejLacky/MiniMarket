using MiniMarket.Helper;
using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public abstract class JedenViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        //połaczenie z baza danych
        protected MiniMarketEntities miniMarketEntities;
        //towar ktory bedziemy dodawac
        protected T item;
        //komenda do zapisu towaru przycisk nie może tego odrazy wywolac tylko komende ktora to wywoluje
        private BaseCommand _SaveCommand;
        private BaseCommand _RefreshCommand;
        private BaseCommand _DeleteCommand;

        #endregion
        #region Konstruktor
        public JedenViewModel() : base()
        {
            
            //polaczenie z baza danych
            miniMarketEntities = new MiniMarketEntities();
        }
        #endregion
        #region Properties
        // tu trzeba tworzyć properties w każdej klasie ktora przekazuje T

        #endregion
        #region Command
        //każda komenda ma tylko get
        // to jest komenda która zostanie podpięta pod przycisk zapisz
        // i ta komenda wywoła metode saveAndClose która jest niżej
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new BaseCommand(() => saveAndClose());
                }
                return _SaveCommand;
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = new BaseCommand(() => Refresh());
                }
                return _RefreshCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => Delete());
                }
                return _DeleteCommand;
            }
        }

        #region Validation
        public virtual bool IsValid()
        {
            return true;
        }
        #endregion

        

        #endregion
        #region Helpers
        //tu tworzymy abstrakcje bo robimy kod w klasie dziedziczącej override
        public abstract void Save();
        public abstract void Delete();
        public abstract void Refresh();
        private void saveAndClose()
        {
            
            if (IsValid())
            {
                Save();
                //zapisujemy do bazy

                // zamykamy okno
                base.OnRequestClose();
            }
            else
            {
                ShowMessageBox("Uzupełnij pola wymagane!");
            }
            
        }




        #endregion
    }
}
