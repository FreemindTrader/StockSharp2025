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
  
  private IChartArea _chartArea;
  
  private string _id;
  
  private bool _isVisible = true;
  
  private string _title;
  
  private string _group;
  
  private bool _switchAxisLocation;
  
  private ChartAxisType _axisType;
  
  private bool _autoRange = true;
  
  private bool _flipCoordinates;
  
  private bool _drawMajorTicks = true;
  
  private bool _drawMajorGridLines = true;
  
  private bool _drawMinorTicks;
  
  private bool _drawMinorGridLines;
  
  private bool _drawLabels = true;
  
  private string _textFormatting;
  
  private string _cursorTextFormatting;
  
  private string _subDayTextFormatting;
  
  private TimeZoneInfo _timeZone;
  
  private double _dataPointWidth = double.NaN;

  /// <inheritdoc />
  [Browsable(false)]
  public IChartArea ChartArea
  {
    get => this._chartArea;
    internal set => this._chartArea = value;
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Identifier", Description = "UniqueId", GroupName = "Parameters", Order = 10)]
  [Browsable(false)]
  public string Id
  {
    get => this._id;
    set
    {
      this._id = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Show", Description = "ShowDot", GroupName = "Parameters", Order = 15)]
  public bool IsVisible
  {
    get => this._isVisible;
    set
    {
      this._isVisible = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Header", Description = "AxisHeader", GroupName = "Parameters", Order = 20)]
  public string Title
  {
    get => this._title;
    set
    {
      this._title = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Browsable(false)]
  public string Group
  {
    get => this._group;
    set
    {
      if (this._group == value)
        return;
      if (this.ChartArea != null)
        throw new InvalidOperationException(LocalizedStrings.ErrorChangingGroupName);
      this._group = value;
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SwitchAxisLocation", Description = "SwitchAxisLocation", GroupName = "Parameters", Order = 40)]
  public bool SwitchAxisLocation
  {
    get => this._switchAxisLocation;
    set
    {
      this._switchAxisLocation = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Browsable(false)]
  public ChartAxisType AxisType
  {
    get => this._axisType;
    set
    {
      if (this._axisType != value && this.ChartArea != null)
        throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
      this._axisType = value;
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "AutoRange", Description = "AutoRangeDot", GroupName = "Parameters", Order = 60)]
  public bool AutoRange
  {
    get => this._autoRange;
    set
    {
      this._autoRange = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "FlipCoords", Description = "FlipCoordsDot", GroupName = "Parameters", Order = 70)]
  public bool FlipCoordinates
  {
    get => this._flipCoordinates;
    set
    {
      this._flipCoordinates = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LinesOnAxis", Description = "MainGridLinesOnAxis", GroupName = "Parameters", Order = 80 /*0x50*/)]
  public bool DrawMajorTicks
  {
    get => this._drawMajorTicks;
    set
    {
      this._drawMajorTicks = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "GridLines", Description = "ShowMainGridLines", GroupName = "Parameters", Order = 90)]
  public bool DrawMajorGridLines
  {
    get => this._drawMajorGridLines;
    set
    {
      this._drawMajorGridLines = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "ExtraLinesOnAxis", Description = "ShowExtraLinesOnAxis", GroupName = "Parameters", Order = 100)]
  public bool DrawMinorTicks
  {
    get => this._drawMinorTicks;
    set
    {
      this._drawMinorTicks = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "ExtraGridLines", Description = "ShowExtraGridLines", GroupName = "Parameters", Order = 110)]
  public bool DrawMinorGridLines
  {
    get => this._drawMinorGridLines;
    set
    {
      this._drawMinorGridLines = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "AxisLabels", Description = "ShowAxisLabels", GroupName = "Parameters", Order = 120)]
  public bool DrawLabels
  {
    get => this._drawLabels;
    set
    {
      this._drawLabels = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LabelsFormat", Description = "LabelsFormatDot", GroupName = "Parameters", Order = 130)]
  public string TextFormatting
  {
    get
    {
      string z5wsolu6uzY0L = this._textFormatting;
      if (z5wsolu6uzY0L != null)
        return z5wsolu6uzY0L;
      return this.AxisType == ChartAxisType.Numeric ? "" : "";
    }
    set
    {
      this._textFormatting = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Cursor", Description = "CursorTextFormat", GroupName = "Parameters", Order = 131)]
  public string CursorTextFormatting
  {
    get
    {
      string z6SqmY60jwVyp = this._cursorTextFormatting;
      if (z6SqmY60jwVyp != null)
        return z6SqmY60jwVyp;
      return this.AxisType == ChartAxisType.Numeric ? "" : "";
    }
    set
    {
      this._cursorTextFormatting = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "LabelsFormatIntraday", Description = "LabelsFormatIntradayDescDot", GroupName = "Parameters", Order = 132)]
  public string SubDayTextFormatting
  {
    get
    {
      return this._subDayTextFormatting ?? "";
    }
    set
    {
      this._subDayTextFormatting = value;
      this.NotifyChanged("");
    }
  }

  /// <inheritdoc />
  [Display(ResourceType = typeof (LocalizedStrings), Name = "TimeZone", Description = "TimeZoneDot", GroupName = "Parameters", Order = 133)]
  public TimeZoneInfo TimeZone
  {
    get => this._timeZone;
    set
    {
      this._timeZone = value;
      this.NotifyChanged("");
    }
  }

  /// <summary>Current data point with for X axis.</summary>
  [Browsable(false)]
  public double DataPointWidth
  {
    get => this._dataPointWidth;
    internal set
    {
      if (Math.Abs(this._dataPointWidth - value) < 0.3)
        return;
      this._dataPointWidth = value;
      this.NotifyChanged("");
    }
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Load(SettingsStorage storage)
  {
    this.Id = storage.GetValue<string>("", (string) null) ?? storage.GetValue<string>("", (string) null);
    this.Title = storage.GetValue<string>("", (string) null);
    this.IsVisible = storage.GetValue<bool>("", this.IsVisible);
    this.Group = storage.GetValue<string>("", this.Group);
    this.AutoRange = storage.GetValue<bool>("", this.AutoRange);
    this.DrawMinorTicks = storage.GetValue<bool>("", this.DrawMinorTicks);
    this.DrawMajorTicks = storage.GetValue<bool>("", this.DrawMajorTicks);
    this.DrawMajorGridLines = storage.GetValue<bool>("", this.DrawMajorGridLines);
    this.DrawMinorGridLines = storage.GetValue<bool>("", this.DrawMinorGridLines);
    this.DrawLabels = storage.GetValue<bool>("", this.DrawLabels);
    this.TextFormatting = storage.GetValue<string>("", this.TextFormatting);
    this.CursorTextFormatting = storage.GetValue<string>("", this.CursorTextFormatting);
    this.SubDayTextFormatting = storage.GetValue<string>("", this.SubDayTextFormatting);
    this.SwitchAxisLocation = storage.GetValue<bool>("", this.SwitchAxisLocation);
    this.AxisType = storage.GetValue<ChartAxisType>("", ChartAxisType.DateTime);
    this.TimeZone = Converter.To<TimeZoneInfo>((object) storage.GetValue<string>("", (string) null)) ?? TimeZoneInfo.Local;
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Save(SettingsStorage storage)
  {
    storage.SetValue<string>("", this.Id);
    storage.SetValue<string>("", this.Title);
    storage.SetValue<bool>("", this.IsVisible);
    storage.SetValue<string>("", this.Group);
    storage.SetValue<bool>("", this.AutoRange);
    storage.SetValue<bool>("", this.DrawMinorTicks);
    storage.SetValue<bool>("", this.DrawMajorTicks);
    storage.SetValue<bool>("", this.DrawMajorGridLines);
    storage.SetValue<bool>("", this.DrawMinorGridLines);
    storage.SetValue<bool>("", this.DrawLabels);
    storage.SetValue<string>("", this.TextFormatting);
    storage.SetValue<string>("", this.CursorTextFormatting);
    storage.SetValue<string>("", this.SubDayTextFormatting);
    storage.SetValue<bool>("", this.SwitchAxisLocation);
    storage.SetValue<ChartAxisType>("", this.AxisType);
    storage.SetValue<string>("", this.TimeZone?.Id);
  }
}
