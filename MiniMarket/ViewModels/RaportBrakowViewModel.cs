using MiniMarket.Helper;
using MiniMarket.Model.BusinessLogic;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniMarket.ViewModels
{
    public class RaportBrakowViewModel : WszystkieViewModel<MagazynForView>
    {
        #region Fields
        private MiniMarketEntities miniMarketEntities;
        private decimal? cenaOd;
        private decimal? cenaDo;
        private int idKategori;

        private decimal? ilosc;
        private decimal? srednia;
        private decimal? suma;
        
        #endregion
        public RaportBrakowViewModel()
        {
            base.DisplayName = "Przedział cen";
            miniMarketEntities = new MiniMarketEntities();
            
        }
        #region Properties
        public decimal? Ilosc
        {
            get
            {
                return ilosc;
            }


            set
            {
                if (value != ilosc)
                {
                    ilosc = value;
                    OnPropertyChanged(() => Ilosc);
                }
            }
        }
        public decimal? Srednia
        {
            get
            {
                return  srednia;
            }


            set
            {
                if (value != srednia)
                {
                     srednia = value;
                    OnPropertyChanged(() => Srednia);
                }
            }
        }
        public decimal? Suma
        {
            get
            {
                return suma;
            }


            set
            {
                if (value != suma)
                {
                    suma = value;
                    OnPropertyChanged(() => Suma);
                }
            }
        }
        
        public int IdKategori
        {
            get
            {
                return idKategori;
            }


            set
            {
                if (value != idKategori)
                {
                    idKategori = value;
                    OnPropertyChanged(() => idKategori);
                }
            }
        }
        public IQueryable<ComboBoxKeyAndValue> KategorieComboBoxItems
        {
            get
            {
                return new KategoriaBusinessLogic(miniMarketEntities).GetKategorieComboBoxItems();
            }
        }
        public decimal? CenaOd
        {
            get
            {
                return cenaOd;
            }


            set
            {
                if (value != cenaOd)
                {
                    cenaOd = value;
                    OnPropertyChanged(() => cenaOd);
                }
            }
        }
        public decimal? CenaDo
        {
            get
            {
                return cenaDo;
            }


            set
            {
                if (value != cenaDo)
                {
                    cenaDo = value;
                    OnPropertyChanged(() => cenaDo);
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
            List = new ObservableCollection<MagazynForView>
            (
            from towar in miniMarketEntities.TowaryIlosci
            where towar.Towary.Twr_IdKategoria == IdKategori
            && towar.Twr_Wartosc>= CenaOd
            && towar.Twr_Wartosc <= CenaDo
           && towar.CzyAktywne == true
           && towar.Twr_ZakupIlosc !=null

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
            Ilosc = new ZakresCenBusinessLogic(miniMarketEntities).ZakresCenIlosc(IdKategori, CenaOd, CenaDo);
            Suma =  new ZakresCenBusinessLogic(miniMarketEntities).ZakresCenSuma(IdKategori, CenaOd, CenaDo);
            Srednia = new ZakresCenBusinessLogic(miniMarketEntities).ZakresCenSrednia(IdKategori, CenaOd, CenaDo);

        }
        #endregion

    }
}
