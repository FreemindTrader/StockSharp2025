// Decompiled with JetBrains decompiler
// Type: #=zJhc8WdlQgSkcniY$669aniHe9rfoFyUgrbTADSj0lBiy
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzJhc8WdlQgSkcniY\u0024669aniHe9rfoFyUgrbTADSj0lBiy : 
  \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_kZtb7YQc\u0024kEo5r\u0024cPBp2g91_YdMug\u003D\u003D
{
  protected override double \u0023\u003Dz3spHJE8\u003D(IComparable _param1)
  {
    return (double) _param1.\u0023\u003Dzxuo5aY4wjkaI().Ticks;
  }

  protected override IComparable RoundUp(IComparable _param1, TimeSpan _param2)
  {
    return (IComparable) DateUtil.RoundUp(_param1.\u0023\u003Dzxuo5aY4wjkaI(), _param2);
  }

  protected override bool \u0023\u003Dzl5VrLhRrr5CB(IComparable _param1, TimeSpan _param2)
  {
    return _param1.\u0023\u003Dzxuo5aY4wjkaI().\u0023\u003Dzl5VrLhRrr5CB(_param2);
  }

  protected override IComparable \u0023\u003Dz8ly0q7w\u003D(IComparable _param1, TimeSpan _param2)
  {
    return (IComparable) _param1.\u0023\u003Dzxuo5aY4wjkaI().\u0023\u003Dz8ly0q7w\u003D(_param2);
  }

  protected override bool IsDivisibleBy(IComparable _param1, TimeSpan _param2)
  {
    return DateUtil.IsDivisibleBy(_param1.\u0023\u003Dzxuo5aY4wjkaI(), _param2);
  }
}
