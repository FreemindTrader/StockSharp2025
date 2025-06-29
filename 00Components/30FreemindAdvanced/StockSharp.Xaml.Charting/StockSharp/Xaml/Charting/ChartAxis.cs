// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartAxis
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart axis.</summary>
[TypeConverter(typeof (ExpandableObjectConverter))]
public class ChartAxis : 
  NotifiableObject,
  IChartAxis,
  IPersistable,
  INotifyPropertyChangedEx,
  INotifyPropertyChanged,
  INotifyPropertyChanging
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IChartArea \u0023\u003DzDlmrofv0iAzdkUselsApTvI\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DziM6zJqE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzVf6ckz06jjnq = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DzIzRA1GQ\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DznLJWjME\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzv9FD1di4RC0T;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ChartAxisType \u0023\u003DzQbjuL1gVekSH;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzdLTpPeMhlR6N = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzrhLICZ53UOoE;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzMoUHwSZ26s\u0024B = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQX_eCV9XBE2Om3pH5A\u003D\u003D = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzomI8jgXvMYgj;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzHLFKxy_IJ02Bf4vYiQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzgTDrusYEVex\u0024 = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003Dz5wsolu6uzY0L;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003Dz6SqmY60jwVYP;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DzqXX3UNR0keSIvzpxvA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private TimeZoneInfo \u0023\u003Dzlm3nxP8O1U6I;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzDtp\u00246FfBRlYq = double.NaN;

  /// <inheritdoc />
  [Browsable(false)]
  public IChartArea ChartArea
  {
    get => this.\u0023\u003DzDlmrofv0iAzdkUselsApTvI\u003D;
    internal set => this.\u0023\u003DzDlmrofv0iAzdkUselsApTvI\u003D = value;
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Identifier", Description = "UniqueId", GroupName = "Parameters", Order = 10)]
  [Browsable(false)]
  public string Id
  {
    get => this.\u0023\u003DziM6zJqE\u003D;
    set
    {
      this.\u0023\u003DziM6zJqE\u003D = value;
      this.NotifyChanged(XXX.SSS(-539431122));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Show", Description = "ShowDot", GroupName = "Parameters", Order = 15)]
  public bool IsVisible
  {
    get => this.\u0023\u003DzVf6ckz06jjnq;
    set
    {
      this.\u0023\u003DzVf6ckz06jjnq = value;
      this.NotifyChanged(XXX.SSS(-539433813));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Header", Description = "AxisHeader", GroupName = "Parameters", Order = 20)]
  public string Title
  {
    get => this.\u0023\u003DzIzRA1GQ\u003D;
    set
    {
      this.\u0023\u003DzIzRA1GQ\u003D = value;
      this.NotifyChanged(XXX.SSS(-539431078));
    }
  }

  /// <inheritdoc />
  [Browsable(false)]
  public string Group
  {
    get => this.\u0023\u003DznLJWjME\u003D;
    set
    {
      if (this.\u0023\u003DznLJWjME\u003D == value)
        return;
      if (this.ChartArea != null)
        throw new InvalidOperationException(LocalizedStrings.ErrorChangingGroupName);
      this.\u0023\u003DznLJWjME\u003D = value;
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SwitchAxisLocation", Description = "SwitchAxisLocation", GroupName = "Parameters", Order = 40)]
  public bool SwitchAxisLocation
  {
    get => this.\u0023\u003Dzv9FD1di4RC0T;
    set
    {
      this.\u0023\u003Dzv9FD1di4RC0T = value;
      this.NotifyChanged(XXX.SSS(-539433797));
    }
  }

  /// <inheritdoc />
  [Browsable(false)]
  public ChartAxisType AxisType
  {
    get => this.\u0023\u003DzQbjuL1gVekSH;
    set
    {
      if (this.\u0023\u003DzQbjuL1gVekSH != value && this.ChartArea != null)
        throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      this.\u0023\u003DzQbjuL1gVekSH = value;
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "AutoRange", Description = "AutoRangeDot", GroupName = "Parameters", Order = 60)]
  public bool AutoRange
  {
    get => this.\u0023\u003DzdLTpPeMhlR6N;
    set
    {
      this.\u0023\u003DzdLTpPeMhlR6N = value;
      this.NotifyChanged(XXX.SSS(-539433854));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "FlipCoords", Description = "FlipCoordsDot", GroupName = "Parameters", Order = 70)]
  public bool FlipCoordinates
  {
    get => this.\u0023\u003DzrhLICZ53UOoE;
    set
    {
      this.\u0023\u003DzrhLICZ53UOoE = value;
      this.NotifyChanged(XXX.SSS(-539433838));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LinesOnAxis", Description = "MainGridLinesOnAxis", GroupName = "Parameters", Order = 80 /*0x50*/)]
  public bool DrawMajorTicks
  {
    get => this.\u0023\u003DzMoUHwSZ26s\u0024B;
    set
    {
      this.\u0023\u003DzMoUHwSZ26s\u0024B = value;
      this.NotifyChanged(XXX.SSS(-539433096));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "GridLines", Description = "ShowMainGridLines", GroupName = "Parameters", Order = 90)]
  public bool DrawMajorGridLines
  {
    get => this.\u0023\u003DzQX_eCV9XBE2Om3pH5A\u003D\u003D;
    set
    {
      this.\u0023\u003DzQX_eCV9XBE2Om3pH5A\u003D\u003D = value;
      this.NotifyChanged(XXX.SSS(-539433145));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "ExtraLinesOnAxis", Description = "ShowExtraLinesOnAxis", GroupName = "Parameters", Order = 100)]
  public bool DrawMinorTicks
  {
    get => this.\u0023\u003DzomI8jgXvMYgj;
    set
    {
      this.\u0023\u003DzomI8jgXvMYgj = value;
      this.NotifyChanged(XXX.SSS(-539433170));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "ExtraGridLines", Description = "ShowExtraGridLines", GroupName = "Parameters", Order = 110)]
  public bool DrawMinorGridLines
  {
    get => this.\u0023\u003DzHLFKxy_IJ02Bf4vYiQ\u003D\u003D;
    set
    {
      this.\u0023\u003DzHLFKxy_IJ02Bf4vYiQ\u003D\u003D = value;
      this.NotifyChanged(XXX.SSS(-539433163));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "AxisLabels", Description = "ShowAxisLabels", GroupName = "Parameters", Order = 120)]
  public bool DrawLabels
  {
    get => this.\u0023\u003DzgTDrusYEVex\u0024;
    set
    {
      this.\u0023\u003DzgTDrusYEVex\u0024 = value;
      this.NotifyChanged(XXX.SSS(-539433188));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LabelsFormat", Description = "LabelsFormatDot", GroupName = "Parameters", Order = 130)]
  public string TextFormatting
  {
    get
    {
      string z5wsolu6uzY0L = this.\u0023\u003Dz5wsolu6uzY0L;
      if (z5wsolu6uzY0L != null)
        return z5wsolu6uzY0L;
      return this.AxisType == ChartAxisType.Numeric ? XXX.SSS(-539432977) : XXX.SSS(-539432967);
    }
    set
    {
      this.\u0023\u003Dz5wsolu6uzY0L = value;
      this.NotifyChanged(XXX.SSS(-539432975));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Cursor", Description = "CursorTextFormat", GroupName = "Parameters", Order = 131)]
  public string CursorTextFormatting
  {
    get
    {
      string z6SqmY60jwVyp = this.\u0023\u003Dz6SqmY60jwVYP;
      if (z6SqmY60jwVyp != null)
        return z6SqmY60jwVyp;
      return this.AxisType == ChartAxisType.Numeric ? XXX.SSS(-539432996) : XXX.SSS(-539432967);
    }
    set
    {
      this.\u0023\u003Dz6SqmY60jwVYP = value;
      this.NotifyChanged(XXX.SSS(-539433005));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LabelsFormatIntraday", Description = "LabelsFormatIntradayDescDot", GroupName = "Parameters", Order = 132)]
  public string SubDayTextFormatting
  {
    get
    {
      return this.\u0023\u003DzqXX3UNR0keSIvzpxvA\u003D\u003D ?? XXX.SSS(-539433036);
    }
    set
    {
      this.\u0023\u003DzqXX3UNR0keSIvzpxvA\u003D\u003D = value;
      this.NotifyChanged(XXX.SSS(-539433076));
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "TimeZone", Description = "TimeZoneDot", GroupName = "Parameters", Order = 133)]
  public TimeZoneInfo TimeZone
  {
    get => this.\u0023\u003Dzlm3nxP8O1U6I;
    set
    {
      this.\u0023\u003Dzlm3nxP8O1U6I = value;
      this.NotifyChanged(XXX.SSS(-539432434));
    }
  }

  /// <summary>Current data point with for X axis.</summary>
  [Browsable(false)]
  public double DataPointWidth
  {
    get => this.\u0023\u003DzDtp\u00246FfBRlYq;
    internal set
    {
      if (Math.Abs(this.\u0023\u003DzDtp\u00246FfBRlYq - value) < 0.3)
        return;
      this.\u0023\u003DzDtp\u00246FfBRlYq = value;
      this.NotifyChanged(XXX.SSS(-539433071));
    }
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Load(SettingsStorage storage)
  {
    this.Id = storage.GetValue<string>(XXX.SSS(-539431122), (string) null) ?? storage.GetValue<string>(XXX.SSS(-539433348), (string) null);
    this.Title = storage.GetValue<string>(XXX.SSS(-539431078), (string) null);
    this.IsVisible = storage.GetValue<bool>(XXX.SSS(-539433813), this.IsVisible);
    this.Group = storage.GetValue<string>(XXX.SSS(-539433359), this.Group);
    this.AutoRange = storage.GetValue<bool>(XXX.SSS(-539433854), this.AutoRange);
    this.DrawMinorTicks = storage.GetValue<bool>(XXX.SSS(-539433170), this.DrawMinorTicks);
    this.DrawMajorTicks = storage.GetValue<bool>(XXX.SSS(-539433096), this.DrawMajorTicks);
    this.DrawMajorGridLines = storage.GetValue<bool>(XXX.SSS(-539433145), this.DrawMajorGridLines);
    this.DrawMinorGridLines = storage.GetValue<bool>(XXX.SSS(-539433163), this.DrawMinorGridLines);
    this.DrawLabels = storage.GetValue<bool>(XXX.SSS(-539433188), this.DrawLabels);
    this.TextFormatting = storage.GetValue<string>(XXX.SSS(-539432975), this.TextFormatting);
    this.CursorTextFormatting = storage.GetValue<string>(XXX.SSS(-539433005), this.CursorTextFormatting);
    this.SubDayTextFormatting = storage.GetValue<string>(XXX.SSS(-539433076), this.SubDayTextFormatting);
    this.SwitchAxisLocation = storage.GetValue<bool>(XXX.SSS(-539433797), this.SwitchAxisLocation);
    this.AxisType = storage.GetValue<ChartAxisType>(XXX.SSS(-539433403), ChartAxisType.DateTime);
    this.TimeZone = Converter.To<TimeZoneInfo>((object) storage.GetValue<string>(XXX.SSS(-539432434), (string) null)) ?? TimeZoneInfo.Local;
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Save(SettingsStorage storage)
  {
    storage.SetValue<string>(XXX.SSS(-539431122), this.Id);
    storage.SetValue<string>(XXX.SSS(-539431078), this.Title);
    storage.SetValue<bool>(XXX.SSS(-539433813), this.IsVisible);
    storage.SetValue<string>(XXX.SSS(-539433359), this.Group);
    storage.SetValue<bool>(XXX.SSS(-539433854), this.AutoRange);
    storage.SetValue<bool>(XXX.SSS(-539433170), this.DrawMinorTicks);
    storage.SetValue<bool>(XXX.SSS(-539433096), this.DrawMajorTicks);
    storage.SetValue<bool>(XXX.SSS(-539433145), this.DrawMajorGridLines);
    storage.SetValue<bool>(XXX.SSS(-539433163), this.DrawMinorGridLines);
    storage.SetValue<bool>(XXX.SSS(-539433188), this.DrawLabels);
    storage.SetValue<string>(XXX.SSS(-539432975), this.TextFormatting);
    storage.SetValue<string>(XXX.SSS(-539433005), this.CursorTextFormatting);
    storage.SetValue<string>(XXX.SSS(-539433076), this.SubDayTextFormatting);
    storage.SetValue<bool>(XXX.SSS(-539433797), this.SwitchAxisLocation);
    storage.SetValue<ChartAxisType>(XXX.SSS(-539433403), this.AxisType);
    storage.SetValue<string>(XXX.SSS(-539432434), this.TimeZone?.Id);
  }
}
