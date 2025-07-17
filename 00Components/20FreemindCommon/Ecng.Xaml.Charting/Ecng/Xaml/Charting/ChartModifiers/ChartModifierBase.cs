// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ChartModifierBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TinyMessenger;

namespace fx.Xaml.Charting
{
    public abstract class ChartModifierBase : ApiElementBase, IChartModifier, IChartModifierBase, IReceiveMouseEvents, INotifyPropertyChanged, IXmlSerializable
    {
        public static readonly DependencyProperty ReceiveHandledEventsProperty = DependencyProperty.Register(nameof (ReceiveHandledEvents), typeof (bool), typeof (ChartModifierBase), new PropertyMetadata((object) false));
        public new static readonly DependencyProperty IsEnabledProperty        = DependencyProperty.RegisterAttached(nameof (IsEnabled), typeof (bool), typeof (ChartModifierBase), new PropertyMetadata((object) true, new PropertyChangedCallback(ChartModifierBase.OnIsEnabledChangedInternal)));
        public static readonly DependencyProperty ExecuteOnProperty            = DependencyProperty.Register(nameof (ExecuteOn), typeof (ExecuteOn), typeof (ChartModifierBase), new PropertyMetadata((object) ExecuteOn.MouseLeftButton));
        public static readonly DependencyProperty MouseModifierProperty        = DependencyProperty.Register(nameof (MouseModifier), typeof (MouseModifier), typeof (ChartModifierBase), new PropertyMetadata((object) MouseModifier.None));
        private ISciChartSurface _ultraChartSurface;
        private static Dictionary<MouseButtons, ExecuteOn> _executeOnMap;
        private IServiceContainer _services;
        private TinyMessageSubscriptionToken _renderedToken;
        private TinyMessageSubscriptionToken _resizedToken;
        private bool _isLeftButtonDown;
        private bool _isMiddleButtonDown;
        private bool _isRightButtonDown;

        protected ChartModifierBase()
        {
            SetName();
            DataContextChanged += ( DependencyPropertyChangedEventHandler ) ( ( s, e ) => ChartModifierBase.OnDataContextChangedInternal( ( DependencyObject ) s, e ) );
            IsPolarChartSupported = true;
        }

        public virtual bool CanReceiveMouseEvents()
        {
            if ( IsEnabled && IsAttached && ( ModifierSurface != null && ParentSurface != null ) )
            {
                return ParentSurface.IsVisible;
            }

            return false;
        }

        public override void OnAttached()
        {
            AssertPolarChartIsSupported();
        }

        private void AssertPolarChartIsSupported()
        {
            if ( !IsPolarChartSupported && XAxis != null && XAxis.IsPolarAxis )
            {
                throw new NotSupportedException( string.Format( "{0} is not supported by PolarXAxis.", ( object ) GetType().Name ) );
            }
        }

        public override void OnDetached()
        {
        }

        public new bool IsEnabled
        {
            get
            {
                return ( bool ) GetValue( ChartModifierBase.IsEnabledProperty );
            }
            set
            {
                SetValue( ChartModifierBase.IsEnabledProperty, ( object ) value );
            }
        }

        public ExecuteOn ExecuteOn
        {
            get
            {
                return ( ExecuteOn ) GetValue( ChartModifierBase.ExecuteOnProperty );
            }
            set
            {
                SetValue( ChartModifierBase.ExecuteOnProperty, ( object ) value );
            }
        }

        public MouseModifier MouseModifier
        {
            get
            {
                return ( MouseModifier ) GetValue( ChartModifierBase.MouseModifierProperty );
            }
            set
            {
                SetValue( ChartModifierBase.MouseModifierProperty, ( object ) value );
            }
        }

        public bool ReceiveHandledEvents
        {
            get
            {
                return ( bool ) GetValue( ChartModifierBase.ReceiveHandledEventsProperty );
            }
            set
            {
                SetValue( ChartModifierBase.ReceiveHandledEventsProperty, ( object ) value );
            }
        }

        public string ModifierName
        {
            get; protected set;
        }

        public bool IsMouseLeftButtonDown
        {
            get
            {
                return _isLeftButtonDown;
            }
        }

        public bool IsMouseMiddleButtonDown
        {
            get
            {
                return _isMiddleButtonDown;
            }
        }

