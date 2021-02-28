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
    public class WZEditViewModel: JedenViewModel<WZ>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;
        private ObservableCollection<FvZakupuPozycjeForView> __ListaPozycji;
        private FvZakupuPozycjeForView _wybranaPozycja;
        public ObservableCollection<WzPozycje> ListaTowarow;
        private decimal? wartoscNetto = 0;
        private decimal? wartoscBrutto = 0;
        private int _WybraneId { get; set; }
        #endregion
        #region Constructor
        public WZEditViewModel(int wybraneId)
        {
            base.DisplayName = "WZ";
            this._WybraneId = wybraneId;
            item = miniMarketEntities.WZ.Where(x => x.Wz_IdWZ == wybraneId).FirstOrDefault();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();
            ListaTowarow = new ObservableCollection<WzPozycje>();
            foreach (var item in miniMarketEntities.WzPozycje)
            {
                if (item.Wz_IdWZ == wybraneId && item.Wz_PozycjeCzyAktywne == true)
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
                if (value != null)
                {
                    _wybranaPozycja = value;

                    if (value.Fvz_PozycjeIlosc != ListaTowarow.FirstOrDefault(x => x.Wz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru && x.Wz_IdWZ == _wybranaPozycja.Fvz_IdFvZakup).Wz_PozycjeIlosc)
                    {
                        miniMarketEntities.WzPozycje.First(x => x.Wz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru && x.Wz_IdWZ == _wybranaPozycja.Fvz_IdFvZakup).Wz_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                        miniMarketEntities.SaveChanges();
                    }
                    if (value.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Wz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Wz_PozycjeCenaNetto)
                    {

                        miniMarketEntities.WzPozycje.First(x => x.Wz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Wz_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
                        miniMarketEntities.SaveChanges();
                    }
                }
                base.OnPropertyChanged(() => WybranaPozycja);
            }
        }
        public ObservableCollection<FvZakupuPozycjeForView> ListaPozycji
        {
            get
            {

                if (__ListaPozycji != null) loadPozycji();
                return __ListaPozycji;

            }
            set
            {

                __ListaPozycji = value;
                //OnPropertyChanged(() => ListaPozycji);
            }
        }

        public double WartoscNetto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscNetto += item.Wz_PozycjeCenaNetto * item.Wz_PozycjeIlosc;

                    }
                return Math.Round(Convert.ToDouble(wartoscNetto), 2);
            }
            set
            {
                OnPropertyChanged(() => WartoscNetto);
            }
        }
        public double? WartoscBrutto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscBrutto += item.Wz_PozycjeCenaNetto * item.Wz_PozycjeIlosc * (item.Wz_PozycjeVatZakup / 100m + 1m);
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
                return item.Wz_NrDok;
            }
            set
            {
                if (value == item.Wz_NrDok)
                    return;
                item.Wz_NrDok = value;
                base.OnPropertyChanged(() => NumerDokumentu);
            }
        }
        public DateTime? DataWystawienia
        {
            get
            {
                return item.Wz_DataWyst;
            }
            set
            {
                if (item.Wz_DataWyst != value)
                {
                    item.Wz_DataWyst = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime? DataSprzedazy
        {
            get
            {
                return item.Wz_DataSprzedazy;
            }
            set
            {
                if (item.Wz_DataSprzedazy != value)
                {
                    item.Wz_DataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public DateTime? DataOd
        {
            get
            {
                return item.Wz_DataOd;
            }
            set
            {
                if (item.Wz_DataOd != value)
                {
                    item.Wz_DataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return item.Wz_Rabat;
            }
            set
            {
                if (item.Wz_Rabat != value)
                {
                    item.Wz_Rabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public DateTime? Termin
        {
            get
            {
                return item.Wz_Termin;
            }
            set
            {
                if (item.Wz_Termin != value)
                {
                    item.Wz_Termin = value;
                    base.OnPropertyChanged(() => Termin);
                }
            }
        }
        public int? IdMagazynu
        {
            get
            {
                return item.Wz_IdMagazyn;
            }
            set
            {
                if (value == item.Wz_IdMagazyn)
                    return;
                item.Wz_IdMagazyn = value;
                base.OnPropertyChanged(() => IdMagazynu);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Wz_IdKategoria;
            }
            set
            {
                if (value == item.Wz_IdKategoria)
                    return;
                item.Wz_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public int? IdRodzajPlatnosci
        {
            get
            {
                return item.Wz_IdRodzajPlatnosci;
            }
            set
            {
                if (value == item.Wz_IdRodzajPlatnosci)
                    return;
                item.Wz_IdRodzajPlatnosci = value;
                base.OnPropertyChanged(() => IdRodzajPlatnosci);
            }
        }
        public int? IdRodzajDokumentu
        {
            get
            {
                return item.Wz_IdRodzajDokumentu;
            }
            set
            {
                if (value == item.Wz_IdRodzajDokumentu)
                    return;
                item.Wz_IdRodzajDokumentu = value;
                base.OnPropertyChanged(() => IdRodzajDokumentu);
            }
        }
        public int? IdDokNettoBrutto
        {
            get
            {
                return item.Wz_IdDokNettoBrutto;
            }
            set
            {
                if (value == item.Wz_IdDokNettoBrutto)
                    return;
                item.Wz_IdDokNettoBrutto = value;
                base.OnPropertyChanged(() => IdDokNettoBrutto);
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.Wz_IdKontrahent;
            }
            set
            {
                if (value == item.Wz_IdKontrahent)
                    return;
                item.Wz_IdKontrahent = value;
                base.OnPropertyChanged(() => IdKontrahenta);
            }
        }
        public int? IdPozycji
        {
            get
            {
                return item.Wz_IdPozycji;
            }
            set
            {
                if (value == item.Wz_IdPozycji)
                    return;
                item.Wz_IdPozycji = value;
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
            item.Wz_CzyAktywne = true;
            item.Wz_DataZmian = DateTime.Now;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_NrDok = NumerDokumentu;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_DataSprzedazy = DataSprzedazy;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_DataOd = DataOd;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_DataWyst = DataWystawienia;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_Rabat = Rabat;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_Termin = Termin;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdMagazyn = IdMagazynu;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdKategoria = IdKategorii;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdKontrahent = IdKontrahenta;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdRodzajDokumentu = IdRodzajDokumentu;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdDokNettoBrutto = IdDokNettoBrutto;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdKontrahentOdbiorca = IdKontrahenta;
            miniMarketEntities.WZ.FirstOrDefault(x => x.Wz_IdWZ == _WybraneId).Wz_IdPozycji = IdPozycji;
            miniMarketEntities.SaveChanges();

            ObservableCollection<WzPozycje> Poz = new ObservableCollection<WzPozycje>();
            var OldList = miniMarketEntities.WzPozycje.Where(x => x.Wz_IdWZ == _WybraneId);
            int liczbaTowarowWBazie = OldList.Count();
            int liczbaTowarowWLiscie = ListaTowarow.Count();
            int roznicaTowarow;
            if (liczbaTowarowWLiscie > liczbaTowarowWBazie)
            {
                roznicaTowarow = liczbaTowarowWLiscie - liczbaTowarowWBazie;
                foreach (var items in ListaTowarow.Reverse().Take(roznicaTowarow))
                {

                    Poz.Add(new WzPozycje
                    {

                        Wz_PozycjeIdTowaru = items.Wz_PozycjeIdTowaru,
                        Wz_PozycjeJm = items.Wz_PozycjeJm,
                        Wz_PozycjeIlosc = items.Wz_PozycjeIlosc,
                        Wz_PozycjeCenaNetto = items.Wz_PozycjeCenaNetto,
                        Wz_PozycjeKod = items.Wz_PozycjeKod,
                        Wz_PozycjeVatSprzedaz = items.Wz_PozycjeVatSprzedaz,
                        Wz_PozycjeVatZakup = items.Wz_PozycjeVatZakup,
                        Wz_PozycjeNazwa = items.Wz_PozycjeNazwa,
                        Wz_Rabat = items.Wz_Rabat,
                        Wz_IdWZ = item.Wz_IdWZ,
                        Wz_PozycjeCzyAktywne = true
                    });
                }

                foreach (var items in Poz)
                {
                    miniMarketEntities.WzPozycje.Add(items);

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
            var wybranyTowar = new WzPozycje()
            {

                Wz_PozycjeIdTowaru = towar.Twr_IdTowaru,
                Wz_PozycjeJm = towar.Twr_JednPodst,
                Wz_PozycjeNazwa = towar.Twr_Nazwa,
                Wz_PozycjeKod = towar.Twr_Kod,
                Wz_PozycjeVatSprzedaz = Convert.ToByte(towar.Twr_VatSpr),
                Wz_PozycjeVatZakup = Convert.ToByte(towar.Twr_VatZak),
                Wz_PozycjeCenaNetto = towar.Twr_CenaZakNetto,
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

                Fvz_IdFvZakup = poz.Wz_IdWZ,
                Fvz_PozycjeIdTowaru = poz.Wz_PozycjeIdTowaru,
                Fvz_PozycjeKod = poz.Wz_PozycjeKod,
                Fvz_PozycjeNazwa = poz.Wz_PozycjeNazwa,
                Fvz_PozycjeJm = poz.Wz_PozycjeJm,
                Fvz_PozycjeVatSprzedaz = poz.Wz_PozycjeVatSprzedaz,
                Fvz_PozycjeVatZakup = poz.Wz_PozycjeVatZakup,
                Fvz_PozycjeCenaNetto = poz.Wz_PozycjeCenaNetto,
                Fvz_PozycjeIlosc = poz.Wz_PozycjeIlosc,
                Fvz_PozycjeCenaBrutto = poz.Wz_PozycjeCenaNetto * (poz.Wz_PozycjeVatZakup / 100m + 1m),
                Fvz_Rabat = poz.Wz_Rabat,
                Fvz_WartoscPozycjeCenaNetto = poz.Wz_PozycjeIlosc * poz.Wz_PozycjeCenaNetto,
                Fvz_WartoscPozycjeCenaBrutto = poz.Wz_PozycjeIlosc * poz.Wz_PozycjeCenaNetto * (poz.Wz_PozycjeVatZakup / 100m + 1m)

            }
            );
        }
        public override void Delete()
        {
            int? idTowar = WybranaPozycja.Fvz_PozycjeIdTowaru;
            if (WybranaPozycja != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var delete = db.WzPozycje.FirstOrDefault(x => x.Wz_PozycjeIdTowaru == idTowar && x.Wz_IdWZ == WybranaPozycja.Fvz_IdFvZakup).Wz_PozycjeCzyAktywne = false;
                db.SaveChanges();
                ListaTowarow.Clear();
                foreach (var item in db.WzPozycje)
                {
                    if (item.Wz_IdWZ == _WybraneId && item.Wz_PozycjeCzyAktywne == true)
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
