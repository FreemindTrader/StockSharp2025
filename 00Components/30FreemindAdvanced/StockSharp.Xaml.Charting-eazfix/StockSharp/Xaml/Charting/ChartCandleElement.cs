// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartCandleElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

[Display(ResourceType = typeof (LocalizedStrings), Name = "CandleSettings")]
public class ChartCandleElement : 
  ChartElement<ChartCandleElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  IChartCandleElement,
  INotifyPropertyChanging,
  IPersistable,
  IfxChartElement,
  \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ChartCandleDrawStyles \u0023\u003DzC4jphaMvwp_c;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzOt6VQIXEz6wR;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzxQUcdBzqU1tV;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzaEdb48\u0024dPZb2;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color \u0023\u003DzXmtPZlntnnIF;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int \u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzCGVfeT7yJc5e;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzgRuR77srSeQQ;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003Dz1qvt9yuVxTg7;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzvu7bxO54zKRR = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Decimal? \u0023\u003Dz\u0024kpsOHjyC4ZcEEkGZw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?> \u0023\u003Dzf_mf3EOeyMmfELM_yQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int? \u0023\u003Dz7BO88sWmQOlbQ4xbNv234Vs\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int? \u0023\u003DzCchKc9p6e3eASDI7XtYEcSE\u003D = new int?(15);
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzuE0IT92bNDWj;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzxziNvK2z\u0024KKtYX2QOUItAIE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzLXxRNLflkZg1QXO9GQ5mbEA\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzYVRqs8SxcIG3\u0024Cmab528k0c\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzVyz_X4QJeeZe;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzhpjhCeO6qVIc;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DztG76ml9LT4Fefpo49w\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzlcMNLwJnh3U0GIQLv6ifbn8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzRZlnxFq_dA8bpVucvA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003Dzm_rCsdjvIme82gDcwg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzlHGQ7vhyms9RRIMG4Q\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzNDBg1nu512sWNcpi3IGLK7I\u003D = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzgHhE3eSOYhXCYO0MnRxti4g\u003D = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dzo3fyW\u0024yqm7aFTjI2Oe\u00245mKM\u003D = 0.15;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzHdBmlsMAGJRI;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003Dz8kh4LIBZUlf_;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DztSljFjtK7JnB = "Segoe UI";
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Decimal \u0023\u003DzItQGboHj57Hj = 14M;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private FontWeight \u0023\u003DzVykjiWPdJqgM = FontWeights.Bold;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzl6q7_ptbSx3A8sqdX2gH1ls\u003D = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzieAJJNZ68tP_;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003Dzes2ibafgS30F;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzTWnsWqFC_c4o;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private System.Windows.Media.Color? \u0023\u003DzPWHjilJVaIGi;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Func<DateTimeOffset, bool, bool, System.Drawing.Color?> \u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D \u0023\u003Dz2YSX_Z4\u003D;

  public ChartCandleElement()
  {
    this.DownFillColor = this.DownBorderColor = Colors.Red;
    this.UpFillColor = this.UpBorderColor = Colors.Green;
    this.StrokeThickness = 1;
    this.DrawStyle = ChartCandleDrawStyles.CandleStick;
  }

  System.Windows.Media.Color \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return Colors.Transparent;
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Style", Description = "StyleCandlesRender", GroupName = "Style", Order = 25)]
  public ChartCandleDrawStyles DrawStyle
  {
    get => this.\u0023\u003DzC4jphaMvwp_c;
    set
    {
      if (this.\u0023\u003DzC4jphaMvwp_c == value)
        return;
      this.RaisePropertyValueChanging(nameof (DrawStyle), (object) value);
      if (this.\u0023\u003DzC4jphaMvwp_c == ChartCandleDrawStyles.PnF)
        this.AntiAliasing = false;
      switch (value)
      {
        case ChartCandleDrawStyles.CandleStick:
        case ChartCandleDrawStyles.LineOpen:
        case ChartCandleDrawStyles.LineHigh:
        case ChartCandleDrawStyles.LineLow:
        case ChartCandleDrawStyles.LineClose:
        case ChartCandleDrawStyles.Area:
          if (this.\u0023\u003DzC4jphaMvwp_c == ChartCandleDrawStyles.Ohlc)
          {
            this.StrokeThickness = 1;
            break;
          }
          break;
        case ChartCandleDrawStyles.Ohlc:
          this.StrokeThickness = 8;
          break;
        case ChartCandleDrawStyles.BoxVolume:
        case ChartCandleDrawStyles.ClusterProfile:
          this.StrokeThickness = 1;
          break;
        case ChartCandleDrawStyles.PnF:
          this.StrokeThickness = 1;
          this.AntiAliasing = true;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      }
      this.\u0023\u003DzC4jphaMvwp_c = value;
      this.RaisePropertyChanged(nameof (DrawStyle));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Decrease", Description = "ColorOfDecreaseCandle", GroupName = "Style", Order = 30)]
  public System.Windows.Media.Color DownFillColor
  {
    get => this.\u0023\u003DzOt6VQIXEz6wR;
    set
    {
      if (this.\u0023\u003DzOt6VQIXEz6wR == value)
        return;
      this.\u0023\u003DzOt6VQIXEz6wR = value;
      this.RaisePropertyChanged(nameof (DownFillColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Increase", Description = "ColorOfIncreaseCandle", GroupName = "Style", Order = 32 /*0x20*/)]
  public System.Windows.Media.Color UpFillColor
  {
    get => this.\u0023\u003DzxQUcdBzqU1tV;
    set
    {
      if (this.\u0023\u003DzxQUcdBzqU1tV == value)
        return;
      this.\u0023\u003DzxQUcdBzqU1tV = value;
      this.RaisePropertyChanged(nameof (UpFillColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "DecreaseBorder", Description = "DecreaseBorderDesc", GroupName = "Style", Order = 31 /*0x1F*/)]
  public System.Windows.Media.Color DownBorderColor
  {
    get => this.\u0023\u003DzaEdb48\u0024dPZb2;
    set
    {
      if (this.\u0023\u003DzaEdb48\u0024dPZb2 == value)
        return;
      this.\u0023\u003DzaEdb48\u0024dPZb2 = value;
      this.RaisePropertyChanged(nameof (DownBorderColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "IncreaseBorder", Description = "IncreaseBorderDesc", GroupName = "Style", Order = 33)]
  public System.Windows.Media.Color UpBorderColor
  {
    get => this.\u0023\u003DzXmtPZlntnnIF;
    set
    {
      if (this.\u0023\u003DzXmtPZlntnnIF == value)
        return;
      this.\u0023\u003DzXmtPZlntnnIF = value;
      this.RaisePropertyChanged(nameof (UpBorderColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "LineWidth", Description = "LineWidthDesc", GroupName = "Style", Order = 40)]
  public int StrokeThickness
  {
    get => this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D == value)
        return;
      this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = value;
      if (this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D < 1 || this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D > 10)
        throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      this.RaisePropertyChanged(nameof (StrokeThickness));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "AntiAliasing", Description = "CandlesRenderAntiAliasing", GroupName = "Style", Order = 40)]
  public bool AntiAliasing
  {
    get => this.\u0023\u003DzCGVfeT7yJc5e;
    set
    {
      if (this.\u0023\u003DzCGVfeT7yJc5e == value)
        return;
      this.\u0023\u003DzCGVfeT7yJc5e = value;
      this.RaisePropertyChanged(nameof (AntiAliasing));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "LineColor", Description = "LineColorDot", GroupName = "Style", Order = 46)]
  public System.Windows.Media.Color? LineColor
  {
    get => this.\u0023\u003DzgRuR77srSeQQ;
    set
    {
      System.Windows.Media.Color? zgRuR77srSeQq = this.\u0023\u003DzgRuR77srSeQQ;
      System.Windows.Media.Color? nullable = value;
      if ((zgRuR77srSeQq.HasValue == nullable.HasValue ? (zgRuR77srSeQq.HasValue ? (zgRuR77srSeQq.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        return;
      this.\u0023\u003DzgRuR77srSeQQ = value;
      this.RaisePropertyChanged(nameof (LineColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "AreaColor", Description = "AreaColorDot", GroupName = "Style", Order = 47)]
  public System.Windows.Media.Color? AreaColor
  {
    get => this.\u0023\u003Dz1qvt9yuVxTg7;
    set
    {
      System.Windows.Media.Color? z1qvt9yuVxTg7 = this.\u0023\u003Dz1qvt9yuVxTg7;
      System.Windows.Media.Color? nullable = value;
      if ((z1qvt9yuVxTg7.HasValue == nullable.HasValue ? (z1qvt9yuVxTg7.HasValue ? (z1qvt9yuVxTg7.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        return;
      this.\u0023\u003Dz1qvt9yuVxTg7 = value;
      this.RaisePropertyChanged(nameof (AreaColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Marker", Description = "ShowAxisMarker", GroupName = "Style", Order = 50)]
  public bool ShowAxisMarker
  {
    get => this.\u0023\u003Dzvu7bxO54zKRR;
    set
    {
      if (this.\u0023\u003Dzvu7bxO54zKRR == value)
        return;
      this.\u0023\u003Dzvu7bxO54zKRR = value;
      this.RaisePropertyChanged(nameof (ShowAxisMarker));
    }
  }

  [DataMember]
  [Display(ResourceType = typeof (LocalizedStrings), Name = "PriceStep", Description = "MinPriceStep", GroupName = "Common", Order = 1004)]
  public Decimal? PriceStep
  {
    get => this.\u0023\u003Dz\u0024kpsOHjyC4ZcEEkGZw\u003D\u003D;
    set
    {
      Decimal? kpsOhjyC4ZcEekGzw = this.\u0023\u003Dz\u0024kpsOHjyC4ZcEEkGZw\u003D\u003D;
      Decimal? nullable1 = value;
      if (kpsOhjyC4ZcEekGzw.GetValueOrDefault() == nullable1.GetValueOrDefault() & kpsOhjyC4ZcEekGzw.HasValue == nullable1.HasValue)
        return;
      Decimal? nullable2 = value;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() <= num & nullable2.HasValue)
        throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      this.\u0023\u003Dz\u0024kpsOHjyC4ZcEEkGZw\u003D\u003D = value;
      this.RaisePropertyChanged(nameof (PriceStep));
    }
  }

  [Browsable(false)]
  public Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?> Colorer
  {
    get => this.\u0023\u003Dzf_mf3EOeyMmfELM_yQ\u003D\u003D;
    set
    {
      this.\u0023\u003Dzf_mf3EOeyMmfELM_yQ\u003D\u003D = value;
      this.RaisePropertyChanged(nameof (Colorer));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Timeframe2Multiplier", Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Order = 3)]
  public int? Timeframe2Multiplier
  {
    get => this.\u0023\u003Dz7BO88sWmQOlbQ4xbNv234Vs\u003D;
    set
    {
      int? wmQolbQ4xbNv234Vs = this.\u0023\u003Dz7BO88sWmQOlbQ4xbNv234Vs\u003D;
      int? nullable1 = value;
      if (wmQolbQ4xbNv234Vs.GetValueOrDefault() == nullable1.GetValueOrDefault() & wmQolbQ4xbNv234Vs.HasValue == nullable1.HasValue)
        return;
      int? nullable2 = value;
      if (nullable2.GetValueOrDefault() < 1 & nullable2.HasValue)
        throw new ArgumentOutOfRangeException(nameof (Timeframe2Multiplier));
      this.SetField<int?>(ref this.\u0023\u003Dz7BO88sWmQOlbQ4xbNv234Vs\u003D, value, nameof (Timeframe2Multiplier));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Timeframe3Multiplier", Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Order = 4)]
  public int? Timeframe3Multiplier
  {
    get => this.\u0023\u003DzCchKc9p6e3eASDI7XtYEcSE\u003D;
    set
    {
      int? kc9p6e3eAsdI7XtYecSe = this.\u0023\u003DzCchKc9p6e3eASDI7XtYEcSE\u003D;
      int? nullable1 = value;
      if (kc9p6e3eAsdI7XtYecSe.GetValueOrDefault() == nullable1.GetValueOrDefault() & kc9p6e3eAsdI7XtYecSe.HasValue == nullable1.HasValue)
        return;
      int? nullable2 = value;
      if (nullable2.GetValueOrDefault() < 1 & nullable2.HasValue)
        throw new ArgumentOutOfRangeException(nameof (Timeframe3Multiplier));
      this.SetField<int?>(ref this.\u0023\u003DzCchKc9p6e3eASDI7XtYEcSE\u003D, value, nameof (Timeframe3Multiplier));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "FontColor", Description = "FontColorDot", GroupName = "VolumeSettings", Order = 5)]
  public System.Windows.Media.Color? FontColor
  {
    get => this.\u0023\u003DzuE0IT92bNDWj;
    set => this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzuE0IT92bNDWj, value, nameof (FontColor));
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Timeframe2GridColor", Description = "Timeframe2GridColorDot", GroupName = "VolumeSettings", Order = 6)]
  public System.Windows.Media.Color? Timeframe2Color
  {
    get => this.\u0023\u003DzxziNvK2z\u0024KKtYX2QOUItAIE\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzxziNvK2z\u0024KKtYX2QOUItAIE\u003D, value, nameof (Timeframe2Color));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Timeframe2FrameColor", Description = "Timeframe2FrameColorDot", GroupName = "VolumeSettings", Order = 7)]
  public System.Windows.Media.Color? Timeframe2FrameColor
  {
    get => this.\u0023\u003DzLXxRNLflkZg1QXO9GQ5mbEA\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzLXxRNLflkZg1QXO9GQ5mbEA\u003D, value, nameof (Timeframe2FrameColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Timeframe3GridColor", Description = "Timeframe3GridColorDot", GroupName = "VolumeSettings", Order = 8)]
  public System.Windows.Media.Color? Timeframe3Color
  {
    get => this.\u0023\u003DzYVRqs8SxcIG3\u0024Cmab528k0c\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzYVRqs8SxcIG3\u0024Cmab528k0c\u003D, value, nameof (Timeframe3Color));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "MaxVolumeColor", Description = "MaxVolumeColorDot", GroupName = "VolumeSettings", Order = 9)]
  public System.Windows.Media.Color? MaxVolumeColor
  {
    get => this.\u0023\u003DzVyz_X4QJeeZe;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzVyz_X4QJeeZe, value, nameof (MaxVolumeColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "MaxVolumeBackground", Description = "MaxVolumeBackground", GroupName = "VolumeSettings", Order = 9)]
  public System.Windows.Media.Color? MaxVolumeBackground
  {
    get => this.\u0023\u003DzhpjhCeO6qVIc;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzhpjhCeO6qVIc, value, nameof (MaxVolumeBackground));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ClusterLineColor", Description = "ClusterLineColorDot", GroupName = "VolumeSettings", Order = 10)]
  public System.Windows.Media.Color? ClusterLineColor
  {
    get => this.\u0023\u003DztG76ml9LT4Fefpo49w\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DztG76ml9LT4Fefpo49w\u003D\u003D, value, nameof (ClusterLineColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ClusterSeparatorLineColor", Description = "ClusterSeparatorLineColorDot", GroupName = "VolumeSettings", Order = 11)]
  public System.Windows.Media.Color? ClusterSeparatorLineColor
  {
    get => this.\u0023\u003DzlcMNLwJnh3U0GIQLv6ifbn8\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzlcMNLwJnh3U0GIQLv6ifbn8\u003D, value, nameof (ClusterSeparatorLineColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ClusterTextColor", Description = "ClusterTextColorDot", GroupName = "VolumeSettings", Order = 12)]
  public System.Windows.Media.Color? ClusterTextColor
  {
    get => this.\u0023\u003DzRZlnxFq_dA8bpVucvA\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzRZlnxFq_dA8bpVucvA\u003D\u003D, value, nameof (ClusterTextColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ClusterColor", Description = "ClusterColorDot", GroupName = "VolumeSettings", Order = 13)]
  public System.Windows.Media.Color? ClusterColor
  {
    get => this.\u0023\u003Dzm_rCsdjvIme82gDcwg\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003Dzm_rCsdjvIme82gDcwg\u003D\u003D, value, nameof (ClusterColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ClusterMaxVolumeColor", Description = "ClusterMaxVolumeColorDot", GroupName = "VolumeSettings", Order = 14)]
  public System.Windows.Media.Color? ClusterMaxColor
  {
    get => this.\u0023\u003DzlHGQ7vhyms9RRIMG4Q\u003D\u003D;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzlHGQ7vhyms9RRIMG4Q\u003D\u003D, value, nameof (ClusterMaxColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ShowHorizontalVolumes", Description = "ShowHorizontalVolumesDot", GroupName = "VolumeSettings", Order = 15)]
  public bool ShowHorizontalVolumes
  {
    get => this.\u0023\u003DzNDBg1nu512sWNcpi3IGLK7I\u003D;
    set
    {
      this.SetField<bool>(ref this.\u0023\u003DzNDBg1nu512sWNcpi3IGLK7I\u003D, value, nameof (ShowHorizontalVolumes));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "LocalHorizontalVolumes", Description = "LocalHorizontalVolumesDot", GroupName = "VolumeSettings", Order = 16 /*0x10*/)]
  public bool LocalHorizontalVolumes
  {
    get => this.\u0023\u003DzgHhE3eSOYhXCYO0MnRxti4g\u003D;
    set
    {
      this.SetField<bool>(ref this.\u0023\u003DzgHhE3eSOYhXCYO0MnRxti4g\u003D, value, nameof (LocalHorizontalVolumes));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "HorizontalVolumeWidthFraction", Description = "HorizontalVolumeWidthFractionDot", GroupName = "VolumeSettings", Order = 17)]
  public double HorizontalVolumeWidthFraction
  {
    get => this.\u0023\u003Dzo3fyW\u0024yqm7aFTjI2Oe\u00245mKM\u003D;
    set
    {
      if (this.\u0023\u003Dzo3fyW\u0024yqm7aFTjI2Oe\u00245mKM\u003D == value)
        return;
      if (value < 0.0 || value > 1.0)
        throw new ArgumentOutOfRangeException(nameof (HorizontalVolumeWidthFraction));
      this.SetField<double>(ref this.\u0023\u003Dzo3fyW\u0024yqm7aFTjI2Oe\u00245mKM\u003D, value, nameof (HorizontalVolumeWidthFraction));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "HorizontalVolumeColor", Description = "HorizontalVolumeColorDot", GroupName = "VolumeSettings", Order = 18)]
  public System.Windows.Media.Color? HorizontalVolumeColor
  {
    get => this.\u0023\u003DzHdBmlsMAGJRI;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzHdBmlsMAGJRI, value, nameof (HorizontalVolumeColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "HorizontalVolumeFontColor", Description = "HorizontalVolumeFontColorDot", GroupName = "VolumeSettings", Order = 19)]
  public System.Windows.Media.Color? HorizontalVolumeFontColor
  {
    get => this.\u0023\u003Dz8kh4LIBZUlf_;
    set
    {
      this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003Dz8kh4LIBZUlf_, value, nameof (HorizontalVolumeFontColor));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "FontFamily", Description = "FontFamily", GroupName = "VolumeSettings", Order = 20)]
  [ItemsSource(typeof (FontFamilyNamesItemsSource))]
  public string FontFamily
  {
    get => this.\u0023\u003DztSljFjtK7JnB;
    set
    {
      ref string local = ref this.\u0023\u003DztSljFjtK7JnB;
      string str = !StringHelper.IsEmpty(value) ? value : throw new ArgumentNullException(nameof (value));
      this.SetField<string>(ref local, str, nameof (FontFamily));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "FontSize", Description = "FontSize", GroupName = "VolumeSettings", Order = 21)]
  public Decimal FontSize
  {
    get => this.\u0023\u003DzItQGboHj57Hj;
    set
    {
      if (this.\u0023\u003DzItQGboHj57Hj == value)
        return;
      if (value < 7M)
        throw new ArgumentOutOfRangeException(nameof (value), (object) value, LocalizedStrings.InvalidValue);
      this.SetField<Decimal>(ref this.\u0023\u003DzItQGboHj57Hj, value, nameof (FontSize));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "FontWeight", Description = "FontWeight", GroupName = "VolumeSettings", Order = 22)]
  public FontWeight FontWeight
  {
    get => this.\u0023\u003DzVykjiWPdJqgM;
    set
    {
      this.SetField<FontWeight>(ref this.\u0023\u003DzVykjiWPdJqgM, value, nameof (FontWeight));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Separate", Description = "DrawSeparateVolumes", GroupName = "VolumeSettings", Order = 23)]
  public bool DrawSeparateVolumes
  {
    get => this.\u0023\u003Dzl6q7_ptbSx3A8sqdX2gH1ls\u003D;
    set
    {
      this.SetField<bool>(ref this.\u0023\u003Dzl6q7_ptbSx3A8sqdX2gH1ls\u003D, value, nameof (DrawSeparateVolumes));
    }
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Buy", Description = "BuyColor", GroupName = "VolumeSettings", Order = 24)]
  public System.Windows.Media.Color? BuyColor
  {
    get => this.\u0023\u003DzieAJJNZ68tP_;
    set => this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzieAJJNZ68tP_, value, nameof (BuyColor));
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Sell", Description = "SellColor", GroupName = "VolumeSettings", Order = 25)]
  public System.Windows.Media.Color? SellColor
  {
    get => this.\u0023\u003Dzes2ibafgS30F;
    set => this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003Dzes2ibafgS30F, value, nameof (SellColor));
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Up", Description = "UpTrend", GroupName = "VolumeSettings", Order = 26)]
  public System.Windows.Media.Color? UpColor
  {
    get => this.\u0023\u003DzTWnsWqFC_c4o;
    set => this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzTWnsWqFC_c4o, value, nameof (UpColor));
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Down", Description = "DownTrend", GroupName = "VolumeSettings", Order = 27)]
  public System.Windows.Media.Color? DownColor
  {
    get => this.\u0023\u003DzPWHjilJVaIGi;
    set => this.SetField<System.Windows.Media.Color?>(ref this.\u0023\u003DzPWHjilJVaIGi, value, nameof (DownColor));
  }

  System.Drawing.Color IChartCandleElement.DownFillColor
  {
    get => this.DownFillColor.FromWpf();
    set => this.DownFillColor = value.ToWpf();
  }

  System.Drawing.Color IChartCandleElement.UpFillColor
  {
    get => this.UpFillColor.FromWpf();
    set => this.UpFillColor = value.ToWpf();
  }

  System.Drawing.Color IChartCandleElement.DownBorderColor
  {
    get => this.DownBorderColor.FromWpf();
    set => this.DownBorderColor = value.ToWpf();
  }

  System.Drawing.Color IChartCandleElement.UpBorderColor
  {
    get => this.UpBorderColor.FromWpf();
    set => this.UpBorderColor = value.ToWpf();
  }

  System.Drawing.Color? IChartCandleElement.LineColor
  {
    get
    {
      System.Windows.Media.Color? lineColor = this.LineColor;
      ref System.Windows.Media.Color? local = ref lineColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.LineColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.AreaColor
  {
    get
    {
      System.Windows.Media.Color? areaColor = this.AreaColor;
      ref System.Windows.Media.Color? local = ref areaColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.AreaColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.FontColor
  {
    get
    {
      System.Windows.Media.Color? fontColor = this.FontColor;
      ref System.Windows.Media.Color? local = ref fontColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.FontColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.Timeframe2Color
  {
    get
    {
      System.Windows.Media.Color? timeframe2Color = this.Timeframe2Color;
      ref System.Windows.Media.Color? local = ref timeframe2Color;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.Timeframe2Color = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.Timeframe2FrameColor
  {
    get
    {
      System.Windows.Media.Color? timeframe2FrameColor = this.Timeframe2FrameColor;
      ref System.Windows.Media.Color? local = ref timeframe2FrameColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.Timeframe2FrameColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.Timeframe3Color
  {
    get
    {
      System.Windows.Media.Color? timeframe3Color = this.Timeframe3Color;
      ref System.Windows.Media.Color? local = ref timeframe3Color;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.Timeframe3Color = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.MaxVolumeColor
  {
    get
    {
      System.Windows.Media.Color? maxVolumeColor = this.MaxVolumeColor;
      ref System.Windows.Media.Color? local = ref maxVolumeColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.MaxVolumeColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.ClusterLineColor
  {
    get
    {
      System.Windows.Media.Color? clusterLineColor = this.ClusterLineColor;
      ref System.Windows.Media.Color? local = ref clusterLineColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.ClusterLineColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.ClusterSeparatorLineColor
  {
    get
    {
      System.Windows.Media.Color? separatorLineColor = this.ClusterSeparatorLineColor;
      ref System.Windows.Media.Color? local = ref separatorLineColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.ClusterSeparatorLineColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.ClusterTextColor
  {
    get
    {
      System.Windows.Media.Color? clusterTextColor = this.ClusterTextColor;
      ref System.Windows.Media.Color? local = ref clusterTextColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.ClusterTextColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.ClusterColor
  {
    get
    {
      System.Windows.Media.Color? clusterColor = this.ClusterColor;
      ref System.Windows.Media.Color? local = ref clusterColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.ClusterColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.ClusterMaxColor
  {
    get
    {
      System.Windows.Media.Color? clusterMaxColor = this.ClusterMaxColor;
      ref System.Windows.Media.Color? local = ref clusterMaxColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.ClusterMaxColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.HorizontalVolumeColor
  {
    get
    {
      System.Windows.Media.Color? horizontalVolumeColor = this.HorizontalVolumeColor;
      ref System.Windows.Media.Color? local = ref horizontalVolumeColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.HorizontalVolumeColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.HorizontalVolumeFontColor
  {
    get
    {
      System.Windows.Media.Color? horizontalVolumeFontColor = this.HorizontalVolumeFontColor;
      ref System.Windows.Media.Color? local = ref horizontalVolumeFontColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.HorizontalVolumeFontColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.BuyColor
  {
    get
    {
      System.Windows.Media.Color? buyColor = this.BuyColor;
      ref System.Windows.Media.Color? local = ref buyColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.BuyColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  System.Drawing.Color? IChartCandleElement.SellColor
  {
    get
    {
      System.Windows.Media.Color? sellColor = this.SellColor;
      ref System.Windows.Media.Color? local = ref sellColor;
      return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
    }
    set
    {
      this.SellColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
    }
  }

  Func<DateTimeOffset, bool, bool, System.Drawing.Color?> IChartCandleElement.Colorer
  {
    get => this.\u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D;
    set
    {
      ChartCandleElement.\u0023\u003Dzpm0QOV89qiXJFj7EloQMSS0\u003D v89qiXjFj7EloQmsS0 = new ChartCandleElement.\u0023\u003Dzpm0QOV89qiXJFj7EloQMSS0\u003D();
      v89qiXjFj7EloQmsS0.\u0023\u003DzxGz2_8k\u003D = value;
      this.\u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D = v89qiXjFj7EloQmsS0.\u0023\u003DzxGz2_8k\u003D;
      if (this.\u0023\u003DzihbAyecvYexTGyVxgQ\u003D\u003D == null)
        this.Colorer = (Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?>) null;
      else
        this.Colorer = new Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?>(v89qiXjFj7EloQmsS0.\u0023\u003DzhES9R3GcxsFNnB11gQcoe_qNslLr6z0WiGpRi_CHokEXxcRuYqcRWK9R6Cn8);
    }
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.UpFillColor = storage.GetValue<int>("UpFillColor", this.UpFillColor.ToInt()).ToColor();
    this.UpBorderColor = storage.GetValue<int>("UpBorderColor", this.UpBorderColor.ToInt()).ToColor();
    this.DownFillColor = storage.GetValue<int>("DownFillColor", this.DownFillColor.ToInt()).ToColor();
    this.DownBorderColor = storage.GetValue<int>("DownBorderColor", this.DownBorderColor.ToInt()).ToColor();
    int? nullable1 = storage.GetValue<int?>("LineColor", new int?());
    ref int? local1 = ref nullable1;
    this.LineColor = local1.HasValue ? new System.Windows.Media.Color?(local1.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage1 = storage;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    nullable1 = settingsStorage1.GetValue<int?>("AreaColor", nullable2);
    ref int? local2 = ref nullable1;
    this.AreaColor = local2.HasValue ? new System.Windows.Media.Color?(local2.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    this.DrawStyle = storage.GetValue<ChartCandleDrawStyles>("DrawStyle", this.DrawStyle);
    this.StrokeThickness = storage.GetValue<int>("StrokeThickness", this.StrokeThickness);
    this.AntiAliasing = storage.GetValue<bool>("AntiAliasing", this.AntiAliasing);
    this.ShowAxisMarker = storage.GetValue<bool>("ShowAxisMarker", this.ShowAxisMarker);
    SettingsStorage settingsStorage2 = storage.GetValue<SettingsStorage>("Cluster", (SettingsStorage) null);
    if (settingsStorage2 == null)
      return;
    this.\u0023\u003DzTaKnQEQ7mav2(settingsStorage2);
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<int>("UpFillColor", this.UpFillColor.ToInt());
    storage.SetValue<int>("UpBorderColor", this.UpBorderColor.ToInt());
    storage.SetValue<int>("DownFillColor", this.DownFillColor.ToInt());
    storage.SetValue<int>("DownBorderColor", this.DownBorderColor.ToInt());
    SettingsStorage settingsStorage1 = storage;
    System.Windows.Media.Color? nullable1 = this.LineColor;
    ref System.Windows.Media.Color? local1 = ref nullable1;
    int? nullable2 = local1.HasValue ? new int?(local1.GetValueOrDefault().ToInt()) : new int?();
    settingsStorage1.SetValue<int?>("LineColor", nullable2);
    SettingsStorage settingsStorage2 = storage;
    nullable1 = this.AreaColor;
    ref System.Windows.Media.Color? local2 = ref nullable1;
    int? nullable3 = local2.HasValue ? new int?(local2.GetValueOrDefault().ToInt()) : new int?();
    settingsStorage2.SetValue<int?>("AreaColor", nullable3);
    storage.SetValue<string>("DrawStyle", Converter.To<string>((object) this.DrawStyle));
    storage.SetValue<int>("StrokeThickness", this.StrokeThickness);
    storage.SetValue<bool>("AntiAliasing", this.AntiAliasing);
    storage.SetValue<bool>("ShowAxisMarker", this.ShowAxisMarker);
    storage.SetValue<SettingsStorage>("Cluster", this.\u0023\u003DzQsknO3CZ\u0024aiy());
  }

  private void \u0023\u003DzTaKnQEQ7mav2(SettingsStorage _param1)
  {
    this.Timeframe2Multiplier = _param1 != null ? _param1.GetValue<int?>("Timeframe2Multiplier", this.Timeframe2Multiplier) : throw new ArgumentNullException("storage");
    this.Timeframe3Multiplier = _param1.GetValue<int?>("Timeframe3Multiplier", this.Timeframe3Multiplier);
    int? nullable1 = _param1.GetValue<int?>("FontColor", new int?());
    ref int? local1 = ref nullable1;
    this.FontColor = local1.HasValue ? new System.Windows.Media.Color?(local1.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage1 = _param1;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    nullable1 = settingsStorage1.GetValue<int?>("Timeframe2Color", nullable2);
    ref int? local2 = ref nullable1;
    this.Timeframe2Color = local2.HasValue ? new System.Windows.Media.Color?(local2.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage2 = _param1;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    nullable1 = settingsStorage2.GetValue<int?>("Timeframe2FrameColor", nullable3);
    ref int? local3 = ref nullable1;
    this.Timeframe2FrameColor = local3.HasValue ? new System.Windows.Media.Color?(local3.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage3 = _param1;
    nullable1 = new int?();
    int? nullable4 = nullable1;
    nullable1 = settingsStorage3.GetValue<int?>("Timeframe3Color", nullable4);
    ref int? local4 = ref nullable1;
    this.Timeframe3Color = local4.HasValue ? new System.Windows.Media.Color?(local4.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage4 = _param1;
    nullable1 = new int?();
    int? nullable5 = nullable1;
    nullable1 = settingsStorage4.GetValue<int?>("MaxVolumeColor", nullable5);
    ref int? local5 = ref nullable1;
    this.MaxVolumeColor = local5.HasValue ? new System.Windows.Media.Color?(local5.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage5 = _param1;
    nullable1 = new int?();
    int? nullable6 = nullable1;
    nullable1 = settingsStorage5.GetValue<int?>("MaxVolumeBackground", nullable6);
    ref int? local6 = ref nullable1;
    this.MaxVolumeBackground = local6.HasValue ? new System.Windows.Media.Color?(local6.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage6 = _param1;
    nullable1 = new int?();
    int? nullable7 = nullable1;
    nullable1 = settingsStorage6.GetValue<int?>("ClusterSeparatorLineColor", nullable7);
    ref int? local7 = ref nullable1;
    this.ClusterSeparatorLineColor = local7.HasValue ? new System.Windows.Media.Color?(local7.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage7 = _param1;
    nullable1 = new int?();
    int? nullable8 = nullable1;
    nullable1 = settingsStorage7.GetValue<int?>("ClusterLineColor", nullable8);
    ref int? local8 = ref nullable1;
    this.ClusterLineColor = local8.HasValue ? new System.Windows.Media.Color?(local8.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage8 = _param1;
    nullable1 = new int?();
    int? nullable9 = nullable1;
    nullable1 = settingsStorage8.GetValue<int?>("ClusterTextColor", nullable9);
    ref int? local9 = ref nullable1;
    this.ClusterTextColor = local9.HasValue ? new System.Windows.Media.Color?(local9.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage9 = _param1;
    nullable1 = new int?();
    int? nullable10 = nullable1;
    nullable1 = settingsStorage9.GetValue<int?>("ClusterColor", nullable10);
    ref int? local10 = ref nullable1;
    this.ClusterColor = local10.HasValue ? new System.Windows.Media.Color?(local10.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage10 = _param1;
    nullable1 = new int?();
    int? nullable11 = nullable1;
    nullable1 = settingsStorage10.GetValue<int?>("ClusterMaxColor", nullable11);
    ref int? local11 = ref nullable1;
    this.ClusterMaxColor = local11.HasValue ? new System.Windows.Media.Color?(local11.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    this.ShowHorizontalVolumes = _param1.GetValue<bool>("ShowHorizontalVolumes", false);
    this.LocalHorizontalVolumes = _param1.GetValue<bool>("LocalHorizontalVolumes", false);
    this.HorizontalVolumeWidthFraction = _param1.GetValue<double>("HorizontalVolumeWidthFraction", 0.0);
    SettingsStorage settingsStorage11 = _param1;
    nullable1 = new int?();
    int? nullable12 = nullable1;
    nullable1 = settingsStorage11.GetValue<int?>("HorizontalVolumeColor", nullable12);
    ref int? local12 = ref nullable1;
    this.HorizontalVolumeColor = local12.HasValue ? new System.Windows.Media.Color?(local12.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage12 = _param1;
    nullable1 = new int?();
    int? nullable13 = nullable1;
    nullable1 = settingsStorage12.GetValue<int?>("HorizontalVolumeFontColor", nullable13);
    ref int? local13 = ref nullable1;
    this.HorizontalVolumeFontColor = local13.HasValue ? new System.Windows.Media.Color?(local13.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    this.PriceStep = _param1.GetValue<Decimal?>("PriceStep", new Decimal?());
    this.FontFamily = _param1.GetValue<string>("FontFamily", this.FontFamily);
    this.FontSize = _param1.GetValue<Decimal>("FontSize", this.FontSize);
    this.FontWeight = FontWeight.FromOpenTypeWeight(_param1.GetValue<int>("FontWeight", this.FontWeight.ToOpenTypeWeight()));
    this.DrawSeparateVolumes = _param1.GetValue<bool>("DrawSeparateVolumes", this.DrawSeparateVolumes);
    SettingsStorage settingsStorage13 = _param1;
    nullable1 = new int?();
    int? nullable14 = nullable1;
    nullable1 = settingsStorage13.GetValue<int?>("BuyColor", nullable14);
    ref int? local14 = ref nullable1;
    this.BuyColor = local14.HasValue ? new System.Windows.Media.Color?(local14.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage14 = _param1;
    nullable1 = new int?();
    int? nullable15 = nullable1;
    nullable1 = settingsStorage14.GetValue<int?>("SellColor", nullable15);
    ref int? local15 = ref nullable1;
    this.SellColor = local15.HasValue ? new System.Windows.Media.Color?(local15.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage15 = _param1;
    nullable1 = new int?();
    int? nullable16 = nullable1;
    nullable1 = settingsStorage15.GetValue<int?>("UpColor", nullable16);
    ref int? local16 = ref nullable1;
    this.UpColor = local16.HasValue ? new System.Windows.Media.Color?(local16.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    SettingsStorage settingsStorage16 = _param1;
    nullable1 = new int?();
    int? nullable17 = nullable1;
    nullable1 = settingsStorage16.GetValue<int?>("DownColor", nullable17);
    ref int? local17 = ref nullable1;
    this.DownColor = local17.HasValue ? new System.Windows.Media.Color?(local17.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
  }

  private SettingsStorage \u0023\u003DzQsknO3CZ\u0024aiy()
  {
    SettingsStorage settingsStorage = new SettingsStorage();
    settingsStorage.SetValue<int?>("Timeframe2Multiplier", this.Timeframe2Multiplier);
    settingsStorage.SetValue<int?>("Timeframe3Multiplier", this.Timeframe3Multiplier);
    System.Windows.Media.Color? nullable = this.FontColor;
    ref System.Windows.Media.Color? local1 = ref nullable;
    settingsStorage.SetValue<int?>("FontColor", local1.HasValue ? new int?(local1.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.Timeframe2Color;
    ref System.Windows.Media.Color? local2 = ref nullable;
    settingsStorage.SetValue<int?>("Timeframe2Color", local2.HasValue ? new int?(local2.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.Timeframe2FrameColor;
    ref System.Windows.Media.Color? local3 = ref nullable;
    settingsStorage.SetValue<int?>("Timeframe2FrameColor", local3.HasValue ? new int?(local3.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.Timeframe3Color;
    ref System.Windows.Media.Color? local4 = ref nullable;
    settingsStorage.SetValue<int?>("Timeframe3Color", local4.HasValue ? new int?(local4.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.MaxVolumeColor;
    ref System.Windows.Media.Color? local5 = ref nullable;
    settingsStorage.SetValue<int?>("MaxVolumeColor", local5.HasValue ? new int?(local5.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.MaxVolumeBackground;
    ref System.Windows.Media.Color? local6 = ref nullable;
    settingsStorage.SetValue<int?>("MaxVolumeBackground", local6.HasValue ? new int?(local6.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.ClusterLineColor;
    ref System.Windows.Media.Color? local7 = ref nullable;
    settingsStorage.SetValue<int?>("ClusterLineColor", local7.HasValue ? new int?(local7.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.ClusterSeparatorLineColor;
    ref System.Windows.Media.Color? local8 = ref nullable;
    settingsStorage.SetValue<int?>("ClusterSeparatorLineColor", local8.HasValue ? new int?(local8.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.ClusterTextColor;
    ref System.Windows.Media.Color? local9 = ref nullable;
    settingsStorage.SetValue<int?>("ClusterTextColor", local9.HasValue ? new int?(local9.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.ClusterColor;
    ref System.Windows.Media.Color? local10 = ref nullable;
    settingsStorage.SetValue<int?>("ClusterColor", local10.HasValue ? new int?(local10.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.ClusterMaxColor;
    ref System.Windows.Media.Color? local11 = ref nullable;
    settingsStorage.SetValue<int?>("ClusterMaxColor", local11.HasValue ? new int?(local11.GetValueOrDefault().ToInt()) : new int?());
    settingsStorage.SetValue<bool>("ShowHorizontalVolumes", this.ShowHorizontalVolumes);
    settingsStorage.SetValue<bool>("LocalHorizontalVolumes", this.LocalHorizontalVolumes);
    settingsStorage.SetValue<double>("HorizontalVolumeWidthFraction", this.HorizontalVolumeWidthFraction);
    nullable = this.HorizontalVolumeColor;
    ref System.Windows.Media.Color? local12 = ref nullable;
    settingsStorage.SetValue<int?>("HorizontalVolumeColor", local12.HasValue ? new int?(local12.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.HorizontalVolumeFontColor;
    ref System.Windows.Media.Color? local13 = ref nullable;
    settingsStorage.SetValue<int?>("HorizontalVolumeFontColor", local13.HasValue ? new int?(local13.GetValueOrDefault().ToInt()) : new int?());
    settingsStorage.SetValue<Decimal?>("PriceStep", this.PriceStep);
    settingsStorage.SetValue<string>("FontFamily", this.FontFamily);
    settingsStorage.SetValue<Decimal>("FontSize", this.FontSize);
    settingsStorage.SetValue<int>("FontWeight", this.FontWeight.ToOpenTypeWeight());
    settingsStorage.SetValue<bool>("DrawSeparateVolumes", this.DrawSeparateVolumes);
    nullable = this.BuyColor;
    ref System.Windows.Media.Color? local14 = ref nullable;
    settingsStorage.SetValue<int?>("BuyColor", local14.HasValue ? new int?(local14.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.SellColor;
    ref System.Windows.Media.Color? local15 = ref nullable;
    settingsStorage.SetValue<int?>("SellColor", local15.HasValue ? new int?(local15.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.UpColor;
    ref System.Windows.Media.Color? local16 = ref nullable;
    settingsStorage.SetValue<int?>("UpColor", local16.HasValue ? new int?(local16.GetValueOrDefault().ToInt()) : new int?());
    nullable = this.DownColor;
    ref System.Windows.Media.Color? local17 = ref nullable;
    settingsStorage.SetValue<int?>("DownColor", local17.HasValue ? new int?(local17.GetValueOrDefault().ToInt()) : new int?());
    return settingsStorage;
  }

  internal override ChartCandleElement \u0023\u003Dz3MbNd8U\u003D(ChartCandleElement _param1)
  {
    _param1 = base.\u0023\u003Dz3MbNd8U\u003D(_param1);
    _param1.DownFillColor = this.DownFillColor;
    _param1.UpFillColor = this.UpFillColor;
    _param1.DownBorderColor = this.DownBorderColor;
    _param1.UpBorderColor = this.UpBorderColor;
    _param1.AntiAliasing = this.AntiAliasing;
    _param1.StrokeThickness = this.StrokeThickness;
    _param1.LineColor = this.LineColor;
    _param1.AreaColor = this.AreaColor;
    _param1.DrawStyle = this.DrawStyle;
    _param1.ShowAxisMarker = this.ShowAxisMarker;
    _param1.FontColor = this.FontColor;
    _param1.Timeframe2Color = this.Timeframe2Color;
    _param1.Timeframe2FrameColor = this.Timeframe2FrameColor;
    _param1.Timeframe3Color = this.Timeframe3Color;
    _param1.MaxVolumeColor = this.MaxVolumeColor;
    _param1.MaxVolumeBackground = this.MaxVolumeBackground;
    _param1.Timeframe2Multiplier = this.Timeframe2Multiplier;
    _param1.Timeframe3Multiplier = this.Timeframe3Multiplier;
    _param1.ClusterLineColor = this.ClusterLineColor;
    _param1.ClusterSeparatorLineColor = this.ClusterSeparatorLineColor;
    _param1.ClusterTextColor = this.ClusterTextColor;
    _param1.ClusterColor = this.ClusterColor;
    _param1.ClusterMaxColor = this.ClusterMaxColor;
    _param1.ShowHorizontalVolumes = this.ShowHorizontalVolumes;
    _param1.LocalHorizontalVolumes = this.LocalHorizontalVolumes;
    _param1.HorizontalVolumeWidthFraction = this.HorizontalVolumeWidthFraction;
    _param1.HorizontalVolumeColor = this.HorizontalVolumeColor;
    _param1.HorizontalVolumeFontColor = this.HorizontalVolumeFontColor;
    _param1.FontFamily = this.FontFamily;
    _param1.FontSize = this.FontSize;
    _param1.FontWeight = this.FontWeight;
    _param1.DrawSeparateVolumes = this.DrawSeparateVolumes;
    _param1.BuyColor = this.BuyColor;
    _param1.SellColor = this.SellColor;
    _param1.UpColor = this.UpColor;
    _param1.DownColor = this.DownColor;
    return _param1;
  }

  \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D = (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D) new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D(this);
  }

  bool \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003DzJXDjnZfs8tGoFCupfSBAn4fwfCXfeCPpi\u0024rZmqxbRCtxRCyVSA\u003D\u003D(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D.Draw(_param1);
  }

  void \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U8XPmuyOBlZpMiNagFIxa3issk4ACmj9rvI\u003D()
  {
    this.\u0023\u003Dz2YSX_Z4\u003D.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  protected override bool OnDraw(ChartDrawData data)
  {
    List<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D> source1 = data.\u0023\u003DzaZ5Qc3xeNY95((IChartCandleElement) this);
    List<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D> source2 = data.\u0023\u003DzCEKAoZ7e0Ko9((IChartCandleElement) this);
    bool flag1 = source1 != null && !CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>((ICollection<ChartDrawData.\u0023\u003DzbzWrw_pExZ6TZuVkEg\u003D\u003D>) source1);
    bool flag2 = source2 != null && !CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>((ICollection<ChartDrawData.\u0023\u003Dzs3gDB01R_wCz\u0024vlS5w\u003D\u003D>) source2);
    if (flag1 || flag2)
      return ((!flag1 ? 0 : (((\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X) this).\u0023\u003Dz2dQykb\u0024x9fU4(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source1.Cast<ChartDrawData.IDrawValue>(), source1.Count)) ? 1 : 0)) | (!flag2 ? (false ? 1 : 0) : (((\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X) this).\u0023\u003Dz2dQykb\u0024x9fU4(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source2.Cast<ChartDrawData.IDrawValue>(), source2.Count)) ? 1 : 0))) != 0;
    ((\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X) this).\u0023\u003Dz0yXrIqwigzcF();
    return false;
  }

  protected override string GetGeneratedTitle()
  {
    Subscription subscription = this.TryGetSubscription();
    return subscription == null ? (string) null : subscription.\u0023\u003DzNsF8TrDL0ndB();
  }

  protected override int Priority => 0;

  private sealed class \u0023\u003Dzpm0QOV89qiXJFj7EloQMSS0\u003D
  {
    public Func<DateTimeOffset, bool, bool, System.Drawing.Color?> \u0023\u003DzxGz2_8k\u003D;

    internal System.Windows.Media.Color? \u0023\u003DzhES9R3GcxsFNnB11gQcoe_qNslLr6z0WiGpRi_CHokEXxcRuYqcRWK9R6Cn8(
      DateTimeOffset _param1,
      bool _param2,
      bool _param3)
    {
      System.Drawing.Color? nullable = this.\u0023\u003DzxGz2_8k\u003D(_param1, _param2, _param3);
      ref System.Drawing.Color? local = ref nullable;
      return !local.HasValue ? new System.Windows.Media.Color?() : new System.Windows.Media.Color?(local.GetValueOrDefault().ToWpf());
    }
  }
}
