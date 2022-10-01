using Ecng.Common;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public static class XamlHelper
    {
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty;

        static XamlHelper()
        {
            UIPropertyMetadata propertyMetadata = new UIPropertyMetadata();

            propertyMetadata.PropertyChangedCallback = ( DependencyObject d, DependencyPropertyChangedEventArgs e ) =>
            {
                var button = ( System.Windows.Controls.Button )d;
                button.Click += new RoutedEventHandler( ( object sender, RoutedEventArgs e2 ) => button.GetWindow().DialogResult = XamlHelper.GetDialogResult( button ) );
            };


            XamlHelper.DialogResultProperty = DependencyProperty.RegisterAttached( "DialogResult", typeof( bool? ), typeof( XamlHelper ), ( PropertyMetadata )propertyMetadata );
        }

        /// <summary>
        /// </summary>
        public static Window GetWindow( this DependencyObject obj )
        {
            return Window.GetWindow( obj );
        }

        /// <summary>
        /// Boilerplate code to register attached property "bool? DialogResult"
        /// </summary>
        public static bool? GetDialogResult( DependencyObject obj )
        {
            if ( obj == null )
            {
                throw new ArgumentNullException( nameof( obj ) );
            }

            return ( bool? )obj.GetValue( XamlHelper.DialogResultProperty );
        }

        /// <summary>
        /// </summary>
        public static T FindLogicalChild<T>( this DependencyObject d ) where T : DependencyObject
        {
            if ( d == null )
            {
                return default( T );
            }

            T obj = d as T;

            if ( obj != null )
            {
                return obj;
            }

            using ( var iter = LogicalTreeHelper.GetChildren( d ).OfType<DependencyObject>().GetEnumerator() )
            {
                while ( iter.MoveNext() )
                {
                    var current = iter.Current;

                    T Tobj = current as T;

                    if ( Tobj != null )
                    {
                        return Tobj;
                    }

                    T child = current.FindLogicalChild<T>();

                    if ( child != null )
                    {
                        return child;
                    }

                }
            }
            return default( T );
        }

        /// <summary>
        /// </summary>
        public static T FindVisualChild<T>( this DependencyObject d ) where T : DependencyObject
        {
            return d.FindVisualChilds<T>().FirstOrDefault<T>();
        }

        /// <summary>
        /// </summary>
        public static IEnumerable<T> FindVisualChilds<T>( this DependencyObject d ) where T : DependencyObject
        {
            for ( int i = 0; i < VisualTreeHelper.GetChildrenCount( d ); i++ )
            {
                var child = VisualTreeHelper.GetChild( d, i );

                if ( child is T )
                {
                    yield return ( T )child;
                }

                foreach ( T t in child.FindVisualChilds<T>() )
                {
                    yield return t;
                }
                child = null;
            }
        }

        /// <summary>
        /// </summary>
        public static void SetDialogResult( DependencyObject d, bool? value )
        {
            if ( d == null )
            {
                throw new ArgumentNullException( nameof( d ) );
            }

            d.SetValue( XamlHelper.DialogResultProperty, ( object )value );
        }

        /// <summary>
        /// </summary>
        public static void GuiAsync( this DispatcherObject d, Action action )
        {
            if ( d == null )
            {
                throw new ArgumentNullException( nameof( d ) );
            }

            d.Dispatcher.GuiAsync( action );
        }

        /// <summary>
        /// </summary>
        public static void GuiAsync( this Dispatcher d, Action action )
        {
            d.GuiAsync( action, ( DispatcherPriority )9 );
        }

        /// <summary>
        /// </summary>
        public static void GuiAsync( this Dispatcher dispatcher, Action action, DispatcherPriority priority )
        {
            if ( dispatcher == null )
            {
                throw new ArgumentNullException( nameof( dispatcher ) );
            }

            if ( action == null )
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            if ( dispatcher.CheckAccess() )
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke( ( Delegate )action, priority, Array.Empty<object>() );
            }
        }

        /// <summary>
        /// </summary>
        public static Task<TResult> GuiThreadGetAsync<TResult>( Func<CancellationToken, Task<TResult>> func, CancellationToken token )
        {

            Dispatcher dispatcher = GuiDispatcher.GlobalDispatcher.Dispatcher;
            if ( dispatcher.CheckAccess() )
                return func( token );

            var completionSource = new TaskCompletionSource<TResult>();

            dispatcher.GuiAsync( async () =>
            {
                try
                {
                    token.ThrowIfCancellationRequested();

                    completionSource.SetResult( await func( token ) );
                    completionSource = null;
                }
                catch ( OperationCanceledException ex )
                {
                    completionSource.SetCanceled();
                }
                catch ( Exception ex )
                {
                    completionSource.SetException( ex );
                }
            } );

            return completionSource.Task;
        }

        /// <summary>
        /// </summary>
        public static Task GuiThreadGetAsync( Func<CancellationToken, Task> func, CancellationToken token )
        {
            async Task<object> asyncClass( CancellationToken p )
            {
                await func( p );
                object obj = null;
                return obj;
            }

            return ( Task )XamlHelper.GuiThreadGetAsync<object>( asyncClass, token );
        }

        /// <summary>
        /// </summary>        
        public static (int width, int height) GetActualSize( this FrameworkElement elem )
        {
            if ( elem == null )
                throw new ArgumentNullException( "elem == null" );

            return (( int )elem.ActualWidth, ( int )elem.ActualHeight);
        }

        /// <summary>
        /// </summary>
        public static void GuiSync( this DispatcherObject obj, Action action )
        {
            if ( obj == null )
                throw new ArgumentNullException( "obj == null" );

            obj.Dispatcher.GuiSync( action );
        }

        /// <summary>
        /// </summary>
        public static T GuiSync<T>( this DispatcherObject obj, Func<T> func )
        {
            if ( obj == null )
                throw new ArgumentNullException( "obj == null" );

            return obj.Dispatcher.GuiSync<T>( func );
        }

        /// <summary>
        /// </summary>
        public static T GuiSync<T>( this Dispatcher dispatcher, Func<T> func )
        {
            return dispatcher.GuiSync<T>( func, ( DispatcherPriority )9 );
        }

        /// <summary>
        /// </summary>
        public static T GuiSync<T>( this Dispatcher dispatcher, Func<T> func, DispatcherPriority priority )
        {
            if ( dispatcher == null )
            {
                throw new ArgumentNullException( "dispatcher == null" );
            }

            if ( func == null )
            {
                throw new ArgumentNullException( "func == null" );
            }

            if ( !dispatcher.CheckAccess() )
            {
                return ( ( object )dispatcher.Invoke<T>( func, priority ) ).To<T>();
            }

            return func();
        }


        /// <summary>
        /// </summary>
        public static void GuiSync( this Dispatcher dispatcher, Action action )
        {
            dispatcher.GuiSync( action, ( DispatcherPriority )9 );
        }

        /// <summary>
        /// </summary>
        public static void GuiSync( this Dispatcher dispatcher, Action action, DispatcherPriority priority )
        {
            if ( dispatcher == null )
            {
                throw new ArgumentNullException( "dispatcher == null" );
            }

            if ( action == null )
            {
                throw new ArgumentNullException( "action == null" );
            }

            if ( dispatcher.CheckAccess() )
                action();
            else
                dispatcher.Invoke( action, priority );
        }

        /// <summary>
        /// </summary>
        public static BitmapSource GetImage( this FrameworkElement elem )
        {
            return elem.GetImage( elem.GetActualSize() );
        }

        /// <summary>
        /// </summary>
        public static BitmapSource GetImage( this Visual visual, (int width, int height) size )
        {
            return visual.GetImage( size.Item1, size.Item2 );
        }

        /// <summary>
        /// </summary>
        public static BitmapSource GetImage( this Visual visual, int width, int height )
        {
            if ( visual == null )
            {
                throw new ArgumentNullException( "visual == null" );
            }

            DrawingVisual drawingVisual = new DrawingVisual();

            using ( DrawingContext drawingContext = drawingVisual.RenderOpen() )
            {
                VisualBrush visualBrush = new VisualBrush( visual );
                drawingContext.DrawRectangle( ( System.Windows.Media.Brush )visualBrush, ( System.Windows.Media.Pen )null, new Rect( new System.Windows.Point( 0.0, 0.0 ), new System.Windows.Point( ( double )width, ( double )height ) ) );
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap( width, height, 96.0, 96.0, PixelFormats.Pbgra32 );
            renderTargetBitmap.Render( ( Visual )drawingVisual );

            return ( BitmapSource )renderTargetBitmap;
        }

        /// <summary>
        /// </summary>
        public static void SaveImage( this BitmapSource image, Stream file )
        {
            if ( image == null )
                throw new ArgumentNullException( "image == null" );
            if ( file == null )
                throw new ArgumentNullException( "file == null" );

            PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();

            pngBitmapEncoder.Frames.Add( BitmapFrame.Create( image ) );
            pngBitmapEncoder.Save( file );
        }

        /// <summary>
        /// </summary>
        public static BitmapImage ToBitmapImage( this byte[ ] imageData )
        {
            using ( MemoryStream memoryStream = new MemoryStream( imageData ) )
            {
                memoryStream.Position = 0L;

                var img = new BitmapImage();

                img.BeginInit();
                img.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = ( Uri )null;
                img.StreamSource = ( Stream )memoryStream;
                img.EndInit();
                return img;
            }
        }

        /// <summary>Cast value to specified type.</summary>
        /// <typeparam name="T">Return type.</typeparam>
        /// <param name="value">Source value.</param>
        /// <returns>Casted value.</returns>
        public static T WpfCast<T>( this object value )
        {
            if ( value != DependencyProperty.UnsetValue )
                return value.To<T>();
            return default( T );
        }



        /// <summary>
        /// </summary>
        public static bool ShowModal( this Window wnd )
        {
            if ( Application.Current != null )
                return XamlHelper.ShowModal( wnd, Application.Current.MainWindow );
            bool? nullable = wnd.ShowDialog();
            return nullable.GetValueOrDefault() & nullable.HasValue;
        }

        /// <summary>
        /// </summary>
        public static bool ShowModal( this Window wnd, DependencyObject obj )
        {
            return XamlHelper.ShowModal( wnd, obj.GetWindow() );
        }

        /// <summary>
        /// </summary>
        public static bool ShowModal( this Window wnd, Window owner )
        {
            if ( wnd == null )
                throw new ArgumentNullException( "wnd == null" );

            if ( owner == null )
                throw new ArgumentNullException( "owner == null" );

            wnd.Owner = owner;
            bool? showResult = wnd.ShowDialog();

            return showResult.GetValueOrDefault() & showResult.HasValue;
        }

        /// <summary>
        /// </summary>
        public static bool ShowModal( this CommonDialog dlg, DependencyObject obj )
        {
            return XamlHelper.ShowModal( dlg, obj.GetWindow() );
        }

        /// <summary>
        /// </summary>
        public static bool ShowModal( this CommonDialog dlg, Window owner )
        {
            if ( dlg == null )
                throw new ArgumentNullException( "dlg == null" );
            if ( owner == null )
                throw new ArgumentNullException( "owner == null" );

            bool? nullable = dlg.ShowDialog( owner );
            return nullable.GetValueOrDefault() & nullable.HasValue;
        }

        /// <summary>
        /// </summary>
        public static void MakeHideable( this Window window )
        {
            if ( window == null )
                throw new ArgumentNullException( "window == null" );

            window.Closing += new CancelEventHandler( XamlHelper.OnWindowClosing );
        }

        /// <summary>
        /// </summary>
        public static void DeleteHideable( this Window window )
        {
            if ( window == null )
                throw new ArgumentNullException( "window == null" );
            window.Closing -= new CancelEventHandler( XamlHelper.OnWindowClosing );
        }

        private static void OnWindowClosing( object wnd, CancelEventArgs e )
        {
            e.Cancel = true;
            ( ( Window )wnd ).Hide();
        }

        /// <summary>
        /// </summary>
        public static int ToInt( this System.Windows.Media.Color color )
        {
            return ( int )color.A << 24 | ( int )color.R << 16 | ( int )color.G << 8 | ( int )color.B;
        }

        /// <summary>
        /// </summary>
        public static System.Windows.Media.Color ToColor( this int color )
        {
            return System.Windows.Media.Color.FromArgb( ( byte )( color >> 24 ), ( byte )( color >> 16 ), ( byte )( color >> 8 ), ( byte )color );
        }

        /// <summary>
        /// Convert <see cref="T:System.Drawing.Color" /> value to <see cref="T:System.Windows.Media.Color" />.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Drawing.Color" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Windows.Media.Color" /> value.</returns>
        public static System.Windows.Media.Color ToWpf( this System.Drawing.Color value )
        {
            return value.ToArgb().ToColor();
        }

        /// <summary>
        /// Convert <see cref="T:System.Windows.Media.Color" /> value to <see cref="T:System.Drawing.Color" />.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Windows.Media.Color" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Drawing.Color" /> value.</returns>
        public static System.Drawing.Color FromWpf( this System.Windows.Media.Color value )
        {
            return System.Drawing.Color.FromArgb( value.ToInt() );
        }

        /// <summary>
        /// Convert <see cref="T:System.Drawing.Brush" /> value to <see cref="T:System.Windows.Media.Brush" />.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Drawing.Brush" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Windows.Media.Brush" /> value.</returns>
        public static System.Windows.Media.Brush ToWpf( this System.Drawing.Brush value )
        {
            if ( value == null )
            {
                return ( System.Windows.Media.Brush )null;
            }

            SolidBrush brush = value as SolidBrush;

            if ( brush != null )
            {
                return ( System.Windows.Media.Brush )new SolidColorBrush( brush.Color.ToWpf() );
            }

            var gradientB = value as System.Drawing.Drawing2D.LinearGradientBrush;

            if ( gradientB == null )
            {
                throw new NotSupportedException();
            }

            var startColor = gradientB.LinearColors[0].ToWpf();
            var endColor = gradientB.LinearColors[1].ToWpf();

            var startPoint = new System.Windows.Point( ( double )gradientB.Rectangle.X, ( double )gradientB.Rectangle.Y );
            RectangleF rectangle = gradientB.Rectangle;
            double right = ( double )rectangle.Right;
            rectangle = gradientB.Rectangle;
            double bottom = ( double )rectangle.Bottom;
            var endPoint = new System.Windows.Point( right, bottom );

            return ( System.Windows.Media.Brush )new System.Windows.Media.LinearGradientBrush( startColor, endColor, startPoint, endPoint );
        }

        /// <summary>
        /// Convert <see cref="T:System.Windows.Media.Brush" /> value to <see cref="T:System.Drawing.Brush" />.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Windows.Media.Brush" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Drawing.Brush" /> value.</returns>
        public static System.Drawing.Brush FromWpf( this System.Windows.Media.Brush value )
        {
            if ( value == null )
            {
                return ( System.Drawing.Brush )null;
            }

            SolidColorBrush brush = value as SolidColorBrush;

            if ( brush != null )
            {
                return ( System.Drawing.Brush )new SolidBrush( brush.Color.FromWpf() );
            }

            var gradient = value as System.Windows.Media.LinearGradientBrush;

            if ( gradient == null )
            {
                throw new NotSupportedException();
            }

            var startPoint = gradient.StartPoint;
            GradientStopCollection gradientStops = gradient.GradientStops;
            GradientStop gradientStop1 = gradientStops[0];
            GradientStop gradientStop2 = gradientStops[1];
            return ( System.Drawing.Brush )new System.Drawing.Drawing2D.LinearGradientBrush( new System.Drawing.Point( ( ( int )startPoint.X + ( int )gradientStop1.Offset ), ( ( int )startPoint.Y ) ), new System.Drawing.Point( ( int )( startPoint.X + gradientStop2.Offset ), ( ( int )startPoint.Y ) ), gradientStop1.Color.FromWpf(), gradientStop2.Color.FromWpf() );
        }

        /// <summary>
        /// Convert <see cref="T:System.Drawing.PointF" /> value to <see cref="T:System.Windows.Point" />.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Drawing.PointF" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Windows.Point" /> value.</returns>
        public static System.Windows.Point ToWpf( this PointF value )
        {
            return new System.Windows.Point( ( double )value.X, ( double )value.Y );
        }

        /// <summary>
        /// Convert <see cref="T:System.Windows.Point" /> value to <see cref="T:System.Drawing.PointF" />.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Windows.Point" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Drawing.PointF" /> value.</returns>
        public static System.Drawing.PointF FromWpf( this System.Windows.Point value )
        {
            return new System.Drawing.PointF( ( float )( value.X ), ( float )( value.Y ) );
        }


        /// <summary>
        /// </summary>
        public static IEnumerable<Window> GetActiveWindows( this Application app )
        {
            if ( app == null )
                throw new ArgumentNullException( "app == null" );

            return app.Windows.Cast<Window>().Where<Window>( w => w.IsActive );
        }

        /// <summary>
        /// </summary>
        public static Window GetActiveWindow( this Application app )
        {
            return app.GetActiveWindows().FirstOrDefault<Window>();
        }

        /// <summary>
        /// </summary>
        public static Window GetActiveOrMainWindow( this Application app )
        {
            return app.GetActiveWindow() ?? app.MainWindow;
        }

        /// <summary>
        /// </summary>
        public static void SetBindings( this DependencyObject obj, DependencyProperty property, object dataObject, string path, BindingMode mode = BindingMode.TwoWay, IValueConverter converter = null, object parameter = null )
        {
            BindingOperations.SetBinding( obj, property, ( BindingBase )new Binding( path )
            {
                Source = dataObject,
                Mode = mode,
                Converter = converter,
                ConverterParameter = parameter
            } );
        }

        /// <summary>
        /// </summary>
        public static void SetBindings( this DependencyObject obj, DependencyProperty property, object dataObject, PropertyPath path, BindingMode mode = BindingMode.TwoWay, IValueConverter converter = null, object parameter = null )
        {
            BindingOperations.SetBinding( obj, property, ( BindingBase )new Binding()
            {
                Source = dataObject,
                Path = path,
                Mode = mode,
                Converter = converter,
                ConverterParameter = parameter
            } );
        }

        /// <summary>
        /// </summary>
        public static void SetMultiBinding( this DependencyObject obj, DependencyProperty prop, IMultiValueConverter conv, params Binding[ ] bindings )
        {
            if ( bindings == null || bindings.Length <= 1 )
                throw new ArgumentException( "bindings" );

            MultiBinding multi = new MultiBinding();

            if ( conv == null )
            {
                throw new ArgumentNullException( "multiValueConverter == null" );
            }

            multi.Converter = conv;

            foreach ( Binding binding in bindings )
            {
                binding.Mode = BindingMode.OneWay;
                multi.Bindings.Add( ( BindingBase )binding );
            }
            BindingOperations.SetBinding( obj, prop, ( BindingBase )multi );
        }

        /// <summary>
        /// Checks if supplied dispatcher/dispatcher object OR current thread is associated with Dispatcher.
        /// </summary>
        /// <param name="d">Any DispatcherObject or Dispatcher or anything else (to check using Dispatcher.FromThread())</param>
        public static void EnsureUIThread( this object d )
        {
            Dispatcher dispatcher = ( d as DispatcherObject )?.Dispatcher ?? d as Dispatcher ?? GuiDispatcher.CurrentThreadDispatcher;

            if ( ( dispatcher != null ? ( !dispatcher.CheckAccess() ? 1 : 0 ) : 1 ) != 0 )
            {
                throw new InvalidOperationException( "dispatcher" );
            }

        }

        /// <summary>
        /// </summary>
        public static System.Windows.Media.Color ToTransparent( this System.Windows.Media.Color color, byte alpha )
        {
            return System.Windows.Media.Color.FromArgb( alpha, color.R, color.G, color.B );
        }

        /// <summary>
        /// </summary>
        public static bool IsDesignMode( this DependencyObject obj )
        {
            return DesignerProperties.GetIsInDesignMode( obj );
        }

        /// <summary>
        /// </summary>
        public static System.Windows.Media.Brush GetBrush( this DrawingImage source )
        {
            if ( source == null )
                throw new ArgumentNullException( "source == null" );

            return source.Drawing.ToMyBrush();
        }

        private static System.Windows.Media.Brush ToMyBrush( this System.Windows.Media.Drawing p )
        {
            var geometryDrawing = p as GeometryDrawing;
            if ( geometryDrawing != null )
                return geometryDrawing.Brush;

            DrawingGroup drawingGroup = p as DrawingGroup;

            if ( drawingGroup == null )
                return ( System.Windows.Media.Brush )null;

            foreach ( System.Windows.Media.Drawing child in drawingGroup.Children )
            {
                System.Windows.Media.Brush brush = child.ToMyBrush();
                if ( brush != null )
                    return brush;
            }
            return ( System.Windows.Media.Brush )null;
        }

        /// <summary>
        /// </summary>
        public static void UpdateBrush( this DrawingImage source, System.Windows.Media.Brush brush )
        {
            if ( source == null )
                throw new ArgumentNullException( "source == null" );

            source.Drawing.SomeUpdateBrush( brush );
        }

        /// <summary>
        /// </summary>
        public static void UpdatePen( this DrawingImage source, System.Windows.Media.Pen pen )
        {
            if ( source == null )
                throw new ArgumentNullException( "Drawing Image Source" );

            source.Drawing.SomeUpdatePen( pen );
        }

        private static void SomeUpdateBrush( this System.Windows.Media.Drawing d, System.Windows.Media.Brush b )
        {
            if ( b == null )
                throw new ArgumentNullException( "brush" );

            GeometryDrawing geometryDrawing = d as GeometryDrawing;
            if ( geometryDrawing == null )
            {
                DrawingGroup drawingGroup = d as DrawingGroup;
                if ( drawingGroup == null )
                    return;
                XamlHelper.SomeUpdateBrush( drawingGroup, b );
            }
            else
                geometryDrawing.Brush = b;
        }

        private static void SomeUpdatePen( this System.Windows.Media.Drawing d, System.Windows.Media.Pen p )
        {
            if ( p == null )
                throw new ArgumentNullException( "pen" );

            GeometryDrawing geometryDrawing = d as GeometryDrawing;
            if ( geometryDrawing == null )
            {
                DrawingGroup drawingGroup = d as DrawingGroup;
                if ( drawingGroup == null )
                    return;
                XamlHelper.SomeUpdatePen( drawingGroup, p );
            }
            else
                geometryDrawing.Pen = p;
        }

        private static void SomeUpdateBrush( this DrawingGroup g, System.Windows.Media.Brush b )
        {
            if ( g == null )
                throw new ArgumentNullException( "DrawingGroup" );
            foreach ( System.Windows.Media.Drawing child in g.Children )
                child.SomeUpdateBrush( b );
        }

        private static void SomeUpdatePen( this DrawingGroup g, System.Windows.Media.Pen p )
        {
            foreach ( System.Windows.Media.Drawing child in g.Children )
                child.SomeUpdatePen( p );
        }

        /// <summary>
        /// </summary>
        public static void CopyToClipboard<T>( this T value )
        {
            Dispatcher d = GuiDispatcher.GlobalDispatcher.Dispatcher;

            if ( !d.CheckAccess() )
            {
                d.GuiAsync( () => value.CopyToClipboard<T>() );
            }
            else
            {
                byte[ ] byteArray = ( object )value as byte[ ];

                if ( byteArray == null )
                {
                    Stream myStream = ( object )value as Stream;

                    if ( myStream == null )
                    {
                        string myString = ( object )value as string;

                        if ( myString == null )
                        {
                            BitmapSource bitmap = ( object )value as BitmapSource;

                            if ( bitmap == null )
                            {
                                throw new NotSupportedException();
                            }

                            Clipboard.SetImage( bitmap );
                        }
                        else
                        {
                            Clipboard.SetDataObject( ( object )myString );
                        }
                    }
                    else
                    {
                        Clipboard.SetAudio( myStream );
                    }
                }
                else
                {
                    Clipboard.SetAudio( byteArray );
                }
            }
        }

        //    private sealed class SomeMoreShit<X>
        //    {
        //      public X _x;

        //      internal void DoCopyToClipboard()
        //      {
        //        this._x.CopyToClipboard<X>();
        //      }
        //    }
    }
}









