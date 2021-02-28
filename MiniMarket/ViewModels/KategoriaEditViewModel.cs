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
    public class KategoriaEditViewModel : JedenViewModel<Kategorie>, IDataErrorInfo
    {
        #region Fields
        private int _WybraneId { get; set; }
        #endregion
        #region Constructor
        public KategoriaEditViewModel(int kategoriaId)
        {
            base.DisplayName = "Kategoria";
            item = miniMarketEntities.Kategorie.Where(x => x.K_IGKId == kategoriaId).FirstOrDefault();
            this._WybraneId = kategoriaId;

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
            item.K_DataZmiany = DateTime.Now;
            item.IGK_Aktywna = true;
            miniMarketEntities.Kategorie.FirstOrDefault(x => x.K_IGKId == _WybraneId).K_Nazwa = Nazwa;
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
