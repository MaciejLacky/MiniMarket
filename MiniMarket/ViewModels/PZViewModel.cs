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
    public class PZViewModel : JedenViewModel<PZ>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;
        private FvZakupuPozycjeForView _wybranaPozycja;
        private ObservableCollection<FvZakupuPozycjeForView> _ListaPozycji;
        public ObservableCollection<PzPozycje> ListaTowarow;
        #endregion
        #region Constructor
        public PZViewModel()
        {
            base.DisplayName = "PZ";
            item = new PZ();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            _ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();
            ListaTowarow = new ObservableCollection<PzPozycje>();
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
                    ListaTowarow.First(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Pz_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                }
                if (_wybranaPozycja.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Pz_PozycjeCenaNetto)
                {
                    ListaTowarow.First(x => x.Pz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Pz_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
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
        public decimal? WartoscNetto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscNetto += item.Pz_PozycjeCenaNetto * item.Pz_PozycjeIlosc;
                    }
                return wartoscNetto;
            }
            set
            {
                OnPropertyChanged(() => WartoscNetto);
            }
        }
        public decimal? WartoscBrutto
        {
            get
            {
                if (ListaTowarow != null)
                    foreach (var item in ListaTowarow)
                    {
                        wartoscBrutto += item.Pz_PozycjeCenaNetto * item.Pz_PozycjeIlosc * (item.Pz_PozycjeVatZakup / 100m + 1m);
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
            item.Pz_DataWprow = DateTime.Today;
            item.Pz_CzyAktywne = true;
            miniMarketEntities.PZ.Add(item);
            miniMarketEntities.SaveChanges();
            ObservableCollection<PzPozycje> Poz = new ObservableCollection<PzPozycje>();
            foreach (var items in ListaTowarow)
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
            int x = 0;
            int y = ListaTowarow.Count;
            for (int i = 0; i < y; i++)
            {
                if (ListaTowarow.FirstOrDefault().Pz_PozycjeIdTowaru == WybranaPozycja.Fvz_PozycjeIdTowaru)
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
