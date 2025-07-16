// Decompiled with JetBrains decompiler
// Type: #=zDIMjRgQEnqoXVDL$LFZbb9dRGvB_L4PhR$MA7GxfHzLZh5MbP3ARzok=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public abstract class \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb9dRGvB_L4PhR\u0024MA7GxfHzLZh5MbP3ARzok\u003D : 
  \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D,
  \u0023\u003DzQN2Zes8h9tElvYmX48o49BUYcGhHuBjHtpu_APLaHrAgLQA2lg\u003D\u003D
{
  private readonly IList<TimeSpan> \u0023\u003Dz3yhjwpbyTKgX = (IList<TimeSpan>) new TimeSpan[24]
  {
    TimeSpan.FromSeconds(1.0),
    TimeSpan.FromSeconds(2.0),
    TimeSpan.FromSeconds(5.0),
    TimeSpan.FromSeconds(10.0),
    TimeSpan.FromSeconds(30.0),
    TimeSpan.FromMinutes(1.0),
    TimeSpan.FromMinutes(2.0),
    TimeSpan.FromMinutes(5.0),
    TimeSpan.FromMinutes(10.0),
    TimeSpan.FromMinutes(30.0),
    TimeSpan.FromHours(1.0),
    TimeSpan.FromHours(2.0),
    TimeSpan.FromHours(4.0),
    TimeSpan.FromDays(1.0),
    TimeSpan.FromDays(2.0),
    TimeSpan.FromDays(7.0),
    TimeSpan.FromDays(14.0),
    TimeSpanExtensions.FromMonths(1),
    TimeSpanExtensions.FromMonths(3),
    TimeSpanExtensions.FromMonths(6),
    TimeSpanExtensions.FromYears(1),
    TimeSpanExtensions.FromYears(2),
    TimeSpanExtensions.FromYears(5),
    TimeSpanExtensions.FromYears(10)
  };

  protected abstract long \u0023\u003Dz3spHJE8\u003D(IComparable _param1);

  public \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D \u0023\u003DzyE145DTzxI8R(
    IComparable _param1,
    IComparable _param2,
    int _param3,
    uint _param4 = 8)
  {
    return this.\u0023\u003DzzYtozYR\u0024Y2EkbVq5yQ\u003D\u003D(this.\u0023\u003Dz3spHJE8\u003D(_param1), this.\u0023\u003Dz3spHJE8\u003D(_param2), _param3, _param4);
  }

  private \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D \u0023\u003DzzYtozYR\u0024Y2EkbVq5yQ\u003D\u003D(
    long _param1,
    long _param2,
    int _param3,
    uint _param4)
  {
    \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb9dRGvB_L4PhR\u0024MA7GxfHzLZh5MbP3ARzok\u003D.SomeWheireosoe vbxLeArTkallkIdHg = new \u0023\u003DzDIMjRgQEnqoXVDL\u0024LFZbb9dRGvB_L4PhR\u0024MA7GxfHzLZh5MbP3ARzok\u003D.SomeWheireosoe();
    vbxLeArTkallkIdHg.\u0023\u003DzCPSuI41d8esj = _param4;
    if (_param1 >= _param2)
      return new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(TimeSpan.Zero, TimeSpan.Zero);
    long ticks = _param2 - _param1;
    vbxLeArTkallkIdHg.\u0023\u003DzFYPaJVU_4gr3 = new TimeSpan(ticks);
    TimeSpan timeSpan = this.\u0023\u003Dz3yhjwpbyTKgX.FirstOrDefault<TimeSpan>(new Func<TimeSpan, bool>(vbxLeArTkallkIdHg.\u0023\u003Dz9sc_qdKdGUGYrwVgpcN5kd0\u003D));
    return !timeSpan.Equals(TimeSpan.Zero) ? this.\u0023\u003Dzqk5b7b_b3MbE8Wpd\u0024w\u003D\u003D(timeSpan, ticks, _param3, vbxLeArTkallkIdHg.\u0023\u003DzCPSuI41d8esj) : this.\u0023\u003DzfX29VwAc2ujh(0L, ticks / TimeSpanExtensions.FromYears(1).Ticks, _param3, vbxLeArTkallkIdHg.\u0023\u003DzCPSuI41d8esj, true);
  }

  private \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D \u0023\u003DzfX29VwAc2ujh(
    long _param1,
    long _param2,
    int _param3,
    uint _param4,
    bool _param5)
  {
    Tuple<long, long> tuple = new \u0023\u003DzdnTE8UjAVGg52Oblqj3yg7gMG_aprhZVUUZ2rxA901RC(_param1, _param2, _param3, _param4).\u0023\u003DzAnYO0vLQEDGX();
    return new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(_param5 ? TimeSpanExtensions.FromYears((int) tuple.Item1) : TimeSpan.FromTicks(tuple.Item1), _param5 ? TimeSpanExtensions.FromYears((int) tuple.Item2) : TimeSpan.FromTicks(tuple.Item2));
  }

  private \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D \u0023\u003Dzqk5b7b_b3MbE8Wpd\u0024w\u003D\u003D(
    TimeSpan _param1,
    long _param2,
    int _param3,
    uint _param4)
  {
    int index = this.\u0023\u003Dz3yhjwpbyTKgX.IndexOf(_param1) - 2;
    return index < 0 ? this.\u0023\u003DzfX29VwAc2ujh(0L, _param2, _param3, _param4, false) : new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(this.\u0023\u003Dz3yhjwpbyTKgX[index], _param1);
  }

  \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqGwqd9HRLNiqw6g\u003D\u003D \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D.\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpWi7tVS8F7CE_ipbGe9sY7HXTn6f23EFrYQlHTyZ(
    IComparable _param1,
    IComparable _param2,
    int _param3,
    uint _param4)
  {
    return (\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqGwqd9HRLNiqw6g\u003D\u003D) this.\u0023\u003DzyE145DTzxI8R(_param1, _param2, _param3, _param4);
  }

  private sealed class SomeWheireosoe
  {
    public uint \u0023\u003DzCPSuI41d8esj;
    public TimeSpan \u0023\u003DzFYPaJVU_4gr3;

    public bool \u0023\u003Dz9sc_qdKdGUGYrwVgpcN5kd0\u003D(TimeSpan _param1)
    {
      return new TimeSpan(_param1.Ticks * (long) this.\u0023\u003DzCPSuI41d8esj) > this.\u0023\u003DzFYPaJVU_4gr3;
    }
  }
}
