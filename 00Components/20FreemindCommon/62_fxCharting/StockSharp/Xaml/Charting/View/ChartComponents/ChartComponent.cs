using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;

namespace fx.Charting
{
    [TypeConverter( typeof( ExpandableObjectConverter ) )]
    public abstract class ChartComponent<T> : ChartPart<T>, ICloneable<IfxChartElement>, INotifyPropertyChanged, IElementWithXYAxes, ICloneable, INotifyPropertyChanging, IfxChartElement where T : ChartComponent<T>
    {
        private readonly SynchronizedDictionary<Guid, string>  _idToName = new SynchronizedDictionary<Guid, string>( );
        private readonly CachedSynchronizedList<IfxChartElement> _childElements = new CachedSynchronizedList<IfxChartElement>( );
        private readonly SynchronizedSet<string>               _extraName = new SynchronizedSet<string>( );

        private bool                                           _isVisible = true;
        private bool                                           _isLegend = true;
        private string                                         _xAxisId = "X";
        private string                                         _yAxisId = "Y";
        private IElementWithXYAxes                             _parentElement;
        private ChartArea                                      _chartArea;
        private ChartArea                                      _persistantChartArea;
        private string                                         _fullTitle;
        private Func<IComparable, Color?>                      _colorer;
        private bool                                           _dontDraw;

        [Browsable( false )]
        public IChart Chart
        {
            get
            {
                return ChartArea?.Chart;
            }
        }

        [Browsable( false )]
        public ChartArea ChartArea
        {
            get
            {
                return _chartArea;
            }
            private set
            {
                _chartArea = value;
            }
        }

        ChartArea IfxChartElement.PersistantChartArea
        {
            get
            {
                return _persistantChartArea;
            }
        }

        

        private int _fifoCapacity;

