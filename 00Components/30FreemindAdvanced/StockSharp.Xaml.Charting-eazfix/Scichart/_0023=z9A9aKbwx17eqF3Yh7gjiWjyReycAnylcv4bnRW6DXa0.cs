// Decompiled with JetBrains decompiler
// Type: #=z9A9aKbwx17eqF3Yh7gjiWjyReycAnylcv4bnRW6DXa0wZWo1GN8_OWPxtqME
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWjyReycAnylcv4bnRW6DXa0wZWo1GN8_OWPxtqME : 
  \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c
{
  private readonly \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA;

  public \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWjyReycAnylcv4bnRW6DXa0wZWo1GN8_OWPxtqME(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> _param1)
  {
    this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA = _param1;
  }

  public IRange \u0023\u003DzQdR08KQ\u003D(
    IRange _param1,
    double _param2,
    double _param3)
  {
    double num1 = this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA.GetDataValue(_param2);
    double num2 = this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA.GetDataValue(_param3);
    if (num1 >= num2)
      NumberUtil.Swap(ref num1, ref num2);
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param1, (IComparable) num1, (IComparable) num2);
  }

  public IRange \u0023\u003Dz40HnRQM\u003D(
    IRange _param1,
    double _param2,
    double _param3)
  {
    DoubleRange klqcJ87Zm8UwE3WEjd = this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA.\u0023\u003Dznj_TkFQ\u003D(_param2, _param3, _param1);
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param1, (IComparable) klqcJ87Zm8UwE3WEjd.Min, (IComparable) klqcJ87Zm8UwE3WEjd.Max);
  }

  public IRange \u0023\u003Dz5asXHk1unXCX(
    IRange _param1,
    double _param2)
  {
    IRange abyLt9clZggmJsWhw = this.\u0023\u003DzquLnA5Y\u003D(_param1, _param2);
    return this.\u0023\u003DzmOhokOsOspCU0xk0y9Zn7Os\u003D(_param1, abyLt9clZggmJsWhw.Min, _param1.Max);
  }

  private IRange \u0023\u003DzmOhokOsOspCU0xk0y9Zn7Os\u003D(
    IRange _param1,
    IComparable _param2,
    IComparable _param3)
  {
    IRange abyLt9clZggmJsWhw = _param1;
    if (_param2.CompareTo((object) _param3) < 0)
      abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param1, _param2, _param3);
    return abyLt9clZggmJsWhw;
  }

  public IRange \u0023\u003DzMKO149dmZRdJ(
    IRange _param1,
    double _param2)
  {
    IRange abyLt9clZggmJsWhw = this.\u0023\u003DzquLnA5Y\u003D(_param1, _param2);
    return this.\u0023\u003DzmOhokOsOspCU0xk0y9Zn7Os\u003D(_param1, _param1.Min, abyLt9clZggmJsWhw.Max);
  }

  public IRange \u0023\u003DzquLnA5Y\u003D(
    IRange _param1,
    double _param2)
  {
    DoubleRange klqcJ87Zm8UwE3WEjd = this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA.\u0023\u003Dznj_TkFQ\u003D(_param2, _param1.AsDoubleRange());
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param1, (IComparable) klqcJ87Zm8UwE3WEjd.Min, (IComparable) klqcJ87Zm8UwE3WEjd.Max);
  }

  [Obsolete("The ScrollBy method is Obsolete as it is only really possible to implement on Category Axis. For this axis type just update the IndexRange (visibleRange) by N to scroll the axis", true)]
  public IRange \u0023\u003Dz7dXLkHs\u003D(
    IRange _param1,
    int _param2)
  {
    throw new NotImplementedException();
  }

  public IRange \u0023\u003DzoaHKvRB3HZP3(
    IRange _param1,
    IRange _param2,
    ClipMode _param3)
  {
    IRange abyLt9clZggmJsWhw1 = _param1;
    if (_param3 != ClipMode.None)
    {
      IRange abyLt9clZggmJsWhw2 = ((IRange) abyLt9clZggmJsWhw1.Clone()).\u0023\u003DzJIqIiUw\u003D(_param2);
      bool flag1 = abyLt9clZggmJsWhw2.Min.CompareTo((object) abyLt9clZggmJsWhw1.Min) != 0;
      bool flag2 = abyLt9clZggmJsWhw2.Max.CompareTo((object) abyLt9clZggmJsWhw1.Max) != 0;
      DoubleRange klqcJ87Zm8UwE3WEjd1 = _param1.AsDoubleRange();
      DoubleRange klqcJ87Zm8UwE3WEjd2 = abyLt9clZggmJsWhw2.AsDoubleRange();
      double num1 = flag2 ? klqcJ87Zm8UwE3WEjd1.Max - klqcJ87Zm8UwE3WEjd2.Max : klqcJ87Zm8UwE3WEjd1.Min - klqcJ87Zm8UwE3WEjd2.Min;
      double num2 = klqcJ87Zm8UwE3WEjd1.Min - num1;
      double num3 = num2 + klqcJ87Zm8UwE3WEjd1.Diff;
      if (this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA.\u0023\u003Dzvv0hxm6toXaEx\u0024Ig9\u0024tNoOTFJF\u0024GzZcDvmTfkJY\u003D())
      {
        \u0023\u003Dz03BSxVLolBnG92GmtCJpdiLKTtCzjuQ0xoe6ulFF1YojE7MNYqd4tFZ\u0024k2xs1tMz\u0024adc_4QoZXJGrlg9Jw\u003D\u003D lhkPyJqwmLp2gNva = (\u0023\u003Dz03BSxVLolBnG92GmtCJpdiLKTtCzjuQ0xoe6ulFF1YojE7MNYqd4tFZ\u0024k2xs1tMz\u0024adc_4QoZXJGrlg9Jw\u003D\u003D) this.\u0023\u003DzDwftsBnB60LHk\u0024_PYJqwmLP2gNVA;
        double num4 = Math.Log(klqcJ87Zm8UwE3WEjd1.Min, lhkPyJqwmLp2gNva.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D());
        double num5 = Math.Log(klqcJ87Zm8UwE3WEjd1.Max, lhkPyJqwmLp2gNva.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D());
        double num6 = flag2 ? num5 - Math.Log(klqcJ87Zm8UwE3WEjd2.Max, lhkPyJqwmLp2gNva.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D()) : num4 - Math.Log(klqcJ87Zm8UwE3WEjd2.Min, lhkPyJqwmLp2gNva.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D());
        num2 = Math.Pow(lhkPyJqwmLp2gNva.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), num4 - num6);
        double num7 = num5 - num4;
        num3 = Math.Pow(lhkPyJqwmLp2gNva.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), num4 - num6 + num7);
      }
      switch (_param3)
      {
        case ClipMode.StretchAtExtents:
          abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw2;
          break;
        case ClipMode.ClipAtMin:
          abyLt9clZggmJsWhw1 = flag1 ? \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param2, num2, num3, _param2) : \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param2, abyLt9clZggmJsWhw2.Min, abyLt9clZggmJsWhw1.Max);
          break;
        case ClipMode.ClipAtExtents:
          if (flag1 & flag2)
          {
            abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw2;
            break;
          }
          if (flag1 | flag2)
          {
            abyLt9clZggmJsWhw1 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param2, num2, num3, _param2);
            break;
          }
          break;
      }
    }
    if (abyLt9clZggmJsWhw1.Min.CompareTo((object) abyLt9clZggmJsWhw1.Max) > 0)
      abyLt9clZggmJsWhw1 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(abyLt9clZggmJsWhw1.Max, abyLt9clZggmJsWhw1.Min);
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D(abyLt9clZggmJsWhw1.Min, "min").\u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D(abyLt9clZggmJsWhw1.Max, "max");
    return abyLt9clZggmJsWhw1;
  }
}
