// Decompiled with JetBrains decompiler
// Type: #=z9jHRW$4hcTcRirEhLafLfK9uv3xju3pa2iknGyt_fR$BJa78L31NP20tc6GzxiyxP0HhXfc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dz9jHRW\u00244hcTcRirEhLafLfK9uv3xju3pa2iknGyt_fR\u0024BJa78L31NP20tc6GzxiyxP0HhXfc\u003D : 
  IImageFilterFunction
{
  private double m_radius;

  public \u0023\u003Dz9jHRW\u00244hcTcRirEhLafLfK9uv3xju3pa2iknGyt_fR\u0024BJa78L31NP20tc6GzxiyxP0HhXfc\u003D(
    double _param1)
  {
    this.m_radius = _param1 < 2.0 ? 2.0 : _param1;
  }

  public double radius() => this.m_radius;

  public double calc_weight(double _param1)
  {
    if (_param1 == 0.0)
      return 1.0;
    if (_param1 > this.m_radius)
      return 0.0;
    _param1 *= Math.PI;
    double a = _param1 / this.m_radius;
    return Math.Sin(_param1) / _param1 * (Math.Sin(a) / a);
  }
}
