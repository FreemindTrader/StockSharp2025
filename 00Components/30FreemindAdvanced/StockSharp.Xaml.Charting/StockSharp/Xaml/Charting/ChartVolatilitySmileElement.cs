// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartVolatilitySmileElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing a volatility smile.</summary>
public class ChartVolatilitySmileElement : 
  ChartElement<ChartVolatilitySmileElement>,
  IChartVolatilitySmileElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ChartLineElement \u0023\u003Dz10jqvLI\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ChartLineElement \u0023\u003DzdBxDEgUJtz0N;

  /// <summary>Create instance.</summary>
  public ChartVolatilitySmileElement()
  {
    this.\u0023\u003Dz10jqvLI\u003D = new ChartLineElement()
    {
      Color = Colors.DarkGreen,
      AdditionalColor = Colors.DarkGreen.ToTransparent((byte) 50)
    };
    this.\u0023\u003DzdBxDEgUJtz0N = new ChartLineElement()
    {
      Color = Colors.DarkGreen,
      AdditionalColor = Colors.DarkGreen.ToTransparent((byte) 50)
    };
    this.AddChildElement((IChartElement) this.Values);
    this.AddChildElement((IChartElement) this.Smile);
  }

  /// <inheritdoc />
  /// .
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SourceValues", Description = "SourceValues")]
  public IChartLineElement Values => (IChartLineElement) this.\u0023\u003Dz10jqvLI\u003D;

  /// <inheritdoc />
  /// .
  [Display(ResourceType = typeof (LocalizedStrings), Name = "VolatilitySmile", Description = "VolatilitySmile")]
  public IChartLineElement Smile => (IChartLineElement) this.\u0023\u003DzdBxDEgUJtz0N;

  /// <inheritdoc />
  public override bool CheckAxesCompatible(ChartAxisType? xType, ChartAxisType? yType)
  {
    return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
  }

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    return (0 | (((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.Values).\u0023\u003DzjgUUUJE\u003D(data) ? 1 : 0) | (((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.Smile).\u0023\u003DzjgUUUJE\u003D(data) ? 1 : 0)) != 0;
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    if (((SynchronizedDictionary<string, object>) storage).ContainsKey(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430006)))
    {
      this.RemoveChildElement((IChartElement) this.Values);
      PersistableHelper.Load((IPersistable) this.Values, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430006));
      this.AddChildElement((IChartElement) this.Values);
    }
    if (!((SynchronizedDictionary<string, object>) storage).ContainsKey(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539429991)))
      return;
    this.RemoveChildElement((IChartElement) this.Smile);
    PersistableHelper.Load((IPersistable) this.Smile, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539429991));
    this.AddChildElement((IChartElement) this.Smile);
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430006), PersistableHelper.Save((IPersistable) this.Values));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539429991), PersistableHelper.Save((IPersistable) this.Smile));
  }

  internal override ChartVolatilitySmileElement \u0023\u003Dz3MbNd8U\u003D(
    ChartVolatilitySmileElement _param1)
  {
    _param1 = base.\u0023\u003Dz3MbNd8U\u003D(_param1);
    this.\u0023\u003Dz10jqvLI\u003D.\u0023\u003Dz3MbNd8U\u003D(_param1.\u0023\u003Dz10jqvLI\u003D);
    this.\u0023\u003DzdBxDEgUJtz0N.\u0023\u003Dz3MbNd8U\u003D(_param1.\u0023\u003DzdBxDEgUJtz0N);
    return _param1;
  }
}