        public bool IsMouseRightButtonDown
        {
            get
            {
                return _isRightButtonDown;
            }
        }

        public string MouseEventGroup
        {
            get; set;
        }

        public virtual void OnModifierDoubleClick( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierMouseDown( ModifierMouseArgs e )
        {
            _isLeftButtonDown = e.MouseButtons == MouseButtons.Left;
            _isRightButtonDown = e.MouseButtons == MouseButtons.Right;
            _isMiddleButtonDown = e.MouseButtons == MouseButtons.Middle;
        }

        public virtual void OnModifierMouseMove( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierMouseUp( ModifierMouseArgs e )
        {
            _isLeftButtonDown = e.MouseButtons != MouseButtons.Left && _isLeftButtonDown;
            _isRightButtonDown = e.MouseButtons != MouseButtons.Right && _isRightButtonDown;
            _isMiddleButtonDown = e.MouseButtons != MouseButtons.Middle && _isMiddleButtonDown;
        }

        public virtual void OnModifierMouseWheel( ModifierMouseArgs e )
        {
        }

        public virtual void OnModifierTouchDown( ModifierTouchManipulationArgs e )
        {
        }

        public virtual void OnModifierTouchMove( ModifierTouchManipulationArgs e )
        {
        }

        public virtual void OnModifierTouchUp( ModifierTouchManipulationArgs e )
        {
        }

        public override IServiceContainer Services
        {
            get
            {
                return _services;
            }
            set
            {
                if ( _services != null )
                {
                    if ( _renderedToken != null )
                    {
                        _services.GetService<IEventAggregator>().Unsubscribe<UltrachartRenderedMessage>( _renderedToken );
                    }

                    if ( _resizedToken != null )
                    {
                        _services.GetService<IEventAggregator>().Unsubscribe<UltrachartResizedMessage>( _resizedToken );
                    }
                }
                _services = value;
                if ( _services == null )
                {
                    return;
                }

                _renderedToken = _services.GetService<IEventAggregator>().Subscribe<UltrachartRenderedMessage>( new Action<UltrachartRenderedMessage>( OnParentSurfaceRendered ), true );
                _resizedToken = _services.GetService<IEventAggregator>().Subscribe<UltrachartResizedMessage>( new Action<UltrachartResizedMessage>( OnParentSurfaceResized ), true );
            }
        }

        public override ISciChartSurface ParentSurface
        {
            get
            {
                return _ultraChartSurface;
            }
            set
            {
                UltrachartSurface ultraChartSurface1 = _ultraChartSurface as UltrachartSurface;
                if ( ultraChartSurface1 != null )
                {
                    ultraChartSurface1.MouseLeave -= new MouseEventHandler( UltrachartSurfaceMouseLeave );
                    ultraChartSurface1.MouseEnter -= new MouseEventHandler( UltrachartSurfaceMouseEnter );
                    ultraChartSurface1.SelectedRenderableSeries.CollectionChanged -= new NotifyCollectionChangedEventHandler( SelectedRenderableSeriesCollectionChanged );
                }
                _ultraChartSurface = value;
                UltrachartSurface ultraChartSurface2 = _ultraChartSurface as UltrachartSurface;
                if ( ultraChartSurface2 != null )
                {
                    ultraChartSurface2.MouseLeave += new MouseEventHandler( UltrachartSurfaceMouseLeave );
                    ultraChartSurface2.MouseEnter += new MouseEventHandler( UltrachartSurfaceMouseEnter );
                    ultraChartSurface2.SelectedRenderableSeries.CollectionChanged += new NotifyCollectionChangedEventHandler( SelectedRenderableSeriesCollectionChanged );
                }
                OnPropertyChanged( nameof( ParentSurface ) );
            }
        }

        internal bool IsPolarChartSupported
        {
            get; set;
        }

        public Point GetPointRelativeTo( Point point, IHitTestable relativeTo )
        {
            return RootGrid.TranslatePoint( point, relativeTo );
        }

        public bool IsPointWithinBounds( Point mousePoint, IHitTestable hitTestable )
        {
            return hitTestable.IsPointWithinBounds( mousePoint );
        }

        [Obsolete( "Use GetPointRelativeTo instead" )]
        public Point GetRelativePosition( Point point, IHitTestable relativeTo )
        {
            throw new NotImplementedException();
        }

        protected virtual void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
        }

        protected virtual void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
        }