//[Serializable]
//private sealed class SomeShit
//{
//    public static readonly XamlHelper.SomeShit ShitMethod02 = new XamlHelper.SomeShit();
//    public static Func<Window, bool> \u0023\u003DzOvjcH1IiPBxXDr9aPQ\u003D\u003D;

//      internal bool SomeShitShit2X(Window _param1)
//      {
//        return _param1.IsActive;
//      }

//    internal void DialogResultPropertyChanged(
//      DependencyObject _param1,
//      DependencyPropertyChangedEventArgs _param2)
//      {
//        XamlHelper.DialogResultClass o0Oe1cOlYTbqj5Oce = new XamlHelper.DialogResultClass();
//        o0Oe1cOlYTbqj5Oce._button = (Button) _param1;
//    o0Oe1cOlYTbqj5Oce._button.Click += new RoutedEventHandler( o0Oe1cOlYTbqj5Oce.DialogResultClassMethod03);
//}
//    }

//    private sealed class \u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ <X> : IEnumerable <X>, IEnumerable, IEnumerator <X>, IEnumerator, IDisposable
//      where X : DependencyObject
//{


//      private int \u0023\u003DzmZCvlu0uWGNm;

//private X \u0023\u003DzWvH5n6COaRvj;

//private int \u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D;

//private DependencyObject \u0023\u003DzkEoWorI\u003D;

