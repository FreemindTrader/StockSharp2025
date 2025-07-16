// Decompiled with JetBrains decompiler
// Type: #=zawTMm83sNsuVHdgLsihy4asPr3wVne_a8c42ZIL8x_849b3R84sqCvZtLZ9vQHB_ZaCCCyc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public static class \u0023\u003DzawTMm83sNsuVHdgLsihy4asPr3wVne_a8c42ZIL8x_849b3R84sqCvZtLZ9vQHB_ZaCCCyc\u003D
{
  public static void \u0023\u003DzqpZRTPYdqNQ9(
    IBlenderByte _param0,
    byte[] _param1,
    int _param2,
    RGBA_Bytes _param3)
  {
    _param0.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3);
  }

  public static void \u0023\u003DzaJC7XkKnnvEJF1Swqw\u003D\u003D(
    IBlenderByte _param0,
    byte[] _param1,
    int _param2,
    RGBA_Bytes _param3,
    int _param4)
  {
    if (_param4 == (int) byte.MaxValue)
    {
      \u0023\u003DzawTMm83sNsuVHdgLsihy4asPr3wVne_a8c42ZIL8x_849b3R84sqCvZtLZ9vQHB_ZaCCCyc\u003D.\u0023\u003DzqpZRTPYdqNQ9(_param0, _param1, _param2, _param3);
    }
    else
    {
      _param3.\u0023\u003DzKCqGEcs\u003D = (byte) ((int) _param3.\u0023\u003DzKCqGEcs\u003D * (_param4 + 1) >> 8);
      _param0.\u0023\u003Dz1sAbEWOIYGyA(_param1, _param2, _param3);
    }
  }
}
