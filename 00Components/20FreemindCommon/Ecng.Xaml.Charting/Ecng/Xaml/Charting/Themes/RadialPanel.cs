// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Themes.RadialPanel
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ecng.Xaml.Charting.Common.Extensions;

namespace Ecng.Xaml.Charting.Themes
{
    public class RadialPanel : Panel
    {
        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached("Angle", typeof (double), typeof (RadialPanel), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(RadialPanel.InvalidateParentArrange)));
        public static readonly DependencyProperty IsHorizontalProperty = DependencyProperty.RegisterAttached("IsHorizontal", typeof (bool), typeof (RadialPanel), new PropertyMetadata((object) false));
        public static readonly DependencyProperty OriginPointProperty = DependencyProperty.RegisterAttached("OriginPoint", typeof (Point), typeof (RadialPanel), new PropertyMetadata((object) new Point(), new PropertyChangedCallback(RadialPanel.InvalidateParentArrange)));

        protected override Size MeasureOverride( Size availableSize )
        {
            Size size = this.GetSize(availableSize.Width, availableSize.Height);
            size.Width /= 2.0;
            size.Height /= 2.0;
            double val1 = 0.0;
            foreach ( UIElement uiElement in this.Children.OfType<UIElement>().Where<UIElement>( ( Func<UIElement, bool> ) ( x => x.IsVisible() ) ) )
            {
                uiElement.Measure( size );
                val1 = Math.Max( val1, Math.Max( uiElement.DesiredSize.Width, uiElement.DesiredSize.Height ) );
            }
            double num = val1 * 2.0;
            return new Size( num, num );
        }

        private Size GetSize( double width, double height )
        {
            width = Math.Max( width, 0.0 );
            height = Math.Max( height, 0.0 );
            return new Size( width, height );
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            Point point = new Point(finalSize.Width / 2.0, finalSize.Height / 2.0);
            foreach ( UIElement element in this.Children.OfType<UIElement>().Where<UIElement>( ( Func<UIElement, bool> ) ( x => x.IsVisible() ) ) )
            {
                Size desiredSize;
                Size size1;
                if ( !RadialPanel.GetIsHorizontal( element ) )
                {
                    desiredSize = element.DesiredSize;
                    size1 = new Size( desiredSize.Width, point.Y );
                }
                else
                {
                    double x = point.X;
                    desiredSize = element.DesiredSize;
                    double height = desiredSize.Height;
                    size1 = new Size( x, height );
                }
                Size size2 = size1;
                Point originPoint = RadialPanel.GetOriginPoint(element);
                Point location = new Point(point.X, point.Y);
                location.X -= size2.Width * originPoint.X;
                location.Y -= size2.Height * originPoint.Y;
                double angle = RadialPanel.GetAngle(element);
                RotateTransform rotateTransform = new RotateTransform() { Angle = angle, CenterX = originPoint.X, CenterY = originPoint.Y };
                element.RenderTransform = ( Transform ) rotateTransform;
                element.RenderTransformOrigin = originPoint;
                element.Arrange( new Rect( location, size2 ) );
            }
            return finalSize;
        }

        public static double GetAngle( UIElement element )
        {
            return ( double ) element.GetValue( RadialPanel.AngleProperty );
        }

        public static void SetAngle( UIElement element, double value )
        {
            element.SetValue( RadialPanel.AngleProperty, ( object ) value );
        }

        public static void SetOriginPoint( UIElement element, Point value )
        {
            element.SetValue( RadialPanel.OriginPointProperty, ( object ) value );
        }

        public static Point GetOriginPoint( UIElement element )
        {
            return ( Point ) element.GetValue( RadialPanel.OriginPointProperty );
        }

        public static void SetIsHorizontal( UIElement element, bool value )
        {
            element.SetValue( RadialPanel.IsHorizontalProperty, ( object ) value );
        }

        public static bool GetIsHorizontal( UIElement element )
        {
            return ( bool ) element.GetValue( RadialPanel.IsHorizontalProperty );
        }

        private static void InvalidateParentArrange( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UIElement uiElement = d as UIElement;
            if ( uiElement == null )
                return;
            ( VisualTreeHelper.GetParent( ( DependencyObject ) uiElement ) as RadialPanel )?.InvalidateArrange();
        }
    }
}
