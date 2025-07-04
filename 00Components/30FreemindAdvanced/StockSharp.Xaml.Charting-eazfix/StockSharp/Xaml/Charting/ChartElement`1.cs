// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartElement`1
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

#nullable enable
namespace StockSharp.Xaml.Charting;

[TypeConverter( typeof( ExpandableObjectConverter ) )]
public abstract class ChartElement<T> :
  ChartPart<T>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartComponent
  where T : ChartElement<T>
{

    private IChartComponent _parentElement;

    private readonly SynchronizedDictionary<Guid, string> _idToName = new SynchronizedDictionary<Guid, string>();

    private readonly SynchronizedSet<string> _extraName = new SynchronizedSet<string>();

    private readonly CachedSynchronizedSet<IChartElement> _childElements = new CachedSynchronizedSet<IChartElement>();

    private IChartArea _chartArea;

    private StockSharp.Xaml.Charting.ChartArea \u0023\u003DzpvwmDngT0_MF;
  
  private string _fullTitle;

    private bool _isVisible = true;

    private bool _isLegend = true;

    private string \u0023\u003DztTcR7ybUS145 = "X";
  
  private string \u0023\u003DzXAlmdTpL5g8f = "Y";
  
  private Func<IComparable, System.Windows.Media.Color?> _colorer;

    private Func<IComparable, System.Drawing.Color?> \u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D;
  
  private bool \u0023\u003DznKRyu26uXo5aoizqkSGB4LJBZD07y13sWMME\u0024ccMbmGwY9k6Vq4BKaE\u003D;

  [Browsable( false )]
    public IChartArea ChartArea
    {
        get => this._chartArea;
        private set => this._chartArea = value;
    }

    IChartArea IChartElement.PersistentChartArea => ( IChartArea ) this.\u0023\u003DzpvwmDngT0_MF;

  [Display( ResourceType = typeof( LocalizedStrings ), Name = "Name", Description = "NameDot", GroupName = "Common", Order = 1000 )]
    [\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D(true)]
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
    [Editor( typeof(\u0023\u003DzoRkHUCjeHss9J7PNBJOkcBvSLaUesrc_cQ\u003D\u003D), typeof (\u0023\u003DzoRkHUCjeHss9J7PNBJOkcBvSLaUesrc_cQ\u003D\u003D))]
  [\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D(true)]
  public string XAxisId
    {
        get => this.ParentElement?.XAxisId ?? this.\u0023\u003DztTcR7ybUS145;
        set
        {
            if ( this.\u0023\u003DztTcR7ybUS145 == value)
        return;
            this.RaisePropertyValueChanging( nameof( XAxisId ), ( object ) value );
            this.\u0023\u003DztTcR7ybUS145 = value;
            this.RaisePropertyChanged( nameof( XAxisId ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "YAxis", Description = "YAxisDesc", GroupName = "Common", Order = 1030 )]
    [Editor( typeof(\u0023\u003DzAJ2g5KE5bawCuhjG0TamYsPgfG5ccyJVug\u003D\u003D), typeof (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYsPgfG5ccyJVug\u003D\u003D))]
  [\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_94__ybky5PHw\u003D\u003D(true)]
  public string YAxisId
    {
        get => this.ParentElement?.YAxisId ?? this.\u0023\u003DzXAlmdTpL5g8f;
        set
        {
            if ( this.\u0023\u003DzXAlmdTpL5g8f == value)
        return;
            this.RaisePropertyValueChanging( nameof( YAxisId ), ( object ) value );
            this.\u0023\u003DzXAlmdTpL5g8f = value;
            this.RaisePropertyChanged( nameof( YAxisId ) );
        }
    }

    [Browsable( false )]
    public Func<IComparable, System.Windows.Media.Color?> Colorer
    {
        get
        {
            return this._parentElement?.Colorer ?? this._colorer;
        }
        set => this._colorer = value;
    }

    Func<IComparable, System.Drawing.Color?> IChartElement.Colorer
    {
        get => this.\u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D;
        set
        {
            ChartElement<T>.\u0023\u003Dz85OtBH9U8Jor1ugvGeCVjho\u003D u8Jor1ugvGeCvjho = new ChartElement<T>.\u0023\u003Dz85OtBH9U8Jor1ugvGeCVjho\u003D();
            u8Jor1ugvGeCvjho.\u0023\u003DzxGz2_8k\u003D = value;
            this.\u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D = u8Jor1ugvGeCvjho.\u0023\u003DzxGz2_8k\u003D;
            if ( this.\u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D == null)
        this.Colorer = ( Func<IComparable, System.Windows.Media.Color?> ) null;
      else
                this.Colorer = new Func<IComparable, System.Windows.Media.Color?>( u8Jor1ugvGeCvjho.\u0023\u003DzNip3gTCc6g3u5i_hYaD3SNqkOIf_XRRRGcMFPq3oEBffKcEyNg\u003D\u003D);
        }
    }

    [Browsable( false )]
    public IChartElement ParentElement => ( IChartElement ) this._parentElement;

    void IChartComponent.\u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqcXFVJpr_GKP9Yp3Hww\u003D(
      IChartElement _param1)
    {
        this._parentElement = this._parentElement == null || _param1 == null ? ( IChartComponent ) _param1 : throw new ArgumentException( LocalizedStrings.ParentElementAlreadySet );
        if ( this._parentElement == null )
            return;
        this._parentElement.PropertyChanged += new PropertyChangedEventHandler( this.\u0023\u003DzSJdYbCcHBpzmYpsDytEtTkY0OpqoIhQR2IXqEAMCtmjWsFRg\u0024g\u003D\u003D);
    }

    int IChartComponent.\u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5Xjl\u0024VTn26BAxExunz_I758h()
    {
        return this.Priority;
    }

    protected virtual int Priority => int.MaxValue;

    IChartComponent IChartComponent.\u0023\u003DzK74oGPE3yyB7zop8uDdzn\u0024EtAlFfparLI9VkIbQLPbt\u0024()
    {
        return this._parentElement != null ? this._parentElement.RootElement : ( IChartComponent ) this;
    }

    [Browsable( false )]
    public IEnumerable<IChartElement> ChildElements
    {
        get => ( IEnumerable<IChartElement> ) this._childElements.Cache;
    }

    protected internal void AddChildElement( IChartElement element, bool dontDraw = false )
    {
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( !( ( SynchronizedSet<IChartElement> ) this._childElements ).TryAdd( element ) )
            throw new InvalidOperationException( "duplicate element" );
        ( ( IChartComponent ) element ).SetParent( ( IChartElement ) this );
        ( ( IChartComponent ) element ).DontDraw = dontDraw;
    }

    protected internal void RemoveChildElement( IChartElement element )
    {
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( !( ( BaseCollection<IChartElement, ISet<IChartElement>> ) this._childElements ).Remove( element ) )
            return;
        ( ( IChartComponent ) element ).SetParent( ( IChartElement ) null );
    }

    protected virtual void OnReset()
    {
    }

    protected abstract bool OnDraw( ChartDrawData data );

    bool IChartComponent.\u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZEW6yH9C3QEvQeoTm_c\u003D(
      ChartDrawData _param1)
    {
        return this.OnDraw( _param1 );
    }

    void IChartComponent.\u0023\u003DzQN2Zes8h9tElvYmX48o49KTKAbz_6vhM\u0024G0TdN0\u003D()
    {
        this.OnReset();
        foreach ( IChartComponent childElement in this.ChildElements )
            childElement.Reset();
    }

    void IChartComponent.\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSswqcr11vVhHbZb1DIg\u003D()
    {
        this.RaisePropertyChanged( "FullTitle" );
    }

    void IChartComponent.AddAxisesAndEventHandler(
      StockSharp.Xaml.Charting.ChartArea _param1 )
    {
        if ( this.ChartArea != null )
            throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
        this.ChartArea = ( IChartArea ) ( this.\u0023\u003DzpvwmDngT0_MF = _param1);
        ( ( INotifyCollection<IChartAxis> ) _param1.XAxises ).Changed += new Action( this.\u0023\u003Dz2oJVQ0QhyrqDOIUd1g\u003D\u003D);
        ( ( INotifyCollection<IChartAxis> ) _param1.YAxises ).Changed += new Action( this.\u0023\u003Dz2DrmhCu9hZz815QBZA\u003D\u003D);
        this.\u0023\u003Dz2oJVQ0QhyrqDOIUd1g\u003D\u003D();
        this.\u0023\u003Dz2DrmhCu9hZz815QBZA\u003D\u003D();
    }

    void IChartComponent.\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAZx\u00241KWiV4nA3yEFrP2Kk8Af()
    {
        if ( this.ChartArea == null )
            return;
        ( ( INotifyCollection<IChartAxis> ) this.ChartArea.XAxises ).Changed -= new Action( this.\u0023\u003Dz2oJVQ0QhyrqDOIUd1g\u003D\u003D);
        ( ( INotifyCollection<IChartAxis> ) this.ChartArea.YAxises ).Changed -= new Action( this.\u0023\u003Dz2DrmhCu9hZz815QBZA\u003D\u003D);
        this.ChartArea = ( IChartArea ) null;
        this.\u0023\u003Dz2oJVQ0QhyrqDOIUd1g\u003D\u003D();
        this.\u0023\u003Dz2DrmhCu9hZz815QBZA\u003D\u003D();
    }

    private void \u0023\u003Dz2oJVQ0QhyrqDOIUd1g\u003D\u003D()
  {
    this.RaisePropertyChanged("XAxisId");
}

private void \u0023\u003Dz2DrmhCu9hZz815QBZA\u003D\u003D()
  {
    this.RaisePropertyChanged("YAxisId");
}

bool IChartComponent.\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKlZ3wGlucPKFCzuSkzT6xviX()
  {
    return this.\u0023\u003DznKRyu26uXo5aoizqkSGB4LJBZD07y13sWMME\u0024ccMbmGwY9k6Vq4BKaE\u003D;
}

void IChartComponent.\u0023\u003Dz4simfJ\u0024MaSW7GKJ8rfRfj9fAdjHgUulUDzuVFYE4LCBP(
  bool _param1)
  {
    this.\u0023\u003DznKRyu26uXo5aoizqkSGB4LJBZD07y13sWMME\u0024ccMbmGwY9k6Vq4BKaE\u003D = _param1;
}

string IChartComponent.\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpSSjP2pjB\u0024sTisy0H4JVrbVL(
  IChartElement _param1)
  {
    return CollectionHelper.TryGetValue<Guid, string>( ( IDictionary<Guid, string> ) this._idToName, _param1.Id );
}

internal void \u0023\u003Dz9i5WbtNpD44L( IChartElement _param1, string _param2 )
{
    this._idToName[ _param1.Id ] = _param2;
}

bool IChartComponent.\u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L6cpvMujIRSLvkaL\u0024hn1t0cz(
  string _param1)
  {
    return ( ( BaseCollection<string, ISet<string>> ) this._extraName ).Contains( _param1 );
}

internal void \u0023\u003DziQx4gl4\u003D(string _param1)
  {
    ((BaseCollection<string, ISet<string>>) this._extraName).Add(_param1);
}

string IChartComponent.\u0023\u003DzNCoz_cr7eiA6K6bzw3PTScnmc152IcDGHYqt97WzXCM_()
  {
    string fullTitle = this.FullTitle;
    if ( !StringHelper.IsEmptyOrWhiteSpace( fullTitle ) )
        return fullTitle;
    string generatedTitle = this.GetGeneratedTitle();
    return !StringHelper.IsEmptyOrWhiteSpace( generatedTitle ) ? generatedTitle : Extensions.GetDisplayName( ( ICustomAttributeProvider ) ( ( object ) this ).GetType(), ( string ) null );
}

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

internal override T CopyTo( T _param1 )
{
    base.CopyTo( _param1 );
    _param1.IsVisible = this.IsVisible;
    _param1.FullTitle = this.FullTitle;
    _param1.IsLegend = this.IsLegend;
    _param1.XAxisId = this.XAxisId;
    _param1.YAxisId = this.YAxisId;
    IChartElement[] cache1 = this._childElements.Cache;
    IChartElement[] cache2 = _param1._childElements.Cache;
    if ( cache1.Length != cache2.Length )
        throw new InvalidOperationException( "unexpected number of clones children" );
    for ( int index = 0 ; index < cache1.Length ; ++index )
    {
        IChartElement chartElement1 = cache1[index];
        IChartElement chartElement2 = cache2[index];
        if ( chartElement1.GetType() != chartElement2.GetType() )
            throw new InvalidOperationException( "unexpected child type" );
        ( ( IChartComponent ) chartElement1 ).CopyTo( ( object ) chartElement2 );
    }
    _param1.DontDraw = ( ( IChartComponent ) this ).DontDraw;
    return _param1;
}

protected virtual T CreateClone() => ( T ) Activator.CreateInstance( ( ( object ) this ).GetType() );

public virtual T Clone()
{
    ChartElement<T>.\u0023\u003Dzp8Uw9yL4K8Elv3B8f_3h_PM\u003D uw9yL4K8Elv3B8f3hPm = new ChartElement<T>.\u0023\u003Dzp8Uw9yL4K8Elv3B8f_3h_PM\u003D();
    uw9yL4K8Elv3B8f3hPm.\u0023\u003DzCdVlvQ8\u003D = this.CreateClone();
    uw9yL4K8Elv3B8f3hPm.\u0023\u003DzCdVlvQ8\u003D._extraName.AddRange( ( IEnumerable<string> ) this._extraName );
    CollectionHelper.ForEach<KeyValuePair<Guid, string>>( ( IEnumerable<KeyValuePair<Guid, string>> ) this._idToName, new Action<KeyValuePair<Guid, string>>( uw9yL4K8Elv3B8f3hPm.\u0023\u003DzHCUE8pxyT_S0mUpBMw\u003D\u003D));
    this.CopyTo(uw9yL4K8Elv3B8f3hPm.\u0023\u003DzCdVlvQ8\u003D);
    return uw9yL4K8Elv3B8f3hPm.\u0023\u003DzCdVlvQ8\u003D;
  }

  void IChartComponent.\u0023\u003Dzq8lPttT4Qpp4TSswk_CaTVGFY7g05gBW2YtIUCo\u003D(
    object _param1)
  {
    this.CopyTo((T) _param1);
  }

  public virtual bool CheckAxesCompatible(ChartAxisType? xType, ChartAxisType? yType)
  {
    if (xType.HasValue && xType.GetValueOrDefault() != ChartAxisType.CategoryDateTime)
      return false;
    return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
  }

  private void \u0023\u003DzSJdYbCcHBpzmYpsDytEtTkY0OpqoIhQR2IXqEAMCtmjWsFRg\u0024g\u003D\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    this.RaisePropertyChanged(_param2.PropertyName);
  }

  private sealed class \u0023\u003Dz85OtBH9U8Jor1ugvGeCVjho\u003D
  {
    public 
    #nullable disable
    Func<IComparable, System.Drawing.Color?> \u0023\u003DzxGz2_8k\u003D;

    internal System.Windows.Media.Color? \u0023\u003DzNip3gTCc6g3u5i_hYaD3SNqkOIf_XRRRGcMFPq3oEBffKcEyNg\u003D\u003D(
      IComparable _param1)
    {
      System.Drawing.Color? nullable = this.\u0023\u003DzxGz2_8k\u003D(_param1);
      ref System.Drawing.Color? local = ref nullable;
      return !local.HasValue ? new System.Windows.Media.Color?() : new System.Windows.Media.Color?(local.GetValueOrDefault().ToWpf());
    }
  }

  private sealed class \u0023\u003Dzp8Uw9yL4K8Elv3B8f_3h_PM\u003D
  {
    public T \u0023\u003DzCdVlvQ8\u003D;

    internal void \u0023\u003DzHCUE8pxyT_S0mUpBMw\u003D\u003D(KeyValuePair<Guid, string> _param1)
    {
      this.\u0023\u003DzCdVlvQ8\u003D._idToName[_param1.Key] = _param1.Value;
    }
  }
}
