// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartActiveOrdersElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

[Display(ResourceType = typeof (LocalizedStrings), Name = "ActiveOrders")]
public class ChartActiveOrdersElement : 
  ChartElement<ChartActiveOrdersElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartActiveOrdersElement,
  IChartComponent,
  IDrawableChartElement
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzNB5fice33i0u;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzieAJJNZ68tP_;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzGxRjOLyGVgTOFE5tow\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzB7fwIKZYaT0OAwSREQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003Dzes2ibafgS30F;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzXNnisOcwXbVoIrOusg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzVB7x7oMoIiJX;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzeEJexoTVv0gn;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003Dzwpv8VkjqZubJ;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzzsiq00hapgVL;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private UIBaseVM \u0023\u003Dz2YSX_Z4\u003D;

  public ChartActiveOrdersElement()
  {
    this.SellColor = Colors.DarkRed;
    this.SellBlinkColor = System.Windows.Media.Color.FromRgb(byte.MaxValue, (byte) 151, (byte) 50);
    this.SellPendingColor = this.BuyPendingColor = Colors.Gray;
    this.BuyColor = Colors.DarkGreen;
    this.BuyBlinkColor = System.Windows.Media.Color.FromRgb((byte) 162, (byte) 204, (byte) 45);
    this.ForegroundColor = Colors.White;
    this.CancelButtonColor = Colors.Black;
    this.CancelButtonBackground = Colors.DarkGray;
    this.IsAnimationEnabled = true;
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "BuyPendingColor", Description = "BuyPendingColorDot", Order = 1)]
  public System.Windows.Media.Color BuyPendingColor
  {
    get => this.\u0023\u003DzNB5fice33i0u;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzNB5fice33i0u, value, nameof (BuyPendingColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "BuyColor", Description = "BuyColorDot", Order = 2)]
  public System.Windows.Media.Color BuyColor
  {
    get => this.\u0023\u003DzieAJJNZ68tP_;
    set => this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzieAJJNZ68tP_, value, nameof (BuyColor));
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "BuyBlinkColor", Description = "BuyBlinkColorDot", Order = 3)]
  public System.Windows.Media.Color BuyBlinkColor
  {
    get => this.\u0023\u003DzGxRjOLyGVgTOFE5tow\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzGxRjOLyGVgTOFE5tow\u003D\u003D, value, nameof (BuyBlinkColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SellPendingColor", Description = "SellPendingColorDot", Order = 4)]
  public System.Windows.Media.Color SellPendingColor
  {
    get => this.\u0023\u003DzB7fwIKZYaT0OAwSREQ\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzB7fwIKZYaT0OAwSREQ\u003D\u003D, value, nameof (SellPendingColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SellColor", Description = "SellColorDot", Order = 5)]
  public System.Windows.Media.Color SellColor
  {
    get => this.\u0023\u003Dzes2ibafgS30F;
    set => this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003Dzes2ibafgS30F, value, nameof (SellColor));
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SellBlinkColor", Description = "SellBlinkColorDot", Order = 6)]
  public System.Windows.Media.Color SellBlinkColor
  {
    get => this.\u0023\u003DzXNnisOcwXbVoIrOusg\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzXNnisOcwXbVoIrOusg\u003D\u003D, value, nameof (SellBlinkColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "CancelButtonColor", Description = "CancelButtonColorDot", Order = 7)]
  public System.Windows.Media.Color CancelButtonColor
  {
    get => this.\u0023\u003DzVB7x7oMoIiJX;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzVB7x7oMoIiJX, value, nameof (CancelButtonColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "CancelButtonBgColor", Description = "CancelButtonBgColorDot", Order = 8)]
  public System.Windows.Media.Color CancelButtonBackground
  {
    get => this.\u0023\u003DzeEJexoTVv0gn;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003DzeEJexoTVv0gn, value, nameof (CancelButtonBackground));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "FontColor", Description = "FontColorDot", Order = 9)]
  public System.Windows.Media.Color ForegroundColor
  {
    get => this.\u0023\u003Dzwpv8VkjqZubJ;
    set
    {
      this.SetField<System.Windows.Media.Color>(ref this.\u0023\u003Dzwpv8VkjqZubJ, value, nameof (ForegroundColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Animation", Description = "AnimationDot", Order = 10)]
  public bool IsAnimationEnabled
  {
    get => this.\u0023\u003Dzzsiq00hapgVL;
    set
    {
      this.SetField<bool>(ref this.\u0023\u003Dzzsiq00hapgVL, value, nameof (IsAnimationEnabled));
    }
  }

  System.Windows.Media.Color IDrawableChartElement.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return Colors.Transparent;
  }

  System.Drawing.Color IChartActiveOrdersElement.BuyPendingColor
  {
    get => this.BuyPendingColor.FromWpf();
    set => this.BuyPendingColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.BuyColor
  {
    get => this.BuyColor.FromWpf();
    set => this.BuyColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.BuyBlinkColor
  {
    get => this.BuyBlinkColor.FromWpf();
    set => this.BuyBlinkColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.SellPendingColor
  {
    get => this.SellPendingColor.FromWpf();
    set => this.SellPendingColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.SellColor
  {
    get => this.SellColor.FromWpf();
    set => this.SellColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.SellBlinkColor
  {
    get => this.SellBlinkColor.FromWpf();
    set => this.SellBlinkColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.CancelButtonColor
  {
    get => this.CancelButtonColor.FromWpf();
    set => this.CancelButtonColor = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.CancelButtonBackground
  {
    get => this.CancelButtonBackground.FromWpf();
    set => this.CancelButtonBackground = value.ToWpf();
  }

  System.Drawing.Color IChartActiveOrdersElement.ForegroundColor
  {
    get => this.ForegroundColor.FromWpf();
    set => this.ForegroundColor = value.ToWpf();
  }

  UIBaseVM IDrawableChartElement.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    ScichartSurfaceMVVM _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D = (UIBaseVM) new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiF4Ij4OLxcPJ6j\u0024MVq9JT52kmtoFstXIgXETlSaEaF89mw\u003D\u003D(this);
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

  protected override bool OnDraw(ChartDrawData data)
  {
    List<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D> source = data.\u0023\u003DzaZ5Qc3xeNY95((IChartActiveOrdersElement) this);
    return source != null && !CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>((ICollection<ChartDrawData.\u0023\u003Dzz3K4Ek4jWvUOemvcOQ\u003D\u003D>) source) && ((IDrawableChartElement) this).StartDrawing(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source.Cast<ChartDrawData.IDrawValue>(), source.Count));
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.BuyColor = storage.GetValue<int>("BuyColor", 0).ToColor();
    this.BuyBlinkColor = storage.GetValue<int>("BuyBlinkColor", 0).ToColor();
    this.BuyPendingColor = storage.GetValue<int>("BuyPendingColor", 0).ToColor();
    this.SellColor = storage.GetValue<int>("SellColor", 0).ToColor();
    this.SellBlinkColor = storage.GetValue<int>("SellBlinkColor", 0).ToColor();
    this.SellPendingColor = storage.GetValue<int>("SellPendingColor", 0).ToColor();
    this.ForegroundColor = storage.GetValue<int>("ForegroundColor", 0).ToColor();
    this.CancelButtonColor = storage.GetValue<int>("CancelButtonColor", 0).ToColor();
    this.CancelButtonBackground = storage.GetValue<int>("CancelButtonBackground", 0).ToColor();
    this.IsAnimationEnabled = storage.GetValue<bool>("IsAnimationEnabled", false);
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.Set<int>("BuyColor", this.BuyColor.ToInt()).Set<int>("BuyBlinkColor", this.BuyBlinkColor.ToInt()).Set<int>("BuyPendingColor", this.BuyPendingColor.ToInt()).Set<int>("SellColor", this.SellColor.ToInt()).Set<int>("SellBlinkColor", this.SellBlinkColor.ToInt()).Set<int>("SellPendingColor", this.SellPendingColor.ToInt()).Set<int>("CancelButtonColor", this.CancelButtonColor.ToInt()).Set<int>("ForegroundColor", this.ForegroundColor.ToInt()).Set<int>("CancelButtonBackground", this.CancelButtonBackground.ToInt()).Set<bool>("IsAnimationEnabled", this.IsAnimationEnabled);
  }

  internal override ChartActiveOrdersElement CopyTo(
    ChartActiveOrdersElement _param1)
  {
    _param1 = base.CopyTo(_param1);
    _param1.BuyColor = this.BuyColor;
    _param1.SellColor = this.SellColor;
    _param1.CancelButtonColor = this.CancelButtonColor;
    _param1.ForegroundColor = this.ForegroundColor;
    _param1.CancelButtonBackground = this.CancelButtonBackground;
    _param1.BuyPendingColor = this.BuyPendingColor;
    _param1.SellPendingColor = this.SellPendingColor;
    _param1.IsAnimationEnabled = this.IsAnimationEnabled;
    return _param1;
  }
}
