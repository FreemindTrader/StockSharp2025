// Decompiled with JetBrains decompiler
// Type: -.PointMarker
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class PointMarker : 
  TemplatableControl
{
  
  public static readonly DependencyProperty \u0023\u003DzTwmsLrVpwa38 = DependencyProperty.Register(nameof (DeferredContent), typeof (DataTemplate), typeof (PointMarker), new PropertyMetadata(new PropertyChangedCallback(PointMarker.\u0023\u003Dzw52y5BiydelX)));

  public DataTemplate DeferredContent
  {
    get
    {
      return (DataTemplate) this.GetValue(PointMarker.\u0023\u003DzTwmsLrVpwa38);
    }
    set
    {
      this.SetValue(PointMarker.\u0023\u003DzTwmsLrVpwa38, (object) value);
    }
  }

  public override void OnApplyTemplate()
  {
    if (this.DeferredContent == null)
      return;
    this.Content = (object) this.DeferredContent.LoadContent();
  }

  public static PointMarker \u0023\u003DzBv1vB\u0024LEKSF4(
    ControlTemplate _param0,
    object _param1)
  {
    PointMarker znetutjmmvvasEjd1 = (PointMarker) null;
    if (_param0 != null)
    {
      PointMarker znetutjmmvvasEjd2 = new PointMarker();
      znetutjmmvvasEjd2.Template = _param0;
      znetutjmmvvasEjd1 = znetutjmmvvasEjd2;
      if (_param1 != null)
        znetutjmmvvasEjd1.DataContext = _param1;
      znetutjmmvvasEjd1.ApplyTemplate();
    }
    return znetutjmmvvasEjd1;
  }

  private static void \u0023\u003Dzw52y5BiydelX(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is PointMarker znetutjmmvvasEjd) || znetutjmmvvasEjd.DeferredContent == null || !(znetutjmmvvasEjd.DeferredContent.LoadContent() is FrameworkElement frameworkElement))
      return;
    Binding binding = new Binding("DataContext")
    {
      Source = (object) znetutjmmvvasEjd
    };
    frameworkElement.SetBinding(FrameworkElement.DataContextProperty, (BindingBase) binding);
    znetutjmmvvasEjd.Content = (object) frameworkElement;
  }
}
