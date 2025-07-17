//// Decompiled with JetBrains decompiler
//// Type: Ecng.Xaml.Charting.Rendering.Common.RenderSurfaceBase
//// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

//using Ecng.Xaml.Charting.Common;
//using Ecng.Xaml.Charting.Common.Extensions;
//using Ecng.Xaml.Charting.Common.Helpers;
//using Ecng.Xaml.Charting.Common.Messaging;
//using Ecng.Xaml.Charting.Licensing;
//using Ecng.Xaml.Charting.Utility;
//using Ecng.Xaml.Charting.Visuals;
//using Ecng.Xaml.Charting.Visuals.RenderableSeries;
//using StockSharp.Xaml.Licensing.Core;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using System.Windows.Threading;

//namespace Ecng.Xaml.Charting.Rendering.Common
//{
//    public abstract class RenderSurfaceBase : ContentControl, IRenderSurface2D, IRenderSurface, IDisposable, IHitTestable, IInvalidatableElement
//    {
//        public static readonly DependencyProperty MaxFrameRateProperty = DependencyProperty.Register(nameof (MaxFrameRate), typeof (double?), typeof (RenderSurfaceBase), new PropertyMetadata((object) null, new PropertyChangedCallback(RenderSurfaceBase.OnMaxFramerateChanged)));
//        public static readonly DependencyProperty UseResizeThrottleProperty = DependencyProperty.Register(nameof (UseResizeThrottle), typeof (bool), typeof (RenderSurfaceBase), new PropertyMetadata((object) false));
//        public static readonly DependencyProperty ResizeThrottleMsProperty = DependencyProperty.Register(nameof (ResizeThrottleMs), typeof (int), typeof (RenderSurfaceBase), new PropertyMetadata((object) 50));
//        public static readonly DependencyProperty RenderSurfaceTypeProperty = DependencyProperty.RegisterAttached("RenderSurfaceType", typeof (string), typeof (RenderSurfaceBase), new PropertyMetadata((object) null, new PropertyChangedCallback(RenderSurfaceBase.OnRenderSurfaceTypeChanged)));
//        internal static readonly string RectIdentifier = Guid.NewGuid().ToString();
//        private volatile bool _isDirty;
//        private RenderTimer _renderTimer;
//        private bool _disposed;
//        private Grid _grid;
//        private readonly Image _image;
//        protected WriteableBitmap RenderWriteableBitmap;
//        private readonly TextureCacheBase _textureCache;
//        private static int instanceCount;
//        private TimedMethod _recreateSurfaceTimedMethod;

//        public static void SetRenderSurfaceType( UIElement element, string value )
//        {
//            element.SetValue( RenderSurfaceBase.RenderSurfaceTypeProperty, ( object ) value );
//        }

//        public static string GetRenderSurfaceType( UIElement element )
//        {
//            return ( string ) element.GetValue( RenderSurfaceBase.RenderSurfaceTypeProperty );
//        }

//        public event EventHandler<DrawEventArgs> Draw;

//        public event EventHandler<RenderedEventArgs> Rendered;

//        internal bool IsLicenseValid { get; set; }

//        protected abstract TextureCacheBase CreateTextureCache( );

//        protected RenderSurfaceBase( )
//        {
//            this._textureCache = this.CreateTextureCache();
//            Image image = new Image();
//            image.HorizontalAlignment = HorizontalAlignment.Stretch;
//            image.VerticalAlignment = VerticalAlignment.Stretch;
//            image.Stretch = Stretch.None;
//            image.IsHitTestVisible = false;
//            this._image = image;
//            this.Initialize();
//            this.SizeChanged += new SizeChangedEventHandler( this.RenderSurfaceSizeChanged );
//            UIElementCollection children = this.Grid.Children;
//            Rectangle rectangle = new Rectangle();
//            rectangle.HorizontalAlignment = HorizontalAlignment.Stretch;
//            rectangle.VerticalAlignment = VerticalAlignment.Stretch;
//            rectangle.Fill = ( Brush ) new SolidColorBrush( Colors.Transparent );
//            rectangle.Tag = ( object ) RenderSurfaceBase.RectIdentifier;
//            children.Add( ( UIElement ) rectangle );
//            this.Loaded += new RoutedEventHandler( this.OnRenderSurfaceBaseLoaded );
//            this.Unloaded += new RoutedEventHandler( this.OnRenderSurfaceBaseUnloaded );
//        }

