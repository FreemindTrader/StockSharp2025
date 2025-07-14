// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartVolatilitySmileElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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

public class ChartVolatilitySmileElement : 
  ChartComponent<ChartVolatilitySmileElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartVolatilitySmileElement
{
  
  private readonly ChartLineElement _childViewModels;
  
  private readonly ChartLineElement \u0023\u003DzdBxDEgUJtz0N;

  public ChartVolatilitySmileElement()
  {
    this._childViewModels = new ChartLineElement()
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

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SourceValues", Description = "SourceValues")]
  public IChartLineElement Values => (IChartLineElement) this._childViewModels;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "VolatilitySmile", Description = "VolatilitySmile")]
  public IChartLineElement Smile => (IChartLineElement) this.\u0023\u003DzdBxDEgUJtz0N;

  public override bool CheckAxesCompatible(ChartAxisType? xType, ChartAxisType? yType)
  {
    return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
  }

  protected override bool OnDraw(ChartDrawData data)
  {
    return (0 | (((IChartComponent) this.Values).Draw(data) ? 1 : 0) | (((IChartComponent) this.Smile).Draw(data) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    if (((SynchronizedDictionary<string, object>) storage).ContainsKey("Values"))
    {
      this.RemoveChildElement((IChartElement) this.Values);
      PersistableHelper.Load((IPersistable) this.Values, storage, "Values");
      this.AddChildElement((IChartElement) this.Values);
    }
    if (!((SynchronizedDictionary<string, object>) storage).ContainsKey("Smile"))
      return;
    this.RemoveChildElement((IChartElement) this.Smile);
    PersistableHelper.Load((IPersistable) this.Smile, storage, "Smile");
    this.AddChildElement((IChartElement) this.Smile);
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Values", PersistableHelper.Save((IPersistable) this.Values));
    storage.SetValue<SettingsStorage>("Smile", PersistableHelper.Save((IPersistable) this.Smile));
  }

  internal override ChartVolatilitySmileElement Clone(
    ChartVolatilitySmileElement _param1)
  {
    _param1 = base.Clone(_param1);
    this._childViewModels.Clone(_param1._childViewModels);
    this.\u0023\u003DzdBxDEgUJtz0N.Clone(_param1.\u0023\u003DzdBxDEgUJtz0N);
    return _param1;
  }
}
