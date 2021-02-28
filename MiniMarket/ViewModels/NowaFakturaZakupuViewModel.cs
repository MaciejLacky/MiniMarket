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
    public class NowaFakturaZakupuViewModel : JedenViewModel<FakturyZakupu>, IDataErrorInfo
    {
        #region Fields
        private BaseCommand _ShowKontrahenci;
        private BaseCommand _ShowKategorie;
        private BaseCommand _ShowTowary;       
        private FvZakupuPozycjeForView _wybranaPozycja;
        private ObservableCollection<FvZakupuPozycjeForView> _ListaPozycji;       
        public ObservableCollection<FakturyZakupuPozycje> ListaTowarow;


        #endregion
        #region Konstruktor
        public NowaFakturaZakupuViewModel()
        {            
            base.DisplayName = "Faktura zakup";
            item = new FakturyZakupu();
            Messenger.Default.Register<Kontrahenci>(this, getWybranyKontrahent);
            Messenger.Default.Register<Kategorie>(this, getWybranaKategoria);
            Messenger.Default.Register<TowarForView>(this, getWybranyTowar);
            _ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>();
            _wybranaPozycja = new FvZakupuPozycjeForView();      
            ListaTowarow = new ObservableCollection<FakturyZakupuPozycje>();
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
                return _wybranaPozycja ;
            }
            set
            {
                    _wybranaPozycja = value;
                if (_wybranaPozycja.Fvz_PozycjeIlosc != null)
                {
                    ListaTowarow.First(x => x.Fvz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Fvz_PozycjeIlosc = _wybranaPozycja.Fvz_PozycjeIlosc;
                }
                if (_wybranaPozycja.Fvz_PozycjeCenaNetto != ListaTowarow.FirstOrDefault(x => x.Fvz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Fvz_PozycjeCenaNetto)
                {
                    ListaTowarow.First(x => x.Fvz_PozycjeIdTowaru == _wybranaPozycja.Fvz_PozycjeIdTowaru).Fvz_PozycjeCenaNetto = _wybranaPozycja.Fvz_PozycjeCenaNetto;
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
                        wartoscNetto += item.Fvz_PozycjeCenaNetto * item.Fvz_PozycjeIlosc;
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
                        wartoscBrutto += item.Fvz_PozycjeCenaNetto * item.Fvz_PozycjeIlosc * (item.Fvz_PozycjeVatZakup / 100m + 1m);
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
                return item.Fvz_NrDok;
            }
            set
            {
                if (value == item.Fvz_NrDok)
                    return;
                item.Fvz_NrDok = value;
                base.OnPropertyChanged(() => NumerDokumentu);
            }
        }

        
        public DateTime? DataWystawienia
        {
            get
            {
                return item.Fvz_DataWyst = item.Fvz_DataWyst == null ? DateTime.Now : item.Fvz_DataWyst;
            }
            set
            {
                if (item.Fvz_DataWyst != value)
                {
                    item.Fvz_DataWyst = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime? DataSprzedazy
        {
            get
            {
                return item.FvzDataSprzedazy = item.FvzDataSprzedazy == null ? DateTime.Now : item.FvzDataSprzedazy;
            }
            set
            {
                if (item.FvzDataSprzedazy != value)
                {
                    item.FvzDataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public DateTime? DataOd
        {
            get
            {
                return item.FvzDataOd;
            }
            set
            {
                if (item.FvzDataOd != value)
                {
                    item.FvzDataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return item.FvzRabat;
            }
            set
            {
                if (item.FvzRabat != value)
                {
                    item.FvzRabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public DateTime? Termin
        {
            get
            {
                return item.Fvz_Termin = item.Fvz_Termin == null ? DateTime.Now.AddDays(7) : item.Fvz_Termin;
            }
            set
            {
                if (item.Fvz_Termin != value)
                {
                    item.Fvz_Termin = value;
                    base.OnPropertyChanged(() => Termin);
                }
            }
        }
        public int? IdMagazynu
        {
            get
            {
                return item.Fvz_IdMagazyn = item.Fvz_IdMagazyn == null ? 1 : item.Fvz_IdMagazyn;
            }
            set
            {
                if (value == item.Fvz_IdMagazyn)
                    return;
                item.Fvz_IdMagazyn = value;
                base.OnPropertyChanged(() => IdMagazynu);
            }
        }
        public int? IdKategorii
        {
            get
            {
                return item.Fvz_IdKategoria;
            }
            set
            {
                if (value == item.Fvz_IdKategoria)
                    return;
                item.Fvz_IdKategoria = value;
                base.OnPropertyChanged(() => IdKategorii);
            }
        }
        public int? IdRodzajPlatnosci
        {
            get
            {
                return item.Fvz_IdRodzajPlatnosci = item.Fvz_IdRodzajPlatnosci == null ? 1 : item.Fvz_IdRodzajPlatnosci;
            }
            set
            {
                if (value == item.Fvz_IdRodzajPlatnosci)
                    return;
                item.Fvz_IdRodzajPlatnosci = value;
                base.OnPropertyChanged(() => IdRodzajPlatnosci);
            }
        }
        public int? IdRodzajDokumentu
        {
            get
            {
                return item.Fvz_IdRodzajDokumentu = item.Fvz_IdRodzajDokumentu== null ? 1 : item.Fvz_IdRodzajDokumentu;
            }
            set
            {
                if (value == item.Fvz_IdRodzajDokumentu)
                    return;
                item.Fvz_IdRodzajDokumentu = value;
                base.OnPropertyChanged(() => IdRodzajDokumentu);
            }
        }
        public int? IdDokNettoBrutto
        {
            get
            {
                return item.Fvz_IdDokNettoBrutto;
            }
            set
            {
                if (value == item.Fvz_IdDokNettoBrutto)
                    return;
                item.Fvz_IdDokNettoBrutto = value;
                base.OnPropertyChanged(() => IdDokNettoBrutto);
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.Fvz_IdKontrahent;
            }
            set
            {
                if (value == item.Fvz_IdKontrahent)
                    return;
                item.Fvz_IdKontrahent = value;
                base.OnPropertyChanged(() => IdKontrahenta);
            }
        }
        public int? IdPozycji
        {
            get
            {
                return item.Fvz_IdPozycji;
            }
            set
            {
                if (value == item.Fvz_IdPozycji)
                    return;
                item.Fvz_IdPozycji = value;
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
            item.CzyAktywne = true;
            item.Fvz_DataWprow = DateTime.Now;
            miniMarketEntities.FakturyZakupu.Add(item);
            miniMarketEntities.SaveChanges();
            ObservableCollection<FakturyZakupuPozycje> Poz = new ObservableCollection<FakturyZakupuPozycje>();
            ObservableCollection<TowaryIlosci> Ilosci = new ObservableCollection<TowaryIlosci>();
            foreach (var items in ListaTowarow)
            {

                Poz.Add(new FakturyZakupuPozycje
                {
                    Fvz_PozycjeIdTowaru = items.Fvz_PozycjeIdTowaru,
                    Fvz_PozycjeJm = items.Fvz_PozycjeJm,
                    Fvz_PozycjeIlosc = items.Fvz_PozycjeIlosc,
                    Fvz_PozycjeCenaNetto = items.Fvz_PozycjeCenaNetto,
                    Fvz_PozycjeKod = items.Fvz_PozycjeKod,
                    Fvz_PozycjeVatSprzedaz = items.Fvz_PozycjeVatSprzedaz,
                    Fvz_PozycjeVatZakup = items.Fvz_PozycjeVatZakup,
                    Fvz_PozycjeNazwa = items.Fvz_PozycjeNazwa,
                    Fvz_Rabat = items.Fvz_Rabat,
                    Fvz_IdFvZakup = item.Fvz_IdZakup,
                    Fvz_PozycjeCzyAktywne = true
                }) ;

                Ilosci.Add(new TowaryIlosci
                {
                    Twr_WprowData = DateTime.Now,
                    CzyAktywne = true,
                    Twr_IdFvZakupu = item.Fvz_IdZakup,
                    Twr_ZakupIlosc = items.Fvz_PozycjeIlosc,
                    Twr_IdTowaru = items.Fvz_PozycjeIdTowaru,
                    Twr_Wartosc = items.Fvz_PozycjeCenaNetto,
                    Twr_NumerDokumentu = item.Fvz_NrDok,
                    Twr_TypDokumentu = item.RodzajeDokumentow.RD_Nazwa,
                    Twr_DataZakup = DateTime.Now
                    
                }) ;
            }
           
            foreach (var items in Poz)
            {
                miniMarketEntities.FakturyZakupuPozycje.Add(items);
                
                
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
            var wybranyTowar = new FakturyZakupuPozycje()
            {
                Fvz_PozycjeIdTowaru = towar.Twr_IdTowaru,
                Fvz_PozycjeJm = towar.Twr_JednPodst,
                Fvz_PozycjeNazwa = towar.Twr_Nazwa,
                Fvz_PozycjeKod = towar.Twr_Kod,
                Fvz_PozycjeVatSprzedaz = Convert.ToByte(towar.Twr_VatSpr),
                Fvz_PozycjeVatZakup = Convert.ToByte(towar.Twr_VatZak),
                Fvz_PozycjeCenaNetto = towar.Twr_CenaZakNetto,                
            };

            ListaTowarow.Add(wybranyTowar);           
        }
       
        public  void loadPozycji()
        {
            ListaPozycji = new ObservableCollection<FvZakupuPozycjeForView>
            (            
            from poz in ListaTowarow
            select new FvZakupuPozycjeForView
            {
                Fvz_PozycjeIdTowaru = poz.Fvz_PozycjeIdTowaru,
                Fvz_PozycjeKod = poz.Fvz_PozycjeKod,
                Fvz_PozycjeNazwa = poz.Fvz_PozycjeNazwa, 
                Fvz_PozycjeJm = poz.Fvz_PozycjeJm,
                Fvz_PozycjeVatSprzedaz = poz.Fvz_PozycjeVatSprzedaz,
                Fvz_PozycjeVatZakup = poz.Fvz_PozycjeVatZakup,
                Fvz_PozycjeCenaNetto = poz.Fvz_PozycjeCenaNetto,
                Fvz_PozycjeIlosc = poz.Fvz_PozycjeIlosc,
                Fvz_PozycjeCenaBrutto = poz.Fvz_PozycjeCenaNetto * (poz.Fvz_PozycjeVatZakup / 100m +1m),
                Fvz_Rabat = poz.Fvz_Rabat,
                Fvz_WartoscPozycjeCenaNetto = poz.Fvz_PozycjeIlosc * poz.Fvz_PozycjeCenaNetto,
                Fvz_WartoscPozycjeCenaBrutto = poz.Fvz_PozycjeIlosc * poz.Fvz_PozycjeCenaNetto * (poz.Fvz_PozycjeVatZakup / 100m + 1m)
            }
            );
        }
        public override void Delete()
        {
            int x = 0;
            int y = ListaTowarow.Count;
            for (int i = 0; i < y; i++)
            {
                if (ListaTowarow.FirstOrDefault().Fvz_PozycjeIdTowaru == WybranaPozycja.Fvz_PozycjeIdTowaru)
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
            if (this["NumerDokumentu"] == null )
            {
                return true;
            }
            return false;
        }

       
        #endregion
    }
}
