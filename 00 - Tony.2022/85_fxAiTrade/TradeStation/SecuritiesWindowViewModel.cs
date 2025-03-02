using DevExpress.Mvvm;
using Ecng.Collections;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    class SecuritiesWindowViewModel : ViewModelBase, ISupportParameter
    {
        IEnumerable<Security> _selectedSecurites = new SynchronizedList<Security>();
        public IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return _selectedSecurites;
            }
        }

    }
}
