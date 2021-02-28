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
    public class RezerwacjaEditViewModel : JedenViewModel<Rezerwacje>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;
        private FvZakupuPozycjeForView _wybranaPozycja;
        private ObservableCollection<FvZakupuPozycjeForView> _ListaPozycji;
        public ObservableCollection<RezerwacjePozycje> ListaTowarow;
        private int _WybraneId { get; set; }
        #endregion

        #region Constructor
        public RezerwacjaEditViewModel(int wybraneId)
        {
            base.DisplayName = "Rezerwacja";
            this._WybraneId = wybraneId;
            item = miniMarketEntities.Rezerwacje.Where(x => x.Rez_IdRezerwacji == wybraneId).FirstOrDefault();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();
            ListaTowarow = new ObservableCollection<RezerwacjePozycje>();
            foreach (var item in miniMarketEntities.RezerwacjePozycje)
            {
                if (item.Rez_IdRezerwacje == wybraneId && item.Rez_PozycjeCzyAktywne == true)
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
                if (_wybranaPozycja.Fvz_PozycjeIlosc != ListaTowarow.FirstOrDefault(x => x.Rez_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru && x.Rez_IdRezerwacje == _wybranaPozycja.Fvz_IdFvZakup).Rez_PozycjeIlosc)
                {
                    miniMarketEntities.RezerwacjePozycje.First(x => x.Rez_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Rez_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                }
                if (_wybranaPozycja.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Rez_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Rez_PozycjeCenaNetto)
                {
                    miniMarketEntities.RezerwacjePozycje.First(x => x.Rez_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Rez_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
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
                        wartoscNetto += item.Rez_PozycjeCenaNetto * item.Rez_PozycjeIlosc;
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
                        wartoscBrutto += item.Rez_PozycjeCenaNetto * item.Rez_PozycjeIlosc * (item.Rez_PozycjeVatZakup / 100m + 1m);
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
                return item.Rez_NrDok;
            }
            set
            {
                if (value == item.Rez_NrDok)
                    return;
                item.Rez_NrDok = value;
                base.OnPropertyChanged(() => NumerDokumentu);
            }
        }
        public DateTime? DataWystawienia
        {
            get
            {
                return item.Rez_DataWyst;
            }
            set
            {
                if (item.Rez_DataWyst != value)
                {
                    item.Rez_DataWyst = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime? DataSprzedazy
        {
            get
            {
                return item.Rez_DataSprzedazy;
            }
            set
            {
                if (item.Rez_DataSprzedazy != value)
                {
                    item.Rez_DataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public DateTime? DataOd
        {
            get
            {
                return item.Rez_DataOd;
            }
            set
            {
                if (item.Rez_DataOd != value)
                {
                    item.Rez_DataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return item.Rez_Rabat;
            }
            set
            {
                if (item.Rez_Rabat != value)
                {
                    item.Rez_Rabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public DateTime? Termin
        {
            get
            {
                return item.Rez_Termin;
            }
            set
            {
                if (item.Rez_Termin != value)
                {
                    item.Rez_Termin = value;
                    base.OnPropertyChanged(() => Termin);
                }
            }
        }
        public int? IdMagazynu
        {
            get
            {
                return item.Rez_IdMagazyn;
            }
            set
            {
                if (value == item.Rez_IdMagazyn)
                    return;
                item.Rez_IdMagazyn = value;
                base.OnPropertyChanged(() => IdMagazynu);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Rez_IdKategoria;
            }
            set
            {
                if (value == item.Rez_IdKategoria)
                    return;
                item.Rez_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public int? IdRodzajPlatnosci
        {
            get
            {
                return item.Rez_IdRodzjaPlatnosci;
            }
            set
            {
                if (value == item.Rez_IdRodzjaPlatnosci)
                    return;
                item.Rez_IdRodzjaPlatnosci = value;
                base.OnPropertyChanged(() => IdRodzajPlatnosci);
            }
        }
        public int? IdRodzajDokumentu
        {
            get
            {
                return item.Rez_IdRodzajDokumentu;
            }
            set
            {
                if (value == item.Rez_IdRodzajDokumentu)
                    return;
                item.Rez_IdRodzajDokumentu = value;
                base.OnPropertyChanged(() => IdRodzajDokumentu);
            }
        }
        public int? IdDokNettoBrutto
        {
            get
            {
                return item.Rez_IdDokNettoBrutto;
            }
            set
            {
                if (value == item.Rez_IdDokNettoBrutto)
                    return;
                item.Rez_IdDokNettoBrutto = value;
                base.OnPropertyChanged(() => IdDokNettoBrutto);
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.Rez_IdKontrahent;
            }
            set
            {
                if (value == item.Rez_IdKontrahent)
                    return;
                item.Rez_IdKontrahent = value;
                base.OnPropertyChanged(() => IdKontrahenta);
            }
        }
        public int? IdPozycji
        {
            get
            {
                return item.Rez_IdPozycji;
            }
            set
            {
                if (value == item.Rez_IdPozycji)
                    return;
                item.Rez_IdPozycji = value;
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
            item.Rez_CzyAktywne = true;
            item.Rez_DataZmian = DateTime.Now;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_NrDok = NumerDokumentu;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_DataSprzedazy = DataSprzedazy;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_DataOd = DataOd;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_DataWyst = DataWystawienia;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_Rabat = Rabat;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_Termin = Termin;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdMagazyn = IdMagazynu;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdKategoria = IdKategorii;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdKontrahent = IdKontrahenta;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdRodzajDokumentu = IdRodzajDokumentu;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdDokNettoBrutto = IdDokNettoBrutto;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdKontrahentOdbiorca = IdKontrahenta;
            miniMarketEntities.Rezerwacje.FirstOrDefault(x => x.Rez_IdRezerwacji == _WybraneId).Rez_IdPozycji = IdPozycji;
            miniMarketEntities.SaveChanges();

            ObservableCollection<RezerwacjePozycje> Poz = new ObservableCollection<RezerwacjePozycje>();
            var OldList = miniMarketEntities.RezerwacjePozycje.Where(x => x.Rez_IdRezerwacje == _WybraneId);
            int liczbaTowarowWBazie = OldList.Count();
            int liczbaTowarowWLiscie = ListaTowarow.Count();
            int roznicaTowarow;
            if (liczbaTowarowWLiscie > liczbaTowarowWBazie)
            {
                roznicaTowarow = liczbaTowarowWLiscie - liczbaTowarowWBazie;
                foreach (var items in ListaTowarow.Reverse().Take(roznicaTowarow))
                {

                    Poz.Add(new RezerwacjePozycje
                    {

                        Rez_PozycjeIdTowaru = items.Rez_PozycjeIdTowaru,
                        Rez_PozycjeJm = items.Rez_PozycjeJm,
                        Rez_PozycjeIlosc = items.Rez_PozycjeIlosc,
                        Rez_PozycjeCenaNetto = items.Rez_PozycjeCenaNetto,
                        Rez_PozycjeKod = items.Rez_PozycjeKod,
                        Rez_PozycjeVatSprzedaz = items.Rez_PozycjeVatSprzedaz,
                        Rez_PozycjeVatZakup = items.Rez_PozycjeVatZakup,
                        Rez_PozycjeNazwa = items.Rez_PozycjeNazwa,
                        Rez_Rabat = items.Rez_Rabat,
                        Rez_IdRezerwacje = item.Rez_IdRezerwacji,
                        Rez_PozycjeCzyAktywne = true
                    });
                }

                foreach (var items in Poz)
                {
                    miniMarketEntities.RezerwacjePozycje.Add(items);

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
            var wybranyTowar = new RezerwacjePozycje()
            {
                Rez_PozycjeIdTowaru = towar.Twr_IdTowaru,
                Rez_PozycjeJm = towar.Twr_JednPodst,
                Rez_PozycjeNazwa = towar.Twr_Nazwa,
                Rez_PozycjeKod = towar.Twr_Kod,
                Rez_PozycjeVatSprzedaz = Convert.ToByte(towar.Twr_VatSpr),
                Rez_PozycjeVatZakup = Convert.ToByte(towar.Twr_VatZak),
                Rez_PozycjeCenaNetto = towar.Twr_CenaZakNetto,
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
                Fvz_IdFvZakup = poz.Rez_IdRezerwacje,
                Fvz_PozycjeIdTowaru = poz.Rez_PozycjeIdTowaru,
                Fvz_PozycjeKod = poz.Rez_PozycjeKod,
                Fvz_PozycjeNazwa = poz.Rez_PozycjeNazwa,
                Fvz_PozycjeJm = poz.Rez_PozycjeJm,
                Fvz_PozycjeVatSprzedaz = poz.Rez_PozycjeVatSprzedaz,
                Fvz_PozycjeVatZakup = poz.Rez_PozycjeVatZakup,
                Fvz_PozycjeCenaNetto = poz.Rez_PozycjeCenaNetto,
                Fvz_PozycjeIlosc = poz.Rez_PozycjeIlosc,
                Fvz_PozycjeCenaBrutto = poz.Rez_PozycjeCenaNetto * (poz.Rez_PozycjeVatZakup / 100m + 1m),
                Fvz_Rabat = poz.Rez_Rabat,
                Fvz_WartoscPozycjeCenaNetto = poz.Rez_PozycjeIlosc * poz.Rez_PozycjeCenaNetto,
                Fvz_WartoscPozycjeCenaBrutto = poz.Rez_PozycjeIlosc * poz.Rez_PozycjeCenaNetto * (poz.Rez_PozycjeVatZakup / 100m + 1m)
            }
            );
        }
        public override void Delete()
        {
            int? idTowar = WybranaPozycja.Fvz_PozycjeIdTowaru;
            if (WybranaPozycja != null)
            {
                MiniMarketEntities db = new MiniMarketEntities();
                var delete = db.RezerwacjePozycje.FirstOrDefault(x => x.Rez_PozycjeIdTowaru == idTowar && x.Rez_IdRezerwacje == WybranaPozycja.Fvz_IdFvZakup).Rez_PozycjeCzyAktywne = false;
                db.SaveChanges();
                ListaTowarow.Clear();
                foreach (var item in db.RezerwacjePozycje)
                {
                    if (item.Rez_IdRezerwacje == _WybraneId && item.Rez_PozycjeCzyAktywne == true)
                    {
                        ListaTowarow.Add(item);
                    }
                }
                Refresh();
            }
        }

        public override void Refresh()
        {
            loadPozycji();
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
