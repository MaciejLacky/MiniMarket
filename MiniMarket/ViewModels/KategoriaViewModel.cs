using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using MiniMarket.Model.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class KategoriaViewModel : JedenViewModel<Kategorie>, IDataErrorInfo
    {
        #region Constructor
        public KategoriaViewModel()
        {
            base.DisplayName = "Kategoria";
            item = new Kategorie();
            
        }
        #endregion
        #region Properties
        public string Nazwa
        {
            get
            {
                return item.K_Nazwa;
            }
            set
            {
                if (value == item.K_Nazwa)
                    return;
                item.K_Nazwa = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }

        public override void Save()
        {
            item.K_DataWprowadzenia = DateTime.Now;
            item.IGK_Aktywna = true;
            miniMarketEntities.Kategorie.Add(item);
            miniMarketEntities.SaveChanges();
        }
        #endregion
        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string komunikat = null;
                if (name == "Nazwa")
                {
                    komunikat = StringValidator.CzyPuste(this.Nazwa);
                }


                return komunikat;
            }

        }
        public override bool IsValid()
        {
            if (this["Nazwa"] == null)
            {
                return true;
            }
            return false;
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
