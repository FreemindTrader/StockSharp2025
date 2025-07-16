// Decompiled with JetBrains decompiler
// Type: #=zupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4UhFWmYrjpRbDFG5IddQAZY6KwEmTlLAbcF
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4UhFWmYrjpRbDFG5IddQAZY6KwEmTlLAbcF : 
  \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7\u00247IVjNcUWYRVrjRbV\u0024QDTRFg\u003D\u003D
{
  private double \u0023\u003DzOD069RY\u003D;
  private double \u0023\u003Dzmjtu3pY\u003D;
  private double \u0023\u003Dz4SlvGgQ\u003D;
  private double \u0023\u003DzRSScfKM\u003D;
  private double \u0023\u003DzvER60UA\u003D;
  private double \u0023\u003DzBVUwUdM\u003D;
  private double \u0023\u003Dzd58rBaE\u003D;

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4UhFWmYrjpRbDFG5IddQAZY6KwEmTlLAbcF()
    : this(1.0 / 3.0, 1.0 / 3.0)
  {
  }

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_1tOh89RHRg432Xo3uSUY4UhFWmYrjpRbDFG5IddQAZY6KwEmTlLAbcF(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzOD069RY\u003D = (6.0 - 2.0 * _param1) / 6.0;
    this.\u0023\u003Dzmjtu3pY\u003D = (12.0 * _param1 - 18.0 + 6.0 * _param2) / 6.0;
    this.\u0023\u003Dz4SlvGgQ\u003D = (12.0 - 9.0 * _param1 - 6.0 * _param2) / 6.0;
    this.\u0023\u003DzRSScfKM\u003D = (8.0 * _param1 + 24.0 * _param2) / 6.0;
    this.\u0023\u003DzvER60UA\u003D = (-12.0 * _param1 - 48.0 * _param2) / 6.0;
    this.\u0023\u003DzBVUwUdM\u003D = (6.0 * _param1 + 30.0 * _param2) / 6.0;
    this.\u0023\u003Dzd58rBaE\u003D = (-_param1 - 6.0 * _param2) / 6.0;
  }

  public double \u0023\u003Dzh1hhOkJ3kH4Y() => 2.0;

  public double \u0023\u003DzG17fc7\u0024pCNOA(double _param1)
  {
    if (_param1 < 1.0)
      return this.\u0023\u003DzOD069RY\u003D + _param1 * _param1 * (this.\u0023\u003Dzmjtu3pY\u003D + _param1 * this.\u0023\u003Dz4SlvGgQ\u003D);
    return _param1 < 2.0 ? this.\u0023\u003DzRSScfKM\u003D + _param1 * (this.\u0023\u003DzvER60UA\u003D + _param1 * (this.\u0023\u003DzBVUwUdM\u003D + _param1 * this.\u0023\u003Dzd58rBaE\u003D)) : 0.0;
  }
}
