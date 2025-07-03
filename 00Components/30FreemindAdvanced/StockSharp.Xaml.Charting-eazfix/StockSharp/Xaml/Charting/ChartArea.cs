// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartArea
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Charts;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable enable
namespace StockSharp.Xaml.Charting;

[Display(ResourceType = typeof (LocalizedStrings), Name = "ChartArea")]
public class ChartArea : 
  ChartPart<ChartArea>,
  IChartArea,
  IDisposable,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartPart<IChartArea>
{
    private class PropertiesNotifyList<T> :
    BaseList<T>,
    INotifyPropertyChanged,
    INotifyCollectionChanged
    {
        
        private int _index;
        
        private T _property;
        
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected override bool OnRemove( T _param1 )
        {
            this._index = base.IndexOf( _param1 );
            return base.OnRemove( _param1 );
        }

        protected override void OnRemoved( T _param1 )
        {
            if ( this._index >= 0 )
                this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Remove, _param1, this._index );
            base.OnRemoved( _param1 );
        }

        protected override void OnInserted( int _param1, T _param2 )
        {
            this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Add, _param2, _param1 );
            base.OnInserted( _param1, _param2 );
        }

        protected override void OnAdded( T _param1 )
        {
            this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Add, _param1, base.Count - 1 );
            base.OnAdded( _param1 );
        }

        protected override void OnRemoveAt( int _param1 )
        {
            this._property = base[ _param1 ];
            base.OnRemoveAt( _param1 );
        }

        protected override void OnRemovedAt( int _param1 )
        {
            if ( _property != null )
                this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Remove, this._property, _param1 );
            base.OnRemovedAt( _param1 );
        }

        protected override void OnCleared()
        {
            this.ResetCollectionChangedEvent();
            base.OnCleared();
        }

        private void RaiseCollectionChangedEvent( NotifyCollectionChangedAction action, T changedItem, int index )
        {
            this.RaisePropertyChangedEvent( "Count" );
            this.RaisePropertyChangedEvent( "Item[]" );
            
            CollectionChanged?.Invoke( this, new NotifyCollectionChangedEventArgs( action, changedItem, index ) );
        }

        private void ResetCollectionChangedEvent()
        {
            this.RaisePropertyChangedEvent( "Count" );
            this.RaisePropertyChangedEvent( "Item[]" );

            CollectionChanged?.Invoke( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        private void RaisePropertyChangedEvent( string _param1 )
        {            
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( _param1 ) );
        }        
    }

    private sealed class ChartElementNotifyList( ChartArea area ) : ChartArea.PropertiesNotifyList<IChartElement>
    {

        private readonly ChartArea _chartArea = area ?? throw new ArgumentNullException("area");

        protected override bool OnAdding( IChartElement element )
        {
            ChartArea.ChartElementNotifyList.SomeClass1234 _someMemebers1234 = new ChartArea.ChartElementNotifyList.SomeClass1234();
            _someMemebers1234._someChartElement = element;

            if ( element.TryGetChart() != null )
            {
                throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
            }
        
            _someMemebers1234.Some22343 = !( ( IEnumerable<IChartElement> ) this ).Any<IChartElement>( new Func<IChartElement, bool>( _someMemebers1234.\u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D) ) ? _someMemebers1234._someChartElement as IfxChartElement : throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
            if ( _someMemebers1234.Some22343 != null)
      {
                IChartAxis chartAxis = ((IEnumerable<IChartAxis>) this._chartArea.YAxises).FirstOrDefault<IChartAxis>( new Func<IChartAxis, bool>( _someMemebers1234.\u0023\u003DzYEKCeOuJjAm6Cv5GiA\u003D\u003D) );
                if ( !_someMemebers1234.Some22343.CheckAxesCompatible( new ChartAxisType?( this._chartArea.XAxisType ), chartAxis?.AxisType ))
          throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.AxesTypesNotSupportedParams, new object[ 3 ]
          {
            (object) _someMemebers1234.Some22343.GetType().Name,
            (object) this._chartArea.XAxisType,
            (object) chartAxis?.AxisType
          } ) );
            }
            return ( ( BaseCollection<IChartElement, IList<IChartElement>> ) this ).OnAdding( _someMemebers1234._someChartElement);
        }

        private sealed class SomeClass1234
    {
      public IChartElement _someChartElement;
      public IfxChartElement Some22343;

      internal bool \u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D(IChartElement _param1)
      {
        return _param1.Id == this._someChartElement.Id;
      }

    internal bool \u0023\u003DzYEKCeOuJjAm6Cv5GiA\u003D\u003D(IChartAxis _param1)
      {
        return _param1.Id == this.Some22343.YAxisId;
      }
    }
  }
    
  private readonly \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj _scichartSurfaceMVVM;
  
  private IChart _chart;
  
  private ChartAxisType _xAxisType = ChartAxisType.CategoryDateTime;
  
  private string _title;
  
  private double _height;
  
  private readonly INotifyList<IChartElement> _chartElementNotifyList;
  
  private readonly INotifyList<IChartAxis> _xAxisNotifyList;
  
  private readonly INotifyList<IChartAxis> _yAxisNotifyList;

  public ChartArea()
  {
    this._chartElementNotifyList; = (INotifyList<IChartElement>) new ChartArea.ChartElementNotifyList(this);
    this._xAxisNotifyList = (INotifyList<IChartAxis>) new ChartArea.AxisNotifyList(this, true);
    this._yAxisNotifyList = (INotifyList<IChartAxis>) new ChartArea.AxisNotifyList(this, false);
    this.InitAxises();
    this._scichartSurfaceMVVM = new \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj(this);
    this.Height = 100.0;
    this.ViewModel().PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzg7PFOA2RIl9h1rTv9w\u003D\u003D);
  }

  internal \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj ViewModel()
  {
    return this._scichartSurfaceMVVM;
  }

  private void InitAxises()
  {
    if (!((IEnumerable<IChartAxis>) this.XAxises).Any<IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<IChartAxis, bool>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzfnNqP9jz3szEAuvQ\u0024gr5C7U\u003D))))
      ((ICollection<IChartAxis>) this.XAxises).Add((IChartAxis) new ChartAxis()
      {
        Id = "X",
        AutoRange = false,
        AxisType = ChartAxisType.CategoryDateTime
      });
    if (((IEnumerable<IChartAxis>) this.YAxises).Any<IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D = new Func<IChartAxis, bool>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz\u0024PoY\u0024FfmSWryZZmtwAl\u0024D38\u003D))))
      return;
    ((ICollection<IChartAxis>) this.YAxises).Add((IChartAxis) new ChartAxis()
    {
      Id = "Y",
      AxisType = ChartAxisType.Numeric
    });
  }

  [Browsable(false)]
  public IChart Chart
  {
    get => this._chart;
    set
    {
      if (this._chart == value)
        return;
      if (value == null)
        this.ViewModel().\u0023\u003DzfzUoR7Shr0zN2v5f65kznZY\u003D();
      this._chart = value;
      if (value == null)
        return;
      this.ViewModel().\u0023\u003DzLHZzmaP3Zzon();
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.Elements, ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D = new Action<IChartElement>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzM30dyEF9Fb2bzYLjmLgjtiE\u003D)));
    }
  }

  [Browsable(false)]
  public ChartAxisType XAxisType
  {
    get => this._xAxisType;
    set
    {
      ChartArea.\u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D dkfA7SK9Zsjh7b7evY = new ChartArea.\u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D();
      dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D = value;
      dkfA7SK9Zsjh7b7evY.\u0023\u003DzRRvwDu67s9Rm = this;
      if (this._xAxisType == dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D)
        return;
      IChart chart1 = this.Chart;
      if (chart1 != null)
        chart1.EnsureUIThread();
      if (((IEnumerable) this.Elements).Cast<IfxChartElement>().Any<IfxChartElement>(new Func<IfxChartElement, bool>(dkfA7SK9Zsjh7b7evY.\u0023\u003Dz6AGy\u0024GSay7_DCrT8g6JJYhI\u003D)))
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.ElementDontSupportAxisTypeParams, new object[1]
        {
          (object) dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D
        }));
      if (this.Chart != null && this.Chart.Areas.Any<IChartArea>(new Func<IChartArea, bool>(dkfA7SK9Zsjh7b7evY.\u0023\u003DzCaHczB6Zuyll\u0024N6TY3bDRZc\u003D)))
        throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      this._xAxisType = dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D;
      List<ChartAxis> chartAxisList = new List<ChartAxis>();
      foreach (IChartAxis xaxise in (IEnumerable<IChartAxis>) this.XAxises)
      {
        ChartAxis chartAxis = new ChartAxis()
        {
          Id = xaxise.Id,
          AutoRange = xaxise.AutoRange,
          AxisType = this._xAxisType
        };
        chartAxisList.Add(chartAxis);
      }
      IChart chart2 = this.Chart;
      this.Chart = (IChart) null;
      ICollection<IChartAxis> xaxises1 = (ICollection<IChartAxis>) this.XAxises;
      INotifyList<IChartAxis> xaxises2 = this.XAxises;
      int index = 0;
      IChartAxis[] chartAxisArray = new IChartAxis[((ICollection<IChartAxis>) xaxises2).Count];
      foreach (IChartAxis chartAxis in (IEnumerable<IChartAxis>) xaxises2)
      {
        chartAxisArray[index] = chartAxis;
        ++index;
      }
      CollectionHelper.RemoveRange<IChartAxis>(xaxises1, (IEnumerable<IChartAxis>) new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<IChartAxis>(chartAxisArray));
      CollectionHelper.AddRange<IChartAxis>((ICollection<IChartAxis>) this.XAxises, (IEnumerable<IChartAxis>) chartAxisList);
      this.Chart = chart2;
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Name", Description = "ChartAreaName", GroupName = "Common", Order = 0)]
  public string Title
  {
    get => this._title;
    set
    {
      this._title = value;
      this.RaisePropertyChanged(nameof (Title));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "GroupId", Description = "ChartPaneGroupDescription", GroupName = "Common", Order = 1)]
  public string GroupId
  {
    get => this._groupId;
    set => this._groupId = value;
  }

  [Browsable(false)]
  public double Height
  {
    get => this._height;
    set
    {
      if (Math.Abs(this._height - value) < double.Epsilon)
        return;
      this._height = value;
      this.RaisePropertyChanged(nameof (Height));
    }
  }

  [Browsable(false)]
  public INotifyList<IChartElement> Elements => this._chartElementNotifyList;;

  public INotifyList<IChartAxis> XAxises => this._xAxisNotifyList;

  public INotifyList<IChartAxis> YAxises => this._yAxisNotifyList;

  public override void Load(SettingsStorage storage)
  {
    ((ICollection<IChartElement>) this.Elements).Clear();
    base.Load(storage);
    this.Title = storage.GetValue<string>("Title", (string) null);
    this.Height = storage.GetValue<double>("Height", 0.0);
    this.XAxisType = storage.GetValue<ChartAxisType>("XAxisType", this.XAxisType);
    this.GroupId = storage.GetValue<string>("GroupId", this.GroupId);
    ChartArea.\u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(storage, "XAxises", (ICollection<IChartAxis>) this.XAxises);
    ChartArea.\u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(storage, "YAxises", (ICollection<IChartAxis>) this.YAxises);
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<string>("Title", this.Title);
    storage.SetValue<double>("Height", this.Height);
    storage.SetValue<ChartAxisType>("XAxisType", this.XAxisType);
    storage.SetValue<string>("GroupId", this.GroupId);
    storage.SetValue<SettingsStorage[]>("XAxises", ((IEnumerable<IChartAxis>) this.XAxises).Select<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D = new Func<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzxv2ll83UBK_RmlktVQ\u003D\u003D))).ToArray<SettingsStorage>());
    storage.SetValue<SettingsStorage[]>("YAxises", ((IEnumerable<IChartAxis>) this.YAxises).Select<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u00247pvGNKBwldamQkwwQ\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u00247pvGNKBwldamQkwwQ\u003D\u003D = new Func<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzx2FJ4suusAb7GDyK1w\u003D\u003D))).ToArray<SettingsStorage>());
  }

  private static void \u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(
    SettingsStorage _param0,
    string _param1,
    ICollection<IChartAxis> _param2)
  {
    IEnumerable<SettingsStorage> source = _param0.GetValue<IEnumerable<SettingsStorage>>(_param1, (IEnumerable<SettingsStorage>) null);
    if (source == null)
      return;
    _param2.Clear();
    CollectionHelper.AddRange<IChartAxis>(_param2, (IEnumerable<IChartAxis>) source.Select<SettingsStorage, ChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D = new Func<SettingsStorage, ChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzr2ViHsE5u5Iy0l8GPqzpBAb_0ZNf))));
  }

  public virtual ChartArea Clone()
  {
    ChartArea chartArea = this.\u0023\u003Dz3MbNd8U\u003D(new ChartArea()
    {
      Title = this.Title,
      Height = this.Height,
      XAxisType = this.XAxisType
    });
    CollectionHelper.AddRange<IChartElement>((ICollection<IChartElement>) chartArea.Elements, ((IEnumerable<IChartElement>) this.Elements).Select<IChartElement, IChartElement>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz8slTl9RRXzpBYOxh4Q\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz8slTl9RRXzpBYOxh4Q\u003D\u003D = new Func<IChartElement, IChartElement>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzLhPQ\u0024JfQhEkyu0vUWg\u003D\u003D))));
    ((ICollection<IChartAxis>) chartArea.XAxises).Clear();
    CollectionHelper.AddRange<IChartAxis>((ICollection<IChartAxis>) chartArea.XAxises, ((IEnumerable<IChartAxis>) this.XAxises).Select<IChartAxis, IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D = new Func<IChartAxis, IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzGeF_1AAuPyWwDbL_iA\u003D\u003D))));
    ((ICollection<IChartAxis>) chartArea.YAxises).Clear();
    CollectionHelper.AddRange<IChartAxis>((ICollection<IChartAxis>) chartArea.YAxises, ((IEnumerable<IChartAxis>) this.YAxises).Select<IChartAxis, IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzrUb4sQiSyo1cFneMgA\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzrUb4sQiSyo1cFneMgA\u003D\u003D = new Func<IChartAxis, IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzf4yIUkrz2a0As47tiA\u003D\u003D))));
    return chartArea;
  }

  public virtual string ToString() => this.Title;

  public void Dispose()
  {
    this.ViewModel().Dispose();
    GC.SuppressFinalize((object) false);
  }

  private void \u0023\u003Dzg7PFOA2RIl9h1rTv9w\u003D\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == "PaneGroupSuffix"))
      return;
    this.RaisePropertyChanged("GroupId");
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly 
    #nullable disable
    ChartArea.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartArea.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IChartAxis, bool> \u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D;
    public static Func<IChartAxis, bool> \u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D;
    public static Action<IChartElement> \u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D;
    public static Func<IChartAxis, SettingsStorage> \u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D;
    public static Func<IChartAxis, SettingsStorage> \u0023\u003Dz\u00247pvGNKBwldamQkwwQ\u003D\u003D;
    public static Func<SettingsStorage, ChartAxis> \u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D;
    public static Func<IChartElement, IChartElement> \u0023\u003Dz8slTl9RRXzpBYOxh4Q\u003D\u003D;
    public static Func<IChartAxis, IChartAxis> \u0023\u003DzG9p0UKsG3FcNaICZMQ\u003D\u003D;
    public static Func<IChartAxis, IChartAxis> \u0023\u003DzrUb4sQiSyo1cFneMgA\u003D\u003D;

    internal bool \u0023\u003DzfnNqP9jz3szEAuvQ\u0024gr5C7U\u003D(IChartAxis _param1)
    {
      return _param1.Id == "X";
    }

    internal bool \u0023\u003Dz\u0024PoY\u0024FfmSWryZZmtwAl\u0024D38\u003D(IChartAxis _param1)
    {
      return _param1.Id == "Y";
    }

    internal void \u0023\u003DzM30dyEF9Fb2bzYLjmLgjtiE\u003D(IChartElement _param1)
    {
      if (!(_param1 is IfxChartElement ddznyiGmdRlAevOq))
        return;
      ddznyiGmdRlAevOq.ResetUI();
    }

    internal SettingsStorage \u0023\u003Dzxv2ll83UBK_RmlktVQ\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }

    internal SettingsStorage \u0023\u003Dzx2FJ4suusAb7GDyK1w\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }

    internal ChartAxis \u0023\u003Dzr2ViHsE5u5Iy0l8GPqzpBAb_0ZNf(SettingsStorage _param1)
    {
      return PersistableHelper.Load<ChartAxis>(_param1);
    }

    internal IChartElement \u0023\u003DzLhPQ\u0024JfQhEkyu0vUWg\u003D\u003D(IChartElement _param1)
    {
      return PersistableHelper.Clone<IChartElement>(_param1);
    }

    internal IChartAxis \u0023\u003DzGeF_1AAuPyWwDbL_iA\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Clone<IChartAxis>(_param1);
    }

    internal IChartAxis \u0023\u003Dzf4yIUkrz2a0As47tiA\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Clone<IChartAxis>(_param1);
    }
  }

  

  private sealed class \u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D
  {
    public ChartAxisType \u0023\u003DzxGz2_8k\u003D;
    public ChartArea \u0023\u003DzRRvwDu67s9Rm;

    internal bool \u0023\u003Dz6AGy\u0024GSay7_DCrT8g6JJYhI\u003D(
      IfxChartElement _param1)
    {
      return !_param1.CheckAxesCompatible(new ChartAxisType?(this.\u0023\u003DzxGz2_8k\u003D), new ChartAxisType?());
    }

    internal bool \u0023\u003DzCaHczB6Zuyll\u0024N6TY3bDRZc\u003D(IChartArea _param1)
    {
      return _param1 != this.\u0023\u003DzRRvwDu67s9Rm && _param1.XAxisType != this.\u0023\u003DzxGz2_8k\u003D;
    }
  }

  private sealed class AxisNotifyList(ChartArea _param1, bool _param2) : 
    ChartArea.PropertiesNotifyList<IChartAxis>
  {
    
    private static int \u0023\u003Dz\u0024aPzPF8\u003D;
    
    private static int \u0023\u003DzllfDbLI\u003D;
    
    private readonly bool \u0023\u003DzxUZHFO7FWvR7ohJHTLp\u002424E\u003D = _param2;
    
    private readonly ChartArea _chartArea = _param1 ?? throw new ArgumentNullException("area");

    private bool \u0023\u003Dz2BfyUzmYpKwx()
    {
      return this.\u0023\u003DzxUZHFO7FWvR7ohJHTLp\u002424E\u003D;
    }

    protected virtual bool OnAdding(IChartAxis _param1)
    {
      ChartArea.AxisNotifyList.\u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D zPKCmcad6Nxc5A8A = new ChartArea.AxisNotifyList.\u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D();
      zPKCmcad6Nxc5A8A._someChartElement = _param1;
      string str = this.\u0023\u003Dz2BfyUzmYpKwx() ? "X" : "Y";
      int num1;
      if (!this.\u0023\u003Dz2BfyUzmYpKwx())
        num1 = ++ChartArea.AxisNotifyList.\u0023\u003DzllfDbLI\u003D;
      else
        ChartArea.AxisNotifyList.\u0023\u003Dz\u0024aPzPF8\u003D = num1 = ChartArea.AxisNotifyList.\u0023\u003Dz\u0024aPzPF8\u003D + 1;
      int num2 = num1;
      if (StringHelper.IsEmpty(zPKCmcad6Nxc5A8A._someChartElement.Id))
        zPKCmcad6Nxc5A8A._someChartElement.Id = $"{str}({Guid.NewGuid()})";
      if (((IEnumerable<IChartAxis>) this).Any<IChartAxis>(new Func<IChartAxis, bool>(zPKCmcad6Nxc5A8A.\u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D)))
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.AxisAlreadyAdded, new object[1]
        {
          (object) zPKCmcad6Nxc5A8A._someChartElement.Id
        }));
      if (this == this._chartArea.XAxises && zPKCmcad6Nxc5A8A._someChartElement.AxisType != this._chartArea.XAxisType)
        throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      foreach (IfxChartElement elem in ((IEnumerable) this._chartArea.Elements).Cast<IfxChartElement>())
      {
        if (elem.TryGetXAxis() == null && this == this._chartArea.XAxises && zPKCmcad6Nxc5A8A._someChartElement.Id == elem.XAxisId && !elem.CheckAxesCompatible(new ChartAxisType?(zPKCmcad6Nxc5A8A._someChartElement.AxisType), new ChartAxisType?()))
          throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
        if (elem.TryGetYAxis() == null && this == this._chartArea.YAxises && zPKCmcad6Nxc5A8A._someChartElement.Id == elem.YAxisId && !elem.CheckAxesCompatible(new ChartAxisType?(), new ChartAxisType?(zPKCmcad6Nxc5A8A._someChartElement.AxisType)))
          throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      }
      if (this.\u0023\u003Dz2BfyUzmYpKwx() && StringHelper.IsEmpty(zPKCmcad6Nxc5A8A._someChartElement.Group))
        zPKCmcad6Nxc5A8A._someChartElement.Group = zPKCmcad6Nxc5A8A._someChartElement.AxisType.ToString() + zPKCmcad6Nxc5A8A._someChartElement.Id;
      if (StringHelper.IsEmpty(zPKCmcad6Nxc5A8A._someChartElement.Title))
        zPKCmcad6Nxc5A8A._someChartElement.Title = str + num2.ToString();
      ((ChartAxis) zPKCmcad6Nxc5A8A._someChartElement).ChartArea = (IChartArea) this._chartArea;
      return ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnAdding(zPKCmcad6Nxc5A8A._someChartElement);
    }

    protected virtual bool OnRemoving(IChartAxis _param1)
    {
      ChartAxis chartAxis = (ChartAxis) _param1;
      bool flag = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).Contains((IChartAxis) chartAxis);
      if (flag && this._chartArea.Chart != null && CompareHelper.IsDefault<ChartAxis>(chartAxis))
        throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
      int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnRemoving((IChartAxis) chartAxis) ? 1 : 0;
      if ((num & (flag ? 1 : 0)) == 0)
        return num != 0;
      chartAxis.ChartArea = (IChartArea) null;
      return num != 0;
    }

    protected virtual bool OnRemovingAt(int _param1)
    {
      ChartAxis chartAxis = (ChartAxis) ((BaseCollection<IChartAxis, IList<IChartAxis>>) this)[_param1];
      if (CompareHelper.IsDefault<ChartAxis>(chartAxis) && this._chartArea.Chart != null)
        throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
      int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnRemovingAt(_param1) ? 1 : 0;
      if (num == 0)
        return num != 0;
      chartAxis.ChartArea = (IChartArea) null;
      return num != 0;
    }

    protected virtual bool OnClearing()
    {
      if (this._chartArea.Chart != null)
        throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
      IChartAxis[] array = ((IEnumerable<IChartAxis>) this).ToArray<IChartAxis>();
      int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnClearing() ? 1 : 0;
      if (num == 0)
        return num != 0;
      CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) array, ChartArea.AxisNotifyList.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (ChartArea.AxisNotifyList.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Action<IChartAxis>(ChartArea.AxisNotifyList.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzE70qt2sPjBBv095jMVMFSaY\u003D)));
      return num != 0;
    }

    [Serializable]
    private sealed class \u0023\u003Dz7qOdpi4\u003D
    {
      public static readonly ChartArea.AxisNotifyList.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartArea.AxisNotifyList.\u0023\u003Dz7qOdpi4\u003D();
      public static Action<IChartAxis> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

      internal void \u0023\u003DzE70qt2sPjBBv095jMVMFSaY\u003D(IChartAxis _param1)
      {
        ((ChartAxis) _param1).ChartArea = (IChartArea) null;
      }
    }

    private sealed class \u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D
    {
      public IChartAxis _someChartElement;

      internal bool \u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D(IChartAxis _param1)
      {
        return _param1.Id == this._someChartElement.Id;
      }
    }
  }

  
}
