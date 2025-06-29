// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.EquityCurveChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>The graphical component to display the equity curve.</summary>
/// <summary>EquityCurveChart</summary>
public class EquityCurveChart : UserControl, IThemeableChart, IPersistable, IComponentConnector
{
  
  private readonly IScichartSurfaceVM \u0023\u003DzKj7nvWQ\u003D;
  
  private readonly dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd[] \u0023\u003DzUyqHQCymOwtN;
  
  private bool \u0023\u003Dzlx9Rh5m0_e0t;
  
  private bool \u0023\u003DzjHU2QreifXYX = true;
  
  internal dje_zZY2QS9KRNTZS9HAG4USNMAU3MR49RS222URU893E_ejd \u0023\u003DzO72kpz0\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.EquityCurveChart" />.
  /// </summary>
  public EquityCurveChart()
  {
    this.InitializeComponent();
    this.\u0023\u003DzKj7nvWQ\u003D = (IScichartSurfaceVM) this.\u0023\u003DzO72kpz0\u003D.DataContext;
    this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().XAxisType = ChartAxisType.CategoryDateTime;
    this.\u0023\u003DzKj7nvWQ\u003D.ShowLegend = true;
    ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.XAxises).First<IChartAxis>().AutoRange = true;
    ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.YAxises).First<IChartAxis>().AutoRange = true;
    dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd[] v7UvhxrxhaatqEjdArray = new dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd[4];
    dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd ypbydebG6VffgcpzeEjd = new dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd();
    ypbydebG6VffgcpzeEjd.XyDirection = dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection;
    ypbydebG6VffgcpzeEjd.ClipModeX = dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None;
    v7UvhxrxhaatqEjdArray[0] = (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd) ypbydebG6VffgcpzeEjd;
    dje_z48XSEY4E7J7ZY268G4C2RR2SX8TP9XUT5MGB3Z3KUFJWUUVR4YBN3_ejd kufjwuuvR4YbN3Ejd = new dje_z48XSEY4E7J7ZY268G4C2RR2SX8TP9XUT5MGB3Z3KUFJWUUVR4YBN3_ejd();
    kufjwuuvR4YbN3Ejd.XyDirection = dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection;
    v7UvhxrxhaatqEjdArray[1] = (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd) kufjwuuvR4YbN3Ejd;
    dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd fk4QgaphfmmujdEjd = new dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd();
    fk4QgaphfmmujdEjd.ExecuteOn = dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseDoubleClick;
    v7UvhxrxhaatqEjdArray[2] = (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd) fk4QgaphfmmujdEjd;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd dgE2H48XyyA87SEjd = new dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd();
    dgE2H48XyyA87SEjd.AxisId = XXX.SSS(-539432528);
    v7UvhxrxhaatqEjdArray[3] = (dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd) dgE2H48XyyA87SEjd;
    this.\u0023\u003DzUyqHQCymOwtN = v7UvhxrxhaatqEjdArray;
    CollectionHelper.AddRange<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>((ICollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers, (IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) this.\u0023\u003DzUyqHQCymOwtN);
    ObservableCollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> childModifiers = this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers;
    dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd jhzfqwrsvK3MyA6SqEjd = new dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd();
    jhzfqwrsvK3MyA6SqEjd.ShowAxisLabels = false;
    jhzfqwrsvK3MyA6SqEjd.UseInterpolation = false;
    \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D> issiadppaSwGkbOr8 = new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>((\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D) jhzfqwrsvK3MyA6SqEjd);
    CollectionHelper.AddRange<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>((ICollection<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) childModifiers, (IEnumerable<\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D>) issiadppaSwGkbOr8);
    this.\u0023\u003DzO72kpz0\u003D.PreviewMouseWheel += new MouseWheelEventHandler(this.\u0023\u003DzzPTbGgY94FCnML89IBWoh_E\u003D);
    this.\u0023\u003DzO72kpz0\u003D.PreviewMouseDoubleClick += new MouseButtonEventHandler(this.\u0023\u003DzihheewX0KTANlZbr4wNeex8\u003D);
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().YAxises, EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzFdH9dhR0fERarkwbZA\u003D\u003D ?? (EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzFdH9dhR0fERarkwbZA\u003D\u003D = new Action<IChartAxis>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzvXldtNp6EqOzZHGiHo5vu0E\u003D)));
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(false);
  }

  /// <summary>The name of the graphic theme.</summary>
  public string ChartTheme
  {
    get => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme;
    set => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme = value;
  }

  /// <summary>
  /// To remove gaps (weekends, holidays) on the chart. When the mode is enabled and multiple curves are used then joint scaling will be lost. Enabled by default.
  /// </summary>
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

  /// <inheritdoc />
  public IChartDrawData CreateData() => this.\u0023\u003DzO72kpz0\u003D.CreateData();

  /// <inheritdoc />
  public void Draw(IChartDrawData data) => this.\u0023\u003DzO72kpz0\u003D.Draw(data);

  /// <summary>Elements.</summary>
  public IEnumerable<IChartBandElement> Elements
  {
    get
    {
      List<IChartBandElement> chartBandElementList = new List<IChartBandElement>();
      chartBandElementList.AddRange(((IEnumerable) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Cast<IChartBandElement>());
      return (IEnumerable<IChartBandElement>) new \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D<IChartBandElement>(chartBandElementList);
    }
  }

  /// <summary>To create new curve to draw the yield.</summary>
  /// <param name="title">The line title.</param>
  /// <param name="color">The line color.</param>
  /// <param name="style">The line drawing style. The default is <see cref="F:Ecng.Drawing.DrawStyles.Line" />.</param>
  /// <param name="id">Unique ID.</param>
  /// <returns>Chart element representing a line.</returns>
  public IChartBandElement CreateCurve(string title, Color color, DrawStyles style, Guid id = default (Guid))
  {
    return this.CreateCurve(title, color, color, style, id);
  }

  /// <summary>To create new curve to draw the yield.</summary>
  /// <param name="title">The line title.</param>
  /// <param name="color">The line color.</param>
  /// <param name="secondColor">The additional line color. It is used to draw <see cref="F:Ecng.Drawing.DrawStyles.Area" />.</param>
  /// <param name="style">The line drawing style. The default is <see cref="F:Ecng.Drawing.DrawStyles.Line" />.</param>
  /// <param name="id">Unique ID.</param>
  /// <returns>Chart element representing a line.</returns>
  public IChartBandElement CreateCurve(
    string title,
    Color color,
    Color secondColor,
    DrawStyles style,
    Guid id = default (Guid))
  {
    if (title == null)
      throw new ArgumentNullException(XXX.SSS(-539441346));
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
      curve.\u0023\u003DziQx4gl4\u003D(XXX.SSS(-539433438));
      curve.\u0023\u003Dz9i5WbtNpD44L((IChartElement) curve.Line1, LocalizedStrings.Line2);
    }
    else
    {
      curve.\u0023\u003Dz9i5WbtNpD44L((IChartElement) curve.Line1, LocalizedStrings.Line2 + XXX.SSS(-539441357));
      curve.\u0023\u003Dz9i5WbtNpD44L((IChartElement) curve.Line2, LocalizedStrings.Line2 + XXX.SSS(-539441398));
    }
    if (id != new Guid())
      curve.Id = id;
    ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Add((IChartElement) curve);
    return (IChartBandElement) curve;
  }

  /// <summary>To remove the yield curve from the chart.</summary>
  /// <param name="elem">The chart element obtained from the method <see cref="M:StockSharp.Xaml.Charting.EquityCurveChart.CreateCurve(System.String,System.Windows.Media.Color,Ecng.Drawing.DrawStyles,System.Guid)" />.</param>
  public void RemoveCurve(IChartBandElement elem)
  {
    if (elem == null)
      throw new ArgumentNullException(XXX.SSS(-539441379));
    ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Remove((IChartElement) elem);
  }

  /// <summary>To remove yield curves from the chart.</summary>
  public void Clear()
  {
    ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Clear();
  }

  /// <summary>To reset all chart elements.</summary>
  public void Reset()
  {
    IScichartSurfaceVM zKj7nvWq = this.\u0023\u003DzKj7nvWQ\u003D;
    INotifyList<IChartElement> elements = this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements;
    int index = 0;
    IChartElement[] chartElementArray = new IChartElement[((ICollection<IChartElement>) elements).Count];
    foreach (IChartElement chartElement in (IEnumerable<IChartElement>) elements)
    {
      chartElementArray[index] = chartElement;
      ++index;
    }
    zKj7nvWq.\u0023\u003DzYI36Ggg\u003D((IEnumerable<IChartElement>) new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D<IChartElement>(chartElementArray));
  }

  /// <summary>To reset specified chart elements.</summary>
  /// <param name="items">Elements to reset.</param>
  public void Reset(IEnumerable<ICollection<LineData<DateTime>>> items)
  {
    this.\u0023\u003DzKj7nvWQ\u003D.\u0023\u003DzYI36Ggg\u003D((IEnumerable<IChartElement>) items.Cast<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq>().Select<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq, IChartBandElement>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D ?? (EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D = new Func<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq, IChartBandElement>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzAVykYn0F15D5Ztnzfw\u003D\u003D))));
  }

  /// <summary>To reset specified chart elements.</summary>
  /// <param name="elements">Elements to reset.</param>
  public void Reset(IEnumerable<IChartBandElement> elements)
  {
    this.\u0023\u003DzKj7nvWQ\u003D.\u0023\u003DzYI36Ggg\u003D((IEnumerable<IChartElement>) elements);
  }

  private void \u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(bool _param1)
  {
    EquityCurveChart.\u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D doDcwiev7trI4Ny0 = new EquityCurveChart.\u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D();
    doDcwiev7trI4Ny0.\u0023\u003DzCPrYc1Q\u003D = _param1;
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().XAxises, new Action<IChartAxis>(doDcwiev7trI4Ny0.\u0023\u003DzVqnnRFG05Ih7j0uGvs43D9hVOdQZ9EhNOA\u003D\u003D));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().YAxises, new Action<IChartAxis>(doDcwiev7trI4Ny0.\u0023\u003DziehXVo6PTkPmH7RBZ7Cax0tsQSrlFTQJ4Q\u003D\u003D));
    CollectionHelper.ForEach<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>((IEnumerable<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>) this.\u0023\u003DzUyqHQCymOwtN, new Action<dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd>(doDcwiev7trI4Ny0.\u0023\u003DznlibN4a3alLRqIFZIAoGE2VjjgqY\u0024QcKlA\u003D\u003D));
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Load(SettingsStorage storage)
  {
    this.Clear();
    foreach (IChartElement chartElement in ((IEnumerable<SettingsStorage>) storage.GetValue<SettingsStorage[]>(XXX.SSS(-539441386), (SettingsStorage[]) null)).Select<SettingsStorage, ChartBandElement>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Func<SettingsStorage, ChartBandElement>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzROcaIyQWPap\u0024vfll6Q\u003D\u003D))))
      ((ICollection<IChartElement>) this.\u0023\u003DzKj7nvWQ\u003D.Area.Elements).Add(chartElement);
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public void Save(SettingsStorage storage)
  {
    storage.SetValue<SettingsStorage[]>(XXX.SSS(-539441386), this.Elements.Select<IChartBandElement, SettingsStorage>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D ?? (EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D = new Func<IChartBandElement, SettingsStorage>(EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzoBMFm\u0024eaw3nqoL0onA\u003D\u003D))).ToArray<SettingsStorage>());
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(XXX.SSS(-539441177), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003DzO72kpz0\u003D = (dje_zZY2QS9KRNTZS9HAG4USNMAU3MR49RS222URU893E_ejd) target;
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
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new EquityCurveChart.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<IChartAxis> \u0023\u003DzFdH9dhR0fERarkwbZA\u003D\u003D;
    public static Func<EquityCurveChart.\u0023\u003DztNaqcZK2DJoq, IChartBandElement> \u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D;
    public static Func<SettingsStorage, ChartBandElement> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;
    public static Func<IChartBandElement, SettingsStorage> \u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D;

    internal void \u0023\u003DzvXldtNp6EqOzZHGiHo5vu0E\u003D(IChartAxis _param1)
    {
      _param1.TextFormatting = XXX.SSS(-539441313);
    }

    internal IChartBandElement \u0023\u003DzAVykYn0F15D5Ztnzfw\u003D\u003D(
      EquityCurveChart.\u0023\u003DztNaqcZK2DJoq _param1)
    {
      return _param1.\u0023\u003Dzj_CyhS4\u003D();
    }

    internal ChartBandElement \u0023\u003DzROcaIyQWPap\u0024vfll6Q\u003D\u003D(
      SettingsStorage _param1)
    {
      return PersistableHelper.Load<ChartBandElement>(_param1);
    }

    internal SettingsStorage \u0023\u003DzoBMFm\u0024eaw3nqoL0onA\u003D\u003D(
      IChartBandElement _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }
  }

  private sealed class \u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D
  {
    public bool \u0023\u003DzCPrYc1Q\u003D;

    internal void \u0023\u003DzVqnnRFG05Ih7j0uGvs43D9hVOdQZ9EhNOA\u003D\u003D(IChartAxis _param1)
    {
      _param1.AutoRange = !this.\u0023\u003DzCPrYc1Q\u003D;
    }

    internal void \u0023\u003DziehXVo6PTkPmH7RBZ7Cax0tsQSrlFTQJ4Q\u003D\u003D(IChartAxis _param1)
    {
      _param1.AutoRange = !this.\u0023\u003DzCPrYc1Q\u003D;
    }

    internal void \u0023\u003DznlibN4a3alLRqIFZIAoGE2VjjgqY\u0024QcKlA\u003D\u003D(
      dje_zD3DVMB6QWGLYD9NXQ7JB76XAKKWUTGVEPUV7UVHXRXHAATQ_ejd _param1)
    {
      _param1.IsEnabled = this.\u0023\u003DzCPrYc1Q\u003D;
    }
  }

  private sealed class \u0023\u003DztNaqcZK2DJoq : BaseList<LineData<DateTime>>
  {
    
    private readonly EquityCurveChart \u0023\u003DzU\u0024_meog\u003D;
    
    private readonly IChartBandElement _indicatorElement;

    public \u0023\u003DztNaqcZK2DJoq(EquityCurveChart _param1, IChartBandElement _param2)
    {
      this.\u0023\u003DzU\u0024_meog\u003D = _param1 ?? throw new ArgumentNullException(XXX.SSS(-539441328));
      this._indicatorElement = _param2 ?? throw new ArgumentNullException(XXX.SSS(-539441371));
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
      IChartDrawData data = this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzO72kpz0\u003D.CreateData();
      data.Group((DateTimeOffset) _param1.X).Add(this.\u0023\u003Dzj_CyhS4\u003D(), (double) _param1.Y, 0.0);
      this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzO72kpz0\u003D.Draw(data);
    }

    protected virtual bool OnRemoving(LineData<DateTime> _param1)
    {
      throw new InvalidOperationException(LocalizedStrings.RemoveNotSupported);
    }

    protected virtual void OnCleared()
    {
      this.\u0023\u003DzU\u0024_meog\u003D.\u0023\u003DzO72kpz0\u003D.Reset((IEnumerable<IChartElement>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartElement>((IChartElement) this.\u0023\u003Dzj_CyhS4\u003D()));
      ((BaseCollection<LineData<DateTime>, IList<LineData<DateTime>>>) this).OnCleared();
    }
  }
}