//        ~RenderSurfaceBase( )
//        {
//            this.Dispose( false );
//        }

//        public IServiceContainer Services { get; set; }

//        internal Image Image
//        {
//            get
//            {
//                return this._image;
//            }
//        }

//        public Grid Grid
//        {
//            get
//            {
//                return this._grid;
//            }
//        }

//        protected TextureCacheBase TextureCache
//        {
//            get
//            {
//                return this._textureCache;
//            }
//        }

//        public double? MaxFrameRate
//        {
//            get
//            {
//                return ( double? ) this.GetValue( RenderSurfaceBase.MaxFrameRateProperty );
//            }
//            set
//            {
//                this.SetValue( RenderSurfaceBase.MaxFrameRateProperty, ( object ) value );
//            }
//        }

//        public int ResizeThrottleMs
//        {
//            get
//            {
//                return ( int ) this.GetValue( RenderSurfaceBase.ResizeThrottleMsProperty );
//            }
//            set
//            {
//                this.SetValue( RenderSurfaceBase.ResizeThrottleMsProperty, ( object ) value );
//            }
//        }

//        public bool UseResizeThrottle
//        {
//            get
//            {
//                return ( bool ) this.GetValue( RenderSurfaceBase.UseResizeThrottleProperty );
//            }
//            set
//            {
//                this.SetValue( RenderSurfaceBase.UseResizeThrottleProperty, ( object ) value );
//            }
//        }

//        public virtual bool NeedsResizing
//        {
//            get
//            {
//                if ( this.RenderWriteableBitmap != null && this.RenderWriteableBitmap.PixelWidth == ( int ) this.ActualWidth )
//                {
//                    return this.RenderWriteableBitmap.PixelHeight != ( int ) this.ActualHeight;
//                }

//                return true;
//            }
//        }

//        public virtual bool IsSizeValidForDrawing
//        {
//            get
//            {
//                if ( this.ActualWidth >= double.Epsilon && this.ActualHeight >= double.Epsilon && this.ActualWidth.IsRealNumber() )
//                {
//                    return this.ActualHeight.IsRealNumber();
//                }

//                return false;
//            }
//        }

//        public ReadOnlyCollection<IRenderableSeries> ChildSeries
//        {
//            get
//            {
//                return new ReadOnlyCollection<IRenderableSeries>( ( IList<IRenderableSeries> ) this.Grid.Children.OfType<IRenderableSeries>().ToArray<IRenderableSeries>() );
//            }
//        }

//        public void InvalidateElement( )
//        {
//            this._isDirty = true;
//        }

//        public void Clear( )
//        {
//            using ( IRenderContext2D renderContext = this.GetRenderContext() )
//            {
//                renderContext.Clear();
//            }
//        }

//        public bool ContainsSeries( IRenderableSeries renderableSeries )
//        {
//            UIElement element = renderableSeries as UIElement;
//            if ( element != null )
//            {
//                return this._grid.Children.Contains( element );
//            }

//            return false;
//        }

//        public void AddSeries( IEnumerable<IRenderableSeries> renderableSeries )
//        {
//            foreach ( IRenderableSeries renderableSeries1 in renderableSeries )
//            {
//                this.AddSeries( renderableSeries1 );
//            }
//        }

//        public void AddSeries( IRenderableSeries renderableSeries )
//        {
//            this.RemoveSeries( renderableSeries );
//            renderableSeries.Services = this.Services;
//            FrameworkElement frameworkElement = renderableSeries as FrameworkElement;
//            if ( frameworkElement == null )
//            {
//                return;
//            }

