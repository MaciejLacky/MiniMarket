using GalaSoft.MvvmLight.Messaging;
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
    public class ZamowienieEditViewModel : JedenViewModel<Zamowienia>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;
        private FvZakupuPozycjeForView _wybranaPozycja;
        private ObservableCollection<FvZakupuPozycjeForView> _ListaPozycji;
        public ObservableCollection<ZamowieniaPozycje> ListaTowarow;
        private int _WybraneId { get; set; }
        #endregion
        #region Constructor
        public ZamowienieEditViewModel(int wybraneId)
        {
            base.DisplayName = "Zamowienie";
            this._WybraneId = wybraneId;
            item = miniMarketEntities.Zamowienia.Where(x => x.Zam_IdZamowienia == wybraneId).FirstOrDefault();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();
            ListaTowarow = new ObservableCollection<ZamowieniaPozycje>();
            foreach (var item in miniMarketEntities.ZamowieniaPozycje)
            {
                if (item.Zam_IdZamowienia == wybraneId && item.Zam_PozycjeCzyAktywne == true)
                {
                    ListaTowarow.Add(item);
                }
            }
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
                    miniMarketEntities.ZamowieniaPozycje.First(x => x.Zam_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Zam_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                }
                if (_wybranaPozycja.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Zam_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Zam_PozycjeCenaNetto)
                {
                    miniMarketEntities.ZamowieniaPozycje.First(x => x.Zam_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Zam_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
                }

                base.OnPropertyChanged(() => WybranaPozycja);
            }
        }
        public ObservableCollection<FvZakupuPozycjeForView> ListaPozycji
        {
            get
            {

                if (_ListaPozycji != null) loadPozycji();
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
        public double WartoscNetto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscNetto += item.Zam_PozycjeCenaNetto * item.Zam_PozycjeIlosc;
                    }
                return Math.Round(Convert.ToDouble(wartoscNetto), 2);
            }
            set
            {
                OnPropertyChanged(() => WartoscNetto);
            }
        }
        public double WartoscBrutto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscBrutto += item.Zam_PozycjeCenaNetto * item.Zam_PozycjeIlosc * (item.Zam_PozycjeVatZakup / 100m + 1m);
                    }
                return Math.Round(Convert.ToDouble(wartoscBrutto), 2);
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
                return item.Zam_NrDok;
            }
            set
            {
                if (value == item.Zam_NrDok)
                    return;
                item.Zam_NrDok = value;
                base.OnPropertyChanged(() => NumerDokumentu);
            }
        }
        public DateTime? DataWystawienia
        {
            get
            {
                return item.Zam_DataWyst;
            }
            set
            {
                if (item.Zam_DataWyst != value)
                {
                    item.Zam_DataWyst = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime? DataSprzedazy
        {
            get
            {
                return item.Zam_DataSprzedazy;
            }
            set
            {
                if (item.Zam_DataSprzedazy != value)
                {
                    item.Zam_DataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public DateTime? DataOd
        {
            get
            {
                return item.Zam_DataOd;
            }
            set
            {
                if (item.Zam_DataOd != value)
                {
                    item.Zam_DataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return item.Zam_Rabat;
            }
            set
            {
                if (item.Zam_Rabat != value)
                {
                    item.Zam_Rabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public DateTime? Termin
        {
            get
            {
                return item.Zam_Termin;
            }
            set
            {
                if (item.Zam_Termin != value)
                {
                    item.Zam_Termin = value;
                    base.OnPropertyChanged(() => Termin);
                }
            }
        }
        public int? IdMagazynu
        {
            get
            {
                return item.Zam_IdMagazyn;
            }
            set
            {
                if (value == item.Zam_IdMagazyn)
                    return;
                item.Zam_IdMagazyn = value;
                base.OnPropertyChanged(() => IdMagazynu);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Zam_IdKategoria;
            }
            set
            {
                if (value == item.Zam_IdKategoria)
                    return;
                item.Zam_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public int? IdRodzajPlatnosci
        {
            get
            {
                return item.Zam_IdRodzajPlatnosci;
            }
            set
            {
                if (value == item.Zam_IdRodzajPlatnosci)
                    return;
                item.Zam_IdRodzajPlatnosci = value;
                base.OnPropertyChanged(() => IdRodzajPlatnosci);
            }
        }
        public int? IdRodzajDokumentu
        {
            get
            {
                return item.Zam_IdRodzajDokumentu;
            }
            set
            {
                if (value == item.Zam_IdRodzajDokumentu)
                    return;
                item.Zam_IdRodzajDokumentu = value;
                base.OnPropertyChanged(() => IdRodzajDokumentu);
            }
        }
        public int? IdDokNettoBrutto
        {
            get
            {
                return item.ZamIdDokumentNettoBrutto;
            }
            set
            {
                if (value == item.ZamIdDokumentNettoBrutto)
                    return;
                item.ZamIdDokumentNettoBrutto = value;
                base.OnPropertyChanged(() => IdDokNettoBrutto);
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.Zam_IdKontrahent;
            }
            set
            {
                if (value == item.Zam_IdKontrahent)
                    return;
                item.Zam_IdKontrahent = value;
                base.OnPropertyChanged(() => IdKontrahenta);
            }
        }
        public int? IdPozycji
        {
            get
            {
                return item.Zam_IdPozycji;
            }
            set
            {
                if (value == item.Zam_IdPozycji)
                    return;
                item.Zam_IdPozycji = value;
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
                        where mag.M_CzyAktywne == true
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
                        where kontrahent.Knt_CzyAktywny ==true
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
                        where rodzaj.RD_CzyAktywne == true
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
            item.Zam_CzyAktywne = true;
            item.Zam_DataZmian = DateTime.Now;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_NrDok = NumerDokumentu;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_DataSprzedazy = DataSprzedazy;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_DataOd = DataOd;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_DataWyst = DataWystawienia;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_Rabat = Rabat;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_Termin = Termin;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_IdMagazyn = IdMagazynu;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_IdKategoria = IdKategorii;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_IdKontrahent = IdKontrahenta;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_IdRodzajDokumentu = IdRodzajDokumentu;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).ZamIdDokumentNettoBrutto = IdDokNettoBrutto;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_IdKontrahentOdbiorca = IdKontrahenta;
            miniMarketEntities.Zamowienia.FirstOrDefault(x => x.Zam_IdZamowienia == _WybraneId).Zam_IdPozycji = IdPozycji;


            miniMarketEntities.SaveChanges();
            ObservableCollection<ZamowieniaPozycje> Poz = new ObservableCollection<ZamowieniaPozycje>();
            var OldList = miniMarketEntities.ZamowieniaPozycje.Where(x => x.Zam_IdZamowienia == _WybraneId);
            int liczbaTowarowWBazie = OldList.Count();
            int liczbaTowarowWLiscie = ListaTowarow.Count();
            int roznicaTowarow;
            if (liczbaTowarowWLiscie > liczbaTowarowWBazie)
            {
                roznicaTowarow = liczbaTowarowWLiscie - liczbaTowarowWBazie;
                foreach (var items in ListaTowarow.Reverse().Take(roznicaTowarow))
                {

                    Poz.Add(new ZamowieniaPozycje
                    {

                        Zam_PozycjeIdTowaru = items.Zam_PozycjeIdTowaru,
                        Zam_PozycjeJm = items.Zam_PozycjeJm,
                        Zam_PozycjeIlosc = items.Zam_PozycjeIlosc,
                        Zam_PozycjeCenaNetto = items.Zam_PozycjeCenaNetto,
                        Zam_PozycjeKod = items.Zam_PozycjeKod,
                        Zam_PozycjeVatSprzedaz = items.Zam_PozycjeVatSprzedaz,
                        Zam_PozycjeVatZakup = items.Zam_PozycjeVatZakup,
                        Zam_PozycjeNazwa = items.Zam_PozycjeNazwa,
                        Zam_Rabat = items.Zam_Rabat,
                        Zam_IdZamowienia = item.Zam_IdZamowienia,
                        Zam_PozycjeCzyAktywne = true
                    });
                }

                foreach (var items in Poz)
                {
                    miniMarketEntities.ZamowieniaPozycje.Add(items);

                }
                miniMarketEntities.SaveChanges();
            }
               
            
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
            var wybranyTowar = new ZamowieniaPozycje()
            {
                Zam_PozycjeIdTowaru = towar.Twr_IdTowaru,
                Zam_PozycjeJm = towar.Twr_JednPodst,
                Zam_PozycjeNazwa = towar.Twr_Nazwa,
                Zam_PozycjeKod = towar.Twr_Kod,
                Zam_PozycjeVatSprzedaz = Convert.ToByte(towar.Twr_VatSpr),
                Zam_PozycjeVatZakup = Convert.ToByte(towar.Twr_VatZak),
                Zam_PozycjeCenaNetto = towar.Twr_CenaZakNetto,
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
                Fvz_IdFvZakup = poz.Zam_IdZamowienia,
                Fvz_PozycjeIdTowaru = poz.Zam_PozycjeIdTowaru,
                Fvz_PozycjeKod = poz.Zam_PozycjeKod,
                Fvz_PozycjeNazwa = poz.Zam_PozycjeNazwa,
                Fvz_PozycjeJm = poz.Zam_PozycjeJm,
                Fvz_PozycjeVatSprzedaz = poz.Zam_PozycjeVatSprzedaz,
                Fvz_PozycjeVatZakup = poz.Zam_PozycjeVatZakup,
                Fvz_PozycjeCenaNetto = poz.Zam_PozycjeCenaNetto,
                Fvz_PozycjeIlosc = poz.Zam_PozycjeIlosc,
                Fvz_PozycjeCenaBrutto = poz.Zam_PozycjeCenaNetto * (poz.Zam_PozycjeVatZakup / 100m + 1m),
                Fvz_Rabat = poz.Zam_Rabat,
                Fvz_WartoscPozycjeCenaNetto = poz.Zam_PozycjeIlosc * poz.Zam_PozycjeCenaNetto,
                Fvz_WartoscPozycjeCenaBrutto = poz.Zam_PozycjeIlosc * poz.Zam_PozycjeCenaNetto * (poz.Zam_PozycjeVatZakup / 100m + 1m)
            }
            );
        }
        public override void Delete()
        {
            int? idTowar = WybranaPozycja.Fvz_PozycjeIdTowaru;
            if (WybranaPozycja != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var delete = db.ZamowieniaPozycje.FirstOrDefault(x => x.Zam_PozycjeIdTowaru == idTowar && x.Zam_IdZamowienia == WybranaPozycja.Fvz_IdFvZakup).Zam_PozycjeCzyAktywne = false;
                db.SaveChanges();
                ListaTowarow.Clear();
                foreach (var item in db.ZamowieniaPozycje)
                {
                    if (item.Zam_IdZamowienia == _WybraneId && item.Zam_PozycjeCzyAktywne == true)
                    {
                        ListaTowarow.Add(item);
                    }
                }
                Refresh();
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

                return komunikat;
            }

        }
        public override bool IsValid()
        {
            if (this["NumerDokumentu"] == null)
            {
                return true;
            }
            return false;
        }

       
        #endregion
    }
}
