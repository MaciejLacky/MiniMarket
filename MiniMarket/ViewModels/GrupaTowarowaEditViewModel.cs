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
    public class GrupaTowarowaEditViewModel : JedenViewModel<GrupyTowarowe>, IDataErrorInfo
    {
        #region Fields
        private int _WybraneId { get; set; }
        #endregion
        #region Constructor
        public GrupaTowarowaEditViewModel(int grupaId)
        {
            base.DisplayName = "Grupa";
            this._WybraneId = grupaId;
            item = miniMarketEntities.GrupyTowarowe.Where(x => x.G_GRId == grupaId).FirstOrDefault();
           
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



            item.G_GRDataZmiany = DateTime.Today;
            item.G_CzyAktywne = true;
            miniMarketEntities.GrupyTowarowe.FirstOrDefault(x => x.G_GRId == _WybraneId).G_GRNazwa = Nazwa;
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
