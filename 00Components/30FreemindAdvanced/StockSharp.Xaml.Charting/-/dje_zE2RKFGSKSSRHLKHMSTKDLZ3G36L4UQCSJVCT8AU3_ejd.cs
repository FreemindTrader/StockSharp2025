// Decompiled with JetBrains decompiler
// Type: -.ThemeManager
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal static class ThemeManager
{
  public static readonly DependencyProperty \u0023\u003DzOJJsEsQ\u003D = DependencyProperty.RegisterAttached(XXX.SSS(-539440425), typeof (string), typeof (ThemeManager), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ThemeManager.\u0023\u003Dzb3YxwRaSuvYM)));
  private static EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> \u0023\u003Dz8Fi7DbieAZ0u;
  private static readonly IDictionary<string, ResourceDictionary> \u0023\u003DzMBUtRf9zXbZf = (IDictionary<string, ResourceDictionary>) new Dictionary<string, ResourceDictionary>();
  private static \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D \u0023\u003DzObiFrX5IN\u0024C0;
  private static readonly Dictionary<string, \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D> \u0023\u003Dz782V6KE\u00242buI = new Dictionary<string, \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D>();
  public static IList<string> \u0023\u003DzPNNG5dU2KvYM = (IList<string>) new List<string>(7)
  {
    XXX.SSS(-539333680),
    XXX.SSS(-539333725),
    XXX.SSS(-539426954),
    XXX.SSS(-539333747),
    XXX.SSS(-539427003),
    XXX.SSS(-539333758),
    XXX.SSS(-539334040)
  };

  static ThemeManager()
  {
    ThemeManager.\u0023\u003DzwI1vePI9DZf8().\u0023\u003DzeHF6BUxLFClh(ThemeManager.\u0023\u003DzB0x3mHFPBNZB(XXX.SSS(-539427003)));
  }

  public static string GetTheme(DependencyObject _param0)
  {
    return (string) _param0.GetValue(ThemeManager.\u0023\u003DzOJJsEsQ\u003D);
  }

  public static void SetTheme(DependencyObject _param0, string _param1)
  {
    _param0.SetValue(ThemeManager.\u0023\u003DzOJJsEsQ\u003D, (object) _param1);
  }

  public static void \u0023\u003DzBkgEo7IVSEjs(
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> _param0)
  {
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> eventHandler1 = ThemeManager.\u0023\u003Dz8Fi7DbieAZ0u;
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> comparand;
    do
    {
      comparand = eventHandler1;
      EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> eventHandler2 = comparand + _param0;
      eventHandler1 = Interlocked.CompareExchange<EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D>>(ref ThemeManager.\u0023\u003Dz8Fi7DbieAZ0u, eventHandler2, comparand);
    }
    while (eventHandler1 != comparand);
  }

  public static void \u0023\u003DzmW2TnDsqzeZU(
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> _param0)
  {
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> eventHandler1 = ThemeManager.\u0023\u003Dz8Fi7DbieAZ0u;
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> comparand;
    do
    {
      comparand = eventHandler1;
      EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> eventHandler2 = comparand - _param0;
      eventHandler1 = Interlocked.CompareExchange<EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D>>(ref ThemeManager.\u0023\u003Dz8Fi7DbieAZ0u, eventHandler2, comparand);
    }
    while (eventHandler1 != comparand);
  }

  public static IList<string> \u0023\u003DzBG3JZN5PsfMx()
  {
    return ThemeManager.\u0023\u003DzPNNG5dU2KvYM;
  }

  public static \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D \u0023\u003DzwI1vePI9DZf8()
  {
    return ThemeManager.\u0023\u003DzObiFrX5IN\u0024C0 ?? (ThemeManager.\u0023\u003DzObiFrX5IN\u0024C0 = (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D) new \u0023\u003DzduViKcXTrKCfnYwdbArizvSbWeE5LHaB3CyMd\u0024w\u003D());
  }

  public static void \u0023\u003DzgrgNv7k\u003D(string _param0, ResourceDictionary _param1)
  {
    if (ThemeManager.\u0023\u003DzMBUtRf9zXbZf.ContainsKey(_param0))
      return;
    ThemeManager.\u0023\u003DzMBUtRf9zXbZf.Add(_param0, _param1);
    \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D gmt9VmM5IkNVtVwybkk = (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D) new \u0023\u003DzduViKcXTrKCfnYwdbArizvSbWeE5LHaB3CyMd\u0024w\u003D();
    gmt9VmM5IkNVtVwybkk.\u0023\u003DzeHF6BUxLFClh(_param1);
    ThemeManager.\u0023\u003Dz782V6KE\u00242buI.Add(_param0, gmt9VmM5IkNVtVwybkk);
    ThemeManager.\u0023\u003DzPNNG5dU2KvYM.Add(_param0);
  }

  public static void \u0023\u003DzRzbRF1o\u003D(string _param0)
  {
    if (ThemeManager.\u0023\u003DzMBUtRf9zXbZf.ContainsKey(_param0) || !ThemeManager.\u0023\u003Dz782V6KE\u00242buI.ContainsKey(_param0))
      return;
    ThemeManager.\u0023\u003DzMBUtRf9zXbZf.Remove(_param0);
    ThemeManager.\u0023\u003Dz782V6KE\u00242buI.Remove(_param0);
    ThemeManager.\u0023\u003DzPNNG5dU2KvYM.Remove(_param0);
  }

  public static \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D \u0023\u003DzrILtKW7bADnV(
    string _param0)
  {
    _param0 = string.IsNullOrEmpty(_param0) ? XXX.SSS(-539427003) : _param0;
    \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D gmt9VmM5IkNVtVwybkk;
    if (!ThemeManager.\u0023\u003Dz782V6KE\u00242buI.TryGetValue(_param0, out gmt9VmM5IkNVtVwybkk))
    {
      gmt9VmM5IkNVtVwybkk = (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D) new \u0023\u003DzduViKcXTrKCfnYwdbArizvSbWeE5LHaB3CyMd\u0024w\u003D();
      gmt9VmM5IkNVtVwybkk.\u0023\u003DzeHF6BUxLFClh(ThemeManager.\u0023\u003DzB0x3mHFPBNZB(_param0));
      ThemeManager.\u0023\u003Dz782V6KE\u00242buI.Add(_param0, gmt9VmM5IkNVtVwybkk);
    }
    return gmt9VmM5IkNVtVwybkk;
  }

  private static ResourceDictionary \u0023\u003DzB0x3mHFPBNZB(string _param0)
  {
    if (_param0 == null)
      return (ResourceDictionary) null;
    if (ThemeManager.\u0023\u003DzMBUtRf9zXbZf.ContainsKey(_param0))
      return ThemeManager.\u0023\u003DzMBUtRf9zXbZf[_param0];
    ResourceDictionary resourceDictionary = new ResourceDictionary()
    {
      Source = ThemeManager.\u0023\u003DzDT70eQaaSY2I(_param0)
    };
    ThemeManager.\u0023\u003DzMBUtRf9zXbZf.Add(_param0, resourceDictionary);
    return resourceDictionary;
  }

  private static Uri \u0023\u003DzDT70eQaaSY2I(string _param0)
  {
    return _param0.ToUpper(CultureInfo.InvariantCulture).Contains(XXX.SSS(-539334027)) ? new Uri(_param0, UriKind.Relative) : new Uri(string.Format(XXX.SSS(-539334073), (object) typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd).Assembly.\u0023\u003DzFARAiudukAjJ(), (object) _param0), UriKind.Relative);
  }

  private static void \u0023\u003DzeHF6BUxLFClh(this FrameworkElement _param0, string _param1)
  {
    if (string.IsNullOrEmpty(_param1))
      return;
    ResourceDictionary resourceDictionary = ThemeManager.\u0023\u003DzB0x3mHFPBNZB(_param1);
    ThemeManager.\u0023\u003DzwI1vePI9DZf8().\u0023\u003DzeHF6BUxLFClh(resourceDictionary);
    ThemeManager.\u0023\u003DzfZvoKk3Lyd9U(new \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D(_param0, _param1));
  }

  private static void \u0023\u003DzfZvoKk3Lyd9U(
    \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D _param0)
  {
    EventHandler<\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSZpwXjaXIqrTNeD8QxE\u003D> z8Fi7DbieAz0u = ThemeManager.\u0023\u003Dz8Fi7DbieAZ0u;
    if (z8Fi7DbieAz0u == null)
      return;
    z8Fi7DbieAz0u((object) null, _param0);
  }

  private static void \u0023\u003Dzb3YxwRaSuvYM(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    string str = _param1.NewValue as string;
    string oldValue = _param1.OldValue as string;
    FrameworkElement frameworkElement = _param0 as FrameworkElement;
    if (string.IsNullOrEmpty(str) || frameworkElement == null)
      return;
    if (str.ToUpper(CultureInfo.InvariantCulture) == XXX.SSS(-539334110))
    {
      Random random = new Random();
      str = ThemeManager.\u0023\u003DzBG3JZN5PsfMx().ElementAt<string>(random.Next(ThemeManager.\u0023\u003DzBG3JZN5PsfMx().Count<string>()));
    }
    if (!(oldValue != str & DependencyPropertyHelper.GetValueSource((DependencyObject) frameworkElement, ThemeManager.\u0023\u003DzOJJsEsQ\u003D).BaseValueSource != BaseValueSource.Inherited))
      return;
    frameworkElement.\u0023\u003DzeHF6BUxLFClh(str);
    if (!(frameworkElement is \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D ks0apzs3KdsLxgXg))
      return;
    ks0apzs3KdsLxgXg.\u0023\u003Dz5q8i9C4\u003D();
  }
}