        [Display( Description = "Fifocapacity", GroupName = "StyleString", Name = "Fifocapacity", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public int FifoCapacity
        {
            get
            {
                return _fifoCapacity;
            }
            set
            {
                _fifoCapacity = value;
                RaisePropertyChanged( nameof( FifoCapacity ) );
            }
        }

        [Attribute0( true )]
        [Display( Description = "NameDot", GroupName = "Common", Name = "Name", Order = 1000, ResourceType = typeof( LocalizedStrings ) )]
        public string FullTitle
        {
            get
            {
                return _fullTitle;
            }
            set
            {
                SetField( ref _fullTitle, value, nameof( FullTitle ) );
            }
        }

        [Display( Description = "Str2933Dot", GroupName = "Common", Name = "Str2933", Order = 1010, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                RaisePropertyChanged( nameof( IsVisible ) );
            }
        }

        [Browsable( false )]
        public bool IsLegend
        {
            get
            {
                return _isLegend;
            }
            set
            {
                if ( _isLegend == value )
                {
                    return;
                }

                _isLegend = value;
                RaisePropertyChanged( nameof( IsLegend ) );
            }
        }

        [Attribute0( true )]
        [Display( Description = "Str1967", GroupName = "Common", Name = "Str1966", Order = 1020, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( XAxisesComboBoxEditSettings ), typeof( XAxisesComboBoxEditSettings ) )]
        public string XAxisId
        {
            get
            {
                IfxChartElement parentElement = ParentElement;
                string id;

                if ( parentElement == null )
                {
                    id = null;
                }
                else
                {
                    id = parentElement.XAxisId;

                    if ( id != null )
                    {
                        return id;
                    }
                }
                return _xAxisId;
            }
            set
            {
                if ( _xAxisId == value )
                {
                    return;
                }

                RaisePropertyValueChanging( nameof( XAxisId ), value );
                _xAxisId = value;
                RaisePropertyChanged( nameof( XAxisId ) );
                RaisePropertyChanged( "XAxis" );
            }
        }

        [Display( Description = "Str1969", GroupName = "Common", Name = "Str1968", Order = 1030, ResourceType = typeof( LocalizedStrings ) )]
        [Attribute0( true )]
        [Editor( typeof( YAxisesComboBoxEditSettings ), typeof( YAxisesComboBoxEditSettings ) )]
        public string YAxisId
        {
            get
            {
                IfxChartElement parentElement = ParentElement;
                string id;

                if ( parentElement == null )
                {
                    id = null;
                }
                else
                {
                    id = parentElement.YAxisId;
                    if ( id != null )
                    {
                        return id;
                    }
                }
                return _yAxisId;
            }
            set
            {
                if ( _yAxisId == value )
                {
                    return;
                }

                RaisePropertyValueChanging( nameof( YAxisId ), value );
                _yAxisId = value;
                RaisePropertyChanged( nameof( YAxisId ) );
                RaisePropertyChanged( "YAxis" );
            }
        }

        [Browsable( false )]
        public Func<IComparable, Color?> Colorer
        {
            get
            {
                Func<IComparable, Color?> colorerFunc;

                if ( _parentElement == null )
                {
                    colorerFunc = null;
                }
                else
                {
                    colorerFunc = _parentElement.Colorer;

                    if ( colorerFunc != null )
                    {
                        return colorerFunc;
                    }
                }

                return _colorer;
            }
            set
            {
                _colorer = value;
            }
        }

        [Browsable( false )]
        public ChartAxis XAxis
        {
            get
            {
                ChartArea chartArea = ChartArea;

                if ( chartArea == null )
                {
                    return null;
                }

                return ChartArea.XAxises.FirstOrDefault( a => a.Id == XAxisId );
            }
        }

        [Browsable( false )]
        public ChartAxis YAxis
        {
            get
            {
                ChartArea chartArea = ChartArea;

                if ( chartArea == null )
                {
                    return null;
                }

                return ChartArea.XAxises.FirstOrDefault( a => a.Id == YAxisId );
            }
        }

        [Browsable( false )]
        public IfxChartElement ParentElement
        {
            get
            {
                return _parentElement;
            }
            set
            {
                if ( _parentElement != null && value != null )
                {
                    throw new ArgumentException( LocalizedStrings.ParentElementAlreadySet );
                }

                _parentElement = ( IElementWithXYAxes )value;

                if ( _parentElement == null )
                {
                    return;
                }

                _parentElement.PropertyChanged += ( s, e ) => RaisePropertyChanged( e.PropertyName );
            }
        }

        IElementWithXYAxes IElementWithXYAxes.ElementWithXYAxes
        {
            get
            {
                if ( _parentElement != null )
                {
                    return _parentElement.ElementWithXYAxes;
                }

                return this;
            }
        }

        [Browsable( false )]
        public IEnumerable<IfxChartElement> ChildElements
        {
            get
            {
                return _childElements.Cache;
            }
        }

        protected internal void AddChildElement( IfxChartElement childElement, bool dontDraw = false )
        {
            if ( _childElements.Contains( childElement ) )
            {
                throw new InvalidOperationException( "duplicate element" );
            } 
            
            ( ( IElementWithXYAxes )childElement ).ParentElement = ( this );
            ( ( IElementWithXYAxes )childElement ).DontDraw = dontDraw;
            _childElements.Add( childElement );
        }

        protected internal void RemoveChildElement( IfxChartElement childElement )
        {
            if ( !_childElements.Remove( childElement ) )
            {
                return;
            } 
            
            ( ( IElementWithXYAxes )childElement ).ParentElement = null;
        }

        protected virtual void OnReset( )
        {
        }

        protected abstract bool OnDraw( ChartDrawDataEx data );

        bool IElementWithXYAxes.Draw( ChartDrawDataEx chartDrawData_0 )
        {
            return OnDraw( chartDrawData_0 );
        }

        void IElementWithXYAxes.Reset( )
        {
            OnReset( );
            foreach ( IElementWithXYAxes childElement in ChildElements )
            {
                childElement.Reset( );
            }
        }

        void IElementWithXYAxes.AddAxisesAndEventHandler( ChartArea area )
        {
            if ( ChartArea != null )
            {
                throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
            }

            ChartArea = _persistantChartArea = area;

            area.XAxises.Changed += new Action( OnXAxisChanged );
            area.YAxises.Changed += new Action( OnYAxisChanged );

            OnXAxisChanged( );
            OnYAxisChanged( );
        }

        void IElementWithXYAxes.RemoveAxisesEventHandler( )
        {
            if ( ChartArea != null )
            {
                ChartArea.XAxises.Changed -= new Action( OnXAxisChanged );
                ChartArea.YAxises.Changed -= new Action( OnYAxisChanged );

                ChartArea = null;

                OnXAxisChanged( );
                OnYAxisChanged( );
            }            
        }

        private void OnXAxisChanged( )
        {
            RaisePropertyChanged( "XAxis" );
        }

        private void OnYAxisChanged( )
        {
            RaisePropertyChanged( "YAxis" );
        }

        bool IElementWithXYAxes.DontDraw
        {
            get
            {
                return _dontDraw;
            }
            set
            {
                _dontDraw = value;
            }
        }

        string IElementWithXYAxes.GetName( IfxChartElement element )
        {
            return _idToName.TryGetValue( element.Id );
        }

        internal void AddName( IfxChartElement element, string string_3 )
        {
            _idToName[ element.Id ] = string_3;
        }

        bool IElementWithXYAxes.AdditionalName( string string_3 )
        {
            return _extraName.Contains( string_3 );
        }

        internal void AdditionalName( string string_3 )
        {
            _extraName.Add( string_3 );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            IsVisible = storage.GetValue( "IsVisible", IsVisible );
            FullTitle = storage.GetValue( "FullTitle", FullTitle );
            IsLegend = storage.GetValue( "IsLegend", IsLegend );
            XAxisId = storage.GetValue( "XAxisId", XAxisId );
            YAxisId = storage.GetValue( "YAxisId", YAxisId );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "IsVisible", IsVisible );
            storage.SetValue( "FullTitle", FullTitle );
            storage.SetValue( "IsLegend", IsLegend );
            storage.SetValue( "XAxisId", XAxisId );
            storage.SetValue( "YAxisId", YAxisId );
        }

        internal override T Clone( T other )
        {
            base.Clone( other );
            other.IsVisible = IsVisible;
            other.FullTitle = FullTitle;
            other.IsLegend = IsLegend;
            other.XAxisId = XAxisId;
            other.YAxisId = YAxisId;
            IfxChartElement[ ] cache1 = _childElements.Cache;
            IfxChartElement[ ] cache2 = other._childElements.Cache;
            if ( cache1.Length != cache2.Length )
            {
                throw new InvalidOperationException( "unexpected number of clones children" );
            }

            for ( int index = 0; index < cache1.Length; ++index )
            {
                IfxChartElement chartElement1 = cache1[ index ];
                IfxChartElement chartElement2 = cache2[ index ];
                if ( chartElement1.GetType( ) != chartElement2.GetType( ) )
                {
                    throw new InvalidOperationException( "unexpected child type" );
                } ( ( IElementWithXYAxes )chartElement1 ).Clone( chartElement2 );
            }
            ( ( IDrawableChartElement )other ).DontDraw = ( ( IDrawableChartElement )this ).DontDraw;
            return other;
        }

        IfxChartElement ICloneable<IfxChartElement>.Clone( )
        {
            return Clone( );
        }

        protected virtual T CreateClone( )
        {
            return ( T )Activator.CreateInstance( GetType( ) );
        }

        public override sealed T Clone( )
        {
            var myClone = CreateClone( );
            myClone._extraName.AddRange( _extraName );

            Ecng.Collections.CollectionHelper.ForEach( _idToName, p => myClone._idToName.Add( p.Key, p.Value ) );
            

            Clone( myClone );
            return myClone;
        }

        void IElementWithXYAxes.Clone( object other )
        {
            Clone( ( T )other );
        }

        public virtual bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
        {
            if ( xType.HasValue )
            {
                if ( xType.Value != ChartAxisType.CategoryDateTime )
                {
                    return false;
                }
            }

            if ( !yType.HasValue )
            {
                return true;
            }

            return yType.GetValueOrDefault( ) == ChartAxisType.Numeric & yType.HasValue;
        }

        public virtual void ResetUI( )
        {
            //throw new NotImplementedException( );
        }

        public string GetGeneratedTitle( )
        {
            throw new NotImplementedException( );
        }
    }
}
