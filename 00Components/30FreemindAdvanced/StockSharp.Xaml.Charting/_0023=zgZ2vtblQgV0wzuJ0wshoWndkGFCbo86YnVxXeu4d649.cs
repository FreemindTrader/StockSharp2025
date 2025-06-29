// Decompiled with JetBrains decompiler
// Type: #=zgZ2vtblQgV0wzuJ0wshoWndkGFCbo86YnVxXeu4d649rQayMtZriFpDMOQeEg6m9il48VSo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
internal struct \u0023\u003DzgZ2vtblQgV0wzuJ0wshoWndkGFCbo86YnVxXeu4d649rQayMtZriFpDMOQeEg6m9il48VSo\u003D : 
  \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7\u00247IVjNcUWYRVrjRbV\u0024QDTRFg\u003D\u003D
{
  public double \u0023\u003Dzh1hhOkJ3kH4Y() => 1.5;

  public double \u0023\u003DzG17fc7\u0024pCNOA(double _param1)
  {
    if (_param1 < 0.5)
      return 0.75 - _param1 * _param1;
    if (_param1 >= 1.5)
      return 0.0;
    double num = _param1 - 1.5;
    return 0.5 * num * num;
  }
}
