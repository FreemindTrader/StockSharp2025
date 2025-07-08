// Decompiled with JetBrains decompiler
// Type: -.dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal class dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd : 
  Panel,
  ISuspendable
{
  
  public static readonly DependencyProperty \u0023\u003DzPqXZPEJZP3as = DependencyProperty.Register(nameof (ClipToBounds), typeof (bool), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003Dz2TPUHBqM4Vp3)));
  
  public static readonly DependencyProperty \u0023\u003Dzo60DLAgevG_S = DependencyProperty.Register(nameof (SizeWidthToContent), typeof (bool), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzp58eziJQD8M\u0024 = DependencyProperty.Register(nameof (SizeHeightToContent), typeof (bool), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DztX3bWaM\u003D = DependencyProperty.RegisterAttached("Left", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzQLHMxl4\u003D = DependencyProperty.RegisterAttached("Right", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzZpWLYz8\u003D = DependencyProperty.RegisterAttached("Top", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzasJeVgQ\u003D = DependencyProperty.RegisterAttached("Bottom", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzHEgPKfijwe68 = DependencyProperty.RegisterAttached("CenterLeft", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzJFNIkcI_P8j8 = DependencyProperty.RegisterAttached("CenterRight", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzLd8ENL0vP3HT = DependencyProperty.RegisterAttached("CenterTop", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzIetIZ2A\u00246GdH = DependencyProperty.RegisterAttached("CenterBottom", typeof (double), typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D)));

  public static double GetLeft(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DztX3bWaM\u003D);
  }

  public static void SetLeft(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DztX3bWaM\u003D, (object) _param1);
  }

  public static double GetRight(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzQLHMxl4\u003D);
  }

  public static void SetRight(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzQLHMxl4\u003D, (object) _param1);
  }

  public static double GetTop(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzZpWLYz8\u003D);
  }

  public static void SetTop(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzZpWLYz8\u003D, (object) _param1);
  }

  public static double GetBottom(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzasJeVgQ\u003D);
  }

  public static void SetBottom(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzasJeVgQ\u003D, (object) _param1);
  }

  public static double GetCenterLeft(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzHEgPKfijwe68);
  }

  public static void SetCenterLeft(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzHEgPKfijwe68, (object) _param1);
  }

  public static double GetCenterRight(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzJFNIkcI_P8j8);
  }

  public static void SetCenterRight(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzJFNIkcI_P8j8, (object) _param1);
  }

  public static double GetCenterTop(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzLd8ENL0vP3HT);
  }

  public static void SetCenterTop(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzLd8ENL0vP3HT, (object) _param1);
  }

  public static double GetCenterBottom(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzIetIZ2A\u00246GdH);
  }

  public static void SetCenterBottom(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzIetIZ2A\u00246GdH, (object) _param1);
  }

  public bool SizeWidthToContent
  {
    get
    {
      return (bool) this.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003Dzo60DLAgevG_S);
    }
    set
    {
      this.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003Dzo60DLAgevG_S, (object) value);
    }
  }

  public bool SizeHeightToContent
  {
    get
    {
      return (bool) this.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003Dzp58eziJQD8M\u0024);
    }
    set
    {
      this.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003Dzp58eziJQD8M\u0024, (object) value);
    }
  }

  public new bool ClipToBounds
  {
    get
    {
      return (bool) this.GetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzPqXZPEJZP3as);
    }
    set
    {
      this.SetValue(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.\u0023\u003DzPqXZPEJZP3as, (object) value);
    }
  }

  protected override Geometry GetLayoutClip(Size _param1)
  {
    return !this.ClipToBounds ? (Geometry) null : base.GetLayoutClip(_param1);
  }

  private void \u0023\u003DzEy\u0024V_bY\u003D()
  {
    if (this.IsSuspended)
      return;
    if (this.SizeHeightToContent || this.SizeWidthToContent)
      this.InvalidateMeasure();
    else
      this.InvalidateArrange();
  }

  protected override Size MeasureOverride(Size _param1)
  {
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomePrivateSealedClass u8Jor1ugvGeCvjho = new dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomePrivateSealedClass();
    Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
    foreach (UIElement child in this.Children)
      child.Measure(availableSize);
    u8Jor1ugvGeCvjho.\u0023\u003Dz5ns\u0024beLh7Lq6 = 0.0;
    u8Jor1ugvGeCvjho.\u0023\u003DzVJWwwbsqQzla = 0.0;
    if (this.SizeHeightToContent || this.SizeWidthToContent)
    {
      UIElement[] array = this.Children.OfType<UIElement>().ToArray<UIElement>();
      if (this.SizeWidthToContent && !((IEnumerable<UIElement>) array).\u0023\u003DzCCMM80zDpO6N<UIElement>())
      {
        u8Jor1ugvGeCvjho.\u0023\u003Dz5ns\u0024beLh7Lq6 = ((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz3y21bA0y_nYOE48ktA\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz3y21bA0y_nYOE48ktA\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzYKvrV0JWnmDlqO_CWglp58k\u003D))).Select<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzdDtC3OxSsoLrT5gJtA\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzdDtC3OxSsoLrT5gJtA\u003D\u003D = new Func<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzAVWvTGcAdkhBave7rMtp6yE\u003D))).Concat<double>(((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzG_4MyYM1jN1jtGvK4w\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzG_4MyYM1jN1jtGvK4w\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003Dz\u00245m21xs\u0024cvAmnIdf2gU_Xac\u003D))).Select<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz1zCHqtshG4A4cEZzvg\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz1zCHqtshG4A4cEZzvg\u003D\u003D = new Func<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzNqdo6wxMxabVyP0X1FEOxSA\u003D)))).\u0023\u003DzAAksTMXIKE7d<double>() ?? ((IEnumerable<UIElement>) array).Max<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzTdv0pOFb8ej3YepRJw\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzTdv0pOFb8ej3YepRJw\u003D\u003D = new Func<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzFbpefW3HjUWuukZ1ttNNd8E\u003D)));
        double valueOrDefault = ((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzgKa7Ws34VPBPBmckMDnLakc\u003D))).Select<UIElement, double>(new Func<UIElement, double>(u8Jor1ugvGeCvjho.\u0023\u003DzEkHlMyEAYNsEFyQhmw\u003D\u003D)).Concat<double>(((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzkPD384Oharu0EDaiwQ\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzkPD384Oharu0EDaiwQ\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzU5vCk358B5H2CkspeunyYO0\u003D))).Select<UIElement, double>(new Func<UIElement, double>(u8Jor1ugvGeCvjho.\u0023\u003Dz9C1di3PQnmzjSxkueA\u003D\u003D))).\u0023\u003Dz98VKYrYGa2Pn<double>().GetValueOrDefault();
        if (valueOrDefault < 0.0)
          u8Jor1ugvGeCvjho.\u0023\u003Dz5ns\u0024beLh7Lq6 += Math.Abs(valueOrDefault);
      }
      if (this.SizeHeightToContent && !((IEnumerable<UIElement>) array).\u0023\u003DzCCMM80zDpO6N<UIElement>())
      {
        u8Jor1ugvGeCvjho.\u0023\u003DzVJWwwbsqQzla = ((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz3xNuW1oowDK_S0sxqw\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz3xNuW1oowDK_S0sxqw\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzEK3\u0024DRApfKZBLp9SFni3nxM\u003D))).Select<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzmMpF_DkT5Fa3VjI7Xw\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzmMpF_DkT5Fa3VjI7Xw\u003D\u003D = new Func<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003Dz0bH_RR\u0024vBn9G_dk9IHgqhHs\u003D))).Concat<double>(((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz1zCHqtshG4BAMGIpPQ\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz1zCHqtshG4BAMGIpPQ\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzAKOrHq8MNBCJX496ulSnUwg\u003D))).Select<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzZRG6G4f9f35G3zleuQ\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzZRG6G4f9f35G3zleuQ\u003D\u003D = new Func<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzTUXHCNVzoXfaHk4oX\u0024dTb_s\u003D)))).\u0023\u003DzAAksTMXIKE7d<double>() ?? ((IEnumerable<UIElement>) array).Max<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzFCLN1xhDY4N0DJ45Xg\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzFCLN1xhDY4N0DJ45Xg\u003D\u003D = new Func<UIElement, double>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzeWUo84FcnBqVep84N3UETx8\u003D)));
        double valueOrDefault = ((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz_mJ1o44751\u0024zYSNI1g\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003Dz_mJ1o44751\u0024zYSNI1g\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzMfPK\u0024Mkqkqf_PHvwIZvXJt0\u003D))).Select<UIElement, double>(new Func<UIElement, double>(u8Jor1ugvGeCvjho.\u0023\u003DzIWa4EBPGhhWapsUzDA\u003D\u003D)).Concat<double>(((IEnumerable<UIElement>) array).Where<UIElement>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzDTNH\u0024s4BMtuReELE\u0024g\u003D\u003D ?? (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.\u0023\u003DzDTNH\u0024s4BMtuReELE\u0024g\u003D\u003D = new Func<UIElement, bool>(dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003Dz3c2h9eD2aQNit16ssTlcYSc\u003D))).Select<UIElement, double>(new Func<UIElement, double>(u8Jor1ugvGeCvjho.\u0023\u003Dz5gqPXwewFyEp00w4Kg\u003D\u003D))).\u0023\u003Dz98VKYrYGa2Pn<double>().GetValueOrDefault();
        if (valueOrDefault < 0.0)
          u8Jor1ugvGeCvjho.\u0023\u003DzVJWwwbsqQzla += Math.Abs(valueOrDefault);
      }
    }
    availableSize = new Size(Math.Max(u8Jor1ugvGeCvjho.\u0023\u003Dz5ns\u0024beLh7Lq6, 0.0), Math.Max(u8Jor1ugvGeCvjho.\u0023\u003DzVJWwwbsqQzla, 0.0));
    return availableSize;
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    foreach (UIElement child in this.Children)
    {
      Rect finalRect = this.\u0023\u003Dzzigj\u0024danccVcsgdXBQ\u003D\u003D(_param1, child);
      child.Arrange(finalRect);
    }
    return _param1;
  }

  protected virtual Rect \u0023\u003Dzzigj\u0024danccVcsgdXBQ\u003D\u003D(
    Size _param1,
    UIElement _param2)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    double left = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetLeft(_param2);
    double centerLeft = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterLeft(_param2);
    double num3 = _param2.DesiredSize.Width / 2.0;
    if (!left.IsNaN())
      num1 = left;
    else if (!centerLeft.IsNaN())
    {
      num1 = centerLeft - num3;
    }
    else
    {
      double right = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetRight(_param2);
      if (!right.IsNaN())
      {
        num1 = _param1.Width - _param2.DesiredSize.Width - right;
      }
      else
      {
        double centerRight = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterRight(_param2);
        if (!centerRight.IsNaN())
          num1 = _param1.Width - num3 - centerRight;
      }
    }
    double top = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetTop(_param2);
    double centerTop = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterTop(_param2);
    Size desiredSize = _param2.DesiredSize;
    double num4 = desiredSize.Height / 2.0;
    if (!top.IsNaN())
      num2 = top;
    else if (!centerTop.IsNaN())
    {
      num2 = centerTop - num4;
    }
    else
    {
      double bottom = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetBottom(_param2);
      if (!bottom.IsNaN())
      {
        double height1 = _param1.Height;
        desiredSize = _param2.DesiredSize;
        double height2 = desiredSize.Height;
        num2 = height1 - height2 - bottom;
      }
      else
      {
        double centerBottom = dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterBottom(_param2);
        if (!centerBottom.IsNaN())
          num2 = _param1.Height - num4 - centerBottom;
      }
    }
    return this.\u0023\u003Dz6P378kEteHu_g56Iv1HUje0\u003D(new Rect(new Point(num1, num2), _param2.DesiredSize), _param1);
  }

  protected virtual Rect \u0023\u003Dz6P378kEteHu_g56Iv1HUje0\u003D(Rect _param1, Size _param2)
  {
    return _param1;
  }

  public bool IsSuspended
  {
    get
    {
      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this);
    }
  }

  public IUpdateSuspender SuspendUpdates()
  {
    UpdateSuspender f5DxplZ7U6Rxl0An = new UpdateSuspender((ISuspendable) this);
    f5DxplZ7U6Rxl0An.ResumeTargetOnDispose=false;
    return (IUpdateSuspender) f5DxplZ7U6Rxl0An;
  }

  public void DecrementSuspend()
  {
  }

  public void ResumeUpdates(
    IUpdateSuspender _param1)
  {
    if (!_param1.ResumeTargetOnDispose)
      return;
    this.\u0023\u003DzEy\u0024V_bY\u003D();
  }

  private static void \u0023\u003Dz2TPUHBqM4Vp3(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
  }

  private static void \u0023\u003DzYiJHpLwOzite2sjmO4Xe9w0\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(((FrameworkElement) _param0).Parent is dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd parent))
      return;
    parent.\u0023\u003DzEy\u0024V_bY\u003D();
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383 SomeMethond0343 = new dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.SomeClass34343383();
    public static Func<UIElement, bool> \u0023\u003Dz3y21bA0y_nYOE48ktA\u003D\u003D;
    public static Func<UIElement, double> \u0023\u003DzdDtC3OxSsoLrT5gJtA\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003DzG_4MyYM1jN1jtGvK4w\u003D\u003D;
    public static Func<UIElement, double> \u0023\u003Dz1zCHqtshG4A4cEZzvg\u003D\u003D;
    public static Func<UIElement, double> \u0023\u003DzTdv0pOFb8ej3YepRJw\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003DzkPD384Oharu0EDaiwQ\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003Dz3xNuW1oowDK_S0sxqw\u003D\u003D;
    public static Func<UIElement, double> \u0023\u003DzmMpF_DkT5Fa3VjI7Xw\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003Dz1zCHqtshG4BAMGIpPQ\u003D\u003D;
    public static Func<UIElement, double> \u0023\u003DzZRG6G4f9f35G3zleuQ\u003D\u003D;
    public static Func<UIElement, double> \u0023\u003DzFCLN1xhDY4N0DJ45Xg\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003Dz_mJ1o44751\u0024zYSNI1g\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003DzDTNH\u0024s4BMtuReELE\u0024g\u003D\u003D;

    internal bool \u0023\u003DzYKvrV0JWnmDlqO_CWglp58k\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetLeft(_param1).IsNaN();
    }

    internal double \u0023\u003DzAVWvTGcAdkhBave7rMtp6yE\u003D(UIElement _param1)
    {
      return dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetLeft(_param1) + _param1.DesiredSize.Width;
    }

    internal bool \u0023\u003Dz\u00245m21xs\u0024cvAmnIdf2gU_Xac\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterLeft(_param1).IsNaN();
    }

    internal double \u0023\u003DzNqdo6wxMxabVyP0X1FEOxSA\u003D(UIElement _param1)
    {
      return dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterLeft(_param1) + _param1.DesiredSize.Width / 2.0;
    }

    internal double \u0023\u003DzFbpefW3HjUWuukZ1ttNNd8E\u003D(UIElement _param1)
    {
      return _param1.DesiredSize.Width;
    }

    internal bool \u0023\u003DzgKa7Ws34VPBPBmckMDnLakc\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetRight(_param1).IsNaN();
    }

    internal bool \u0023\u003DzU5vCk358B5H2CkspeunyYO0\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterRight(_param1).IsNaN();
    }

    internal bool \u0023\u003DzEK3\u0024DRApfKZBLp9SFni3nxM\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetTop(_param1).IsNaN();
    }

    internal double \u0023\u003Dz0bH_RR\u0024vBn9G_dk9IHgqhHs\u003D(UIElement _param1)
    {
      return dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetTop(_param1) + _param1.DesiredSize.Height;
    }

    internal bool \u0023\u003DzAKOrHq8MNBCJX496ulSnUwg\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterTop(_param1).IsNaN();
    }

    internal double \u0023\u003DzTUXHCNVzoXfaHk4oX\u0024dTb_s\u003D(UIElement _param1)
    {
      return dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterTop(_param1) + _param1.DesiredSize.Height / 2.0;
    }

    internal double \u0023\u003DzeWUo84FcnBqVep84N3UETx8\u003D(UIElement _param1)
    {
      return _param1.DesiredSize.Height;
    }

    internal bool \u0023\u003DzMfPK\u0024Mkqkqf_PHvwIZvXJt0\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetBottom(_param1).IsNaN();
    }

    internal bool \u0023\u003Dz3c2h9eD2aQNit16ssTlcYSc\u003D(UIElement _param1)
    {
      return !dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterBottom(_param1).IsNaN();
    }
  }

  private sealed class SomePrivateSealedClass
  {
    public double \u0023\u003Dz5ns\u0024beLh7Lq6;
    public double \u0023\u003DzVJWwwbsqQzla;

    internal double \u0023\u003DzEkHlMyEAYNsEFyQhmw\u003D\u003D(UIElement _param1)
    {
      return this.\u0023\u003Dz5ns\u0024beLh7Lq6 - dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetRight(_param1) - _param1.DesiredSize.Width;
    }

    internal double \u0023\u003Dz9C1di3PQnmzjSxkueA\u003D\u003D(UIElement _param1)
    {
      return this.\u0023\u003Dz5ns\u0024beLh7Lq6 - dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterRight(_param1) - _param1.DesiredSize.Width / 2.0;
    }

    internal double \u0023\u003DzIWa4EBPGhhWapsUzDA\u003D\u003D(UIElement _param1)
    {
      return this.\u0023\u003DzVJWwwbsqQzla - dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetBottom(_param1) - _param1.DesiredSize.Height;
    }

    internal double \u0023\u003Dz5gqPXwewFyEp00w4Kg\u003D\u003D(UIElement _param1)
    {
      return this.\u0023\u003DzVJWwwbsqQzla - dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd.GetCenterBottom(_param1) - _param1.DesiredSize.Height / 2.0;
    }
  }
}
