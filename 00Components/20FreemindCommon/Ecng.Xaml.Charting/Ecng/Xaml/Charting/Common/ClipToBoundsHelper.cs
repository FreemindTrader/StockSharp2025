// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.ClipToBoundsHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Media;

namespace Ecng.Xaml.Charting.Common
{
    public static class ClipToBoundsHelper
    {
        public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached("ClipToBounds", typeof (bool), typeof (ClipToBoundsHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(ClipToBoundsHelper.OnClipToBoundsPropertyChanged)));
        public static readonly DependencyProperty ClipToEllipseBoundsProperty = DependencyProperty.RegisterAttached("ClipToEllipseBounds", typeof (bool), typeof (ClipToBoundsHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(ClipToBoundsHelper.OnClipToEllipseBoundsPropertyChanged)));

        public static bool GetClipToBounds( DependencyObject depObj )
        {
            return ( bool ) depObj.GetValue( ClipToBoundsHelper.ClipToBoundsProperty );
        }

        public static void SetClipToBounds( DependencyObject depObj, bool clipToBounds )
        {
            depObj.SetValue( ClipToBoundsHelper.ClipToBoundsProperty, ( object ) clipToBounds );
        }

        public static bool GetClipToEllipseBounds( DependencyObject depObj )
        {
            return ( bool ) depObj.GetValue( ClipToBoundsHelper.ClipToEllipseBoundsProperty );
        }

        public static void SetClipToEllipseBounds( DependencyObject depObj, bool value )
        {
            depObj.SetValue( ClipToBoundsHelper.ClipToEllipseBoundsProperty, ( object ) value );
        }

        private static void OnClipToBoundsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            FrameworkElement frameworkElement = d as FrameworkElement;
            if ( frameworkElement == null )
                return;
            frameworkElement.ClipToBounds = ( bool ) e.NewValue;
        }

        private static void ClipToBounds( FrameworkElement element )
        {
            if ( ClipToBoundsHelper.GetClipToBounds( ( DependencyObject ) element ) )
                element.Clip = ( Geometry ) new RectangleGeometry()
                {
                    Rect = new Rect( 0.0, 0.0, element.ActualWidth, element.ActualHeight )
                };
            else
                element.Clip = ( Geometry ) null;
        }

        private static void OnClipToEllipseBoundsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            FrameworkElement element = d as FrameworkElement;
            if ( element == null )
                return;
            ClipToBoundsHelper.ClipToEllipseBounds( element );
            element.Loaded += ( RoutedEventHandler ) ( ( sender, args ) => ClipToBoundsHelper.ClipToEllipseBounds( sender as FrameworkElement ) );
            element.SizeChanged += ( SizeChangedEventHandler ) ( ( sender, args ) => ClipToBoundsHelper.ClipToEllipseBounds( sender as FrameworkElement ) );
        }

        private static void ClipToEllipseBounds( FrameworkElement element )
        {
            if ( ClipToBoundsHelper.GetClipToEllipseBounds( ( DependencyObject ) element ) )
            {
                double x = element.ActualWidth / 2.0;
                double y = element.ActualHeight / 2.0;
                element.Clip = ( Geometry ) new EllipseGeometry()
                {
                    Center = new Point( x, y ),
                    RadiusX = x,
                    RadiusY = y
                };
            }
            else
                element.Clip = ( Geometry ) null;
        }
    }
}
