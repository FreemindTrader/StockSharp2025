using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting
{
    public class AreaSelection
    {
        private IComparable _x1, _x2;

        public ISciChartSurface Surface { get; set; }

        public void SetSelectionRect(double x1, double width)
        {
            var xCalc = Surface.XAxis?.GetCurrentCoordinateCalculator();

            if ( xCalc != null )
            {
                _x1 = xCalc.GetDataValue(x1);
                _x2 = xCalc.GetDataValue(x1 + width);
            }
        }

        ///<summary>
        /// Checks whether current <see cref="AreaSelection"/> contains a point with x coordinate
        ///</summary>
        public bool Contains(double x)
        {
            return x.CompareTo(_x1) >= 0 && x.CompareTo(_x2) <= 0;
        }
    }


    /// <summary>
    /// The following class is a base class for all palette providers in this example. It implements the IXxxPaletteProvider interface
    /// 
    /// Since this interface IXxxPaletteProvider is used in the old SciChart versions, I also implement IPaletteProvider interface for compatibility with the new SciChart versions
    /// </summary>
    public class MyPaletteProviderBase : DependencyObject, IPaletteProvider, ISSPaletteProvider
    {
        public static readonly DependencyProperty AreaSelectionProperty = DependencyProperty.Register("AreaSelection", typeof(AreaSelection), typeof(MyPaletteProviderBase), new PropertyMetadata(null));
        public virtual Color? GetColor(IRenderableSeries rSeries, double _param2, double _param3)
        {
            return null;
        }

        public void OnBeginSeriesDraw(IRenderableSeries rSeries)
        {
            
        }

        public virtual Color? OverrideColor(IRenderableSeries rSeries, double candleIndex, double openPrice, double highPrice, double lowPrice, double closePrice)
        {
            return null;
        }

        public virtual Color? OverrideColor(IRenderableSeries rSeries, double _param2, double _param3, double _param4)
        {
            return null;
        }

        public Color? PointMarkerStrokeOverride { get; set; }

        public Color? PointMarkerFillOverride { get; set; }

        public Color StrokeOverride { get; set; }

        public AreaSelection AreaSelection
        {
            get { return (AreaSelection)GetValue(AreaSelectionProperty); }
            set { SetValue(AreaSelectionProperty, value); }
        }

        protected bool IsColorOverridden(double xValue)
        {
            return AreaSelection != null && AreaSelection.Contains(xValue);
        }

    }
}
