using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Helper;
using MiniMarket.Model.Entities;
using MiniMarket.Model.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public class KontrahentEditViewModel : JedenViewModel<Kontrahenci>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKategorie;
        
        private int _WybraneId { get; set; }
        private bool radioButtonOsoba = true;
        private bool radioButtonFirma = false;
        #endregion

        #region Construktor
        public KontrahentEditViewModel(int wybraneId)
        {
            base.DisplayName = "Kontrahent";
            this._WybraneId = wybraneId;
            item = miniMarketEntities.Kontrahenci.Where(x => x.Knt_KntId == wybraneId).FirstOrDefault();
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
        }
        #endregion
        #region Properties
        //zmienić properties na takie jak w przykładzie 
        public bool RadioButtonOsoba
        {
            get { return radioButtonOsoba; }
            set { radioButtonOsoba = value; }
        }
        public bool RadioButtonFirma
        {
            get { return radioButtonFirma; }
            set { radioButtonFirma = value; }
        }

        public string Kod
        {
            get
            {
                return item.Knt_Kod;
            }
            set
            {
                if (value == item.Knt_Kod)
                    return;
                item.Knt_Kod = value;
                base.OnPropertyChanged(() => Kod);
            }
        }
        public string Nazwa
        {
            get
            {
                return item.Knt_Nazwa1;
            }
            set
            {
                if (value == item.Knt_Nazwa1)
                    return;
                item.Knt_Nazwa1 = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }
        public string Regon
        {
            get
            {
                return item.Knt_Regon;
            }
            set
            {
                if (value == item.Knt_Regon)
                    return;
                item.Knt_Regon = value;
                base.OnPropertyChanged(() => Regon);
            }
        }
        public string Nip
        {
            get
            {
                return item.Knt_NipKraj;
            }
            set
            {
                if (value == item.Knt_NipKraj)
                    return;
                item.Knt_NipKraj = value;
                base.OnPropertyChanged(() => Nip);
            }
        }
        public string Pesel
        {
            get
            {
                return item.Knt_Pesel;
            }
            set
            {
                if (value == item.Knt_Pesel)
                    return;
                item.Knt_Pesel = value;
                base.OnPropertyChanged(() => Pesel);
            }
        }
        public string Kraj
        {
            get
            {
                return item.Knt_Kraj;
            }
            set
            {
                if (value == item.Knt_Kraj)
                    return;
                item.Knt_Kraj = value;
                base.OnPropertyChanged(() => Kraj);
            }
        }
        public string Miasto
        {
            get
            {
                return item.Knt_Miasto;
            }
            set
            {
                if (value == item.Knt_Miasto)
                    return;
                item.Knt_Miasto = value;
                base.OnPropertyChanged(() => Miasto);
            }
        }
        public string Poczta
        {
            get
            {
                return item.Knt_Poczta;
            }
            set
            {
                if (value == item.Knt_Poczta)
                    return;
                item.Knt_Poczta = value;
                base.OnPropertyChanged(() => Poczta);
            }
        }
        public string Ulica
        {
            get
            {
                return item.Knt_Ulica;
            }
            set
            {
                if (value == item.Knt_Ulica)
                    return;
                item.Knt_Ulica = value;
                base.OnPropertyChanged(() => Ulica);
            }
        }
        public string NrDomu
        {
            get
            {
                return item.Knt_NrDomu;
            }
            set
            {
                if (value == item.Knt_NrDomu)
                    return;
                item.Knt_NrDomu = value;
                base.OnPropertyChanged(() => NrDomu);
            }
        }
        public string NrLokalu
        {
            get
            {
                return item.Knt_NrLokalu;
            }
            set
            {
                if (value == item.Knt_NrLokalu)
                    return;
                item.Knt_NrLokalu = value;
                base.OnPropertyChanged(() => NrLokalu);
            }
        }
        public string KrajIso
        {
            get
            {
                return item.Knt_KrajISO;
            }
            set
            {
                if (value == item.Knt_KrajISO)
                    return;
                item.Knt_KrajISO = value;
                base.OnPropertyChanged(() => KrajIso);
            }
        }
        public string Wojewodztwo
        {
            get
            {
                return item.Knt_Wojewodztwo;
            }
            set
            {
                if (value == item.Knt_Wojewodztwo)
                    return;
                item.Knt_Wojewodztwo = value;
                base.OnPropertyChanged(() => Wojewodztwo);
            }
        }
        public string KodPocztowy
        {
            get
            {
                return item.Knt_KodPocztowy;
            }
            set
            {
                if (value == item.Knt_KodPocztowy)
                    return;
                item.Knt_KodPocztowy = value;
                base.OnPropertyChanged(() => KodPocztowy);
            }
        }
        public string Powiat
        {
            get
            {
                return item.Knt_Powiat;
            }
            set
            {
                if (value == item.Knt_Powiat)
                    return;
                item.Knt_Powiat = value;
                base.OnPropertyChanged(() => Powiat);
            }
        }
        public string Telefon
        {
            get
            {
                return item.Knt_Telefon1;
            }
            set
            {
                if (value == item.Knt_Telefon1)
                    return;
                item.Knt_Telefon1 = value;
                base.OnPropertyChanged(() => Telefon);
            }
        }
        public string Telefon1
        {
            get
            {
                return item.Knt_Telefon2;
            }
            set
            {
                if (value == item.Knt_Telefon2)
                    return;
                item.Knt_Telefon2 = value;
                base.OnPropertyChanged(() => Telefon1);
            }
        }
        public string TelefonSms
        {
            get
            {
                return item.Knt_TelefonSms;
            }
            set
            {
                if (value == item.Knt_TelefonSms)
                    return;
                item.Knt_TelefonSms = value;
                base.OnPropertyChanged(() => TelefonSms);
            }
        }
        public string Fax
        {
            get
            {
                return item.Knt_Fax;
            }
            set
            {
                if (value == item.Knt_Fax)
                    return;
                item.Knt_Fax = value;
                base.OnPropertyChanged(() => Fax);
            }
        }
        public string Email
        {
            get
            {
                return item.Knt_Email;
            }
            set
            {
                if (value == item.Knt_Email)
                    return;
                item.Knt_Email = value;
                base.OnPropertyChanged(() => Email);
            }
        }
        public string Url
        {
            get
            {
                return item.Knt_URL;
            }
            set
            {
                if (value == item.Knt_URL)
                    return;
                item.Knt_URL = value;
                base.OnPropertyChanged(() => Url);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Knt_IdKategori;
            }
            set
            {
                if (value == item.Knt_IdKategori)
                    return;
                item.Knt_IdKategori = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public IQueryable<Kategorie> KategorieComboBoxItem
        {
            get
            {
                return
                    (
                        //zapytanie sposoby platnosci
                        from kategoria in miniMarketEntities.Kategorie
                        where kategoria.IGK_Aktywna == true
                        select kategoria
                    ).ToList().AsQueryable();
            }
        }
        #endregion
        #region Command
        public ICommand ShowKategorie
        {
            get
            {
                if (_ShowKategorie == null)
                {

                    _ShowKategorie = new BaseCommand(() => Messenger.Default.Send("KategorieAll"));
                }
                return _ShowKategorie;
            }

        }
        #endregion
        #region Helpers
        public override void Save()
        {
            item.Knt_dataZmiany = DateTime.Now;
            item.Knt_CzyAktywny = true;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Kod = Kod;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Nazwa1 = Nazwa;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Pesel = Pesel;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Regon = Regon;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Nip = Nip;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Kraj = Kraj;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Miasto = Miasto;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Poczta = Poczta;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Ulica = Ulica;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_NrDomu = NrDomu;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_NrLokalu = NrLokalu;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_KrajISO = KrajIso;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Wojewodztwo = Wojewodztwo;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Powiat = Powiat;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Telefon1 = Telefon;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Telefon2 = Telefon1;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_TelefonSms = TelefonSms;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_URL = Url;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Fax = Fax;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_Email = Email;
            miniMarketEntities.Kontrahenci.FirstOrDefault(x => x.Knt_KntId == _WybraneId).Knt_IdKategori = IdKategorii;
            miniMarketEntities.SaveChanges();
        }
        private void getWybranaKategoria(Kategorie kategoria)
        {
            IdKategorii = kategoria.K_IGKId;
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
                if (name == "Nip")
                {
                    komunikat = LogicValidator.Nip(this.Nip);
                }
                if (name == "Regon")
                {
                    komunikat = LogicValidator.REGON(this.Regon);
                }
                if (name == "Pesel")
                {
                    komunikat = LogicValidator.PESEL(this.Pesel);
                }

                return komunikat;
            }

        }
        public override bool IsValid()
        {
            if (RadioButtonFirma == true)
            {
                if (this["Nazwa"] == null && this["Nip"] == null && this["Regon"] == null && this["Pesel"] == null)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (this["Nazwa"] == null && this["Pesel"] == null)
                {
                    return true;
                }
                return false;
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
    }
}
