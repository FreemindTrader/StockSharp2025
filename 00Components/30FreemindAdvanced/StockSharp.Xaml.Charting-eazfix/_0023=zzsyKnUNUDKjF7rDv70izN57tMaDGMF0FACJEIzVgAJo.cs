// Decompiled with JetBrains decompiler
// Type: #=zzsyKnUNUDKjF7rDv70izN57tMaDGMF0FACJEIzVgAJoem9mflw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003DzzsyKnUNUDKjF7rDv70izN57tMaDGMF0FACJEIzVgAJoem9mflw\u003D\u003D : 
  \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDO3VFM5XbySLODko9bHLvrDkMuy0qw\u003D\u003D
{
  protected override bool \u0023\u003DzxGhbraO0gg9\u0024(double _param1)
  {
    bool flag;
    if (this.\u0023\u003DzHZDgUSdfqmkx().\u0023\u003DzFrVmckt\u0024NpG6())
    {
      int num1 = 0;
      int num2 = 360;
      flag = ((_param1 <= (double) num1 ? (false ? 1 : 0) : (_param1 < (double) num2 ? 1 : 0)) | (this.\u0023\u003DzHZDgUSdfqmkx().get_FlipCoordinates() ? (_param1 <= (double) num2 ? 1 : 0) : (_param1 >= (double) num1 ? 1 : 0))) != 0;
    }
    else
      flag = base.\u0023\u003DzxGhbraO0gg9\u0024(_param1);
    return flag;
  }
}
