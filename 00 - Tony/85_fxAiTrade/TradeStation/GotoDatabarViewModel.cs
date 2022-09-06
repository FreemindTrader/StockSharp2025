using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Docking.Base;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using fx.Definitions;
using fx.Common;
using fx.Definitions.UndoRedo;
using fx.Algorithm;
using Ecng.Collections;
using StockSharp.Logging;
using fx.Indicators;

namespace FreemindAITrade.ViewModels
{
    [POCOViewModel]
    public class GotoDatabarViewModel
    {
        public virtual int DatabarIndex { get; set; }

        public static GotoDatabarViewModel Create( )
        {
            return ViewModelSource.Create( ( ) => new GotoDatabarViewModel( ) );
        }
    }
}