//public DependencyObject \u0023\u003Dz130Zs0ynp\u0024Dl;

//      private int \u0023\u003Dzmx\u0024vtRlnPb5c;

//      private DependencyObject \u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D;

//      private IEnumerator<X> \u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D;

//      [DebuggerHidden]
//      public \u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ(int _param1)
//      {
//        this.\u0023\u003DzmZCvlu0uWGNm = _param1;
//        this.\u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D = Environment.CurrentManagedThreadId;
//      }

//      [DebuggerHidden]
//      void IDisposable.\Dispose()
//      {
//        switch (this.\u0023\u003DzmZCvlu0uWGNm)
//        {
//          case -3:
//          case 2:
//            try
//            {
//            }
//            finally
//            {
//              this.\u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D();
//            }
//            break;
//        }
//      }

//      bool IEnumerator.MoveNext()
//      {
//        // ISSUE: fault handler
//        try
//        {
//          switch (this.\u0023\u003DzmZCvlu0uWGNm)
//          {
//            case 0:
//              this.\u0023\u003DzmZCvlu0uWGNm = -1;
//              this.\u0023\u003Dzmx\u0024vtRlnPb5c = 0;
//              goto label_11;
//            case 1:
//              this.\u0023\u003DzmZCvlu0uWGNm = -1;
//              break;
//            case 2:
//              this.\u0023\u003DzmZCvlu0uWGNm = -3;
//              goto label_9;
//            default:
//              return false;
//          }
//label_6:
//          this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D = this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D.FindVisualChilds<X>().GetEnumerator();
//          this.\u0023\u003DzmZCvlu0uWGNm = -3;
//label_9:
//          if (this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D.MoveNext())
//          {
//            this.\u0023\u003DzWvH5n6COaRvj = this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D.Current;
//            this.\u0023\u003DzmZCvlu0uWGNm = 2;
//            return true;
//          }
//          this.\u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D();
//          this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D = (IEnumerator<X>) null;
//          this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D = (DependencyObject) null;
//          ++this.\u0023\u003Dzmx\u0024vtRlnPb5c;
//label_11:
//          if (this.\u0023\u003Dzmx\u0024vtRlnPb5c >= VisualTreeHelper.GetChildrenCount(this.\u0023\u003DzkEoWorI\u003D))
//            return false;
//          this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D = VisualTreeHelper.GetChild(this.\u0023\u003DzkEoWorI\u003D, this.\u0023\u003Dzmx\u0024vtRlnPb5c);
//          X yfKllAUwFzSmE7Bea = this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D as X;
//          if ((object) yfKllAUwFzSmE7Bea != null)
//          {
//            this.\u0023\u003DzWvH5n6COaRvj = yfKllAUwFzSmE7Bea;
//            this.\u0023\u003DzmZCvlu0uWGNm = 1;
//            return true;
//          }
//          goto label_6;
//        }
//        __fault
//        {
//          this.\Dispose();
//        }
//      }

