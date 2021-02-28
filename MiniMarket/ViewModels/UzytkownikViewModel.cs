using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class UzytkownikViewModel : JedenViewModel<Uzytkownicy>
    {
        #region Constructor
        public UzytkownikViewModel()
        {
            base.DisplayName = "Użytkownicy";
            item = new Uzytkownicy();
        }
        #endregion
        #region Properties
        public string Nazwa
        {
            get
            {
                return item.U_UZNazwa;
            }
            set
            {
                if (value == item.U_UZNazwa)
                    return;
                item.U_UZNazwa = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }
        public string Telefon
        {
            get
            {
                return item.U_UZTelefon;
            }
            set
            {
                if (value == item.U_UZTelefon)
                    return;
                item.U_UZTelefon = value;
                base.OnPropertyChanged(() => Telefon);
            }
        }
        public string Stanowisko
        {
            get
            {
                return item.U_UZStanowisko;
            }
            set
            {
                if (value == item.U_UZStanowisko)
                    return;
                item.U_UZStanowisko = value;
                base.OnPropertyChanged(() => Stanowisko);
            }
        }
        public bool? Admin
        {
            get
            {
                return item.U_UZCzyAdmin;
            }
            set
            {
                if (value == item.U_UZCzyAdmin)
                    return;
                item.U_UZCzyAdmin = value;
                base.OnPropertyChanged(() => Admin);
            }
        }
        public bool? Manager
        {
            get
            {
                return item.U_UZCzyManager;
            }
            set
            {
                if (value == item.U_UZCzyManager)
                    return;
                item.U_UZCzyManager = value;
                base.OnPropertyChanged(() => Manager);
            }
        }
        public bool? Pracownik
        {
            get
            {
                return item.U_UZCzyPracownik;
            }
            set
            {
                if (value == item.U_UZCzyPracownik)
                    return;
                item.U_UZCzyPracownik = value;
                base.OnPropertyChanged(() => Pracownik);
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
            item.U_UZCzyAktywna = true;
            item.U_UZDataWprowadzenia = DateTime.Now;
            miniMarketEntities.Uzytkownicy.Add(item);
            miniMarketEntities.SaveChanges();
        }
        #endregion
    }
}
