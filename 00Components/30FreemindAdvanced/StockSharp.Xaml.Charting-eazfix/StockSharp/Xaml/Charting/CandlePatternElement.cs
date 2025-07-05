// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.CandlePatternElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class CandlePatternElement : 
  ChartElement<CandlePatternElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartComponent,
  IDrawableChartElement
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color _downColor;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color _upColor;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private UIChartBaseViewModel \u0023\u003Dz2YSX_Z4\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Decrease", Description = "ColorOfDecreaseCandle", GroupName = "Style", Order = 30)]
  public Color DownColor
  {
    get => this._downColor;
    set
    {
      this._downColor = value;
      this.RaisePropertyChanged(nameof (DownColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Increase", Description = "ColorOfIncreaseCandle", GroupName = "Style", Order = 31 /*0x1F*/)]
  public Color UpColor
  {
    get => this._upColor;
    set
    {
      this._upColor = value;
      this.RaisePropertyChanged(nameof (UpColor));
    }
  }

  UIChartBaseViewModel IDrawableChartElement.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    ScichartSurfaceMVVM _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D = (UIChartBaseViewModel) new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D(this);
  }

  bool IDrawableChartElement.\u0023\u003DzJXDjnZfs8tGoFCupfSBAn4fwfCXfeCPpi\u0024rZmqxbRCtxRCyVSA\u003D\u003D(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D.Draw(_param1);
  }

  void IDrawableChartElement.\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U8XPmuyOBlZpMiNagFIxa3issk4ACmj9rvI\u003D()
  {
    this.\u0023\u003Dz2YSX_Z4\u003D.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  protected override bool OnDraw(ChartDrawData data) => throw new NotSupportedException();

  Color IDrawableChartElement.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return Colors.Transparent;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.DownColor = storage.GetValue<int>("DownColor", 0).ToColor();
    this.UpColor = storage.GetValue<int>("UpColor", 0).ToColor();
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>("DownColor", this.DownColor.ToInt());
    storage.SetValue<int>("UpColor", this.UpColor.ToInt());
  }
}
