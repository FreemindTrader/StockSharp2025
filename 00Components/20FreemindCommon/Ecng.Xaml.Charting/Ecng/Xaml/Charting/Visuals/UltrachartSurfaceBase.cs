// Ecng.Xaml.Charting.Visuals.UltrachartSurfaceBase
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Utility;
using TinyMessenger;

namespace Ecng.Xaml.Charting.Visuals
{

    [TemplatePart( Name = "PART_ChartModifierSurface", Type = typeof( ChartModifierSurface ) )]
    [TemplatePart( Name = "PART_MainGrid", Type = typeof( Grid ) )]
    public abstract class UltrachartSurfaceBase : Control, IUltrachartSurfaceBase, ISuspendable, IInvalidatableElement
    {
        public static readonly DependencyProperty ClipModifierSurfaceProperty = DependencyProperty.Register("ClipModifierSurface", typeof(bool), typeof(UltrachartSurfaceBase), new PropertyMetadata(false));

        public static readonly DependencyProperty ChartTitleProperty = DependencyProperty.Register("ChartTitle", typeof(string), typeof(UltrachartSurfaceBase), new PropertyMetadata(null, OnInvalidateUltrachartSurface));

        public static readonly DependencyProperty RenderSurfaceProperty = DependencyProperty.Register("RenderSurface", typeof(IRenderSurface), typeof(UltrachartSurfaceBase), new PropertyMetadata(null, delegate(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        ((UltrachartSurfaceBase)s).OnRenderSurfaceDependencyPropertyChanged(e);
    }));

        public static readonly DependencyProperty MaxFrameRateProperty = DependencyProperty.Register("MaxFrameRate", typeof(double?), typeof(UltrachartSurfaceBase), new PropertyMetadata(null));

        private ChartModifierSurface _modifierSurface;

        private readonly object _syncRoot = new object();

        private volatile bool _isLoaded;

        private volatile bool _disposed;

        private IServiceContainer _serviceContainer;

        private MainGrid _rootGrid;

        private IRenderSurface _rsCache;

        private readonly RoutedEventHandler _loadedHandler;

        private readonly RoutedEventHandler _unloadedHandler;

        private readonly SizeChangedEventHandler _sizeChangedHandler;

        private readonly DependencyPropertyChangedEventHandler _dataContextChangedHandler;

        private volatile bool _firstEverLoaded;

        public bool DebugWhyDoesntUltrachartRender
        {
            get;
            set;
        }

        public double? MaxFrameRate
        {
            get
            {
                return ( double? ) GetValue( MaxFrameRateProperty );
            }
            set
            {
                SetValue( MaxFrameRateProperty, value );
            }
        }

        public IServiceContainer Services
        {
            get
            {
                return _serviceContainer;
            }
            protected internal set
            {
                _serviceContainer = value;
            }
        }

        protected bool IsDisposed => _disposed;

        protected bool IsUltrachartSurfaceLoaded => _isLoaded;

        public object SyncRoot => _syncRoot;

        public string ChartTitle
        {
            get
            {
                return ( string ) GetValue( ChartTitleProperty );
            }
            set
            {
                SetValue( ChartTitleProperty, value );
            }
        }

        public bool ClipModifierSurface
        {
            get
            {
                return ( bool ) GetValue( ClipModifierSurfaceProperty );
            }
            set
            {
                SetValue( ClipModifierSurfaceProperty, value );
            }
        }

        public bool IsSuspended
        {
            get
            {
                if ( !UpdateSuspender.GetIsSuspended( this ) )
                {
                    return !_firstEverLoaded;
                }
                return true;
            }
        }

        public IChartModifierSurface ModifierSurface => _modifierSurface;

        public RenderPriority RenderPriority
        {
            get;
            set;
        }

        public IRenderSurface RenderSurface
        {
            get
            {
                return ( IRenderSurface ) GetValue( RenderSurfaceProperty );
            }
            set
            {
                SetValue( RenderSurfaceProperty, value );
            }
        }

        public IMainGrid RootGrid => _rootGrid;

        public event EventHandler<EventArgs> Rendered;

        protected UltrachartSurfaceBase()
        {
            UltrachartDebugLogger.Instance.WriteLine( "Instantiating {0}", GetType().Name );
            _serviceContainer = new ServiceContainer();
            RegisterServices( _serviceContainer );
            _loadedHandler = delegate
            {
                OnUltrachartSurfaceLoaded();
            };
            _unloadedHandler = delegate
            {
                OnUltrachartSurfaceUnloaded();
            };
            _sizeChangedHandler = delegate
            {
                OnUltrachartSurfaceSizeChanged();
            };
            _dataContextChangedHandler = delegate ( object s, DependencyPropertyChangedEventArgs e )
            {
                OnDataContextChanged( e );
            };
            base.SizeChanged += _sizeChangedHandler;
            base.Loaded += _loadedHandler;
            base.Unloaded += _unloadedHandler;
            base.DataContextChanged += _dataContextChangedHandler;
            DebugWhyDoesntUltrachartRender = false;
            RenderPriority = RenderPriority.Normal;
        }

        ~UltrachartSurfaceBase()
        {
            Dispose( false );
        }

