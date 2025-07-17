// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.XyScatterRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Visuals.PointMarkers;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    [UltrachartLicenseProvider( typeof( RenderableSeriesUltrachartLicenseProvider ) )]
    public class XyScatterRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty DoClusterResamplingProperty = DependencyProperty.Register(nameof (DoClusterResampling), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata((object) false));
        private const int _binsWidth = 400;
        private const int _binsHeight = 300;
        private static byte[] _bins;

        public bool DoClusterResampling
        {
            get
            {
                return ( bool ) GetValue( XyScatterRenderableSeries.DoClusterResamplingProperty );
            }
            set
            {
                SetValue( XyScatterRenderableSeries.DoClusterResamplingProperty, ( object ) value );
            }
        }

        public override bool DisplaysDataAsXy
        {
            get
            {
                return true;
            }
        }

        public XyScatterRenderableSeries()
        {
            DefaultStyleKey = ( object ) typeof( XyScatterRenderableSeries );
            SetCurrentValue( BaseRenderableSeries.ResamplingModeProperty, ( object ) ResamplingMode.None );
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            IPointSeries pointSeries = CurrentRenderPassData.PointSeries;
            IRenderPassData currentRenderPassData = CurrentRenderPassData;
            IPointMarker pointMarker = GetPointMarker();
            if ( pointMarker == null )
                return;
            IPaletteProvider paletteProvider = PaletteProvider;
            Color seriesColor = SeriesColor;
            IPathContextFactory markerPathFactory = SeriesDrawingHelpersFactory.GetPointMarkerPathFactory(renderContext, CurrentRenderPassData, pointMarker);
            if ( paletteProvider != null )
            {
                PenManager penManager = new PenManager(renderContext, AntiAliasing, (float) StrokeThickness, Opacity, (double[]) null);
                try
                {
                    Func<double, double, IPen2D> func = (Func<double, double, IPen2D>) ((x, y) => penManager.GetPen(PaletteProvider.GetColor((IRenderableSeries) this, x, y) ?? seriesColor));
                    FastPointsHelper.IteratePoints( markerPathFactory, ( Func<double, double, IPathColor> ) func, pointSeries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator );
                }
                finally
                {
                    if ( penManager != null )
                        penManager.Dispose();
                }
            }
            else
                FastPointsHelper.IteratePoints( markerPathFactory, pointSeries, currentRenderPassData.XCoordinateCalculator, currentRenderPassData.YCoordinateCalculator );
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            return base.HitTestInternal( rawPoint, GetHitTestRadiusConsideringPointMarkerSize( hitTestRadius ), false );
        }
    }
}
