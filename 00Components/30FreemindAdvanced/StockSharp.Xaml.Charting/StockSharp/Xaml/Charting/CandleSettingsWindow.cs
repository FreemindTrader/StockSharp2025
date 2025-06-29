// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.CandleSettingsWindow
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// The window for edit <see cref="T:StockSharp.Algo.Candles.CandleSeries" />.
/// </summary>
/// <summary>CandleSettingsWindow</summary>
public class CandleSettingsWindow : ThemedWindow, IComponentConnector
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Subscription \u0023\u003Dzh4DsglU\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal PropertyGridEx \u0023\u003DzSBQp\u0024Vhxv74c;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal SimpleButton \u0023\u003DzR8YHGiJOGNl5;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.CandleSettingsWindow" />.
  /// </summary>
  public CandleSettingsWindow() => this.InitializeComponent();

  /// <summary>Candles settings.</summary>
  public Subscription Subscription
  {
    get => this.\u0023\u003Dzh4DsglU\u003D;
    set
    {
      this.\u0023\u003Dzh4DsglU\u003D = value;
      this.\u0023\u003DzSBQp\u0024Vhxv74c.SelectedObject = value == null ? (object) (CandleSettingsWindow.\u0023\u003DzYlNSvXB2xb3ElCeVJA\u003D\u003D) null : (object) new CandleSettingsWindow.\u0023\u003DzYlNSvXB2xb3ElCeVJA\u003D\u003D(value);
      this.\u0023\u003DzR8YHGiJOGNl5.IsEnabled = value != null;
    }
  }

  /// <summary>The provider of information about instruments.</summary>
  public ISecurityProvider SecurityProvider
  {
    get => this.\u0023\u003DzSBQp\u0024Vhxv74c.SecurityProvider;
    set => this.\u0023\u003DzSBQp\u0024Vhxv74c.SecurityProvider = value;
  }

  private void \u0023\u003DzdcCnJSj8d2oz(object _param1, RoutedEventArgs _param2)
  {
    if (!this.Subscription.SecurityId.HasValue)
    {
      int num1 = (int) new MessageBoxBuilder().Owner((Window) this).Error().Text(LocalizedStrings.SecurityNotSpecified).Show();
    }
    else
    {
      MarketDataMessage marketData = this.Subscription.MarketData;
      DateTimeOffset? from = marketData.From;
      DateTimeOffset? to = marketData.To;
      if ((from.HasValue & to.HasValue ? (from.GetValueOrDefault() > to.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        int num2 = (int) new MessageBoxBuilder().Owner((Window) this).Error().Text(StringHelper.Put(LocalizedStrings.StartCannotBeMoreEnd, new object[2]
        {
          (object) marketData.From,
          (object) marketData.To
        })).Show();
      }
      else
        this.DialogResult = new bool?(true);
    }
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432648), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 1)
    {
      if (connectionId == 2)
      {
        this.\u0023\u003DzR8YHGiJOGNl5 = (SimpleButton) target;
        this.\u0023\u003DzR8YHGiJOGNl5.Click += new RoutedEventHandler(this.\u0023\u003DzdcCnJSj8d2oz);
      }
      else
        this.\u0023\u003DzQGCmQMjHdLKS = true;
    }
    else
      this.\u0023\u003DzSBQp\u0024Vhxv74c = (PropertyGridEx) target;
  }

  private sealed class \u0023\u003DzYlNSvXB2xb3ElCeVJA\u003D\u003D : NotifiableObject
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly MarketDataMessage \u0023\u003Dz7LtG0SkMrvA7;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Security \u0023\u003DzpaXnuR8\u003D;

    public \u0023\u003DzYlNSvXB2xb3ElCeVJA\u003D\u003D(Subscription _param1)
    {
      this.\u0023\u003Dz7LtG0SkMrvA7 = _param1 != null ? _param1.MarketData : throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432617));
      DateTimeOffset? nullable = this.\u0023\u003Dz7LtG0SkMrvA7.From;
      if (nullable.HasValue)
        return;
      nullable = this.\u0023\u003Dz7LtG0SkMrvA7.To;
      if (nullable.HasValue)
        return;
      this.\u0023\u003Dz7LtG0SkMrvA7.From = new DateTimeOffset?((DateTimeOffset) DateTime.Today.Subtract(TimeSpan.FromDays(10.0)));
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "CandlesType", Description = "CandlesType", GroupName = "General", Order = 1)]
    [Editor(typeof (ICandleDataTypeEditor), typeof (ICandleDataTypeEditor))]
    public DataType CandleType
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.DataType2;
      set
      {
        this.\u0023\u003Dz7LtG0SkMrvA7.DataType2 = value;
        if ((value != null ? (!Extensions.IsBuildOnly(value.MessageType) ? 1 : 0) : 1) != 0)
          return;
        this.BuildMode = (MarketDataBuildModes) 2;
        this.NotifyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432664));
      }
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Security", Description = "SecurityDot", GroupName = "General", Order = 0)]
    public Security Security
    {
      get => this.\u0023\u003DzpaXnuR8\u003D;
      set
      {
        this.\u0023\u003DzpaXnuR8\u003D = value;
        if (value == null)
          return;
        ((BaseSubscriptionIdMessage<SecurityMessage>) value.ToMessage()).CopyTo((SecurityMessage) this.\u0023\u003Dz7LtG0SkMrvA7);
      }
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "VolumeProfile", Description = "VolumeProfileCalc", GroupName = "General", Order = 2)]
    public bool IsCalcVolumeProfile
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.IsCalcVolumeProfile;
      set
      {
        this.\u0023\u003Dz7LtG0SkMrvA7.IsCalcVolumeProfile = value;
        if (!value)
          return;
        this.BuildMode = (MarketDataBuildModes) 2;
        this.NotifyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539432664));
      }
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "From", Description = "StartDateDesc", GroupName = "General", Order = 3)]
    public DateTimeOffset? From
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.From;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.From = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Until", Description = "ToDateDesc", GroupName = "General", Order = 4)]
    public DateTimeOffset? To
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.To;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.To = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "SmallerTimeFrame", Description = "SmallerTimeFrameDesc", GroupName = "General", Order = 5)]
    public bool AllowBuildFromSmallerTimeFrame
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.AllowBuildFromSmallerTimeFrame;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.AllowBuildFromSmallerTimeFrame = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "RegularHours", Description = "RegularTradingHours", GroupName = "General", Order = 6)]
    public bool? IsRegularTradingHours
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.IsRegularTradingHours;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.IsRegularTradingHours = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Count", Description = "CandlesCount", GroupName = "General", Order = 7)]
    public long? Count
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.Count;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.Count = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Mode", Description = "BuildMode", GroupName = "Build", Order = 20)]
    public MarketDataBuildModes BuildMode
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.BuildMode;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.BuildMode = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Source", Description = "CandlesBuildSource", GroupName = "Build", Order = 21)]
    [ItemsSource(typeof (BuildCandlesFromSource))]
    public DataType BuildFrom
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.BuildFrom;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.BuildFrom = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Field", Description = "Level1Field", GroupName = "Build", Order = 22)]
    [ItemsSource(typeof (BuildCandlesFieldSource))]
    public Level1Fields? BuildField
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.BuildField;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.BuildField = value;
    }

    [Display(ResourceType = typeof (LocalizedStrings), Name = "Finished", Description = "Finished", GroupName = "Build", Order = 23)]
    public bool IsFinishedOnly
    {
      get => this.\u0023\u003Dz7LtG0SkMrvA7.IsFinishedOnly;
      set => this.\u0023\u003Dz7LtG0SkMrvA7.IsFinishedOnly = value;
    }
  }
}
