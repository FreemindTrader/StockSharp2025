// Decompiled with JetBrains decompiler
// Type: #=zEa5ACpOap4rFIaHj5p9yfBXHkLrSpjKkECN63LlD3q0NQ7YuqbrWmwnkh1lphV2PvRok2vPGT0BXaveXSW9mXjk=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

#nullable disable
internal sealed class \u0023\u003DzEa5ACpOap4rFIaHj5p9yfBXHkLrSpjKkECN63LlD3q0NQ7YuqbrWmwnkh1lphV2PvRok2vPGT0BXaveXSW9mXjk\u003D : 
  \u0023\u003DzyhbJ\u0024o3\u0024d46dmasRm5VGLHE4noQv5jvPOdidvRVonqXR1_40XGWjzqTPSwaJ
{
  private \u0023\u003DzyhbJ\u0024o3\u0024d46dmasRm5VGLHE4noQv5jvPOdidvRVonqXR1_40XGWjzqTPSwaJ \u0023\u003DzoVRCaVeCBmwtQQ3gMw\u003D\u003D;

  public \u0023\u003DzEa5ACpOap4rFIaHj5p9yfBXHkLrSpjKkECN63LlD3q0NQ7YuqbrWmwnkh1lphV2PvRok2vPGT0BXaveXSW9mXjk\u003D(
    \u0023\u003DzyhbJ\u0024o3\u0024d46dmasRm5VGLHE4noQv5jvPOdidvRVonqXR1_40XGWjzqTPSwaJ _param1)
  {
    this.\u0023\u003DzoVRCaVeCBmwtQQ3gMw\u003D\u003D = _param1;
  }

  public int \u0023\u003DzBkt7RJwYL3SkbpPPrQ\u003D\u003D(int _param1, int _param2, int _param3)
  {
    int num1 = _param3 << 1;
    int num2 = this.\u0023\u003DzoVRCaVeCBmwtQQ3gMw\u003D\u003D.\u0023\u003DzBkt7RJwYL3SkbpPPrQ\u003D\u003D(_param1, _param2, _param3) % num1;
    if (num2 < 0)
      num2 += num1;
    if (num2 >= _param3)
      num2 = num1 - num2;
    return num2;
  }
}
