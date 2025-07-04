// Decompiled with JetBrains decompiler
// Type: #=zKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE\u003D : 
  \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOx1RkmuVmhXUogDkNvXBX5gA\u003D\u003D
{
  private double \u0023\u003Dzd7Ch78N2\u0024CEF;
  private double \u0023\u003Dz\u0024Sd6FL97Xiz8;

  public \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE\u003D()
  {
    this.\u0023\u003Dzd7Ch78N2\u0024CEF = 0.0;
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = 1.0;
  }

  public \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxDKxT7_SwkbbXB5ljLeIXcMJiILP8FzQLfCtZ2tm3r51dHmf6KE\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003Dzd7Ch78N2\u0024CEF = _param1;
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = _param2;
  }

  public void \u0023\u003DzUtSIjNc\u003D(double _param1, double _param2)
  {
    this.\u0023\u003Dzd7Ch78N2\u0024CEF = _param1;
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = _param2;
  }

  public void \u0023\u003DzCVPVJaY\u003D(double _param1)
  {
    this.\u0023\u003Dzd7Ch78N2\u0024CEF = _param1;
  }

  public void \u0023\u003DzADTLGy4\u003D(double _param1)
  {
    this.\u0023\u003Dz\u0024Sd6FL97Xiz8 = _param1;
  }

  public double \u0023\u003DzCVPVJaY\u003D() => this.\u0023\u003Dzd7Ch78N2\u0024CEF;

  public double \u0023\u003DzADTLGy4\u003D() => this.\u0023\u003Dz\u0024Sd6FL97Xiz8;

  public double \u0023\u003DzoxmYZFvB84ZN(double _param1)
  {
    if (_param1 < this.\u0023\u003Dzd7Ch78N2\u0024CEF)
      return 0.0;
    if (_param1 > this.\u0023\u003Dz\u0024Sd6FL97Xiz8)
      return 1.0;
    double num = this.\u0023\u003Dz\u0024Sd6FL97Xiz8 - this.\u0023\u003Dzd7Ch78N2\u0024CEF;
    return num != 0.0 ? (_param1 - this.\u0023\u003Dzd7Ch78N2\u0024CEF) / num : 0.0;
  }
}
