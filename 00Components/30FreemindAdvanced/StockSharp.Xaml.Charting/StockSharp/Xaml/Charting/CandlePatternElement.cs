// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.CandlePatternElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>Candle pattern chart element.</summary>
public class CandlePatternElement : 
  ChartElement<CandlePatternElement>,
  \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X,
  \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color \u0023\u003DzPWHjilJVaIGi;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color \u0023\u003DzTWnsWqFC_c4o;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D \u0023\u003Dz2YSX_Z4\u003D;

  /// <summary>Down candle color.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Decrease", Description = "ColorOfDecreaseCandle", GroupName = "Style", Order = 30)]
  public Color DownColor
  {
    get => this.\u0023\u003DzPWHjilJVaIGi;
    set
    {
      this.\u0023\u003DzPWHjilJVaIGi = value;
      this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432591));
    }
  }

  /// <summary>Up candle color.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Increase", Description = "ColorOfIncreaseCandle", GroupName = "Style", Order = 31 /*0x1F*/)]
  public Color UpColor
  {
    get => this.\u0023\u003DzTWnsWqFC_c4o;
    set
    {
      this.\u0023\u003DzTWnsWqFC_c4o = value;
      this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432639));
    }
  }

  \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D = (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D) new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9Ur9WO7TFzBBD24ufNSokcCRpZmI_iRRi1f09FSCgNU6tg\u003D\u003D(this);
  }

  bool \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003DzJXDjnZfs8tGoFCupfSBAn4fwfCXfeCPpi\u0024rZmqxbRCtxRCyVSA\u003D\u003D(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D.\u0023\u003DzjgUUUJE\u003D(_param1);
  }

  void \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U8XPmuyOBlZpMiNagFIxa3issk4ACmj9rvI\u003D()
  {
    this.\u0023\u003Dz2YSX_Z4\u003D.\u0023\u003DzjgUUUJE\u003D(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data) => throw new NotSupportedException();

  Color \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return Colors.Transparent;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.DownColor = storage.GetValue<int>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432591), 0).ToColor();
    this.UpColor = storage.GetValue<int>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432639), 0).ToColor();
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432591), this.DownColor.ToInt());
    storage.SetValue<int>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432639), this.UpColor.ToInt());
  }
}
