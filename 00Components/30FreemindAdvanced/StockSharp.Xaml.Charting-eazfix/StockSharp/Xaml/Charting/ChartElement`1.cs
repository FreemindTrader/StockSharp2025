// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartComponent`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// The base class that describes the chart element (indicator, candle, etc.).
/// </summary>
/// <typeparam name="T">The chart element type.</typeparam>
[TypeConverter( typeof( ExpandableObjectConverter ) )]
public abstract class ChartComponent<T> :
  ChartPart<T>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartComponent
  where T : ChartComponent<T>
{

    private IChartComponent _parentElement;

    private readonly SynchronizedDictionary<Guid, string> _idToName = new SynchronizedDictionary<Guid, string>();

    private readonly SynchronizedSet<string> _extraName = new SynchronizedSet<string>();

    private readonly CachedSynchronizedSet<IChartElement> _componentsCache = new CachedSynchronizedSet<IChartElement>();

    private IChartArea _chartArea;

    private StockSharp.Xaml.Charting.ChartArea _persistentChartArea;

    private string _fullTitle;

    private bool _isVisible = true;

    private bool _isLegend = true;

    private string _xAxisId = "X";

    private string _yAxisId = "Y";

    private Func<IComparable, System.Windows.Media.Color?> _mediaColor;

    private Func<IComparable, System.Drawing.Color?> _drawingColor;

    private bool _dontDraw;

    [Browsable( false )]
    public IChartArea ChartArea
    {
        get => this._chartArea;
        private set => this._chartArea = value;
    }

    IChartArea IChartElement.PersistentChartArea => ( IChartArea ) this._persistentChartArea;

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Name", Description = "NameDot", GroupName = "Common", Order = 1000 )]
    [Attribute0( true )]
    public string FullTitle
    {
        get => this._fullTitle;
        set
        {
            if ( this._fullTitle == value )
                return;
            this._fullTitle = value;
            this.RaisePropertyChanged( nameof( FullTitle ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Show", Description = "ShowDot", GroupName = "Common", Order = 1010 )]
    public bool IsVisible
    {
        get => this._isVisible;
        set
        {
            if ( this._isVisible == value )
                return;
            this._isVisible = value;
            this.RaisePropertyChanged( nameof( IsVisible ) );
        }
    }

    [Browsable( false )]
    public bool IsLegend
    {
        get => this._isLegend;
        set
        {
            if ( this._isLegend == value )
                return;
            this._isLegend = value;
            this.RaisePropertyChanged( nameof( IsLegend ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "XAxis", Description = "XAxisDesc", GroupName = "Common", Order = 1020 )]
    [Editor( typeof( XAxisesComboBoxEditSettings ), typeof( XAxisesComboBoxEditSettings ) )]
    [Attribute0( true )]
    public string XAxisId
    {
        get => this.ParentElement?.XAxisId ?? this._xAxisId;
        set
        {
            if ( this._xAxisId == value )
                return;
            this.RaisePropertyValueChanging( nameof( XAxisId ), ( object ) value );
            this._xAxisId = value;
            this.RaisePropertyChanged( nameof( XAxisId ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "YAxis", Description = "YAxisDesc", GroupName = "Common", Order = 1030 )]
    [Editor( typeof( YAxisesComboBoxEditSettings ), typeof( YAxisesComboBoxEditSettings ) )]
    [Attribute0( true )]
    public string YAxisId
    {
        get => this.ParentElement?.YAxisId ?? this._yAxisId;
        set
        {
            if ( this._yAxisId == value )
                return;
            this.RaisePropertyValueChanging( nameof( YAxisId ), ( object ) value );
            this._yAxisId = value;
            this.RaisePropertyChanged( nameof( YAxisId ) );
        }
    }

    [Browsable( false )]
    public Func<IComparable, System.Windows.Media.Color?> Colorer
    {
        get
        {
            return this._parentElement?.Colorer ?? this._mediaColor;
        }
        set => this._mediaColor = value;
    }

    Func<IComparable, System.Drawing.Color?> IChartElement.Colorer
    {
        get => this._drawingColor;
        set
        {
            ChartComponent<T>.SomePrivateSealedClass u8Jor1ugvGeCvjho = new ChartComponent<T>.SomePrivateSealedClass();
            u8Jor1ugvGeCvjho._IComparable = value;
            this._drawingColor = u8Jor1ugvGeCvjho._IComparable;
            if ( this._drawingColor == null )
                this.Colorer = ( Func<IComparable, System.Windows.Media.Color?> ) null;
            else
                this.Colorer = new Func<IComparable, System.Windows.Media.Color?>( u8Jor1ugvGeCvjho.SomeMethod034);
        }
    }

    [Browsable( false )]
    public IChartElement ParentElement => ( IChartElement ) this._parentElement;

    void IChartComponent.SetParent( IChartElement _param1 )
    {
        this._parentElement = this._parentElement == null || _param1 == null ? ( IChartComponent ) _param1 : throw new ArgumentException( LocalizedStrings.ParentElementAlreadySet );
        if ( this._parentElement == null )
            return;
        this._parentElement.PropertyChanged += new PropertyChangedEventHandler( this.OnParentPropertyChanged );
    }

    int IChartComponent.Priority
    {
        get
        {
            return this.Priority;
        }
    }

    /// <summary>
    /// </summary>
    protected virtual int Priority => int.MaxValue;

    public IChartComponent RootElement
    {
        get
        {
            return this._parentElement != null ? this._parentElement.RootElement : ( IChartComponent ) this;
        }
    }

    [Browsable( false )]
    public IEnumerable<IChartElement> ChildElements
    {
        get => ( IEnumerable<IChartElement> ) this._componentsCache.Cache;
    }



    public bool DontDraw
    {
        get
        {
            return this._dontDraw;
        }
        set
        {
            this._dontDraw = value;
        }
    }




    /// <summary>Add child chart element.</summary>
    /// <param name="element">
    /// </param>
    /// <param name="dontDraw">Do not create corresponding chart element. Used for nested elements.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// </exception>
    protected public void AddChildElement( IChartElement element, bool dontDraw = false )
    {
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( !( ( SynchronizedSet<IChartElement> ) this._componentsCache ).TryAdd( element ) )
            throw new InvalidOperationException( "duplicate element" );
        ( ( IChartComponent ) element ).SetParent( ( IChartElement ) this );
        ( ( IChartComponent ) element ).DontDraw = dontDraw;
    }

    /// <summary>Remove child chart element.</summary>
    protected public void RemoveChildElement( IChartElement element )
    {
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( !( ( BaseCollection<IChartElement, ISet<IChartElement>> ) this._componentsCache ).Remove( element ) )
            return;
        ( ( IChartComponent ) element ).SetParent( ( IChartElement ) null );
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

    bool IChartComponent.Draw( ChartDrawData _param1 )
    {
        return this.OnDraw( _param1 );
    }

    void IChartComponent.Reset()
    {
        this.OnReset();
        foreach ( IChartComponent childElement in this.ChildElements )
            childElement.Reset();
    }

    void IChartComponent.ResetUI()
    {
        this.RaisePropertyChanged( "FullTitle" );
    }

    void IChartComponent.AddAxisesAndEventHandler( StockSharp.Xaml.Charting.ChartArea _param1 )
    {
        if ( this.ChartArea != null )
            throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
        this.ChartArea = ( IChartArea ) ( this._persistentChartArea = _param1 );
        ( ( INotifyCollection<IChartAxis> ) _param1.XAxises ).Changed += new Action( this.OnXAxisChanged );
        ( ( INotifyCollection<IChartAxis> ) _param1.YAxises ).Changed += new Action( this.OnYAxisChange );
        this.OnXAxisChanged();
        this.OnYAxisChange();
    }

    void IChartComponent.RemoveAxisesEventHandler()
    {
        if ( this.ChartArea == null )
            return;
        ( ( INotifyCollection<IChartAxis> ) this.ChartArea.XAxises ).Changed -= new Action( this.OnXAxisChanged );
        ( ( INotifyCollection<IChartAxis> ) this.ChartArea.YAxises ).Changed -= new Action( this.OnYAxisChange );
        this.ChartArea = ( IChartArea ) null;
        this.OnXAxisChanged();
        this.OnYAxisChange();
    }



    private void OnXAxisChanged()
    {
        this.RaisePropertyChanged( "XAxisId" );
    }

    private void OnYAxisChange()
    {
        this.RaisePropertyChanged( "YAxisId" );
    }



    string IChartComponent.GetName(
      IChartElement _param1 )
    {
        return CollectionHelper.TryGetValue<Guid, string>( ( IDictionary<Guid, string> ) this._idToName, _param1.Id );
    }

    public void SetName( IChartElement _param1, string _param2 )
    {
        this._idToName[ _param1.Id ] = _param2;
    }

    bool IChartComponent.HasExtraName(
      string _param1 )
    {
        return ( ( BaseCollection<string, ISet<string>> ) this._extraName ).Contains( _param1 );
    }

    public void AddExtraName( string _param1 )
    {
        ( ( BaseCollection<string, ISet<string>> ) this._extraName ).Add( _param1 );
    }

    string IChartComponent.GetGeneratedTitle()
    {
        
            string fullTitle = this.FullTitle;
            if ( !StringHelper.IsEmptyOrWhiteSpace( fullTitle ) )
                return fullTitle;
            string generatedTitle = this.GetGeneratedTitle();
            return !StringHelper.IsEmptyOrWhiteSpace( generatedTitle ) ? generatedTitle : Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) ( ( object ) this ).GetType(), ( string ) null );
        
    }

    /// <summary>Get generated title.</summary>
    /// <returns>Auto generate chart element title.</returns>
    protected virtual string GetGeneratedTitle() => ( string ) null;

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        this.IsVisible = storage.GetValue<bool>( "IsVisible", this.IsVisible );
        this.FullTitle = storage.GetValue<string>( "FullTitle", this.FullTitle );
        this.IsLegend = storage.GetValue<bool>( "IsLegend", this.IsLegend );
        this.XAxisId = storage.GetValue<string>( "XAxisId", this.XAxisId );
        this.YAxisId = storage.GetValue<string>( "YAxisId", this.YAxisId );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<bool>( "IsVisible", this.IsVisible );
        storage.SetValue<string>( "FullTitle", this.FullTitle );
        storage.SetValue<bool>( "IsLegend", this.IsLegend );
        storage.SetValue<string>( "XAxisId", this.XAxisId );
        storage.SetValue<string>( "YAxisId", this.YAxisId );
    }

    public override T Clone( T _param1 )
    {
        base.Clone( _param1 );
        _param1.IsVisible = this.IsVisible;
        _param1.FullTitle = this.FullTitle;
        _param1.IsLegend = this.IsLegend;
        _param1.XAxisId = this.XAxisId;
        _param1.YAxisId = this.YAxisId;
        IChartElement[] cache1 = this._componentsCache.Cache;
        IChartElement[] cache2 = _param1._componentsCache.Cache;
        if ( cache1.Length != cache2.Length )
            throw new InvalidOperationException( "unexpected number of clones children" );
        for ( int index = 0 ; index < cache1.Length ; ++index )
        {
            IChartElement chartElement1 = cache1[index];
            IChartElement chartElement2 = cache2[index];
            if ( chartElement1.GetType() != chartElement2.GetType() )
                throw new InvalidOperationException( "unexpected child type" );
            ( ( IChartComponent ) chartElement1 ).Clone( ( object ) chartElement2 );
        }
        _param1.DontDraw = ( ( IChartComponent ) this ).DontDraw;
        return _param1;
    }

    protected virtual T CreateClone() => ( T ) Activator.CreateInstance( ( ( object ) this ).GetType() );

    public override T Clone()
    {
        ChartComponent<T>.SomePrivateSealedClass034 uw9yL4K8Elv3B8f3hPm = new ChartComponent<T>.SomePrivateSealedClass034();
        uw9yL4K8Elv3B8f3hPm._SomeMethod034098 = this.CreateClone();
        uw9yL4K8Elv3B8f3hPm._SomeMethod034098._extraName.AddRange( ( IEnumerable<string> ) this._extraName );
        CollectionHelper.ForEach<KeyValuePair<Guid, string>>( ( IEnumerable<KeyValuePair<Guid, string>> ) this._idToName, new Action<KeyValuePair<Guid, string>>( uw9yL4K8Elv3B8f3hPm.SomeShitNow) );
        this.Clone( uw9yL4K8Elv3B8f3hPm._SomeMethod034098);
        return uw9yL4K8Elv3B8f3hPm._SomeMethod034098;
    }

    void IChartComponent.Clone(
        object _param1 )
    {
        this.Clone( ( T ) _param1 );
    }

    public virtual bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
    {
        if ( xType.HasValue && xType.GetValueOrDefault() != ChartAxisType.CategoryDateTime )
            return false;
        return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    private void OnParentPropertyChanged(
#nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2 )
    {
        this.RaisePropertyChanged( _param2.PropertyName );
    }

    private sealed class SomePrivateSealedClass
    {
        public
#nullable disable
        Func<IComparable, System.Drawing.Color?> _IComparable;

    public System.Windows.Media.Color? SomeMethod034(
      IComparable _param1)
    {
      System.Drawing.Color? nullable = this._IComparable(_param1);
      ref System.Drawing.Color? local = ref nullable;
      return !local.HasValue? new System.Windows.Media.Color? () : new System.Windows.Media.Color? (local.GetValueOrDefault().ToWpf());
    }
}

private sealed class SomePrivateSealedClass034
{
    public T _SomeMethod034098;

    public void SomeShitNow(KeyValuePair<Guid, string> _param1)
    {
      this._SomeMethod034098._idToName[ _param1.Key ] = _param1.Value;
}
  }
}
