// Decompiled with JetBrains decompiler
// Type: #=z03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public interface IDrawable
{
  double Width { get; set; }

  double Height { get; set; }

  void OnDraw(
    IRenderContext2D _param1,
    IRenderPassData _param2);
}
