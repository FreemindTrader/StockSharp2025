// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.ElementExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class ElementExtensions
    {
        internal static void MeasureArrange( this UIElement element )
        {
            element.Measure( new Size( double.PositiveInfinity, double.PositiveInfinity ) );
            element.Arrange( new Rect( new Point( 0.0, 0.0 ), element.DesiredSize ) );
        }

        internal static bool IsVisible( this UIElement element )
        {
            return element.Visibility == Visibility.Visible;
        }

        internal static bool IsInVisualTree( this DependencyObject element )
        {
            Window mainWindow = Application.Current.MainWindow;
            return element.IsInVisualTree( ( DependencyObject ) mainWindow );
        }

        internal static bool IsInVisualTree( this DependencyObject element, DependencyObject ancestor )
        {
            for ( DependencyObject reference = element ; reference != null ; reference = VisualTreeHelper.GetParent( reference ) )
            {
                if ( reference == ancestor )
                {
                    return true;
                }
            }
            return false;
        }

        internal static Point TranslatePoint( this FrameworkElement element, Point point, IHitTestable relativeTo )
        {
            UIElement relativeTo1 = relativeTo as UIElement;
            if ( relativeTo1 != null )
            {
                return element.TranslatePoint( point, relativeTo1 );
            }

            return new Point( 0.0, 0.0 );
        }

        internal static bool IsPointWithinBounds( this FrameworkElement element, Point point )
        {
            Point point1 = element.TranslatePoint(point, (UIElement) element);
            if ( point1.X <= element.ActualWidth && point1.X >= 0.0 && point1.Y <= element.ActualHeight )
            {
                return point1.Y >= 0.0;
            }

            return false;
        }

        internal static Rect GetBoundsRelativeTo( this FrameworkElement element, IHitTestable relativeTo )
        {
            UIElement otherElement = relativeTo as UIElement;
            Rect? nullable = new Rect?();
            if ( otherElement != null )
            {
                nullable = element.GetBoundsRelativeTo( otherElement );
            }

            if ( !nullable.HasValue )
            {
                return Rect.Empty;
            }

            return nullable.Value;
        }

        internal static Rect? GetBoundsRelativeTo( this FrameworkElement element, UIElement otherElement )
        {
            try
            {
                GeneralTransform visual = element.TransformToVisual((Visual) otherElement);
                if ( visual != null )
                {
                    Point result1;
                    if ( visual.TryTransform( new Point(), out result1 ) )
                    {
                        Point result2;
                        if ( visual.TryTransform( new Point( element.ActualWidth, element.ActualHeight ), out result2 ) )
                        {
                            return new Rect?( new Rect( result1, result2 ) );
                        }
                    }
                }
            }
            catch
            {
            }
            return new Rect?();
        }

        public static T FindLogicalParent<T>( this FrameworkElement frameworkElement ) where T : FrameworkElement
        {
            FrameworkElement parent = frameworkElement.Parent as FrameworkElement;
            if ( parent == null || parent is T )
            {
                return ( T ) parent;
            }

            return parent.FindLogicalParent<T>();
        }

        public static T FindVisualParent<T>( this UIElement element ) where T : UIElement
        {
            for ( UIElement uiElement = element ; uiElement != null ; uiElement = VisualTreeHelper.GetParent( ( DependencyObject ) uiElement ) as UIElement )
            {
                T obj = uiElement as T;
                if ( ( object ) obj != null )
                {
                    return obj;
                }
            }
            return default( T );
        }

        public static WriteableBitmap RenderToBitmap( this FrameworkElement element )
        {
            element.Measure( new Size( double.PositiveInfinity, double.PositiveInfinity ) );
            element.Arrange( new Rect( new Point( 0.0, 0.0 ), element.DesiredSize ) );
            Size desiredSize = element.DesiredSize;
            int width = (int) desiredSize.Width;
            desiredSize = element.DesiredSize;
            int height = (int) desiredSize.Height;
            double dpiX = 96.0;
            double dpiY = 96.0;
            PixelFormat pbgra32 = PixelFormats.Pbgra32;
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, dpiX, dpiY, pbgra32);
            renderTargetBitmap.Render( ( Visual ) element );
            return new WriteableBitmap( ( BitmapSource ) renderTargetBitmap );
        }

        public static WriteableBitmap RenderToBitmap( this FrameworkElement element, int width, int height )
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32);
            renderTargetBitmap.Render( ( Visual ) element );
            return new WriteableBitmap( ( BitmapSource ) renderTargetBitmap );
        }

        public static void RegisterForNotification( this FrameworkElement element, string propertyName, PropertyChangedCallback callback )
        {
            Binding binding = new Binding(propertyName)
            {
                Source = (object) element
            };
            DependencyProperty dp = DependencyProperty.RegisterAttached("ListenAttached" + propertyName, typeof (object), element.GetType(), new PropertyMetadata(callback));
            element.SetBinding( dp, ( BindingBase ) binding );
        }

        internal static void ThreadSafeSetValue( this FrameworkElement element, DependencyProperty property, object value )
        {
            Dispatcher dispatcher = element.Dispatcher;
            Action action = (Action) (() => element.SetValue(property, value));
            if ( dispatcher.CheckAccess() )
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke( ( Delegate ) action );
            }
        }

        internal static void Schedule( this FrameworkElement element, DispatcherPriority priority, Action action )
        {
            Guard.NotNull( ( object ) element, nameof( element ) );
            if ( DispatcherUtil.GetTestMode() )
            {
                action();
            }
            else
            {
                element.Dispatcher.BeginInvoke( ( Delegate ) action, priority, new object[ 0 ] );
            }
        }
    }
}
