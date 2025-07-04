using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using SciChart.Charting.Common;
using StockSharp.Localization;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using fx.Charting.ATony;
using fx.Charting.Definitions;
using SciChart.Charting.ChartModifiers;
using StockSharp.Charting;

namespace fx.Charting
{
    
    public class ChartArea : ChartPart< ChartArea >, IDisposable, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable
    {
        public static readonly string XAxisId = "X";
        public static readonly string YAxisId = "Y";

        private readonly SynchronizedList< string >   _stackTrace = new SynchronizedList< string >( );
        private ChartAxisType                         _xAxisType = ChartAxisType.CategoryDateTime;
        private IScichartSurfaceVM                     _chartSurfaceVM;
        private IChart                                _chart;
        private string                                _title;
        private float                                 _height;
        private readonly INotifyList< IChartElement > _chartElementNotifyList;
        private readonly INotifyList< ChartAxis >        _xAxisNotifyList;
        private readonly INotifyList< ChartAxis >        _yAxisNotifyList;
        private int _indicatorCount = 0;

        public ChartArea( )
        {
            _chartElementNotifyList = new ChartElementNotifyList( this );
            _xAxisNotifyList        = new AxisNotifyList( this, XAxisId, ( a, i ) => ( ( int )a.AxisType ).ToString( ) + a.Id );
            _yAxisNotifyList        = new AxisNotifyList( this, YAxisId, ( a, i ) => ( ( int )a.AxisType ).ToString( ) + a.Id + i );
            InitAxises( );            
            Height = 100f;
        }


        public ChartArea( int count )
        {
            _indicatorCount = count;
            _chartElementNotifyList = new ChartElementNotifyList( this );
            _xAxisNotifyList = new AxisNotifyList( this, XAxisId, ( a, i ) => ( ( int ) a.AxisType ).ToString() + a.Id );
            _yAxisNotifyList = new AxisNotifyList( this, YAxisId, ( a, i ) => ( ( int ) a.AxisType ).ToString() + a.Id + i );
            InitAxises( count  );
            Height = 200f;
        }


        public IScichartSurfaceVM ChartSurfaceViewModel
        {
            get
            {
                return _chartSurfaceVM;
            }

            set
            {
                _chartSurfaceVM = value;
            }
            
        }

        private void InitAxises( )
        {
            if( XAxises.FirstOrDefault( x => x.Id == XAxisId ) == null )
            {
                XAxises.Add( new ChartAxis( )
                                            {
                                                Id = XAxisId,                                                
                                                AxisType = ChartAxisType.CategoryDateTime
                                            } );
            }

            if( YAxises.FirstOrDefault( y => y.Id == YAxisId ) != null )
            {
                return;
            }

            var yAxis = new ChartAxis( ) { Id = YAxisId, AxisType = ChartAxisType.Numeric };            

            YAxises.Add( yAxis );
        }

        private void InitAxises( int count )
        {
            string newX = XAxisId;// + count.ToString( );

            if ( XAxises.FirstOrDefault( x => x.Id == newX ) == null )
            {
                XAxises.Add( new ChartAxis()
                {
                    Id = newX,
                    AxisType = ChartAxisType.CategoryDateTime
                } );
            }

            string newY = YAxisId;// + count.ToString( );

            if ( YAxises.FirstOrDefault( y => y.Id == newY ) == null )
            {
                YAxises.Add( new ChartAxis()
                {
                    Id = newY,
                    AxisType = ChartAxisType.Numeric
                } );
            }
        }

        internal SynchronizedList< string > GetStackTrace( )
        {
            return _stackTrace;
        }

        [Browsable( false )]
        public IChart Chart
        {
            get
            {
                return _chart;
            }
            internal set
            {
                if( _chart == value )
                {
                    return;
                }

                GetStackTrace( ).Add( string.Format( "(tid={0})\n", Thread.CurrentThread.ManagedThreadId ) + Environment.StackTrace );

                if( value == null )
                {
                    ChartSurfaceViewModel.Release( );
                }

                _chart = value;

                if( value == null )
                {
                    return;
                }

                int count = 0;

                if ( value is ChartExViewModel )
                {
                    count =( ( ChartExViewModel) value ).ChartCount;
                }

                

                if ( count > 0 )
                {
                    InitAxises( count );
                }
                else
                {
                    InitAxises( );
                }
                

                ChartSurfaceViewModel.InitPropertiesEventHandlers( );
            }
        }



