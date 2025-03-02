using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using fx.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using fx.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using fx.Charting.HewFibonacci;
using fx.Definitions;
using SciChart.Core.Extensions;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Data.Model;


namespace fx.Charting
{
    public partial class ChartExViewModel : DevExpress.Mvvm.ViewModelBase, IChart, IPersistable, IThemeableChart
    {        
        public void ResearchPastTA( bool show )
        {
            var selectionModifer = ( fxDataPointSelectionModifier ) _drawSurface.PointSelectionModifier;
            selectionModifer.SendMessageWhenSelectionChange = true;
        }
    }
}