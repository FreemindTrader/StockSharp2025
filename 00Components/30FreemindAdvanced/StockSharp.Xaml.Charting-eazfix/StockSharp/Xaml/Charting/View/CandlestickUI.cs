// Decompiled with JetBrains decompiler
// Type: #=zNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml.Converters;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
public sealed class ChartCandleElementViewModel(
  ChartCandleElement _param1) : 
  ChartCompentWpfBaseViewModel<ChartCandleElement>(_param1),
  IXxxPaletteProvider
{
  
  private readonly OhlcDataSeries<DateTime, double> _ohlcDataSeries;
  
  private readonly XyDataSeries<DateTime, double> _xyDataSeries;
  
  private readonly SynchronizedList<CandlePatternElementViewModel> _chartPatternList;
  
  private ChartSeriesViewModel _chartSeriesViewModel;
  
  private TimeframeSegmentDataSeries _timeframeSegmentDataSeries;
  
  private DateTime \u0023\u003DzTqpoRUfBxm2O;
  
  private bool? \u0023\u003DzuuCm_xeHWuMy;
  
  private IndexRange  _indexRange;
  
  private ChartElementViewModel _openViewModel;
  
  private ChartElementViewModel _highViewModel;
  
  private ChartElementViewModel _lowViewModel;
  
  private ChartElementViewModel _closeViewModel;
  
  private ChartElementViewModel _lineViewModel;
  
  private ChartElementViewModel _volumeViewModel;
  
  private Decimal? _pnfBoxSize;
  
  private Func<DateTimeOffset, bool, bool, Color?> _colorerFunction;
  
  private readonly SynchronizedDictionary<DateTime, Color> _dateTime2ColorMap;
  
  private readonly ChartCandleElementViewModel.ChartCandleElementHelper _candleHelper = new ChartCandleElementViewModel.ChartCandleElementHelper(_param1);
  
  private bool \u0023\u003DzZ8C5xEdop1TJ;

  public OhlcDataSeries<DateTime, double> OhlcSeries
  {
    get => this._ohlcDataSeries;
  }

  public TimeSpan? CandlesTimeframe => this.OhlcSeries.Timeframe;

  private IDataSeries GetDataSeriesByDrawStyle()
  {
    if (this.ChartComponentView.DrawStyle.IsVolumeProfileChart())
      return (IDataSeries) this._timeframeSegmentDataSeries;
    return this.ChartComponentView.DrawStyle != ChartCandleDrawStyles.Area ? (IDataSeries) this.OhlcSeries : (IDataSeries) this._xyDataSeries;
  }

  public void AddPattern(
    CandlePatternElementViewModel _param1)
  {
    if (((BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>) this._chartPatternList).Contains(_param1))
      return;
    ((BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>) this._chartPatternList).Add(_param1);
  }

  public void RemovePattern(
    CandlePatternElementViewModel _param1)
  {
    ((BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>) this._chartPatternList).Remove(_param1);
  }

  protected override void Init()
  {
    base.Init();
    string[] strArray = new string[2]
    {
      "UpFillColor",
      "DownFillColor"
    };
    this.ChartViewModel.AddChild(this._openViewModel = new ChartElementViewModel("O", (INotifyPropertyChanged) this.ChartComponentView, new Func<SeriesInfo, Color>(this.GetSereisInfoColor), ChartCandleElementViewModel.SomeClass34343383.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D ?? (ChartCandleElementViewModel.SomeClass34343383.\u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D = new Func<SeriesInfo, string>(ChartCandleElementViewModel.SomeClass34343383.SomeMethond0343.Method01)), strArray));
    this.ChartViewModel.AddChild(this._highViewModel = new ChartElementViewModel("H", (INotifyPropertyChanged) this.ChartComponentView, new Func<SeriesInfo, Color>(this.GetSereisInfoColor), ChartCandleElementViewModel.SomeClass34343383.\u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D ?? (ChartCandleElementViewModel.SomeClass34343383.\u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D = new Func<SeriesInfo, string>(ChartCandleElementViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003DzyboOJPrkbYbHrtK_jYxCKz0\u003D)), strArray));
    this.ChartViewModel.AddChild(this._lowViewModel = new ChartElementViewModel("L", (INotifyPropertyChanged) this.ChartComponentView, new Func<SeriesInfo, Color>(this.GetSereisInfoColor), ChartCandleElementViewModel.SomeClass34343383.\u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D ?? (ChartCandleElementViewModel.SomeClass34343383.\u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D = new Func<SeriesInfo, string>(ChartCandleElementViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003Dz_F9udS\u0024bvXmu7p7kNM2l1tM\u003D)), strArray));
    this.ChartViewModel.AddChild(this._closeViewModel = new ChartElementViewModel("C", (INotifyPropertyChanged) this.ChartComponentView, new Func<SeriesInfo, Color>(this.GetSereisInfoColor), ChartCandleElementViewModel.SomeClass34343383.\u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D ?? (ChartCandleElementViewModel.SomeClass34343383.\u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D = new Func<SeriesInfo, string>(ChartCandleElementViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003Dz_2ZdaKSEm\u0024dowilMRXDN0r4\u003D)), strArray));
    this.ChartViewModel.AddChild(this._lineViewModel = new ChartElementViewModel("Line", (INotifyPropertyChanged) this.ChartComponentView, new Func<SeriesInfo, Color>(this.GetSereisInfoColor), new Func<SeriesInfo, string>(this.SetLineViewModelName), strArray));
    this.ChartViewModel.AddChild(this._volumeViewModel = new ChartElementViewModel("Vol", (INotifyPropertyChanged) this.ChartComponentView, new Func<SeriesInfo, Color>(this.GetSereisInfoColor), ChartCandleElementViewModel.SomeClass34343383.\u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D ?? (ChartCandleElementViewModel.SomeClass34343383.\u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D = new Func<SeriesInfo, string>(ChartCandleElementViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003Dzu0cWRPhAEXPa2F76HfbtuFw\u003D)), strArray));
    this.GuiInitSeries();
    ChartCompentWpfBaseViewModel<ChartCandleElement>.AddStylePropertyChanging<ChartCandleDrawStyles>((IChartComponent) this.ChartComponentView, "DrawStyle", new ChartCandleDrawStyles[10]
    {
      ChartCandleDrawStyles.CandleStick,
      ChartCandleDrawStyles.Ohlc,
      ChartCandleDrawStyles.PnF,
      ChartCandleDrawStyles.LineOpen,
      ChartCandleDrawStyles.LineHigh,
      ChartCandleDrawStyles.LineLow,
      ChartCandleDrawStyles.LineClose,
      ChartCandleDrawStyles.BoxVolume,
      ChartCandleDrawStyles.ClusterProfile,
      ChartCandleDrawStyles.Area
    });
  }

  private void GuiInitSeries()
  {
    if (!DrawableChartComponentBaseViewModel.IsUiThread())
    {
      this.PerformUiAction(new Action(this.GuiInitSeries), true);
    }
    else
    {
      this.InitTimeframeSegmentDataSeries();
      if (this.GetDataSeriesByDrawStyle() == null)
      {
        this.RemoveChartSeries();
      }
      else
      {
        if (this._chartSeriesViewModel != null)
        {
          Type type1 = this.GetRenderingSeriesByType();
          Type type2 = this._chartSeriesViewModel.RenderSeries.GetType();
          if (this._chartSeriesViewModel.DataSeries == this.GetDataSeriesByDrawStyle() && type1 == type2)
            return;
          BindingOperations.ClearAllBindings((DependencyObject) this._chartSeriesViewModel.RenderSeries);
        }
        if (this._chartSeriesViewModel == null || this._chartSeriesViewModel.DataSeries != this.GetDataSeriesByDrawStyle())
        {
          this.RemoveChartSeries();
          this.NewChartSeries();
        }
        else
        {
          this._chartSeriesViewModel.RenderSeries = (IRenderableSeries) this.CreateRenderableSeries();
          this.ClearAll();
          this.SetupAxisMarkerAndBinding(this._chartSeriesViewModel.RenderSeries, (IChartComponent) this.ChartComponentView, "ShowAxisMarker", (string) null);
        }
      }
    }
  }

  private void \u0023\u003DzywI2pEkeRxof()
  {
    if (this._timeframeSegmentDataSeries != null)
      return;
    this.GuiInitSeries();
  }

  private void \u0023\u003DzuqKjoe_5lTdYuAkGZw\u003D\u003D(object _param1)
  {
    if (this._pnfBoxSize.HasValue || !(_param1 is PnFArg pnFarg))
      return;
    this._pnfBoxSize = new Decimal?(pnFarg.BoxSize.Type == 1 ? pnFarg.BoxSize.Value : Unit.op_Explicit(pnFarg.BoxSize));
    if (!(this._chartSeriesViewModel.RenderSeries is FastXORenderableSeries renderSeries))
      return;
    renderSeries.XOBoxSize = (double) this._pnfBoxSize.Value;
  }

  private void InitTimeframeSegmentDataSeries()
  {
    TimeSpan? candlesTimeframe = this.CandlesTimeframe;
    TimeframeSegmentDataSeries zQ73Ei9NuGdXx = this._timeframeSegmentDataSeries;
    TimeframeSegmentDataSeries segmentDataSeries;
    if (zQ73Ei9NuGdXx != null)
    {
      TimeSpan? timeframe = zQ73Ei9NuGdXx.Timeframe;
      TimeSpan? nullable = candlesTimeframe;
      if ((timeframe.HasValue == nullable.HasValue ? (timeframe.HasValue ? (timeframe.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        segmentDataSeries = zQ73Ei9NuGdXx;
        goto label_4;
      }
    }
    segmentDataSeries = new TimeframeSegmentDataSeries(candlesTimeframe, this.ChartComponentView);
label_4:
    this._timeframeSegmentDataSeries = segmentDataSeries;
  }

  private Type GetRenderingSeriesByType()
  {
    switch (this.ChartComponentView.DrawStyle)
    {
      case ChartCandleDrawStyles.CandleStick:
        return typeof (FastCandlestickRenderableSeries);
      case ChartCandleDrawStyles.Ohlc:
        return typeof (FastOhlcRenderableSeries);
      case ChartCandleDrawStyles.LineOpen:
      case ChartCandleDrawStyles.LineHigh:
      case ChartCandleDrawStyles.LineLow:
      case ChartCandleDrawStyles.LineClose:
        return typeof (FastLineRenderableSeries);
      case ChartCandleDrawStyles.BoxVolume:
        return typeof (BoxVolumeRenderableSeries);
      case ChartCandleDrawStyles.ClusterProfile:
        return typeof (ClusterProfileRenderableSeries);
      case ChartCandleDrawStyles.Area:
        return typeof (FastMountainRenderableSeries);
      case ChartCandleDrawStyles.PnF:
        return typeof (FastXORenderableSeries);
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  protected override void RootElementPropertyChanged(
    IChartComponent _param1,
    string _param2)
  {
    base.RootElementPropertyChanged(_param1, _param2);
    if (!(_param2 == "DrawStyle"))
      return;
    this.GuiInitSeries();
  }

  private BaseRenderableSeries CreateRenderableSeries()
  {
    BaseRenderableSeries target;
    switch (this.ChartComponentView.DrawStyle)
    {
      case ChartCandleDrawStyles.CandleStick:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<FastCandlestickRenderableSeries>(new ChartElementViewModel[4]
        {
          this._openViewModel,
          this._highViewModel,
          this._lowViewModel,
          this._closeViewModel
        });
        target.SetBindings(FastCandlestickRenderableSeries.\u0023\u003DzQE5RIB4g32gf, (object) this.ChartComponentView, "UpFillColor", converter: (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
        target.SetBindings(FastCandlestickRenderableSeries.\u0023\u003DzzR4yyf\u0024wfFYI, (object) this.ChartComponentView, "DownFillColor", converter: (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
        target.SetBindings(FastCandlestickRenderableSeries.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) this.ChartComponentView, "UpBorderColor");
        target.SetBindings(FastCandlestickRenderableSeries.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) this.ChartComponentView, "DownBorderColor");
        target.SetBindings(BaseRenderableSeries.StrokeThicknessProperty, (object) this.ChartComponentView, "StrokeThickness");
        target.PaletteProvider = (IXxxPaletteProvider) this;
        break;
      case ChartCandleDrawStyles.Ohlc:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<FastOhlcRenderableSeries>(new ChartElementViewModel[4]
        {
          this._openViewModel,
          this._highViewModel,
          this._lowViewModel,
          this._closeViewModel
        });
        target.SetBindings(FastOhlcRenderableSeries.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) this.ChartComponentView, "UpBorderColor");
        target.SetBindings(FastOhlcRenderableSeries.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) this.ChartComponentView, "DownBorderColor");
        target.SetBindings(FastOhlcRenderableSeries.\u0023\u003DzVvc2lVdKTrj8, (object) this.ChartComponentView, "StrokeThickness", converter: (IValueConverter) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhiiYRKe0897RDjLr\u0024L9wcxjXImUKaPnpxZj0\u003D());
        BindingOperations.ClearBinding((DependencyObject) target, BaseRenderableSeries.StrokeThicknessProperty);
        target.PaletteProvider = (IXxxPaletteProvider) this;
        break;
      case ChartCandleDrawStyles.LineOpen:
      case ChartCandleDrawStyles.LineHigh:
      case ChartCandleDrawStyles.LineLow:
      case ChartCandleDrawStyles.LineClose:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<FastLineRenderableSeries>(new ChartElementViewModel[1]
        {
          this._lineViewModel
        });
        target.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this._candleHelper, "LineColor");
        target.SetBindings(BaseRenderableSeries.StrokeThicknessProperty, (object) this.ChartComponentView, "StrokeThickness");
        target.SetBindings(FastLineRenderableSeries.\u0023\u003DzGQjlrvOq17qko2MPPw\u003D\u003D, (object) this.ChartComponentView, "DrawStyle", converter: (IValueConverter) new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUm_WprFxHE3GMkMYTmM5Gv6xPE1lfu86yUSurnkA());
        break;
      case ChartCandleDrawStyles.BoxVolume:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<BoxVolumeRenderableSeries>(new ChartElementViewModel[1]
        {
          this._volumeViewModel
        });
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003Dz1cQ4wVMzHjSWdOxgiAXT\u0024gw\u003D, (object) this._candleHelper, "Timeframe2Color");
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003DzFin1pB0TmtTClsb5xicohbw\u003D, (object) this._candleHelper, "Timeframe2FrameColor");
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003Dzu_UBGSutECvla_Z4FBIjQVw\u003D, (object) this._candleHelper, "Timeframe3Color");
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003DzJmV2YqfjqzBN, (object) this._candleHelper, "FontColor");
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003Dz\u0024pEkT\u0024GyBsLd, (object) this._candleHelper, "MaxVolumeColor");
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003DzeOkHtL891mol_E40\u0024w\u003D\u003D, (object) this._candleHelper, "MaxVolumeBackground");
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D, (object) this.ChartComponentView, "Timeframe2Multiplier", BindingMode.OneWay, (IValueConverter) new ChartCandleElementViewModel.\u0023\u003DzBaQAMfOA0Xrd(this.CandlesTimeframe));
        target.SetBindings(BoxVolumeRenderableSeries.\u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D, (object) this.ChartComponentView, "Timeframe3Multiplier", BindingMode.OneWay, (IValueConverter) new ChartCandleElementViewModel.\u0023\u003DzBaQAMfOA0Xrd(this.CandlesTimeframe));
        break;
      case ChartCandleDrawStyles.ClusterProfile:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<ClusterProfileRenderableSeries>(new ChartElementViewModel[1]
        {
          this._volumeViewModel
        });
        target.SetBindings(ClusterProfileRenderableSeries.\u0023\u003Dz3vhKFHvmUfSR, (object) this._candleHelper, "ClusterSeparatorLineColor");
        target.SetBindings(ClusterProfileRenderableSeries.\u0023\u003DzTpSTo8U\u003D, (object) this._candleHelper, "ClusterLineColor");
        target.SetBindings(ClusterProfileRenderableSeries.\u0023\u003DzFLdFQ9M\u003D, (object) this._candleHelper, "ClusterTextColor");
        target.SetBindings(ClusterProfileRenderableSeries.\u0023\u003DzpELAaZtMaBgQ, (object) this._candleHelper, "ClusterColor");
        target.SetBindings(ClusterProfileRenderableSeries.\u0023\u003DzcL2qfCOpQZK5, (object) this._candleHelper, "ClusterMaxColor");
        break;
      case ChartCandleDrawStyles.Area:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<FastMountainRenderableSeries>(new ChartElementViewModel[1]
        {
          this._lineViewModel
        });
        target.SeriesColor = Colors.Transparent;
        target.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this._candleHelper, "LineColor");
        target.SetBindings(BaseRenderableSeries.StrokeThicknessProperty, (object) this.ChartComponentView, "StrokeThickness");
        target.SetBindings(BaseMountainRenderableSeries.\u0023\u003DzXc9apgJiH9mm, (object) this._candleHelper, "AreaColor", converter: (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
        break;
      case ChartCandleDrawStyles.PnF:
        target = (BaseRenderableSeries) this.CreateRenderableSeries<FastXORenderableSeries>(new ChartElementViewModel[4]
        {
          this._openViewModel,
          this._highViewModel,
          this._lowViewModel,
          this._closeViewModel
        });
        target.SetBindings(FastOhlcRenderableSeries.\u0023\u003DzofZZEllOaqFCpmhtlA\u003D\u003D, (object) this.ChartComponentView, "UpBorderColor");
        target.SetBindings(FastOhlcRenderableSeries.\u0023\u003DzGlIIfIPiLV_pixpxdA\u003D\u003D, (object) this.ChartComponentView, "DownBorderColor");
        target.SetBindings(BaseRenderableSeries.StrokeThicknessProperty, (object) this.ChartComponentView, "StrokeThickness");
        Decimal? zfIlpmP5Jfeem = this._pnfBoxSize;
        if (zfIlpmP5Jfeem.HasValue)
        {
          Decimal valueOrDefault = zfIlpmP5Jfeem.GetValueOrDefault();
          ((FastXORenderableSeries) target).XOBoxSize = (double) valueOrDefault;
        }
        target.PaletteProvider = (IXxxPaletteProvider) this;
        break;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
        {
          (object) this.ChartComponentView.DrawStyle
        }));
    }
    if (this.ChartComponentView.DrawStyle.IsVolumeProfileChart())
    {
      target.SetBindings(Control.FontFamilyProperty, (object) this.ChartComponentView, "FontFamily", converter: (IValueConverter) new FontFamilyValueConverter());
      target.SetBindings(Control.FontSizeProperty, (object) this.ChartComponentView, "FontSize", converter: (IValueConverter) new TypeCastConverter());
      target.SetBindings(Control.FontWeightProperty, (object) this.ChartComponentView, "FontWeight");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzclgWzqVX9aMz0ymbjg\u003D\u003D, (object) this.ChartComponentView, "PriceStep");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzQertKBH63fdmC8AJyRsykJc\u003D, (object) this.ChartComponentView, "ShowHorizontalVolumes");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003Dzi0KcPWeppPsdH7enWLwUIiA\u003D, (object) this.ChartComponentView, "LocalHorizontalVolumes");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzItT23YVSDC4D2EO\u0024iA\u003D\u003D, (object) this.ChartComponentView, "HorizontalVolumeWidthFraction");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzkZL83tvq\u0024ktX, (object) this._candleHelper, "HorizontalVolumeColor", BindingMode.OneWay, (IValueConverter) new Ecng.Xaml.Converters.ColorToBrushConverter());
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzagbwXpWOZg7bLqSU7A\u003D\u003D, (object) this._candleHelper, "HorizontalVolumeFontColor");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003Dz6m31HqKWmW3sf6kpzl00drQ\u003D, (object) this.ChartComponentView, "DrawSeparateVolumes");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzsiwadMWumB4q, (object) this._candleHelper, "BuyColor");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzyQkELvBo\u002432H, (object) this._candleHelper, "SellColor");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003Dzd0DbwufxB45U, (object) this._candleHelper, "UpColor");
      target.SetBindings(TimeframeSegmentRenderableSeries.\u0023\u003DzMymlj1BxH8MY, (object) this._candleHelper, "DownColor");
    }
    return target;
  }

  protected override void Clear()
  {
    this.RemoveChartSeries();
    this._candleHelper.Dispose();
  }

  protected override void UpdateUi()
  {
    this.OhlcSeries.Clear();
    this.OhlcSeries.Timeframe = new TimeSpan?();
    this._xyDataSeries.Clear();
    this._timeframeSegmentDataSeries = (TimeframeSegmentDataSeries) null;
    this.\u0023\u003DzTqpoRUfBxm2O = new DateTime();
    this._pnfBoxSize = new Decimal?();
    this._dateTime2ColorMap.Clear();
    this.PerformUiAction(new Action(this.\u0023\u003Dzp5J6G301TJ9o31R2wgChW2E\u003D), true);
  }

  private void NewChartSeries()
  {
    this._chartSeriesViewModel = new ChartSeriesViewModel(this.GetDataSeriesByDrawStyle(), (IRenderableSeries) this.CreateRenderableSeries());
    this.ScichartSurfaceMVVM.AddSeriesViewModelsToRoot(this.RootElem, (IRenderableSeries) this._chartSeriesViewModel);
    this.ClearAll();
    this.SetupAxisMarkerAndBinding(this._chartSeriesViewModel.RenderSeries, (IChartComponent) this.ChartComponentView, "ShowAxisMarker", (string) null);
  }

  private void RemoveChartSeries()
  {
    if (this._chartSeriesViewModel == null)
      return;
    this.ScichartSurfaceMVVM.RemoveChartComponent(this.RootElem);
    this._chartSeriesViewModel = (ChartSeriesViewModel) null;
  }

  public override void PerformPeriodicalAction()
  {
    base.PerformPeriodicalAction();
    VisibleRangeDpo g72ZksY7iW1Jk3iR = this.ScichartSurfaceMVVM.GetVisibleRangeDpo(this.ChartComponentView.XAxisId);
    if (g72ZksY7iW1Jk3iR == null)
      return;
    IndexRange  categoryDateTimeRange = g72ZksY7iW1Jk3iR.CategoryDateTimeRange;
    int count = this.OhlcSeries.Count;
    bool flag1 = count > 0 && categoryDateTimeRange.IsDefined && !this.ScichartSurfaceMVVM.Chart.IsAutoRange;
    bool flag2 = flag1 && this.ScichartSurfaceMVVM.Chart.IsAutoScroll;
    if (!this.\u0023\u003DzZ8C5xEdop1TJ & flag1)
    {
      this.\u0023\u003DzZ8C5xEdop1TJ = true;
      if (!flag2)
      {
        IndexRange  g8Oq2rGx6KyfAreq = new IndexRange (0, categoryDateTimeRange.Max - categoryDateTimeRange.Min);
        g72ZksY7iW1Jk3iR.CategoryDateTimeRange = g8Oq2rGx6KyfAreq;
      }
    }
    if (!flag2)
    {
      this.\u0023\u003DzuuCm_xeHWuMy = new bool?();
      this._indexRange = (IndexRange ) null;
    }
    else
    {
      int num1;
      if (this.\u0023\u003DzuuCm_xeHWuMy.HasValue && (!this.\u0023\u003DzuuCm_xeHWuMy.GetValueOrDefault() || this._indexRange != categoryDateTimeRange))
      {
        bool? zuuCmXeHwuMy = this.\u0023\u003DzuuCm_xeHWuMy;
        num1 = !(!zuuCmXeHwuMy.GetValueOrDefault() & zuuCmXeHwuMy.HasValue) ? 0 : (categoryDateTimeRange.Max >= count ? 1 : 0);
      }
      else
        num1 = 1;
      bool flag3 = num1 != 0;
      int num2 = count;
      if (flag3 && (categoryDateTimeRange.Max < num2 || !this.\u0023\u003DzuuCm_xeHWuMy.HasValue))
      {
        int num3 = categoryDateTimeRange.Max - categoryDateTimeRange.Min + 1;
        IndexRange  g8Oq2rGx6KyfAreq = new IndexRange (num2 - num3 + 1, num2);
        g72ZksY7iW1Jk3iR.CategoryDateTimeRange = g8Oq2rGx6KyfAreq;
        this._indexRange = g8Oq2rGx6KyfAreq;
      }
      else
        this._indexRange = categoryDateTimeRange;
      this.\u0023\u003DzuuCm_xeHWuMy = new bool?(flag3);
    }
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    switch (_param1 != null ? ((IEnumerable<ChartDrawData.IDrawValue>) _param1).FirstOrDefault<ChartDrawData.IDrawValue>() : (ChartDrawData.IDrawValue) null)
    {
      case null:
        return false;
      case ChartDrawData.sCandle _:
        return this.Draw(CollectionHelper.ToEx<ChartDrawData.sCandle>(((IEnumerable) _param1).Cast<ChartDrawData.sCandle>(), ((IEnumerableEx) _param1).Count));
      case ChartDrawData.sCandleColor _:
        return this.Draw(CollectionHelper.ToEx<ChartDrawData.sCandleColor>(((IEnumerable) _param1).Cast<ChartDrawData.sCandleColor>(), ((IEnumerableEx) _param1).Count));
      default:
        throw new ArgumentOutOfRangeException("values");
    }
  }

  private bool Draw(
    IEnumerableEx<ChartDrawData.sCandleColor> _param1)
  {
    if (CollectionHelper.IsEmpty<ChartDrawData.sCandleColor>((IEnumerable<ChartDrawData.sCandleColor>) _param1))
      return false;
    foreach (ChartDrawData.sCandleColor zs3gDb01RWCzVlS5w in (IEnumerable<ChartDrawData.sCandleColor>) _param1)
    {
      Color? color1 = zs3gDb01RWCzVlS5w.Color;
      if (color1.HasValue)
      {
        SynchronizedDictionary<DateTime, Color> zK1tfXeY7PpNb = this._dateTime2ColorMap;
        DateTime dateTime = zs3gDb01RWCzVlS5w.Time;
        color1 = zs3gDb01RWCzVlS5w.Color;
        Color color2 = color1.Value;
        zK1tfXeY7PpNb[dateTime] = color2;
      }
      else
        this._dateTime2ColorMap.Remove(zs3gDb01RWCzVlS5w.Time);
    }
    this._chartSeriesViewModel?.RenderSeries.Services()?.GetService<ISciChartSurface>()?.InvalidateElement();
    return true;
  }

  private bool Draw(
    IEnumerableEx<ChartDrawData.sCandle> _param1)
  {
    if (this._colorerFunction != this.ChartComponentView.Colorer)
    {
      this._colorerFunction = this.ChartComponentView.Colorer;
      this._chartSeriesViewModel?.RenderSeries.Services()?.GetService<ISciChartSurface>()?.InvalidateElement();
    }
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.sCandle>((IEnumerable<ChartDrawData.sCandle>) _param1))
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    DateTime dateTime = this.\u0023\u003DzTqpoRUfBxm2O;
    int index = -1;
    bool flag = false;
    DateTime[] array1 = new DateTime[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    double[] array4 = new double[count];
    double[] array5 = new double[count];
    foreach (ChartDrawData.sCandle zbzWrwPExZ6TzuVkEg in (IEnumerable<ChartDrawData.sCandle>) _param1)
    {
      object obj = zbzWrwPExZ6TzuVkEg.\u0023\u003DzdR0PhFO4Br84().Arg;
      if (obj is TimeSpan timeSpan)
        this.OhlcSeries.Timeframe = new TimeSpan?(timeSpan);
      this.\u0023\u003DzuqKjoe_5lTdYuAkGZw\u003D\u003D(obj);
      this.\u0023\u003DzywI2pEkeRxof();
      switch (zbzWrwPExZ6TzuVkEg.Time.CompareTo(dateTime))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zbzWrwPExZ6TzuVkEg.Time,
            (object) dateTime
          }));
        case 0:
          flag = true;
          this.OhlcSeries.UpdateOrderAdornerLayer(zbzWrwPExZ6TzuVkEg.Time, zbzWrwPExZ6TzuVkEg.\u0023\u003DzGze4a8XU7KvB(), zbzWrwPExZ6TzuVkEg.\u0023\u003DzolXXlhDBER_c(), zbzWrwPExZ6TzuVkEg.\u0023\u003DzchuwVU\u00245sIH8(), zbzWrwPExZ6TzuVkEg.Close);
          this._xyDataSeries.UpdateOrderAdornerLayer(zbzWrwPExZ6TzuVkEg.Time, zbzWrwPExZ6TzuVkEg.Close);
          if (zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8() != null && this._timeframeSegmentDataSeries != null)
          {
            foreach (CandlePriceLevel level in zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8())
              this._timeframeSegmentDataSeries.Update(zbzWrwPExZ6TzuVkEg.Time, (double) ((CandlePriceLevel) ref level).Price, level);
          }
          --count;
          break;
        default:
          ++index;
          array1[index] = zbzWrwPExZ6TzuVkEg.Time;
          array2[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003DzGze4a8XU7KvB();
          array3[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003DzolXXlhDBER_c();
          array4[index] = zbzWrwPExZ6TzuVkEg.\u0023\u003DzchuwVU\u00245sIH8();
          array5[index] = zbzWrwPExZ6TzuVkEg.Close;
          if (zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8() != null && this._timeframeSegmentDataSeries != null)
          {
            foreach (CandlePriceLevel level in zbzWrwPExZ6TzuVkEg.\u0023\u003Dzeu8tE1P9bfD8())
              this._timeframeSegmentDataSeries.Append(zbzWrwPExZ6TzuVkEg.Time, (double) ((CandlePriceLevel) ref level).Price, level);
            break;
          }
          break;
      }
      dateTime = zbzWrwPExZ6TzuVkEg.Time;
    }
    if (count == 0)
      return flag;
    Array.Resize<DateTime>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    Array.Resize<double>(ref array4, count);
    Array.Resize<double>(ref array5, count);
    this.OhlcSeries.\u0023\u003Dznc8esWY\u003D((IEnumerable<DateTime>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3, (IEnumerable<double>) array4, (IEnumerable<double>) array5);
    this._xyDataSeries.\u0023\u003Dznc8esWY\u003D((IEnumerable<DateTime>) array1, (IEnumerable<double>) array5);
    this.\u0023\u003DzTqpoRUfBxm2O = dateTime;
    return true;
  }

  Color? IXxxPaletteProvider.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3yzxavQxmzggPmZ8V62OI0Kr0hq2Km30eq101sCk(
    IRenderableSeries _param1,
    double _param2,
    double _param3)
  {
    return new Color?();
  }

  Color? IXxxPaletteProvider.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    return new Color?();
  }

  Color? IXxxPaletteProvider.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    int index = (int) _param2;
    DateTime xvalue = this.OhlcSeries.XValues[index];
    foreach (CandlePatternElementViewModel rri1f09FsCgNu6tg in (BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>) this._chartPatternList)
    {
      Color? nullable = rri1f09FsCgNu6tg.\u0023\u003Dzj4w_lAs\u003D(xvalue, _param6 > _param3);
      if (nullable.HasValue)
        return nullable;
    }
    Color color;
    if (this._dateTime2ColorMap.TryGetValue(xvalue, ref color))
      return new Color?(color);
    Func<DateTimeOffset, bool, bool, Color?> qjskTkReJlfF5mAk = this._colorerFunction;
    return qjskTkReJlfF5mAk == null ? new Color?() : qjskTkReJlfF5mAk(TimeHelper.ToDateTimeOffset(xvalue, TimeZoneInfo.Utc), _param6 >= _param3, index == this.OhlcSeries.Count - 1);
  }

  public static bool IsRising(
    SeriesInfo _param0)
  {
    return _param0 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg && vo1e0c8c41pWqbDkntdB13Yg.CloseValue > vo1e0c8c41pWqbDkntdB13Yg.OpenValue;
  }

  private Color GetSereisInfoColor(
    SeriesInfo _param1)
  {
    Color color;
    if (this._dateTime2ColorMap.TryGetValue(this.OhlcSeries.XValues[_param1.DataSeriesIndex], ref color))
      return color;
    return ChartCandleElementViewModel.IsRising(_param1) ? (!this._candleHelper.IsDark ? Colors.Green : Colors.LimeGreen) : (!this._candleHelper.IsDark ? Colors.Red : Colors.OrangeRed);
  }

  private string SetLineViewModelName(
    SeriesInfo _param1)
  {
    if (!(_param1 is XySeriesInfo uyciTixSveYm8Upq))
      return (string) null;
    switch (this.ChartComponentView.DrawStyle)
    {
      case ChartCandleDrawStyles.LineOpen:
        this._lineViewModel.Name = "O";
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.LineHigh:
        this._lineViewModel.Name = "H";
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.LineLow:
        this._lineViewModel.Name = "L";
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.LineClose:
        this._lineViewModel.Name = "C";
        return uyciTixSveYm8Upq.FormattedYValue;
      case ChartCandleDrawStyles.Area:
        this._lineViewModel.Name = "C";
        return uyciTixSveYm8Upq.FormattedYValue;
      default:
        this._lineViewModel.Value = (string) null;
        return (string) null;
    }
  }

  private void \u0023\u003Dzp5J6G301TJ9o31R2wgChW2E\u003D()
  {
    this.ClearAll();
    this.RemoveChartSeries();
    this.NewChartSeries();
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ChartCandleElementViewModel.SomeClass34343383 SomeMethond0343 = new ChartCandleElementViewModel.SomeClass34343383();
    public static Func<SeriesInfo, string> \u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D;

    public string Method01(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedOpenValue;
    }

    public string \u0023\u003DzyboOJPrkbYbHrtK_jYxCKz0\u003D(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedHighValue;
    }

    public string \u0023\u003Dz_F9udS\u0024bvXmu7p7kNM2l1tM\u003D(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedLowValue;
    }

    public string \u0023\u003Dz_2ZdaKSEm\u0024dowilMRXDN0r4\u003D(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedCloseValue;
    }

    public string \u0023\u003Dzu0cWRPhAEXPa2F76HfbtuFw\u003D(
      SeriesInfo _param1)
    {
      if (!(_param1 is \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc cs3fSnyvEdhmBS76Lhc))
        return string.Empty;
      string str = string.Empty;
      if (cs3fSnyvEdhmBS76Lhc.XValue != null)
        str = $"O:{cs3fSnyvEdhmBS76Lhc.FormattedOpenValue} H:{cs3fSnyvEdhmBS76Lhc.FormattedHighValue} L:{cs3fSnyvEdhmBS76Lhc.FormattedLowValue} C:{cs3fSnyvEdhmBS76Lhc.FormattedCloseValue}";
      return $"{str}   {cs3fSnyvEdhmBS76Lhc.Level}";
    }
  }

  private sealed class \u0023\u003DzBaQAMfOA0Xrd(TimeSpan? _param1) : IValueConverter
  {
    
    private readonly TimeSpan? \u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D = _param1;

    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) (!this.\u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D.HasValue || !(_param1 is int num) ? new TimeSpan?() : new TimeSpan?(TimeSpan.FromTicks((long) num * this.\u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D.Value.Ticks)));
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }

  private sealed class ChartCandleElementHelper : NotifiableObject, IDisposable
  {
    
    private readonly ChartCandleElement _candle;
    
    private IThemeProvider _themeProvider;
    
    private bool _isDarkTheme;

    public ChartCandleElementHelper(ChartCandleElement _param1)
    {
      this._candle = _param1 ?? throw new ArgumentNullException("elem");
      this._candle.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
      this.InitThemes();
      ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler(this.OnThemeChanged);
    }

    public void Dispose()
    {
      this._candle.PropertyChanged -= new PropertyChangedEventHandler(this.OnPropertyChanged);
      ThemeManager.ApplicationThemeChanged -= new ThemeChangedRoutedEventHandler(this.OnThemeChanged);
      GC.SuppressFinalize((object) this);
    }

    private void OnPropertyChanged(object _param1, PropertyChangedEventArgs _param2)
    {
      this.NotifyChanged(_param2.PropertyName);
    }

    private void OnThemeChanged(
      DependencyObject _param1,
      ThemeChangedRoutedEventArgs _param2)
    {
      this.InitThemes();
      this.NotifyChanged("LineColor");
      this.NotifyChanged("AreaColor");
      this.NotifyChanged("FontColor");
      this.NotifyChanged("Timeframe2Color");
      this.NotifyChanged("Timeframe2FrameColor");
      this.NotifyChanged("Timeframe3Color");
      this.NotifyChanged("MaxVolumeColor");
      this.NotifyChanged("MaxVolumeBackground");
      this.NotifyChanged("ClusterLineColor");
      this.NotifyChanged("ClusterSeparatorLineColor");
      this.NotifyChanged("ClusterTextColor");
      this.NotifyChanged("ClusterColor");
      this.NotifyChanged("ClusterMaxColor");
      this.NotifyChanged("HorizontalVolumeColor");
      this.NotifyChanged("HorizontalVolumeFontColor");
      this.NotifyChanged("BuyColor");
      this.NotifyChanged("SellColor");
      this.NotifyChanged("UpColor");
      this.NotifyChanged("DownColor");
    }

    private void InitThemes()
    {
      this.IsDark = ChartHelper.CurrChartTheme() == "ExpressionDark";
      this._themeProvider = ThemeManager.GetThemeProvider(ChartHelper.CurrChartTheme());
    }

    public bool IsDark
    {
      get => this._isDarkTheme;
      private set => this._isDarkTheme = value;
    }

    public Color LineColor
    {
      get
      {
        Color? lineColor = this._candle.LineColor;
        if (lineColor.HasValue)
          return lineColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Blue : Colors.Orange;
      }
      set => this._candle.LineColor = new Color?(value);
    }

    public Color AreaColor
    {
      get
      {
        Color? areaColor = this._candle.AreaColor;
        if (areaColor.HasValue)
          return areaColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Blue : Colors.Orange;
      }
      set => this._candle.AreaColor = new Color?(value);
    }

    public Color FontColor
    {
      get
      {
        Color? fontColor = this._candle.FontColor;
        if (fontColor.HasValue)
          return fontColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Black : Colors.White;
      }
      set => this._candle.FontColor = new Color?(value);
    }

    public Color Timeframe2Color
    {
      get
      {
        return this._candle.Timeframe2Color ?? this._themeProvider.get_BoxVolumeTimeframe2Color();
      }
      set => this._candle.Timeframe2Color = new Color?(value);
    }

    public Color Timeframe2FrameColor
    {
      get
      {
        return this._candle.Timeframe2FrameColor ?? this._themeProvider.get_BoxVolumeTimeframe2FrameColor();
      }
      set => this._candle.Timeframe2FrameColor = new Color?(value);
    }

    public Color Timeframe3Color
    {
      get
      {
        return this._candle.Timeframe3Color ?? this._themeProvider.get_BoxVolumeTimeframe3Color();
      }
      set => this._candle.Timeframe3Color = new Color?(value);
    }

    public Color MaxVolumeColor
    {
      get
      {
        return this._candle.MaxVolumeColor ?? this._themeProvider.get_BoxVolumeHighVolColor();
      }
      set => this._candle.MaxVolumeColor = new Color?(value);
    }

    public Color MaxVolumeBackground
    {
      get => this._candle.MaxVolumeBackground ?? Colors.LightBlue;
      set => this._candle.MaxVolumeBackground = new Color?(value);
    }

    public Color ClusterLineColor
    {
      get
      {
        return this._candle.ClusterLineColor ?? this._themeProvider.get_ClusterProfileLineColor();
      }
      set => this._candle.ClusterLineColor = new Color?(value);
    }

    public Color ClusterSeparatorLineColor
    {
      get
      {
        return this._candle.ClusterSeparatorLineColor ?? this._themeProvider.get_ClusterProfileSeparatorLineColor();
      }
      set => this._candle.ClusterSeparatorLineColor = new Color?(value);
    }

    public Color ClusterTextColor
    {
      get
      {
        return this._candle.ClusterTextColor ?? this._themeProvider.get_ClusterProfileTextColor();
      }
      set => this._candle.ClusterTextColor = new Color?(value);
    }

    public Color ClusterColor
    {
      get
      {
        return this._candle.ClusterColor ?? this._themeProvider.get_ClusterProfileClusterColor();
      }
      set => this._candle.ClusterColor = new Color?(value);
    }

    public Color ClusterMaxColor
    {
      get
      {
        return this._candle.ClusterMaxColor ?? this._themeProvider.get_ClusterProfileClusterMaxColor();
      }
      set => this._candle.ClusterMaxColor = new Color?(value);
    }

    public Color HorizontalVolumeColor
    {
      get => this._candle.HorizontalVolumeColor ?? Colors.Green;
      set => this._candle.HorizontalVolumeColor = new Color?(value);
    }

    public Color HorizontalVolumeFontColor
    {
      get
      {
        return this._candle.HorizontalVolumeFontColor ?? this._themeProvider.get_BoxVolumeCellFontColor();
      }
      set => this._candle.HorizontalVolumeFontColor = new Color?(value);
    }

    public Color BuyColor
    {
      get => this._candle.BuyColor ?? Colors.Green;
      set => this._candle.BuyColor = new Color?(value);
    }

    public Color SellColor
    {
      get => this._candle.SellColor ?? Colors.Red;
      set => this._candle.SellColor = new Color?(value);
    }

    public Color UpColor
    {
      get
      {
        Color? upColor = this._candle.UpColor;
        if (upColor.HasValue)
          return upColor.GetValueOrDefault();
        return !this.IsDark ? Colors.DarkGreen : Colors.Yellow;
      }
      set => this._candle.UpColor = new Color?(value);
    }

    public Color DownColor
    {
      get
      {
        Color? downColor = this._candle.DownColor;
        if (downColor.HasValue)
          return downColor.GetValueOrDefault();
        return !this.IsDark ? Colors.Red : Colors.DeepPink;
      }
      set => this._candle.DownColor = new Color?(value);
    }
  }
}
