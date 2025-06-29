// Decompiled with JetBrains decompiler
// Type: #=zPm$a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D : 
  \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<TimeSpan>,
  \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqGwqd9HRLNiqw6g\u003D\u003D,
  ICloneable,
  IEquatable<\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D>
{
  
  private TimeSpan \u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D;
  
  private TimeSpan \u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D;

  public \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D()
  {
  }

  public \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(
    TimeSpan _param1,
    TimeSpan _param2)
  {
    this.\u0023\u003DznbvDc7H\u0024gWhJ(_param1);
    this.\u0023\u003DzLBsbhOSbi0NY(_param2);
  }

  IComparable \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqGwqd9HRLNiqw6g\u003D\u003D.\u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u002479gayIqWmvKX\u0024h1p6Zc_qj8()
  {
    return (IComparable) this.\u0023\u003Dzgq30Jn5PclK8();
  }

  IComparable \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqGwqd9HRLNiqw6g\u003D\u003D.\u0023\u003DzPauio66DvxKtWOFEEHOV9Z1zYQEc32ee4NKE7EIwudWm()
  {
    return (IComparable) this.\u0023\u003DzZ85DqsktXJL3();
  }

  [CompilerGenerated]
  [SpecialName]
  public TimeSpan \u0023\u003Dzgq30Jn5PclK8()
  {
    return this.\u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzLBsbhOSbi0NY(TimeSpan _param1)
  {
    this.\u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public TimeSpan \u0023\u003DzZ85DqsktXJL3() => this.\u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D;

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DznbvDc7H\u0024gWhJ(TimeSpan _param1)
  {
    this.\u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D = _param1;
  }

  public object Clone()
  {
    return (object) new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(this.\u0023\u003DzZ85DqsktXJL3(), this.\u0023\u003Dzgq30Jn5PclK8());
  }

  public override bool Equals(object _param1)
  {
    return this.Equals(_param1 as \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D);
  }

  public bool Equals(
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D _param1)
  {
    if ((object) _param1 == null)
      return false;
    if ((object) this == (object) _param1)
      return true;
    TimeSpan timeSpan = _param1.\u0023\u003Dzgq30Jn5PclK8();
    if (!timeSpan.Equals(this.\u0023\u003Dzgq30Jn5PclK8()))
      return false;
    timeSpan = _param1.\u0023\u003DzZ85DqsktXJL3();
    return timeSpan.Equals(this.\u0023\u003DzZ85DqsktXJL3());
  }

  public override int GetHashCode()
  {
    TimeSpan timeSpan = this.\u0023\u003Dzgq30Jn5PclK8();
    int num = timeSpan.GetHashCode() * 397;
    timeSpan = this.\u0023\u003DzZ85DqsktXJL3();
    int hashCode = timeSpan.GetHashCode();
    return num ^ hashCode;
  }

  public static bool operator ==(
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D _param0,
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D _param1)
  {
    return object.Equals((object) _param0, (object) _param1);
  }

  public static bool operator !=(
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D _param0,
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D _param1)
  {
    return !object.Equals((object) _param0, (object) _param1);
  }

  public override string ToString()
  {
    return string.Format(XXX.SSS(-539329255), (object) this.\u0023\u003DzZ85DqsktXJL3(), (object) this.\u0023\u003Dzgq30Jn5PclK8());
  }
}
