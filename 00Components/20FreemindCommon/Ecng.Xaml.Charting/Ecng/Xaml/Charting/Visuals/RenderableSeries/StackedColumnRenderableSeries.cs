// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.StackedColumnRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{

    public class StackedColumnRenderableSeries : BaseColumnRenderableSeries, IStackedColumnRenderableSeries, IStackedRenderableSeries, IRenderableSeries, IRenderableSeriesBase, IDrawable, IXmlSerializable
    {
        public static readonly DependencyProperty StackedGroupIdProperty = DependencyProperty.Register(nameof (StackedGroupId), typeof (string), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) "DefaultStackedGroupId", new PropertyChangedCallback(StackedColumnRenderableSeries.StackedGroupIdPropertyChanged)));
        public static readonly DependencyProperty IsOneHundredPercentProperty = DependencyProperty.Register(nameof (IsOneHundredPercent), typeof (bool), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) false));
        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof (Spacing), typeof (double), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) 0.1, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty SpacingModeProperty = DependencyProperty.Register(nameof (SpacingMode), typeof (SpacingMode), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) SpacingMode.Relative, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty ShowLabelProperty = DependencyProperty.Register(nameof (ShowLabel), typeof (bool), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty LabelColorProperty = DependencyProperty.Register(nameof (LabelColor), typeof (Color), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) Colors.White));
        public static readonly DependencyProperty LabelFontSizeProperty = DependencyProperty.Register(nameof (LabelFontSize), typeof (float), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) 12f));
        public static readonly DependencyProperty LabelTextFormattingProperty = DependencyProperty.Register(nameof (LabelTextFormatting), typeof (string), typeof (StackedColumnRenderableSeries), new PropertyMetadata((object) "0.00"));

        public StackedColumnRenderableSeries()
        {
            this.DefaultStyleKey = ( object ) typeof( StackedColumnRenderableSeries );
        }

        public string StackedGroupId
        {
            get
            {
                return ( string ) this.GetValue( StackedColumnRenderableSeries.StackedGroupIdProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.StackedGroupIdProperty, ( object ) value );
            }
        }

        public bool IsOneHundredPercent
        {
            get
            {
                return ( bool ) this.GetValue( StackedColumnRenderableSeries.IsOneHundredPercentProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.IsOneHundredPercentProperty, ( object ) value );
                this.GetParentSurface()?.InvalidateElement();
            }
        }

        public double Spacing
        {
            get
            {
                return ( double ) this.GetValue( StackedColumnRenderableSeries.SpacingProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.SpacingProperty, ( object ) value );
            }
        }

        public SpacingMode SpacingMode
        {
            get
            {
                return ( SpacingMode ) this.GetValue( StackedColumnRenderableSeries.SpacingModeProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.SpacingModeProperty, ( object ) value );
            }
        }

        public bool ShowLabel
        {
            get
            {
                return ( bool ) this.GetValue( StackedColumnRenderableSeries.ShowLabelProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.ShowLabelProperty, ( object ) value );
            }
        }

        public Color LabelColor
        {
            get
            {
                return ( Color ) this.GetValue( StackedColumnRenderableSeries.LabelColorProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.LabelColorProperty, ( object ) value );
            }
        }

        public float LabelFontSize
        {
            get
            {
                return ( float ) this.GetValue( StackedColumnRenderableSeries.LabelFontSizeProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.LabelFontSizeProperty, ( object ) value );
            }
        }

        public string LabelTextFormatting
        {
            get
            {
                return ( string ) this.GetValue( StackedColumnRenderableSeries.LabelTextFormattingProperty );
            }
            set
            {
                this.SetValue( StackedColumnRenderableSeries.LabelTextFormattingProperty, ( object ) value );
            }
        }

        bool IStackedColumnRenderableSeries.IsValidForDrawing
        {
            get
            {
                return this.IsValidForDrawing;
            }
        }

        public IStackedColumnsWrapper Wrapper
        {
            get
            {
                return ( ( UltrachartSurface ) this.GetParentSurface() )?.StackedColumnsWrapper;
            }
        }

        public override IRange GetXRange()
        {
            return this.Wrapper.GetXRange( this.CurrentRenderPassData != null && this.CurrentRenderPassData.XCoordinateCalculator.IsLogarithmicAxisCalculator );
        }

        public override IRange GetYRange( IRange xRange, bool getPositiveRange )
        {
            if ( xRange == null )
            {
                throw new ArgumentNullException( nameof( xRange ) );
            }

            return ( IRange ) this.Wrapper.CalculateYRange( ( IStackedColumnRenderableSeries ) this, this.DataSeries.GetIndicesRange( xRange ) );
        }

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            this.Wrapper.DrawStackedSeries( renderContext );
        }

        public double GetChartRotationAngle()
        {
            return this.GetChartRotationAngle( this.CurrentRenderPassData );
        }

        protected override double GetSeriesBodyLowerDataBound( HitTestInfo nearestHitPoint )
        {
            Tuple<double, double> seriesVerticalBounds = this.Wrapper.GetSeriesVerticalBounds((IStackedColumnRenderableSeries) this, nearestHitPoint.DataSeriesIndex);
            return Math.Min( seriesVerticalBounds.Item1, seriesVerticalBounds.Item2 );
        }

        protected override double GetSeriesBodyUpperDataBound( HitTestInfo nearestHitPoint )
        {
            Tuple<double, double> seriesVerticalBounds = this.Wrapper.GetSeriesVerticalBounds((IStackedColumnRenderableSeries) this, nearestHitPoint.DataSeriesIndex);
            return Math.Max( seriesVerticalBounds.Item1, seriesVerticalBounds.Item2 );
        }

        protected override double GetSeriesBodyWidth( HitTestInfo nearestHitPoint )
        {
            return this.Wrapper.GetSeriesBodyWidth( ( IStackedColumnRenderableSeries ) this, nearestHitPoint.DataSeriesIndex );
        }

        protected override HitTestInfo HitTestInternal( Point rawPoint, double hitTestRadius, bool interpolate )
        {
            HitTestInfo hitTestInfo = HitTestInfo.Empty;
            if ( this.IsVisible )
            {
                HitTestInfo nearestHitResult = this.NearestHitResult(rawPoint, this.GetHitTestRadiusConsideringPointMarkerSize(hitTestRadius), SearchMode.Nearest, false);
                hitTestInfo = this.Wrapper.ShiftHitTestInfo( rawPoint, nearestHitResult, hitTestRadius, ( IStackedColumnRenderableSeries ) this );
            }
            return hitTestInfo;
        }

        private static void StackedGroupIdPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            StackedColumnRenderableSeries renderableSeries = d as StackedColumnRenderableSeries;
            if ( renderableSeries == null || renderableSeries.Wrapper == null )
            {
                return;
            }

            renderableSeries.Wrapper.MoveSeriesToAnotherGroup( ( IStackedColumnRenderableSeries ) renderableSeries, ( string ) e.OldValue, ( string ) e.NewValue );
        }

        [SpecialName]
        Style IRenderableSeries.Style
        {
            get
            {
                return this.Style;
            }

            set
            {
                this.Style = value;
            }
        }


        [SpecialName]
        object IRenderableSeries.DataContext
        {
            get
            {
                return this.DataContext;
            }

            set
            {
                this.DataContext = value;
            }

        }


    }
}
