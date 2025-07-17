// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Themes.PolarTickLabelAxisCanvas
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Common.Helpers;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Themes
{
    public class PolarTickLabelAxisCanvas : TickLabelAxisCanvas
    {
        public static readonly DependencyProperty MaxChildSizeProperty = DependencyProperty.Register(nameof (MaxChildSize), typeof (double), typeof (PolarTickLabelAxisCanvas), new PropertyMetadata((object) 0.0));
        private PolarCartesianTransformationHelper _transformationHelpser;
        private double _radius;

        public double MaxChildSize
        {
            get
            {
                return ( double ) this.GetValue( PolarTickLabelAxisCanvas.MaxChildSizeProperty );
            }
            set
            {
                this.SetValue( PolarTickLabelAxisCanvas.MaxChildSizeProperty, ( object ) value );
            }
        }

        protected override Size MeasureOverride( Size constraint )
        {
            Size size = base.MeasureOverride(constraint);
            this.MaxChildSize = this.GetMaxChildSize();
            return size;
        }

        private double GetMaxChildSize()
        {
            return this.Children.OfType<UIElement>().Select<UIElement, double>( ( Func<UIElement, double> ) ( x => x.DesiredSize.Height ) ).MaxOrNullable<double>() ?? 0.0;
        }

        protected override Size ArrangeOverride( Size arrangeSize )
        {
            this._transformationHelpser = new PolarCartesianTransformationHelper( arrangeSize.Width, arrangeSize.Height );
            this._radius = PolarUtil.CalculateViewportRadius( arrangeSize );
            return base.ArrangeOverride( arrangeSize );
        }

        protected override Rect GetArrangedRect( Size arrangeSize, UIElement element )
        {
            Rect rect = Rect.Empty;
            DefaultTickLabel defaultTickLabel = element as DefaultTickLabel;
            if ( defaultTickLabel != null )
            {
                double x1 = defaultTickLabel.Position.X;
                double r = this._radius - this.MaxChildSize / 2.0;
                Point cartesian = this._transformationHelpser.ToCartesian(x1, r);
                double x2 = cartesian.X - element.DesiredSize.Width / 2.0;
                double y = cartesian.Y - element.DesiredSize.Height / 2.0;
                RotateTransform rotateTransform = new RotateTransform() { Angle = x1 + 90.0, CenterX = 0.5, CenterY = 0.5 };
                element.RenderTransform = ( Transform ) rotateTransform;
                element.RenderTransformOrigin = new Point( 0.5, 0.5 );
                rect = new Rect( new Point( x2, y ), element.DesiredSize );
            }
            return rect;
        }
    }
}