        [Browsable( false )]
        public ChartAxisType XAxisType
        {
            get
            {
                return _xAxisType;
            }
            set
            {
                if( _xAxisType == value )
                {
                    return;
                }
                if( Chart != null )
                {
                    Chart.EnsureUIThread( );
                }

                if( Elements.Cast< IElementWithXYAxes >( ).Any( i => !i.CheckAxesCompatible( new ChartAxisType?( value ), new ChartAxisType?( ) ) ) )
                {
                    throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.ElementDontSupportAxisTypeParams, value ) );
                }

                if( Chart != null && Chart.XAxisType != value )
                {
                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                }

                _xAxisType = value;

                PooledList< ChartAxis > chartAxisList = new PooledList< ChartAxis >( );

                foreach( ChartAxis xAxis in XAxises )
                {
                    ChartAxis chartAxis = new ChartAxis( )
                                                        {
                                                            Id        = xAxis.Id,
                                                            AutoRange = xAxis.AutoRange,
                                                            AxisType  = _xAxisType
                                                        };
                    chartAxisList.Add( chartAxis );
                }

                IChart resetChart = Chart;
                Chart = null;

                XAxises.RemoveRange( XAxises.ToArray( ) );
                XAxises.AddRange( chartAxisList );
                Chart = resetChart;
            }
        }

        [Display( Description = "Str1905", GroupName = "Common", Name = "Name", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged( nameof( Title ) );
            }
        }

        [Browsable( false )]
        public float Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        [Browsable( false )]
        public INotifyList< IChartElement > Elements
        {
            get
            {
                return _chartElementNotifyList;
            }
        }

        public INotifyList< ChartAxis > XAxises
        {
            get
            {
                return _xAxisNotifyList;
            }
        }

        public INotifyList< ChartAxis > YAxises
        {
            get
            {
                return _yAxisNotifyList;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            Elements.Clear( );
            base.Load( storage );
            Title     = storage.GetValue< string >( "Title", null );
            Height    = storage.GetValue< float >( "Height", 0 );
            XAxisType = ChartAxisType.CategoryDateTime;
            //XAxisType = storage.GetValue<ChartAxisType>( "XAxisType", ChartAxisType.CategoryDateTime );
            LoadAxises( storage, "XAxises", XAxises );
            LoadAxises( storage, "YAxises", YAxises );
            LoadChartElements< CandlestickUI >( storage, "Candles" );
            LoadChartElements< IndicatorUI >( storage, "Indicators" );
            LoadChartElements< TradesUI >( storage, "Trades" );
            LoadChartElements< OrdersUI >( storage, "Orders" );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Title", Title );
            storage.SetValue( "Height", Height );
            storage.SetValue( "XAxisType", XAxisType );
            storage.SetValue( "XAxises", XAxises.Select( x => x.Save( ) ).ToArray( ) );
            storage.SetValue( "YAxises", YAxises.Select( y => y.Save( ) ).ToArray( ) );
            SaveChartElments< CandlestickUI >( storage, "Candles" );
            SaveChartElments< IndicatorUI >( storage, "Indicators" );
            SaveChartElments< TradesUI >( storage, "Trades" );
            SaveChartElments< OrdersUI >( storage, "Orders" );
        }

        private void LoadChartElements< T >( SettingsStorage settings, string name ) where T : ChartElement< T >, new()
        {
            Elements.AddRange( settings.GetValue< IEnumerable< SettingsStorage > >( name, null ).Select( s => s.Load< T >( ) ) );
        }

        private void SaveChartElments< T >( SettingsStorage settings, string name ) where T : ChartElement< T >
        {
            settings.SetValue( name, Elements.OfType< T >( ).Select( s => s.Save( ) ).ToArray( ) );
        }

        private static void LoadAxises( SettingsStorage settings, string axisName, ICollection< ChartAxis > Axises )
        {
            var source = settings.GetValue< IEnumerable< SettingsStorage > >( axisName, null );

            if( source == null )
            {
                return;
            }
            Axises.Clear( );

            Axises.AddRange( source.Select( s => s.Load< ChartAxis >( ) ) );
        }

        public override ChartArea Clone( )
        {
            ChartArea chartArea = Clone( new ChartArea( )
            {
                Title = Title,
                Height = Height,
                XAxisType = XAxisType
            } );

            chartArea.Elements.AddRange( Elements.Select( e => e.Clone( ) ) );

            chartArea.XAxises.Clear( );
            chartArea.XAxises.AddRange( XAxises.Select( x => x.Clone( ) ) );

            chartArea.YAxises.Clear( );
            chartArea.YAxises.AddRange( YAxises.Select( y => y.Clone( ) ) );
            return chartArea;
        }

        public override string ToString( )
        {
            return Title;
        }

        public void Dispose( )
        {
            ChartSurfaceViewModel.Dispose( );
        }

        private sealed class AxisNotifyList : PropertiesNotifyList< ChartAxis >
        {
            private static int _xAxisCount;
            private static int _yAxisCount;
            private readonly string _defaultId;
            private readonly ChartArea _chartArea;
            private readonly Func< ChartAxis, int, string > _getDefaultGroupFunc;

            public AxisNotifyList( ChartArea chartArea_1, string string_1, Func< ChartAxis, int, string > func_1 )
            {
                ChartArea chartArea = chartArea_1;
                if( chartArea == null )
                {
                    throw new ArgumentNullException( "area" );
                }
                _chartArea = chartArea;
                string str = string_1;
                if( str == null )
                {
                    throw new ArgumentNullException( "defaultId" );
                }
                _defaultId = str;
                Func< ChartAxis, int, string > func = func_1;
                if( func == null )
                {
                    throw new ArgumentNullException( "getDefaultGroup" );
                }
                _getDefaultGroupFunc = func;
            }

            private static int GetAxisCount( string string_1 )
            {
                if( !( string_1 == XAxisId ) )
                {
                    return ++_yAxisCount;
                }
                return ++_xAxisCount;
            }

            protected override bool OnAdding( ChartAxis axis )
            {
                int num = GetAxisCount( _defaultId );

                if( StringHelper.IsEmpty( axis.Id ) )
                {
                    axis.Id = string.Format( "{0}({1})", _defaultId, Guid.NewGuid( ) );
                }

                if( this.Any( a => a.Id == axis.Id ) )
                {
                    throw new InvalidOperationException( "StringHelper.Put( LocalizedStrings.Str1904Params, axis.Id )" );
                }

                if( this == _chartArea.XAxises && axis.AxisType != _chartArea.XAxisType )
                {
                    axis.AxisType = _chartArea.XAxisType;

                    //throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                }

                foreach( IElementWithXYAxes axisElement in _chartArea.Elements.Cast< IElementWithXYAxes >( ) )
                {
                    if( axisElement.XAxis == null && this == _chartArea.XAxises && axis.Id == axisElement.XAxisId && !axisElement.CheckAxesCompatible( new ChartAxisType?( axis.AxisType ), new ChartAxisType?( ) ) )
                    {
                        throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                    }

                    if( axisElement.YAxis == null && this == _chartArea.YAxises && axis.Id == axisElement.YAxisId && !axisElement.CheckAxesCompatible( new ChartAxisType?( ), new ChartAxisType?( axis.AxisType ) ) )
                    {
                        throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                    }
                }

                if( axis.Group.IsEmpty( ) )
                {
                    axis.Group = _getDefaultGroupFunc( axis, num );
                }

                if( axis.Title.IsEmpty( ) )
                {
                    axis.Title = _defaultId + num;
                }

                axis.ChartArea = _chartArea;
                return base.OnAdding( axis );
            }

            protected override bool OnRemoving( ChartAxis axis )
            {
                bool hasAxis = Contains( axis );

                if( hasAxis && _chartArea.Chart != null && axis.IsDefault )
                {
                    throw new InvalidOperationException( LocalizedStrings.ErrorRemovingDefaultAxis );
                }

                if( ( base.OnRemoving( axis ) & hasAxis ) == false )
                {
                    return false;
                }

                axis.ChartArea = null;

                return true;
            }

            protected override bool OnRemovingAt( int index )
            {
                ChartAxis chartAxis = this[ index ];

                if( chartAxis.IsDefault && _chartArea.Chart != null )
                {
                    throw new InvalidOperationException( LocalizedStrings.ErrorRemovingDefaultAxis );
                }

                if( base.OnRemovingAt( index ) )
                {
                    chartAxis.ChartArea = null;

                    return true;
                }

                return false;
            }

            protected override bool OnClearing( )
            {
                if( _chartArea.Chart != null )
                {
                    throw new InvalidOperationException( LocalizedStrings.ErrorRemovingDefaultAxis );
                }

                ChartAxis[ ] axises = this.ToArray( );

                if( base.OnClearing( ) )
                {
                    foreach( ChartAxis axis in axises )
                    {
                        axis.ChartArea = null;
                    }

                    return true;
                }

                return false;
            }
        }

        private class PropertiesNotifyList< T > : BaseList< T >, INotifyPropertyChanged, INotifyCollectionChanged
        {
            private int _index;
            private T _property;

            public event PropertyChangedEventHandler PropertyChangedEvent;

            public event NotifyCollectionChangedEventHandler NotifyCollectionChangedEvent;

            protected override bool OnRemove( T property )
            {
                _index = IndexOf( property );
                return base.OnRemove( property );
            }

            protected override void OnRemoved( T property )
            {
                if( _index >= 0 )
                {
                    RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Remove, property, _index );
                }
                base.OnRemoved( property );
            }

            protected override void OnInserted( int index, T property )
            {
                RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Add, property, index );
                base.OnInserted( index, property );
            }


            /* -------------------------------------------------------------------------------------------------------------------------------------------
             *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
             *  
             *  Step A ----------> 2 Here we are subscribing to the Area Notifying PooledList OnAdded Event
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- 
             */
            protected override void OnAdded( T property )
            {
                RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Add, property, Count - 1 );
                base.OnAdded( property );
            }

            protected override void OnRemoveAt( int index )
            {
                _property = this[ index ];
                base.OnRemoveAt( index );
            }

            protected override void OnRemovedAt( int index )
            {
                if( _property != null )
                {
                    RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Remove, _property, index );
                }
                base.OnRemovedAt( index );
            }

            protected override void OnCleared( )
            {
                RaiseCollectionChangedEvent( );
                base.OnCleared( );
            }

            private void RaiseCollectionChangedEvent( NotifyCollectionChangedAction notifyCollectionChangedAction_0,
                                   T gparam_1,
                                   int index )
            {
                RaisePropertyChangedEvent( "Count" );
                RaisePropertyChangedEvent( "Item[]" );

                NotifyCollectionChangedEvent?.Invoke( this, new NotifyCollectionChangedEventArgs( notifyCollectionChangedAction_0, gparam_1, index ) );
            }

            private void RaiseCollectionChangedEvent( )
            {
                RaisePropertyChangedEvent( "Count" );
                RaisePropertyChangedEvent( "Item[]" );

                NotifyCollectionChangedEvent?.Invoke( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
            }

            private void RaisePropertyChangedEvent( string string_0 )
            {
                PropertyChangedEvent?.Invoke( this, new PropertyChangedEventArgs( string_0 ) );
            }

            event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
            {
                add
                {
                    PropertyChangedEvent += value;
                }

                remove
                {
                    PropertyChangedEvent -= value;
                }
            }

            event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
            {
                add
                {
                    NotifyCollectionChangedEvent += value;
                }
                remove
                {
                    NotifyCollectionChangedEvent -= value;
                }
            }
        }

        private sealed class ChartElementNotifyList : PropertiesNotifyList< IChartElement >
        {
            private readonly ChartArea _area;

            public ChartElementNotifyList( ChartArea area )
            {
                ChartArea chartArea = area;
                if( chartArea == null )
                {
                    throw new ArgumentNullException( "area" );
                }
                _area = chartArea;
            }

            protected override bool OnAdding( IChartElement element )
            {
                if( element.Chart != null )
                {
                    throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
                }

                if( this.Any( i => i.Id == element.Id ) )
                {
                    throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
                }

                var elementXY = element as IElementWithXYAxes;

                Maybe.Do( elementXY, a =>
                {
                    var axis = _area.YAxises.FirstOrDefault( i => i.Id == a.YAxisId );
                    if( !a.CheckAxesCompatible( new ChartAxisType?( _area.XAxisType ), axis?.AxisType ) )
                    {
                        throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.AxesTypesNotSupportedParams, new object[ ]
                        {
                                                       a.GetType( ).Name,
                                                      _area.XAxisType,
                                                      axis?.AxisType
                        } ) );
                    }
                } );

                return base.OnAdding( element );
            }
        }
    }
}
