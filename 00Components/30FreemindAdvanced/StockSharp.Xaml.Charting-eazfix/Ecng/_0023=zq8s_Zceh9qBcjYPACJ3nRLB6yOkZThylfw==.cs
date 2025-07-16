// Decompiled with JetBrains decompiler
// Type: #=zq8s_Zceh9qBcjYPACJ3nRLB6yOkZThylfw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
public sealed class \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRLB6yOkZThylfw\u003D\u003D : 
  IDrawable,
  IXmlSerializable,
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXvZYOg6JAbLCIgQvZp9odw6FSOKg1daH3vPLNHtT2ZG4iQ\u003D\u003D,
  IRenderableSeries
{
  
  private readonly IRenderableSeries \u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D;
  
  private double \u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
  
  private double \u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;
  
  private EventHandler \u0023\u003DzDpzMqzE\u003D;
  
  private IServiceContainer _serviceContainer;
  
  private bool \u0023\u003DzDVmik2Cw62ImfJj4oQ\u003D\u003D;
  
  private bool \u0023\u003DznkfEoFr8zh4Cq3PUWdbHj5k\u003D;
  
  private ResamplingMode \u0023\u003DzYpAapsQ5ph\u0024Fz\u0024QW29lAZ8huNfaT95Ph4Q\u003D\u003D;
  
  private readonly object \u0023\u003DzTHDpVqL3PQeslcVw73Y\u0024d38\u003D;
  
  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzZLrNEf3FM2NGYcsrMw\u003D\u003D;
  
  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzQsY1\u0024NybEpjavuW8NInEKEc\u003D;
  
  private IAxis \u0023\u003DzSzpQmjX3i_6OFgfsBw\u003D\u003D;
  
  private IAxis \u0023\u003DztSzcLDVAJZhbQ5yz2A\u003D\u003D;
  
  private Color \u0023\u003Dz9tBP_ZS2eU\u00247YM1NDA\u003D\u003D;
  
  private Style \u0023\u003DzaqWuOs__wYWAX7X839hxECM\u003D;
  
  private Style \u0023\u003DzNhK7wIQ54jWzEcQcMg\u003D\u003D;
  
  private object \u0023\u003DzymhPC80OqalJIm0GwQ\u003D\u003D;
  
  private bool \u0023\u003Dz9Fb_zuVvFqZpmCGznA\u003D\u003D;
  
  private FrameworkElement \u0023\u003DzocgkzGfzXI\u0024Dll0ioXu2P4aVRxxJ;
  
  private string \u0023\u003DzO6q\u0024C4aUFYSTBpEVuxoNAzI\u003D;
  
  private string \u0023\u003DzHKEm8AXUqk19HDxQqw9GqLQ\u003D;
  
  private IRenderPassData \u0023\u003Dzq_BOEKz6TP1hJ5Vkn77QRCE\u003D;
  
  private \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D \u0023\u003Dz4LlMqXRKgSCCgq5b0w\u003D\u003D;
  
  private int \u0023\u003DzPLg_3LuBugD5T43IfVvWRrI\u003D;
  
  private bool \u0023\u003DzxZnVF9e_Olw9LuFVWAjGc3e_XlJ2yyzlwQ\u003D\u003D;

  public \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRLB6yOkZThylfw\u003D\u003D(
    IRenderableSeries _param1)
  {
    this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D = _param1;
    this.IsVisible = _param1.IsVisible;
    this.ResamplingMode = _param1.get_ResamplingMode();
    this.DataSeries = _param1.get_DataSeries();
    this.XAxisId = _param1.get_XAxisId();
    this.YAxisId = _param1.get_YAxisId();
    this.\u0023\u003DzBjZmwQKXw9Dlg8q\u0024I_nYy\u0024A\u003D(_param1.\u0023\u003DztPaciKMZWysZOtqEskMFjk8\u003D());
  }

  public double Width
  {
    get => this.\u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
    set => this.\u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D = value;
  }

  public double Height
  {
    get => this.\u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;
    set => this.\u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D = value;
  }

  public void OnDraw(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D.OnDraw(_param1, _param2);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzhBqSd5Scc0Hy(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzDpzMqzE\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzDpzMqzE\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz_bNSX12Vpev3(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzDpzMqzE\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzDpzMqzE\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public IServiceContainer Services()
  {
    return this._serviceContainer;
  }

  [CompilerGenerated]
  [SpecialName]
  public void Services(
    IServiceContainer _param1)
  {
    this._serviceContainer = _param1;
  }

  public bool IsVisible
  {
    get => this.\u0023\u003DzDVmik2Cw62ImfJj4oQ\u003D\u003D;
    set => this.\u0023\u003DzDVmik2Cw62ImfJj4oQ\u003D\u003D = value;
  }

  public bool AntiAliasing
  {
    get => this.\u0023\u003DznkfEoFr8zh4Cq3PUWdbHj5k\u003D;
    set => this.\u0023\u003DznkfEoFr8zh4Cq3PUWdbHj5k\u003D = value;
  }

  public ResamplingMode ResamplingMode
  {
    get => this.\u0023\u003DzYpAapsQ5ph\u0024Fz\u0024QW29lAZ8huNfaT95Ph4Q\u003D\u003D;
    set => this.\u0023\u003DzYpAapsQ5ph\u0024Fz\u0024QW29lAZ8huNfaT95Ph4Q\u003D\u003D = value;
  }

  [CompilerGenerated]
  [SpecialName]
  public object \u0023\u003DzQavr9eonlwL7DeqLQA\u003D\u003D()
  {
    return this.\u0023\u003DzTHDpVqL3PQeslcVw73Y\u0024d38\u003D;
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D DataSeries
  {
    get => this.\u0023\u003DzZLrNEf3FM2NGYcsrMw\u003D\u003D;
    set => this.\u0023\u003DzZLrNEf3FM2NGYcsrMw\u003D\u003D = value;
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzrxNpasLpWieV8A5\u0024og\u003D\u003D()
  {
    return this.\u0023\u003DzQsY1\u0024NybEpjavuW8NInEKEc\u003D;
  }

  public void \u0023\u003DzbqehJBG79B7DqvkFGg\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1)
  {
    this.\u0023\u003DzQsY1\u0024NybEpjavuW8NInEKEc\u003D = _param1;
  }

  public IAxis XAxis
  {
    get => this.\u0023\u003DzSzpQmjX3i_6OFgfsBw\u003D\u003D;
    set => this.\u0023\u003DzSzpQmjX3i_6OFgfsBw\u003D\u003D = value;
  }

  public IAxis YAxis
  {
    get => this.\u0023\u003DztSzcLDVAJZhbQ5yz2A\u003D\u003D;
    set => this.\u0023\u003DztSzcLDVAJZhbQ5yz2A\u003D\u003D = value;
  }

  public Color SeriesColor
  {
    get => this.\u0023\u003Dz9tBP_ZS2eU\u00247YM1NDA\u003D\u003D;
    set => this.\u0023\u003Dz9tBP_ZS2eU\u00247YM1NDA\u003D\u003D = value;
  }

  public Style SelectedSeriesStyle
  {
    get => this.\u0023\u003DzaqWuOs__wYWAX7X839hxECM\u003D;
    set => this.\u0023\u003DzaqWuOs__wYWAX7X839hxECM\u003D = value;
  }

  [CompilerGenerated]
  [SpecialName]
  public Style \u0023\u003Dz35pNjj8Nlecj() => this.\u0023\u003DzNhK7wIQ54jWzEcQcMg\u003D\u003D;

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzlqgXLHiucpqc(Style _param1)
  {
    this.\u0023\u003DzNhK7wIQ54jWzEcQcMg\u003D\u003D = _param1;
  }

  public object DataContext
  {
    get => this.\u0023\u003DzymhPC80OqalJIm0GwQ\u003D\u003D;
    set => this.\u0023\u003DzymhPC80OqalJIm0GwQ\u003D\u003D = value;
  }

  public bool IsSelected
  {
    get => this.\u0023\u003Dz9Fb_zuVvFqZpmCGznA\u003D\u003D;
    set => this.\u0023\u003Dz9Fb_zuVvFqZpmCGznA\u003D\u003D = value;
  }

  [CompilerGenerated]
  [SpecialName]
  public FrameworkElement \u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D()
  {
    return this.\u0023\u003DzocgkzGfzXI\u0024Dll0ioXu2P4aVRxxJ;
  }

  private void \u0023\u003DzrjjZgoOOFly9hg9Mc\u0024Dq3kk\u003D(FrameworkElement _param1)
  {
    this.\u0023\u003DzocgkzGfzXI\u0024Dll0ioXu2P4aVRxxJ = _param1;
  }

  public string YAxisId
  {
    get => this.\u0023\u003DzO6q\u0024C4aUFYSTBpEVuxoNAzI\u003D;
    set => this.\u0023\u003DzO6q\u0024C4aUFYSTBpEVuxoNAzI\u003D = value;
  }

  public string XAxisId
  {
    get => this.\u0023\u003DzHKEm8AXUqk19HDxQqw9GqLQ\u003D;
    set => this.\u0023\u003DzHKEm8AXUqk19HDxQqw9GqLQ\u003D = value;
  }

  [CompilerGenerated]
  [SpecialName]
  public IRenderPassData \u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D()
  {
    return this.\u0023\u003Dzq_BOEKz6TP1hJ5Vkn77QRCE\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzHZiyrsua6EwHR04JCw\u003D\u003D(
    IRenderPassData _param1)
  {
    this.\u0023\u003Dzq_BOEKz6TP1hJ5Vkn77QRCE\u003D = _param1;
  }

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D PaletteProvider
  {
    get => this.\u0023\u003Dz4LlMqXRKgSCCgq5b0w\u003D\u003D;
    set => this.\u0023\u003Dz4LlMqXRKgSCCgq5b0w\u003D\u003D = value;
  }

  public int StrokeThickness
  {
    get => this.\u0023\u003DzPLg_3LuBugD5T43IfVvWRrI\u003D;
    set => this.\u0023\u003DzPLg_3LuBugD5T43IfVvWRrI\u003D = value;
  }

  public HitTestInfo \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1,
    bool _param2,
    double? _param3)
  {
    throw new NotImplementedException();
  }

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003DztPaciKMZWysZOtqEskMFjk8\u003D()
  {
    return this.\u0023\u003DzxZnVF9e_Olw9LuFVWAjGc3e_XlJ2yyzlwQ\u003D\u003D;
  }

  private void \u0023\u003DzBjZmwQKXw9Dlg8q\u0024I_nYy\u0024A\u003D(bool _param1)
  {
    this.\u0023\u003DzxZnVF9e_Olw9LuFVWAjGc3e_XlJ2yyzlwQ\u003D\u003D = _param1;
  }

  public HitTestInfo \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1,
    bool _param2 = false)
  {
    throw new NotImplementedException();
  }

  public HitTestInfo \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1,
    double _param2,
    bool _param3 = false)
  {
    throw new NotImplementedException();
  }

  public HitTestInfo \u0023\u003DznVLFa68vHPHy(
    Point _param1,
    bool _param2 = false)
  {
    throw new NotImplementedException();
  }

  public \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003DzZZbJdAS6fDJ\u0024(
    HitTestInfo _param1)
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003Dzq3MgExWxza1L()
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzxNQHuqrEvxH2(
    IRange _param1)
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzxNQHuqrEvxH2(
    IRange _param1,
    bool _param2)
  {
    throw new NotImplementedException();
  }

  public virtual IndexRange  \u0023\u003DzVAnbwOJn98Ya(
    IndexRange  _param1)
  {
    throw new NotImplementedException();
  }

  public bool \u0023\u003DzVxrZQ3k9ZBGJ(
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D _param1)
  {
    throw new NotImplementedException();
  }

  public XmlSchema GetSchema() => throw new NotImplementedException();

  public void ReadXml(XmlReader _param1) => throw new NotImplementedException();

  public void WriteXml(XmlWriter _param1) => throw new NotImplementedException();
}