//      private void \u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D()
//      {
//        this.\u0023\u003DzmZCvlu0uWGNm = -1;
//        if (this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D == null)
//          return;
//        this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D.Dispose();
//      }

//      [DebuggerHidden]
//      X IEnumerator<X>.\u0023\u003DzUdIx4uHGmeS8GtRxu6exQAqa6I8f()
//      {
//        return this.\u0023\u003DzWvH5n6COaRvj;
//      }

//      [DebuggerHidden]
//      void IEnumerator.\u0023\u003DzPz2CkL3H4EO2s8Al1w\u003D\u003D()
//      {
//        throw new NotSupportedException();
//      }

//      [DebuggerHidden]
//      object IEnumerator.\u0023\u003DzUvRtPry9Z3_r4VkfO99sKo4\u003D()
//      {
//        return (object) this.\u0023\u003DzWvH5n6COaRvj;
//      }

//      [DebuggerHidden]
//      [return: Nullable(new byte[] {1, 0})]
//      IEnumerator<X> IEnumerable<X>.\u0023\u003DzHW1acyQZIGnGvJbOS44ATNpP3OQa()
//      {
//        XamlHelper.\u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ<X> dcyb5cZafHptn0k6tHvSz;
//        if (this.\u0023\u003DzmZCvlu0uWGNm == -2 && this.\u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D == Environment.CurrentManagedThreadId)
//        {
//          this.\u0023\u003DzmZCvlu0uWGNm = 0;
//          dcyb5cZafHptn0k6tHvSz = this;
//        }
//        else
//          dcyb5cZafHptn0k6tHvSz = new XamlHelper.\u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ<X>(0);
//        dcyb5cZafHptn0k6tHvSz.\u0023\u003DzkEoWorI\u003D = this.\u0023\u003Dz130Zs0ynp\u0024Dl;
//        return (IEnumerator<X>) dcyb5cZafHptn0k6tHvSz;
//      }

