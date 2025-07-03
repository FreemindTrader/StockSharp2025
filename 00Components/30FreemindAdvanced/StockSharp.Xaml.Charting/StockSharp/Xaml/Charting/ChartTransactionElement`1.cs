// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartTransactionElement`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// The base class that describes the Orders Or Trades chart element.
/// </summary>
/// <typeparam name="T">The element type.</typeparam>
public abstract class ChartTransactionElement<T> : 
  ChartElement<T>,
  IDrawableChartElement,
  IfxChartElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable,
  IChartTransactionElement
  where T : ChartTransactionElement<T>, new()
{
  
  private System.Windows.Media.Color \u0023\u003DzieAJJNZ68tP_;
  
  private System.Windows.Media.Color \u0023\u003DzGbPWR9A8cF4f;
  
  private System.Windows.Media.Color \u0023\u003Dzes2ibafgS30F;
  
  private System.Windows.Media.Color \u0023\u003Dzot9Qiz4J5vKT7sYMVQ\u003D\u003D;
  
  private bool \u0023\u003DzilRS6_jvjVLN;
  
  private double \u0023\u003Dzv0dodASrSZi6;
  
  private UIBaseVM \u0023\u003Dz2YSX_Z4\u003D;

  /// <summary>
  /// Initialize <see cref="T:StockSharp.Xaml.Charting.ChartTransactionElement`1" />.
  /// </summary>
  protected ChartTransactionElement()
  {
    this.BuyColor = this.BuyStrokeColor = Colors.Lime;
    this.SellColor = this.SellStrokeColor = Colors.HotPink;
  }

  /// <summary>Series header that will be shown on chart.</summary>
  [Browsable(false)]
  [Obsolete("Use FullTitle property.")]
  public string Title
  {
    get => this.FullTitle;
    set => this.FullTitle = value;
  }

  /// <summary>Color of graphics element on chart, indicating buy.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "BuyColor", Description = "BuyColorDesc", GroupName = "Style", Order = 30)]
  public System.Windows.Media.Color BuyColor
  {
    get => this.\u0023\u003DzieAJJNZ68tP_;
    set
    {
      if (this.\u0023\u003DzieAJJNZ68tP_ == value)
        return;
      this.\u0023\u003DzieAJJNZ68tP_ = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// Border color of graphics element on chart, indicating buy.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "BuyBorderColor", Description = "BuyBorderColorDesc", GroupName = "Style", Order = 40)]
  public System.Windows.Media.Color BuyStrokeColor
  {
    get => this.\u0023\u003DzGbPWR9A8cF4f;
    set
    {
      if (this.\u0023\u003DzGbPWR9A8cF4f == value)
        return;
      this.\u0023\u003DzGbPWR9A8cF4f = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>Color of graphics element on chart, indicating sell.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SellColor", Description = "SellColorDesc", GroupName = "Style", Order = 50)]
  public System.Windows.Media.Color SellColor
  {
    get => this.\u0023\u003Dzes2ibafgS30F;
    set
    {
      if (this.\u0023\u003Dzes2ibafgS30F == value)
        return;
      this.\u0023\u003Dzes2ibafgS30F = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>
  /// Border color of graphics element on chart, indicating sell.
  /// </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SellBorderColor", Description = "SellBorderColorDesc", GroupName = "Style", Order = 60)]
  public System.Windows.Media.Color SellStrokeColor
  {
    get => this.\u0023\u003Dzot9Qiz4J5vKT7sYMVQ\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dzot9Qiz4J5vKT7sYMVQ\u003D\u003D == value)
        return;
      this.\u0023\u003Dzot9Qiz4J5vKT7sYMVQ\u003D\u003D = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>Use alternative icons.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "UseAltIcon", Description = "UseAltIcon", GroupName = "Style", Order = 70)]
  public bool UseAltIcon
  {
    get => this.\u0023\u003DzilRS6_jvjVLN;
    set
    {
      if (this.\u0023\u003DzilRS6_jvjVLN == value)
        return;
      this.\u0023\u003DzilRS6_jvjVLN = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <summary>Draw size.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "DrawSize", Description = "DrawSize", GroupName = "Style", Order = 80 /*0x50*/)]
  public double DrawSize
  {
    get => this.\u0023\u003Dzv0dodASrSZi6;
    set
    {
      if (value < 0.0)
        throw new ArgumentOutOfRangeException("");
      if (Math.Abs(this.\u0023\u003Dzv0dodASrSZi6 - value) < 1E-05)
        return;
      this.\u0023\u003Dzv0dodASrSZi6 = value;
      this.RaisePropertyChanged("");
    }
  }

  /// <inheritdoc />
  protected override string GetGeneratedTitle()
  {
    Subscription subscription = this.TryGetSubscription();
    if (subscription == null)
      return (string) null;
    string elementTitleParams = LocalizedStrings.ChartTranElementTitleParams;
    object[] objArray = new object[2];
    SecurityId? securityId = subscription.SecurityId;
    ref SecurityId? local = ref securityId;
    string str;
    if (!local.HasValue)
    {
      str = (string) null;
    }
    else
    {
      SecurityId valueOrDefault = local.GetValueOrDefault();
      str = ((SecurityId) ref valueOrDefault).SecurityCode;
    }
    objArray[0] = (object) str;
    objArray[1] = (object) Extensions.GetDisplayName((ICustomAttributeProvider) ((object) this).GetType(), (string) null).ToLower();
    return StringHelper.Put(elementTitleParams, objArray);
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.BuyColor = storage.GetValue<int>("", this.BuyColor.ToInt()).ToColor();
    this.BuyStrokeColor = storage.GetValue<int>("", this.BuyStrokeColor.ToInt()).ToColor();
    this.SellColor = storage.GetValue<int>("", this.SellColor.ToInt()).ToColor();
    this.SellStrokeColor = storage.GetValue<int>("", this.SellStrokeColor.ToInt()).ToColor();
    this.UseAltIcon = storage.GetValue<bool>("", this.UseAltIcon);
    this.DrawSize = storage.GetValue<double>("", this.DrawSize);
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>("", this.BuyColor.ToInt());
    storage.SetValue<int>("", this.BuyStrokeColor.ToInt());
    storage.SetValue<int>("", this.SellColor.ToInt());
    storage.SetValue<int>("", this.SellStrokeColor.ToInt());
    storage.SetValue<bool>("", this.UseAltIcon);
    storage.SetValue<double>("", this.DrawSize);
  }

  internal override T Clone(T _param1)
  {
    _param1.BuyColor = this.BuyColor;
    _param1.BuyStrokeColor = this.BuyStrokeColor;
    _param1.SellColor = this.SellColor;
    _param1.SellStrokeColor = this.SellStrokeColor;
    _param1.UseAltIcon = this.UseAltIcon;
    _param1.DrawSize = this.DrawSize;
    return base.Clone(_param1);
  }

  System.Windows.Media.Color IDrawableChartElement.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return Colors.Transparent;
  }

  System.Drawing.Color IChartTransactionElement.BuyColor
  {
    get => this.BuyColor.FromWpf();
    set => this.BuyColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.BuyStrokeColor
  {
    get => this.BuyStrokeColor.FromWpf();
    set => this.BuyStrokeColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.SellColor
  {
    get => this.SellColor.FromWpf();
    set => this.SellColor = value.ToWpf();
  }

  System.Drawing.Color IChartTransactionElement.SellStrokeColor
  {
    get => this.SellStrokeColor.FromWpf();
    set => this.SellStrokeColor = value.ToWpf();
  }

  UIBaseVM IDrawableChartElement.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    ScichartSurfaceMVVM _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D = (UIBaseVM) new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>((T) this);
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
}
