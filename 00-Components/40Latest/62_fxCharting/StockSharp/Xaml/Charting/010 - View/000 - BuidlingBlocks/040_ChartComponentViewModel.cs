using DevExpress.ClipboardSource.SpreadsheetML;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using MathNet.Numerics;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using System.Windows.Forms;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// The base class that makes up the UI for any ChartComponents (indicator, candle, etc.).
/// 
/// The ViewModel is responsible for the following:
/// Encapsulating UI state: It holds the data that the view needs to display and persists it through configuration changes, like screen rotations.
/// Preparing data for display: It fetches data from a repository or use case and then transforms it into a format that the view can easily render.
/// This involves things like filtering, sorting, or combining data from multiple sources.
/// Handling user events: It responds to user actions from the view, such as a button click, and decides what business logic to call in the layers below.
/// </summary>
/// <typeparam name="T">The chart element type.</typeparam>
[TypeConverter( typeof( ExpandableObjectConverter ) )]
public abstract class ChartComponentViewModel<T> :   ChartPart<T>,
IChartElement,
IChartPart<IChartElement>,
                                                INotifyPropertyChanged,
                                                INotifyPropertyChanging,
                                                IPersistable,
                                                IChartComponent
                                                where T : ChartComponentViewModel<T>
{
   
    private readonly SynchronizedDictionary<Guid, string>   _idToName = new SynchronizedDictionary<Guid, string>();

    private readonly SynchronizedSet<string>                _extraName = new SynchronizedSet<string>();

    private readonly CachedSynchronizedSet<IChartElement>   _componentsCache = new CachedSynchronizedSet<IChartElement>();

    private Func<IComparable, System.Windows.Media.Color?>? _mediaColor;

    private Func<IComparable, System.Drawing.Color?>?       _drawingColor;

    private IChartComponent? _parentElement;

    private IChartArea?      _chartArea;

    private ChartArea?       _persistentChartArea;

    private string?          _fullTitle;

    private bool             _isVisible = true;

    private bool             _isLegend  = true;

    private string           _xAxisId   = "X";

    private string           _yAxisId   = "Y";

    

    private bool _dontDraw;

    
    public IChartArea? ChartArea
    {
        get => _chartArea;
        private set => _chartArea = value;
    }

    public IChartArea? PersistentChartArea => ( IChartArea? ) _persistentChartArea;

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Name", Description = "NameDot", GroupName = "Common", Order = 1000 )]
    [Attribute0( true )]
    public string? FullTitle
    {
        get => _fullTitle;
        set
        {
            if ( _fullTitle == value )
                return;
            _fullTitle = value;
            RaisePropertyChanged( nameof( FullTitle ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Show", Description = "ShowDot", GroupName = "Common", Order = 1010 )]
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if ( _isVisible == value )
                return;
            _isVisible = value;
            RaisePropertyChanged( nameof( IsVisible ) );
        }
    }

    
    public bool IsLegend
    {
        get => _isLegend;
        set
        {
            if ( _isLegend == value )
                return;
            _isLegend = value;
            RaisePropertyChanged( nameof( IsLegend ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "XAxis", Description = "XAxisDesc", GroupName = "Common", Order = 1020 )]
    [Editor( typeof( XAxisesComboBoxEditSettings ), typeof( XAxisesComboBoxEditSettings ) )]
    [Attribute0( true )]
    public string XAxisId
    {
        get => ParentElement?.XAxisId ?? _xAxisId;
        set
        {
            if ( _xAxisId == value )
                return;

            RaisePropertyValueChanging( nameof( XAxisId ),  value );
            _xAxisId = value;
            RaisePropertyChanged( nameof( XAxisId ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "YAxis", Description = "YAxisDesc", GroupName = "Common", Order = 1030 )]
    [Editor( typeof( YAxisesComboBoxEditSettings ), typeof( YAxisesComboBoxEditSettings ) )]
    [Attribute0( true )]
    public string YAxisId
    {
        get => ParentElement?.YAxisId ?? _yAxisId;
        set
        {
            if ( _yAxisId == value )
                return;
            RaisePropertyValueChanging( nameof( YAxisId ),  value );
            _yAxisId = value;
            RaisePropertyChanged( nameof( YAxisId ) );
        }
    }

    
    public Func<IComparable, System.Windows.Media.Color?>? WinColorer
    {
        get
        {
            return _parentElement?.WinColorer ?? _mediaColor;
        }
        set => _mediaColor = value;
    }

    public Func<IComparable, System.Drawing.Color?>? Colorer
    {
        get => _drawingColor;
        set
        {            
            _drawingColor = value;

            if ( _drawingColor == null )
            {
                WinColorer = null;
            }
            else
            {
                WinColorer = new Func<IComparable, System.Windows.Media.Color?>( p =>
                {
                    return !value( p ).HasValue ? new System.Windows.Media.Color?() : new System.Windows.Media.Color?( value( p ).GetValueOrDefault().ToWpf() );

                } );
            }            
        }
    }

    
    public IChartElement? ParentElement => ( IChartElement? ) _parentElement;

    void IChartComponent.SetParent( IChartElement newParent )
    {
        _parentElement = _parentElement == null || newParent == null ? ( IChartComponent? ) newParent : throw new ArgumentException( LocalizedStrings.ParentElementAlreadySet );
        
        if ( _parentElement == null )
            return;
        
        _parentElement.PropertyChanged += new PropertyChangedEventHandler( OnParentPropertyChanged );
    }


    /// <summary>
    /// The drawing priority of the chart element. The smaller the value, the higher the priority.
    /// 
    ///     - CandleElement has priority 0
    ///     - CHartIndicatorElement has priority 1
    ///     - ChartBandElement has priority int.MaxValue
    /// </summary>
    int IChartComponent.Priority
    {
        get
        {
            return Priority;
        }
    }

    /// <summary>
    /// </summary>
    protected virtual int Priority => int.MaxValue;

    public IChartComponent RootElement
    {
        get
        {
            return _parentElement != null ? _parentElement.RootElement : ( IChartComponent ) this;
        }
    }

    
    public IEnumerable<IChartElement> ChildElements
    {
        get => ( IEnumerable<IChartElement> ) _componentsCache.Cache;
    }



    public bool DontDraw
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




    /// <summary>Add child chart element.</summary>
    /// <param name="element">
    /// </param>
    /// <param name="dontDraw">Do not create corresponding chart element. Used for nested elements.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// </exception>
    protected internal void AddChildElement( IChartElement element, bool dontDraw = false )
    {
        if ( element == null )
        {
            throw new ArgumentNullException( nameof( element ) );
        }
            
        if ( ! _componentsCache.TryAdd( element ) )
        {
            throw new InvalidOperationException( "duplicate element" );
        }
            
        ( ( IChartComponent ) element ).SetParent(this );
        ( ( IChartComponent ) element ).DontDraw = dontDraw;
    }

    /// <summary>Remove child chart element.</summary>
    protected internal void RemoveChildElement( IChartElement element )
    {
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( !( ( BaseCollection<IChartElement, ISet<IChartElement>> ) _componentsCache ).Remove( element ) )
            return;
        ( ( IChartComponent ) element ).SetParent( null );
    }

    /// <summary>Reset element.</summary>
    protected virtual void OnReset()
    {
    }

    /// <summary>Draw on root element.</summary>
    /// <param name="data">Chart drawing data.</param>
    /// <returns>
    /// <see langword="true" /> if the data was successfully drawn, otherwise, returns <see langword="false" />.</returns>
    protected abstract bool OnDraw( ChartDrawData data );

    bool IChartComponent.Draw( ChartDrawData data )
    {
        return OnDraw( data );
    }

    void IChartComponent.Reset()
    {
        OnReset();
        foreach ( IChartComponent childElement in ChildElements )
            childElement.Reset();
    }

    void IChartComponent.ResetUI()
    {
        RaisePropertyChanged( "FullTitle" );
    }

    void IChartComponent.AddAxisesAndEventHandler( ChartArea area )
    {
        if ( ChartArea != null )
            throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
        
        ChartArea = _persistentChartArea = area;

        ChartArea.XAxises.Changed += OnXAxisChanged;
        ChartArea.YAxises.Changed += OnYAxisChange;

        OnXAxisChanged();
        OnYAxisChange();
    }

    void IChartComponent.RemoveAxisesEventHandler()
    {
        if ( ChartArea == null )
            return;

        ChartArea.XAxises.Changed -= OnXAxisChanged;
        ChartArea.YAxises.Changed -= OnYAxisChange;
        
        ChartArea = null;

        OnXAxisChanged();
        OnYAxisChange();
    }



    private void OnXAxisChanged()
    {
        RaisePropertyChanged( "XAxisId" );
    }

    private void OnYAxisChange()
    {
        RaisePropertyChanged( "YAxisId" );
    }



    string IChartComponent.GetName(
      IChartElement _param1 )
    {
        return CollectionHelper.TryGetValue<Guid, string>( ( IDictionary<Guid, string> ) _idToName, _param1.Id );
    }

    internal void AddName( IChartElement _param1, string _param2 )
    {
        _idToName[ _param1.Id ] = _param2;
    }

    bool IChartComponent.HasExtraName(
      string _param1 )
    {
        return ( ( BaseCollection<string, ISet<string>> ) _extraName ).Contains( _param1 );
    }

    internal void AddExtraName( string _param1 )
    {
        ( ( BaseCollection<string, ISet<string>> ) _extraName ).Add( _param1 );
    }

    string? IChartComponent.GetGeneratedTitle()
    {

        string? fullTitle = FullTitle;
        if ( !StringHelper.IsEmptyOrWhiteSpace( FullTitle ) )
            return FullTitle;
        string? generatedTitle = GetGeneratedTitle();
        return !StringHelper.IsEmptyOrWhiteSpace( generatedTitle ) ? generatedTitle : Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) (  this ).GetType(), ( string ) null );

    }

    /// <summary>Get generated title.</summary>
    /// <returns>Auto generate chart element title.</returns>
    protected virtual string? GetGeneratedTitle() => null;

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );

        IsVisible = storage.GetValue<bool>( "IsVisible", IsVisible );
        FullTitle = storage.GetValue<string>( "FullTitle", FullTitle );
        IsLegend  = storage.GetValue<bool>( "IsLegend", IsLegend );
        XAxisId   = storage.GetValue<string>( "XAxisId", XAxisId );
        YAxisId   = storage.GetValue<string>( "YAxisId", YAxisId );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<bool>( "IsVisible", IsVisible );
        storage.SetValue<string>( "FullTitle", FullTitle );
        storage.SetValue<bool>( "IsLegend", IsLegend );
        storage.SetValue<string>( "XAxisId", XAxisId );
        storage.SetValue<string>( "YAxisId", YAxisId );
    }

    internal override T Clone( T to )
    {
        base.Clone( to );

        to.IsVisible  = IsVisible;
        to.FullTitle  = FullTitle;
        to.IsLegend   = IsLegend;
        to.XAxisId    = XAxisId;
        to.YAxisId    = YAxisId;
        var fromCache = _componentsCache.Cache;
        var toCache   = to._componentsCache.Cache;
        
        if ( fromCache.Length != toCache.Length )
            throw new InvalidOperationException( "unexpected number of clones children" );

        for ( int index = 0 ; index < fromCache.Length ; ++index )
        {
            var fromElement = fromCache[index];
            var toElement = toCache[index];
            
            if ( fromElement.GetType() != toElement.GetType() )
            {
                throw new InvalidOperationException( "unexpected child type" );
            }
                
            ( ( IChartComponent ) fromElement ).Clone(  toElement );
        }
        to.DontDraw = ( ( IChartComponent ) this ).DontDraw;
        return to;
    }

    protected virtual T CreateClone() => ( T ) Activator.CreateInstance( ( this ).GetType() );

    public override T Clone()
    {
        T myClone = CreateClone();
        myClone._extraName.AddRange( ( IEnumerable<string> ) _extraName );

        CollectionHelper.ForEach( _idToName, p => myClone._idToName[p.Key] = p.Value );
        Clone( myClone );

        return myClone;
    }
    
    void IChartComponent.Clone( object _param1 )
    {
        Clone( ( T ) _param1 );
    }

    public virtual bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
    {
        if ( xType.HasValue && xType.GetValueOrDefault() != ChartAxisType.CategoryDateTime )
            return false;
        return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    private void OnParentPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
    {
        RaisePropertyChanged( _param2.PropertyName );
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
}
