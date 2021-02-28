using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.BusinessLogic
{
    public class KategoriaBusinessLogic : DataBaseClass
    {
        #region Constructor
        public KategoriaBusinessLogic(MiniMarketEntities miniMarketEntities) : base(miniMarketEntities)
        {

        }
        #endregion
        #region ViewFunction
        public IQueryable<ComboBoxKeyAndValue> GetKategorieComboBoxItems()
        {
            return
                (
                from kat in miniMarketEntities.Kategorie
                select new ComboBoxKeyAndValue
                {
                    Key = kat.K_IGKId,
                    Value = kat.K_Nazwa
                }
                ).ToList().AsQueryable();
        }
        #endregion

    }
}
