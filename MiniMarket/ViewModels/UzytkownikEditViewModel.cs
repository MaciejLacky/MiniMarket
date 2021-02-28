using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class UzytkownikEditViewModel: JedenViewModel<Uzytkownicy>
    {
        private int _WybraneId { get; set; }
        #region Constructor
        public UzytkownikEditViewModel(int wybraneId)
        {
            this._WybraneId = wybraneId;
            base.DisplayName = "Użytkownicy";
            item = miniMarketEntities.Uzytkownicy.Where(x => x.U_UzytkownikId == wybraneId).FirstOrDefault();
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
        #endregion
        #region Helpers
        public override void Save()
        {
            item.U_UZCzyAktywna = true;
            item.U_UZDataZmiany = DateTime.Now;
            miniMarketEntities.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == _WybraneId).U_UZNazwa = Nazwa;
            miniMarketEntities.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == _WybraneId).U_UZStanowisko = Stanowisko;
            miniMarketEntities.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == _WybraneId).U_UZTelefon = Telefon;
            miniMarketEntities.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == _WybraneId).U_UZCzyManager = Manager;
            miniMarketEntities.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == _WybraneId).U_UZCzyPracownik = Pracownik;
            miniMarketEntities.Uzytkownicy.FirstOrDefault(x => x.U_UzytkownikId == _WybraneId).U_UZCzyAdmin = Admin;
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
