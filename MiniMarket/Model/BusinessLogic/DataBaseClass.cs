using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.BusinessLogic
{
    public class DataBaseClass
    {
        // ta klasa umożliwa dostęp do bazy danych
        #region Fields
        protected MiniMarketEntities miniMarketEntities;
        #endregion
        #region Constructor
        public DataBaseClass(MiniMarketEntities miniMarketEntities)
        {
            this.miniMarketEntities = miniMarketEntities;
        }
        #endregion
    }
}
