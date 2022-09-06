using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreemindAITrade.ViewModels
{
    [POCOViewModel]
    public class TradeControlViewModel : ISupportParentViewModel
    {
        TradeStationViewModel _parent;
        public virtual object ParentViewModel { get; set; }

        protected void OnParentViewModelChanged()
        {
            _parent = ( ( TradeStationViewModel ) ParentViewModel );

           
        }
    }
}