//            frameworkElement.Visibility = Visibility.Collapsed;
//            this._grid.Children.Add( ( UIElement ) frameworkElement );
//        }



//public virtual void ClearSeries( )
//{
//    for ( int index = this._grid.Children.Count - 1; index >= 0; --index )
//    {
//        if ( this._grid.Children[ index ] is IRenderableSeriesBase )
//        {
//            this._grid.Children.RemoveAt( index );
//        }
//    }
//}



//        public virtual void RecreateSurface( )
//        {
//            int width = (int) this.ActualWidth;
//            int height = (int) this.ActualHeight;
//            if ( !this.IsSizeValidForDrawing )
//            {
//                width = height = 1;
//            }

//            this.RenderWriteableBitmap = RenderSurfaceBase.CreateWriteableBitmap( width, height );
//            this.PublishResizedMessage( width, height );
//        }

//        protected void PublishResizedMessage( int width, int height )
//        {
//            if ( this.Services == null )
//            {
//                return;
//            }

//            this.Services.GetService<IEventAggregator>().Publish<RenderSurfaceResizedMessage>( new RenderSurfaceResizedMessage( ( object ) this, new Size( ( double ) width, ( double ) height ) ) );
//        }

//        public abstract IRenderContext2D GetRenderContext( );

//        protected virtual void DisposeUnmanagedResources( )
//        {
//        }

//        protected virtual void OnRenderTimeElapsed( )
//        {
//            if ( !this._isDirty )
//            {
//                return;
//            }

//            this._isDirty = false;
//            this.OnDraw();
//        }

//        protected virtual void OnDraw( )
//        {
//            // ISSUE: reference to a compiler-generated field
//            EventHandler<DrawEventArgs> draw = this.Draw;
//            if ( draw == null )
//            {
//                return;
//            }

//            Stopwatch stopwatch = Stopwatch.StartNew();
//            draw( ( object ) this, new DrawEventArgs( ( IRenderSurface2D ) this ) );
//            stopwatch.Stop();
//            this.OnRendered( ( double ) stopwatch.ElapsedMilliseconds );
//        }

//        protected virtual void OnRendered( double duration )
//        {
//            // ISSUE: reference to a compiler-generated field
//            EventHandler<RenderedEventArgs> rendered = this.Rendered;
//            if ( rendered == null )
//            {
//                return;
//            }

//            rendered( ( object ) this, new RenderedEventArgs( duration ) );
//        }

//        protected virtual void OnRenderSurfaceBaseLoaded( object sender, RoutedEventArgs e )
//        {
//            Binding binding = new Binding()
//            {
//                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor)
//                {
//                    AncestorType = typeof (UltrachartSurfaceBase)
//                },
//                Path = new PropertyPath("MaxFrameRate", new object[0])
//            };
//            this.SetBinding( RenderSurfaceBase.MaxFrameRateProperty, ( BindingBase ) binding );
//            this.StopTimer();
//            this.StartTimer();
//            this.OnRenderTimeElapsed();
//        }

//        protected virtual void OnRenderSurfaceBaseUnloaded( object sender, RoutedEventArgs e )
//        {
//            this.StopTimer();
//        }

//        private void StopTimer( )
//        {
//            if ( this._renderTimer == null )
//            {
//                return;
//            }

//            Interlocked.Decrement( ref RenderSurfaceBase.instanceCount );
//            this._renderTimer.Dispose();
//            this._renderTimer = ( RenderTimer ) null;
//        }

//        private void StartTimer( )
//        {
//            if ( this.Services == null || this._disposed )
//            {
//                return;
//            }

//            int num;
//            if ( this._renderTimer != null )
//            {
//                num = Interlocked.Decrement( ref RenderSurfaceBase.instanceCount );
//                this._renderTimer.Dispose();
//                this._renderTimer = ( RenderTimer ) null;
//            }
//            num = Interlocked.Increment( ref RenderSurfaceBase.instanceCount );
//            this._renderTimer = new RenderTimer( this.MaxFrameRate, this.Services.GetService<IDispatcherFacade>(), new Action( this.OnRenderTimeElapsed ) );
//        }

