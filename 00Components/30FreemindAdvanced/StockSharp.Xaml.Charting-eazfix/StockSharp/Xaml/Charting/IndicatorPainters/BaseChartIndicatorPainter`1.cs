// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.BaseChartIndicatorPainter`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

public abstract class BaseChartIndicatorPainter<TIndicator> : 
  ChartBaseViewModel,
  IPersistable,
  IChartIndicatorPainter
  where TIndicator : IIndicator
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly List<IChartElement> \u0023\u003DzqFRpave0Vtab = new List<IChartElement>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IChartIndicatorElement \u0023\u003DzqdET1btrCufwgzakJw\u003D\u003D;

  internal static IIndicatorColorProvider \u0023\u003Dzl7RImWAQVb2K()
  {
    return ChartHelper.EnsureColorProvider();
  }

  [Browsable(false)]
  public IChartIndicatorElement Element
  {
    get => this.\u0023\u003DzqdET1btrCufwgzakJw\u003D\u003D;
    private set => this.\u0023\u003DzqdET1btrCufwgzakJw\u003D\u003D = value;
  }

  [Browsable(false)]
  public IReadOnlyList<IChartElement> InnerElements
  {
    get => (IReadOnlyList<IChartElement>) this.\u0023\u003DzqFRpave0Vtab;
  }

  protected bool IsAttached => this.Element != null;

  private ChartIndicatorElement \u0023\u003DzcINLDhshur8x() => (ChartIndicatorElement) this.Element;

  private void \u0023\u003Dz0yXrIqwigzcF()
  {
    CollectionHelper.ForEach<\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X>(this.\u0023\u003DzcINLDhshur8x().ChildElements.OfType<\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X>(), BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D ?? (BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D = new Action<\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X>(BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzwjAYs6wFYWgvI8xvaRDrrWE\u003D)));
  }

  protected abstract bool OnDraw(
    TIndicator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data);

  public virtual bool Draw(IChartDrawData data)
  {
    if (!this.IsAttached)
      return false;
    List<ChartDrawData.IndicatorData> indicatorDataList = ((ChartDrawData) data).\u0023\u003DzaZ5Qc3xeNY95(this.Element);
    if (indicatorDataList == null || CollectionHelper.IsEmpty<ChartDrawData.IndicatorData>((ICollection<ChartDrawData.IndicatorData>) indicatorDataList))
    {
      this.\u0023\u003Dz0yXrIqwigzcF();
      return false;
    }
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D dkfA7SK9Zsjh7b7evY;
    dkfA7SK9Zsjh7b7evY.\u0023\u003DzQGpA0P4\u003D = new Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>>();
    foreach (ChartDrawData.IndicatorData indicatorData in indicatorDataList)
      BaseChartIndicatorPainter<TIndicator>.\u0023\u003DqHBqb1AYxgNo2ie3lzN8tC_nrhxBQRNPRtaqNxzADD3c\u003D(indicatorData.Value.Indicator, indicatorData.Time, indicatorData.Value, ref dkfA7SK9Zsjh7b7evY);
    return this.OnDraw((TIndicator) indicatorDataList[0].Value.Indicator, (IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>>) dkfA7SK9Zsjh7b7evY.\u0023\u003DzQGpA0P4\u003D);
  }

  public virtual void Reset()
  {
  }

  public void OnAttached(IChartIndicatorElement element)
  {
    this.Element = element;
    CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.InnerElements, new Action<IChartElement>(this.\u0023\u003Dz1QuGUAX1zkMSls2D\u0024DLiPPw\u003D));
  }

  public void OnDetached()
  {
    if (this.Element == null)
      return;
    CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>) this.InnerElements, new Action<IChartElement>(this.\u0023\u003DzELzX6nSZmdU0vOyq3PAcXl8\u003D));
    this.Element = (IChartIndicatorElement) null;
  }

  protected bool DrawValues(
    IList<ChartDrawData.IndicatorData> vals,
    IChartElement element,
    Func<ChartDrawData.IndicatorData, double> getValue)
  {
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D lvtppRwsYcyoelU8 = new BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D();
    lvtppRwsYcyoelU8.\u0023\u003DzVvGg4nk2a5lf = vals;
    lvtppRwsYcyoelU8.\u0023\u003DztbpwouM\u003D = getValue;
    if (lvtppRwsYcyoelU8.\u0023\u003DzVvGg4nk2a5lf == null)
      throw new ArgumentNullException(nameof (vals));
    return BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz4W7cGG\u0024hFbB\u0024(element, lvtppRwsYcyoelU8.\u0023\u003DzVvGg4nk2a5lf.Count, new Func<int, DateTime>(lvtppRwsYcyoelU8.\u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D), new Func<int, double>(lvtppRwsYcyoelU8.\u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D), (Func<int, double>) null, (Func<int, int>) null);
  }

  protected bool DrawValues(IList<ChartDrawData.IndicatorData> vals, IChartElement element)
  {
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D kiaDl76b0Nyu42rxJq = new BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D();
    kiaDl76b0Nyu42rxJq.\u0023\u003DzVvGg4nk2a5lf = vals;
    kiaDl76b0Nyu42rxJq.\u0023\u003DzRRvwDu67s9Rm = this;
    if (kiaDl76b0Nyu42rxJq.\u0023\u003DzVvGg4nk2a5lf == null)
      throw new ArgumentNullException(nameof (vals));
    return BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz4W7cGG\u0024hFbB\u0024(element, kiaDl76b0Nyu42rxJq.\u0023\u003DzVvGg4nk2a5lf.Count, new Func<int, DateTime>(kiaDl76b0Nyu42rxJq.\u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D), new Func<int, double>(kiaDl76b0Nyu42rxJq.\u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D), (Func<int, double>) null, new Func<int, int>(kiaDl76b0Nyu42rxJq.\u0023\u003DzI5IxWb1lSuTpD9hsyw\u003D\u003D));
  }

  protected bool DrawValues(
    IList<ChartDrawData.IndicatorData> vals1,
    IList<ChartDrawData.IndicatorData> vals2,
    IChartElement element)
  {
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D v4vdZv8GtEzAmB0rzFq = new BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzaLhe6nMpxxxN = vals1;
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzRRvwDu67s9Rm = this;
    v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz4e3i1kk9sNEQ = vals2;
    if (v4vdZv8GtEzAmB0rzFq.\u0023\u003DzaLhe6nMpxxxN == null)
      throw new ArgumentNullException(nameof (vals1));
    return BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz4W7cGG\u0024hFbB\u0024(element, v4vdZv8GtEzAmB0rzFq.\u0023\u003DzaLhe6nMpxxxN.Count, new Func<int, DateTime>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D), new Func<int, double>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D), new Func<int, double>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzI5IxWb1lSuTpD9hsyw\u003D\u003D), (Func<int, int>) null);
  }

  protected bool DrawValues(
    IList<ChartDrawData.IndicatorData> vals1,
    IList<ChartDrawData.IndicatorData> vals2,
    IChartElement element,
    Func<double, double, double> op)
  {
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D jq9Llz3ahZ2LrQl4 = new BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D();
    jq9Llz3ahZ2LrQl4.\u0023\u003DzaLhe6nMpxxxN = vals1;
    jq9Llz3ahZ2LrQl4.\u0023\u003Dz6BEwh7k\u003D = op;
    jq9Llz3ahZ2LrQl4.\u0023\u003DzRRvwDu67s9Rm = this;
    jq9Llz3ahZ2LrQl4.\u0023\u003Dz4e3i1kk9sNEQ = vals2;
    if (jq9Llz3ahZ2LrQl4.\u0023\u003DzaLhe6nMpxxxN == null)
      throw new ArgumentNullException(nameof (vals1));
    if (jq9Llz3ahZ2LrQl4.\u0023\u003Dz6BEwh7k\u003D == null)
      throw new ArgumentNullException(nameof (op));
    return BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz4W7cGG\u0024hFbB\u0024(element, jq9Llz3ahZ2LrQl4.\u0023\u003DzaLhe6nMpxxxN.Count, new Func<int, DateTime>(jq9Llz3ahZ2LrQl4.\u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D), new Func<int, double>(jq9Llz3ahZ2LrQl4.\u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D), (Func<int, double>) null, (Func<int, int>) null);
  }

  private static bool \u0023\u003Dz4W7cGG\u0024hFbB\u0024(
    IChartElement _param0,
    int _param1,
    Func<int, DateTime> _param2,
    Func<int, double> _param3,
    Func<int, double> _param4,
    Func<int, int> _param5)
  {
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D doDcwiev7trI4Ny0 = new BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D();
    doDcwiev7trI4Ny0.\u0023\u003DzlKtizqE\u003D = _param2;
    doDcwiev7trI4Ny0.\u0023\u003DzpXxsWzTWfg7d = _param3;
    doDcwiev7trI4Ny0.\u0023\u003Dz5Kb6DbUnfYSy = _param4;
    doDcwiev7trI4Ny0.\u0023\u003DzSD3FqrQ\u003D = _param5;
    if (!(_param0 is \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X uuxsVv2V6fHz4Vm4X))
      throw new InvalidOperationException("invalid chart element");
    return uuxsVv2V6fHz4Vm4X.\u0023\u003Dz2dQykb\u0024x9fU4(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Range(0, _param1).Select<int, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>(new Func<int, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>>(doDcwiev7trI4Ny0.\u0023\u003DzDejq7n39xBdAUCa3_A\u003D\u003D)).Cast<ChartDrawData.IDrawValue>(), _param1));
  }

  private double \u0023\u003DzHjhGbB8\u003D(IList<ChartDrawData.IndicatorData> _param1, int _param2)
  {
    if (_param1 == null || _param2 >= _param1.Count)
      return double.NaN;
    IIndicatorValue indicatorValue = _param1[_param2].Value;
    if (indicatorValue != null && !indicatorValue.IsEmpty)
    {
      IChart chart = this.Element.TryGetChart();
      if (chart == null)
        throw new InvalidOperationException($"Chart is not set for '{this.Element}'.");
      if (chart.ShowNonFormedIndicators || indicatorValue.IsFormed)
        return (double) indicatorValue.GetValue<Decimal>();
    }
    return double.NaN;
  }

  private static int \u0023\u003Dz6nBC9dM\u003D(
    IList<ChartDrawData.IndicatorData> _param0,
    int _param1)
  {
    return _param0 == null || _param1 >= _param0.Count || !(_param0[_param1].Value is ShiftedIndicatorValue shiftedIndicatorValue) || shiftedIndicatorValue.IsEmpty ? 0 : shiftedIndicatorValue.Shift;
  }

  protected void AddChildElement(IChartElement element)
  {
    BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzIcUXbzVqd1QHu2nAW1nzwT0\u003D vqd1Qhu2nAw1nzwT0 = new BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzIcUXbzVqd1QHu2nAW1nzwT0\u003D();
    vqd1Qhu2nAw1nzwT0.\u0023\u003DzRRvwDu67s9Rm = this;
    vqd1Qhu2nAw1nzwT0.\u0023\u003Dz_i6sZDg\u003D = element;
    if (!CollectionHelper.TryAdd<IChartElement>((ICollection<IChartElement>) this.\u0023\u003DzqFRpave0Vtab, vqd1Qhu2nAw1nzwT0.\u0023\u003Dz_i6sZDg\u003D))
      throw new ArgumentException(nameof (element));
    if (!this.IsAttached)
      return;
    GuiDispatcher.GlobalDispatcher.AddSyncAction(new Action(vqd1Qhu2nAw1nzwT0.\u0023\u003Dzcq6kjuER1auFcNKkPQ\u003D\u003D));
  }

  protected void RemoveChildElement(IChartElement element)
  {
    if (!this.\u0023\u003DzqFRpave0Vtab.Remove(element))
      throw new ArgumentException(nameof (element));
    if (!this.IsAttached)
      return;
    this.\u0023\u003DzcINLDhshur8x().RemoveChildElement(element);
  }

  public virtual void Load(SettingsStorage storage)
  {
  }

  public virtual void Save(SettingsStorage storage)
  {
  }

  internal static void \u0023\u003DqHBqb1AYxgNo2ie3lzN8tC_nrhxBQRNPRtaqNxzADD3c\u003D(
    IIndicator _param0,
    DateTime _param1,
    IIndicatorValue _param2,
    ref BaseChartIndicatorPainter<TIndicator>.\u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D _param3)
  {
    CollectionHelper.SafeAdd<IIndicator, IList<ChartDrawData.IndicatorData>>((IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>>) _param3.\u0023\u003DzQGpA0P4\u003D, _param0, BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzWz\u0024_6kj_uLxgW8Fq3Q\u003D\u003D ?? (BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzWz\u0024_6kj_uLxgW8Fq3Q\u003D\u003D = new Func<IIndicator, IList<ChartDrawData.IndicatorData>>(BaseChartIndicatorPainter<TIndicator>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzEr1L4a4MYK7N\u0024OD_XA\u003D\u003D))).Add(new ChartDrawData.IndicatorData(_param1, _param2));
    if (!(_param2 is ComplexIndicatorValue complexIndicatorValue))
      return;
    foreach (KeyValuePair<IIndicator, IIndicatorValue> innerValue in (IEnumerable<KeyValuePair<IIndicator, IIndicatorValue>>) complexIndicatorValue.InnerValues)
      BaseChartIndicatorPainter<TIndicator>.\u0023\u003DqHBqb1AYxgNo2ie3lzN8tC_nrhxBQRNPRtaqNxzADD3c\u003D(innerValue.Key, _param1, innerValue.Value, ref _param3);
  }

  private void \u0023\u003Dz1QuGUAX1zkMSls2D\u0024DLiPPw\u003D(IChartElement _param1)
  {
    this.\u0023\u003DzcINLDhshur8x().AddChildElement(_param1);
  }

  private void \u0023\u003DzELzX6nSZmdU0vOyq3PAcXl8\u003D(IChartElement _param1)
  {
    this.\u0023\u003DzcINLDhshur8x().RemoveChildElement(_param1);
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D>.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D>.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X> \u0023\u003Dz6FRqMaLO3vZJA57SJw\u003D\u003D;
    public static Func<IIndicator, IList<ChartDrawData.IndicatorData>> \u0023\u003DzWz\u0024_6kj_uLxgW8Fq3Q\u003D\u003D;

    internal void \u0023\u003DzwjAYs6wFYWgvI8xvaRDrrWE\u003D(
      \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X _param1)
    {
      _param1.\u0023\u003Dz0yXrIqwigzcF();
    }

    internal IList<ChartDrawData.IndicatorData> \u0023\u003DzEr1L4a4MYK7N\u0024OD_XA\u003D\u003D(
      IIndicator _param1)
    {
      return (IList<ChartDrawData.IndicatorData>) new List<ChartDrawData.IndicatorData>();
    }
  }

  private sealed class \u0023\u003DzIcUXbzVqd1QHu2nAW1nzwT0\u003D
  {
    public BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D> \u0023\u003DzRRvwDu67s9Rm;
    public IChartElement \u0023\u003Dz_i6sZDg\u003D;

    internal void \u0023\u003Dzcq6kjuER1auFcNKkPQ\u003D\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzcINLDhshur8x().AddChildElement(this.\u0023\u003Dz_i6sZDg\u003D);
      // ISSUE: explicit non-virtual call
      \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj tdnKj06Uu87Wzk09Wj = ((ChartArea) __nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.Element).PersistentChartArea).ViewModel();
      \u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB a4VgOpCeDiqsTdzB;
      if (!tdnKj06Uu87Wzk09Wj.\u0023\u003DzKDbpj6zM462r((IfxChartElement) this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzcINLDhshur8x(), out a4VgOpCeDiqsTdzB))
        return;
      a4VgOpCeDiqsTdzB.\u0023\u003DzkFJdjYoyxP8n((IEnumerable<\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D>(((\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X) this.\u0023\u003Dz_i6sZDg\u003D).\u0023\u003DzfuiyUvM\u003D(tdnKj06Uu87Wzk09Wj)));
    }
  }

  private sealed class \u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D
  {
    public IList<ChartDrawData.IndicatorData> \u0023\u003DzaLhe6nMpxxxN;
    public Func<double, double, double> \u0023\u003Dz6BEwh7k\u003D;
    public BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D> \u0023\u003DzRRvwDu67s9Rm;
    public IList<ChartDrawData.IndicatorData> \u0023\u003Dz4e3i1kk9sNEQ;

    internal DateTime \u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzaLhe6nMpxxxN[_param1].Time;
    }

    internal double \u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D(int _param1)
    {
      return this.\u0023\u003Dz6BEwh7k\u003D(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzHjhGbB8\u003D(this.\u0023\u003DzaLhe6nMpxxxN, _param1), this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzHjhGbB8\u003D(this.\u0023\u003Dz4e3i1kk9sNEQ, _param1));
    }
  }

  private sealed class \u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D
  {
    public IList<ChartDrawData.IndicatorData> \u0023\u003DzVvGg4nk2a5lf;
    public Func<ChartDrawData.IndicatorData, double> \u0023\u003DztbpwouM\u003D;

    internal DateTime \u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzVvGg4nk2a5lf[_param1].Time;
    }

    internal double \u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DztbpwouM\u003D(this.\u0023\u003DzVvGg4nk2a5lf[_param1]);
    }
  }

  [StructLayout(LayoutKind.Auto)]
  private struct \u0023\u003DzbMZ4DKfA7S\u0024k9ZSjh7b7evY\u003D
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>> \u0023\u003DzQGpA0P4\u003D;
  }

  private sealed class \u0023\u003Dzg5oaV4vdZV8GtEzAmB0rzFQ\u003D
  {
    public IList<ChartDrawData.IndicatorData> \u0023\u003DzaLhe6nMpxxxN;
    public BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D> \u0023\u003DzRRvwDu67s9Rm;
    public IList<ChartDrawData.IndicatorData> \u0023\u003Dz4e3i1kk9sNEQ;

    internal DateTime \u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzaLhe6nMpxxxN[_param1].Time;
    }

    internal double \u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzHjhGbB8\u003D(this.\u0023\u003DzaLhe6nMpxxxN, _param1);
    }

    internal double \u0023\u003DzI5IxWb1lSuTpD9hsyw\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzHjhGbB8\u003D(this.\u0023\u003Dz4e3i1kk9sNEQ, _param1);
    }
  }

  private sealed class \u0023\u003DzpcA\u0024d1dDoDCwiev7trI4NY0\u003D
  {
    public Func<int, DateTime> \u0023\u003DzlKtizqE\u003D;
    public Func<int, double> \u0023\u003DzpXxsWzTWfg7d;
    public Func<int, double> \u0023\u003Dz5Kb6DbUnfYSy;
    public Func<int, int> \u0023\u003DzSD3FqrQ\u003D;

    internal ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime> \u0023\u003DzDejq7n39xBdAUCa3_A\u003D\u003D(
      int _param1)
    {
      DateTime dateTime = this.\u0023\u003DzlKtizqE\u003D(_param1);
      double num1 = this.\u0023\u003DzpXxsWzTWfg7d(_param1);
      Func<int, double> z5Kb6DbUnfYsy = this.\u0023\u003Dz5Kb6DbUnfYSy;
      double num2 = z5Kb6DbUnfYsy != null ? z5Kb6DbUnfYsy(_param1) : double.NaN;
      Func<int, int> zSd3FqrQ = this.\u0023\u003DzSD3FqrQ\u003D;
      int num3 = zSd3FqrQ != null ? zSd3FqrQ(_param1) : 0;
      return ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>.\u0023\u003DzpxJeWbQ\u003D(dateTime, num1, num2, num3);
    }
  }

  private sealed class \u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D
  {
    public IList<ChartDrawData.IndicatorData> \u0023\u003DzVvGg4nk2a5lf;
    public BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D> \u0023\u003DzRRvwDu67s9Rm;

    internal DateTime \u0023\u003DzoedkyJHxKB4GO9h_Zg\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzVvGg4nk2a5lf[_param1].Time;
    }

    internal double \u0023\u003DzI1EnfZppMTtCDsn2yw\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzHjhGbB8\u003D(this.\u0023\u003DzVvGg4nk2a5lf, _param1);
    }

    internal int \u0023\u003DzI5IxWb1lSuTpD9hsyw\u003D\u003D(int _param1)
    {
      return BaseChartIndicatorPainter<\u0023\u003Dzt3swxfw\u003D>.\u0023\u003Dz6nBC9dM\u003D(this.\u0023\u003DzVvGg4nk2a5lf, _param1);
    }
  }
}
