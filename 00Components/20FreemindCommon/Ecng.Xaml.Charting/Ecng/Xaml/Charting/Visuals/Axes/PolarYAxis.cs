// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PolarYAxis
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    public class PolarYAxis : NumericAxis
    {
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof (Angle), typeof (double), typeof (PolarYAxis), new PropertyMetadata((object) 0.0));

        public PolarYAxis()
        {
            DefaultStyleKey = ( object ) typeof( PolarYAxis );
        }

        public double Angle
        {
            get
            {
                return ( double ) GetValue( PolarYAxis.AngleProperty );
            }
            set
            {
                SetValue( PolarYAxis.AngleProperty, ( object ) value );
            }
        }

        public override bool IsPolarAxis
        {
            get
            {
                return true;
            }
        }

        protected override void DrawGridLine( IRenderContext2D renderContext, Style gridLineStyle, IEnumerable<float> coordsToDraw )
        {
            LineToStyle.Style = gridLineStyle;
            ThemeManager.SetTheme( ( DependencyObject ) LineToStyle, ThemeManager.GetTheme( ( DependencyObject ) this ) );

            using ( IPen2D styledPen = renderContext.GetStyledPen( LineToStyle, false ) )
            {
                if ( IsXAxis )
                    return;

                Point center = Services.GetService<IStrategyManager>().GetTransformationStrategy().ReverseTransform(new Point(0.0, 0.0));
                SolidColorBrush solidColorBrush = new SolidColorBrush();

                using ( IBrush2D brush = renderContext.CreateBrush( ( Brush ) solidColorBrush, 1.0, TextureMappingMode.PerPrimitive ) )
                {
                    foreach ( float num in coordsToDraw )
                        renderContext.DrawEllipse( styledPen, brush, center, ( double ) num * 2.0, ( double ) num * 2.0 );
                }
            }
        }

        public override double GetAxisOffset()
        {
            return 0.0;
        }

        public override AxisParams GetAxisParams()
        {
            AxisParams axisParams = base.GetAxisParams();
            axisParams.IsPolarAxis = IsPolarAxis;
            if ( Math.Abs( IsHorizontalAxis ? ActualWidth : ActualHeight ) < double.Epsilon )
                axisParams.Size /= 2.0;
            return axisParams;
        }
    }
}
