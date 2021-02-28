using MiniMarket.Model.BusinessLogic;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class RaportZyskViewModel : WszystkieViewModel<MagazynForView>
    {


        #region Fields
        private MiniMarketEntities miniMarketEntities;
        private DateTime dataOd;
        private DateTime dataDo;
       

        private decimal? iloscPrzychod;
        private decimal? iloscRozchod;
        private decimal? przychodNetto;
        private decimal? rozchodNetto;
        private ObservableCollection<MagazynForView> _ListRozchodu;

        #endregion
        public RaportZyskViewModel()
        {
            base.DisplayName = "Analiza finansowa";
            miniMarketEntities = new MiniMarketEntities();
            dataDo = DateTime.Now;
            dataOd = DateTime.Now;

        }
        #region Properties
        public ObservableCollection<MagazynForView> ListaRozchodu
        {
            get
            {

                if (_ListRozchodu == null) load();
                return _ListRozchodu;

            }
            set
            {

                _ListRozchodu = value; OnPropertyChanged(() => ListaRozchodu);
            }
        }
        public decimal? IloscPrzychod
        {
            get
            {
                return iloscPrzychod;
            }


            set
            {
                if (value != iloscPrzychod)
                {
                    iloscPrzychod = value;
                    OnPropertyChanged(() => IloscPrzychod);
                }
            }
        }
        public decimal? IloscRozchod
        {
            get
            {
                return iloscRozchod;
            }


            set
            {
                if (value != iloscRozchod)
                {
                    iloscRozchod = value;
                    OnPropertyChanged(() => IloscRozchod);
                }
            }
        }
        public decimal? PrzychodNetto
        {
            get
            {
                return przychodNetto;
            }


            set
            {
                if (value != przychodNetto)
                {
                    przychodNetto = value;
                    OnPropertyChanged(() => PrzychodNetto);
                }
            }
        }
        public decimal? RozchodNetto
        {
            get
            {
                return rozchodNetto;
            }


            set
            {
                if (value != rozchodNetto)
                {
                    rozchodNetto = value;
                    OnPropertyChanged(() => RozchodNetto);
                }
            }
        }



        public DateTime DataOd
        {
            get
            {
                return dataOd;
            }


            set
            {
                if (value != dataOd)
                {
                    dataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }

        public DateTime DataDo
        {
            get
            {
                return dataDo;
            }


            set
            {
                if (value != dataDo)
                {
                    dataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }
        #endregion
        #region Helpers
        public override void delete()
        {
            throw new NotImplementedException();
        }

        public override void edit()
        {
            throw new NotImplementedException();
        }

        public override void find()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboboxFindList()
        {
            return null;
        }

        public override List<string> GetComboboxSortList()
        {
            return null;
        }

        public override void load()
        {
            ObliczClick();
            var listaSprzedazy = from towar in miniMarketEntities.TowaryIlosci
                        join spr in miniMarketEntities.FakturySprzedazyPozycje
                        on towar.Twr_IdTowaru equals spr.Fvs_PozycjeIdTowaru
                        where towar.FakturySprzedazy.FvsDataSprzedazy >= DataOd
                        && towar.FakturySprzedazy.FvsDataSprzedazy <= DataDo
                        select towar;
            var listaZakupu = from towar in miniMarketEntities.TowaryIlosci
                                 join spr in miniMarketEntities.FakturyZakupuPozycje
                                 on towar.Twr_IdTowaru equals spr.Fvz_PozycjeIdTowaru
                                 where towar.FakturyZakupu.FvzDataSprzedazy >= DataOd
                                 && towar.FakturyZakupu.FvzDataSprzedazy <= DataDo
                              select towar;
            

            List = new ObservableCollection<MagazynForView>
           (
           from towar in listaSprzedazy
           
           select new MagazynForView
           {
               Twr_IdTowaru = towar.Twr_IdTowaru,
               Twr_DataZmian = towar.Twr_DataZmian,
               Twr_WprowData = towar.Twr_WprowData,
               Twr_SprzedazIlosc = towar.Twr_SprzedazIlosc,
               Twr_ZakupIlosc = towar.Twr_ZakupIlosc,
               Twr_Nazwa = towar.Towary.Twr_Nazwa,
               Twr_JednPodst = towar.Towary.Twr_JednPodst,
               Twr_Kod = towar.Towary.Twr_Kod,
               Twr_NumerKat = towar.Towary.Twr_NumerKat,
               Twr_Grupa = towar.Towary.GrupyTowarowe.G_GRNazwa,
               Twr_KatSprzedazy = towar.Towary.Kategorie.K_Nazwa,
               Twr_KatZakupu = towar.Towary.Kategorie.K_Nazwa,
               Twr_VatSpr = towar.Towary.VatSprzedaz.VatSpr_Wartosc,
               Twr_VatZak = towar.Towary.VatZakup.VatZak_Wartosc,
               Twr_CenaZakNetto = towar.Twr_Wartosc,
               


           });
            ListaRozchodu = new ObservableCollection<MagazynForView>
          (
          from towar in listaZakupu

          select new MagazynForView
          {
              Twr_IdTowaru = towar.Twr_IdTowaru,
              Twr_DataZmian = towar.Twr_DataZmian,
              Twr_WprowData = towar.Twr_WprowData,
              Twr_SprzedazIlosc = towar.Twr_SprzedazIlosc,
              Twr_ZakupIlosc = towar.Twr_ZakupIlosc,
              Twr_Nazwa = towar.Towary.Twr_Nazwa,
              Twr_JednPodst = towar.Towary.Twr_JednPodst,
              Twr_Kod = towar.Towary.Twr_Kod,
              Twr_NumerKat = towar.Towary.Twr_NumerKat,
              Twr_Grupa = towar.Towary.GrupyTowarowe.G_GRNazwa,
              Twr_KatSprzedazy = towar.Towary.Kategorie.K_Nazwa,
              Twr_KatZakupu = towar.Towary.Kategorie.K_Nazwa,
              Twr_VatSpr = towar.Towary.VatSprzedaz.VatSpr_Wartosc,
              Twr_VatZak = towar.Towary.VatZakup.VatZak_Wartosc,
              Twr_CenaZakNetto = towar.Twr_Wartosc,



          });

        }

        public override void sort()
        {
            throw new NotImplementedException();
        }

        private void ObliczClick()
        {
            IloscPrzychod = new ZakresCenBusinessLogic(miniMarketEntities).IloscPrzychod( DataOd, DataDo);
            IloscRozchod = new ZakresCenBusinessLogic(miniMarketEntities).IloscRozchod(DataOd, DataDo);
            RozchodNetto = new ZakresCenBusinessLogic(miniMarketEntities).RozchodNetto(DataOd, DataDo);
            PrzychodNetto = new ZakresCenBusinessLogic(miniMarketEntities).PrzychodNetto(DataOd, DataDo);


        }
        #endregion
    }
}
