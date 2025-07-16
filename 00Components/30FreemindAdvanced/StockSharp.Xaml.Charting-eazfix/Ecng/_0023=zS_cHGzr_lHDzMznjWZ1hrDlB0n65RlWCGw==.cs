// Decompiled with JetBrains decompiler
// Type: #=zS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
public static class \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D
{
  public static IRange \u0023\u003Dzo7udA0u6sNJJ(
    IRange _param0,
    IComparable _param1,
    IComparable _param2)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D(_param1, "min").\u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D(_param2, "max");
    IRange abyLt9clZggmJsWhw = (IRange) _param0.Clone();
    abyLt9clZggmJsWhw.SetMinMax(_param1.ToDouble(), _param2.ToDouble());
    return abyLt9clZggmJsWhw;
  }

  public static IRange \u0023\u003Dzo7udA0u6sNJJ(
    IRange _param0,
    double _param1,
    double _param2,
    IRange _param3)
  {
    IRange abyLt9clZggmJsWhw = (IRange) _param0.Clone();
    abyLt9clZggmJsWhw.\u0023\u003DzsutwFhFKqYRf34G8vw\u003D\u003D(_param1, _param2, _param3);
    return abyLt9clZggmJsWhw;
  }

  public static IRange \u0023\u003DzLc65\u0024pc\u003D(
    IComparable _param0,
    IComparable _param1)
  {
    Type type = _param0.GetType();
    return !(type == typeof (float)) ? (!(type == typeof (int)) ? (!(type == typeof (long)) ? (!(type == typeof (DateTime)) ? (!(type == typeof (TimeSpan)) ? (IRange) new DoubleRange(_param0.ToDouble(), _param1.ToDouble()) : (IRange) new \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D((TimeSpan) _param0, (TimeSpan) _param1)) : (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D((DateTime) _param0, (DateTime) _param1)) : (IRange) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D((long) _param0, (long) _param1)) : (IRange) new \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D((int) _param0, (int) _param1)) : (IRange) new \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw_VCdMZ55IvngQ\u003D\u003D((float) _param0, (float) _param1);
  }

  public static IRange \u0023\u003DzLc65\u0024pc\u003D(
    Type _param0,
    IComparable _param1,
    IComparable _param2)
  {
    IRange instance = Activator.CreateInstance(_param0) as IRange;
    instance.SetMinMax(_param1.ToDouble(), _param2.ToDouble());
    return instance;
  }
}
