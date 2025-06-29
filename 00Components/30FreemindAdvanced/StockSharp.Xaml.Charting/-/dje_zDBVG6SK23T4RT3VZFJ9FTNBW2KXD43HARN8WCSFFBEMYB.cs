// Decompiled with JetBrains decompiler
// Type: -.dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable enable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd : 
  \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003DzxJ9WluMHlZpH = DependencyProperty.Register(XXX.SSS(-539429234), typeof (dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd), typeof (dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd), new PropertyMetadata((object) dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.ClipAtExtents));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Dictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> \u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzhcsKvLfw_p5c;

  public dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd()
  {
    this.\u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(false);
  }

  public dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd ClipModeX
  {
    get
    {
      return (dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd) this.GetValue(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003DzxJ9WluMHlZpH);
    }
    set
    {
      this.SetValue(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003DzxJ9WluMHlZpH, (object) value);
    }
  }

  protected override \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzFLmJq0JJlr0n()
  {
    return this.\u0023\u003DzI0EiGDjWkH8S(this.AxisId);
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    this.\u0023\u003DzhcsKvLfw_p5c = _param1.\u0023\u003DztkyOk5amPcz3();
    this.\u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D = this.XAxes.Where<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D ?? (dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D = new Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool>(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzxsT5eH8ST5wURnPDRSExaUI\u003D))).ToDictionary<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyrbJPDSEp4QjajjhiA\u003D\u003D ?? (dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyrbJPDSEp4QjajjhiA\u003D\u003D = new Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, string>(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz42dJuYUoRwO2twIy\u0024zPWSEE\u003D)), dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzBXy6HataguDyrzDt9g\u003D\u003D ?? (dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzBXy6HataguDyrzDt9g\u003D\u003D = new Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>(dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzDdeB7QUmKNmVJObfmPxXrUc\u003D)));
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    this.\u0023\u003DzhcsKvLfw_p5c = new Point();
    this.\u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D = new Dictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>();
  }

  protected override void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, Point _param2)
  {
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = this.\u0023\u003DzFLmJq0JJlr0n();
    double num = this.\u0023\u003DzSq_tAg1m2DDh(dynWmoFzgH4RlWB0lB, _param1, _param2);
    dynWmoFzgH4RlWB0lB.\u0023\u003DzquLnA5Y\u003D(num, this.ClipModeX);
  }

  private double \u0023\u003DzSq_tAg1m2DDh(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    Point _param2,
    Point _param3)
  {
    double num1 = _param2.X - _param3.X;
    double num2 = _param3.Y - _param2.Y;
    if (_param1.get_IsCategoryAxis())
    {
      _param1.VisibleRange = this.\u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D[_param1.get_Id()];
      num1 = _param2.X - this.\u0023\u003DzhcsKvLfw_p5c.X;
      num2 = this.\u0023\u003DzhcsKvLfw_p5c.Y - _param2.Y;
    }
    return !_param1.IsHorizontalAxis ? -num2 : num1;
  }

  protected override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzFQz4aIsJtfEk(
    Point _param1,
    Point _param2,
    bool _param3,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param4)
  {
    \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = _param4.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
    double num = this.\u0023\u003DzSq_tAg1m2DDh(_param4, _param1, _param2);
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = _param3 ? txZaHyXliZ9wXjzC.\u0023\u003DzMKO149dmZRdJ(_param4.VisibleRange, num) : txZaHyXliZ9wXjzC.\u0023\u003Dz5asXHk1unXCX(_param4.VisibleRange, num);
    if (_param4.get_AutoRange() != dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always)
      abyLt9clZggmJsWhw = this.\u0023\u003DzoaHKvRB3HZP3(abyLt9clZggmJsWhw, num, _param3, _param4);
    return abyLt9clZggmJsWhw;
  }

  private \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzoaHKvRB3HZP3(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1,
    double _param2,
    bool _param3,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param4)
  {
    if (this.ClipModeX != dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None)
    {
      \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = _param4.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
      \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw1 = _param4.\u0023\u003DzFwoMKP9juTnt();
      \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw2 = ((\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) _param1.Clone()).\u0023\u003DzJIqIiUw\u003D(abyLt9clZggmJsWhw1);
      bool flag1 = abyLt9clZggmJsWhw2.Min.CompareTo((object) _param1.Min) != 0;
      bool flag2 = abyLt9clZggmJsWhw2.Max.CompareTo((object) _param1.Max) != 0;
      if (_param3)
      {
        if (flag2)
        {
          if (this.ClipModeX != dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.ClipAtMin)
            _param1 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param4.VisibleRange, _param1.Min, abyLt9clZggmJsWhw2.Max);
          if (this.ClipModeX == dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.StretchAtExtents)
            _param1 = txZaHyXliZ9wXjzC.\u0023\u003Dz5asXHk1unXCX(_param4.VisibleRange, _param2);
        }
      }
      else if (flag1)
      {
        if (this.ClipModeX != dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.ClipAtMax)
          _param1 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param4.VisibleRange, abyLt9clZggmJsWhw2.Min, _param1.Max);
        if (this.ClipModeX == dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.StretchAtExtents)
          _param1 = txZaHyXliZ9wXjzC.\u0023\u003DzMKO149dmZRdJ(_param4.VisibleRange, _param2);
      }
    }
    return _param1;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zDBVG6SK23T4RT3VZFJ9FTNBW2KXD43HARN8WCSFFBEMYBNM9U2CJD_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool> \u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D;
    public static Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, 
    #nullable enable
    string> \u0023\u003DzyrbJPDSEp4QjajjhiA\u003D\u003D;
    public static 
    #nullable disable
    Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> \u0023\u003DzBXy6HataguDyrzDt9g\u003D\u003D;

    internal bool \u0023\u003DzxsT5eH8ST5wURnPDRSExaUI\u003D(
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      return _param1.get_IsCategoryAxis();
    }

    internal 
    #nullable enable
    string \u0023\u003Dz42dJuYUoRwO2twIy\u0024zPWSEE\u003D(
      #nullable disable
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      return _param1.get_Id();
    }

    internal \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzDdeB7QUmKNmVJObfmPxXrUc\u003D(
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      return _param1.VisibleRange;
    }
  }
}
