// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IRenderableSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
namespace Ecng.Xaml.Charting
{
    public interface IRenderableSeries : IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        event EventHandler SelectionChanged;

        event EventHandler IsVisibleChanged;

        bool IsVisible
        {
            get; set;
        }

        bool AntiAliasing
        {
            get; set;
        }

        Color SeriesColor
        {
            get; set;
        }

        bool IsSelected
        {
            get; set;
        }

        int StrokeThickness
        {
            get; set;
        }

        ResamplingMode ResamplingMode
        {
            get; set;
        }

        object PointSeriesArg
        {
            get;
        }

        IDataSeries DataSeries
        {
            get; set;
        }

        IAxis XAxis
        {
            get; set;
        }

        IAxis YAxis
        {
            get; set;
        }

        Style SelectedSeriesStyle
        {
            get; set;
        }

        Style Style
        {
            get; set;
        }

        object DataContext
        {
            get; set;
        }

        FrameworkElement RolloverMarker
        {
            get;
        }

        string YAxisId
        {
            get; set;
        }

        string XAxisId
        {
            get; set;
        }

        IRenderPassData CurrentRenderPassData
        {
            get; set;
        }

        IPaletteProvider PaletteProvider
        {
            get; set;
        }

        bool DisplaysDataAsXy
        {
            get;
        }

        HitTestInfo HitTest( Point rawPoint, bool interpolate = false );

        HitTestInfo HitTest( Point rawPoint, double hitTestRadius, bool interpolate = false );

        HitTestInfo VerticalSliceHitTest( Point rawPoint, bool interpolate = false );

        SeriesInfo GetSeriesInfo( HitTestInfo hitTestInfo );

        IRange GetXRange();

        IRange GetYRange( IRange xRange );

        IRange GetYRange( IRange xRange, bool getPositiveRange );

        IndexRange GetExtendedXRange( IndexRange IndexRange );

        bool GetIncludeSeries( Modifier modifier );
    }
}
