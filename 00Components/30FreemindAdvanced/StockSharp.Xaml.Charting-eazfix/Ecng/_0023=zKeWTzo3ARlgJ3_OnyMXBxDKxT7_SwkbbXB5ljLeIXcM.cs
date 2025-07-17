// Decompiled with JetBrains decompiler
// Type: #=zKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE\u003D : 
  IGammaFunction
{
  private double m_start;
  private double \u0023\u003Dz\u0024Sd6FL97Xiz8;

  public \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE\u003D()
  {
    this.m_start = 0.0;
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = 1.0;
  }

  public \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE\u003D(
    double _param1,
    double _param2)
  {
    this.m_start = _param1;
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = _param2;
  }

  public void \u0023\u003DzUtSIjNc\u003D(double _param1, double _param2)
  {
    this.m_start = _param1;
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = _param2;
  }

  public void \u0023\u003DzCVPVJaY\u003D(double _param1)
  {
    this.m_start = _param1;
  }

  public void \u0023\u003DzADTLGy4\u003D(double _param1)
  {
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = _param1;
  }

  public double \u0023\u003DzCVPVJaY\u003D() => this.m_start;

  public double \u0023\u003DzADTLGy4\u003D() => this.\u0023\u003Dz\u0024Sd6FL97Xiz8;

  public double \u0023\u003DzoxmYZFvB84ZN(double _param1)
  {
    if (_param1 < this.m_start)
      return 0.0;
    if (_param1 > this.\u0023\u003Dz\u0024Sd6FL97Xiz8)
      return 1.0;
    double num = this.\u0023\u003Dz\u0024Sd6FL97Xiz8 - this.m_start;
    return num != 0.0 ? (_param1 - this.m_start) / num : 0.0;
  }
}
