// Decompiled with JetBrains decompiler
// Type: #=zfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP$CeDIqsTdzB
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable enable
internal sealed class ParentVM : 
  ChartBaseViewModel,
  IDisposable
{
  
  private readonly 
  #nullable disable
  ObservableCollection<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy> \u0023\u003Dz10jqvLI\u003D = new ObservableCollection<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>();
  
  private readonly List<UIBaseVM> \u0023\u003DzH31vDNM\u003D = new List<UIBaseVM>();
  
  private readonly ScichartSurfaceMVVM \u0023\u003DznCb1THp8SgddfKhj6w\u003D\u003D;
  
  private readonly IfxChartElement \u0023\u003Dz\u0024dH1b9H0ZppyjxlZ6w\u003D\u003D;
  
  private readonly bool \u0023\u003DzltAOnz7pENYNz4RZiZiGFJ7j3MDa;
  
  private \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D \u0023\u003Dz\u0024j34drxVvrdumV_HyGybhvc\u003D;
  
  private bool \u0023\u003DzeOVf5rToxe0_\u0024sT_tw\u003D\u003D;

  public ParentVM(
    ScichartSurfaceMVVM _param1,
    IfxChartElement _param2)
  {
    this.\u0023\u003DznCb1THp8SgddfKhj6w\u003D\u003D = _param1 ?? throw new ArgumentNullException("");
    this.\u0023\u003Dz\u0024dH1b9H0ZppyjxlZ6w\u003D\u003D = _param2 ?? throw new ArgumentNullException("");
    this.\u0023\u003DzltAOnz7pENYNz4RZiZiGFJ7j3MDa = _param2 is IChartCandleElement;
    ChartViewModel.\u0023\u003DztwqF4KBjQLI4w4fkq\u0024UNEzaV82mj(new Action(this.\u0023\u003DzJRKin3dIscU4TEH\u0024FkxyZyjDceEY));
    this.ChartElement.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzqKjZcEftBQYu8FctY__O05c\u003D);
  }

  public ScichartSurfaceMVVM Pane
  {
    get => this.\u0023\u003DznCb1THp8SgddfKhj6w\u003D\u003D;
  }

  public bool AllowToRemove => this.Pane.\u0023\u003Dzv_LIRKQ\u003D(this);

  public IfxChartElement ChartElement
  {
    get => this.\u0023\u003Dz\u0024dH1b9H0ZppyjxlZ6w\u003D\u003D;
  }

  public string Title => this.ChartElement.GetGeneratedTitle();

  public bool IsCandleElement => this.\u0023\u003DzltAOnz7pENYNz4RZiZiGFJ7j3MDa;

  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D Candles
  {
    get => this.\u0023\u003Dz\u0024j34drxVvrdumV_HyGybhvc\u003D;
    private set => this.\u0023\u003Dz\u0024j34drxVvrdumV_HyGybhvc\u003D = value;
  }

  public IEnumerable<UIBaseVM> Elements
  {
    get
    {
      return (IEnumerable<UIBaseVM>) this.\u0023\u003DzH31vDNM\u003D;
    }
  }

  public IEnumerable<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy> Values
  {
    get
    {
      return (IEnumerable<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>) this.\u0023\u003Dz10jqvLI\u003D;
    }
  }

  public Color Color
  {
    get
    {
      return this.\u0023\u003DzH31vDNM\u003D.Count == 1 ? this.\u0023\u003DzH31vDNM\u003D[0].Element.Color : Colors.Transparent;
    }
  }

  public void \u0023\u003DzkFJdjYoyxP8n(
    IEnumerable<UIBaseVM> _param1)
  {
    UIBaseVM[] array = _param1.ToArray<UIBaseVM>();
    if (CollectionHelper.IsEmpty<UIBaseVM>(array))
      throw new ArgumentException("");
    this.\u0023\u003DzH31vDNM\u003D.AddRange((IEnumerable<UIBaseVM>) array);
    if (this.IsCandleElement && this.Candles == null)
      this.Candles = array.OfType<\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D>().First<\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUmMviGnZF5zyP8Vq715pyobvSG_F30ddnEdMvAIP_dliVQ\u003D\u003D>();
    if (this.\u0023\u003DzH31vDNM\u003D.Count == 1)
      this.MapPropertyChangeNotification((INotifyPropertyChanged) this.\u0023\u003DzH31vDNM\u003D[0].Element, "", "");
    CollectionHelper.ForEach<UIBaseVM>((IEnumerable<UIBaseVM>) array, new Action<UIBaseVM>(this.\u0023\u003Dz7x6SlCXIp8hAJNVeAefGj\u0024I\u003D));
  }

  private void \u0023\u003DzJRKin3dIscU4TEH\u0024FkxyZyjDceEY()
  {
    this.NotifyChanged("");
  }

  public void \u0023\u003Dzfc4TzKM\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy _param1)
  {
    if (_param1 == null)
      throw new ArgumentNullException("");
    if (this.\u0023\u003Dz10jqvLI\u003D.Contains(_param1))
      throw new ArgumentException("");
    _param1.Parent = this;
    this.\u0023\u003Dz10jqvLI\u003D.Add(_param1);
  }

  public void \u0023\u003DzMNK339lzrtSc()
  {
    CollectionHelper.ForEach<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>((IEnumerable<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>) this.\u0023\u003Dz10jqvLI\u003D, ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D ?? (ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D = new Action<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz\u0024Y0CRLvNGz\u0024QVOp_KD9Goz0\u003D)));
  }

  public bool Draw(ChartDrawData _param1)
  {
    return this.ChartElement.Draw(_param1);
  }

  public void Reset()
  {
    this.ChartElement.Reset();
    this.\u0023\u003DzH31vDNM\u003D.ForEach(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D ?? (ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D = new Action<UIBaseVM>(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz78guPox9d5EyKS2_DA\u003D\u003D)));
  }

  public void \u0023\u003DzoK7PFLI\u003D()
  {
    this.\u0023\u003DzH31vDNM\u003D.ForEach(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D ?? (ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D = new Action<UIBaseVM>(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzy8xbdOhlUdYc9J3lehtuYeU\u003D)));
  }

  public void \u0023\u003DzKy5smiO3gHXp()
  {
    this.\u0023\u003DzH31vDNM\u003D.ForEach(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzh2rq1rNuTua1OY6e3Q\u003D\u003D ?? (ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzh2rq1rNuTua1OY6e3Q\u003D\u003D = new Action<UIBaseVM>(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz8jVsNNt6XUrYulKMVzGRKbI\u003D)));
  }

  public void \u0023\u003Dz\u0024abmkXc\u003D()
  {
    this.\u0023\u003DzH31vDNM\u003D.ForEach(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D ?? (ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D = new Action<UIBaseVM>(ParentVM.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzIFx97lVTaAg3xg6hNg\u003D\u003D)));
  }

  internal Subscription \u0023\u003DzZ0VU1NABDfD8(
    IfxChartElement _param1)
  {
    return ((Chart) this.Pane.Chart).TryGetSubscription((IChartElement) _param1);
  }

  public bool IsDisposed
  {
    get => this.\u0023\u003DzeOVf5rToxe0_\u0024sT_tw\u003D\u003D;
    private set => this.\u0023\u003DzeOVf5rToxe0_\u0024sT_tw\u003D\u003D = value;
  }

  public void Dispose()
  {
    ChartViewModel.\u0023\u003DzFKIjoTgcr_ZQ\u0024BFi5BCDqDF5zYZT(new Action(this.\u0023\u003DzJRKin3dIscU4TEH\u0024FkxyZyjDceEY));
    this.IsDisposed = true;
  }

  private void \u0023\u003DzqKjZcEftBQYu8FctY__O05c\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == ""))
      return;
    this.NotifyChanged("");
  }

  private void \u0023\u003Dz7x6SlCXIp8hAJNVeAefGj\u0024I\u003D(
    #nullable disable
    UIBaseVM _param1)
  {
    _param1.\u0023\u003Dzfd2adzY\u003D(this);
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly ParentVM.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ParentVM.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy> \u0023\u003Dzxza6M2rQ\u0024\u0024Ygg7l8sg\u003D\u003D;
    public static Action<UIBaseVM> \u0023\u003DzujJHoXZ3YoJgJW6Elg\u003D\u003D;
    public static Action<UIBaseVM> \u0023\u003Dza6ES3WKQ8eKYsVbcqw\u003D\u003D;
    public static Action<UIBaseVM> \u0023\u003Dzh2rq1rNuTua1OY6e3Q\u003D\u003D;
    public static Action<UIBaseVM> \u0023\u003Dzc_JZPYTnozknjOo_1Q\u003D\u003D;

    internal void \u0023\u003Dz\u0024Y0CRLvNGz\u0024QVOp_KD9Goz0\u003D(
      \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy _param1)
    {
      _param1.Value = (string) null;
    }

    internal void \u0023\u003Dz78guPox9d5EyKS2_DA\u003D\u003D(
      UIBaseVM _param1)
    {
      _param1.Reset();
    }

    internal void \u0023\u003Dzy8xbdOhlUdYc9J3lehtuYeU\u003D(
      UIBaseVM _param1)
    {
      _param1.\u0023\u003DzoK7PFLI\u003D();
    }

    internal void \u0023\u003Dz8jVsNNt6XUrYulKMVzGRKbI\u003D(
      UIBaseVM _param1)
    {
      _param1.\u0023\u003DzKy5smiO3gHXp();
    }

    internal void \u0023\u003DzIFx97lVTaAg3xg6hNg\u003D\u003D(
      UIBaseVM _param1)
    {
      _param1.\u0023\u003Dz\u0024abmkXc\u003D();
    }
  }
}