        public virtual void OnLoad()
        {
            OnUltrachartSurfaceLoaded();
        }

        public IUpdateSuspender SuspendUpdates()
        {
            return new UpdateSuspender( this );
        }

        public void ResumeUpdates( IUpdateSuspender suspender )
        {
            if ( suspender.ResumeTargetOnDispose )
            {
                InvalidateElement();
            }
        }

        public void DecrementSuspend()
        {
        }

        public override void OnApplyTemplate()
        {
            UltrachartDebugLogger.Instance.WriteLine( "OnApplyTemplate" );
            base.OnApplyTemplate();
            ServiceContainer serviceContainer = (ServiceContainer)Services;
            if ( serviceContainer.HasService<IChartModifierSurface>() )
            {
                serviceContainer.DeRegisterService<IChartModifierSurface>();
            }
            _modifierSurface = GetAndAssertTemplateChild<ChartModifierSurface>( "PART_ChartModifierSurface" );
            _rootGrid = GetAndAssertTemplateChild<MainGrid>( "PART_MainGrid" );
            ( ( ServiceContainer ) Services ).RegisterService( ModifierSurface );
        }

        public virtual void InvalidateElement()
        {
            if ( IsSuspended )
            {
                UltrachartDebugLogger.Instance.WriteLine( "UltrachartSurface.IsSuspended=true. Ignoring InvalidateElement() call" );
            }
            else if ( DispatcherUtil.GetTestMode() || RenderPriority == RenderPriority.Immediate )
            {
                Services.GetService<IDispatcherFacade>().BeginInvokeIfRequired( DoDrawingLoop, DispatcherPriority.Normal );
            }
            else if ( _rsCache != null )
            {
                _rsCache.InvalidateElement();
            }
        }

        public virtual void OnUltrachartRendered()
        {
            this.Rendered?.Invoke( this, EventArgs.Empty );
        }

        public void SetMouseCursor( Cursor cursor )
        {
            SetCurrentValue( FrameworkElement.CursorProperty, cursor );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( !_disposed )
            {
                base.Dispatcher.BeginInvoke( ( Action ) delegate
                {
                    base.DataContextChanged -= _dataContextChangedHandler;
                    base.SizeChanged -= _sizeChangedHandler;
                    base.Loaded -= _loadedHandler;
                    base.Unloaded -= _unloadedHandler;
                    OnUltrachartSurfaceUnloaded();
                    if ( _rootGrid != null )
                    {
                        _rootGrid.UnregisterEventsOnShutdown();
                    }
                } );
                _disposed = true;
                base.Dispatcher.BeginInvoke( ( Action ) delegate
                {
                    RenderSurface = null;
                    BindingOperations.ClearAllBindings( this );
                } );
            }
        }

        protected static void OnInvalidateUltrachartSurface( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as IInvalidatableElement )?.InvalidateElement();
        }

        protected T GetAndAssertTemplateChild<T>( string childName ) where T : class
        {
            T val = GetTemplateChild(childName) as T;
            if ( val == null )
            {
                throw new InvalidOperationException( $"Unable to Apply the Control Template. {childName} is missing or of the wrong type" );
            }
            return val;
        }

        protected virtual void OnUltrachartSurfaceUnloaded()
        {
            _isLoaded = false;
        }

        protected virtual void OnUltrachartSurfaceLoaded()
        {
            _firstEverLoaded = true;
            _isLoaded = true;
            InvalidateElement();
        }

        protected virtual void OnUltrachartSurfaceSizeChanged()
        {
        }

        protected virtual void OnDataContextChanged( DependencyPropertyChangedEventArgs e )
        {
            UltrachartDebugLogger.Instance.WriteLine( "OnDataContextChanged" );
        }

        protected virtual void OnRenderSurfaceDependencyPropertyChanged( DependencyPropertyChangedEventArgs e )
        {
            _rsCache = ( e.NewValue as IRenderSurface );
        }

        protected virtual void RegisterServices( IServiceContainer serviceContainer )
        {
            serviceContainer.RegisterService( ( IDispatcherFacade ) new DispatcherUtil( GetDispatcher( this ) ) );
            serviceContainer.RegisterService( ( IEventAggregator ) new TinyMessengerHub() );
        }

        protected abstract void DoDrawingLoop();

        protected internal void OnRenderFault( Exception caught )
        {
            string text = $" {GetType().Name} didn't render, because an exception was thrown:\n  Message: {caught.Message}\n  Stack Trace: {caught.StackTrace}";
            Console.WriteLine( text );
            UltrachartDebugLogger.Instance.WriteLine( text );
        }

        internal void OnDataSeriesUpdated( object sender, EventArgs e )
        {
            if ( RenderPriority != RenderPriority.Manual )
            {
                InvalidateElement();
            }
        }

        internal static Dispatcher GetDispatcher( DependencyObject obj )
        {
            return obj.Dispatcher;
        }



        bool IUltrachartSurfaceBase.IsVisible
        {
            get
            {
                return base.IsVisible;
            }
            //ILSpy generated this explicit interface implementation from .override directive in get_IsVisible

        }
    }
}