//        private void RenderSurfaceSizeChanged( object sender, SizeChangedEventArgs sizeChangedEventArgs )
//        {
//            if ( this.UseResizeThrottle )
//            {
//                if ( this._recreateSurfaceTimedMethod != null )
//                {
//                    this._recreateSurfaceTimedMethod.Dispose();
//                }

//                this._recreateSurfaceTimedMethod = TimedMethod.Invoke( ( Action ) ( ( ) =>
//                {
//                    this.RecreateSurface();
//                    this.InvalidateElement();
//                } ) ).After( this.ResizeThrottleMs ).WithPriority( DispatcherPriority.Render ).Go();
//            }
//            else
//            {
//                this.RecreateSurface();
//                this.InvalidateElement();
//            }
//        }

//        private static void OnMaxFramerateChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            double? newValue = (double?) e.NewValue;
//            if ( newValue.HasValue )
//            {
//                if ( ( ( IComparable ) newValue ).IsDefined() )
//                {
//                    double? nullable = newValue;
//                    double num1 = 0.0;
//                    if ( ( nullable.GetValueOrDefault() <= num1 ? ( nullable.HasValue ? 1 : 0 ) : 0 ) == 0 )
//                    {
//                        nullable = newValue;
//                        double num2 = 100.0;
//                        if ( ( nullable.GetValueOrDefault() > num2 ? ( nullable.HasValue ? 1 : 0 ) : 0 ) == 0 )
//                        {
//                            goto label_5;
//                        }
//                    }
//                }
//                throw new ArgumentException( string.Format( "{0}.MaxFramerate must be greater than 0.0 and less than or equal to 100.0", ( object ) d.GetType().Name ) );
//            }
//        label_5:
//            ( ( RenderSurfaceBase ) d ).StopTimer();
//            ( ( RenderSurfaceBase ) d ).StartTimer();
//        }

//        private static void OnRenderSurfaceTypeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            UltrachartSurface ultrachartSurface = d as UltrachartSurface;
//            string newValue = e.NewValue as string;
//            if ( ultrachartSurface == null || newValue.IsNullOrWhiteSpace() )
//            {
//                return;
//            }

//            Type type = Type.GetType(newValue);
//            if ( !( type != ( Type ) null ) )
//            {
//                return;
//            }

//            IRenderSurface instance = Activator.CreateInstance(type) as IRenderSurface;
//            if ( instance == null )
//            {
//                return;
//            }

//            ultrachartSurface.RenderSurface = instance;
//        }

//        private static WriteableBitmap CreateWriteableBitmap( int width, int height )
//        {
//            return BitmapFactory.New( width, height );
//        }

//        [Obfuscation( Exclude = false, Feature = "encryptmethod" )]
//        private void Initialize( )
//        {
//            this.IsTabStop = false;
//            this.HorizontalAlignment = HorizontalAlignment.Stretch;
//            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
//            this.VerticalAlignment = VerticalAlignment.Stretch;
//            this.VerticalContentAlignment = VerticalAlignment.Stretch;
//            this._grid = new Grid();
//            Device.SetSnapsToDevicePixels( ( DependencyObject ) this, true );
//            Device.SetSnapsToDevicePixels( ( DependencyObject ) this._grid, true );
//            Device.SetSnapsToDevicePixels( ( DependencyObject ) this._image, true );
//            this._grid.Children.Add( ( UIElement ) this._image );
//            new LicenseManager().Validate<RenderSurfaceBase>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
//            this.Content = ( object ) this._grid;
//        }

//        public Point TranslatePoint( Point point, IHitTestable relativeTo )
//        {
//            return ElementExtensions.TranslatePoint( this, point, relativeTo );
//        }

//        public bool IsPointWithinBounds( Point point )
//        {
//            return ElementExtensions.IsPointWithinBounds( this, point );
//        }

