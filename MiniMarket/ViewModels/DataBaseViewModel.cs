using MiniMarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.ViewModels
{
    public class DataBaseViewModel : WorkspaceViewModel
    {
        #region Fields
        protected MiniMarketEntities miniMarketEntities;
        #endregion
        #region Constructor
        public DataBaseViewModel()
        {
            this.miniMarketEntities = new MiniMarketEntities();
        }
        #endregion
    }
}