//      [DebuggerHidden]
//      [return: Nullable(1)]
//      IEnumerator IEnumerable.\u0023\u003DzGBBKW4936O1cYMySvZ\u0024sDvw\u003D()
//      {
//        return (IEnumerator) this.\u0023\u003DzHW1acyQZIGnGvJbOS44ATNpP3OQa();
//      }
//    }





//      [StructLayout(LayoutKind.Auto)]
//      private struct \u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D : IAsyncStateMachine
//      {

//        public int \u0023\u003DzmZCvlu0uWGNm;

//        public AsyncVoidMethodBuilder \u0023\u003Dzu1QJGuMAn80M;

//        public XamlHelper.LambdaShit003<T> _delayActionHelper;

//        private TaskCompletionSource<T> \u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D;

//        private TaskAwaiter<T> \u0023\u003DzxUTTsn8LiP4f;

//        void IAsyncStateMachine.MoveNext()
//        {
//          int zmZcvlu0uWgNm = this.\u0023\u003DzmZCvlu0uWGNm;
//          XamlHelper.LambdaShit003<T> z0sZyIb2k8Bgs = this._delayActionHelper;
//          try
//          {
//            try
//            {
//              TaskAwaiter<T> awaiter;
//              int num;
//              if (zmZcvlu0uWgNm != 0)
//              {
//                z0sZyIb2k8Bgs._cancelToken.ThrowIfCancellationRequested();
//                this.\u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D = z0sZyIb2k8Bgs._taskSource;
//                awaiter = z0sZyIb2k8Bgs._function003(z0sZyIb2k8Bgs._cancelToken).GetAwaiter();
//                if (!awaiter.IsCompleted)
//                {
//                  this.\u0023\u003DzmZCvlu0uWGNm = num = 0;
//                  this.\u0023\u003DzxUTTsn8LiP4f = awaiter;
//                  this.\u0023\u003Dzu1QJGuMAn80M.AwaitUnsafeOnCompleted<TaskAwaiter<T>, XamlHelper.LambdaShit003<T>.\u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D>(ref awaiter, ref this);
//                  return;
//                }
//              }
//              else
//              {
//                awaiter = this.\u0023\u003DzxUTTsn8LiP4f;
//                this.\u0023\u003DzxUTTsn8LiP4f = new TaskAwaiter<T>();
//                this.\u0023\u003DzmZCvlu0uWGNm = num = -1;
//              }
//              this.\u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D.SetResult(awaiter.GetResult());
//              this.\u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D = (TaskCompletionSource<T>) null;
//            }
//            catch (OperationCanceledException ex)
//            {
//              z0sZyIb2k8Bgs._taskSource.SetCanceled();
//            }
//            catch (Exception ex)
//            {
//              z0sZyIb2k8Bgs._taskSource.SetException(ex);
//            }
//          }
//          catch (Exception ex)
//          {
//            this.\u0023\u003DzmZCvlu0uWGNm = -2;
//            this.\u0023\u003Dzu1QJGuMAn80M.SetException(ex);
//            return;
//          }
//          this.\u0023\u003DzmZCvlu0uWGNm = -2;
//          this.\u0023\u003Dzu1QJGuMAn80M.SetResult();
//        }

