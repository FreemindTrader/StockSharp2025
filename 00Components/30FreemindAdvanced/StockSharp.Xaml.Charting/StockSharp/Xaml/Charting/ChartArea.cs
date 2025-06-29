// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartArea
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
using System.Runtime.CompilerServices;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>Chart area.</summary>
[Display(ResourceType = typeof (LocalizedStrings), Name = "ChartArea")]
public class ChartArea : 
  ChartPart<
  #nullable disable
  ChartArea>,
  IDisposable,
  IChartArea,
  IChartPart<IChartArea>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj \u0023\u003Dzu1T81HbxhQKC3OpEvw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IChart \u0023\u003DzF\u002458l4g\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ChartAxisType \u0023\u003Dzzk\u00249uybYN\u0024wy = ChartAxisType.CategoryDateTime;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DzIzRA1GQ\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzqcgkI5Q\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly INotifyList<IChartElement> \u0023\u003DzflSOSCebwQXGi8fUNg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly INotifyList<IChartAxis> \u0023\u003DzYe8QEajBkzI0US3jz3KFoMQ\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly INotifyList<IChartAxis> \u0023\u003DzYQqL3flRD33LHxyIdb39M1g\u003D;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartArea" />.
  /// </summary>
  public ChartArea()
  {
    this.\u0023\u003DzflSOSCebwQXGi8fUNg\u003D\u003D = (INotifyList<IChartElement>) new ChartArea.\u0023\u003DzI6tCp9WWq\u0024x\u0024(this);
    this.\u0023\u003DzYe8QEajBkzI0US3jz3KFoMQ\u003D = (INotifyList<IChartAxis>) new ChartArea.\u0023\u003Dzc77TSXc\u003D(this, true);
    this.\u0023\u003DzYQqL3flRD33LHxyIdb39M1g\u003D = (INotifyList<IChartAxis>) new ChartArea.\u0023\u003Dzc77TSXc\u003D(this, false);
    this.\u0023\u003DzzEkR_P1lc2uI();
    this.\u0023\u003Dzu1T81HbxhQKC3OpEvw\u003D\u003D = new \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj(this);
    this.Height = 100.0;
    this.\u0023\u003Dz3ThQNm3rQ1fp().PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzg7PFOA2RIl9h1rTv9w\u003D\u003D);
  }

  internal \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj \u0023\u003Dz3ThQNm3rQ1fp()
  {
    return this.\u0023\u003Dzu1T81HbxhQKC3OpEvw\u003D\u003D;
  }

  private void \u0023\u003DzzEkR_P1lc2uI()
  {
    if (!((IEnumerable<IChartAxis>) this.XAxises).Any<IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<IChartAxis, bool>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzfnNqP9jz3szEAuvQ\u0024gr5C7U\u003D))))
      ((ICollection<IChartAxis>) this.XAxises).Add((IChartAxis) new ChartAxis()
      {
        Id = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433902),
        AutoRange = false,
        AxisType = ChartAxisType.CategoryDateTime
      });
    if (((IEnumerable<IChartAxis>) this.YAxises).Any<IChartAxis>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D = new Func<IChartAxis, bool>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz\u0024PoY\u0024FfmSWryZZmtwAl\u0024D38\u003D))))
      return;
    ((ICollection<IChartAxis>) this.YAxises).Add((IChartAxis) new ChartAxis()
    {
      Id = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432528),
      AxisType = ChartAxisType.Numeric
    });
  }

  /// <inheritdoc />
  [Browsable(false)]
  public IChart Chart
  {
    get => this.\u0023\u003DzF\u002458l4g\u003D;
    set
    {
      if (this.\u0023\u003DzF\u002458l4g\u003D == value)
        return;
      if (value == null)
        this.\u0023\u003Dz3ThQNm3rQ1fp().\u0023\u003DzfzUoR7Shr0zN2v5f65kznZY\u003D();
      this.\u0023\u003DzF\u002458l4g\u003D = value;
      if (value == null)
        return;
      this.\u0023\u003Dz3ThQNm3rQ1fp().\u0023\u003DzLHZzmaP3Zzon();
      CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.Elements, ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D = new Action<IChartElement>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzM30dyEF9Fb2bzYLjmLgjtiE\u003D)));
    }
  }

  /// <inheritdoc />
  [Browsable(false)]
  public ChartAxisType XAxisType
  {
    get => this.\u0023\u003Dzzk\u00249uybYN\u0024wy;
    set
    {
      ChartArea.\u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D dkfA7SK9Zsjh7b7evY = new ChartArea.\u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D();
      dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D = value;
      dkfA7SK9Zsjh7b7evY.\u0023\u003DzRRvwDu67s9Rm = this;
      if (this.\u0023\u003Dzzk\u00249uybYN\u0024wy == dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D)
        return;
      IChart chart1 = this.Chart;
      if (chart1 != null)
        chart1.EnsureUIThread();
      if (((IEnumerable) this.Elements).Cast<\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D>().Any<\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D>(new Func<\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D, bool>(dkfA7SK9Zsjh7b7evY.\u0023\u003Dz6AGy\u0024GSay7_DCrT8g6JJYhI\u003D)))
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.ElementDontSupportAxisTypeParams, new object[1]
        {
          (object) dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D
        }));
      if (this.Chart != null && this.Chart.Areas.Any<IChartArea>(new Func<IChartArea, bool>(dkfA7SK9Zsjh7b7evY.\u0023\u003DzCaHczB6Zuyll\u0024N6TY3bDRZc\u003D)))
        throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      this.\u0023\u003Dzzk\u00249uybYN\u0024wy = dkfA7SK9Zsjh7b7evY.\u0023\u003DzxGz2_8k\u003D;
      List<ChartAxis> chartAxisList = new List<ChartAxis>();
      foreach (IChartAxis xaxise in (IEnumerable<IChartAxis>) this.XAxises)
      {
        ChartAxis chartAxis = new ChartAxis()
        {
          Id = xaxise.Id,
          AutoRange = xaxise.AutoRange,
          AxisType = this.\u0023\u003Dzzk\u00249uybYN\u0024wy
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

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Name", Description = "ChartAreaName", GroupName = "Common", Order = 0)]
  public string Title
  {
    get => this.\u0023\u003DzIzRA1GQ\u003D;
    set
    {
      this.\u0023\u003DzIzRA1GQ\u003D = value;
      this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431078));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "GroupId", Description = "ChartPaneGroupDescription", GroupName = "Common", Order = 1)]
  public string GroupId
  {
    get => this.\u0023\u003Dz3ThQNm3rQ1fp().PaneGroupSuffix;
    set => this.\u0023\u003Dz3ThQNm3rQ1fp().PaneGroupSuffix = value;
  }

  /// <inheritdoc />
  [Browsable(false)]
  public double Height
  {
    get => this.\u0023\u003DzqcgkI5Q\u003D;
    set
    {
      if (Math.Abs(this.\u0023\u003DzqcgkI5Q\u003D - value) < double.Epsilon)
        return;
      this.\u0023\u003DzqcgkI5Q\u003D = value;
      this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432499));
    }
  }

  /// <inheritdoc />
  [Browsable(false)]
  public INotifyList<IChartElement> Elements => this.\u0023\u003DzflSOSCebwQXGi8fUNg\u003D\u003D;

  /// <inheritdoc />
  public INotifyList<IChartAxis> XAxises => this.\u0023\u003DzYe8QEajBkzI0US3jz3KFoMQ\u003D;

  /// <inheritdoc />
  public INotifyList<IChartAxis> YAxises => this.\u0023\u003DzYQqL3flRD33LHxyIdb39M1g\u003D;

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    ((ICollection<IChartElement>) this.Elements).Clear();
    base.Load(storage);
    this.Title = storage.GetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431078), (string) null);
    this.Height = storage.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432499), 0.0);
    this.XAxisType = storage.GetValue<ChartAxisType>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433957), this.XAxisType);
    this.GroupId = storage.GetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433749), this.GroupId);
    ChartArea.\u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433735), (ICollection<IChartAxis>) this.XAxises);
    ChartArea.\u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433777), (ICollection<IChartAxis>) this.YAxises);
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539431078), this.Title);
    storage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432499), this.Height);
    storage.SetValue<ChartAxisType>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433957), this.XAxisType);
    storage.SetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433749), this.GroupId);
    storage.SetValue<SettingsStorage[]>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433735), ((IEnumerable<IChartAxis>) this.XAxises).Select<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz_NsrFZCmUa8QKCiCnA\u003D\u003D = new Func<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzxv2ll83UBK_RmlktVQ\u003D\u003D))).ToArray<SettingsStorage>());
    storage.SetValue<SettingsStorage[]>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433777), ((IEnumerable<IChartAxis>) this.YAxises).Select<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u00247pvGNKBwldamQkwwQ\u003D\u003D ?? (ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u00247pvGNKBwldamQkwwQ\u003D\u003D = new Func<IChartAxis, SettingsStorage>(ChartArea.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzx2FJ4suusAb7GDyK1w\u003D\u003D))).ToArray<SettingsStorage>());
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

  /// <summary>
  /// Create a copy of <see cref="T:StockSharp.Xaml.Charting.ChartArea" />.
  /// </summary>
  /// <returns>Copy.</returns>
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

  /// <inheritdoc />
  public virtual string ToString() => this.Title;

  /// <inheritdoc />
  public void Dispose()
  {
    this.\u0023\u003Dz3ThQNm3rQ1fp().Dispose();
    GC.SuppressFinalize((object) false);
  }

  private void \u0023\u003Dzg7PFOA2RIl9h1rTv9w\u003D\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433763)))
      return;
    this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433749));
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
      return _param1.Id == \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433902);
    }

    internal bool \u0023\u003Dz\u0024PoY\u0024FfmSWryZZmtwAl\u0024D38\u003D(IChartAxis _param1)
    {
      return _param1.Id == \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432528);
    }

    internal void \u0023\u003DzM30dyEF9Fb2bzYLjmLgjtiE\u003D(IChartElement _param1)
    {
      if (!(_param1 is \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D ddznyiGmdRlAevOq))
        return;
      ddznyiGmdRlAevOq.\u0023\u003DzMIAnwWQ\u003D();
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

  private sealed class \u0023\u003DzI6tCp9WWq\u0024x\u0024(ChartArea _param1) : 
    ChartArea.\u0023\u003DztvVgmnUXBPgS<IChartElement>
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ChartArea \u0023\u003DzeckSod0\u003D = _param1 ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433978));

    protected virtual bool OnAdding(IChartElement _param1)
    {
      ChartArea.\u0023\u003DzI6tCp9WWq\u0024x\u0024.\u0023\u003DzEIQ1d2DpkNoBum8VDQ\u003D\u003D q1d2DpkNoBum8Vdq = new ChartArea.\u0023\u003DzI6tCp9WWq\u0024x\u0024.\u0023\u003DzEIQ1d2DpkNoBum8VDQ\u003D\u003D();
      q1d2DpkNoBum8Vdq.\u0023\u003DzNoL9aC4\u003D = _param1;
      if (q1d2DpkNoBum8Vdq.\u0023\u003DzNoL9aC4\u003D.TryGetChart() != null)
        throw new InvalidOperationException(LocalizedStrings.ElementAlreadyAttached);
      q1d2DpkNoBum8Vdq.\u0023\u003DzAH1vHCc\u003D = !((IEnumerable<IChartElement>) this).Any<IChartElement>(new Func<IChartElement, bool>(q1d2DpkNoBum8Vdq.\u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D)) ? q1d2DpkNoBum8Vdq.\u0023\u003DzNoL9aC4\u003D as \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D : throw new InvalidOperationException(LocalizedStrings.ElementAlreadyAttached);
      if (q1d2DpkNoBum8Vdq.\u0023\u003DzAH1vHCc\u003D != null)
      {
        IChartAxis chartAxis = ((IEnumerable<IChartAxis>) this.\u0023\u003DzeckSod0\u003D.YAxises).FirstOrDefault<IChartAxis>(new Func<IChartAxis, bool>(q1d2DpkNoBum8Vdq.\u0023\u003DzYEKCeOuJjAm6Cv5GiA\u003D\u003D));
        if (!q1d2DpkNoBum8Vdq.\u0023\u003DzAH1vHCc\u003D.CheckAxesCompatible(new ChartAxisType?(this.\u0023\u003DzeckSod0\u003D.XAxisType), chartAxis?.AxisType))
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.AxesTypesNotSupportedParams, new object[3]
          {
            (object) q1d2DpkNoBum8Vdq.\u0023\u003DzAH1vHCc\u003D.GetType().Name,
            (object) this.\u0023\u003DzeckSod0\u003D.XAxisType,
            (object) chartAxis?.AxisType
          }));
      }
      return ((BaseCollection<IChartElement, IList<IChartElement>>) this).OnAdding(q1d2DpkNoBum8Vdq.\u0023\u003DzNoL9aC4\u003D);
    }

    private sealed class \u0023\u003DzEIQ1d2DpkNoBum8VDQ\u003D\u003D
    {
      public IChartElement \u0023\u003DzNoL9aC4\u003D;
      public \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D \u0023\u003DzAH1vHCc\u003D;

      internal bool \u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D(IChartElement _param1)
      {
        return _param1.Id == this.\u0023\u003DzNoL9aC4\u003D.Id;
      }

      internal bool \u0023\u003DzYEKCeOuJjAm6Cv5GiA\u003D\u003D(IChartAxis _param1)
      {
        return _param1.Id == this.\u0023\u003DzAH1vHCc\u003D.YAxisId;
      }
    }
  }

  private sealed class \u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D
  {
    public ChartAxisType \u0023\u003DzxGz2_8k\u003D;
    public ChartArea \u0023\u003DzRRvwDu67s9Rm;

    internal bool \u0023\u003Dz6AGy\u0024GSay7_DCrT8g6JJYhI\u003D(
      \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D _param1)
    {
      return !_param1.CheckAxesCompatible(new ChartAxisType?(this.\u0023\u003DzxGz2_8k\u003D), new ChartAxisType?());
    }

    internal bool \u0023\u003DzCaHczB6Zuyll\u0024N6TY3bDRZc\u003D(IChartArea _param1)
    {
      return _param1 != this.\u0023\u003DzRRvwDu67s9Rm && _param1.XAxisType != this.\u0023\u003DzxGz2_8k\u003D;
    }
  }

  private sealed class \u0023\u003Dzc77TSXc\u003D(ChartArea _param1, bool _param2) : 
    ChartArea.\u0023\u003DztvVgmnUXBPgS<IChartAxis>
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static int \u0023\u003Dz\u0024aPzPF8\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static int \u0023\u003DzllfDbLI\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool \u0023\u003DzxUZHFO7FWvR7ohJHTLp\u002424E\u003D = _param2;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ChartArea \u0023\u003DzeckSod0\u003D = _param1 ?? throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433942));

    private bool \u0023\u003Dz2BfyUzmYpKwx()
    {
      return this.\u0023\u003DzxUZHFO7FWvR7ohJHTLp\u002424E\u003D;
    }

    protected virtual bool OnAdding(IChartAxis _param1)
    {
      ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D zPKCmcad6Nxc5A8A = new ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D();
      zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D = _param1;
      string str = this.\u0023\u003Dz2BfyUzmYpKwx() ? \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433902) : \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432528);
      int num1;
      if (!this.\u0023\u003Dz2BfyUzmYpKwx())
        num1 = ++ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003DzllfDbLI\u003D;
      else
        ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz\u0024aPzPF8\u003D = num1 = ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz\u0024aPzPF8\u003D + 1;
      int num2 = num1;
      if (StringHelper.IsEmpty(zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Id))
      {
        IChartAxis zNoL9aC4 = zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
        interpolatedStringHandler.AppendFormatted(str);
        interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432568));
        interpolatedStringHandler.AppendFormatted<Guid>(Guid.NewGuid());
        interpolatedStringHandler.AppendLiteral(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432576));
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        zNoL9aC4.Id = stringAndClear;
      }
      if (((IEnumerable<IChartAxis>) this).Any<IChartAxis>(new Func<IChartAxis, bool>(zPKCmcad6Nxc5A8A.\u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D)))
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.AxisAlreadyAdded, new object[1]
        {
          (object) zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Id
        }));
      if (this == this.\u0023\u003DzeckSod0\u003D.XAxises && zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.AxisType != this.\u0023\u003DzeckSod0\u003D.XAxisType)
        throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      foreach (\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D elem in ((IEnumerable) this.\u0023\u003DzeckSod0\u003D.Elements).Cast<\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D>())
      {
        if (elem.TryGetXAxis() == null && this == this.\u0023\u003DzeckSod0\u003D.XAxises && zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Id == elem.XAxisId && !elem.CheckAxesCompatible(new ChartAxisType?(zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.AxisType), new ChartAxisType?()))
          throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
        if (elem.TryGetYAxis() == null && this == this.\u0023\u003DzeckSod0\u003D.YAxises && zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Id == elem.YAxisId && !elem.CheckAxesCompatible(new ChartAxisType?(), new ChartAxisType?(zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.AxisType)))
          throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      }
      if (this.\u0023\u003Dz2BfyUzmYpKwx() && StringHelper.IsEmpty(zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Group))
        zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Group = zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.AxisType.ToString() + zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Id;
      if (StringHelper.IsEmpty(zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Title))
        zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D.Title = str + num2.ToString();
      ((ChartAxis) zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D).ChartArea = (IChartArea) this.\u0023\u003DzeckSod0\u003D;
      return ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnAdding(zPKCmcad6Nxc5A8A.\u0023\u003DzNoL9aC4\u003D);
    }

    protected virtual bool OnRemoving(IChartAxis _param1)
    {
      ChartAxis chartAxis = (ChartAxis) _param1;
      bool flag = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).Contains((IChartAxis) chartAxis);
      if (flag && this.\u0023\u003DzeckSod0\u003D.Chart != null && CompareHelper.IsDefault<ChartAxis>(chartAxis))
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
      if (CompareHelper.IsDefault<ChartAxis>(chartAxis) && this.\u0023\u003DzeckSod0\u003D.Chart != null)
        throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
      int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnRemovingAt(_param1) ? 1 : 0;
      if (num == 0)
        return num != 0;
      chartAxis.ChartArea = (IChartArea) null;
      return num != 0;
    }

    protected virtual bool OnClearing()
    {
      if (this.\u0023\u003DzeckSod0\u003D.Chart != null)
        throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
      IChartAxis[] array = ((IEnumerable<IChartAxis>) this).ToArray<IChartAxis>();
      int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnClearing() ? 1 : 0;
      if (num == 0)
        return num != 0;
      CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) array, ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Action<IChartAxis>(ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzE70qt2sPjBBv095jMVMFSaY\u003D)));
      return num != 0;
    }

    [Serializable]
    private sealed class \u0023\u003Dz7qOdpi4\u003D
    {
      public static readonly ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartArea.\u0023\u003Dzc77TSXc\u003D.\u0023\u003Dz7qOdpi4\u003D();
      public static Action<IChartAxis> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

      internal void \u0023\u003DzE70qt2sPjBBv095jMVMFSaY\u003D(IChartAxis _param1)
      {
        ((ChartAxis) _param1).ChartArea = (IChartArea) null;
      }
    }

    private sealed class \u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D
    {
      public IChartAxis \u0023\u003DzNoL9aC4\u003D;

      internal bool \u0023\u003DzrnUOoaI6rwNWZtaLsg\u003D\u003D(IChartAxis _param1)
      {
        return _param1.Id == this.\u0023\u003DzNoL9aC4\u003D.Id;
      }
    }
  }

  private class \u0023\u003DztvVgmnUXBPgS<\u0023\u003DzH9HNkng\u003D> : 
    BaseList<\u0023\u003DzH9HNkng\u003D>,
    INotifyCollectionChanged,
    INotifyPropertyChanged
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003Dz6c10OEEYbDZc;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private \u0023\u003DzH9HNkng\u003D \u0023\u003DzJth_SLf0lYGy;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private PropertyChangedEventHandler \u0023\u003DziApqnpw\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private NotifyCollectionChangedEventHandler \u0023\u003DzLMHThYKh0KAF;

    protected virtual bool OnRemove(\u0023\u003DzH9HNkng\u003D _param1)
    {
      this.\u0023\u003Dz6c10OEEYbDZc = ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).IndexOf(_param1);
      return ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).OnRemove(_param1);
    }

    protected virtual void OnRemoved(\u0023\u003DzH9HNkng\u003D _param1)
    {
      if (this.\u0023\u003Dz6c10OEEYbDZc >= 0)
        this.\u0023\u003DzhnUiJs\u00243VfIz(NotifyCollectionChangedAction.Remove, _param1, this.\u0023\u003Dz6c10OEEYbDZc);
      ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).OnRemoved(_param1);
    }

    protected virtual void OnInserted(int _param1, \u0023\u003DzH9HNkng\u003D _param2)
    {
      this.\u0023\u003DzhnUiJs\u00243VfIz(NotifyCollectionChangedAction.Add, _param2, _param1);
      ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).OnInserted(_param1, _param2);
    }

    protected virtual void OnAdded(\u0023\u003DzH9HNkng\u003D _param1)
    {
      this.\u0023\u003DzhnUiJs\u00243VfIz(NotifyCollectionChangedAction.Add, _param1, ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).Count - 1);
      ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).OnAdded(_param1);
    }

    protected virtual void OnRemoveAt(int _param1)
    {
      this.\u0023\u003DzJth_SLf0lYGy = ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this)[_param1];
      base.OnRemoveAt(_param1);
    }

    protected virtual void OnRemovedAt(int _param1)
    {
      if ((object) this.\u0023\u003DzJth_SLf0lYGy != null)
        this.\u0023\u003DzhnUiJs\u00243VfIz(NotifyCollectionChangedAction.Remove, this.\u0023\u003DzJth_SLf0lYGy, _param1);
      ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).OnRemovedAt(_param1);
    }

    protected virtual void OnCleared()
    {
      this.\u0023\u003DzvLLUhiQ3_S8T();
      ((BaseCollection<\u0023\u003DzH9HNkng\u003D, IList<\u0023\u003DzH9HNkng\u003D>>) this).OnCleared();
    }

    private void \u0023\u003DzhnUiJs\u00243VfIz(
      NotifyCollectionChangedAction _param1,
      \u0023\u003DzH9HNkng\u003D _param2,
      int _param3)
    {
      this.\u0023\u003Dz0AXa0sp7IkH8(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433921));
      this.\u0023\u003Dz0AXa0sp7IkH8(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433933));
      NotifyCollectionChangedEventHandler zLmhThYkh0Kaf = this.\u0023\u003DzLMHThYKh0KAF;
      if (zLmhThYkh0Kaf == null)
        return;
      zLmhThYkh0Kaf((object) this, new NotifyCollectionChangedEventArgs(_param1, (object) _param2, _param3));
    }

    private void \u0023\u003DzvLLUhiQ3_S8T()
    {
      this.\u0023\u003Dz0AXa0sp7IkH8(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433921));
      this.\u0023\u003Dz0AXa0sp7IkH8(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433933));
      NotifyCollectionChangedEventHandler zLmhThYkh0Kaf = this.\u0023\u003DzLMHThYKh0KAF;
      if (zLmhThYkh0Kaf == null)
        return;
      zLmhThYkh0Kaf((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void \u0023\u003Dz0AXa0sp7IkH8(string _param1)
    {
      PropertyChangedEventHandler ziApqnpw = this.\u0023\u003DziApqnpw\u003D;
      if (ziApqnpw == null)
        return;
      ziApqnpw((object) this, new PropertyChangedEventArgs(_param1));
    }

    void INotifyPropertyChanged.\u0023\u003DzEv9LVlkJvUjy\u0024TeZl6vNKqTVYPst(
      PropertyChangedEventHandler _param1)
    {
      this.\u0023\u003DziApqnpw\u003D += _param1;
    }

    void INotifyPropertyChanged.\u0023\u003DzrlR5kckLtgr8uURxQFpsiL65kjNN(
      PropertyChangedEventHandler _param1)
    {
      this.\u0023\u003DziApqnpw\u003D -= _param1;
    }

    void INotifyCollectionChanged.\u0023\u003DzG2C70xprN5ERO4guauyqfAkQ0\u0024Uc(
      NotifyCollectionChangedEventHandler _param1)
    {
      this.\u0023\u003DzLMHThYKh0KAF += _param1;
    }

    void INotifyCollectionChanged.\u0023\u003DzLrYKMFBtFHAbhFefh7XiLD\u0024ANX6j(
      NotifyCollectionChangedEventHandler _param1)
    {
      this.\u0023\u003DzLMHThYKh0KAF -= _param1;
    }
  }
}
