using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    [POCOViewModel]
    public class GotoDatabarViewModel
    {
        public virtual int DatabarIndex { get; set; }

        public static GotoDatabarViewModel Create()
        {
            return ViewModelSource.Create( () => new GotoDatabarViewModel() );
        }
    }
}
