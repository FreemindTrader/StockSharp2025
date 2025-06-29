// Decompiled with JetBrains decompiler
// Type: #=zdPAQRlt3VWWvvKbSPLZ0IbXKr1uUQJhy25OOrcypN2PAALR7DaWZkesO3q0NSOGk1Q==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.CompilerServices;

#nullable disable
internal class \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IbXKr1uUQJhy25OOrcypN2PAALR7DaWZkesO3q0NSOGk1Q\u003D\u003D : 
  \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0LKrMWRP8HpfFNPgYY55tLjfw\u003D\u003D
{
  protected \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D \u0023\u003DzcRozJLFRZ0BY;
  protected int \u0023\u003DzI6P8IpE\u003D;
  protected int \u0023\u003DzgwQEFlDsVMon;
  protected int \u0023\u003DzFfSb8y0\u003D;
  protected int \u0023\u003DzVllaPSUsIxTUfFLaKw\u003D\u003D;
  protected byte[] \u0023\u003DzDm5AkRtEtmYQ;
  protected int \u0023\u003DzLVC\u00242OK7OxP3 = -1;
  private int \u0023\u003Dz9LgI12vZMy\u0024F;

  public \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IbXKr1uUQJhy25OOrcypN2PAALR7DaWZkesO3q0NSOGk1Q\u003D\u003D(
    \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D _param1)
  {
    this.\u0023\u003DzotQWOIc\u003D(_param1);
  }

  private void \u0023\u003DzotQWOIc\u003D(
    \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D _param1)
  {
    this.\u0023\u003DzcRozJLFRZ0BY = _param1;
    this.\u0023\u003DzDm5AkRtEtmYQ = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
    this.\u0023\u003Dz9LgI12vZMy\u0024F = this.\u0023\u003DzcRozJLFRZ0BY.Width;
    this.\u0023\u003DzVllaPSUsIxTUfFLaKw\u003D\u003D = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzQB4v2EccUot6eT2VRw\u003D\u003D();
  }

  [SpecialName]
  public \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D \u0023\u003Dz8hY71usSYcKH()
  {
    return this.\u0023\u003DzcRozJLFRZ0BY;
  }

  private byte[] \u0023\u003Dz\u00246e75ZE\u003D(out int _param1)
  {
    int num1 = this.\u0023\u003DzI6P8IpE\u003D;
    int num2 = this.\u0023\u003DzFfSb8y0\u003D;
    if ((uint) num1 >= (uint) this.\u0023\u003DzcRozJLFRZ0BY.Width)
      num1 = num1 >= 0 ? this.\u0023\u003DzcRozJLFRZ0BY.Width - 1 : 0;
    if ((uint) num2 >= (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height)
      num2 = num2 >= 0 ? this.\u0023\u003DzcRozJLFRZ0BY.Height - 1 : 0;
    _param1 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(num1, num2);
    return this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
  }

  public byte[] \u0023\u003DzmiTEKiA\u003D(int _param1, int _param2, int _param3, out int _param4)
  {
    this.\u0023\u003DzI6P8IpE\u003D = this.\u0023\u003DzgwQEFlDsVMon = _param1;
    this.\u0023\u003DzFfSb8y0\u003D = _param2;
    if ((uint) _param2 < (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height && _param1 >= 0 && _param1 + _param3 <= this.\u0023\u003DzcRozJLFRZ0BY.Width)
    {
      _param4 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
      this.\u0023\u003DzDm5AkRtEtmYQ = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
      this.\u0023\u003DzLVC\u00242OK7OxP3 = _param4;
      return this.\u0023\u003DzDm5AkRtEtmYQ;
    }
    this.\u0023\u003DzLVC\u00242OK7OxP3 = -1;
    return this.\u0023\u003Dz\u00246e75ZE\u003D(out _param4);
  }

  public byte[] \u0023\u003DziwTQ98wsEeu3(out int _param1)
  {
    if (this.\u0023\u003DzLVC\u00242OK7OxP3 != -1)
    {
      this.\u0023\u003DzLVC\u00242OK7OxP3 += this.\u0023\u003DzVllaPSUsIxTUfFLaKw\u003D\u003D;
      _param1 = this.\u0023\u003DzLVC\u00242OK7OxP3;
      return this.\u0023\u003DzDm5AkRtEtmYQ;
    }
    ++this.\u0023\u003DzI6P8IpE\u003D;
    return this.\u0023\u003Dz\u00246e75ZE\u003D(out _param1);
  }

  public byte[] \u0023\u003DzXgTbLkAgh1Pd(out int _param1)
  {
    ++this.\u0023\u003DzFfSb8y0\u003D;
    this.\u0023\u003DzI6P8IpE\u003D = this.\u0023\u003DzgwQEFlDsVMon;
    if (this.\u0023\u003DzLVC\u00242OK7OxP3 != -1 && (uint) this.\u0023\u003DzFfSb8y0\u003D < (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height)
    {
      this.\u0023\u003DzLVC\u00242OK7OxP3 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(this.\u0023\u003DzI6P8IpE\u003D, this.\u0023\u003DzFfSb8y0\u003D);
      this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
      _param1 = this.\u0023\u003DzLVC\u00242OK7OxP3;
      return this.\u0023\u003DzDm5AkRtEtmYQ;
    }
    this.\u0023\u003DzLVC\u00242OK7OxP3 = -1;
    return this.\u0023\u003Dz\u00246e75ZE\u003D(out _param1);
  }
}
