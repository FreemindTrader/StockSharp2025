// Decompiled with JetBrains decompiler
// Type: #=z7oKBks6ccXdMBOl$qXdcQJfGolgX9jTeAGFiNSE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

#nullable disable
internal static class \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQJfGolgX9jTeAGFiNSE\u003D
{
  internal static void \u0023\u003DzI0WdlDcUgrX_(this UIElement _param0)
  {
    _param0.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
    _param0.Arrange(new Rect(new Point(0.0, 0.0), _param0.DesiredSize));
  }

  internal static bool \u0023\u003DzST\u0024t7rI\u003D(this UIElement _param0)
  {
    return _param0.Visibility == Visibility.Visible;
  }

  internal static bool \u0023\u003DzBqQ1kl1\u0024w5oI(this DependencyObject _param0)
  {
    Window mainWindow = Application.Current.MainWindow;
    return _param0.\u0023\u003DzBqQ1kl1\u0024w5oI((DependencyObject) mainWindow);
  }

  internal static bool \u0023\u003DzBqQ1kl1\u0024w5oI(
    this DependencyObject _param0,
    DependencyObject _param1)
  {
    for (DependencyObject reference = _param0; reference != null; reference = VisualTreeHelper.GetParent(reference))
    {
      if (reference == _param1)
        return true;
    }
    return false;
  }

  internal static Point \u0023\u003DzaPPLsvfM_Sst(
    this FrameworkElement _param0,
    Point _param1,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param2)
  {
    return _param2 is UIElement relativeTo ? _param0.TranslatePoint(_param1, relativeTo) : new Point(0.0, 0.0);
  }

  internal static bool \u0023\u003DzbOxVzAyGdX66(this FrameworkElement _param0, Point _param1)
  {
    Point point = _param0.TranslatePoint(_param1, (UIElement) _param0);
    return point.X <= _param0.ActualWidth && point.X >= 0.0 && point.Y <= _param0.ActualHeight && point.Y >= 0.0;
  }

  internal static Rect \u0023\u003DzdC9whUui_gN\u0024(
    this FrameworkElement _param0,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param1)
  {
    UIElement uiElement = _param1 as UIElement;
    Rect? nullable = new Rect?();
    if (uiElement != null)
      nullable = _param0.\u0023\u003DzdC9whUui_gN\u0024(uiElement);
    return !nullable.HasValue ? Rect.Empty : nullable.Value;
  }

  internal static Rect? \u0023\u003DzdC9whUui_gN\u0024(
    this FrameworkElement _param0,
    UIElement _param1)
  {
    try
    {
      GeneralTransform visual = _param0.TransformToVisual((Visual) _param1);
      if (visual != null)
      {
        Point result1;
        if (visual.TryTransform(new Point(), out result1))
        {
          Point result2;
          if (visual.TryTransform(new Point(_param0.ActualWidth, _param0.ActualHeight), out result2))
            return new Rect?(new Rect(result1, result2));
        }
      }
    }
    catch
    {
    }
    return new Rect?();
  }

  public static T \u0023\u003Dz\u0024h0EaBALwnXr<T>(this FrameworkElement _param0) where T : FrameworkElement
  {
    return !(_param0.Parent is FrameworkElement parent) || parent is T ? (T) parent : parent.\u0023\u003Dz\u0024h0EaBALwnXr<T>();
  }

  public static T \u0023\u003DzTXWydaSPeI\u0024J<T>(this UIElement _param0) where T : UIElement
  {
    for (UIElement reference = _param0; reference != null; reference = VisualTreeHelper.GetParent((DependencyObject) reference) as UIElement)
    {
      if (reference is T obj)
        return obj;
    }
    return default (T);
  }

  public static WriteableBitmap \u0023\u003DzWmxEXx9881f\u0024(this FrameworkElement _param0)
  {
    _param0.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
    _param0.Arrange(new Rect(new Point(0.0, 0.0), _param0.DesiredSize));
    Size desiredSize = _param0.DesiredSize;
    int width = (int) desiredSize.Width;
    desiredSize = _param0.DesiredSize;
    int height = (int) desiredSize.Height;
    PixelFormat pbgra32 = PixelFormats.Pbgra32;
    RenderTargetBitmap source = new RenderTargetBitmap(width, height, 96.0, 96.0, pbgra32);
    source.Render((Visual) _param0);
    return new WriteableBitmap((BitmapSource) source);
  }

  public static WriteableBitmap \u0023\u003DzWmxEXx9881f\u0024(
    this FrameworkElement _param0,
    int _param1,
    int _param2)
  {
    RenderTargetBitmap source = new RenderTargetBitmap(_param1, _param2, 96.0, 96.0, PixelFormats.Pbgra32);
    source.Render((Visual) _param0);
    return new WriteableBitmap((BitmapSource) source);
  }

  public static void \u0023\u003DzevTCbw5vauBB(
    this FrameworkElement _param0,
    string _param1,
    PropertyChangedCallback _param2)
  {
    Binding binding = new Binding(_param1)
    {
      Source = (object) _param0
    };
    DependencyProperty dp = DependencyProperty.RegisterAttached("" + _param1, typeof (object), ((object) _param0).GetType(), new PropertyMetadata(_param2));
    _param0.SetBinding(dp, (BindingBase) binding);
  }

  internal static void \u0023\u003DzEYsWXVUJ8mGX(
    this FrameworkElement _param0,
    DependencyProperty _param1,
    object _param2)
  {
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQJfGolgX9jTeAGFiNSE\u003D.\u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D lmbTiAitFc7nJ884 = new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQJfGolgX9jTeAGFiNSE\u003D.\u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D();
    lmbTiAitFc7nJ884.\u0023\u003Dz_i6sZDg\u003D = _param0;
    lmbTiAitFc7nJ884.\u0023\u003Dz89jjF1s\u003D = _param1;
    lmbTiAitFc7nJ884.\u0023\u003DzxGz2_8k\u003D = _param2;
    Dispatcher dispatcher = ((DispatcherObject) lmbTiAitFc7nJ884.\u0023\u003Dz_i6sZDg\u003D).Dispatcher;
    Action action = new Action(lmbTiAitFc7nJ884.\u0023\u003DzjX0s_IsrHsVW2ijiPdnNHq0\u003D);
    if (dispatcher.CheckAccess())
      action();
    else
      dispatcher.BeginInvoke((Delegate) action, Array.Empty<object>());
  }

  internal static void \u0023\u003DzHGGuFpQ\u003D(
    this FrameworkElement _param0,
    DispatcherPriority _param1,
    Action _param2)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param0, "");
    if (\u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D.\u0023\u003DzMNVV9_LtRFVB())
      _param2();
    else
      ((DispatcherObject) _param0).Dispatcher.BeginInvoke((Delegate) _param2, _param1, Array.Empty<object>());
  }

  private sealed class \u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D
  {
    public FrameworkElement \u0023\u003Dz_i6sZDg\u003D;
    public DependencyProperty \u0023\u003Dz89jjF1s\u003D;
    public object \u0023\u003DzxGz2_8k\u003D;

    internal void \u0023\u003DzjX0s_IsrHsVW2ijiPdnNHq0\u003D()
    {
      this.\u0023\u003Dz_i6sZDg\u003D.SetValue(this.\u0023\u003Dz89jjF1s\u003D, this.\u0023\u003DzxGz2_8k\u003D);
    }
  }
}
