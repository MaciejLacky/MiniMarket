using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Helper;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using MiniMarket.Model.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public class NowyTowarViewModel : JedenViewModel<Towary>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowGrupyTowarowe;
        private BaseCommand _ShowKategorieZakupu;
        private BaseCommand _ShowKategorieSprzedazy;
        #endregion
        #region Constructor
        public NowyTowarViewModel()
        {
            base.DisplayName = "Nowy towar";
            item = new Towary();
            Messenger.Default.Register<GrupyTowarowe>(this, getWybranaGrupa);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoriaSpr);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoriaZak);

        }
        #endregion
        #region Command
        public ICommand ShowGrupyTowarowe
        {
            get
            {
                if (_ShowGrupyTowarowe == null)
                {

                    _ShowGrupyTowarowe = new BaseCommand(() => Messenger.Default.Send("GrupyAll"));
                }
                return _ShowGrupyTowarowe;
            }
          
        }
        public ICommand ShowKategorieZakupu
        {
            get
            {
                if (_ShowKategorieZakupu == null)
                {

                    _ShowKategorieZakupu = new BaseCommand(() => Messenger.Default.Send("KategorieZakupuAll"));
                }
                return _ShowKategorieZakupu;
            }
        }
        public ICommand ShowKategorieSprzedazy
        {
            get
            {
                if (_ShowKategorieSprzedazy == null)
                {

                    _ShowKategorieSprzedazy = new BaseCommand(() => Messenger.Default.Send("KategorieSprzedazyAll"));
                }
                return _ShowKategorieSprzedazy;
            }
        }
        #endregion


        #region Properties
        public string Kod
        {
            get
            {
                return item.Twr_Kod;
            }
            set
            {
                if (value == item.Twr_Kod)
                    return;
                item.Twr_Kod = value;
                base.OnPropertyChanged(() => Kod);
            }
        }
        public string NumerKatalogowy
        {
            get
            {
                return item.Twr_NumerKat;
            }
            set
            {
                if (value == item.Twr_NumerKat)
                    return;
                item.Twr_NumerKat = value;
                base.OnPropertyChanged(() => NumerKatalogowy);
            }
        }
        public string Typ
        {
            get
            {
                return item.Twr_Typ;
            }
            set
            {
                if (value == item.Twr_Typ)
                    return;
                item.Twr_Typ = value;
                base.OnPropertyChanged(() => Typ);
            }
        }
        public string Ean
        {
            get
            {
                return item.Twr_EAN;
            }
            set
            {
                if (value == item.Twr_EAN)
                    return;
                item.Twr_EAN = value;
                base.OnPropertyChanged(() => Ean);
            }
        }
        public string PKWiU
        {
            get
            {
                return item.Twr_PKWiU;
            }
            set
            {
                if (value == item.Twr_PKWiU)
                    return;
                item.Twr_PKWiU = value;
                base.OnPropertyChanged(() => PKWiU);
            }
        }
        public byte? StawkaVatSprzedazy
        {
            get
            {
                return item.Twr_VatSpr;
            }
            set
            {
                if (item.Twr_VatSpr != value)
                {
                    item.Twr_VatSpr = value;
                    base.OnPropertyChanged(() => StawkaVatSprzedazy);
                }
            }
        }
        public byte? StawkaVatZakupu
        {
            get
            {
                return item.Twr_VatZak;
            }
            set
            {
                if (item.Twr_VatZak != value)
                {
                    item.Twr_VatZak = value;
                    base.OnPropertyChanged(() => StawkaVatZakupu);
                   
                }
            }
        }
        public decimal? CenaDomyslna
        {
            get
            {
                return item.Twr_CenaDomysl;
            }
            set
            {
                if (item.Twr_CenaDomysl != value)
                {
                    item.Twr_CenaDomysl = value;
                    base.OnPropertyChanged(() => CenaDomyslna);
                }
            }
        }
        public string Nazwa
        {
            get
            {
                return item.Twr_Nazwa;
            }
            set
            {
                if (value == item.Twr_Nazwa)
                    return;
                item.Twr_Nazwa = value;
                base.OnPropertyChanged(() => Nazwa);
            }
        }
        private string _grupaNazwa;
        public string GrupaNazwa
        {
            get
            {
                return _grupaNazwa;
            }
            set
            {
                if (_grupaNazwa != value)
                {
                    _grupaNazwa = value;
                    base.OnPropertyChanged(() => GrupaNazwa);
                }
            }
        }
        public string JednostkaPodstawowa
        {
            get
            {
                return item.Twr_JednPodst;
            }
            set
            {
                if (value == item.Twr_JednPodst)
                    return;
                item.Twr_JednPodst = value;
                base.OnPropertyChanged(() => JednostkaPodstawowa);
            }
        }
        public string JednostkaPomocnicza
        {
            get
            {
                return item.Twr_JednPomoc;
            }
            set
            {
                if (value == item.Twr_JednPomoc)
                    return;
                item.Twr_JednPomoc = value;
                base.OnPropertyChanged(() => JednostkaPomocnicza);
            }
        }
        public decimal? CenaZakupuNetto
        {
            get
            {
                return item.Twr_CenaZakNetto;
            }
            set
            {
                if (item.Twr_CenaZakNetto != value)
                {
                    item.Twr_CenaZakNetto = value;
                    base.OnPropertyChanged(() => CenaZakupuNetto);
                    base.OnPropertyChanged(() => CenaZakupuBrutto);
                }
            }
        }
        public decimal? CenaZakupuBrutto
        {
            get
            {
                if (CenaZakupuNetto != null)
                {
                    return item.Twr_CenaZakBrutto = CenaZakupuNetto * ( IdVatZakup== null ? (VatZakupComboBoxItem.First().VatZak_Wartosc/100 +1) : VatZakupComboBoxItem.First(x=>x.VatZak_Id == IdVatZakup).VatZak_Wartosc / 100m + 1m);
                }
                return item.Twr_CenaZakBrutto;
            }
            set
            {
                if (item.Twr_CenaZakBrutto != value)
                {
                    item.Twr_CenaZakBrutto = value;
                    base.OnPropertyChanged(() => CenaZakupuBrutto);
                }
            }


        }
        public decimal? CenaSprzedazyNetto
        {
            get
            {
                if (CenaZakupuNetto != null)
                {
                    return item.Twr_CenaSprNetto = CenaZakupuNetto * (Marza / 100m + 1m);
                }
                return item.Twr_CenaSprNetto;


            }
            set
            {
                if (item.Twr_CenaSprNetto != value)
                {
                    item.Twr_CenaSprNetto = value;
                    base.OnPropertyChanged(() => CenaSprzedazyNetto);
                    base.OnPropertyChanged(() => CenaSprzedazyBrutto);
                }
            }
        }
        public decimal? CenaSprzedazyBrutto
        {
            get
            {
                if (CenaZakupuNetto != null)
                {
                    return item.Twr_CenaSprBrutto = CenaZakupuNetto * (Marza / 100m + 1m) * ( IdVatSprzedaz == null ? (VatSprzedazComboBoxItem.First().VatSpr_Wartosc/100m+1m): VatSprzedazComboBoxItem.First(x => x.VatSpr_Id == IdVatSprzedaz).VatSpr_Wartosc / 100m + 1m);
                }
                return item.Twr_CenaSprBrutto;
                
            }
            set
            {
                if (item.Twr_CenaSprBrutto != value)
                {
                    item.Twr_CenaSprBrutto = value;
                    base.OnPropertyChanged(() => CenaSprzedazyBrutto);
                }
            }

        }
        public decimal? Marza
        {
            get
            {
                return item.Twr_Marza;
            }
            set
            {
                if (item.Twr_Marza != value)
                {
                    item.Twr_Marza = value;
                    base.OnPropertyChanged(() => Marza);
                    base.OnPropertyChanged(() => CenaSprzedazyNetto);
                    base.OnPropertyChanged(() => CenaSprzedazyBrutto);
                }
            }
        }
        public string Waluta
        {
            get
            {
                return item.Twr_Waluta;
            }
            set
            {
                if (value == item.Twr_Waluta)
                    return;
                item.Twr_Waluta = value;
                base.OnPropertyChanged(() => Waluta);
            }
        }
        public int? IdKategoriiSpr
        {
            get
            {
                return item.Twr_IdKategoria;
            }
            set
            {
                if (value == item.Twr_IdKategoria)
                    return;
                item.Twr_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategoriiSpr);
            }
        }
        public int? IdKategoriiZak
        {
            get
            {
                return item.Twr_IdKategoria;
            }
            set
            {
                if (value == item.Twr_IdKategoria)
                    return;
                item.Twr_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategoriiZak);
               
            }
        }
        public int? IdVatSprzedaz
        {
            get
            {
                return item.Twr_IdVatSprzedaz;
            }
            set
            {
                if (value == item.Twr_IdVatSprzedaz)
                    return;
                item.Twr_IdVatSprzedaz = value;
                base.OnPropertyChanged(() => IdVatSprzedaz);
                base.OnPropertyChanged(() => CenaSprzedazyBrutto);
            }
        }
        public int? IdVatZakup
        {
            get
            {
                return item.Twr_IdVatZakup;
            }
            set
            {
                if (value == item.Twr_IdVatZakup)
                    return;
                item.Twr_IdVatZakup = value;
                base.OnPropertyChanged(() => IdVatZakup);
                base.OnPropertyChanged(() => CenaZakupuBrutto);
                
            }
        }
        public int? IdCenaDomyslna
        {
            get
            {
                return item.Twr_CenaDomyslna;
            }
            set
            {
                if (value == item.Twr_CenaDomyslna)
                    return;
                item.Twr_CenaDomyslna = value;
                base.OnPropertyChanged(() => IdCenaDomyslna);
            }
        }
        public int? IdTypTowaru
        {
            get
            {
                return item.Twr_IdTypTowaru;
            }
            set
            {
                if (value == item.Twr_IdTypTowaru)
                    return;
                item.Twr_IdTypTowaru = value;
                base.OnPropertyChanged(() => IdTypTowaru);
            }
        }
        public int? IdGrupa
        {
            get
            {
                return item.Twr_IdGrupa;
            }
            set
            {
                if (value == item.Twr_IdGrupa)
                    return;
                item.Twr_IdGrupa = value;
                base.OnPropertyChanged(() => IdGrupa);
            }
        }
        public int? IdJednostkaPodstawowa
        {
            get
            {
                return item.Twr_IdJednostkaPodstawowa;
            }
            set
            {
                if (value == item.Twr_IdJednostkaPodstawowa)
                    return;
                item.Twr_IdJednostkaPodstawowa = value;
                base.OnPropertyChanged(() => IdJednostkaPodstawowa);
            }
        }
        public int? IdJednostkaPomocnicza
        {
            get
            {
                return item.Twr_IdJednostkaPomocnicza;
            }
            set
            {
                if (value == item.Twr_IdJednostkaPomocnicza)
                    return;
                item.Twr_IdJednostkaPomocnicza = value;
                base.OnPropertyChanged(() => IdJednostkaPomocnicza);
            }
        }

        public IQueryable<Kategorie> KategorieComboBoxItem
        {
            get
            {
                return
                    (
                       
                        from kategoria in miniMarketEntities.Kategorie
                        where kategoria.IGK_Aktywna == true
                        select kategoria
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<VatSprzedaz> VatSprzedazComboBoxItem
        {
            get
            {
                return
                    (
                        
                        from vatspr in miniMarketEntities.VatSprzedaz
                        
                        select vatspr
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<VatZakup> VatZakupComboBoxItem
        {
            get
            {
                return
                    (
                       
                        from vatzak in miniMarketEntities.VatZakup

                        select vatzak
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<CenaDomyslna> CenaDomyslnaComboBoxItem
        {
            get
            {
                return
                    (
                       
                        from cena in miniMarketEntities.CenaDomyslna

                        select cena
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<TypTowaru> TypTowaruComboBoxItem
        {
            get
            {
                return
                    (
                        
                        from typ in miniMarketEntities.TypTowaru
                        select typ
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<GrupyTowarowe> GrupaComboBoxItem
        {
            get
            {
                return
                    (                       
                        from grupa in miniMarketEntities.GrupyTowarowe
                        where grupa.G_CzyAktywne == true
                        select grupa
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<JednostkaPodstawowa> JednostkaPodstawowaComboBoxItem
        {
            get
            {
                return
                    (
                        from jed in miniMarketEntities.JednostkaPodstawowa
                        select jed
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<JednostkaPomocnicza> JednostkaPomocniczaComboBoxItem
        {
            get
            {
                return
                    (
                        from jed in miniMarketEntities.JednostkaPomocnicza
                        select jed
                    ).ToList().AsQueryable();
            }
        }


        #endregion
        #region Helpers
        public override void Save()
        {
            item.Twr_CzyAktywne = true;
            item.Twr_WprUzytData = DateTime.Now;
            
            miniMarketEntities.Towary.Add(item);
            miniMarketEntities.SaveChanges();
        }
        private void getWybranaGrupa(GrupyTowarowe grupa)
        {
            IdGrupa = grupa.G_GRId;
            GrupaNazwa = grupa.G_GRNazwa;
            
        }
        private void getWybranaKategoriaSpr(Kategorie kategoria)
        {
            IdKategoriiSpr = kategoria.K_IGKId;
        }
        private void getWybranaKategoriaZak(Kategorie kategoria)
        {
            IdKategoriiZak = kategoria.K_IGKId;
        }
        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            
            base.OnPropertyChanged(() => CenaSprzedazyBrutto);
            base.OnPropertyChanged(() => CenaZakupuBrutto);
        }
        #endregion
        #region Validations
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
                if (name == "Nazwa")
                {
                    komunikat = StringValidator.CzyDuzaLitera(this.Nazwa);
                }
                if (name == "Kod")
                {
                    komunikat = StringValidator.CzyPuste(this.Kod);
                }
                if (name == "CenaZakupuNetto")
                {
                    komunikat = StringValidator.CzyLiczbaDodatnia(this.CenaZakupuNetto);
                }
                

                return komunikat;
            }

        }
        public override bool IsValid()
        {
            if (this["Nazwa"] == null && this["Kod"] == null && this["CenaZakupuNetto"] == null )
            {
                return true;
            }
            return false;

        }

       
        #endregion

    }
}
