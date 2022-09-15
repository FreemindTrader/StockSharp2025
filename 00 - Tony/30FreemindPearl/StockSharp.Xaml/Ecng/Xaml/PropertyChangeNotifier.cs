// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.PropertyChangeNotifier
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public sealed class PropertyChangeNotifier : DependencyObject, IDisposable
  {
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(2127277753), typeof (object), typeof (PropertyChangeNotifier), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzHv9p87s\u003D))));
    
    private readonly WeakReference \u0023\u003DzFGE_Pj0\u003D;

    /// <summary>
    /// </summary>
    public PropertyChangeNotifier(DependencyObject propertySource, string path)
      : this(propertySource, new PropertyPath(path, Array.Empty<object>()))
    {
    }

    /// <summary>
    /// </summary>
    public PropertyChangeNotifier(DependencyObject propertySource, DependencyProperty property)
      : this(propertySource, new PropertyPath((object) property))
    {
    }

    /// <summary>
    /// </summary>
    public PropertyChangeNotifier(DependencyObject propertySource, PropertyPath property)
    {
      this.\u002Ector();
      if (propertySource == null)
        throw new ArgumentNullException(nameof(2127280055));
      if (property == null)
        throw new ArgumentNullException(nameof(2127280034));
      this.\u0023\u003DzFGE_Pj0\u003D = new WeakReference((object) propertySource);
      Binding binding = new Binding() { Path = property, Mode = BindingMode.OneWay, Source = (object) propertySource };
      BindingOperations.SetBinding((DependencyObject) this, PropertyChangeNotifier.ValueProperty, (BindingBase) binding);
    }

    private static void \u0023\u003DzHv9p87s\u003D(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      Action z6CcXvpw = ((PropertyChangeNotifier) _param0).\u0023\u003Dz6CCXvpw\u003D;
      if (z6CcXvpw == null)
        return;
      z6CcXvpw();
    }

    /// <summary>
    /// </summary>
    [Bindable(true)]
    public object Value
    {
      get
      {
        return this.GetValue(PropertyChangeNotifier.ValueProperty);
      }
      set
      {
        this.SetValue(PropertyChangeNotifier.ValueProperty, value);
      }
    }

    /// <summary>
    /// </summary>
    public DependencyObject PropertySource
    {
      get
      {
        try
        {
          return this.\u0023\u003DzFGE_Pj0\u003D.IsAlive ? this.\u0023\u003DzFGE_Pj0\u003D.Target as DependencyObject : (DependencyObject) null;
        }
        catch
        {
          return (DependencyObject) null;
        }
      }
    }

    /// <summary>
    /// </summary>
    public event Action ValueChanged;

    /// <inheritdoc />
    public void Dispose()
    {
      BindingOperations.ClearBinding((DependencyObject) this, PropertyChangeNotifier.ValueProperty);
    }
  }
}
