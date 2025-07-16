// Decompiled with JetBrains decompiler
// Type: #=zx24ajpn1eHsuu_VYWsSDOekGezrOMR3n8XeekKxbnsBAyei2EFEOSZU6tCOc
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOekGezrOMR3n8XeekKxbnsBAyei2EFEOSZU6tCOc<\u0023\u003Dz2n1QiX8\u003D>
{
  private \u0023\u003Dz2n1QiX8\u003D[][] \u0023\u003Dz\u0024XVulME\u003D;

  public \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOekGezrOMR3n8XeekKxbnsBAyei2EFEOSZU6tCOc(
    int _param1,
    int _param2)
  {
    this.\u0023\u003Dz\u0024XVulME\u003D = new \u0023\u003Dz2n1QiX8\u003D[_param2][];
    for (int index = 0; index < _param2; ++index)
      this.\u0023\u003Dz\u0024XVulME\u003D[index] = new \u0023\u003Dz2n1QiX8\u003D[_param1];
  }

  public int Width => this.\u0023\u003Dznk4ubo4\u003D(0).Length;

  public int Height => this.\u0023\u003Dz\u0024XVulME\u003D.Length;

  public \u0023\u003Dz2n1QiX8\u003D[] \u0023\u003Dznk4ubo4\u003D(int _param1)
  {
    return this.\u0023\u003Dz\u0024XVulME\u003D[_param1];
  }

  public void \u0023\u003Dzf12y3QM\u003D(\u0023\u003Dz2n1QiX8\u003D _param1)
  {
    for (int index1 = 0; index1 < this.Height; ++index1)
    {
      \u0023\u003Dz2n1QiX8\u003D[] z2n1QiX8Array = this.\u0023\u003Dznk4ubo4\u003D(index1);
      for (int index2 = 0; index2 < this.Width; ++index2)
        z2n1QiX8Array[index2] = _param1;
    }
  }

  public \u0023\u003Dz2n1QiX8\u003D \u0023\u003Dzm2nn9hA\u003D(int _param1, int _param2)
  {
    return this.\u0023\u003Dznk4ubo4\u003D(_param2)[_param1];
  }

  public void \u0023\u003DzoqqYOEI\u003D(
    int _param1,
    int _param2,
    \u0023\u003Dz2n1QiX8\u003D _param3)
  {
    this.\u0023\u003Dznk4ubo4\u003D(_param2)[_param1] = _param3;
  }
}
