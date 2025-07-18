// Decompiled with JetBrains decompiler
// Type: #=za9eQbgAsftIGbI_4wdfcZHvhZ0MvmL4S$g==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Schema;

#nullable disable
public sealed class \u0023\u003Dza9eQbgAsftIGbI_4wdfcZHvhZ0MvmL4S\u0024g\u003D\u003D : 
  IDrawable,
  IAxisParams,
  ISuspendable,
  IInvalidatableElement,
  IAxis,
  IHitTestable
{
  private IAxis \u0023\u003DzLXQXNXQ\u003D;
  private double \u0023\u003DzPVzRZY4RUs63fQH0UZlBwwQ\u003D;
  private double \u0023\u003DzWSKbtyDsDUrkj_6aPt4\u0024LAc\u003D;
  private bool \u0023\u003Dzu_GzT8bMWkGK5e7tIw\u003D\u003D;
  private EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> \u0023\u003Dz7F6WbE57tL8z;
  private EventHandler<EventArgs> \u0023\u003DzrN9MkyArV3jJ;
  private string \u0023\u003Dz2lo_hq36wM_r\u0024Us8fw\u003D\u003D;
  private bool \u0023\u003Dzs0Z9gR9q9AhlP_Xse1ll_RE\u003D;
  private ITickProvider \u0023\u003DzjIW6bZkM3WeYBMKCvw\u003D\u003D;
  private IRange \u0023\u003DzGmZr\u0024oMYjkAL1VmdhsWq7jY\u003D;
  private IRange \u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;
  private IRange \u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D;
  private double \u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
  private double \u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;
  private IRange<double> \u0023\u003DztXZBASMM9qIoqbN93Q\u003D\u003D;
  private IComparable \u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D;
  private IComparable \u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D;
  private IServiceContainer _serviceContainer;
  private ISciChartSurface \u0023\u003Dz5EWufkemTvDX91A_jQ\u003D\u003D;
  private Orientation \u0023\u003DzEEAnOu4SHH0UfRpYZA\u003D\u003D;
  private Brush \u0023\u003Dz54ElmcdEpyKjoyYgYbDr2BA\u003D;
  private Brush \u0023\u003DzSGGi4TgEUt5mUF3pihli_4g\u003D;
  private Style \u0023\u003DzSrJGExC1cFymeKbb7SnhZU0\u003D;
  private Style \u0023\u003DzqudCmo6OH6TsUzsHsLaqGC4\u003D;
  private Style \u0023\u003DzE7CsFgaAmnjF6\u0024fHfpvv6Bc\u003D;
  private Style \u0023\u003DzppPsVcgxelS_HKNLg8lTiOc\u003D;
  private dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd \u0023\u003Dz63qhRFjKOsUggrHep98Xryc\u003D;
  private bool \u0023\u003DzUIGQlR2nTtVHv7f_sPdYlsk\u003D;
  private string \u0023\u003DzQNPprkAewzNuJ8BKgQ\u003D\u003D;
  private string \u0023\u003DzcfCQIxVozaICVFl_NBxBzog\u003D;
  private ILabelProvider \u0023\u003DzF89yE4I0sNZToajVmw\u003D\u003D;
  private bool \u0023\u003Dzd_LGQNec_oZzeIc6UgVO1bo\u003D;
  private bool \u0023\u003Dz6u7rpXiPZ0ELNt9NTNmaSBs\u003D;
  private bool \u0023\u003Dz87HLNmE7ZOYXhABhXdXP_Qs\u003D;
  private bool \u0023\u003DzRGFzsYDyR61IZsdwtsP46tM\u003D;
  private bool \u0023\u003DzgppbQROkBQEoPZ\u002401W2LKjk\u003D;
  private bool \u0023\u003DzlgpKpTWQTghq2XsMH3\u0024yh3g\u003D;
  private string \u0023\u003DzE44SwWOPIA7RUKWFl3AfSyE\u003D;
  private Brush \u0023\u003Dz76rrAqkZX_yTjoL8V15HDUc\u003D;
  private bool \u0023\u003Dzh4plG7YN9ToCsd7Z8841Rh8ERp_W;
  private bool \u0023\u003Dz\u0024v09X5oMHHrLRmrA\u0024iFjCV8\u003D;
  private bool \u0023\u003Dz9sq2bpQA7MkL9vtU0QFqNQU\u003D;
  private bool \u0023\u003Dzv5vEPhr33\u0024lD\u0024hy1EJpF8rrpNEYh;
  private bool \u0023\u003Dz5wl2rj9xJzaaRIuhQA9JjD3JTxoe;
  private HorizontalAlignment \u0023\u003DzEVNVu_ZK7\u00248NZaMf6lPJDrQ\u003D;
  private VerticalAlignment \u0023\u003DzD\u0024vImVfDGuVPRP1kiGxuWsE\u003D;
  private \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3 \u0023\u003Dz3roz5ID2WsvKJYjSPpMBQF4\u003D;
  private ChartAxisAlignment \u0023\u003Dz_emvXNgm7Xa94MS7OGNgTK0\u003D;
  private bool \u0023\u003DzxkxRfeJKyToxpPT4fWwas78\u003D;
  private bool \u0023\u003DzHZdoCrtkBoXvozMBD\u0024zQ6WHB3IFoPNT1\u0024A\u003D\u003D;
  private bool \u0023\u003DzE8wAOIfSLDbe62ukKlhboN5j5Pb3;
  private bool \u0023\u003DzmugSdmHGd\u00241nPW6PJdIoa0A\u003D;
  private bool \u0023\u003DzU4h9CnMMJpfE\u0024Yp9vRVQDl4\u003D;
  private IAnnotationCanvas \u0023\u003DzeBpVkMb4JTc7krWreljVw38\u003D;
  private Visibility \u0023\u003DzYst3SO_\u0024ax6JKrNE9g\u003D\u003D;
  private bool \u0023\u003Dz95Z1kAUJHBMg6bO6WMkzJg_U4AV0;
  private IRange \u0023\u003Dz6D\u0024QoAqBdRidfWOtLpZV44k\u003D;
  private \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D \u0023\u003DzoCK1qsWB8frOWaD56ETphhG4QNUY;
  private IComparable \u0023\u003Dzp5XbM\u0024uN17Ek79xiOJ2Q8kg\u003D;
  private bool \u0023\u003DzRoux7W5jZAVE88YvvTzQyR6QfsrTykyeBA\u003D\u003D;
  private double \u0023\u003Dz95UBp1xHzQQZTB6VIAc\u0024oKE\u003D;
  private double \u0023\u003DzBZa4I2\u0024V0AAJfaHpg8DsUV4\u003D;

  public \u0023\u003Dza9eQbgAsftIGbI_4wdfcZHvhZ0MvmL4S\u0024g\u003D\u003D(
    IAxis _param1)
  {
    this.VisibleRange = _param1.VisibleRange;
    this.\u0023\u003DzDdpIQsZIDiEk(_param1.get_IsCategoryAxis());
    this.Id = _param1.Id;
    this.\u0023\u003DzLXQXNXQ\u003D = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public double ActualWidth => this.\u0023\u003DzPVzRZY4RUs63fQH0UZlBwwQ\u003D;

  private void \u0023\u003DzdEkXh9u7QiOB(double _param1)
  {
    this.\u0023\u003DzPVzRZY4RUs63fQH0UZlBwwQ\u003D = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public double ActualHeight
  {
    return this.\u0023\u003DzWSKbtyDsDUrkj_6aPt4\u0024LAc\u003D;
  }

  private void \u0023\u003Dz1YyqNxVuCZSr(double _param1)
  {
    this.\u0023\u003DzWSKbtyDsDUrkj_6aPt4\u0024LAc\u003D = _param1;
  }

  public Point TranslatePoint(
    Point _param1,
    IHitTestable _param2)
  {
    throw new NotImplementedException();
  }

  public bool IsPointWithinBounds(Point _param1) => throw new NotImplementedException();

  public Rect GetBoundsRelativeTo(
    IHitTestable _param1)
  {
    throw new NotImplementedException();
  }

  public bool IsSuspended => this.\u0023\u003Dzu_GzT8bMWkGK5e7tIw\u003D\u003D;

  private void \u0023\u003DzHScfdGHKGaS7(bool _param1)
  {
    this.\u0023\u003Dzu_GzT8bMWkGK5e7tIw\u003D\u003D = _param1;
  }

  public IUpdateSuspender SuspendUpdates()
  {
    throw new NotImplementedException();
  }

  public void ResumeUpdates(
    IUpdateSuspender _param1)
  {
    throw new NotImplementedException();
  }

  public void DecrementSuspend() => throw new NotImplementedException();

  public void InvalidateElement() => throw new NotImplementedException();

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dzf1TnIHLmqeNf(
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> _param1)
  {
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> eventHandler = this.\u0023\u003Dz7F6WbE57tL8z;
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D>>(ref this.\u0023\u003Dz7F6WbE57tL8z, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzO6DAydbqJOaS(
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> _param1)
  {
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> eventHandler = this.\u0023\u003Dz7F6WbE57tL8z;
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D>>(ref this.\u0023\u003Dz7F6WbE57tL8z, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzF_\u0024wky5\u0024qiYa(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzrN9MkyArV3jJ;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzrN9MkyArV3jJ, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzwG_uRQ_EmTwc(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzrN9MkyArV3jJ;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzrN9MkyArV3jJ, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public string Id
  {
    get => this.\u0023\u003Dz2lo_hq36wM_r\u0024Us8fw\u003D\u003D;
    set => this.\u0023\u003Dz2lo_hq36wM_r\u0024Us8fw\u003D\u003D = value;
  }

  public bool AutoTicks
  {
    get => this.\u0023\u003Dzs0Z9gR9q9AhlP_Xse1ll_RE\u003D;
    set => this.\u0023\u003Dzs0Z9gR9q9AhlP_Xse1ll_RE\u003D = value;
  }

  public ITickProvider TickProvider
  {
    get => this.\u0023\u003DzjIW6bZkM3WeYBMKCvw\u003D\u003D;
    set => this.\u0023\u003DzjIW6bZkM3WeYBMKCvw\u003D\u003D = value;
  }

  public IRange AnimatedVisibleRange
  {
    get => this.\u0023\u003DzGmZr\u0024oMYjkAL1VmdhsWq7jY\u003D;
    set => this.\u0023\u003DzGmZr\u0024oMYjkAL1VmdhsWq7jY\u003D = value;
  }

  public IRange VisibleRange
  {
    get => this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;
    set => this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D = value;
  }

  public IRange DataRange
  {
    get => this.\u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D;
  }

  private void \u0023\u003DzfkCfEPmvv8HX(
    IRange _param1)
  {
    this.\u0023\u003DzcnAqvT\u0024_SnI6ZnmDtg\u003D\u003D = _param1;
  }

  public double Width
  {
    get => this.\u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
    private set => this.\u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D = value;
  }

  double IDrawable.\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ZE9YOd5sMhl\u0024Z\u0024xSADAZlqXzWzlvA\u003D\u003D()
  {
    return this.Height;
  }

  void IDrawable.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntaAE_X2h6PHbsMnRBK9cYE8yLrOBvg\u003D\u003D(
    double _param1)
  {
    this.Height = _param1;
  }

  public void OnDraw(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    throw new NotImplementedException();
  }

  double IDrawable.\u0023\u003DzEa5ACpOap4rFIaHj5p9yfH70ARbSZe0FxQ0q\u00240QfMpnPN_04zQ\u003D\u003D()
  {
    return this.Width;
  }

  void IDrawable.\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI4gQW2p5XnENj22E0ug7VJ0RyC3hMw\u003D\u003D(
    double _param1)
  {
    this.Width = _param1;
  }

  public double Height
  {
    get => this.\u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;
    private set => this.\u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D = value;
  }

  public IRange<double> GrowBy
  {
    get => this.\u0023\u003DztXZBASMM9qIoqbN93Q\u003D\u003D;
    set => this.\u0023\u003DztXZBASMM9qIoqbN93Q\u003D\u003D = value;
  }

  public IComparable MinorDelta
  {
    get => this.\u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D;
    set => this.\u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D = value;
  }

  public IComparable MajorDelta
  {
    get => this.\u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D;
    set => this.\u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D = value;
  }

  public IServiceContainer Services
  {
    get => this._serviceContainer;
    set => this._serviceContainer = value;
  }

  public ISciChartSurface ParentSurface
  {
    get => this.\u0023\u003Dz5EWufkemTvDX91A_jQ\u003D\u003D;
    set => this.\u0023\u003Dz5EWufkemTvDX91A_jQ\u003D\u003D = value;
  }

  public Orientation Orientation
  {
    get => this.\u0023\u003DzEEAnOu4SHH0UfRpYZA\u003D\u003D;
    set => this.\u0023\u003DzEEAnOu4SHH0UfRpYZA\u003D\u003D = value;
  }

  public Brush MajorLineStroke
  {
    get => this.\u0023\u003Dz54ElmcdEpyKjoyYgYbDr2BA\u003D;
    set => this.\u0023\u003Dz54ElmcdEpyKjoyYgYbDr2BA\u003D = value;
  }

  public Brush MinorLineStroke
  {
    get => this.\u0023\u003DzSGGi4TgEUt5mUF3pihli_4g\u003D;
    set => this.\u0023\u003DzSGGi4TgEUt5mUF3pihli_4g\u003D = value;
  }

  public Style MajorTickLineStyle
  {
    get => this.\u0023\u003DzSrJGExC1cFymeKbb7SnhZU0\u003D;
    set => this.\u0023\u003DzSrJGExC1cFymeKbb7SnhZU0\u003D = value;
  }

  public Style MinorTickLineStyle
  {
    get => this.\u0023\u003DzqudCmo6OH6TsUzsHsLaqGC4\u003D;
    set => this.\u0023\u003DzqudCmo6OH6TsUzsHsLaqGC4\u003D = value;
  }

  public Style MajorGridLineStyle
  {
    get => this.\u0023\u003DzE7CsFgaAmnjF6\u0024fHfpvv6Bc\u003D;
    set => this.\u0023\u003DzE7CsFgaAmnjF6\u0024fHfpvv6Bc\u003D = value;
  }

  public Style MinorGridLineStyle
  {
    get => this.\u0023\u003DzppPsVcgxelS_HKNLg8lTiOc\u003D;
    set => this.\u0023\u003DzppPsVcgxelS_HKNLg8lTiOc\u003D = value;
  }

  public dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd AutoRange
  {
    get => this.\u0023\u003Dz63qhRFjKOsUggrHep98Xryc\u003D;
    set => this.\u0023\u003Dz63qhRFjKOsUggrHep98Xryc\u003D = value;
  }

  public bool \u0023\u003DzC76roKe\u0024cC4TJqf_QA\u003D\u003D()
  {
    return this.\u0023\u003DzUIGQlR2nTtVHv7f_sPdYlsk\u003D;
  }

  public void \u0023\u003Dz7_ex\u0024u7IoRreIaBIQA\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzUIGQlR2nTtVHv7f_sPdYlsk\u003D = _param1;
  }

  public string TextFormatting
  {
    get => this.\u0023\u003DzQNPprkAewzNuJ8BKgQ\u003D\u003D;
    set => this.\u0023\u003DzQNPprkAewzNuJ8BKgQ\u003D\u003D = value;
  }

  public string CursorTextFormatting
  {
    get => this.\u0023\u003DzcfCQIxVozaICVFl_NBxBzog\u003D;
    set => this.\u0023\u003DzcfCQIxVozaICVFl_NBxBzog\u003D = value;
  }

  public CategoryDateTimeAxisLabelProvider LabelProvider
  {
    get => this.\u0023\u003DzF89yE4I0sNZToajVmw\u003D\u003D;
    set => this.\u0023\u003DzF89yE4I0sNZToajVmw\u003D\u003D = value;
  }

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003DzFrVmckt\u0024NpG6() => this.\u0023\u003Dzd_LGQNec_oZzeIc6UgVO1bo\u003D;

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz\u0024mpHDOeBCMkH(bool _param1)
  {
    this.\u0023\u003Dzd_LGQNec_oZzeIc6UgVO1bo\u003D = _param1;
  }

  public bool IsHorizontalAxis
  {
    get => this.\u0023\u003Dz6u7rpXiPZ0ELNt9NTNmaSBs\u003D;
    private set => this.\u0023\u003Dz6u7rpXiPZ0ELNt9NTNmaSBs\u003D = value;
  }

  public bool IsStaticAxis
  {
    get => this.\u0023\u003Dz87HLNmE7ZOYXhABhXdXP_Qs\u003D;
    set => this.\u0023\u003Dz87HLNmE7ZOYXhABhXdXP_Qs\u003D = value;
  }

  public bool FlipCoordinates
  {
    get => this.\u0023\u003DzRGFzsYDyR61IZsdwtsP46tM\u003D;
    set => this.\u0023\u003DzRGFzsYDyR61IZsdwtsP46tM\u003D = value;
  }

  public bool HasValidVisibleRange => this.\u0023\u003DzgppbQROkBQEoPZ\u002401W2LKjk\u003D;

  private void \u0023\u003DzK20oezvUCIm_hF7jXw\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzgppbQROkBQEoPZ\u002401W2LKjk\u003D = _param1;
  }

  public bool HasDefaultVisibleRange => this.\u0023\u003DzlgpKpTWQTghq2XsMH3\u0024yh3g\u003D;

  private void \u0023\u003DzHjKg5iMM\u0024RPizpypLQ\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzlgpKpTWQTghq2XsMH3\u0024yh3g\u003D = _param1;
  }

  public string AxisTitle
  {
    get => this.\u0023\u003DzE44SwWOPIA7RUKWFl3AfSyE\u003D;
    set => this.\u0023\u003DzE44SwWOPIA7RUKWFl3AfSyE\u003D = value;
  }

  public Brush TickTextBrush
  {
    get => this.\u0023\u003Dz76rrAqkZX_yTjoL8V15HDUc\u003D;
    set => this.\u0023\u003Dz76rrAqkZX_yTjoL8V15HDUc\u003D = value;
  }

  public bool AutoAlignVisibleRange
  {
    get => this.\u0023\u003Dzh4plG7YN9ToCsd7Z8841Rh8ERp_W;
    set => this.\u0023\u003Dzh4plG7YN9ToCsd7Z8841Rh8ERp_W = value;
  }

  public bool DrawMinorTicks
  {
    get => this.\u0023\u003Dz\u0024v09X5oMHHrLRmrA\u0024iFjCV8\u003D;
    set => this.\u0023\u003Dz\u0024v09X5oMHHrLRmrA\u0024iFjCV8\u003D = value;
  }

  public bool DrawMajorTicks
  {
    get => this.\u0023\u003Dz9sq2bpQA7MkL9vtU0QFqNQU\u003D;
    set => this.\u0023\u003Dz9sq2bpQA7MkL9vtU0QFqNQU\u003D = value;
  }

  public bool DrawMajorGridLines
  {
    get => this.\u0023\u003Dzv5vEPhr33\u0024lD\u0024hy1EJpF8rrpNEYh;
    set => this.\u0023\u003Dzv5vEPhr33\u0024lD\u0024hy1EJpF8rrpNEYh = value;
  }

  public bool DrawMinorGridLines
  {
    get => this.\u0023\u003Dz5wl2rj9xJzaaRIuhQA9JjD3JTxoe;
    set => this.\u0023\u003Dz5wl2rj9xJzaaRIuhQA9JjD3JTxoe = value;
  }

  public HorizontalAlignment HorizontalAlignment
  {
    get => this.\u0023\u003DzEVNVu_ZK7\u00248NZaMf6lPJDrQ\u003D;
    set => this.\u0023\u003DzEVNVu_ZK7\u00248NZaMf6lPJDrQ\u003D = value;
  }

  public VerticalAlignment VerticalAlignment
  {
    get => this.\u0023\u003DzD\u0024vImVfDGuVPRP1kiGxuWsE\u003D;
    set => this.\u0023\u003DzD\u0024vImVfDGuVPRP1kiGxuWsE\u003D = value;
  }

  public \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3 AxisMode
  {
    get => this.\u0023\u003Dz3roz5ID2WsvKJYjSPpMBQF4\u003D;
    set => this.\u0023\u003Dz3roz5ID2WsvKJYjSPpMBQF4\u003D = value;
  }

  public ChartAxisAlignment AxisAlignment
  {
    get => this.\u0023\u003Dz_emvXNgm7Xa94MS7OGNgTK0\u003D;
    set => this.\u0023\u003Dz_emvXNgm7Xa94MS7OGNgTK0\u003D = value;
  }

  public bool IsCategoryAxis => this.\u0023\u003DzxkxRfeJKyToxpPT4fWwas78\u003D;

  private void \u0023\u003DzDdpIQsZIDiEk(bool _param1)
  {
    this.\u0023\u003DzxkxRfeJKyToxpPT4fWwas78\u003D = _param1;
  }

  public bool IsLogarithmicAxis
  {
    get => this.\u0023\u003DzHZdoCrtkBoXvozMBD\u0024zQ6WHB3IFoPNT1\u0024A\u003D\u003D;
  }

  private void \u0023\u003DzcKJ_g1O7hl6X2pWwNiEg3TX3mROl(bool _param1)
  {
    this.\u0023\u003DzHZdoCrtkBoXvozMBD\u0024zQ6WHB3IFoPNT1\u0024A\u003D\u003D = _param1;
  }

  public bool IsPolarAxis => this.\u0023\u003DzE8wAOIfSLDbe62ukKlhboN5j5Pb3;

  private void \u0023\u003DzNy1WwuC8OSs8vOy6fw\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzE8wAOIfSLDbe62ukKlhboN5j5Pb3 = _param1;
  }

  public bool IsCenterAxis
  {
    get => this.\u0023\u003DzmugSdmHGd\u00241nPW6PJdIoa0A\u003D;
    set => this.\u0023\u003DzmugSdmHGd\u00241nPW6PJdIoa0A\u003D = value;
  }

  public bool IsPrimaryAxis
  {
    get => this.\u0023\u003DzU4h9CnMMJpfE\u0024Yp9vRVQDl4\u003D;
    set => this.\u0023\u003DzU4h9CnMMJpfE\u0024Yp9vRVQDl4\u003D = value;
  }

  public IAnnotationCanvas ModifierAxisCanvas
  {
    get => this.\u0023\u003DzeBpVkMb4JTc7krWreljVw38\u003D;
  }

  private void \u0023\u003DzUphMuHrYNIWehkx3pA\u003D\u003D(
    IAnnotationCanvas _param1)
  {
    this.\u0023\u003DzeBpVkMb4JTc7krWreljVw38\u003D = _param1;
  }

  public Visibility Visibility
  {
    get => this.\u0023\u003DzYst3SO_\u0024ax6JKrNE9g\u003D\u003D;
    set => this.\u0023\u003DzYst3SO_\u0024ax6JKrNE9g\u003D\u003D = value;
  }

  public bool IsAxisFlipped => this.\u0023\u003Dz95Z1kAUJHBMg6bO6WMkzJg_U4AV0;

  private void \u0023\u003Dz8qqlfxGRa6JEY\u0024ylSVHQua0\u003D(bool _param1)
  {
    this.\u0023\u003Dz95Z1kAUJHBMg6bO6WMkzJg_U4AV0 = _param1;
  }

  public IRange VisibleRangeLimit
  {
    get => this.\u0023\u003Dz6D\u0024QoAqBdRidfWOtLpZV44k\u003D;
    set => this.\u0023\u003Dz6D\u0024QoAqBdRidfWOtLpZV44k\u003D = value;
  }

  public \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D VisibleRangeLimitMode
  {
    get => this.\u0023\u003DzoCK1qsWB8frOWaD56ETphhG4QNUY;
    set => this.\u0023\u003DzoCK1qsWB8frOWaD56ETphhG4QNUY = value;
  }

  public IComparable MinimalZoomConstrain
  {
    get => this.\u0023\u003Dzp5XbM\u0024uN17Ek79xiOJ2Q8kg\u003D;
    set => this.\u0023\u003Dzp5XbM\u0024uN17Ek79xiOJ2Q8kg\u003D = value;
  }

  public bool IsLabelCullingEnabled
  {
    get => this.\u0023\u003DzRoux7W5jZAVE88YvvTzQyR6QfsrTykyeBA\u003D\u003D;
    set => this.\u0023\u003DzRoux7W5jZAVE88YvvTzQyR6QfsrTykyeBA\u003D\u003D = value;
  }

  public \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> GetCurrentCoordinateCalculator()
  {
    throw new NotImplementedException();
  }

  public \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c \u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0()
  {
    throw new NotImplementedException();
  }

  public bool CaptureMouse() => throw new NotImplementedException();

  public void ReleaseMouseCapture() => throw new NotImplementedException();

  public void \u0023\u003DzqFIyyIbnwGLq(Cursor _param1) => throw new NotImplementedException();

  public \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1)
  {
    throw new NotImplementedException();
  }

  public \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D \u0023\u003Dz53g0bO2haOY4()
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzFwoMKP9juTnt()
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IDictionary<string, IRange> _param1)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz8arxsDEMmmqh(int _param1) => throw new NotImplementedException();

  public double \u0023\u003DzhL6gsJw\u003D(IComparable _param1)
  {
    throw new NotImplementedException();
  }

  public double GetOffsetForLabels() => throw new NotImplementedException();

  public IComparable GetDataValue(double _param1)
  {
    throw new NotImplementedException();
  }

  public Size \u0023\u003Dz\u0024AYTdb\u0024Pg7tA() => throw new NotImplementedException();

  public void \u0023\u003Dzs15X3Ar32F1\u0024(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1 = default (\u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D),
    IPointSeries _param2 = null)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzquLnA5Y\u003D(
    double _param1,
    ClipMode _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzquLnA5Y\u003D(
    double _param1,
    ClipMode _param2,
    TimeSpan _param3)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1) => throw new NotImplementedException();

  public void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1, TimeSpan _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzQdR08KQ\u003D(double _param1, double _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzQdR08KQ\u003D(double _param1, double _param2, TimeSpan _param3)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz40HnRQM\u003D(double _param1, double _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz40HnRQM\u003D(double _param1, double _param2, TimeSpan _param3)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzSYETXFE\u003D(
    IRange _param1,
    double _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzCIPDlIJQeLiZ(
    IRange _param1,
    double _param2,
    IRange _param3)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzQ4klw1orSVl\u0024(Type _param1)
  {
    this.\u0023\u003DzLXQXNXQ\u003D.\u0023\u003DzQ4klw1orSVl\u0024(_param1);
  }

  public string \u0023\u003DzRDs3D1Q\u003D(IComparable _param1, string _param2)
  {
    throw new NotImplementedException();
  }

  public string \u0023\u003DzRDs3D1Q\u003D(IComparable _param1)
  {
    throw new NotImplementedException();
  }

  public string \u0023\u003DzRQVMnjXxoCTF(IComparable _param1, bool _param2)
  {
    throw new NotImplementedException();
  }

  public bool \u0023\u003Dz2OKbyRBzRCBL(
    IRange _param1)
  {
    throw new NotImplementedException();
  }

  public IAxis \u0023\u003DzQ8SgRgQ\u003D()
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzwrnVUenT8f7v7FlPviBwd40\u003D(
    IRange _param1,
    TimeSpan _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzpTR8\u0024ECbZOHX() => throw new NotImplementedException();

  public void Clear() => throw new NotImplementedException();

  public IRange \u0023\u003DzspbjXJnVtbB\u0024()
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    throw new NotImplementedException();
  }

  public double CurrentDatapointPixelSize => double.NaN;

  public XmlSchema \u0023\u003DzFOK3Wwg\u003D() => throw new NotImplementedException();

  public double \u0023\u003DztF4lQCS6R9tA() => this.\u0023\u003Dz95UBp1xHzQQZTB6VIAc\u0024oKE\u003D;

  public void \u0023\u003DzO5991riZR0wk(double _param1)
  {
    this.\u0023\u003Dz95UBp1xHzQQZTB6VIAc\u0024oKE\u003D = _param1;
  }

  public double \u0023\u003Dzt5IyOefcxt3n4R5WOQ\u003D\u003D()
  {
    return this.\u0023\u003DzBZa4I2\u0024V0AAJfaHpg8DsUV4\u003D;
  }

  public void \u0023\u003DzK\u0024tv6UWby7nP3hDmPw\u003D\u003D(double _param1)
  {
    this.\u0023\u003DzBZa4I2\u0024V0AAJfaHpg8DsUV4\u003D = _param1;
  }
}
