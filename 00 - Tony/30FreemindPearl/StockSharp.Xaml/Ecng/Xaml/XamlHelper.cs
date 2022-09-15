// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.XamlHelper
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public static class XamlHelper
  {
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty DialogResultProperty;

    static XamlHelper()
    {
      string str = nameof(2127278838);
      Type type1 = typeof (bool?);
      Type type2 = typeof (XamlHelper);
      UIPropertyMetadata propertyMetadata = new UIPropertyMetadata();
      // ISSUE: method pointer
      propertyMetadata.set_PropertyChangedCallback(new PropertyChangedCallback((object) XamlHelper.SomeShit.ShitMethod02, __methodptr(\u0023\u003Dzrtw30rzpm5EKlION_Anaf8k\u003D)));
      XamlHelper.DialogResultProperty = DependencyProperty.RegisterAttached(str, type1, type2, (PropertyMetadata) propertyMetadata);
    }

    /// <summary>
    /// </summary>
    public static T FindLogicalChild<T>(this DependencyObject obj) where T : DependencyObject
    {
      if (obj == null)
        return default (T);
      T obj1 = obj as T;
      if ((object) obj1 != null)
        return obj1;
      using (IEnumerator<DependencyObject> enumerator = LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          DependencyObject current = enumerator.Current;
          T obj2 = current as T;
          if ((object) obj2 != null)
            return obj2;
          T logicalChild = current.FindLogicalChild<T>();
          if ((object) logicalChild != null)
            return logicalChild;
        }
      }
      return default (T);
    }

    /// <summary>
    /// </summary>
    public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
    {
      return obj.FindVisualChilds<T>().FirstOrDefault<T>();
    }

    /// <summary>
    /// </summary>
    public static IEnumerable<T> FindVisualChilds<T>(this DependencyObject obj) where T : DependencyObject
    {
      return (IEnumerable<T>) new XamlHelper.\u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ<T>(-2) { \u0023\u003Dz130Zs0ynp\u0024Dl = obj };
    }

    /// <summary>
    /// Boilerplate code to register attached property "bool? DialogResult"
    /// </summary>
    public static bool? GetDialogResult(DependencyObject obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(2127278809));
      return (bool?) obj.GetValue(XamlHelper.DialogResultProperty);
    }

    /// <summary>
    /// </summary>
    public static void SetDialogResult(DependencyObject obj, bool? value)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(2127278804));
      obj.SetValue(XamlHelper.DialogResultProperty, (object) value);
    }

    /// <summary>
    /// </summary>
    public static void GuiAsync(this DispatcherObject obj, Action action)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(2127278799));
      obj.get_Dispatcher().GuiAsync(action);
    }

    /// <summary>
    /// </summary>
    public static void GuiAsync(this Dispatcher dispatcher, Action action)
    {
      dispatcher.GuiAsync(action, (DispatcherPriority) 9);
    }

    /// <summary>
    /// </summary>
    public static Task<TResult> GuiThreadGetAsync<TResult>(
      Func<CancellationToken, Task<TResult>> func,
      CancellationToken token)
    {
      XamlHelper.\u0023\u003DzQWV56bPXgWklnozSzU2MLJI\u003D<TResult> pxgWklnozSzU2Mlji = new XamlHelper.\u0023\u003DzQWV56bPXgWklnozSzU2MLJI\u003D<TResult>();
      pxgWklnozSzU2Mlji.\u0023\u003DzUGk3x08\u003D = token;
      pxgWklnozSzU2Mlji.\u0023\u003DzY0lu6DE\u003D = func;
      Dispatcher dispatcher = GuiDispatcher.GlobalDispatcher.Dispatcher;
      if (dispatcher.CheckAccess())
        return pxgWklnozSzU2Mlji.\u0023\u003DzY0lu6DE\u003D(pxgWklnozSzU2Mlji.\u0023\u003DzUGk3x08\u003D);
      pxgWklnozSzU2Mlji.\u0023\u003DzFkbXWxE\u003D = new TaskCompletionSource<TResult>();
      dispatcher.GuiAsync(new Action(pxgWklnozSzU2Mlji.\u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D));
      return pxgWklnozSzU2Mlji.\u0023\u003DzFkbXWxE\u003D.Task;
    }

    /// <summary>
    /// </summary>
    public static Task GuiThreadGetAsync(
      Func<CancellationToken, Task> func,
      CancellationToken token)
    {
      return (Task) XamlHelper.GuiThreadGetAsync<object>(new Func<CancellationToken, Task<object>>(new XamlHelper.\u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D() { \u0023\u003DzY0lu6DE\u003D = func }.\u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D), token);
    }

    /// <summary>
    /// </summary>
    public static Task<TResult> GuiThreadGetAsync<TResult>(
      Func<CancellationToken, TResult> func,
      CancellationToken token)
    {
      return XamlHelper.GuiThreadGetAsync<TResult>(new Func<CancellationToken, Task<TResult>>(new XamlHelper.\u0023\u003Dzj5oesZ9ZobsUrBo6EXkbAsI\u003D<TResult>() { \u0023\u003DzY0lu6DE\u003D = func }.\u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D), token);
    }

    /// <summary>
    /// </summary>
    public static Task GuiThreadGetAsync(
      Action<CancellationToken> action,
      CancellationToken token)
    {
      return XamlHelper.GuiThreadGetAsync(new Func<CancellationToken, Task>(new XamlHelper.\u0023\u003DzotccE3NUKIPTq1tNYza9wtI\u003D() { _myAction = action }.\u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D), token);
    }

    /// <summary>
    /// </summary>
    public static void GuiAsync(
      this Dispatcher dispatcher,
      Action action,
      DispatcherPriority priority)
    {
      if (dispatcher == null)
        throw new ArgumentNullException(nameof(2127278778));
      if (action == null)
        throw new ArgumentNullException(nameof(2127278773));
      if (dispatcher.CheckAccess())
        action();
      else
        dispatcher.BeginInvoke((Delegate) action, priority, Array.Empty<object>());
    }

    /// <summary>
    /// </summary>
    [return: TupleElementNames(new string[] {"width", "height"})]
    public static ValueTuple<int, int> GetActualSize(this FrameworkElement elem)
    {
      if (elem == null)
        throw new ArgumentNullException(nameof(2127278752));
      return new ValueTuple<int, int>((int) elem.ActualWidth, (int) elem.ActualHeight);
    }

    /// <summary>
    /// </summary>
    public static Dispatcher CurrentThreadDispatcher
    {
      get
      {
        Dispatcher dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
        if (dispatcher != null)
          return dispatcher;
        throw new InvalidOperationException(nameof(2127278747));
      }
    }

    /// <summary>
    /// </summary>
    public static void GuiSync(this DispatcherObject obj, Action action)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(2127278974));
      obj.get_Dispatcher().GuiSync(action);
    }

    /// <summary>
    /// </summary>
    public static T GuiSync<T>(this DispatcherObject obj, Func<T> func)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(2127278953));
      return obj.get_Dispatcher().GuiSync<T>(func);
    }

    /// <summary>
    /// </summary>
    public static void GuiSync(this Dispatcher dispatcher, Action action)
    {
      dispatcher.GuiSync(action, (DispatcherPriority) 9);
    }

    /// <summary>
    /// </summary>
    public static void GuiSync(
      this Dispatcher dispatcher,
      Action action,
      DispatcherPriority priority)
    {
      if (dispatcher == null)
        throw new ArgumentNullException(nameof(2127278948));
      if (action == null)
        throw new ArgumentNullException(nameof(2127278943));
      if (dispatcher.CheckAccess())
        action();
      else
        dispatcher.Invoke(action, priority);
    }

    /// <summary>
    /// </summary>
    public static T GuiSync<T>(this Dispatcher dispatcher, Func<T> func)
    {
      return dispatcher.GuiSync<T>(func, (DispatcherPriority) 9);
    }

    /// <summary>
    /// </summary>
    public static T GuiSync<T>(
      this Dispatcher dispatcher,
      Func<T> func,
      DispatcherPriority priority)
    {
      if (dispatcher == null)
        throw new ArgumentNullException(nameof(2127278922));
      if (func == null)
        throw new ArgumentNullException(nameof(2127278917));
      if (!dispatcher.CheckAccess())
        return ((object) dispatcher.Invoke<T>(func, priority)).To<T>();
      return func();
    }

    /// <summary>
    /// </summary>
    public static BitmapSource GetImage(this FrameworkElement elem)
    {
      return elem.GetImage(elem.GetActualSize());
    }

    /// <summary>
    /// </summary>
    public static BitmapSource GetImage(this Visual visual, [TupleElementNames(new string[] {"width", "height"})] ValueTuple<int, int> size)
    {
      return visual.GetImage(size.Item1, size.Item2);
    }

    /// <summary>
    /// </summary>
    public static BitmapSource GetImage(this Visual visual, int width, int height)
    {
      if (visual == null)
        throw new ArgumentNullException(nameof(2127278896));
      DrawingVisual drawingVisual = new DrawingVisual();
      using (DrawingContext drawingContext = drawingVisual.RenderOpen())
      {
        VisualBrush visualBrush = new VisualBrush(visual);
        drawingContext.DrawRectangle((System.Windows.Media.Brush) visualBrush, (System.Windows.Media.Pen) null, new Rect(new Point(0.0, 0.0), new Point((double) width, (double) height)));
      }
      RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32);
      renderTargetBitmap.Render((Visual) drawingVisual);
      return (BitmapSource) renderTargetBitmap;
    }

    /// <summary>
    /// </summary>
    public static void SaveImage(this BitmapSource image, Stream file)
    {
      if (image == null)
        throw new ArgumentNullException(nameof(2127278891));
      if (file == null)
        throw new ArgumentNullException(nameof(2127278886));
      PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
      pngBitmapEncoder.Frames.Add(BitmapFrame.Create(image));
      pngBitmapEncoder.Save(file);
    }

    /// <summary>
    /// </summary>
    public static BitmapImage ToBitmapImage(this byte[] imageData)
    {
      using (MemoryStream memoryStream = new MemoryStream(imageData))
      {
        memoryStream.Position = 0L;
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.UriSource = (Uri) null;
        bitmapImage.StreamSource = (Stream) memoryStream;
        bitmapImage.EndInit();
        return bitmapImage;
      }
    }

    /// <summary>Cast value to specified type.</summary>
    /// <typeparam name="T">Return type.</typeparam>
    /// <param name="value">Source value.</param>
    /// <returns>Casted value.</returns>
    public static T WpfCast<T>(this object value)
    {
      if (value != DependencyProperty.UnsetValue)
        return value.To<T>();
      return default (T);
    }

    /// <summary>
    /// </summary>
    public static Window GetWindow(this DependencyObject obj)
    {
      return Window.GetWindow(obj);
    }

    /// <summary>
    /// </summary>
    public static bool ShowModal(this Window wnd)
    {
      if (Application.Current != null)
        return XamlHelper.ShowModal(wnd, Application.Current.MainWindow);
      bool? nullable = wnd.ShowDialog();
      return nullable.GetValueOrDefault() & nullable.HasValue;
    }

    /// <summary>
    /// </summary>
    public static bool ShowModal(this Window wnd, DependencyObject obj)
    {
      return XamlHelper.ShowModal(wnd, obj.GetWindow());
    }

    /// <summary>
    /// </summary>
    public static bool ShowModal(this Window wnd, Window owner)
    {
      if (wnd == null)
        throw new ArgumentNullException(nameof(2127278865));
      Window window1 = wnd;
      Window window2 = owner;
      if (window2 == null)
        throw new ArgumentNullException(nameof(2127278860));
      window1.Owner = window2;
      bool? nullable = wnd.ShowDialog();
      return nullable.GetValueOrDefault() & nullable.HasValue;
    }

    /// <summary>
    /// </summary>
    public static bool ShowModal(this CommonDialog dlg, DependencyObject obj)
    {
      return XamlHelper.ShowModal(dlg, obj.GetWindow());
    }

    /// <summary>
    /// </summary>
    public static bool ShowModal(this CommonDialog dlg, Window owner)
    {
      if (dlg == null)
        throw new ArgumentNullException(nameof(2127278855));
      if (owner == null)
        throw new ArgumentNullException(nameof(2127279090));
      bool? nullable = dlg.ShowDialog(owner);
      return nullable.GetValueOrDefault() & nullable.HasValue;
    }

    /// <summary>
    /// </summary>
    public static void CopyToClipboard<T>(this T value)
    {
      XamlHelper.\u0023\u003DzhyKVD2aeDlBqxvwiDTlRX4o\u003D<T> d2aeDlBqxvwiDtlRx4o = new XamlHelper.\u0023\u003DzhyKVD2aeDlBqxvwiDTlRX4o\u003D<T>();
      d2aeDlBqxvwiDtlRx4o.\u0023\u003DznEMvnog\u003D = value;
      Dispatcher dispatcher = GuiDispatcher.GlobalDispatcher.Dispatcher;
      if (!dispatcher.CheckAccess())
      {
        dispatcher.GuiAsync(new Action(d2aeDlBqxvwiDtlRx4o.\u0023\u003DzvrJjwdEXjyupAgELEA\u003D\u003D));
      }
      else
      {
        byte[] znEmvnog1 = (object) d2aeDlBqxvwiDtlRx4o.\u0023\u003DznEMvnog\u003D as byte[];
        if (znEmvnog1 == null)
        {
          Stream znEmvnog2 = (object) d2aeDlBqxvwiDtlRx4o.\u0023\u003DznEMvnog\u003D as Stream;
          if (znEmvnog2 == null)
          {
            string znEmvnog3 = (object) d2aeDlBqxvwiDtlRx4o.\u0023\u003DznEMvnog\u003D as string;
            if (znEmvnog3 == null)
            {
              BitmapSource znEmvnog4 = (object) d2aeDlBqxvwiDtlRx4o.\u0023\u003DznEMvnog\u003D as BitmapSource;
              if (znEmvnog4 == null)
                throw new NotSupportedException();
              Clipboard.SetImage(znEmvnog4);
            }
            else
              Clipboard.SetDataObject((object) znEmvnog3);
          }
          else
            Clipboard.SetAudio(znEmvnog2);
        }
        else
          Clipboard.SetAudio(znEmvnog1);
      }
    }

    /// <summary>
    /// </summary>
    public static void MakeHideable(this Window window)
    {
      if (window == null)
        throw new ArgumentNullException(nameof(2127279085));
      window.Closing += new CancelEventHandler(XamlHelper.\u0023\u003DzPhT_LGHDD0GdKZvh0w\u003D\u003D);
    }

    /// <summary>
    /// </summary>
    public static void DeleteHideable(this Window window)
    {
      if (window == null)
        throw new ArgumentNullException(nameof(2127279064));
      window.Closing -= new CancelEventHandler(XamlHelper.\u0023\u003DzPhT_LGHDD0GdKZvh0w\u003D\u003D);
    }

    private static void \u0023\u003DzPhT_LGHDD0GdKZvh0w\u003D\u003D(
      object _param0,
      CancelEventArgs _param1)
    {
      _param1.Cancel = true;
      ((Window) _param0).Hide();
    }

    /// <summary>
    /// </summary>
    public static IEnumerable<Window> GetActiveWindows(this Application app)
    {
      if (app == null)
        throw new ArgumentNullException(nameof(2127279059));
      return app.Windows.Cast<Window>().Where<Window>(XamlHelper.SomeShit.\u0023\u003DzOvjcH1IiPBxXDr9aPQ\u003D\u003D ?? (XamlHelper.SomeShit.\u0023\u003DzOvjcH1IiPBxXDr9aPQ\u003D\u003D = new Func<Window, bool>(XamlHelper.SomeShit.ShitMethod02.\u0023\u003DzL_NNu2zL2C1l7ebvazkIT_w\u003D)));
    }

    /// <summary>
    /// </summary>
    public static Window GetActiveWindow(this Application app)
    {
      return app.GetActiveWindows().FirstOrDefault<Window>();
    }

    /// <summary>
    /// </summary>
    public static Window GetActiveOrMainWindow(this Application app)
    {
      return app.GetActiveWindow() ?? app.MainWindow;
    }

    /// <summary>
    /// </summary>
    public static void SetBindings(
      this DependencyObject obj,
      DependencyProperty property,
      object dataObject,
      string path,
      BindingMode mode = BindingMode.TwoWay,
      IValueConverter converter = null,
      object parameter = null)
    {
      BindingOperations.SetBinding(obj, property, (BindingBase) new Binding(path)
      {
        Source = dataObject,
        Mode = mode,
        Converter = converter,
        ConverterParameter = parameter
      });
    }

    /// <summary>
    /// </summary>
    public static void SetBindings(
      this DependencyObject obj,
      DependencyProperty property,
      object dataObject,
      PropertyPath path,
      BindingMode mode = BindingMode.TwoWay,
      IValueConverter converter = null,
      object parameter = null)
    {
      BindingOperations.SetBinding(obj, property, (BindingBase) new Binding()
      {
        Source = dataObject,
        Path = path,
        Mode = mode,
        Converter = converter,
        ConverterParameter = parameter
      });
    }

    /// <summary>
    /// </summary>
    public static void SetMultiBinding(
      this DependencyObject obj,
      DependencyProperty prop,
      IMultiValueConverter conv,
      params Binding[] bindings)
    {
      if (bindings == null || bindings.Length <= 1)
        throw new ArgumentException(nameof(2127279054));
      MultiBinding multiBinding1 = new MultiBinding();
      IMultiValueConverter multiValueConverter = conv;
      if (multiValueConverter == null)
        throw new ArgumentNullException(nameof(2127279037));
      multiBinding1.Converter = multiValueConverter;
      MultiBinding multiBinding2 = multiBinding1;
      foreach (Binding binding in bindings)
      {
        binding.Mode = BindingMode.OneWay;
        multiBinding2.Bindings.Add((BindingBase) binding);
      }
      BindingOperations.SetBinding(obj, prop, (BindingBase) multiBinding2);
    }

    /// <summary>
    /// Checks if supplied dispatcher/dispatcher object OR current thread is associated with Dispatcher.
    /// </summary>
    /// <param name="obj">Any DispatcherObject or Dispatcher or anything else (to check using Dispatcher.FromThread())</param>
    public static void EnsureUIThread(this object obj)
    {
      Dispatcher dispatcher = (obj as DispatcherObject)?.get_Dispatcher() ?? obj as Dispatcher ?? XamlHelper.CurrentThreadDispatcher;
      if ((dispatcher != null ? (!dispatcher.CheckAccess() ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException(nameof(2127279016));
    }

    /// <summary>
    /// </summary>
    public static System.Windows.Media.Color ToTransparent(this System.Windows.Media.Color color, byte alpha)
    {
      return System.Windows.Media.Color.FromArgb(alpha, color.R, color.G, color.B);
    }

    /// <summary>
    /// </summary>
    public static bool IsDesignMode(this DependencyObject obj)
    {
      return DesignerProperties.GetIsInDesignMode(obj);
    }

    /// <summary>
    /// </summary>
    public static System.Windows.Media.Brush GetBrush(this DrawingImage source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(2127278983));
      return source.Drawing.\u0023\u003DzEzCAirU\u003D();
    }

    private static System.Windows.Media.Brush \u0023\u003DzEzCAirU\u003D(this System.Windows.Media.Drawing _param0)
    {
      GeometryDrawing geometryDrawing = _param0 as GeometryDrawing;
      if (geometryDrawing != null)
        return geometryDrawing.Brush;
      DrawingGroup drawingGroup = _param0 as DrawingGroup;
      if (drawingGroup == null)
        return (System.Windows.Media.Brush) null;
      foreach (System.Windows.Media.Drawing child in drawingGroup.Children)
      {
        System.Windows.Media.Brush brush = child.\u0023\u003DzEzCAirU\u003D();
        if (brush != null)
          return brush;
      }
      return (System.Windows.Media.Brush) null;
    }

    /// <summary>
    /// </summary>
    public static void UpdateBrush(this DrawingImage source, System.Windows.Media.Brush brush)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(2127278194));
      source.Drawing.\u0023\u003Dz64UATUI\u003D(brush);
    }

    /// <summary>
    /// </summary>
    public static void UpdatePen(this DrawingImage source, System.Windows.Media.Pen pen)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(2127278189));
      source.Drawing.\u0023\u003Dz0uzqTbo\u003D(pen);
    }

    private static void \u0023\u003Dz64UATUI\u003D(this System.Windows.Media.Drawing _param0, System.Windows.Media.Brush _param1)
    {
      if (_param1 == null)
        throw new ArgumentNullException(nameof(2127278168));
      GeometryDrawing geometryDrawing = _param0 as GeometryDrawing;
      if (geometryDrawing == null)
      {
        DrawingGroup drawingGroup = _param0 as DrawingGroup;
        if (drawingGroup == null)
          return;
        XamlHelper.\u0023\u003Dz64UATUI\u003D(drawingGroup, _param1);
      }
      else
        geometryDrawing.Brush = _param1;
    }

    private static void \u0023\u003Dz0uzqTbo\u003D(this System.Windows.Media.Drawing _param0, System.Windows.Media.Pen _param1)
    {
      if (_param1 == null)
        throw new ArgumentNullException(nameof(2127278163));
      GeometryDrawing geometryDrawing = _param0 as GeometryDrawing;
      if (geometryDrawing == null)
      {
        DrawingGroup drawingGroup = _param0 as DrawingGroup;
        if (drawingGroup == null)
          return;
        XamlHelper.\u0023\u003Dz0uzqTbo\u003D(drawingGroup, _param1);
      }
      else
        geometryDrawing.Pen = _param1;
    }

    private static void \u0023\u003Dz64UATUI\u003D(this DrawingGroup _param0, System.Windows.Media.Brush _param1)
    {
      if (_param0 == null)
        throw new ArgumentNullException(nameof(2127278158));
      foreach (System.Windows.Media.Drawing child in _param0.Children)
        child.\u0023\u003Dz64UATUI\u003D(_param1);
    }

    private static void \u0023\u003Dz0uzqTbo\u003D(this DrawingGroup _param0, System.Windows.Media.Pen _param1)
    {
      foreach (System.Windows.Media.Drawing child in _param0.Children)
        child.\u0023\u003Dz0uzqTbo\u003D(_param1);
    }

    /// <summary>
    /// </summary>
    public static int ToInt(this System.Windows.Media.Color color)
    {
      return (int) color.A << 24 | (int) color.R << 16 | (int) color.G << 8 | (int) color.B;
    }

    /// <summary>
    /// </summary>
    public static System.Windows.Media.Color ToColor(this int color)
    {
      return System.Windows.Media.Color.FromArgb((byte) (color >> 24), (byte) (color >> 16), (byte) (color >> 8), (byte) color);
    }

    /// <summary>
    /// Convert <see cref="T:System.Drawing.Color" /> value to <see cref="T:System.Windows.Media.Color" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="T:System.Drawing.Color" /> value.</param>
    /// <returns>
    /// <see cref="T:System.Windows.Media.Color" /> value.</returns>
    public static System.Windows.Media.Color ToWpf(this System.Drawing.Color value)
    {
      return value.ToArgb().ToColor();
    }

    /// <summary>
    /// Convert <see cref="T:System.Windows.Media.Color" /> value to <see cref="T:System.Drawing.Color" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="T:System.Windows.Media.Color" /> value.</param>
    /// <returns>
    /// <see cref="T:System.Drawing.Color" /> value.</returns>
    public static System.Drawing.Color FromWpf(this System.Windows.Media.Color value)
    {
      return System.Drawing.Color.FromArgb(value.ToInt());
    }

    /// <summary>
    /// Convert <see cref="T:System.Drawing.Brush" /> value to <see cref="T:System.Windows.Media.Brush" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="T:System.Drawing.Brush" /> value.</param>
    /// <returns>
    /// <see cref="T:System.Windows.Media.Brush" /> value.</returns>
    public static System.Windows.Media.Brush ToWpf(this System.Drawing.Brush value)
    {
      if (value == null)
        return (System.Windows.Media.Brush) null;
      SolidBrush solidBrush = value as SolidBrush;
      if (solidBrush != null)
        return (System.Windows.Media.Brush) new SolidColorBrush(solidBrush.Color.ToWpf());
      System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush = value as System.Drawing.Drawing2D.LinearGradientBrush;
      if (linearGradientBrush == null)
        throw new NotSupportedException();
      System.Drawing.Color linearColor1 = linearGradientBrush.LinearColors[0];
      System.Drawing.Color linearColor2 = linearGradientBrush.LinearColors[1];
      System.Windows.Media.Color wpf1 = linearColor1.ToWpf();
      System.Windows.Media.Color wpf2 = linearColor2.ToWpf();
      Point startPoint = new Point((double) linearGradientBrush.Rectangle.X, (double) linearGradientBrush.Rectangle.Y);
      RectangleF rectangle = linearGradientBrush.Rectangle;
      double right = (double) rectangle.Right;
      rectangle = linearGradientBrush.Rectangle;
      double bottom = (double) rectangle.Bottom;
      Point endPoint = new Point(right, bottom);
      return (System.Windows.Media.Brush) new System.Windows.Media.LinearGradientBrush(wpf1, wpf2, startPoint, endPoint);
    }

    /// <summary>
    /// Convert <see cref="T:System.Windows.Media.Brush" /> value to <see cref="T:System.Drawing.Brush" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="T:System.Windows.Media.Brush" /> value.</param>
    /// <returns>
    /// <see cref="T:System.Drawing.Brush" /> value.</returns>
    public static System.Drawing.Brush FromWpf(this System.Windows.Media.Brush value)
    {
      if (value == null)
        return (System.Drawing.Brush) null;
      SolidColorBrush solidColorBrush = value as SolidColorBrush;
      if (solidColorBrush != null)
        return (System.Drawing.Brush) new SolidBrush(solidColorBrush.Color.FromWpf());
      System.Windows.Media.LinearGradientBrush linearGradientBrush = value as System.Windows.Media.LinearGradientBrush;
      if (linearGradientBrush == null)
        throw new NotSupportedException();
      Point startPoint = linearGradientBrush.StartPoint;
      GradientStopCollection gradientStops = linearGradientBrush.GradientStops;
      GradientStop gradientStop1 = gradientStops[0];
      GradientStop gradientStop2 = gradientStops[1];
      return (System.Drawing.Brush) new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int) (((Point) ref startPoint).get_X() + gradientStop1.Offset), (int) ((Point) ref startPoint).get_Y()), new Point((int) (((Point) ref startPoint).get_X() + gradientStop2.Offset), (int) ((Point) ref startPoint).get_Y()), gradientStop1.Color.FromWpf(), gradientStop2.Color.FromWpf());
    }

    /// <summary>
    /// Convert <see cref="T:System.Drawing.PointF" /> value to <see cref="T:System.Windows.Point" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="T:System.Drawing.PointF" /> value.</param>
    /// <returns>
    /// <see cref="T:System.Windows.Point" /> value.</returns>
    public static Point ToWpf(this PointF value)
    {
      return new Point((double) value.X, (double) value.Y);
    }

    /// <summary>
    /// Convert <see cref="T:System.Windows.Point" /> value to <see cref="T:System.Drawing.PointF" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="T:System.Windows.Point" /> value.</param>
    /// <returns>
    /// <see cref="T:System.Drawing.PointF" /> value.</returns>
    public static PointF FromWpf(this Point value)
    {
      return new PointF((float) ((Point) ref value).get_X(), (float) ((Point) ref value).get_Y());
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly XamlHelper.SomeShit ShitMethod02 = new XamlHelper.SomeShit();
      public static Func<Window, bool> \u0023\u003DzOvjcH1IiPBxXDr9aPQ\u003D\u003D;

      internal bool \u0023\u003DzL_NNu2zL2C1l7ebvazkIT_w\u003D(Window _param1)
      {
        return _param1.IsActive;
      }

      internal void \u0023\u003Dzrtw30rzpm5EKlION_Anaf8k\u003D(
        DependencyObject _param1,
        DependencyPropertyChangedEventArgs _param2)
      {
        XamlHelper.\u0023\u003DzGO8P3O0Oe1cOl_yTbqj5OCE\u003D o0Oe1cOlYTbqj5Oce = new XamlHelper.\u0023\u003DzGO8P3O0Oe1cOl_yTbqj5OCE\u003D();
        o0Oe1cOlYTbqj5Oce.\u0023\u003Dzr38O_RA\u003D = (Button) _param1;
        o0Oe1cOlYTbqj5Oce.\u0023\u003Dzr38O_RA\u003D.Click += new RoutedEventHandler(o0Oe1cOlYTbqj5Oce.\u0023\u003DziSzdHKQZzhNgvhhO7LT3pwc\u003D);
      }
    }

    private sealed class \u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ<\u0023\u003DznSahTwA\u003D> : IEnumerable<\u0023\u003DznSahTwA\u003D>, IEnumerable, IEnumerator<\u0023\u003DznSahTwA\u003D>, IEnumerator, IDisposable
      where \u0023\u003DznSahTwA\u003D : DependencyObject
    {
      
      private int \u0023\u003DzmZCvlu0uWGNm;
      
      private \u0023\u003DznSahTwA\u003D \u0023\u003DzWvH5n6COaRvj;
      
      private int \u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D;
      
      private DependencyObject \u0023\u003DzkEoWorI\u003D;
      
      public DependencyObject \u0023\u003Dz130Zs0ynp\u0024Dl;
      
      private int \u0023\u003Dzmx\u0024vtRlnPb5c;
      
      private DependencyObject \u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D;
      
      private IEnumerator<\u0023\u003DznSahTwA\u003D> \u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D;

      [DebuggerHidden]
      public \u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ(int _param1)
      {
        this.\u0023\u003DzmZCvlu0uWGNm = _param1;
        this.\u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D = Environment.CurrentManagedThreadId;
      }

      [DebuggerHidden]
      void IDisposable.\u0023\u003DzkQukXWEgNG41rF31Rw\u003D\u003D()
      {
        switch (this.\u0023\u003DzmZCvlu0uWGNm)
        {
          case -3:
          case 2:
            try
            {
            }
            finally
            {
              this.\u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D();
            }
            break;
        }
      }

      bool IEnumerator.MoveNext()
      {
        // ISSUE: fault handler
        try
        {
          switch (this.\u0023\u003DzmZCvlu0uWGNm)
          {
            case 0:
              this.\u0023\u003DzmZCvlu0uWGNm = -1;
              this.\u0023\u003Dzmx\u0024vtRlnPb5c = 0;
              goto label_11;
            case 1:
              this.\u0023\u003DzmZCvlu0uWGNm = -1;
              break;
            case 2:
              this.\u0023\u003DzmZCvlu0uWGNm = -3;
              goto label_9;
            default:
              return false;
          }
label_6:
          this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D = this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D.FindVisualChilds<\u0023\u003DznSahTwA\u003D>().GetEnumerator();
          this.\u0023\u003DzmZCvlu0uWGNm = -3;
label_9:
          if (this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D.MoveNext())
          {
            this.\u0023\u003DzWvH5n6COaRvj = this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D.Current;
            this.\u0023\u003DzmZCvlu0uWGNm = 2;
            return true;
          }
          this.\u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D();
          this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D = (IEnumerator<\u0023\u003DznSahTwA\u003D>) null;
          this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D = (DependencyObject) null;
          ++this.\u0023\u003Dzmx\u0024vtRlnPb5c;
label_11:
          if (this.\u0023\u003Dzmx\u0024vtRlnPb5c >= VisualTreeHelper.GetChildrenCount(this.\u0023\u003DzkEoWorI\u003D))
            return false;
          this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D = VisualTreeHelper.GetChild(this.\u0023\u003DzkEoWorI\u003D, this.\u0023\u003Dzmx\u0024vtRlnPb5c);
          \u0023\u003DznSahTwA\u003D yfKllAUwFzSmE7Bea = this.\u0023\u003DzYFKllA_uwFzSmE7BEA\u003D\u003D as \u0023\u003DznSahTwA\u003D;
          if ((object) yfKllAUwFzSmE7Bea != null)
          {
            this.\u0023\u003DzWvH5n6COaRvj = yfKllAUwFzSmE7Bea;
            this.\u0023\u003DzmZCvlu0uWGNm = 1;
            return true;
          }
          goto label_6;
        }
        __fault
        {
          this.\u0023\u003DzkQukXWEgNG41rF31Rw\u003D\u003D();
        }
      }

      private void \u0023\u003DzpscbbenJpKdpEDtciA\u003D\u003D()
      {
        this.\u0023\u003DzmZCvlu0uWGNm = -1;
        if (this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D == null)
          return;
        this.\u0023\u003DzTEy3ohYJbQQb_CB9SA\u003D\u003D.Dispose();
      }

      [DebuggerHidden]
      \u0023\u003DznSahTwA\u003D IEnumerator<\u0023\u003DznSahTwA\u003D>.\u0023\u003DzUdIx4uHGmeS8GtRxu6exQAqa6I8f()
      {
        return this.\u0023\u003DzWvH5n6COaRvj;
      }

      [DebuggerHidden]
      void IEnumerator.\u0023\u003DzPz2CkL3H4EO2s8Al1w\u003D\u003D()
      {
        throw new NotSupportedException();
      }

      [DebuggerHidden]
      object IEnumerator.\u0023\u003DzUvRtPry9Z3_r4VkfO99sKo4\u003D()
      {
        return (object) this.\u0023\u003DzWvH5n6COaRvj;
      }

      [DebuggerHidden]
      [return: Nullable(new byte[] {1, 0})]
      IEnumerator<\u0023\u003DznSahTwA\u003D> IEnumerable<\u0023\u003DznSahTwA\u003D>.\u0023\u003DzHW1acyQZIGnGvJbOS44ATNpP3OQa()
      {
        XamlHelper.\u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ<\u0023\u003DznSahTwA\u003D> dcyb5cZafHptn0k6tHvSz;
        if (this.\u0023\u003DzmZCvlu0uWGNm == -2 && this.\u0023\u003DzvtNL1OGWjIFr14kY\u0024A\u003D\u003D == Environment.CurrentManagedThreadId)
        {
          this.\u0023\u003DzmZCvlu0uWGNm = 0;
          dcyb5cZafHptn0k6tHvSz = this;
        }
        else
          dcyb5cZafHptn0k6tHvSz = new XamlHelper.\u0023\u003Dz9SB4p7iDcyb5cZAfHPtn0k6tHvSZ<\u0023\u003DznSahTwA\u003D>(0);
        dcyb5cZafHptn0k6tHvSz.\u0023\u003DzkEoWorI\u003D = this.\u0023\u003Dz130Zs0ynp\u0024Dl;
        return (IEnumerator<\u0023\u003DznSahTwA\u003D>) dcyb5cZafHptn0k6tHvSz;
      }

      [DebuggerHidden]
      [return: Nullable(1)]
      IEnumerator IEnumerable.\u0023\u003DzGBBKW4936O1cYMySvZ\u0024sDvw\u003D()
      {
        return (IEnumerator) this.\u0023\u003DzHW1acyQZIGnGvJbOS44ATNpP3OQa();
      }
    }

    private sealed class \u0023\u003DzGO8P3O0Oe1cOl_yTbqj5OCE\u003D
    {
      public Button \u0023\u003Dzr38O_RA\u003D;

      internal void \u0023\u003DziSzdHKQZzhNgvhhO7LT3pwc\u003D(
        object _param1,
        RoutedEventArgs _param2)
      {
        this.\u0023\u003Dzr38O_RA\u003D.GetWindow().DialogResult = XamlHelper.GetDialogResult((DependencyObject) this.\u0023\u003Dzr38O_RA\u003D);
      }
    }

    private sealed class \u0023\u003DzQWV56bPXgWklnozSzU2MLJI\u003D<\u0023\u003Dz\u0024eO7x5w\u003D>
    {
      public CancellationToken \u0023\u003DzUGk3x08\u003D;
      public TaskCompletionSource<\u0023\u003Dz\u0024eO7x5w\u003D> \u0023\u003DzFkbXWxE\u003D;
      public Func<CancellationToken, Task<\u0023\u003Dz\u0024eO7x5w\u003D>> \u0023\u003DzY0lu6DE\u003D;

      internal async void \u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D()
      {
        try
        {
          this.\u0023\u003DzUGk3x08\u003D.ThrowIfCancellationRequested();
          TaskCompletionSource<\u0023\u003Dz\u0024eO7x5w\u003D> completionSource = this.\u0023\u003DzFkbXWxE\u003D;
          completionSource.SetResult(await this.\u0023\u003DzY0lu6DE\u003D(this.\u0023\u003DzUGk3x08\u003D));
          completionSource = (TaskCompletionSource<\u0023\u003Dz\u0024eO7x5w\u003D>) null;
        }
        catch (OperationCanceledException ex)
        {
          this.\u0023\u003DzFkbXWxE\u003D.SetCanceled();
        }
        catch (Exception ex)
        {
          this.\u0023\u003DzFkbXWxE\u003D.SetException(ex);
        }
      }

      [StructLayout(LayoutKind.Auto)]
      private struct \u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D : IAsyncStateMachine
      {
        
        public int \u0023\u003DzmZCvlu0uWGNm;
        
        public AsyncVoidMethodBuilder \u0023\u003Dzu1QJGuMAn80M;
        
        public XamlHelper.\u0023\u003DzQWV56bPXgWklnozSzU2MLJI\u003D<\u0023\u003Dz\u0024eO7x5w\u003D> _delayActionHelper;
        
        private TaskCompletionSource<\u0023\u003Dz\u0024eO7x5w\u003D> \u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D;
        
        private TaskAwaiter<\u0023\u003Dz\u0024eO7x5w\u003D> \u0023\u003DzxUTTsn8LiP4f;

        void IAsyncStateMachine.MoveNext()
        {
          int zmZcvlu0uWgNm = this.\u0023\u003DzmZCvlu0uWGNm;
          XamlHelper.\u0023\u003DzQWV56bPXgWklnozSzU2MLJI\u003D<\u0023\u003Dz\u0024eO7x5w\u003D> z0sZyIb2k8Bgs = this._delayActionHelper;
          try
          {
            try
            {
              TaskAwaiter<\u0023\u003Dz\u0024eO7x5w\u003D> awaiter;
              int num;
              if (zmZcvlu0uWgNm != 0)
              {
                z0sZyIb2k8Bgs.\u0023\u003DzUGk3x08\u003D.ThrowIfCancellationRequested();
                this.\u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D = z0sZyIb2k8Bgs.\u0023\u003DzFkbXWxE\u003D;
                awaiter = z0sZyIb2k8Bgs.\u0023\u003DzY0lu6DE\u003D(z0sZyIb2k8Bgs.\u0023\u003DzUGk3x08\u003D).GetAwaiter();
                if (!awaiter.IsCompleted)
                {
                  this.\u0023\u003DzmZCvlu0uWGNm = num = 0;
                  this.\u0023\u003DzxUTTsn8LiP4f = awaiter;
                  this.\u0023\u003Dzu1QJGuMAn80M.AwaitUnsafeOnCompleted<TaskAwaiter<\u0023\u003Dz\u0024eO7x5w\u003D>, XamlHelper.\u0023\u003DzQWV56bPXgWklnozSzU2MLJI\u003D<\u0023\u003Dz\u0024eO7x5w\u003D>.\u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D>(ref awaiter, ref this);
                  return;
                }
              }
              else
              {
                awaiter = this.\u0023\u003DzxUTTsn8LiP4f;
                this.\u0023\u003DzxUTTsn8LiP4f = new TaskAwaiter<\u0023\u003Dz\u0024eO7x5w\u003D>();
                this.\u0023\u003DzmZCvlu0uWGNm = num = -1;
              }
              this.\u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D.SetResult(awaiter.GetResult());
              this.\u0023\u003Dzj1lKA\u0024ALRacJw0jYmw\u003D\u003D = (TaskCompletionSource<\u0023\u003Dz\u0024eO7x5w\u003D>) null;
            }
            catch (OperationCanceledException ex)
            {
              z0sZyIb2k8Bgs.\u0023\u003DzFkbXWxE\u003D.SetCanceled();
            }
            catch (Exception ex)
            {
              z0sZyIb2k8Bgs.\u0023\u003DzFkbXWxE\u003D.SetException(ex);
            }
          }
          catch (Exception ex)
          {
            this.\u0023\u003DzmZCvlu0uWGNm = -2;
            this.\u0023\u003Dzu1QJGuMAn80M.SetException(ex);
            return;
          }
          this.\u0023\u003DzmZCvlu0uWGNm = -2;
          this.\u0023\u003Dzu1QJGuMAn80M.SetResult();
        }

        [DebuggerHidden]
        void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine _param1)
        {
          this.\u0023\u003Dzu1QJGuMAn80M.SetStateMachine(_param1);
        }
      }
    }

    private sealed class \u0023\u003DzhyKVD2aeDlBqxvwiDTlRX4o\u003D<\u0023\u003DznSahTwA\u003D>
    {
      public \u0023\u003DznSahTwA\u003D \u0023\u003DznEMvnog\u003D;

      internal void \u0023\u003DzvrJjwdEXjyupAgELEA\u003D\u003D()
      {
        this.\u0023\u003DznEMvnog\u003D.CopyToClipboard<\u0023\u003DznSahTwA\u003D>();
      }
    }

    private sealed class \u0023\u003Dzj5oesZ9ZobsUrBo6EXkbAsI\u003D<\u0023\u003Dz\u0024eO7x5w\u003D>
    {
      public Func<CancellationToken, \u0023\u003Dz\u0024eO7x5w\u003D> \u0023\u003DzY0lu6DE\u003D;

      internal Task<\u0023\u003Dz\u0024eO7x5w\u003D> \u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D(
        CancellationToken _param1)
      {
        return Task.FromResult<\u0023\u003Dz\u0024eO7x5w\u003D>(this.\u0023\u003DzY0lu6DE\u003D(_param1));
      }
    }

    private sealed class \u0023\u003DzotccE3NUKIPTq1tNYza9wtI\u003D
    {
      public Action<CancellationToken> _myAction;

      internal Task \u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D(CancellationToken _param1)
      {
        this._myAction(_param1);
        return Task.CompletedTask;
      }
    }

    private sealed class \u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D
    {
      public Func<CancellationToken, Task> \u0023\u003DzY0lu6DE\u003D;

      internal async Task<object> \u0023\u003Dzju1MKb8dq9tXBqFg3_aIsj0\u003D(
        CancellationToken _param1)
      {
        await this.\u0023\u003DzY0lu6DE\u003D(_param1);
        object obj;
        return obj;
      }

      [StructLayout(LayoutKind.Auto)]
      private struct \u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D : IAsyncStateMachine
      {
        
        public int \u0023\u003DzmZCvlu0uWGNm;
        
        public AsyncTaskMethodBuilder<object> \u0023\u003Dzu1QJGuMAn80M;
        
        public XamlHelper.\u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D _delayActionHelper;
        
        public CancellationToken \u0023\u003DzRsets6Q\u003D;
        
        private TaskAwaiter \u0023\u003DzxUTTsn8LiP4f;

        void IAsyncStateMachine.MoveNext()
        {
          int zmZcvlu0uWgNm = this.\u0023\u003DzmZCvlu0uWGNm;
          XamlHelper.\u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D z0sZyIb2k8Bgs = this._delayActionHelper;
          object result;
          try
          {
            TaskAwaiter awaiter;
            int num;
            if (zmZcvlu0uWgNm != 0)
            {
              awaiter = z0sZyIb2k8Bgs.\u0023\u003DzY0lu6DE\u003D(this.\u0023\u003DzRsets6Q\u003D).GetAwaiter();
              if (!awaiter.IsCompleted)
              {
                this.\u0023\u003DzmZCvlu0uWGNm = num = 0;
                this.\u0023\u003DzxUTTsn8LiP4f = awaiter;
                this.\u0023\u003Dzu1QJGuMAn80M.AwaitUnsafeOnCompleted<TaskAwaiter, XamlHelper.\u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D.\u0023\u003DzVAbuZPtBIyuBuUKcw7qO\u0024Hg\u003D>(ref awaiter, ref this);
                return;
              }
            }
            else
            {
              awaiter = this.\u0023\u003DzxUTTsn8LiP4f;
              this.\u0023\u003DzxUTTsn8LiP4f = new TaskAwaiter();
              this.\u0023\u003DzmZCvlu0uWGNm = num = -1;
            }
            awaiter.GetResult();
            result = (object) null;
          }
          catch (Exception ex)
          {
            this.\u0023\u003DzmZCvlu0uWGNm = -2;
            this.\u0023\u003Dzu1QJGuMAn80M.SetException(ex);
            return;
          }
          this.\u0023\u003DzmZCvlu0uWGNm = -2;
          this.\u0023\u003Dzu1QJGuMAn80M.SetResult(result);
        }

        [DebuggerHidden]
        void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine _param1)
        {
          this.\u0023\u003Dzu1QJGuMAn80M.SetStateMachine(_param1);
        }
      }
    }
  }
}
