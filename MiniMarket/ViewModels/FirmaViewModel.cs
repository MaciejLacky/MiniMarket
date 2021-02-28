using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class FirmaViewModel : JedenViewModel<Firmy>
    {
        #region Constructor
        public FirmaViewModel()
        {
            base.DisplayName = "Firmy";
            item = new Firmy();
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

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Helpers
        public override void Save()
        {
            item.F_CzyAktywne = true;
            item.F_FRDataWprowadzenia = DateTime.Now;
            miniMarketEntities.Firmy.Add(item);
            miniMarketEntities.SaveChanges();
        }
        #endregion
    }
}
