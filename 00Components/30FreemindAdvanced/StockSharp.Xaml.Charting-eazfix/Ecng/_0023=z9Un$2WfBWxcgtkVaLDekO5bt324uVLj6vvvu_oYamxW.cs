// Decompiled with JetBrains decompiler
// Type: #=z9Un$2WfBWxcgtkVaLDekO5bt324uVLj6vvvu_oYamxWfKZJqA7XWueY_TW6QxtgNQQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003Dz9Un\u00242WfBWxcgtkVaLDekO5bt324uVLj6vvvu_oYamxWfKZJqA7XWueY_TW6QxtgNQQ\u003D\u003D : 
  \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IbXKr1uUQJhy25OOrcypN2PAALR7DaWZkesO3q0NSOGk1Q\u003D\u003D
{
  private byte[] \u0023\u003Dzz4UoRxITY5g\u0024;

  public \u0023\u003Dz9Un\u00242WfBWxcgtkVaLDekO5bt324uVLj6vvvu_oYamxWfKZJqA7XWueY_TW6QxtgNQQ\u003D\u003D(
    IImageByte _param1,
    RGBA_Bytes _param2)
    : base(_param1)
  {
    this.\u0023\u003Dzz4UoRxITY5g\u0024 = new byte[4];
    this.\u0023\u003Dzz4UoRxITY5g\u0024[0] = _param2.\u0023\u003Dz4WHdt9g\u003D;
    this.\u0023\u003Dzz4UoRxITY5g\u0024[1] = _param2.\u0023\u003DzoRsAtmfOFDZe;
    this.\u0023\u003Dzz4UoRxITY5g\u0024[2] = _param2.\u0023\u003DzcdKuX48ZXN_S;
    this.\u0023\u003Dzz4UoRxITY5g\u0024[3] = _param2.\u0023\u003DzKCqGEcs\u003D;
  }

  private byte[] \u0023\u003Dz\u00246e75ZE\u003D(out int _param1)
  {
    if ((uint) this.\u0023\u003DzI6P8IpE\u003D < (uint) this.\u0023\u003DzcRozJLFRZ0BY.Width && (uint) this.\u0023\u003DzFfSb8y0\u003D < (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height)
    {
      _param1 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(this.\u0023\u003DzI6P8IpE\u003D, this.\u0023\u003DzFfSb8y0\u003D);
      return this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
    }
    _param1 = 0;
    return this.\u0023\u003Dzz4UoRxITY5g\u0024;
  }
}
