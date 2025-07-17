// Decompiled with JetBrains decompiler
// Type: #=zMv9TAT1PEEnC0UeBhCNwDO3VFM5XbySLODko9bHLvrDkMuy0qw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public class \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDO3VFM5XbySLODko9bHLvrDkMuy0qw\u003D\u003D : 
  \u0023\u003DzMDDpCIYr0KRiCa3HPMUguqs\u002454aYHA4owg\u003D\u003D,
  \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA
{
  protected readonly List<double> _minorTicks = new List<double>();
  protected readonly List<double> \u0023\u003DzlBZQ9Hda4_JM = new List<double>();
  protected readonly List<float> \u0023\u003Dzjsd5bsWN_iJC9g3Wpg\u003D\u003D = new List<float>();
  protected readonly List<float> \u0023\u003DzDi26vlWukOA5T4p1DA\u003D\u003D = new List<float>();
  private double \u0023\u003DzO_MoV2Kpn\u0024rL;
  private double \u0023\u003DzYy\u0024VrU8bBAGC;

  public virtual TickCoordinates \u0023\u003DzU4j4bt2YhYuc(
    double[] _param1,
    double[] _param2)
  {
    this._minorTicks.Clear();
    this.\u0023\u003DzlBZQ9Hda4_JM.Clear();
    this.\u0023\u003Dzjsd5bsWN_iJC9g3Wpg\u003D\u003D.Clear();
    this.\u0023\u003DzDi26vlWukOA5T4p1DA\u003D\u003D.Clear();
    this.\u0023\u003DzYy\u0024VrU8bBAGC = this.\u0023\u003DzeuxrJCE00Q0n();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w = this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    if (xkzemsMs5tGkouk5w != null && Math.Abs(this.\u0023\u003DzYy\u0024VrU8bBAGC) >= double.Epsilon)
    {
      this.\u0023\u003DzO_MoV2Kpn\u0024rL = this.\u0023\u003DzHZDgUSdfqmkx().GetOffsetForLabels();
      if (!((IEnumerable<double>) _param1).\u0023\u003DzCCMM80zDpO6N<double>())
        this.\u0023\u003DzrfVZUmcocqSCBDcs2A\u003D\u003D(_param1, xkzemsMs5tGkouk5w, false);
      if (!((IEnumerable<double>) _param2).\u0023\u003DzCCMM80zDpO6N<double>())
        this.\u0023\u003DzrfVZUmcocqSCBDcs2A\u003D\u003D(_param2, xkzemsMs5tGkouk5w, true);
    }
    return new TickCoordinates(this._minorTicks.ToArray(), this.\u0023\u003DzlBZQ9Hda4_JM.ToArray(), this.\u0023\u003Dzjsd5bsWN_iJC9g3Wpg\u003D\u003D.ToArray(), this.\u0023\u003DzDi26vlWukOA5T4p1DA\u003D\u003D.ToArray());
  }

  private double \u0023\u003DzeuxrJCE00Q0n()
  {
    double num = this.\u0023\u003DzHZDgUSdfqmkx().IsHorizontalAxis ? this.\u0023\u003DzHZDgUSdfqmkx().ActualWidth : this.\u0023\u003DzHZDgUSdfqmkx().ActualHeight;
    if (Math.Abs(num) < double.Epsilon && this.\u0023\u003DzHZDgUSdfqmkx().get_ParentSurface() != null)
    {
      IRenderSurface renderSurface = this.\u0023\u003DzHZDgUSdfqmkx().get_ParentSurface().get_RenderSurface();
      if (renderSurface != null)
        num = this.\u0023\u003DzHZDgUSdfqmkx().IsHorizontalAxis ? renderSurface.ActualWidth : renderSurface.ActualHeight;
    }
    return num;
  }

  protected virtual void \u0023\u003DzrfVZUmcocqSCBDcs2A\u003D\u003D(
    double[] _param1,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> _param2,
    bool _param3)
  {
    List<double> doubleList = _param3 ? this.\u0023\u003DzlBZQ9Hda4_JM : this._minorTicks;
    List<float> floatList = _param3 ? this.\u0023\u003DzDi26vlWukOA5T4p1DA\u003D\u003D : this.\u0023\u003Dzjsd5bsWN_iJC9g3Wpg\u003D\u003D;
    double num1 = _param1[0];
    double num2 = num1;
    double num3 = _param2.\u0023\u003DzhL6gsJw\u003D(num2);
    double num4 = num3;
    if (this.\u0023\u003DzxGhbraO0gg9\u0024(num4))
    {
      floatList.Add((float) num4);
      doubleList.Add(num2);
    }
    for (int index = 1; index < _param1.Length; ++index)
    {
      double num5 = _param1[index];
      if (Math.Abs(num5 - num1) > double.Epsilon)
      {
        double num6 = _param2.\u0023\u003DzhL6gsJw\u003D(_param1[index]);
        if (Math.Abs(num6 - num3) > double.Epsilon && this.\u0023\u003DzxGhbraO0gg9\u0024(num6))
        {
          doubleList.Add(num5);
          floatList.Add((float) num6);
          num1 = num5;
          num3 = num6;
        }
      }
    }
  }

  protected virtual bool \u0023\u003DzxGhbraO0gg9\u0024(double _param1)
  {
    _param1 -= this.\u0023\u003DzO_MoV2Kpn\u0024rL;
    return _param1 >= -1E-08 && _param1 < this.\u0023\u003DzYy\u0024VrU8bBAGC - -1E-08;
  }
}
