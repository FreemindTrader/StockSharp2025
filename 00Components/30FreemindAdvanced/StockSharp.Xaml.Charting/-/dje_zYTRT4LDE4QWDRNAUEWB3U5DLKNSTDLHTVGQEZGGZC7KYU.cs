// Decompiled with JetBrains decompiler
// Type: -.dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal abstract class dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd : 
  ContentControl,
  \u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6,
  \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7,
  IDisposable,
  \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzjroGW0xeV8YH = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440596), typeof (double?), typeof (dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzL8ZRB9csVw_UcT6v5V6Hxo0\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzU3sbyS2_qQyl = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539438991), typeof (bool), typeof (dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd), new PropertyMetadata((object) false));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzB\u0024niq80SYDhY = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439015), typeof (int), typeof (dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd), new PropertyMetadata((object) 50));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzsKzwhvawatYW = DependencyProperty.RegisterAttached(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440469), typeof (string), typeof (dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzV210hqnzErQ4)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> \u0023\u003DzjgUUUJE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> \u0023\u003DzcyGdlF8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal static readonly string \u0023\u003Dz8UAGL9e3cOCn = Guid.NewGuid().ToString();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile bool \u0023\u003DzAJ8tNFa80f45;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D \u0023\u003Dz\u0024wP7eNcNWEpx;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzvBK\u00248KQ\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Grid \u0023\u003DzS\u0024OTg_s\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Image \u0023\u003Dz2TNhyDg\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected WriteableBitmap \u0023\u003DzRIsZuY3LT4U\u0024;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D \u0023\u003Dzwa3i3hwVZeqr;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static int \u0023\u003Dzf3Z0CZk\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D \u0023\u003DzJMDWU3P\u0024MwfW43PZdrim0N0\u003D;

  protected dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd()
  {
    this.\u0023\u003Dzwa3i3hwVZeqr = this.\u0023\u003Dzrl_5XeVvahdb();
    Image image = new Image();
    image.HorizontalAlignment = HorizontalAlignment.Stretch;
    image.VerticalAlignment = VerticalAlignment.Stretch;
    image.Stretch = Stretch.None;
    image.IsHitTestVisible = false;
    this.\u0023\u003Dz2TNhyDg\u003D = image;
    this.\u0023\u003DzFeNr2Uw\u003D();
    this.SizeChanged += new SizeChangedEventHandler(this.\u0023\u003DzxsT1NlAcKqkM);
    UIElementCollection children = this.\u0023\u003DzqgMiDwaFbXWy().Children;
    Rectangle element = new Rectangle();
    element.HorizontalAlignment = HorizontalAlignment.Stretch;
    element.VerticalAlignment = VerticalAlignment.Stretch;
    element.Fill = (Brush) new SolidColorBrush(Colors.Transparent);
    element.Tag = (object) dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003Dz8UAGL9e3cOCn;
    children.Add((UIElement) element);
    this.Loaded += new RoutedEventHandler(this.\u0023\u003DzPqfANn0gaWCSKnTktw\u003D\u003D);
    this.Unloaded += new RoutedEventHandler(this.\u0023\u003DzS\u0024ZyWNYVo9XvYqd2kQ\u003D\u003D);
  }

  public static void SetRenderSurfaceType(UIElement _param0, string _param1)
  {
    _param0.SetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzsKzwhvawatYW, (object) _param1);
  }

  public static string GetRenderSurfaceType(UIElement _param0)
  {
    return (string) _param0.GetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzsKzwhvawatYW);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzQ_ByYlCf\u0024Kac(
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> _param1)
  {
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> eventHandler = this.\u0023\u003DzjgUUUJE\u003D;
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy>>(ref this.\u0023\u003DzjgUUUJE\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzvM8pYfLF8h8E(
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> _param1)
  {
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> eventHandler = this.\u0023\u003DzjgUUUJE\u003D;
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy>>(ref this.\u0023\u003DzjgUUUJE\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzKPHSi1vgK\u0024Fx(
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> _param1)
  {
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> eventHandler = this.\u0023\u003DzcyGdlF8\u003D;
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr>>(ref this.\u0023\u003DzcyGdlF8\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzrRRdxqQwy\u0024OJ(
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> _param1)
  {
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> eventHandler = this.\u0023\u003DzcyGdlF8\u003D;
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr>>(ref this.\u0023\u003DzcyGdlF8\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  protected abstract \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D \u0023\u003Dzrl_5XeVvahdb();

  ~dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd()
  {
    try
    {
      this.\u0023\u003DzTuM3X1E\u003D(false);
    }
    finally
    {
      // ISSUE: explicit finalizer call
      // ISSUE: explicit non-virtual call
      __nonvirtual (((object) this).Finalize());
    }
  }

  [CompilerGenerated]
  [SpecialName]
  public \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzu\u0024P3XgkcE7BC()
  {
    return this.\u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzrEoWi5uPS0Yz(
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D _param1)
  {
    this.\u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D = _param1;
  }

  internal Image \u0023\u003DzZAw3cTjlwxct() => this.\u0023\u003Dz2TNhyDg\u003D;

  public Grid \u0023\u003DzqgMiDwaFbXWy() => this.\u0023\u003DzS\u0024OTg_s\u003D;

  protected \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D \u0023\u003DzNFOu7BeFZYda()
  {
    return this.\u0023\u003Dzwa3i3hwVZeqr;
  }

  public double? MaxFrameRate
  {
    get
    {
      return (double?) this.GetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzjroGW0xeV8YH);
    }
    set
    {
      this.SetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzjroGW0xeV8YH, (object) value);
    }
  }

  public int ResizeThrottleMs
  {
    get
    {
      return (int) this.GetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzB\u0024niq80SYDhY);
    }
    set
    {
      this.SetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzB\u0024niq80SYDhY, (object) value);
    }
  }

  public bool UseResizeThrottle
  {
    get
    {
      return (bool) this.GetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzU3sbyS2_qQyl);
    }
    set
    {
      this.SetValue(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzU3sbyS2_qQyl, (object) value);
    }
  }

  [SpecialName]
  public virtual bool \u0023\u003Dzn2tbOrs4ILOI()
  {
    return this.\u0023\u003DzRIsZuY3LT4U\u0024 == null || this.\u0023\u003DzRIsZuY3LT4U\u0024.PixelWidth != (int) this.ActualWidth || this.\u0023\u003DzRIsZuY3LT4U\u0024.PixelHeight != (int) this.ActualHeight;
  }

  [SpecialName]
  public virtual bool \u0023\u003Dzldc13_HNXRCXYOL0DA\u003D\u003D()
  {
    return this.ActualWidth >= double.Epsilon && this.ActualHeight >= double.Epsilon && this.ActualWidth.\u0023\u003Dz_Bj0HmLWq3hY() && this.ActualHeight.\u0023\u003Dz_Bj0HmLWq3hY();
  }

  [SpecialName]
  public ReadOnlyCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> \u0023\u003Dzvxq3X_8T\u0024Noo()
  {
    return new ReadOnlyCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>((IList<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>) this.\u0023\u003DzqgMiDwaFbXWy().Children.OfType<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>().ToArray<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>());
  }

  public void \u0023\u003Dz5q8i9C4\u003D() => this.\u0023\u003DzAJ8tNFa80f45 = true;

  public void \u0023\u003DzUf222sU\u003D()
  {
    using (\u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I mvXdEdq1k7UiFd2I = this.\u0023\u003Dz1cRMfLZU4Eo2())
      mvXdEdq1k7UiFd2I.\u0023\u003DzUf222sU\u003D();
  }

  public bool \u0023\u003DzdBvSINdoeQWX(
    \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
  {
    return _param1 is UIElement element && this.\u0023\u003DzS\u0024OTg_s\u003D.Children.Contains(element);
  }

  public void \u0023\u003DzJoneIt0\u003D(
    IEnumerable<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> _param1)
  {
    foreach (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D s1JolYrWoYpqmQ6ug in _param1)
      this.\u0023\u003DzJoneIt0\u003D(s1JolYrWoYpqmQ6ug);
  }

  public void \u0023\u003DzJoneIt0\u003D(
    \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
  {
    this.\u0023\u003Dz_SCZwjM\u003D(_param1);
    _param1.\u0023\u003DzrEoWi5uPS0Yz(this.\u0023\u003Dzu\u0024P3XgkcE7BC());
    if (!(_param1 is FrameworkElement element))
      return;
    element.Visibility = Visibility.Collapsed;
    this.\u0023\u003DzS\u0024OTg_s\u003D.Children.Add((UIElement) element);
  }

  public void \u0023\u003Dz_SCZwjM\u003D(
    \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
  {
    _param1.\u0023\u003DzrEoWi5uPS0Yz((\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null);
    if (!(_param1 is FrameworkElement frameworkElement))
      return;
    (frameworkElement.Parent as Panel).\u0023\u003DziYdJ\u00246cCiBha((object) frameworkElement);
  }

  public virtual void \u0023\u003Dzqtb9toLjXu0t()
  {
    for (int index = this.\u0023\u003DzS\u0024OTg_s\u003D.Children.Count - 1; index >= 0; --index)
    {
      if (this.\u0023\u003DzS\u0024OTg_s\u003D.Children[index] is \u0023\u003Dz5VLaAZX2bctAcuSoajSAXvZYOg6JAbLCIgQvZp9odw6FSOKg1daH3vPLNHtT2ZG4iQ\u003D\u003D)
        this.\u0023\u003DzS\u0024OTg_s\u003D.Children.RemoveAt(index);
    }
  }

  public void Dispose()
  {
    this.\u0023\u003DzTuM3X1E\u003D(true);
    GC.SuppressFinalize((object) this);
  }

  public void \u0023\u003DzTuM3X1E\u003D(bool _param1)
  {
    if (this.\u0023\u003DzvBK\u00248KQ\u003D)
      return;
    this.\u0023\u003DzvBK\u00248KQ\u003D = true;
    ((DispatcherObject) this).Dispatcher.BeginInvoke((Delegate) new Action(this.\u0023\u003DzD0y7hB3\u0024LdB07QMkVQ\u003D\u003D), Array.Empty<object>());
    if (this.\u0023\u003Dz\u0024wP7eNcNWEpx != null)
    {
      Interlocked.Decrement(ref dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003Dzf3Z0CZk\u003D);
      this.\u0023\u003Dz\u0024wP7eNcNWEpx.Dispose();
      this.\u0023\u003Dz\u0024wP7eNcNWEpx = (\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D) null;
    }
    this.\u0023\u003Dzuq\u0024Rld84MQAH();
  }

  public virtual void \u0023\u003DzSgMgi9QlqY9x()
  {
    int num1 = (int) this.ActualWidth;
    int num2 = (int) this.ActualHeight;
    if (!this.\u0023\u003Dzldc13_HNXRCXYOL0DA\u003D\u003D())
      num1 = num2 = 1;
    this.\u0023\u003DzRIsZuY3LT4U\u0024 = dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DznU63H0xqIsmj(num1, num2);
    this.\u0023\u003DzFunQ6MROjKPXPk7oig\u003D\u003D(num1, num2);
  }

  protected void \u0023\u003DzFunQ6MROjKPXPk7oig\u003D\u003D(int _param1, int _param2)
  {
    if (this.\u0023\u003Dzu\u0024P3XgkcE7BC() == null)
      return;
    this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzosHqOAc\u003D<\u0023\u003DzE2B_RS0KvtqHnw_gRshK2QRz_uGPlEIz4W0k_ThlbVRdyLkdBA\u003D\u003D>(new \u0023\u003DzE2B_RS0KvtqHnw_gRshK2QRz_uGPlEIz4W0k_ThlbVRdyLkdBA\u003D\u003D((object) this, new Size((double) _param1, (double) _param2)));
  }

  public abstract \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I \u0023\u003Dz1cRMfLZU4Eo2();

  protected virtual void \u0023\u003Dzuq\u0024Rld84MQAH()
  {
  }

  protected virtual void \u0023\u003DzWJbfQAfmCvtw()
  {
    if (!this.\u0023\u003DzAJ8tNFa80f45)
      return;
    this.\u0023\u003DzAJ8tNFa80f45 = false;
    this.\u0023\u003DzjAYqfck\u003D();
  }

  protected virtual void \u0023\u003DzjAYqfck\u003D()
  {
    EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy> zjgUuuje = this.\u0023\u003DzjgUUUJE\u003D;
    if (zjgUuuje == null)
      return;
    Stopwatch stopwatch = Stopwatch.StartNew();
    zjgUuuje((object) this, new \u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy((\u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6) this));
    stopwatch.Stop();
    this.\u0023\u003DzoK7PFLI\u003D((double) stopwatch.ElapsedMilliseconds);
  }

  protected virtual void \u0023\u003DzoK7PFLI\u003D(double _param1)
  {
    EventHandler<\u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr> zcyGdlF8 = this.\u0023\u003DzcyGdlF8\u003D;
    if (zcyGdlF8 == null)
      return;
    zcyGdlF8((object) this, new \u0023\u003DzuPRmIFUVJkGxyCE55JH19euDoShPRPT3Wvs_KD4jptvr(_param1));
  }

  protected virtual void \u0023\u003DzPqfANn0gaWCSKnTktw\u003D\u003D(
    object _param1,
    RoutedEventArgs _param2)
  {
    Binding binding = new Binding()
    {
      RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor)
      {
        AncestorType = typeof (dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd)
      },
      Path = new PropertyPath(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440596), Array.Empty<object>())
    };
    this.SetBinding(dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003DzjroGW0xeV8YH, (BindingBase) binding);
    this.\u0023\u003DzedzqGkG8VlN1();
    this.\u0023\u003DzBH2FQrQ\u003D();
    this.\u0023\u003DzWJbfQAfmCvtw();
  }

  protected virtual void \u0023\u003DzS\u0024ZyWNYVo9XvYqd2kQ\u003D\u003D(
    object _param1,
    RoutedEventArgs _param2)
  {
    this.\u0023\u003DzedzqGkG8VlN1();
  }

  private void \u0023\u003DzedzqGkG8VlN1()
  {
    if (this.\u0023\u003Dz\u0024wP7eNcNWEpx == null)
      return;
    Interlocked.Decrement(ref dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003Dzf3Z0CZk\u003D);
    this.\u0023\u003Dz\u0024wP7eNcNWEpx.Dispose();
    this.\u0023\u003Dz\u0024wP7eNcNWEpx = (\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D) null;
  }

  private void \u0023\u003DzBH2FQrQ\u003D()
  {
    if (this.\u0023\u003Dzu\u0024P3XgkcE7BC() == null || this.\u0023\u003DzvBK\u00248KQ\u003D)
      return;
    if (this.\u0023\u003Dz\u0024wP7eNcNWEpx != null)
    {
      Interlocked.Decrement(ref dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003Dzf3Z0CZk\u003D);
      this.\u0023\u003Dz\u0024wP7eNcNWEpx.Dispose();
      this.\u0023\u003Dz\u0024wP7eNcNWEpx = (\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D) null;
    }
    Interlocked.Increment(ref dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd.\u0023\u003Dzf3Z0CZk\u003D);
    this.\u0023\u003Dz\u0024wP7eNcNWEpx = new \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D(this.MaxFrameRate, this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>(), new Action(this.\u0023\u003DzWJbfQAfmCvtw));
  }

  private void \u0023\u003DzxsT1NlAcKqkM(object _param1, SizeChangedEventArgs _param2)
  {
    if (this.UseResizeThrottle)
    {
      if (this.\u0023\u003DzJMDWU3P\u0024MwfW43PZdrim0N0\u003D != null)
        this.\u0023\u003DzJMDWU3P\u0024MwfW43PZdrim0N0\u003D.Dispose();
      this.\u0023\u003DzJMDWU3P\u0024MwfW43PZdrim0N0\u003D = \u0023\u003Dzro0Io1hfSw7LlH634iIk6E8I_CiUqBSaCicrMV4\u003D.\u0023\u003DzEbmjWGc\u003D(new Action(this.\u0023\u003Dz606jjiq_5XFxyfIiITtWCFc\u003D)).\u0023\u003DzjuNK2y0\u003D(this.ResizeThrottleMs).\u0023\u003Dz5PVA6zg\u003D((DispatcherPriority) 7).\u0023\u003Dz0g8_jDM\u003D();
    }
    else
    {
      this.\u0023\u003DzSgMgi9QlqY9x();
      this.\u0023\u003Dz5q8i9C4\u003D();
    }
  }

  private static void \u0023\u003DzL8ZRB9csVw_UcT6v5V6Hxo0\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    double? newValue = (double?) _param1.NewValue;
    if (newValue.HasValue)
    {
      if (((IComparable) newValue).\u0023\u003DzutrFxOU\u003D())
      {
        double? nullable = newValue;
        double num1 = 0.0;
        if (!(nullable.GetValueOrDefault() <= num1 & nullable.HasValue))
        {
          nullable = newValue;
          double num2 = 100.0;
          if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
            goto label_5;
        }
      }
      throw new ArgumentException(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539439066), (object) _param0.GetType().Name));
    }
label_5:
    ((dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd) _param0).\u0023\u003DzedzqGkG8VlN1();
    ((dje_zYTRT4LDE4QWDRNAUEWB3U5DLKNSTDLHTVGQEZGGZC7KYU3DXH4MC4_ejd) _param0).\u0023\u003DzBH2FQrQ\u003D();
  }

  private static void \u0023\u003DzV210hqnzErQ4(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = _param0 as dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd;
    string newValue = _param1.NewValue as string;
    if (elwvdvgwnmJ5AjuaEjd == null || newValue.\u0023\u003DzHHfYuvvaA57ehwCJow\u003D\u003D())
      return;
    Type type = Type.GetType(newValue);
    if (!(type != (Type) null) || !(Activator.CreateInstance(type) is \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 instance))
      return;
    elwvdvgwnmJ5AjuaEjd.RenderSurface = instance;
  }

  private static WriteableBitmap \u0023\u003DznU63H0xqIsmj(int _param0, int _param1)
  {
    return \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(_param0, _param1);
  }

  private void \u0023\u003DzFeNr2Uw\u003D()
  {
    this.IsTabStop = false;
    this.HorizontalAlignment = HorizontalAlignment.Stretch;
    this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
    this.VerticalAlignment = VerticalAlignment.Stretch;
    this.VerticalContentAlignment = VerticalAlignment.Stretch;
    this.\u0023\u003DzS\u0024OTg_s\u003D = new Grid();
    dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.SetSnapsToDevicePixels((DependencyObject) this, true);
    dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.SetSnapsToDevicePixels((DependencyObject) this.\u0023\u003DzS\u0024OTg_s\u003D, true);
    dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.SetSnapsToDevicePixels((DependencyObject) this.\u0023\u003Dz2TNhyDg\u003D, true);
    this.\u0023\u003DzS\u0024OTg_s\u003D.Children.Add((UIElement) this.\u0023\u003Dz2TNhyDg\u003D);
    this.Content = (object) this.\u0023\u003DzS\u0024OTg_s\u003D;
  }

  public Point TranslatePoint(
    Point _param1,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param2)
  {
    return this.\u0023\u003DzaPPLsvfM_Sst(_param1, _param2);
  }

  public bool IsPointWithinBounds(Point _param1) => this.\u0023\u003DzbOxVzAyGdX66(_param1);

  public Rect GetBoundsRelativeTo(
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param1)
  {
    return this.\u0023\u003DzdC9whUui_gN\u0024(_param1);
  }

  Style \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7.\u0023\u003DzfzpoEN5yUOQdiliEPK6UtQR9KJ5bVQ0hIUpsMqks089gdIe0uw\u003D\u003D()
  {
    return this.Style;
  }

  void \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7.\u0023\u003Dzxn9vS9UX4BfDgK8stUp1bVpGxTBScI_x\u0024oZ9Lt4\u0024I5fgHzwunA\u003D\u003D(
    Style _param1)
  {
    this.Style = _param1;
  }

  double \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }

  private void \u0023\u003DzD0y7hB3\u0024LdB07QMkVQ\u003D\u003D()
  {
    BindingOperations.ClearAllBindings((DependencyObject) this);
  }

  private void \u0023\u003Dz606jjiq_5XFxyfIiITtWCFc\u003D()
  {
    this.\u0023\u003DzSgMgi9QlqY9x();
    this.\u0023\u003Dz5q8i9C4\u003D();
  }
}
