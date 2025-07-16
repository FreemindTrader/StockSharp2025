// Decompiled with JetBrains decompiler
// Type: #=z4lH8q7tXMt_gtLJO2itFk8kG5ycMeCyB90H5GJw=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public static class \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk8kG5ycMeCyB90H5GJw\u003D
{
  public static float \u0023\u003DzcYUW_6FX9t5L(this float _param0)
  {
    if ((double) _param0 > 2147483648.0)
      return (float) int.MaxValue;
    return (double) _param0 < (double) int.MinValue ? (float) int.MinValue : _param0;
  }
}
