// Decompiled with JetBrains decompiler
// Type: -.dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd : 
  dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzTwmsLrVpwa38 = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539339955), typeof (DataTemplate), typeof (dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd.\u0023\u003Dzw52y5BiydelX)));

  public DataTemplate DeferredContent
  {
    get
    {
      return (DataTemplate) this.GetValue(dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd.\u0023\u003DzTwmsLrVpwa38);
    }
    set
    {
      this.SetValue(dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd.\u0023\u003DzTwmsLrVpwa38, (object) value);
    }
  }

  public override void OnApplyTemplate()
  {
    if (this.DeferredContent == null)
      return;
    this.Content = (object) this.DeferredContent.LoadContent();
  }

  public static dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd \u0023\u003DzBv1vB\u0024LEKSF4(
    ControlTemplate _param0,
    object _param1)
  {
    dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd znetutjmmvvasEjd1 = (dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd) null;
    if (_param0 != null)
    {
      dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd znetutjmmvvasEjd2 = new dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd();
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
    if (!(_param0 is dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd znetutjmmvvasEjd) || znetutjmmvvasEjd.DeferredContent == null || !(znetutjmmvvasEjd.DeferredContent.LoadContent() is FrameworkElement frameworkElement))
      return;
    Binding binding = new Binding(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539339941))
    {
      Source = (object) znetutjmmvvasEjd
    };
    frameworkElement.SetBinding(FrameworkElement.DataContextProperty, (BindingBase) binding);
    znetutjmmvvasEjd.Content = (object) frameworkElement;
  }
}
