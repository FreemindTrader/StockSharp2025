using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Utility;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using StockSharp.Localization;
using StockSharp.Messages;
using fx.Charting;
using fx.Charting.ATony;
using fx.Charting.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace fx.Charting
{
    internal partial class CandlestickVM : UIHigherVM<CandlestickUI>, IPaletteProvider, IStrokePaletteProvider, IFillPaletteProvider, INullBar
    {
        public void ShowLessWaves( )
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.ShowLessWaves( );
            }
        }

        public void ShowMoreWaves( )
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.ShowMoreWaves( );
            }
        }
    }
}
