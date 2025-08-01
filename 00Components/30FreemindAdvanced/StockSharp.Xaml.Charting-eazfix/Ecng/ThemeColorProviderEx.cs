// Decompiled with JetBrains decompiler
// Type: #=zduViKcXTrKCfnYwdbArizvSbWeE5LHaB3CyMd$w=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
public sealed class ThemeColorProviderEx : 
  BindableObject ,
  IThemeProvider
{
  
  private Brush \u0023\u003DzPsgXVEWl1rncLpvu7Q\u003D\u003D;
  
  private Brush \u0023\u003Dzfqi6oaajC7jiVKKPPQ\u003D\u003D;
  
  private Brush _cursorLabelBackgroundBrush;
  
  private Brush _cursorLabelBorderBrush;
  
  private Brush _cursorLabelForeground;
  
  private Brush _cursorLineBrush;
  
  private Brush _defaultColumnFillBrush;
  
  private Color _defaultColumnOutlineColor;
  
  private Color _defaultLineSeriesColor;
  
  private Color _defaultDownBandFillColor;
  
  private Color _defaultDownBandLineColor;
  
  private Brush _defaultCandleDownBodyBrush;
  
  private Color _defaultCandleDownWickColor;
  
  private Brush _gridBackgroundBrush;
  
  private Brush _gridBorderBrush;
  
  private Brush \u0023\u003Dz3Rm3iZjVScDe0eALvA\u003D\u003D;
  
  private Brush _majorGridLinesBrush;
  
  private Brush _minorGridLinesBrush;
  
  private Brush _defaultMountainAreaBrush;
  
  private Color _defaultMountainLineColor;
  
  private Brush _defaultColorMapBrush;
  
  private Brush _overviewFillBrush;
  
  private Brush _rolloverLabelBackgroundBrush;
  
  private Brush _rolloverLabelBorderBrush;
  
  private Brush _rubberBandFillBrush;
  
  private Brush _rubberBandStrokeBrush;
  
  private Brush \u0023\u003DzP4awWuKKUxT2n4J18w\u003D\u003D;
  
  private Brush \u0023\u003Dzap74WFPU3iLE;
  
  private Brush \u0023\u003Dzk5ghe9hMNl\u0024p;
  
  private Brush _tickTextBrush;
  
  private Color _defaultUpBandFillColor;
  
  private Color _defaultUpBandLineColor;
  
  private Brush _defaultCandleUpBodyBrush;
  
  private Color _defaultCandleUpWickColor;
  
  private Color \u0023\u003DzQwNDTBB02BQM;
  
  private Brush _rolloverLineStroke;
  
  private Brush _scrollbarFillBrush;
  
  private Color _boxVolumeTimeframe2Color;
  
  private Color \u0023\u003DzcIz1D66ueGH4ksNMfPW2YZTA7NHC;
  
  private Color \u0023\u003DzlzAfzI2VYa0KbIVncvBFXBt36bnV;
  
  private Color \u0023\u003Dz8cVhrEgqpp0yBk9Chw\u003D\u003D;
  
  private Color \u0023\u003DzK\u0024_HFKxIyVRF7UOjjQ\u003D\u003D;
  
  private Color \u0023\u003DzKreKVOTT14wmqVT4SJblWBI\u003D;
  
  private Color \u0023\u003Dz8irkkLlHyMoYrRC8ONqJmOU\u003D;
  
  private Color \u0023\u003Dzi7gMwBaEi25hMUFE1Zcyejk\u003D;
  
  private Color \u0023\u003DzGUo7xQ0pG5bHr76JuUCiQKQ\u003D;
  
  private Color \u0023\u003DzCQBrhh\u0024vUP6E80YOsyu6M38\u003D;

  public void ApplyTheme(
    IThemeProvider _param1)
  {
    InterfaceHelpers2025.CopyInterfaceProperties<IThemeProvider>(_param1, (IThemeProvider) this);
  }

  public void ApplyTheme(ResourceDictionary _param1)
  {
    foreach (DictionaryEntry dictionaryEntry in _param1)
    {
      if (dictionaryEntry.Value is DependencyObject dependencyObject && !dependencyObject.IsSealed)
        FreezeHelper2025.SetFreeze(dependencyObject, true);
    }
    this.GridBorderBrush = (Brush) _param1[(object) "GridBorderBrush"];
    this.GridBackgroundBrush = (Brush) _param1[(object) "GridBackgroundBrush"];
    this.SciChartBackground = (Brush) _param1[(object) "SciChartBackground"];
    this.TickTextBrush = (Brush) _param1[(object) "TickTextBrush"];
    this.MajorGridLinesBrush = (Brush) _param1[(object) "MajorGridLineBrush"];
    this.MinorGridLinesBrush = (Brush) _param1[(object) "MinorGridLineBrush"];
    this.RolloverLineStroke = (Brush) _param1[(object) "RolloverLineBrush"];
    this.RolloverLabelBorderBrush = (Brush) _param1[(object) "LabelBorderBrush"];
    this.RolloverLabelBackgroundBrush = (Brush) _param1[(object) "LabelBackgroundBrush"];
    this.DefaultCandleUpWickColor = (Color) _param1[(object) "UpWickColor"];
    this.DefaultCandleDownWickColor = (Color) _param1[(object) "DownWickColor"];
    this.DefaultCandleUpBodyBrush = (Brush) _param1[(object) "UpBodyBrush"];
    this.DefaultCandleDownBodyBrush = (Brush) _param1[(object) "DownBodyBrush"];
    this.DefaultColumnOutlineColor = (Color) _param1[(object) "ColumnLineColor"];
    this.DefaultColumnFillBrush = (Brush) _param1[(object) "ColumnFillBrush"];
    this.DefaultLineSeriesColor = (Color) _param1[(object) "LineSeriesColor"];
    this.DefaultMountainAreaBrush = (Brush) _param1[(object) "MountainAreaBrush"];
    this.DefaultMountainLineColor = (Color) _param1[(object) "MountainLineColor"];
    this.DefaultColorMapBrush = (Brush) _param1[(object) "DefaultColorMapBrush"];
    this.DefaultDownBandFillColor = (Color) _param1[(object) "DownBandSeriesLineColor"];
    this.DefaultUpBandFillColor = (Color) _param1[(object) "UpBandSeriesLineColor"];
    this.DefaultUpBandLineColor = (Color) _param1[(object) "UpBandSeriesFillColor"];
    this.DefaultDownBandLineColor = (Color) _param1[(object) "DownBandSeriesFillColor"];
    this.RubberBandFillBrush = (Brush) _param1[(object) "RubberBandFillBrush"];
    this.RubberBandStrokeBrush = (Brush) _param1[(object) "RubberBandStrokeBrush"];
    this.CursorLabelForeground = (Brush) _param1[(object) "LabelForegroundBrush"];
    this.CursorLabelBackgroundBrush = (Brush) _param1[(object) "LabelBackgroundBrush"];
    this.CursorLabelBorderBrush = (Brush) _param1[(object) "LabelBorderBrush"];
    this.CursorLineBrush = (Brush) _param1[(object) "CursorLineBrush"];
    this.OverviewFillBrush = (Brush) _param1[(object) "OverviewFillBrush"];
    this.ScrollbarFillBrush = (Brush) _param1[(object) "ScrollbarFillBrush"];
    this.LegendBackgroundBrush = (Brush) _param1[(object) "LegendBackgroundBrush"];
    this.DefaultTextAnnotationBackground = (Brush) _param1[(object) "TextAnnotationBackground"];
    this.DefaultTextAnnotationForeground = (Brush) _param1[(object) "TextAnnotationForeground"];
    this.DefaultAxisMarkerAnnotationBackground = (Brush) _param1[(object) "TextAnnotationBackground"];
    this.DefaultAxisMarkerAnnotationForeground = (Brush) _param1[(object) "TextAnnotationForeground"];
    this.AxisBandsFill = (Color) _param1[(object) "AxisBandsFill"];
    this.BoxVolumeTimeframe2Color = (Color) _param1[(object) "BoxVolumeTimeframe2Color"];
    this.BoxVolumeTimeframe2FrameColor = (Color) _param1[(object) "BoxVolumeTimeframe2FrameColor"];
    this.BoxVolumeTimeframe3Color = (Color) _param1[(object) "BoxVolumeTimeframe3Color"];
    this.BoxVolumeCellFontColor = (Color) _param1[(object) "BoxVolumeCellFontColor"];
    this.BoxVolumeHighVolColor = (Color) _param1[(object) "BoxVolumeHighVolColor"];
    this.ClusterProfileSeparatorLineColor = (Color) _param1[(object) "ClusterProfileSeparatorLineColor"];
    this.ClusterProfileLineColor = (Color) _param1[(object) "ClusterProfileLineColor"];
    this.ClusterProfileTextColor = (Color) _param1[(object) "ClusterProfileTextColor"];
    this.ClusterProfileClusterColor = (Color) _param1[(object) "ClusterProfileClusterColor"];
    this.ClusterProfileClusterMaxColor = (Color) _param1[(object) "ClusterProfileClusterMaxColor"];
  }

  public Brush GridBorderBrush
  {
    get => this._gridBorderBrush;
    set
    {
      if (this._gridBorderBrush == value)
        return;
      this._gridBorderBrush = value;
      this.OnPropertyChanged(nameof (GridBorderBrush));
    }
  }

  public Brush GridBackgroundBrush
  {
    get => this._gridBackgroundBrush;
    set
    {
      if (this._gridBackgroundBrush == value)
        return;
      this._gridBackgroundBrush = value;
      this.OnPropertyChanged(nameof (GridBackgroundBrush));
    }
  }

  public Brush SciChartBackground
  {
    get => this.\u0023\u003DzP4awWuKKUxT2n4J18w\u003D\u003D;
    set
    {
      if (this.\u0023\u003DzP4awWuKKUxT2n4J18w\u003D\u003D == value)
        return;
      this.\u0023\u003DzP4awWuKKUxT2n4J18w\u003D\u003D = value;
      this.OnPropertyChanged(nameof (SciChartBackground));
    }
  }

  public Brush TickTextBrush
  {
    get => this._tickTextBrush;
    set
    {
      if (this._tickTextBrush == value)
        return;
      this._tickTextBrush = value;
      this.OnPropertyChanged(nameof (TickTextBrush));
    }
  }

  public Brush MajorGridLinesBrush
  {
    get => this._majorGridLinesBrush;
    set
    {
      if (this._majorGridLinesBrush == value)
        return;
      this._majorGridLinesBrush = value;
      this.OnPropertyChanged(nameof (MajorGridLinesBrush));
    }
  }

  public Brush MinorGridLinesBrush
  {
    get => this._minorGridLinesBrush;
    set
    {
      if (this._minorGridLinesBrush == value)
        return;
      this._minorGridLinesBrush = value;
      this.OnPropertyChanged(nameof (MinorGridLinesBrush));
    }
  }

  public Brush RolloverLineStroke
  {
    get => this._rolloverLineStroke;
    set
    {
      if (this._rolloverLineStroke == value)
        return;
      this._rolloverLineStroke = value;
      this.OnPropertyChanged(nameof (RolloverLineStroke));
    }
  }

  public Brush RolloverLabelBorderBrush
  {
    get => this._rolloverLabelBorderBrush;
    set
    {
      if (this._rolloverLabelBorderBrush == value)
        return;
      this._rolloverLabelBorderBrush = value;
      this.OnPropertyChanged(nameof (RolloverLabelBorderBrush));
    }
  }

  public Brush RolloverLabelBackgroundBrush
  {
    get => this._rolloverLabelBackgroundBrush;
    set
    {
      if (this._rolloverLabelBackgroundBrush == value)
        return;
      this._rolloverLabelBackgroundBrush = value;
      this.OnPropertyChanged(nameof (RolloverLabelBackgroundBrush));
    }
  }

  public Color DefaultCandleUpWickColor
  {
    get => this._defaultCandleUpWickColor;
    set
    {
      if (!(this._defaultCandleUpWickColor != value))
        return;
      this._defaultCandleUpWickColor = value;
      this.OnPropertyChanged(nameof (DefaultCandleUpWickColor));
    }
  }

  public Color DefaultCandleDownWickColor
  {
    get => this._defaultCandleDownWickColor;
    set
    {
      if (!(this._defaultCandleDownWickColor != value))
        return;
      this._defaultCandleDownWickColor = value;
      this.OnPropertyChanged(nameof (DefaultCandleDownWickColor));
    }
  }

  public Brush DefaultCandleUpBodyBrush
  {
    get => this._defaultCandleUpBodyBrush;
    set
    {
      if (this._defaultCandleUpBodyBrush == value)
        return;
      this._defaultCandleUpBodyBrush = value;
      this.OnPropertyChanged(nameof (DefaultCandleUpBodyBrush));
    }
  }

  public Brush DefaultCandleDownBodyBrush
  {
    get => this._defaultCandleDownBodyBrush;
    set
    {
      if (this._defaultCandleDownBodyBrush == value)
        return;
      this._defaultCandleDownBodyBrush = value;
      this.OnPropertyChanged(nameof (DefaultCandleDownBodyBrush));
    }
  }

  public Color DefaultColumnOutlineColor
  {
    get => this._defaultColumnOutlineColor;
    set
    {
      if (!(this._defaultColumnOutlineColor != value))
        return;
      this._defaultColumnOutlineColor = value;
      this.OnPropertyChanged(nameof (DefaultColumnOutlineColor));
    }
  }

  public Brush DefaultColumnFillBrush
  {
    get => this._defaultColumnFillBrush;
    set
    {
      if (this._defaultColumnFillBrush == value)
        return;
      this._defaultColumnFillBrush = value;
      this.OnPropertyChanged(nameof (DefaultColumnFillBrush));
    }
  }

  public Color DefaultLineSeriesColor
  {
    get => this._defaultLineSeriesColor;
    set
    {
      if (!(this._defaultLineSeriesColor != value))
        return;
      this._defaultLineSeriesColor = value;
      this.OnPropertyChanged(nameof (DefaultLineSeriesColor));
    }
  }

  public Color DefaultMountainLineColor
  {
    get => this._defaultMountainLineColor;
    set
    {
      if (!(this._defaultMountainLineColor != value))
        return;
      this._defaultMountainLineColor = value;
      this.OnPropertyChanged(nameof (DefaultMountainLineColor));
    }
  }

  public Brush DefaultMountainAreaBrush
  {
    get => this._defaultMountainAreaBrush;
    set
    {
      if (this._defaultMountainAreaBrush == value)
        return;
      this._defaultMountainAreaBrush = value;
      this.OnPropertyChanged(nameof (DefaultMountainAreaBrush));
    }
  }

  public Brush DefaultColorMapBrush
  {
    get => this._defaultColorMapBrush;
    set
    {
      if (this._defaultColorMapBrush == value)
        return;
      this._defaultColorMapBrush = value;
      this.OnPropertyChanged(nameof (DefaultColorMapBrush));
    }
  }

  public Color DefaultDownBandFillColor
  {
    get => this._defaultDownBandFillColor;
    set
    {
      if (!(this._defaultDownBandFillColor != value))
        return;
      this._defaultDownBandFillColor = value;
      this.OnPropertyChanged(nameof (DefaultDownBandFillColor));
    }
  }

  public Color DefaultUpBandFillColor
  {
    get => this._defaultUpBandFillColor;
    set
    {
      if (!(this._defaultUpBandFillColor != value))
        return;
      this._defaultUpBandFillColor = value;
      this.OnPropertyChanged(nameof (DefaultUpBandFillColor));
    }
  }

  public Color DefaultUpBandLineColor
  {
    get => this._defaultUpBandLineColor;
    set
    {
      if (!(this._defaultUpBandLineColor != value))
        return;
      this._defaultUpBandLineColor = value;
      this.OnPropertyChanged(nameof (DefaultUpBandLineColor));
    }
  }

  public Color DefaultDownBandLineColor
  {
    get => this._defaultDownBandLineColor;
    set
    {
      if (!(this._defaultDownBandLineColor != value))
        return;
      this._defaultDownBandLineColor = value;
      this.OnPropertyChanged(nameof (DefaultDownBandLineColor));
    }
  }

  public Brush CursorLabelForeground
  {
    get => this._cursorLabelForeground;
    set
    {
      if (this._cursorLabelForeground == value)
        return;
      this._cursorLabelForeground = value;
      this.OnPropertyChanged(nameof (CursorLabelForeground));
    }
  }

  public Brush CursorLabelBackgroundBrush
  {
    get => this._cursorLabelBackgroundBrush;
    set
    {
      if (this._cursorLabelBackgroundBrush == value)
        return;
      this._cursorLabelBackgroundBrush = value;
      this.OnPropertyChanged(nameof (CursorLabelBackgroundBrush));
    }
  }

  public Brush CursorLabelBorderBrush
  {
    get => this._cursorLabelBorderBrush;
    set
    {
      if (this._cursorLabelBorderBrush == value)
        return;
      this._cursorLabelBorderBrush = value;
      this.OnPropertyChanged(nameof (CursorLabelBorderBrush));
    }
  }

  public Brush RubberBandFillBrush
  {
    get => this._rubberBandFillBrush;
    set
    {
      if (this._rubberBandFillBrush == value)
        return;
      this._rubberBandFillBrush = value;
      this.OnPropertyChanged(nameof (RubberBandFillBrush));
    }
  }

  public Brush RubberBandStrokeBrush
  {
    get => this._rubberBandStrokeBrush;
    set
    {
      if (this._rubberBandStrokeBrush == value)
        return;
      this._rubberBandStrokeBrush = value;
      this.OnPropertyChanged(nameof (RubberBandStrokeBrush));
    }
  }

  public Brush CursorLineBrush
  {
    get => this._cursorLineBrush;
    set
    {
      if (this._cursorLineBrush == value)
        return;
      this._cursorLineBrush = value;
      this.OnPropertyChanged(nameof (CursorLineBrush));
    }
  }

  public Brush OverviewFillBrush
  {
    get => this._overviewFillBrush;
    set
    {
      if (this._overviewFillBrush == value)
        return;
      this._overviewFillBrush = value;
      this.OnPropertyChanged(nameof (OverviewFillBrush));
    }
  }

  public Brush ScrollbarFillBrush
  {
    get => this._scrollbarFillBrush;
    set
    {
      if (this._scrollbarFillBrush == value)
        return;
      this._scrollbarFillBrush = value;
      this.OnPropertyChanged(nameof (ScrollbarFillBrush));
    }
  }

  public Brush LegendBackgroundBrush
  {
    get => this.\u0023\u003Dz3Rm3iZjVScDe0eALvA\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dz3Rm3iZjVScDe0eALvA\u003D\u003D == value)
        return;
      this.\u0023\u003Dz3Rm3iZjVScDe0eALvA\u003D\u003D = value;
      this.OnPropertyChanged(nameof (LegendBackgroundBrush));
    }
  }

  public Brush DefaultTextAnnotationBackground
  {
    get => this.\u0023\u003Dzap74WFPU3iLE;
    set
    {
      if (this.\u0023\u003Dzap74WFPU3iLE == value)
        return;
      this.\u0023\u003Dzap74WFPU3iLE = value;
      this.OnPropertyChanged(nameof (DefaultTextAnnotationBackground));
    }
  }

  public Brush DefaultTextAnnotationForeground
  {
    get => this.\u0023\u003Dzk5ghe9hMNl\u0024p;
    set
    {
      if (this.\u0023\u003Dzk5ghe9hMNl\u0024p == value)
        return;
      this.\u0023\u003Dzk5ghe9hMNl\u0024p = value;
      this.OnPropertyChanged(nameof (DefaultTextAnnotationForeground));
    }
  }

  public Brush DefaultAxisMarkerAnnotationBackground
  {
    get => this.\u0023\u003DzPsgXVEWl1rncLpvu7Q\u003D\u003D;
    set
    {
      if (this.\u0023\u003DzPsgXVEWl1rncLpvu7Q\u003D\u003D == value)
        return;
      this.\u0023\u003DzPsgXVEWl1rncLpvu7Q\u003D\u003D = value;
      this.OnPropertyChanged(nameof (DefaultAxisMarkerAnnotationBackground));
    }
  }

  public Brush DefaultAxisMarkerAnnotationForeground
  {
    get => this.\u0023\u003Dzfqi6oaajC7jiVKKPPQ\u003D\u003D;
    set
    {
      if (this.\u0023\u003Dzfqi6oaajC7jiVKKPPQ\u003D\u003D == value)
        return;
      this.\u0023\u003Dzfqi6oaajC7jiVKKPPQ\u003D\u003D = value;
      this.OnPropertyChanged(nameof (DefaultAxisMarkerAnnotationForeground));
    }
  }

  public Color AxisBandsFill
  {
    get => this.\u0023\u003DzQwNDTBB02BQM;
    set
    {
      if (!(this.\u0023\u003DzQwNDTBB02BQM != value))
        return;
      this.\u0023\u003DzQwNDTBB02BQM = value;
      this.OnPropertyChanged(nameof (AxisBandsFill));
    }
  }

  public Color BoxVolumeTimeframe2Color
  {
    get => this._boxVolumeTimeframe2Color;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this._boxVolumeTimeframe2Color, value, nameof (BoxVolumeTimeframe2Color));
    }
  }

  public Color BoxVolumeTimeframe2FrameColor
  {
    get => this.\u0023\u003DzcIz1D66ueGH4ksNMfPW2YZTA7NHC;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003DzcIz1D66ueGH4ksNMfPW2YZTA7NHC, value, nameof (BoxVolumeTimeframe2FrameColor));
    }
  }

  public Color BoxVolumeTimeframe3Color
  {
    get => this.\u0023\u003DzlzAfzI2VYa0KbIVncvBFXBt36bnV;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003DzlzAfzI2VYa0KbIVncvBFXBt36bnV, value, nameof (BoxVolumeTimeframe3Color));
    }
  }

  public Color BoxVolumeCellFontColor
  {
    get => this.\u0023\u003Dz8cVhrEgqpp0yBk9Chw\u003D\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003Dz8cVhrEgqpp0yBk9Chw\u003D\u003D, value, nameof (BoxVolumeCellFontColor));
    }
  }

  public Color BoxVolumeHighVolColor
  {
    get => this.\u0023\u003DzK\u0024_HFKxIyVRF7UOjjQ\u003D\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003DzK\u0024_HFKxIyVRF7UOjjQ\u003D\u003D, value, nameof (BoxVolumeHighVolColor));
    }
  }

  public Color ClusterProfileSeparatorLineColor
  {
    get => this.\u0023\u003Dz8irkkLlHyMoYrRC8ONqJmOU\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003Dz8irkkLlHyMoYrRC8ONqJmOU\u003D, value, nameof (ClusterProfileSeparatorLineColor));
    }
  }

  public Color ClusterProfileLineColor
  {
    get => this.\u0023\u003DzKreKVOTT14wmqVT4SJblWBI\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003DzKreKVOTT14wmqVT4SJblWBI\u003D, value, nameof (ClusterProfileLineColor));
    }
  }

  public Color ClusterProfileTextColor
  {
    get => this.\u0023\u003Dzi7gMwBaEi25hMUFE1Zcyejk\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003Dzi7gMwBaEi25hMUFE1Zcyejk\u003D, value, nameof (ClusterProfileTextColor));
    }
  }

  public Color ClusterProfileClusterColor
  {
    get => this.\u0023\u003DzGUo7xQ0pG5bHr76JuUCiQKQ\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003DzGUo7xQ0pG5bHr76JuUCiQKQ\u003D, value, nameof (ClusterProfileClusterColor));
    }
  }

  public Color ClusterProfileClusterMaxColor
  {
    get => this.\u0023\u003DzCQBrhh\u0024vUP6E80YOsyu6M38\u003D;
    set
    {
      this.OnSetPropertyChanged<Color>(ref this.\u0023\u003DzCQBrhh\u0024vUP6E80YOsyu6M38\u003D, value, nameof (ClusterProfileClusterMaxColor));
    }
  }
}