        protected virtual void OnAnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
        }

        public virtual void OnParentSurfaceResized( UltrachartResizedMessage e )
        {
        }

        public virtual void OnParentSurfaceRendered( UltrachartRenderedMessage e )
        {
        }

        protected void SetCursor( Cursor cursor )
        {
            if ( ParentSurface == null )
            {
                return;
            }

            ParentSurface.SetMouseCursor( cursor );
        }

        protected virtual void OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
        }

        protected virtual void OnIsEnabledChanged()
        {
        }

        public virtual void OnMasterMouseLeave( ModifierMouseArgs e )
        {
            OnParentSurfaceMouseLeave();
        }

        protected virtual void OnParentSurfaceMouseLeave()
        {
        }

        protected virtual void OnParentSurfaceMouseEnter()
        {
        }

        protected virtual void OnSelectedSeriesChanged( IEnumerable<IRenderableSeries> oldSeries, IEnumerable<IRenderableSeries> newSeries )
        {
        }

        protected bool MatchesExecuteOn( MouseButtons mouseButtons, ExecuteOn executeOn )
        {
            MouseModifier currentModifier = MouseExtensions.GetCurrentModifier();
            if ( ChartModifierBase._executeOnMap == null )
            {
                ChartModifierBase._executeOnMap = new Dictionary<MouseButtons, ExecuteOn>();
                ChartModifierBase._executeOnMap.Add( MouseButtons.None, ExecuteOn.MouseMove );
                ChartModifierBase._executeOnMap.Add( MouseButtons.Left, ExecuteOn.MouseLeftButton );
                ChartModifierBase._executeOnMap.Add( MouseButtons.Middle, ExecuteOn.MouseMiddleButton );
                ChartModifierBase._executeOnMap.Add( MouseButtons.Right, ExecuteOn.MouseRightButton );
            }
            if ( ChartModifierBase._executeOnMap.ContainsKey( mouseButtons ) && ChartModifierBase._executeOnMap[ mouseButtons ] == executeOn )
            {
                return ( MouseModifier & currentModifier ) == currentModifier;
            }

            return false;
        }

        public XmlSchema GetSchema()
        {
            return ( XmlSchema ) null;
        }

        public virtual void ReadXml( XmlReader reader )
        {
            if ( reader.MoveToContent() != XmlNodeType.Element )
            {
                return;
            }

            ChartModifierSerializationHelper.Instance.DeserializeProperties( this, reader );
        }

        public virtual void WriteXml( XmlWriter writer )
        {
            ChartModifierSerializationHelper.Instance.SerializeProperties( this, writer );
        }

        public virtual void ResetInertia()
        {
        }

        void IChartModifier.OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            AssertPolarChartIsSupported();
            OnXAxesCollectionChanged( sender, args );
        }

        void IChartModifier.OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            OnYAxesCollectionChanged( sender, args );
        }

        void IChartModifier.OnAnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            OnAnnotationCollectionChanged( sender, args );
        }

        private static void OnDataContextChangedInternal( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ChartModifierBase ) d ).OnDataContextChanged( ( object ) d, e );
        }

        private static void OnIsEnabledChangedInternal( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ChartModifierBase ) d ).OnIsEnabledChanged();
        }

        private void SetName()
        {
            string str = GetType().ToString();
            int num = str.LastIndexOf('.');
            ModifierName = str.Substring( num + 1 );
        }

        private void UltrachartSurfaceMouseLeave( object sender, MouseEventArgs e )
        {
            OnParentSurfaceMouseLeave();
        }

        private void UltrachartSurfaceMouseEnter( object sender, MouseEventArgs e )
        {
            OnParentSurfaceMouseEnter();
        }

        private void SelectedRenderableSeriesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            OnSelectedSeriesChanged( e.OldItems != null ? e.OldItems.Cast<IRenderableSeries>() : ( IEnumerable<IRenderableSeries> ) null, e.NewItems != null ? e.NewItems.Cast<IRenderableSeries>() : ( IEnumerable<IRenderableSeries> ) null );
        }

        [SpecialName]
        object IChartModifierBase.DataContext
        {
            get
            {
                return DataContext;
            }

            set
            {
                DataContext = value;
            }
        }
    }
}
