// Decompiled with JetBrains decompiler
// Type: #=z2zU2vmiOzxGfUBTaur1A_iQu$j6ASeZtpadRHtigMHDTNt3I_DlkbO$xcPiwr2VVK3iNKP1vqFN5YRbV4Q==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_iQu\u0024j6ASeZtpadRHtigMHDTNt3I_DlkbO\u0024xcPiwr2VVK3iNKP1vqFN5YRbV4Q\u003D\u003D
{
  private int \u0023\u003DzFfSb8y0\u003D;
  private int \u0023\u003DzyZHOoitujESX;
  private int \u0023\u003DzOxmXVfz021CI;
  private int \u0023\u003DzcVjsGLmh4U4S;

  public \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_iQu\u0024j6ASeZtpadRHtigMHDTNt3I_DlkbO\u0024xcPiwr2VVK3iNKP1vqFN5YRbV4Q\u003D\u003D(
    int _param1)
  {
    this.\u0023\u003DzcVjsGLmh4U4S = _param1;
  }

  public \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_iQu\u0024j6ASeZtpadRHtigMHDTNt3I_DlkbO\u0024xcPiwr2VVK3iNKP1vqFN5YRbV4Q\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    this.\u0023\u003DzcVjsGLmh4U4S = _param4;
    this.\u0023\u003DzFfSb8y0\u003D = _param1;
    this.\u0023\u003DzyZHOoitujESX = (_param2 - _param1 << this.\u0023\u003DzcVjsGLmh4U4S) / _param3;
    this.\u0023\u003DzOxmXVfz021CI = 0;
  }

  public void \u0023\u003DzXiQrjbw\u003D()
  {
    this.\u0023\u003DzOxmXVfz021CI += this.\u0023\u003DzyZHOoitujESX;
  }

  public void \u0023\u003DzocM1nio\u003D()
  {
    this.\u0023\u003DzOxmXVfz021CI -= this.\u0023\u003DzyZHOoitujESX;
  }

  public void \u0023\u003DzXiQrjbw\u003D(int _param1)
  {
    this.\u0023\u003DzOxmXVfz021CI += this.\u0023\u003DzyZHOoitujESX * _param1;
  }

  public void \u0023\u003DzocM1nio\u003D(int _param1)
  {
    this.\u0023\u003DzOxmXVfz021CI -= this.\u0023\u003DzyZHOoitujESX * _param1;
  }

  public int \u0023\u003Dzi8jDI4I\u003D()
  {
    return this.\u0023\u003DzFfSb8y0\u003D + (this.\u0023\u003DzOxmXVfz021CI >> this.\u0023\u003DzcVjsGLmh4U4S);
  }

  public int \u0023\u003Dz5QE1hBg\u003D() => this.\u0023\u003DzOxmXVfz021CI;
}
