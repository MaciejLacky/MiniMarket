using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class FirmaEditViewModel: JedenViewModel<Firmy>
    {
        private int _WybraneId { get; set; }
        #region Constructor
        public FirmaEditViewModel(int wybraneId)
        {
            this._WybraneId = wybraneId;
            base.DisplayName = "Firmy";
            item = miniMarketEntities.Firmy.Where(x => x.F_FirmaId == wybraneId).FirstOrDefault();
        }
        #endregion
        #region Properties
        public string Nazwa
        {
            get
            {
                return item.F_FRNazwa;
            }
            set
            {
                if (value == item.F_FRNazwa)
                    return;
                item.F_FRNazwa = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }
        public string Adres
        {
            get
            {
                return item.F_FRAdres;
            }
            set
            {
                if (value == item.F_FRAdres)
                    return;
                item.F_FRAdres = value;
                base.OnPropertyChanged(() => Adres);
            }
        }
        public string Nip
        {
            get
            {
                return item.F_FRNIP;
            }
            set
            {
                if (value == item.F_FRNIP)
                    return;
                item.F_FRNIP = value;
                base.OnPropertyChanged(() => Nip);
            }
        }
        public string Regon
        {
            get
            {
                return item.F_FRREGON;
            }
            set
            {
                if (value == item.F_FRREGON)
                    return;
                item.F_FRREGON = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            item.F_CzyAktywne = true;
            item.F_FRDataZmiany = DateTime.Now;
            miniMarketEntities.Firmy.FirstOrDefault(x => x.F_FirmaId == _WybraneId).F_FRAdres = Adres;
            miniMarketEntities.Firmy.FirstOrDefault(x => x.F_FirmaId == _WybraneId).F_FRNazwa = Nazwa;
            miniMarketEntities.Firmy.FirstOrDefault(x => x.F_FirmaId == _WybraneId).F_FRNIP = Nip;
            miniMarketEntities.Firmy.FirstOrDefault(x => x.F_FirmaId == _WybraneId).F_FRREGON = Regon;
            
            miniMarketEntities.SaveChanges();
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
