// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.RenderableSeriesExtension
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Linq;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class RenderableSeriesExtension
    {
        internal static DependencyProperty SeriesStyleProperty = DependencyProperty.RegisterAttached("SeriesStyle", typeof (Style), typeof (RenderableSeriesExtension), new PropertyMetadata((PropertyChangedCallback) null));

        internal static Style GetSeriesStyle( DependencyObject o )
        {
            return ( Style ) o.GetValue( RenderableSeriesExtension.SeriesStyleProperty );
        }

        internal static void SetSeriesStyle( DependencyObject o, Style value )
        {
            o.SetValue( RenderableSeriesExtension.SeriesStyleProperty, ( object ) value );
        }

        internal static void SetStyle( this BaseRenderableSeries series, Style style )
        {
            if ( style == null )
                return;
            Style style1 = new Style() { TargetType = typeof (BaseRenderableSeries) };
            foreach ( Setter setter in style.Setters.OfType<Setter>() )
            {
                DependencyProperty property = setter.Property;
                object obj = series.GetValue(property);
                style1.Setters.Add( ( SetterBase ) new Setter( property, obj ) );
                series.SetCurrentValue( property, setter.Value );
            }
            RenderableSeriesExtension.SetSeriesStyle( ( DependencyObject ) series, style1 );
        }

        internal static bool HasDigitalLine( this IRenderableSeries rSeries )
        {
            if ( rSeries is FastLineRenderableSeries && ( ( FastLineRenderableSeries ) rSeries ).IsDigitalLine || rSeries is FastMountainRenderableSeries && ( ( BaseMountainRenderableSeries ) rSeries ).IsDigitalLine )
                return true;
            if ( rSeries is FastBandRenderableSeries )
                return ( ( FastBandRenderableSeries ) rSeries ).IsDigitalLine;
            return false;
        }

        internal static SeriesInfo GetSeriesInfo( this IRenderableSeries rSeries, HitTestInfo hitTestInfo )
        {
            SeriesInfo seriesInfo;
            switch ( hitTestInfo.DataSeriesType )
            {
                case DataSeriesType.Ohlc:
                    seriesInfo = ( SeriesInfo ) new OhlcSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.Xyy:
                    seriesInfo = ( SeriesInfo ) new BandSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.Xyz:
                    seriesInfo = ( SeriesInfo ) new XyzSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.Hlc:
                    seriesInfo = ( SeriesInfo ) new HlcSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.StackedXy:
                    seriesInfo = ( SeriesInfo ) new XyStackedSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.Heatmap:
                    seriesInfo = ( SeriesInfo ) new HeatmapSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.OneHundredPercentStackedXy:
                    seriesInfo = ( SeriesInfo ) new OneHundredPercentStackedSeriesInfo( rSeries, hitTestInfo );
                    break;
                case DataSeriesType.TimeframeSegment:
                    seriesInfo = ( SeriesInfo ) new TimeframeSegmentSeriesInfo( rSeries, hitTestInfo );
                    break;
                default:
                    seriesInfo = ( SeriesInfo ) new XySeriesInfo( rSeries, hitTestInfo );
                    break;
            }
            return seriesInfo;
        }

        internal static Color GetSeriesColorAtPoint( this IRenderableSeries rSeries, HitTestInfo hitTestInfo )
        {
            IPaletteProvider paletteProvider = rSeries.PaletteProvider;
            Color color1 = rSeries.SeriesColor;
            FastCandlestickRenderableSeries renderableSeries1 = rSeries as FastCandlestickRenderableSeries;
            FastOhlcRenderableSeries renderableSeries2 = rSeries as FastOhlcRenderableSeries;
            if ( renderableSeries1 != null || renderableSeries2 != null )
            {
                Color color2 = renderableSeries1 != null ? renderableSeries1.UpWickColor : renderableSeries2.UpWickColor;
                Color color3 = renderableSeries1 != null ? renderableSeries1.DownWickColor : renderableSeries2.DownWickColor;
                color1 = hitTestInfo.CloseValue.CompareTo( ( object ) hitTestInfo.OpenValue ) >= 0 ? color2 : color3;
            }
            if ( rSeries.PaletteProvider != null )
            {
                Color? nullable1 = new Color?();
                Color? nullable2 = hitTestInfo.DataSeriesType != DataSeriesType.Ohlc ? (hitTestInfo.DataSeriesType != DataSeriesType.Xyz ? paletteProvider.GetColor(rSeries, hitTestInfo.XValue.ToDouble(), hitTestInfo.YValue.ToDouble()) : paletteProvider.OverrideColor(rSeries, hitTestInfo.XValue.ToDouble(), hitTestInfo.YValue.ToDouble(), hitTestInfo.ZValue.ToDouble())) : paletteProvider.OverrideColor(rSeries, hitTestInfo.XValue.ToDouble(), hitTestInfo.OpenValue.ToDouble(), hitTestInfo.HighValue.ToDouble(), hitTestInfo.LowValue.ToDouble(), hitTestInfo.CloseValue.ToDouble());
                color1 = nullable2.HasValue ? nullable2.Value : color1;
            }
            return color1;
        }
    }
}
