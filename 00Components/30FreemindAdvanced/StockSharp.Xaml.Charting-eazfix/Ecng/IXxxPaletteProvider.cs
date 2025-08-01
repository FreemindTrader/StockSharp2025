// Decompiled with JetBrains decompiler
// Type: #=z8HlC6EDl$btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows.Media;

#nullable disable
public interface IXxxPaletteProvider
{
  Color? GetColor01(
    IRenderableSeries _param1,
    double _param2,
    double _param3);

  Color? GetColor02(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6);

  Color? GetColor02(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4);
}