//        [DebuggerHidden]
//        void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine _param1)
//        {
//          this.\u0023\u003Dzu1QJGuMAn80M.SetStateMachine(_param1);
//        }
//      }
//    }

//    private sealed class SomeMoreShit<X>
//    {
//      public X _x;

//      internal void DoCopyToClipboard()
//      {
//        this._x.CopyToClipboard<X>();
//      }
//    }

//    private sealed class \u0023\u003Dzj5oesZ9ZobsUrBo6EXkbAsI\u003D<T>
//    {
//      public Func<CancellationToken, T> _function003;

//      internal Task<T> LambdaShit003Method0003(
//        CancellationToken _param1)
//      {
//        return Task.FromResult<T>(this._function003(_param1));
//      }
//    }

//    private sealed class SomeShitLambda00342234
//    {
//      public Action<CancellationToken> _myAction;

//      internal Task LambdaShit003Method0003(CancellationToken _param1)
//      {
//        this._myAction(_param1);
//        return Task.CompletedTask;
//      }
//    }

//    private sealed class HolyShitClass
//    {
//      public Func<CancellationToken, Task> _function003;

//      internal async Task<object> LambdaShit003Method0003(
//        CancellationToken _param1)
//      {
//        await this._function003(_param1);
//        object obj;
//        return obj;
//      }

