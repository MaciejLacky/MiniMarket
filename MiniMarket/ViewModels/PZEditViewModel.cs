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
    public class PZEditViewModel : JedenViewModel<PZ>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;
        private ObservableCollection<FvZakupuPozycjeForView> __ListaPozycji;
        private FvZakupuPozycjeForView _wybranaPozycja;
        public ObservableCollection<PzPozycje> ListaTowarow;
        private decimal? wartoscNetto = 0;
        private decimal? wartoscBrutto = 0;
        private int _WybraneId { get; set; }
        #endregion
        #region Constructor
        public PZEditViewModel(int wybraneId)
        {
            base.DisplayName = "PZ";
            this._WybraneId = wybraneId;
            item = miniMarketEntities.PZ.Where(x => x.Pz_IdPZ == wybraneId).FirstOrDefault();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();
            ListaTowarow = new ObservableCollection<PzPozycje>();
            foreach (var item in miniMarketEntities.PzPozycje)
            {
                if (item.Pz_IdPZ == wybraneId && item.Pz_PozycjeCzyAktywne == true)
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

                    if (value.Fvz_PozycjeIlosc != ListaTowarow.FirstOrDefault(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru && x.Pz_IdPZ == _wybranaPozycja.Fvz_IdFvZakup).Pz_PozycjeIlosc)
                    {
                        miniMarketEntities.PzPozycje.First(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru && x.Pz_IdPZ == _wybranaPozycja.Fvz_IdFvZakup).Pz_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                        
                        miniMarketEntities.SaveChanges();
                    }
                    if (value.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Pz_PozycjeCenaNetto)
                    {

                        miniMarketEntities.PzPozycje.First(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Pz_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
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
                        wartoscNetto += item.Pz_PozycjeCenaNetto * item.Pz_PozycjeIlosc;

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
                        wartoscBrutto += item.Pz_PozycjeCenaNetto * item.Pz_PozycjeIlosc * (item.Pz_PozycjeVatZakup / 100m + 1m);
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
                return item.Pz_NrDok;
            }
            set
            {
                if (value == item.Pz_NrDok)
                    return;
                item.Pz_NrDok = value;
                base.OnPropertyChanged(() => NumerDokumentu);
            }
        }
        public DateTime? DataWystawienia
        {
            get
            {
                return item.Pz_DataWyst;
            }
            set
            {
                if (item.Pz_DataWyst != value)
                {
                    item.Pz_DataWyst = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime? DataSprzedazy
        {
            get
            {
                return item.Pz_DataSprzedazy;
            }
            set
            {
                if (item.Pz_DataSprzedazy != value)
                {
                    item.Pz_DataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public DateTime? DataOd
        {
            get
            {
                return item.Pz_DataOd;
            }
            set
            {
                if (item.Pz_DataOd != value)
                {
                    item.Pz_DataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return item.Pz_Rabat;
            }
            set
            {
                if (item.Pz_Rabat != value)
                {
                    item.Pz_Rabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public DateTime? Termin
        {
            get
            {
                return item.Pz_Termin;
            }
            set
            {
                if (item.Pz_Termin != value)
                {
                    item.Pz_Termin = value;
                    base.OnPropertyChanged(() => Termin);
                }
            }
        }
        public int? IdMagazynu
        {
            get
            {
                return item.Pz_IdMagazyn;
            }
            set
            {
                if (value == item.Pz_IdMagazyn)
                    return;
                item.Pz_IdMagazyn = value;
                base.OnPropertyChanged(() => IdMagazynu);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Pz_IdKategoria;
            }
            set
            {
                if (value == item.Pz_IdKategoria)
                    return;
                item.Pz_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public int? IdRodzajPlatnosci
        {
            get
            {
                return item.Pz_RodzajPlatnosci;
            }
            set
            {
                if (value == item.Pz_RodzajPlatnosci)
                    return;
                item.Pz_RodzajPlatnosci = value;
                base.OnPropertyChanged(() => IdRodzajPlatnosci);
            }
        }
        public int? IdRodzajDokumentu
        {
            get
            {
                return item.Pz_IdRodzajDokumentu;
            }
            set
            {
                if (value == item.Pz_IdRodzajDokumentu)
                    return;
                item.Pz_IdRodzajDokumentu = value;
                base.OnPropertyChanged(() => IdRodzajDokumentu);
            }
        }
        public int? IdDokNettoBrutto
        {
            get
            {
                return item.Pz_IdDokNettoBrutto;
            }
            set
            {
                if (value == item.Pz_IdDokNettoBrutto)
                    return;
                item.Pz_IdDokNettoBrutto = value;
                base.OnPropertyChanged(() => IdDokNettoBrutto);
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.Pz_IdKontrahent;
            }
            set
            {
                if (value == item.Pz_IdKontrahent)
                    return;
                item.Pz_IdKontrahent = value;
                base.OnPropertyChanged(() => IdKontrahenta);
            }
        }
        public int? IdPozycji
        {
            get
            {
                return item.Pz_IdPozycji;
            }
            set
            {
                if (value == item.Pz_IdPozycji)
                    return;
                item.Pz_IdPozycji = value;
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
            item.Pz_CzyAktywne = true;
            item.Pz_DataZmian = DateTime.Now;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_NrDok = NumerDokumentu;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_DataSprzedazy = DataSprzedazy;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_DataOd = DataOd;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_DataWyst = DataWystawienia;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_Rabat = Rabat;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_Termin = Termin;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdMagazyn = IdMagazynu;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdKategoria = IdKategorii;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdKontrahent = IdKontrahenta;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdRodzajDokumentu = IdRodzajDokumentu;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdDokNettoBrutto = IdDokNettoBrutto;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdKontrahentOdbiorca = IdKontrahenta;
            miniMarketEntities.PZ.FirstOrDefault(x => x.Pz_IdPZ == _WybraneId).Pz_IdPozycji = IdPozycji;
            miniMarketEntities.SaveChanges();

            ObservableCollection<PzPozycje> Poz = new ObservableCollection<PzPozycje>();
            var OldList = miniMarketEntities.PzPozycje.Where(x => x.Pz_IdPZ == _WybraneId);
            int liczbaTowarowWBazie = OldList.Count();
            int liczbaTowarowWLiscie = ListaTowarow.Count();
            int roznicaTowarow;
            if (liczbaTowarowWLiscie > liczbaTowarowWBazie)
            {
                roznicaTowarow = liczbaTowarowWLiscie - liczbaTowarowWBazie;
                foreach (var items in ListaTowarow.Reverse().Take(roznicaTowarow))
                {

                    Poz.Add(new PzPozycje
                    {

                        Pz_PozycjeIdTowaru = items.Pz_PozycjeIdTowaru,
                        Pz_PozycjeJm = items.Pz_PozycjeJm,
                        Pz_PozycjeIlosc = items.Pz_PozycjeIlosc,
                        Pz_PozycjeCenaNetto = items.Pz_PozycjeCenaNetto,
                        Pz_PozycjeKod = items.Pz_PozycjeKod,
                        Pz_PozycjeVatSprzedaz = items.Pz_PozycjeVatSprzedaz,
                        Pz_PozycjeVatZakup = items.Pz_PozycjeVatZakup,
                        Pz_PozycjeNazwa = items.Pz_PozycjeNazwa,
                        Pz_Rabat = items.Pz_Rabat,
                        Pz_IdPZ = item.Pz_IdPZ,
                        Pz_PozycjeCzyAktywne = true
                    });
                }

                foreach (var items in Poz)
                {
                    miniMarketEntities.PzPozycje.Add(items);

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
            var wybranyTowar = new PzPozycje()
            {

                Pz_PozycjeIdTowaru = towar.Twr_IdTowaru,
                Pz_PozycjeJm = towar.Twr_JednPodst,
                Pz_PozycjeNazwa = towar.Twr_Nazwa,
                Pz_PozycjeKod = towar.Twr_Kod,
                Pz_PozycjeVatSprzedaz = Convert.ToByte(towar.Twr_VatSpr),
                Pz_PozycjeVatZakup = Convert.ToByte(towar.Twr_VatZak),
                Pz_PozycjeCenaNetto = towar.Twr_CenaZakNetto,
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

                Fvz_IdFvZakup = poz.Pz_IdPZ,
                Fvz_PozycjeIdTowaru = poz.Pz_PozycjeIdTowaru,
                Fvz_PozycjeKod = poz.Pz_PozycjeKod,
                Fvz_PozycjeNazwa = poz.Pz_PozycjeNazwa,
                Fvz_PozycjeJm = poz.Pz_PozycjeJm,
                Fvz_PozycjeVatSprzedaz = poz.Pz_PozycjeVatSprzedaz,
                Fvz_PozycjeVatZakup = poz.Pz_PozycjeVatZakup,
                Fvz_PozycjeCenaNetto = poz.Pz_PozycjeCenaNetto,
                Fvz_PozycjeIlosc = poz.Pz_PozycjeIlosc,
                Fvz_PozycjeCenaBrutto = poz.Pz_PozycjeCenaNetto * (poz.Pz_PozycjeVatZakup / 100m + 1m),
                Fvz_Rabat = poz.Pz_Rabat,
                Fvz_WartoscPozycjeCenaNetto = poz.Pz_PozycjeIlosc * poz.Pz_PozycjeCenaNetto,
                Fvz_WartoscPozycjeCenaBrutto = poz.Pz_PozycjeIlosc * poz.Pz_PozycjeCenaNetto * (poz.Pz_PozycjeVatZakup / 100m + 1m)

            }
            );
        }
        public override void Delete()
        {
            int? idTowar = WybranaPozycja.Fvz_PozycjeIdTowaru;
            if (WybranaPozycja != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var delete = db.PzPozycje.FirstOrDefault(x => x.Pz_PozycjeIdTowaru == idTowar && x.Pz_IdPZ == WybranaPozycja.Fvz_IdFvZakup).Pz_PozycjeCzyAktywne = false;
                db.SaveChanges();
                ListaTowarow.Clear();
                foreach (var item in db.PzPozycje)
                {
                    if (item.Pz_IdPZ == _WybraneId && item.Pz_PozycjeCzyAktywne == true)
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
