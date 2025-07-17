// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.RenderSeriesProxy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ecng.Xaml.Charting.ChartModifiers;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Axes;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    internal class RenderSeriesProxy : IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        private readonly IRenderableSeries _renderableSeries;

        public RenderSeriesProxy( IRenderableSeries renderableSeries )
        {
            this._renderableSeries = renderableSeries;
            this.IsVisible = renderableSeries.IsVisible;
            this.ResamplingMode = renderableSeries.ResamplingMode;
            this.DataSeries = renderableSeries.DataSeries;
            this.XAxisId = renderableSeries.XAxisId;
            this.YAxisId = renderableSeries.YAxisId;
            this.DisplaysDataAsXy = renderableSeries.DisplaysDataAsXy;
        }

        public double Width
        {
            get; set;
        }

        public double Height
        {
            get; set;
        }

        public void OnDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            this._renderableSeries.OnDraw( renderContext, renderPassData );
        }

        public event EventHandler SelectionChanged;

        public event EventHandler IsVisibleChanged;

        public IServiceContainer Services
        {
            get; set;
        }

        public bool IsVisible
        {
            get; set;
        }

        public bool AntiAliasing
        {
            get; set;
        }

        public ResamplingMode ResamplingMode
        {
            get; set;
        }

        public object PointSeriesArg
        {
            get;
        }

        public IDataSeries DataSeries
        {
            get; set;
        }

        public IDataSeries DataSeriesForCore
        {
            get; set;
        }

        public IAxis XAxis
        {
            get; set;
        }

        public IAxis YAxis
        {
            get; set;
        }

        public Color SeriesColor
        {
            get; set;
        }

        public Style SelectedSeriesStyle
        {
            get; set;
        }

        public Style Style
        {
            get; set;
        }

        public object DataContext
        {
            get; set;
        }

        public bool IsSelected
        {
            get; set;
        }

        public FrameworkElement RolloverMarker
        {
            get; private set;
        }

        public string YAxisId
        {
            get; set;
        }

        public string XAxisId
        {
            get; set;
        }

        public IRenderPassData CurrentRenderPassData
        {
            get; set;
        }

        public IPaletteProvider PaletteProvider
        {
            get; set;
        }

        public int StrokeThickness
        {
            get; set;
        }

        public HitTestInfo HitTest( Point rawPoint, bool interpolate, double? dataPointRadius )
        {
            throw new NotImplementedException();
        }

        public bool DisplaysDataAsXy
        {
            get; private set;
        }

        public HitTestInfo HitTest( Point rawPoint, bool interpolate = false )
        {
            throw new NotImplementedException();
        }

        public HitTestInfo HitTest( Point rawPoint, double hitTestRadius, bool interpolate = false )
        {
            throw new NotImplementedException();
        }

        public HitTestInfo VerticalSliceHitTest( Point rawPoint, bool interpolate = false )
        {
            throw new NotImplementedException();
        }

        public SeriesInfo GetSeriesInfo( HitTestInfo hitTestInfo )
        {
            throw new NotImplementedException();
        }

        public IRange GetXRange()
        {
            throw new NotImplementedException();
        }

        public IRange GetYRange( IRange xRange )
        {
            throw new NotImplementedException();
        }

        public IRange GetYRange( IRange xRange, bool getPositiveRange )
        {
            throw new NotImplementedException();
        }

        public virtual IndexRange GetExtendedXRange( IndexRange range )
        {
            throw new NotImplementedException();
        }

        public bool GetIncludeSeries( Modifier modifier )
        {
            throw new NotImplementedException();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml( XmlReader reader )
        {
            throw new NotImplementedException();
        }

        public void WriteXml( XmlWriter writer )
        {
            throw new NotImplementedException();
        }
    }
}
