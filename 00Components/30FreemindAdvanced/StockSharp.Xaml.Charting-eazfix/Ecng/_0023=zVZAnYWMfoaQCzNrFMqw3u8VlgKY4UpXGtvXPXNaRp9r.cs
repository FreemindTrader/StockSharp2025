// Decompiled with JetBrains decompiler
// Type: #=zVZAnYWMfoaQCzNrFMqw3u8VlgKY4UpXGtvXPXNaRp9rB
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u8VlgKY4UpXGtvXPXNaRp9rB : 
  \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_kZtb7YQc\u0024kEo5r\u0024cPBp2g91_YdMug\u003D\u003D
{
  protected override double \u0023\u003Dz3spHJE8\u003D(IComparable _param1)
  {
    return (double) _param1.\u0023\u003Dzto51K8pl8UAh().Ticks;
  }

  protected override IComparable RoundUp(IComparable _param1, TimeSpan _param2)
  {
    return (IComparable) new TimeSpan((long) NumberUtil.RoundUp(this.\u0023\u003Dz3spHJE8\u003D(_param1), (double) _param2.Ticks));
  }

  protected override bool \u0023\u003Dzl5VrLhRrr5CB(IComparable _param1, TimeSpan _param2)
  {
    return _param1.\u0023\u003Dzto51K8pl8UAh().\u0023\u003Dzl5VrLhRrr5CB(_param2);
  }

  protected override IComparable \u0023\u003Dz8ly0q7w\u003D(IComparable _param1, TimeSpan _param2)
  {
    return (IComparable) (_param1.\u0023\u003Dzto51K8pl8UAh() + _param2);
  }

  protected override bool IsDivisibleBy(IComparable _param1, TimeSpan _param2)
  {
    return _param1.\u0023\u003Dzto51K8pl8UAh().IsDivisibleBy(_param2);
  }
}
