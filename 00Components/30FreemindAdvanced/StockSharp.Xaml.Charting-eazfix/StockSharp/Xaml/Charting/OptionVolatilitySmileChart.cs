// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.OptionVolatilitySmileChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.MathLight.LinearAlgebra;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
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

public class OptionVolatilitySmileChart : 
  UserControl,
  IPersistable,
  IComponentConnector,
  IThemeableChart
{
  public static readonly DependencyProperty SmileStepProperty = DependencyProperty.Register(nameof (SmileStep), typeof (double), typeof (OptionVolatilitySmileChart), new PropertyMetadata((object) 10.0, new PropertyChangedCallback(OptionVolatilitySmileChart.\u0023\u003DzcLFqfxuYYRmEBVt_YQ\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ScichartSurfaceMVVM \u0023\u003DzKj7nvWQ\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ChartModifierBase[] \u0023\u003DzUyqHQCymOwtN;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly List<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA> \u0023\u003Dz1I2yIarPUD_j = new List<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dzlx9Rh5m0_e0t;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal Chart \u0023\u003DzO72kpz0\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public OptionVolatilitySmileChart()
  {
    this.InitializeComponent();
    this.\u0023\u003DzKj7nvWQ\u003D = (ScichartSurfaceMVVM) this.\u0023\u003DzO72kpz0\u003D.DataContext;
    this.\u0023\u003DzKj7nvWQ\u003D.ShowLegend = true;
    IChartAxis chartAxis1 = ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.XAxises).First<IChartAxis>();
    IChartAxis chartAxis2 = ((IEnumerable<IChartAxis>) this.\u0023\u003DzKj7nvWQ\u003D.Area.YAxises).First<IChartAxis>();
    chartAxis1.AutoRange = true;
    chartAxis2.AutoRange = true;
    chartAxis2.TextFormatting = "0.##";
    ChartModifierBase[] v7UvhxrxhaatqEjdArray = new ChartModifierBase[4];
    dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd ypbydebG6VffgcpzeEjd = new dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd();
    ypbydebG6VffgcpzeEjd.XyDirection = dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection;
    ypbydebG6VffgcpzeEjd.ClipModeX = dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None;
    v7UvhxrxhaatqEjdArray[0] = (ChartModifierBase) ypbydebG6VffgcpzeEjd;
    dje_z48XSEY4E7J7ZY268G4C2RR2SX8TP9XUT5MGB3Z3KUFJWUUVR4YBN3_ejd kufjwuuvR4YbN3Ejd = new dje_z48XSEY4E7J7ZY268G4C2RR2SX8TP9XUT5MGB3Z3KUFJWUUVR4YBN3_ejd();
    kufjwuuvR4YbN3Ejd.XyDirection = dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection;
    v7UvhxrxhaatqEjdArray[1] = (ChartModifierBase) kufjwuuvR4YbN3Ejd;
    dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd fk4QgaphfmmujdEjd = new dje_zNHZFRV6VYN2XDNU56GMDGQJ2YP79UFMBF66RXN4FK4QGAPHFMMUJD_ejd();
    fk4QgaphfmmujdEjd.ExecuteOn = dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseDoubleClick;
    v7UvhxrxhaatqEjdArray[2] = (ChartModifierBase) fk4QgaphfmmujdEjd;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd dgE2H48XyyA87SEjd = new dje_zHZJUNELMY3BAWUYNNRAVXVEJSS7HS9SSZHRJV76DGE2H48XYYA87S_ejd();
    dgE2H48XyyA87SEjd.AxisId = "Y";
    v7UvhxrxhaatqEjdArray[3] = (ChartModifierBase) dgE2H48XyyA87SEjd;
    this.\u0023\u003DzUyqHQCymOwtN = v7UvhxrxhaatqEjdArray;
    CollectionHelper.AddRange<IChartModifier>((ICollection<IChartModifier>) this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers, (IEnumerable<IChartModifier>) this.\u0023\u003DzUyqHQCymOwtN);
    ObservableCollection<IChartModifier> childModifiers = this.\u0023\u003DzKj7nvWQ\u003D.ChartModifier.ChildModifiers;
    dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd jhzfqwrsvK3MyA6SqEjd = new dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd();
    jhzfqwrsvK3MyA6SqEjd.ShowAxisLabels = false;
    jhzfqwrsvK3MyA6SqEjd.UseInterpolation = false;
    \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartModifier> issiadppaSwGkbOr8 = new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartModifier>((IChartModifier) jhzfqwrsvK3MyA6SqEjd);
    CollectionHelper.AddRange<IChartModifier>((ICollection<IChartModifier>) childModifiers, (IEnumerable<IChartModifier>) issiadppaSwGkbOr8);
    this.\u0023\u003DzO72kpz0\u003D.PreviewMouseWheel += new MouseWheelEventHandler(this.\u0023\u003DzBsahwWdfdEWjKOxCgMVywFE\u003D);
    this.\u0023\u003DzO72kpz0\u003D.PreviewMouseDoubleClick += new MouseButtonEventHandler(this.\u0023\u003DzHIax\u0024yo0Oo2CNG1giLPOKl4\u003D);
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(false);
  }

  public string ChartTheme
  {
    get => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme;
    set => this.\u0023\u003DzKj7nvWQ\u003D.SelectedTheme = value;
  }

  public double SmileStep
  {
    get => (double) this.GetValue(OptionVolatilitySmileChart.SmileStepProperty);
    set => this.SetValue(OptionVolatilitySmileChart.SmileStepProperty, (object) value);
  }

  public IEnumerable<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>> Elements
  {
    get
    {
      List<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>> keyValuePairList = new List<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>();
      keyValuePairList.AddRange(this.\u0023\u003Dz1I2yIarPUD_j.Select<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA, KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>(OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D ?? (OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D = new Func<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA, KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>(OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzWW8oopirOPrw56aXJ46tTLo\u003D))));
      return (IEnumerable<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>) new \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>(keyValuePairList);
    }
  }

  public ICollection<LineData<double>> CreateSmile(
    string title,
    Color color,
    DrawStyles style = DrawStyles.Line,
    Guid id = default (Guid))
  {
    return this.CreateSmile(title, color, color, style, id);
  }

  public ICollection<LineData<double>> CreateSmile(
    string title,
    Color color,
    Color secondColor,
    DrawStyles style = DrawStyles.Line,
    Guid id = default (Guid))
  {
    ChartVolatilitySmileElement volatilitySmileElement1 = new ChartVolatilitySmileElement();
    volatilitySmileElement1.FullTitle = StringHelper.ThrowIfEmpty(title, nameof (title));
    volatilitySmileElement1.Values.Color = color.FromWpf();
    volatilitySmileElement1.Values.AdditionalColor = secondColor.FromWpf();
    volatilitySmileElement1.Values.Style = DrawStyles.Dot;
    volatilitySmileElement1.Values.StrokeThickness = 8;
    volatilitySmileElement1.Values.ShowAxisMarker = false;
    volatilitySmileElement1.Smile.Color = color.FromWpf();
    volatilitySmileElement1.Smile.AdditionalColor = secondColor.FromWpf();
    volatilitySmileElement1.Smile.Style = style;
    volatilitySmileElement1.Smile.ShowAxisMarker = false;
    ChartVolatilitySmileElement volatilitySmileElement2 = volatilitySmileElement1;
    if (id != new Guid())
      volatilitySmileElement2.Id = id;
    OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA smile = new OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA(this, volatilitySmileElement2);
    this.\u0023\u003Dz1I2yIarPUD_j.Add(smile);
    return (ICollection<LineData<double>>) smile;
  }

  public void RemoveSmile(ICollection<LineData<double>> items)
  {
    if (!(items is OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA zItgG9Jh6wfpA))
      throw new ArgumentNullException(nameof (items));
    if (!this.\u0023\u003Dz1I2yIarPUD_j.Remove(zItgG9Jh6wfpA))
      return;
    zItgG9Jh6wfpA.Dispose();
  }

  public void Clear()
  {
    foreach (ICollection<LineData<double>> items in this.\u0023\u003Dz1I2yIarPUD_j.ToArray())
      this.RemoveSmile(items);
  }

  private static void \u0023\u003DzcLFqfxuYYRmEBVt_YQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((OptionVolatilitySmileChart) _param0).\u0023\u003Dz1I2yIarPUD_j.ForEach(OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyvA5_ZcIGt2EBuRCvA\u003D\u003D ?? (OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyvA5_ZcIGt2EBuRCvA\u003D\u003D = new Action<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA>(OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzfx9JP\u0024zTFhplZv0Th91qjYeBvcL3)));
  }

  private void \u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(bool _param1)
  {
    OptionVolatilitySmileChart.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D v4vdZv8GtEzAmB0rzFq = new OptionVolatilitySmileChart.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzCPrYc1Q\u003D = _param1;
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().XAxises, new Action<IChartAxis>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzVqnnRFG05Ih7j0uGvs43D9hVOdQZ9EhNOA\u003D\u003D));
    CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>) this.\u0023\u003DzO72kpz0\u003D.\u0023\u003DzigsRD8\u0024hw_SZ().YAxises, new Action<IChartAxis>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DziehXVo6PTkPmH7RBZ7Cax0tsQSrlFTQJ4Q\u003D\u003D));
    CollectionHelper.ForEach<ChartModifierBase>((IEnumerable<ChartModifierBase>) this.\u0023\u003DzUyqHQCymOwtN, new Action<ChartModifierBase>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DznlibN4a3alLRqIFZIAoGE2VjjgqY\u0024QcKlA\u003D\u003D));
  }

  IChartDrawData IThemeableChart.CreateData() => throw new NotSupportedException();

  void IThemeableChart.Draw(IChartDrawData data) => throw new NotSupportedException();

  public void Load(SettingsStorage storage)
  {
    this.ChartTheme = storage.GetValue<string>("ChartTheme", this.ChartTheme);
    this.Clear();
    CollectionHelper.ForEach<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA>(((IEnumerable<SettingsStorage>) storage.GetValue<SettingsStorage[]>("elements", (SettingsStorage[]) null)).Select<SettingsStorage, OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA>(new Func<SettingsStorage, OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA>(this.\u0023\u003DzROcaIyQWPap\u0024vfll6Q\u003D\u003D)), new Action<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA>(this.\u0023\u003DzXutiackwLewjRRecQA\u003D\u003D));
  }

  public void Save(SettingsStorage storage)
  {
    storage.SetValue<string>("ChartTheme", this.ChartTheme);
    storage.SetValue<SettingsStorage[]>("elements", this.\u0023\u003Dz1I2yIarPUD_j.Select<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA, SettingsStorage>(OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D ?? (OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D = new Func<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA, SettingsStorage>(OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzoBMFm\u0024eaw3nqoL0onA\u003D\u003D))).ToArray<SettingsStorage>());
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/optionvolatilitysmilechart.xaml", UriKind.Relative));
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

  private void \u0023\u003DzBsahwWdfdEWjKOxCgMVywFE\u003D(
    object _param1,
    MouseWheelEventArgs _param2)
  {
    if (this.\u0023\u003Dzlx9Rh5m0_e0t || _param2.Delta <= 0)
      return;
    this.\u0023\u003Dzlx9Rh5m0_e0t = true;
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(true);
  }

  private void \u0023\u003DzHIax\u0024yo0Oo2CNG1giLPOKl4\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (!this.\u0023\u003Dzlx9Rh5m0_e0t)
      return;
    this.\u0023\u003Dzlx9Rh5m0_e0t = false;
    this.\u0023\u003DzG8IJ51fy4J4_607kP0quVX8\u003D(false);
  }

  private OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA \u0023\u003DzROcaIyQWPap\u0024vfll6Q\u003D\u003D(
    SettingsStorage _param1)
  {
    OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA zItgG9Jh6wfpA = new OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA(this);
    zItgG9Jh6wfpA.Load(_param1);
    return zItgG9Jh6wfpA;
  }

  private void \u0023\u003DzXutiackwLewjRRecQA\u003D\u003D(
    OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA _param1)
  {
    this.\u0023\u003Dz1I2yIarPUD_j.Add(_param1);
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new OptionVolatilitySmileChart.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA, KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>> \u0023\u003Dz2VqJw9mMZbQw3wwD\u0024A\u003D\u003D;
    public static Action<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA> \u0023\u003DzyvA5_ZcIGt2EBuRCvA\u003D\u003D;
    public static Func<OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA, SettingsStorage> \u0023\u003DzlGkGNQogYMM2PMCQxA\u003D\u003D;

    internal KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement> \u0023\u003DzWW8oopirOPrw56aXJ46tTLo\u003D(
      OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA _param1)
    {
      return new KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>((ICollection<LineData<double>>) _param1, (IChartVolatilitySmileElement) _param1.\u0023\u003Dzj_CyhS4\u003D());
    }

    internal void \u0023\u003Dzfx9JP\u0024zTFhplZv0Th91qjYeBvcL3(
      OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA _param1)
    {
      _param1.Reset();
    }

    internal SettingsStorage \u0023\u003DzoBMFm\u0024eaw3nqoL0onA\u003D\u003D(
      OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }
  }

  private sealed class \u0023\u003DzITGG9JH6wfpA : 
    BaseList<LineData<double>>,
    IDisposable,
    IPersistable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly OptionVolatilitySmileChart _parentElement;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly List<LineData<double>> \u0023\u003DzEEABO3tZsceL = new List<LineData<double>>();
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double[] \u0023\u003DzIMYwtfTasyYu;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ChartVolatilitySmileElement _indicatorElement;

    public \u0023\u003DzITGG9JH6wfpA(
      OptionVolatilitySmileChart _param1,
      ChartVolatilitySmileElement _param2)
    {
      this._parentElement = _param1 ?? throw new ArgumentNullException("parent");
      this._indicatorElement = _param2 ?? throw new ArgumentNullException("elem");
      ((ICollection<IChartElement>) this.\u0023\u003DzASpaYkPu0U\u0024F().\u0023\u003DzigsRD8\u0024hw_SZ().Elements).Add((IChartElement) this.\u0023\u003Dzj_CyhS4\u003D());
    }

    public \u0023\u003DzITGG9JH6wfpA(OptionVolatilitySmileChart _param1)
      : this(_param1, new ChartVolatilitySmileElement())
    {
    }

    private Chart \u0023\u003DzASpaYkPu0U\u0024F()
    {
      return this._parentElement.\u0023\u003DzO72kpz0\u003D;
    }

    public ChartVolatilitySmileElement \u0023\u003Dzj_CyhS4\u003D()
    {
      return this._indicatorElement;
    }

    protected virtual void OnAdded(LineData<double> _param1)
    {
      this.Reset();
      ((BaseCollection<LineData<double>, IList<LineData<double>>>) this).OnAdded(_param1);
    }

    protected virtual void OnRemoved(LineData<double> _param1)
    {
      this.Reset();
      ((BaseCollection<LineData<double>, IList<LineData<double>>>) this).OnRemoved(_param1);
    }

    protected virtual void OnCleared()
    {
      this.Reset();
      ((BaseCollection<LineData<double>, IList<LineData<double>>>) this).OnCleared();
    }

    private void \u0023\u003DzgHn_8Jw\u003D()
    {
      this.\u0023\u003DzASpaYkPu0U\u0024F().Reset((IEnumerable<IChartElement>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<IChartElement>((IChartElement) this.\u0023\u003Dzj_CyhS4\u003D()));
      IChartDrawData data = this.\u0023\u003DzASpaYkPu0U\u0024F().CreateData();
      foreach (LineData<double> lineData in (BaseCollection<LineData<double>, IList<LineData<double>>>) this)
        data.Group(lineData.X).Add(this.\u0023\u003Dzj_CyhS4\u003D().Values, (double) lineData.Y);
      foreach (LineData<double> lineData in this.\u0023\u003DzEEABO3tZsceL)
        data.Group(lineData.X).Add(this.\u0023\u003Dzj_CyhS4\u003D().Smile, (double) lineData.Y);
      this.\u0023\u003DzASpaYkPu0U\u0024F().Draw(data);
    }

    public void Reset()
    {
      this.\u0023\u003DzEEABO3tZsceL.Clear();
      if (((BaseCollection<LineData<double>, IList<LineData<double>>>) this).Count < 3)
        return;
      double num1 = this.\u0023\u003DzQFEcYv_sDNlA();
      List<double> doubleList1 = new List<double>();
      doubleList1.AddRange(((IEnumerable<LineData<double>>) this).Select<LineData<double>, double>(OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D ?? (OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D = new Func<LineData<double>, double>(OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzRYrdyX\u0024NtiJBVs4ggQ\u003D\u003D))));
      double[] array1 = doubleList1.ToArray();
      List<double> doubleList2 = new List<double>();
      doubleList2.AddRange(((IEnumerable<LineData<double>>) this).Select<LineData<double>, double>(OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzQHsHz_ktErkb7vgTPQ\u003D\u003D ?? (OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzQHsHz_ktErkb7vgTPQ\u003D\u003D = new Func<LineData<double>, double>(OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzMAaHV4k3kfGyBjSv1w\u003D\u003D))));
      double[] array2 = doubleList2.ToArray();
      this.\u0023\u003DzIMYwtfTasyYu = new PolyFit(array1, array2, 2).Coeff;
      if (this.\u0023\u003DzIMYwtfTasyYu.Length != 3)
        return;
      double smileStep = this._parentElement.SmileStep;
      double num2 = Math.Min(Math.Max(0.0001, double.IsNaN(smileStep) ? 10.0 : smileStep), num1 / 2.0);
      double num3 = ((BaseCollection<LineData<double>, IList<LineData<double>>>) this)[((BaseCollection<LineData<double>, IList<LineData<double>>>) this).Count - 1].X + num2;
      for (double x = ((BaseCollection<LineData<double>, IList<LineData<double>>>) this)[0].X; x < num3; x += num2)
        this.\u0023\u003DzEEABO3tZsceL.Add(new LineData<double>()
        {
          X = x,
          Y = MathHelper.ToDecimal(this.\u0023\u003Dz4GNf2anVypsR(x)).GetValueOrDefault()
        });
      this.\u0023\u003DzgHn_8Jw\u003D();
    }

    private double \u0023\u003Dz4GNf2anVypsR(double _param1)
    {
      return this.\u0023\u003DzIMYwtfTasyYu[2] * _param1 * _param1 + this.\u0023\u003DzIMYwtfTasyYu[1] * _param1 + this.\u0023\u003DzIMYwtfTasyYu[0];
    }

    private double \u0023\u003DzQFEcYv_sDNlA()
    {
      double val1 = double.MaxValue;
      for (int index = 1; index < ((BaseCollection<LineData<double>, IList<LineData<double>>>) this).Count; ++index)
      {
        double val2 = ((BaseCollection<LineData<double>, IList<LineData<double>>>) this)[index].X - ((BaseCollection<LineData<double>, IList<LineData<double>>>) this)[index - 1].X;
        val1 = val2 > 0.0 ? Math.Min(val1, val2) : throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.NewXValueIsLessThanPrev, new object[2]
        {
          (object) ((BaseCollection<LineData<double>, IList<LineData<double>>>) this)[index].X,
          (object) ((BaseCollection<LineData<double>, IList<LineData<double>>>) this)[index - 1].X
        }));
      }
      return val1;
    }

    public void Dispose()
    {
      ((ICollection<IChartElement>) this.\u0023\u003DzASpaYkPu0U\u0024F().\u0023\u003DzigsRD8\u0024hw_SZ().Elements).Remove((IChartElement) this.\u0023\u003Dzj_CyhS4\u003D());
    }

    public void Load(SettingsStorage _param1)
    {
      PersistableHelper.Load((IPersistable) this.\u0023\u003Dzj_CyhS4\u003D(), _param1, "Element");
    }

    public void Save(SettingsStorage _param1)
    {
      _param1.SetValue<SettingsStorage>("Element", PersistableHelper.Save((IPersistable) this.\u0023\u003Dzj_CyhS4\u003D()));
    }

    [Serializable]
    private sealed class \u0023\u003Dz7qOdpi4\u003D
    {
      public static readonly OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new OptionVolatilitySmileChart.\u0023\u003DzITGG9JH6wfpA.\u0023\u003Dz7qOdpi4\u003D();
      public static Func<LineData<double>, double> \u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D;
      public static Func<LineData<double>, double> \u0023\u003DzQHsHz_ktErkb7vgTPQ\u003D\u003D;

      internal double \u0023\u003DzRYrdyX\u0024NtiJBVs4ggQ\u003D\u003D(LineData<double> _param1)
      {
        return _param1.X;
      }

      internal double \u0023\u003DzMAaHV4k3kfGyBjSv1w\u003D\u003D(LineData<double> _param1)
      {
        return (double) _param1.Y;
      }
    }
  }

  private sealed class \u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D
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
      ChartModifierBase _param1)
    {
      _param1.IsEnabled = this.\u0023\u003DzCPrYc1Q\u003D;
    }
  }
}