//        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
//        {
//            return ElementExtensions.GetBoundsRelativeTo( this, relativeTo );
//        }


//    }
//}


// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.Common.RenderSurfaceBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Ecng.Xaml.Charting.Common;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Common.Helpers;
using Ecng.Xaml.Charting.Common.Messaging;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Rendering.Common
{
    public abstract class RenderSurfaceBase : ContentControl, IRenderSurface2D, IRenderSurface, IDisposable, IHitTestable, IInvalidatableElement
    {
        public static readonly DependencyProperty MaxFrameRateProperty = DependencyProperty.Register(nameof (MaxFrameRate), typeof (double?), typeof (RenderSurfaceBase), new PropertyMetadata((object) null, new PropertyChangedCallback(RenderSurfaceBase.OnMaxFramerateChanged)));
        public static readonly DependencyProperty UseResizeThrottleProperty = DependencyProperty.Register(nameof (UseResizeThrottle), typeof (bool), typeof (RenderSurfaceBase), new PropertyMetadata((object) false));
        public static readonly DependencyProperty ResizeThrottleMsProperty = DependencyProperty.Register(nameof (ResizeThrottleMs), typeof (int), typeof (RenderSurfaceBase), new PropertyMetadata((object) 50));
        public static readonly DependencyProperty RenderSurfaceTypeProperty = DependencyProperty.RegisterAttached("RenderSurfaceType", typeof (string), typeof (RenderSurfaceBase), new PropertyMetadata((object) null, new PropertyChangedCallback(RenderSurfaceBase.OnRenderSurfaceTypeChanged)));
        internal static readonly string RectIdentifier = Guid.NewGuid().ToString();
        private volatile bool _isDirty;
        private RenderTimer _renderTimer;
        private bool _disposed;
        private Grid _grid;
        private readonly Image _image;
        protected WriteableBitmap RenderWriteableBitmap;
        private readonly TextureCacheBase _textureCache;
        private static int instanceCount;
        private TimedMethod _recreateSurfaceTimedMethod;

        public static void SetRenderSurfaceType( UIElement element, string value )
        {
            element.SetValue( RenderSurfaceBase.RenderSurfaceTypeProperty, ( object ) value );
        }

        public static string GetRenderSurfaceType( UIElement element )
        {
            return ( string ) element.GetValue( RenderSurfaceBase.RenderSurfaceTypeProperty );
        }

        public event EventHandler<DrawEventArgs> Draw;

        public event EventHandler<RenderedEventArgs> Rendered;

        internal bool IsLicenseValid
        {
            get; set;
        }

        protected abstract TextureCacheBase CreateTextureCache();

        protected RenderSurfaceBase()
        {
            this._textureCache = this.CreateTextureCache();
            Image image = new Image();
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.Stretch = Stretch.None;
            image.IsHitTestVisible = false;
            this._image = image;
            this.Initialize();
            this.SizeChanged += new SizeChangedEventHandler( this.RenderSurfaceSizeChanged );
            UIElementCollection children = this.Grid.Children;
            Rectangle rectangle = new Rectangle();
            rectangle.HorizontalAlignment = HorizontalAlignment.Stretch;
            rectangle.VerticalAlignment = VerticalAlignment.Stretch;
            rectangle.Fill = ( Brush ) new SolidColorBrush( Colors.Transparent );
            rectangle.Tag = ( object ) RenderSurfaceBase.RectIdentifier;
            children.Add( ( UIElement ) rectangle );
            this.Loaded += new RoutedEventHandler( this.OnRenderSurfaceBaseLoaded );
            this.Unloaded += new RoutedEventHandler( this.OnRenderSurfaceBaseUnloaded );
        }

        ~RenderSurfaceBase()
        {
            this.Dispose( false );
        }

        public IServiceContainer Services
        {
            get; set;
        }

        internal Image Image
        {
            get
            {
                return this._image;
            }
        }

        public Grid Grid
        {
            get
            {
                return this._grid;
            }
        }

        protected TextureCacheBase TextureCache
        {
            get
            {
                return this._textureCache;
            }
        }

        public double? MaxFrameRate
        {
            get
            {
                return ( double? ) this.GetValue( RenderSurfaceBase.MaxFrameRateProperty );
            }
            set
            {
                this.SetValue( RenderSurfaceBase.MaxFrameRateProperty, ( object ) value );
            }
        }

        public int ResizeThrottleMs
        {
            get
            {
                return ( int ) this.GetValue( RenderSurfaceBase.ResizeThrottleMsProperty );
            }
            set
            {
                this.SetValue( RenderSurfaceBase.ResizeThrottleMsProperty, ( object ) value );
            }
        }

        public bool UseResizeThrottle
        {
            get
            {
                return ( bool ) this.GetValue( RenderSurfaceBase.UseResizeThrottleProperty );
            }
            set
            {
                this.SetValue( RenderSurfaceBase.UseResizeThrottleProperty, ( object ) value );
            }
        }

        public virtual bool NeedsResizing
        {
            get
            {
                if ( this.RenderWriteableBitmap != null && this.RenderWriteableBitmap.PixelWidth == ( int ) this.ActualWidth )
                    return this.RenderWriteableBitmap.PixelHeight != ( int ) this.ActualHeight;
                return true;
            }
        }

        public virtual bool IsSizeValidForDrawing
        {
            get
            {
                if ( this.ActualWidth >= double.Epsilon && this.ActualHeight >= double.Epsilon && this.ActualWidth.IsRealNumber() )
                    return this.ActualHeight.IsRealNumber();
                return false;
            }
        }

        public ReadOnlyCollection<IRenderableSeries> ChildSeries
        {
            get
            {
                return new ReadOnlyCollection<IRenderableSeries>( ( IList<IRenderableSeries> ) this.Grid.Children.OfType<IRenderableSeries>().ToArray<IRenderableSeries>() );
            }
        }

        public void InvalidateElement()
        {
            this._isDirty = true;
        }

        public void Clear()
        {
            using ( IRenderContext2D renderContext = this.GetRenderContext() )
                renderContext.Clear();
        }

        public bool ContainsSeries( IRenderableSeries renderableSeries )
        {
            UIElement element = renderableSeries as UIElement;
            if ( element != null )
                return this._grid.Children.Contains( element );
            return false;
        }

        public void AddSeries( IEnumerable<IRenderableSeries> renderableSeries )
        {
            foreach ( IRenderableSeries renderableSeries1 in renderableSeries )
                this.AddSeries( renderableSeries1 );
        }

        public void AddSeries( IRenderableSeries renderableSeries )
        {
            this.RemoveSeries( renderableSeries );
            renderableSeries.Services = this.Services;
            FrameworkElement frameworkElement = renderableSeries as FrameworkElement;
            if ( frameworkElement == null )
                return;
            frameworkElement.Visibility = Visibility.Collapsed;
            this._grid.Children.Add( ( UIElement ) frameworkElement );
        }

        //public void RemoveSeries( IRenderableSeries renderableSeries )
        //{
        //    renderableSeries.Services = ( IServiceContainer ) null;
        //    FrameworkElement frameworkElement = renderableSeries as FrameworkElement;
        //    if ( frameworkElement == null )
        //        return;
        //    ( frameworkElement.Parent as Panel ).SafeRemoveChild( ( object ) frameworkElement );
        //}

        public void RemoveSeries( IRenderableSeries renderableSeries )
        {
            renderableSeries.Services = ( IServiceContainer ) null;
            FrameworkElement frameworkElement = renderableSeries as FrameworkElement;
            if ( frameworkElement == null )
            {
                return;
            } ( frameworkElement.Parent as Panel ).SafeRemoveChild( ( object ) frameworkElement );
        }

        public virtual void ClearSeries()
        {
            for ( int index = this._grid.Children.Count - 1 ; index >= 0 ; --index )
            {
                if ( this._grid.Children[ index ] is IRenderableSeriesBase )
                    this._grid.Children.RemoveAt( index );
            }
        }

        //public void Dispose( )
        //{
        //    this.Dispose( true );
        //    GC.SuppressFinalize( ( object ) this );
        //}

        //public void Dispose( bool disposing )
        //{
        //    if ( this._disposed )
        //        return;
        //    this._disposed = true;
        //    this.Dispatcher.BeginInvoke( ( Delegate ) ( ( ) => BindingOperations.ClearAllBindings( ( DependencyObject ) this ) ) );
        //    if ( this._renderTimer != null )
        //    {
        //        Interlocked.Decrement( ref RenderSurfaceBase.instanceCount );
        //        this._renderTimer.Dispose( );
        //        this._renderTimer = ( RenderTimer ) null;
        //    }
        //    this.DisposeUnmanagedResources( );
        //}

        public void Dispose()
        {
            this.Dispose( true );
            GC.SuppressFinalize( ( object ) this );
        }

        public void Dispose( bool disposing )
        {
            if ( !_disposed )
            {
                _disposed = true;
                base.Dispatcher.BeginInvoke( ( Action ) delegate
                                                        {
                                                            BindingOperations.ClearAllBindings( this );
                                                        }
                                           );

                if ( _renderTimer != null )
                {
                    int num = Interlocked.Decrement(ref instanceCount);
                    _renderTimer.Dispose();
                    _renderTimer = null;
                }
                DisposeUnmanagedResources();
            }
        }

        public virtual void RecreateSurface()
        {
            int width = (int) this.ActualWidth;
            int height = (int) this.ActualHeight;
            if ( !this.IsSizeValidForDrawing )
                width = height = 1;
            this.RenderWriteableBitmap = RenderSurfaceBase.CreateWriteableBitmap( width, height );
            this.PublishResizedMessage( width, height );
        }

        protected void PublishResizedMessage( int width, int height )
        {
            if ( this.Services == null )
                return;
            this.Services.GetService<IEventAggregator>().Publish<RenderSurfaceResizedMessage>( new RenderSurfaceResizedMessage( ( object ) this, new Size( ( double ) width, ( double ) height ) ) );
        }

        public abstract IRenderContext2D GetRenderContext();

        protected virtual void DisposeUnmanagedResources()
        {
        }

        protected virtual void OnRenderTimeElapsed()
        {
            if ( !this._isDirty )
                return;
            this._isDirty = false;
            this.OnDraw();
        }

        protected virtual void OnDraw()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<DrawEventArgs> draw = this.Draw;
            if ( draw == null )
                return;
            Stopwatch stopwatch = Stopwatch.StartNew();
            draw( ( object ) this, new DrawEventArgs( ( IRenderSurface2D ) this ) );
            stopwatch.Stop();
            this.OnRendered( ( double ) stopwatch.ElapsedMilliseconds );
        }

        protected virtual void OnRendered( double duration )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<RenderedEventArgs> rendered = this.Rendered;
            if ( rendered == null )
                return;
            rendered( ( object ) this, new RenderedEventArgs( duration ) );
        }

        protected virtual void OnRenderSurfaceBaseLoaded( object sender, RoutedEventArgs e )
        {
            Binding binding = new Binding()
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor)
                {
                    AncestorType = typeof (UltrachartSurfaceBase)
                },
                Path = new PropertyPath("MaxFrameRate", new object[0])
            };
            this.SetBinding( RenderSurfaceBase.MaxFrameRateProperty, ( BindingBase ) binding );
            this.StopTimer();
            this.StartTimer();
            this.OnRenderTimeElapsed();
        }

        protected virtual void OnRenderSurfaceBaseUnloaded( object sender, RoutedEventArgs e )
        {
            this.StopTimer();
        }

        private void StopTimer()
        {
            if ( this._renderTimer == null )
                return;
            Interlocked.Decrement( ref RenderSurfaceBase.instanceCount );
            this._renderTimer.Dispose();
            this._renderTimer = ( RenderTimer ) null;
        }

        private void StartTimer()
        {
            if ( this.Services == null || this._disposed )
                return;
            if ( this._renderTimer != null )
            {
                Interlocked.Decrement( ref RenderSurfaceBase.instanceCount );
                this._renderTimer.Dispose();
                this._renderTimer = ( RenderTimer ) null;
            }
            Interlocked.Increment( ref RenderSurfaceBase.instanceCount );
            this._renderTimer = new RenderTimer( this.MaxFrameRate, this.Services.GetService<IDispatcherFacade>(), new Action( this.OnRenderTimeElapsed ) );
        }

        private void RenderSurfaceSizeChanged( object sender, SizeChangedEventArgs sizeChangedEventArgs )
        {
            if ( this.UseResizeThrottle )
            {
                if ( this._recreateSurfaceTimedMethod != null )
                    this._recreateSurfaceTimedMethod.Dispose();
                this._recreateSurfaceTimedMethod = TimedMethod.Invoke( ( Action ) ( () =>
                {
                    this.RecreateSurface();
                    this.InvalidateElement();
                } ) ).After( this.ResizeThrottleMs ).WithPriority( DispatcherPriority.Render ).Go();
            }
            else
            {
                this.RecreateSurface();
                this.InvalidateElement();
            }
        }

        private static void OnMaxFramerateChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            double? newValue = (double?) e.NewValue;
            if ( newValue.HasValue )
            {
                if ( ( ( IComparable ) newValue ).IsDefined() )
                {
                    double? nullable = newValue;
                    double num1 = 0.0;
                    if ( !( nullable.GetValueOrDefault() <= num1 & nullable.HasValue ) )
                    {
                        nullable = newValue;
                        double num2 = 100.0;
                        if ( !( nullable.GetValueOrDefault() > num2 & nullable.HasValue ) )
                            goto label_5;
                    }
                }
                throw new ArgumentException( string.Format( "{0}.MaxFramerate must be greater than 0.0 and less than or equal to 100.0", ( object ) d.GetType().Name ) );
            }
        label_5:
            ( ( RenderSurfaceBase ) d ).StopTimer();
            ( ( RenderSurfaceBase ) d ).StartTimer();
        }

        private static void OnRenderSurfaceTypeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = d as UltrachartSurface;
            string newValue = e.NewValue as string;
            if ( ultrachartSurface == null || newValue.IsNullOrWhiteSpace() )
                return;
            Type type = Type.GetType(newValue);
            if ( !( type != ( Type ) null ) )
                return;
            IRenderSurface instance = Activator.CreateInstance(type) as IRenderSurface;
            if ( instance == null )
                return;
            ultrachartSurface.RenderSurface = instance;
        }

        private static WriteableBitmap CreateWriteableBitmap( int width, int height )
        {
            return BitmapFactory.New( width, height );
        }

        [Obfuscation( Exclude = false, Feature = "encryptmethod" )]
        private void Initialize()
        {
            this.IsTabStop = false;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.VerticalContentAlignment = VerticalAlignment.Stretch;
            this._grid = new Grid();
            Device.SetSnapsToDevicePixels( ( DependencyObject ) this, true );
            Device.SetSnapsToDevicePixels( ( DependencyObject ) this._grid, true );
            Device.SetSnapsToDevicePixels( ( DependencyObject ) this._image, true );
            this._grid.Children.Add( ( UIElement ) this._image );
            new LicenseManager().Validate<RenderSurfaceBase>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
            this.Content = ( object ) this._grid;
        }

        public Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            return ElementExtensions.TranslatePoint( this, point, relativeTo );
        }

        public bool IsPointWithinBounds( Point point )
        {
            return ElementExtensions.IsPointWithinBounds( this, point );
        }

        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            return ElementExtensions.GetBoundsRelativeTo( this, relativeTo );
        }

        [SpecialName]
        Style IRenderSurface.Style
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
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }

        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }
    }
}