//      [StructLayout(LayoutKind.Auto)]
//      private struct \u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D : IAsyncStateMachine
//      {

//        public int \u0023\u003DzmZCvlu0uWGNm;

//        public AsyncTaskMethodBuilder<object> \u0023\u003Dzu1QJGuMAn80M;

//        public XamlHelper.HolyShitClass _delayActionHelper;

//        public CancellationToken \u0023\u003DzRsets6Q\u003D;

//        private TaskAwaiter \u0023\u003DzxUTTsn8LiP4f;

//        void IAsyncStateMachine.MoveNext()
//        {
//          int zmZcvlu0uWgNm = this.\u0023\u003DzmZCvlu0uWGNm;
//          XamlHelper.HolyShitClass z0sZyIb2k8Bgs = this._delayActionHelper;
//          object result;
//          try
//          {
//            TaskAwaiter awaiter;
//            int num;
//            if (zmZcvlu0uWgNm != 0)
//            {
//              awaiter = z0sZyIb2k8Bgs._function003(this.\u0023\u003DzRsets6Q\u003D).GetAwaiter();
//              if (!awaiter.IsCompleted)
//              {
//                this.\u0023\u003DzmZCvlu0uWGNm = num = 0;
//                this.\u0023\u003DzxUTTsn8LiP4f = awaiter;
//                this.\u0023\u003Dzu1QJGuMAn80M.AwaitUnsafeOnCompleted<TaskAwaiter, XamlHelper.HolyShitClass.\u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D>(ref awaiter, ref this);
//                return;
//              }
//            }
//            else
//            {
//              awaiter = this.\u0023\u003DzxUTTsn8LiP4f;
//              this.\u0023\u003DzxUTTsn8LiP4f = new TaskAwaiter();
//              this.\u0023\u003DzmZCvlu0uWGNm = num = -1;
//            }
//            awaiter.GetResult();
//            result = (object) null;
//          }
//          catch (Exception ex)
//          {
//            this.\u0023\u003DzmZCvlu0uWGNm = -2;
//            this.\u0023\u003Dzu1QJGuMAn80M.SetException(ex);
//            return;
//          }
//          this.\u0023\u003DzmZCvlu0uWGNm = -2;
//          this.\u0023\u003Dzu1QJGuMAn80M.SetResult(result);
//        }

//        [DebuggerHidden]
//        void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine _param1)
//        {
//          this.\u0023\u003Dzu1QJGuMAn80M.SetStateMachine(_param1);
//        }
//      }
//    }
//  }
//}
