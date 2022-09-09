using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    [POCOViewModel]
    public class TradeControlViewModel : ISupportParentViewModel
    {
        TradeStationViewModel _parent;
        public virtual object ParentViewModel { get; set; }

        protected void OnParentViewModelChanged()
        {
            _parent = ( ( TradeStationViewModel )ParentViewModel );


        }
    }
}
