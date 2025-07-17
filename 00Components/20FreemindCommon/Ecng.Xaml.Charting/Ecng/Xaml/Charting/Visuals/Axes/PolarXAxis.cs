// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.PolarXAxis
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Charting.Numerics.CoordinateProviders;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.StrategyManager;
using StockSharp.Xaml.Charting.Themes;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public class PolarXAxis : NumericAxis
    {
        public PolarXAxis()
        {
            this.DefaultStyleKey = ( object ) typeof( PolarXAxis );
            this.TickCoordinatesProvider = ( ITickCoordinatesProvider ) new PolarTickCoordinatesProvider();
        }

        public override bool IsPolarAxis
        {
            get
            {
                return true;
            }
        }

        public override bool IsHorizontalAxis
        {
            get
            {
                return true;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.AxisContainer.SizeChanged += ( SizeChangedEventHandler ) ( ( sender, args ) => PolarPanel.SetThickness( ( UIElement ) this.AxisContainer, this.AxisAlignment == AxisAlignment.Top || this.AxisAlignment == AxisAlignment.Bottom ? this.AxisContainer.ActualHeight : this.AxisContainer.ActualWidth ) );
        }

        public override AxisParams GetAxisParams()
        {
            AxisParams axisParams = base.GetAxisParams();
            axisParams.IsPolarAxis = this.IsPolarAxis;
            return axisParams;
        }

        protected override double GetOffsetForLabels()
        {
            return 0.0;
        }

        protected override void DrawGridLine( IRenderContext2D renderContext, Style gridLineStyle, IEnumerable<float> coordsToDraw )
        {
            this.LineToStyle.Style = gridLineStyle;
            ThemeManager.SetTheme( ( DependencyObject ) this.LineToStyle, ThemeManager.GetTheme( ( DependencyObject ) this ) );
            using ( IPen2D styledPen = renderContext.GetStyledPen( this.LineToStyle, true ) )
            {
                if ( !this.IsXAxis )
                    return;
                float[] array = coordsToDraw.ToArray<float>();
                ITransformationStrategy transformationStrategy = this.Services.GetService<IStrategyManager>().GetTransformationStrategy();
                double viewportRadius = PolarUtil.CalculateViewportRadius(transformationStrategy.ViewportSize);
                Point pt1 = transformationStrategy.ReverseTransform(new Point(0.0, 0.0));
                for ( int index = 0 ; index < array.Length ; ++index )
                {
                    Point pt2 = transformationStrategy.ReverseTransform(new Point((double) array[index], viewportRadius));
                    renderContext.DrawLine( styledPen, pt1, pt2 );
                }
            }
        }

        public override double GetAxisOffset()
        {
            return 0.0;
        }

        protected override Point GetLabelPosition( float offset, float coords )
        {
            return new Point( ( double ) coords, ( double ) offset );
        }

        public override AxisInfo HitTest( IComparable dataValue )
        {
            AxisInfo axisInfo = base.HitTest(dataValue);
            axisInfo.AxisAlignment = AxisAlignment.Top;
            axisInfo.IsHorizontal = true;
            return axisInfo;
        }
    }
}
