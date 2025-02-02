﻿using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Helper;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using MiniMarket.Model.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public class FakturaSprzedazyViewModel : JedenViewModel<FakturySprzedazy>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;
        private FvZakupuPozycjeForView _wybranaPozycja;
        private ObservableCollection<FvZakupuPozycjeForView> _ListaPozycji;
        public ObservableCollection<FakturySprzedazyPozycje> ListaTowarow;
        #endregion
        #region Konstruktor
        public FakturaSprzedazyViewModel()
        {
            base.DisplayName = "FV sprzedaż";
            item = new FakturySprzedazy();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            _ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();
            ListaTowarow = new ObservableCollection<FakturySprzedazyPozycje>();
        }
        #endregion
        #region Commands
        public ICommand ShowKontrahenci
        {
            get
            {
                if (_ShowKontrahenci == null)
                {

                    _ShowKontrahenci = new BaseCommand(() => Messenger.Default.Send("KontrahenciAll"));
                }
                return _ShowKontrahenci;
            }

        }
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
        public ICommand ShowTowary
        {
            get
            {
                if (_ShowTowary == null)
                {

                    _ShowTowary = new BaseCommand(() => Messenger.Default.Send("TowaryAll"));
                }
                return _ShowTowary;
            }

        }
        #endregion
        #region Properties

        public FvZakupuPozycjeForView WybranaPozycja
        {
            get
            {
                return _wybranaPozycja;
            }
            set
            {
                _wybranaPozycja = value;
                if (_wybranaPozycja.Fvz_PozycjeIlosc != null)
                {
                    ListaTowarow.First(x => x.Fvs_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Fvs_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                   
                }
                if (_wybranaPozycja.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Fvs_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Fvs_PozycjeCenaNetto)
                {
                    ListaTowarow.First(x => x.Fvs_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Fvs_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
                }


                base.OnPropertyChanged(() => WybranaPozycja);
                
            }
        }
        public ObservableCollection<FvZakupuPozycjeForView> ListaPozycji
        {
            get
            {

                if (_ListaPozycji != null)
                {
                    loadPozycji();
                    
                }
                    
                return _ListaPozycji;

            }
            set
            {

                _ListaPozycji = value;
                
                
                    //OnPropertyChanged(() => ListaPozycji);
                

            }
        }
        public decimal? wartoscNetto = 0;
        public decimal? wartoscBrutto = 0;
        public decimal? WartoscNetto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscNetto += item.Fvs_PozycjeCenaNetto * item.Fvs_PozycjeIlosc;
                    }
                return wartoscNetto;
            }
            set
            {
                OnPropertyChanged(() => WartoscNetto);
                OnPropertyChanged(() => WartoscBrutto);
            }
        }
        public decimal? WartoscBrutto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscBrutto += item.Fvs_PozycjeCenaNetto * item.Fvs_PozycjeIlosc * (item.Fvs_PozycjeVatZakup / 100m + 1m);
                    }
                return wartoscBrutto;
            }
            set
            {

                OnPropertyChanged(() => WartoscBrutto);
            }
        }
        
        public string NumerDokumentu
        {
            get
            {
                return item.Fvs_NrDok;
            }
            set
            {
                if (value == item.Fvs_NrDok)
                    return;
                item.Fvs_NrDok = value;
                base.OnPropertyChanged(() => NumerDokumentu);
            }
        }
        public DateTime? DataWystawienia
        {
            get
            {
                return item.Fvs_DataWyst =  item.Fvs_DataWyst == null ? DateTime.Now : item.Fvs_DataWyst;
            }
            set
            {
                if (item.Fvs_DataWyst != value)
                {
                    item.Fvs_DataWyst = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime? DataSprzedazy
        {
            get
            {
                return item.FvsDataSprzedazy = item.FvsDataSprzedazy == null ? DateTime.Now : item.FvsDataSprzedazy;
            }
            set
            {
                if (item.FvsDataSprzedazy != value)
                {
                    item.FvsDataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public DateTime? DataOd
        {
            get
            {
                return item.FvsDataOd;
            }
            set
            {
                if (item.FvsDataOd != value)
                {
                    item.FvsDataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return item.FvsRabat;
            }
            set
            {
                if (item.FvsRabat != value)
                {
                    item.FvsRabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public DateTime? Termin
        {
            get
            {
                return item.Fvs_Termin =  item.Fvs_Termin == null ? DateTime.Now.AddDays(7)  : item.Fvs_Termin;
            }
            set
            {
                if (item.Fvs_Termin != value)
                {
                    item.Fvs_Termin = value;
                    base.OnPropertyChanged(() => Termin);
                }
            }
        }
        public int? IdMagazynu
        {
            get
            {
                return item.Fvs_IdMagazyn = item.Fvs_IdMagazyn == null ? 1 : item.Fvs_IdMagazyn;
            }
            set
            {
                if (value == item.Fvs_IdMagazyn)
                    return;
                item.Fvs_IdMagazyn = value;
                base.OnPropertyChanged(() => IdMagazynu);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Fvs_IdKategoria;
            }
            set
            {
                if (value == item.Fvs_IdKategoria)
                    return;
                item.Fvs_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public int? IdRodzajPlatnosci
        {
            get
            {
                return item.Fvs_IdRodzajPlatnosci = item.Fvs_IdRodzajPlatnosci == null ? 1 : item.Fvs_IdRodzajPlatnosci;
            }
            set
            {
                if (value == item.Fvs_IdRodzajPlatnosci)
                    return;
                item.Fvs_IdRodzajPlatnosci = value;
                base.OnPropertyChanged(() => IdRodzajPlatnosci);
            }
        }
        public int? IdRodzajDokumentu
        {
            get
            {
                return item.Fvs_IdRodzajDokumentu = item.Fvs_IdRodzajDokumentu == null ? 2 : item.Fvs_IdRodzajDokumentu;
            }
            set
            {
                if (value == item.Fvs_IdRodzajDokumentu)
                    return;
                item.Fvs_IdRodzajDokumentu = value;
                base.OnPropertyChanged(() => IdRodzajDokumentu);
            }
        }
        public int? IdDokNettoBrutto
        {
            get
            {
                return item.Fvs_IdDokNettoBrutto;
            }
            set
            {
                if (value == item.Fvs_IdDokNettoBrutto)
                    return;
                item.Fvs_IdDokNettoBrutto = value;
                base.OnPropertyChanged(() => IdDokNettoBrutto);
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.Fvs_IdKontrahent;
            }
            set
            {
                if (value == item.Fvs_IdKontrahent)
                    return;
                item.Fvs_IdKontrahent = value;
                base.OnPropertyChanged(() => IdKontrahenta);
            }
        }
        public int? IdPozycji
        {
            get
            {
                return item.Fvs_IdPozycji;
            }
            set
            {
                if (value == item.Fvs_IdPozycji)
                    return;
                item.Fvs_IdPozycji = value;
                base.OnPropertyChanged(() => IdPozycji);
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
        public IQueryable<Magazyny> MagazynyComboBoxItem
        {
            get
            {
                return
                    (

                        from mag in miniMarketEntities.Magazyny
                        where mag.M_CzyAktywne ==true
                        select mag
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<Kontrahenci> KontrahenciComboBoxItem
        {
            get
            {
                return
                    (

                        from kontrahent in miniMarketEntities.Kontrahenci
                        where kontrahent.Knt_CzyAktywny == true
                        select kontrahent
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<RodzajePlatnosci> RodzajePlatnosciComboBoxItem
        {
            get
            {
                return
                    (

                        from rodzaj in miniMarketEntities.RodzajePlatnosci
                        where rodzaj.RP_CzyAktywne == true
                        select rodzaj
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<RodzajeDokumentow> RodzajeDokumentowComboBoxItem
        {
            get
            {
                return
                    (

                        from rodzaj in miniMarketEntities.RodzajeDokumentow
                        where rodzaj.RD_CzyAktywne ==true
                        select rodzaj
                    ).ToList().AsQueryable();
            }
        }
        public IQueryable<DokumentNettoBrutto> DokumentLiczonyOdComboBoxItem
        {
            get
            {
                return
                    (

                        from dok in miniMarketEntities.DokumentNettoBrutto
                        where dok.CzyAktywne == true
                        select dok
                    ).ToList().AsQueryable();
            }
        }


        #endregion
        #region Helpers
        public override void Save()
        {
            item.CzyAktywne = true;
            item.Fvs_DataWprow = DateTime.Now;
            miniMarketEntities.FakturySprzedazy.Add(item);
            miniMarketEntities.SaveChanges();
            ObservableCollection<FakturySprzedazyPozycje> Poz = new ObservableCollection<FakturySprzedazyPozycje>();
            ObservableCollection<TowaryIlosci> Ilosci = new ObservableCollection<TowaryIlosci>();
           
            foreach (var items in ListaTowarow)
            {

                Poz.Add(new FakturySprzedazyPozycje
                {
                   
                    Fvs_PozycjeIdTowaru = items.Fvs_PozycjeIdTowaru,
                    Fvs_PozycjeJm = items.Fvs_PozycjeJm,
                    Fvs_PozycjeIlosc = items.Fvs_PozycjeIlosc,
                    Fvs_PozycjeCenaNetto = items.Fvs_PozycjeCenaNetto,
                    Fvs_PozycjeKod = items.Fvs_PozycjeKod,
                    Fvs_PozycjeVatSprzedaz = items.Fvs_PozycjeVatSprzedaz,
                    Fvs_PozycjeVatZakup = items.Fvs_PozycjeVatZakup,
                    Fvs_PozycjeNazwa = items.Fvs_PozycjeNazwa,
                    Fvs_Rabat = items.Fvs_Rabat,
                    Fvs_IdFvSprzedaz = item.Fvs_IdSprzedaz,
                    Fvs_PozycjeCzyAktywne = true
                });
                Ilosci.Add(new TowaryIlosci
                {
                    Twr_WprowData = DateTime.Now,
                    CzyAktywne = true,
                    Twr_IdFvSprzedaz = item.Fvs_IdSprzedaz,
                    Twr_SprzedazIlosc = items.Fvs_PozycjeIlosc,
                    Twr_IdTowaru = items.Fvs_PozycjeIdTowaru,
                    Twr_Wartosc = items.Fvs_PozycjeCenaNetto,
                    Twr_NumerDokumentu = item.Fvs_NrDok,
                    Twr_TypDokumentu = item.RodzajeDokumentow.RD_Nazwa



                });
            }

            foreach (var items in Poz)
            {
                miniMarketEntities.FakturySprzedazyPozycje.Add(items);

            }
            miniMarketEntities.SaveChanges();
            foreach (var items in Ilosci)
            {
                miniMarketEntities.TowaryIlosci.Add(items);

            }
            miniMarketEntities.SaveChanges();
        }
        private void getWybranaKategoria(Kategorie kategoria)
        {
            IdKategorii = kategoria.K_IGKId;
        }

        private void getWybranyKontrahent(Kontrahenci kontrahent)
        {
            IdKontrahenta = kontrahent.Knt_KntId;
        }
        private void getWybranyTowar(TowarForView towar)
        {
            var wybranyTowar = new FakturySprzedazyPozycje()
            {
                Fvs_PozycjeIdTowaru = towar.Twr_IdTowaru,
                Fvs_PozycjeJm = towar.Twr_JednPodst,
                Fvs_PozycjeNazwa = towar.Twr_Nazwa,
                Fvs_PozycjeKod = towar.Twr_Kod,
                Fvs_PozycjeVatSprzedaz = Convert.ToByte(towar.Twr_VatSpr),
                Fvs_PozycjeVatZakup = Convert.ToByte(towar.Twr_VatZak),
                Fvs_PozycjeCenaNetto = towar.Twr_CenaSprNetto,
            };

            ListaTowarow.Add(wybranyTowar);
        }
        public void loadPozycji()
        {
            ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>
            (
            from poz in ListaTowarow
            select new FvZakupuPozycjeForView
            {
                Fvz_PozycjeIdTowaru = poz.Fvs_PozycjeIdTowaru,
                Fvz_PozycjeKod = poz.Fvs_PozycjeKod,
                Fvz_PozycjeNazwa = poz.Fvs_PozycjeNazwa,
                Fvz_PozycjeJm = poz.Fvs_PozycjeJm,
                Fvz_PozycjeVatSprzedaz = poz.Fvs_PozycjeVatSprzedaz,
                Fvz_PozycjeVatZakup = poz.Fvs_PozycjeVatZakup,
                Fvz_PozycjeCenaNetto = poz.Fvs_PozycjeCenaNetto,
                Fvz_PozycjeIlosc = poz.Fvs_PozycjeIlosc,
                Fvz_PozycjeCenaBrutto = poz.Fvs_PozycjeCenaNetto * (poz.Fvs_PozycjeVatZakup / 100m + 1m),
                Fvz_Rabat = poz.Fvs_Rabat,
                Fvz_WartoscPozycjeCenaNetto = poz.Fvs_PozycjeIlosc * poz.Fvs_PozycjeCenaNetto,
                Fvz_WartoscPozycjeCenaBrutto = poz.Fvs_PozycjeIlosc * poz.Fvs_PozycjeCenaNetto * (poz.Fvs_PozycjeVatZakup / 100m + 1m)
            }
            );
        }
        public override void Delete()
        {
            int x = 0;
            int y = ListaTowarow.Count;
            for (int i = 0; i < y; i++)
            {
                if (ListaTowarow.FirstOrDefault().Fvs_PozycjeIdTowaru == WybranaPozycja.Fvz_PozycjeIdTowaru)
                {
                    ListaTowarow.RemoveAt(x);
                    
                    Refresh();
                    break;
                }
                x++;
            }
        }

        public override void Refresh()
        {
            OnPropertyChanged(() => ListaTowarow);
            OnPropertyChanged(() => ListaPozycji);
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
                if (name == "NumerDokumentu")
                {
                    komunikat = StringValidator.CzyPuste(this.NumerDokumentu);
                }
                if (name == "KontrahenciComboBoxItem")
                {
                    komunikat = StringValidator.CzyPusteCombo(this.KontrahenciComboBoxItem.FirstOrDefault().Knt_Nazwa1);
                }


                return komunikat;
            }

        }
        public override bool IsValid()
        {
            if (this["NumerDokumentu"] == null && this["KontrahenciComboBoxItem"] == null)
            {
                return true;
            }
            return false;
        }

        
        #endregion
    }
}
