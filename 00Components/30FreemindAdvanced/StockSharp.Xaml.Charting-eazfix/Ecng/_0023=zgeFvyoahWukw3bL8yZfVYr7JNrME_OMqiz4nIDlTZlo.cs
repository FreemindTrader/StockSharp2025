// Decompiled with JetBrains decompiler
// Type: #=zgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;

#nullable disable
public struct TickCoordinates(
  double[ ] minorTicks,
  double[ ] majorTicks,
  float[ ] minorCoords,
  float[ ] majorCoords )
{

    private readonly double[ ] _minorTicks = minorTicks;
  
  private readonly double[ ] _majorTicks = majorTicks;
  
  private readonly float[ ] _minorCoords = minorCoords;
  
  private readonly float[ ] _majorCoords = majorCoords;

  public bool \u0023\u003DzE9zGTWQAFP9k()
    {
        return this._majorCoords == null || this._minorCoords == null;
    }

    public double[ ] \u0023\u003Dza3zX1a5AAgFk() => this._minorTicks;

  public double[ ] \u0023\u003Dzyqh0CrzbJnzy() => this._majorTicks;

  public float[ ] \u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D() => this._minorCoords;

  public float[ ] \u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D()
  {
    return this._majorCoords;
  }
}
