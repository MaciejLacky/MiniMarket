using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.BusinessLogic
{
    public class ZakresCenBusinessLogic : DataBaseClass
    {
        public ZakresCenBusinessLogic(MiniMarketEntities miniMarketEntities):base(miniMarketEntities)
        {

        }

        public decimal? ZakresCenIlosc(int idKategori, decimal? CenadOd, decimal? CenaDo)
        {
            return
                (
                    from towar in miniMarketEntities.TowaryIlosci
                    where towar.Towary.Kategorie.K_IGKId == idKategori && towar.Twr_Wartosc >= CenadOd
                    && towar.Twr_Wartosc <= CenaDo && towar.CzyAktywne == true && towar.Twr_SprzedazIlosc == null
                   
                    select towar.Twr_ZakupIlosc


                ).Sum();
        }
        public decimal? ZakresCenSuma(int idKategori, decimal? CenadOd, decimal? CenaDo)
        {
            return
                (
                    from towar in miniMarketEntities.TowaryIlosci
                    where towar.Towary.Kategorie.K_IGKId == idKategori && towar.Twr_Wartosc >= CenadOd
                    && towar.Twr_Wartosc <= CenaDo && towar.CzyAktywne == true && towar.Twr_SprzedazIlosc == null
                    select towar.Twr_Wartosc * towar.Twr_ZakupIlosc


                ).Sum();
        }
        public decimal? ZakresCenSrednia(int idKategori, decimal? CenadOd, decimal? CenaDo)
        {
            return
                (
                     from towar in miniMarketEntities.TowaryIlosci
                     where towar.Towary.Kategorie.K_IGKId == idKategori && towar.Twr_Wartosc >= CenadOd
                     && towar.Twr_Wartosc <= CenaDo && towar.CzyAktywne == true && towar.Twr_SprzedazIlosc == null
                     select towar.Twr_Wartosc * towar.Twr_ZakupIlosc


                ).Average();
        }
        public decimal? ZakresCenIloscSprzedaz(int idKategori, decimal? CenadOd, decimal? CenaDo)
        {
            return
                (
                    from towar in miniMarketEntities.TowaryIlosci
                    where towar.Towary.Kategorie.K_IGKId == idKategori && towar.Twr_Wartosc >= CenadOd
                    && towar.Twr_Wartosc <= CenaDo && towar.CzyAktywne == true && towar.Twr_SprzedazIlosc != null
                    select towar.Twr_SprzedazIlosc


                ).Sum();
        }
        public decimal? ZakresCenSumaSprzedaz(int idKategori, decimal? CenadOd, decimal? CenaDo)
        {
            return
                (
                    from towar in miniMarketEntities.TowaryIlosci
                    where towar.Towary.Kategorie.K_IGKId == idKategori && towar.Twr_Wartosc >= CenadOd
                    && towar.Twr_Wartosc <= CenaDo && towar.CzyAktywne == true && towar.Twr_SprzedazIlosc!=null
                    select towar.Twr_Wartosc * towar.Twr_SprzedazIlosc


                ).Sum();
        }
        public decimal? ZakresCenSredniaSprzedaz(int idKategori, decimal? CenadOd, decimal? CenaDo)
        {
            return
                (
                    from towar in miniMarketEntities.TowaryIlosci
                    where towar.Towary.Kategorie.K_IGKId == idKategori && towar.Twr_Wartosc >= CenadOd
                    && towar.Twr_Wartosc <= CenaDo && towar.CzyAktywne == true && towar.Twr_SprzedazIlosc != null
                    select towar.Twr_Wartosc * towar.Twr_SprzedazIlosc


                ).Average();
        }
        public decimal? IloscRozchod(DateTime dataOd, DateTime dataDo)
        {
            return
                (
                from towar in miniMarketEntities.TowaryIlosci
                join spr in miniMarketEntities.FakturySprzedazyPozycje
                on towar.Twr_IdTowaru equals spr.Fvs_PozycjeIdTowaru
                where towar.FakturySprzedazy.FvsDataSprzedazy >= dataOd
                && towar.FakturySprzedazy.FvsDataSprzedazy <= dataDo
                select towar.Twr_SprzedazIlosc
                ).Sum();
        }
        public decimal? RozchodNetto(DateTime dataOd, DateTime dataDo)
        {
            return
                (
                from towar in miniMarketEntities.TowaryIlosci
                join spr in miniMarketEntities.FakturySprzedazyPozycje
                on towar.Twr_IdTowaru equals spr.Fvs_PozycjeIdTowaru
                where towar.FakturySprzedazy.FvsDataSprzedazy >= dataOd
                && towar.FakturySprzedazy.FvsDataSprzedazy <= dataDo
                select towar.Twr_SprzedazIlosc* towar.Twr_Wartosc
                ).Sum();
        }
        public decimal? IloscPrzychod(DateTime dataOd, DateTime dataDo)
        {
            return
                (
                from towar in miniMarketEntities.TowaryIlosci
                join spr in miniMarketEntities.FakturyZakupuPozycje
                on towar.Twr_IdTowaru equals spr.Fvz_PozycjeIdTowaru
                where towar.FakturyZakupu.FvzDataSprzedazy >= dataOd
                && towar.FakturyZakupu.FvzDataSprzedazy <= dataDo
                select towar.Twr_ZakupIlosc
                ).Sum();
        }
        public decimal? PrzychodNetto(DateTime dataOd, DateTime dataDo)
        {
            return
                (
                from towar in miniMarketEntities.TowaryIlosci
                join spr in miniMarketEntities.FakturyZakupuPozycje
                on towar.Twr_IdTowaru equals spr.Fvz_PozycjeIdTowaru
                where towar.FakturyZakupu.FvzDataSprzedazy >= dataOd
                && towar.FakturyZakupu.FvzDataSprzedazy <= dataDo
                select towar.Twr_ZakupIlosc * towar.Twr_Wartosc
                ).Sum();
        }
    }
}
