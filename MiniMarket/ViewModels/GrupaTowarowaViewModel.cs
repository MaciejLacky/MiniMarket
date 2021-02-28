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
    public class GrupaTowarowaViewModel : JedenViewModel<GrupyTowarowe>, IDataErrorInfo
    {
        #region Constructor
        public GrupaTowarowaViewModel()
        {
            base.DisplayName = "Grupa";
            item = new GrupyTowarowe();
        }
        #endregion
        #region Properties
        public string Nazwa
        {
            get
            {
                return item.G_GRNazwa;
            }
            set
            {
                if (value == item.G_GRNazwa)
                    return;
                item.G_GRNazwa = value;              
                base.OnPropertyChanged(() => Nazwa);
            }
        }
        #endregion
        #region Helpers
        public override void Save()
        {
          
            
            item.G_GRDataWprowadzenia = DateTime.Today;                     
            item.G_CzyAktywne = true;
            miniMarketEntities.GrupyTowarowe.Add(item);
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
            if (this["Nazwa"] == null )
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
