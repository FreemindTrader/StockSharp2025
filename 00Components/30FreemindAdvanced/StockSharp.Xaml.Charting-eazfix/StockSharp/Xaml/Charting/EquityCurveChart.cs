// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.EquityCurveChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class EquityCurveChart : UserControl, IPersistable, IComponentConnector, IThemeableChart
{
  
  private readonly ScichartSurfaceMVVM \u0023\u003DzKj7nvWQ\u003D;
  
  private readonly ChartModifierBase[] \u0023\u003DzUyqHQCymOwtN;
  
  private bool \u0023\u003Dzlx9Rh5m0_e0t;
  
  private bool \u0023\u003DzjHU2QreifXYX = true;
  
  public Chart \u0023\u003DzO72kpz0\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public EquityCurveChart()
  {
    this.InitializeComponent();
    this.\u0023\u003DzKj7nvWQ\u003D = (ScichartSurfaceMVVM) this.\u0023\u003DzO72kpz0\u003D.DataContext;
    this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().XAxisType = ChartAxisType.CategoryDateTime;
    this.\u0023\u003DzKj7nvWQ\u003D.ShowLegend = true;
    ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.XAxises).First<IChartAxis>().AutoRange = true;
    ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.YAxises).First<IChartAxis>().AutoRange = true;
    ChartModifierBase[] v7UvhxrxhaatqEjdArray = new ChartModifierBase[4];
    fxZoomPanModifier ypbydebG6VffgcpzeEjd = new fxZoomPanModifier();
    ypbydebG6VffgcpzeEjd.XyDirection = XyDirection.XDirection;
    ypbydebG6VffgcpzeEjd.ClipModeX = ClipMode.None;
    v7UvhxrxhaatqEjdArray[0] = (ChartModifierBase) ypbydebG6VffgcpzeEjd;
    MouseWheelZoomModifier kufjwuuvR4YbN3Ejd = new MouseWheelZoomModifier();
    kufjwuuvR4YbN3Ejd.XyDirection = XyDirection.XDirection;
    v7UvhxrxhaatqEjdArray[1] = (ChartModifierBase) kufjwuuvR4YbN3Ejd;
    ZoomExtentsModifier fk4QgaphfmmujdEjd = new ZoomExtentsModifier();
    fk4QgaphfmmujdEjd.ExecuteOn = ExecuteOn.MouseDoubleClick;
    v7UvhxrxhaatqEjdArray[2] = (ChartModifierBase) fk4QgaphfmmujdEjd;
    YAxisDragModifier dgE2H48XyyA87SEjd = new YAxisDragModifier();
    dgE2H48XyyA87SEjd.AxisId = "Y";
    v7UvhxrxhaatqEjdArray[3] = (ChartModifierBase) dgE2H48XyyA87SEjd;
    this.\u0023\u003DzUyqHQCymOwtN = v7UvhxrxhaatqEjdArray;
    CollectionHelper.AddRange<IChartModifier>((ICollection<IChartModifier>) this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers, (IEnumerable<IChartModifier>) this.\u0023\u003DzUyqHQCymOwtN);
    ObservableCollection<IChartModifier> childModifiers = this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers;
    RolloverModifier jhzfqwrsvK3MyA6SqEjd = new RolloverModifier();
    jhzfqwrsvK3MyA6SqEjd.ShowAxisLabels = false;
    jhzfqwrsvK3MyA6SqEjd.UseInterpolation = false;
    \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartModifier> issiadppaSwGkbOr8 = new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartModifier>((IChartModifier) jhzfqwrsvK3MyA6SqEjd);
    CollectionHelper.AddRange<IChartModifier>((ICollection<IChartModifier>) childModifiers, (IEnumerable<IChartModifier>) issiadppaSwGkbOr8);
    this.\u0023\u003DzO72kpz0\u003D.PreviewMouseWheel += new MouseWheelEventHandler(this.\u0023\u003DzzPTbGgY94FCnML89IBWoh_E\u003D);
    this.\u0023\u003DzO72kpz0\u003D.PreviewMouseDoubleClick += new MouseButtonEventHandler(this.\u0023\u003DzihheewX0KTANlZbr4wNeex8\u003D);
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().YAxises, EquityCurveChart.SomeClass34343383.\u0023\u003DzFdH9dhR0fERarkwbZA\u003D\u003D ?? (EquityCurveChart.SomeClass34343383.\u0023\u003DzFdH9dhR0fERarkwbZA\u003D\u003D = new Action<IChartAxis>(EquityCurveChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzvXldtNp6EqOzZHGiHo5vu0E\u003D)));
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(false);
  }

  public string ChartTheme
  {
    get => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme;
    set => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme = value;
  }

  public bool NoGapMode
  {
    get => this.\u0023\u003DzjHU2QreifXYX;
    set
    {
      if (this.\u0023\u003DzjHU2QreifXYX == value)
        return;
      this.\u0023\u003DzjHU2QreifXYX = value;
      this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().XAxisType = value ? ChartAxisType.DateTime : ChartAxisType.CategoryDateTime;
    }
  }

  public IChartDrawData CreateData() => this.\u0023\u003DzO72kpz0\u003D.CreateData();

  public void Draw(IChartDrawData data) => this.\u0023\u003DzO72kpz0\u003D.Draw(data);

  public IEnumerable<IChartBandElement> Elements
  {
    get
    {
      List<IChartBandElement> chartBandElementList = new List<IChartBandElement>();
      chartBandElementList.AddRange(((IEnumerable) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Cast<IChartBandElement>());
      return (IEnumerable<IChartBandElement>) new \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D<IChartBandElement>(chartBandElementList);
    }
  }

  public IChartBandElement CreateCurve(string title, Color color, DrawStyles style, Guid id = default (Guid))
  {
    return this.CreateCurve(title, color, color, style, id);
  }

  public IChartBandElement CreateCurve(
    string title,
    Color color,
    Color secondColor,
    DrawStyles style,
    Guid id = default (Guid))
  {
    if (title == null)
      throw new ArgumentNullException(nameof (title));
    if (style == DrawStyles.Band || style == DrawStyles.Area)
      style = DrawStyles.BandOneValue;
    ChartBandElement chartBandElement = new ChartBandElement();
    chartBandElement.FullTitle = title;
    chartBandElement.Style = DrawStyles.BandOneValue;
    chartBandElement.Line1.ShowAxisMarker = true;
    chartBandElement.Line1.Color = color;
    chartBandElement.Line1.AdditionalColor = style != DrawStyles.BandOneValue ? Colors.Transparent : color.ToTransparent((byte) 50);
    chartBandElement.Line2.Color = secondColor;
    chartBandElement.Line2.AdditionalColor = style != DrawStyles.BandOneValue ? Colors.Transparent : secondColor.ToTransparent((byte) 50);
    ChartBandElement curve = chartBandElement;
    if (style != DrawStyles.BandOneValue)
    {
      curve.Line2.IsVisible = false;
      curve.AddExtraName("Line2");
      curve.SetName((IChartElement) curve.Line1, LocalizedStrings.Line2);
    }
    else
    {
      curve.SetName((IChartElement) curve.Line1, LocalizedStrings.Line2 + " 1");
      curve.SetName((IChartElement) curve.Line2, LocalizedStrings.Line2 + " 2");
    }
    if (id != new Guid())
      curve.Id = id;
    ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Add((IChartElement) curve);
    return (IChartBandElement) curve;
  }

  public void RemoveCurve(IChartBandElement elem)
  {
    if (elem == null)
      throw new ArgumentNullException(nameof (elem));
    ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Remove((IChartElement) elem);
  }

  public void Clear()
  {
    ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Clear();
  }

  public void Reset()
  {
    ScichartSurfaceMVVM zKj7nvWq = this.\u0023\u003DzKj7nvWQ\u003D;
    INotifyList<IChartElement> elements = this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements;
    int index = 0;
    IChartElement[] chartElementArray = new IChartElement[((ICollection<IChartElement>) elements).Count];
    foreach (IChartElement chartElement in (IEnumerable<IChartElement>) elements)
    {
      chartElementArray[index] = chartElement;
      ++index;
    }
    zKj7nvWq.Reset((IEnumerable<IChartElement>) new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<IChartElement>(chartElementArray));
  }

  public void Reset(IEnumerable<ICollection<LineData<DateTime>>> items)
  {
    this.\u0023\u003DzKj7nvWQ\u003D.Reset((IEnumerable<IChartElement>) items.Cast<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq>().Select<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq, IChartBandElement>(EquityCurveChart.SomeClass34343383.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D ?? (EquityCurveChart.SomeClass34343383.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D = new Func<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq, IChartBandElement>(EquityCurveChart.SomeClass34343383.SomeMethond0343.ResetY1Annotation))));
  }

  public void Reset(IEnumerable<IChartBandElement> elements)
  {
    this.\u0023\u003DzKj7nvWQ\u003D.Reset((IEnumerable<IChartElement>) elements);
  }

  private void \u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(bool _param1)
  {
    EquityCurveChart.SomeClass7237 doDcwiev7trI4Ny0 = new EquityCurveChart.SomeClass7237();
    doDcwiev7trI4Ny0.\u0023\u003DzCPrYc1Q\u003D = _param1;
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().XAxises, new Action<IChartAxis>(doDcwiev7trI4Ny0.\u0023\u003DzVqnnRFG05Ih7j0uGvs43D9hVOdQZ9EhNOA\u003D\u003D));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().YAxises, new Action<IChartAxis>(doDcwiev7trI4Ny0.\u0023\u003DziehXVo6PTkPmH7RBZ7Cax0tsQSrlFTQJ4Q\u003D\u003D));
    CollectionHelper.ForEach<ChartModifierBase>((IEnumerable<ChartModifierBase>) this.\u0023\u003DzUyqHQCymOwtN, new Action<ChartModifierBase>(doDcwiev7trI4Ny0.\u0023\u003DznlibN4a3alLRqIFZIAoGE2VjjgqY\u0024QcKlA\u003D\u003D));
  }

  public void Load(SettingsStorage storage)
  {
    this.Clear();
    foreach (IChartElement chartElement in ((IEnumerable<SettingsStorage>) storage.GetValue<SettingsStorage[]>("elements", (SettingsStorage[]) null)).Select<SettingsStorage, ChartBandElement>(EquityCurveChart.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (EquityCurveChart.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Func<SettingsStorage, ChartBandElement>(EquityCurveChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzROcaIyQWPap\u0024vfll6Q\u003D\u003D))))
      ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Add(chartElement);
  }

  public void Save(SettingsStorage storage)
  {
    storage.SetValue<SettingsStorage[]>("elements", this.Elements.Select<IChartBandElement, SettingsStorage>(EquityCurveChart.SomeClass34343383.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D ?? (EquityCurveChart.SomeClass34343383.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D = new Func<IChartBandElement, SettingsStorage>(EquityCurveChart.SomeClass34343383.SomeMethond0343.\u0023\u003DzoBMFm\u0024eaw3nqoL0onA\u003D\u003D))).ToArray<SettingsStorage>());
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/equitycurvechart.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003DzO72kpz0\u003D = (Chart) target;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }

  private void \u0023\u003DzzPTbGgY94FCnML89IBWoh_E\u003D(
    object _param1,
    MouseWheelEventArgs _param2)
  {
    if (this.\u0023\u003Dzlx9Rh5m0_e0t || _param2.Delta <= 0)
      return;
    this.\u0023\u003Dzlx9Rh5m0_e0t = true;
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(true);
  }

  private void \u0023\u003DzihheewX0KTANlZbr4wNeex8\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (!this.\u0023\u003Dzlx9Rh5m0_e0t)
      return;
    this.\u0023\u003Dzlx9Rh5m0_e0t = false;
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(false);
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly EquityCurveChart.SomeClass34343383 SomeMethond0343 = new EquityCurveChart.SomeClass34343383();
    public static Action<IChartAxis> \u0023\u003DzFdH9dhR0fERarkwbZA\u003D\u003D;
    public static Func<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq, IChartBandElement> \u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D;
    public static Func<SettingsStorage, ChartBandElement> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;
    public static Func<IChartBandElement, SettingsStorage> \u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D;

    public void \u0023\u003DzvXldtNp6EqOzZHGiHo5vu0E\u003D(IChartAxis _param1)
    {
      _param1.TextFormatting = "0.##";
    }

    public IChartBandElement ResetY1Annotation(
      EquityCurveChart.\u0023\u003DztNaqcZK2DJoq _param1)
    {
      return _param1.\u0023\u003Dzj_CyhS4\u003D();
    }

    public ChartBandElement \u0023\u003DzROcaIyQWPap\u0024vfll6Q\u003D\u003D(
      SettingsStorage _param1)
    {
      return PersistableHelper.Load<ChartBandElement>(_param1);
    }

    public SettingsStorage \u0023\u003DzoBMFm\u0024eaw3nqoL0onA\u003D\u003D(
      IChartBandElement _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }
  }

  private sealed class SomeClass7237
  {
    public bool \u0023\u003DzCPrYc1Q\u003D;

    public void \u0023\u003DzVqnnRFG05Ih7j0uGvs43D9hVOdQZ9EhNOA\u003D\u003D(IChartAxis _param1)
    {
      _param1.AutoRange = !this.\u0023\u003DzCPrYc1Q\u003D;
    }

    public void \u0023\u003DziehXVo6PTkPmH7RBZ7Cax0tsQSrlFTQJ4Q\u003D\u003D(IChartAxis _param1)
    {
      _param1.AutoRange = !this.\u0023\u003DzCPrYc1Q\u003D;
    }

    public void \u0023\u003DznlibN4a3alLRqIFZIAoGE2VjjgqY\u0024QcKlA\u003D\u003D(
      ChartModifierBase _param1)
    {
      _param1.IsEnabled = this.\u0023\u003DzCPrYc1Q\u003D;
    }
  }

  private sealed class \u0023\u003DztNaqcZK2DJoq : BaseList<LineData<DateTime>>
  {
    
    private readonly EquityCurveChart _parentElement;
    
    private readonly IChartBandElement _indicatorElement;

    public \u0023\u003DztNaqcZK2DJoq(EquityCurveChart _param1, IChartBandElement _param2)
    {
      this._parentElement = _param1 ?? throw new ArgumentNullException("parent");
      this._indicatorElement = _param2 ?? throw new ArgumentNullException("element");
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    public IChartBandElement \u0023\u003Dzj_CyhS4\u003D()
    {
      return this._indicatorElement;
    }

    protected virtual void OnAdded(LineData<DateTime> _param1)
    {
      ((BaseCollection<LineData<DateTime>, IList<LineData<DateTime>>>) this).OnAdded(_param1);
      IChartDrawData data = this._parentElement.\u0023\u003DzO72kpz0\u003D.CreateData();
      data.Group((DateTimeOffset) _param1.X).Add(this.\u0023\u003Dzj_CyhS4\u003D(), (double) _param1.Y, 0.0);
      this._parentElement.\u0023\u003DzO72kpz0\u003D.Draw(data);
    }

    protected virtual bool OnRemoving(LineData<DateTime> _param1)
    {
      throw new InvalidOperationException(LocalizedStrings.RemoveNotSupported);
    }

    protected virtual void OnCleared()
    {
      this._parentElement.\u0023\u003DzO72kpz0\u003D.Reset((IEnumerable<IChartElement>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartElement>((IChartElement) this.\u0023\u003Dzj_CyhS4\u003D()));
      ((BaseCollection<LineData<DateTime>, IList<LineData<DateTime>>>) this).OnCleared();
    }
  }
}
