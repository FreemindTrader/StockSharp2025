// Decompiled with JetBrains decompiler
// Type: #=zMLvZWaqDqEKovfY1GVv1jDvRHJm4zN8x0ttFQf_UGKPMTTL3MdOFwfA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

#nullable disable
internal struct \u0023\u003DzMLvZWaqDqEKovfY1GVv1jDvRHJm4zN8x0ttFQf_UGKPMTTL3MdOFwfA\u003D : 
  \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D,
  \u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_bD_Pyrb\u0024d0P2noEI5c\u003D,
  IDisposable
{
  
  private Color \u0023\u003Dzfzo3Zt0\u003D;
  
  private bool \u0023\u003Dzy_U\u00246cJFiCuB;
  
  private bool \u0023\u003DzPsHNvsjqIq72;

  internal \u0023\u003DzMLvZWaqDqEKovfY1GVv1jDvRHJm4zN8x0ttFQf_UGKPMTTL3MdOFwfA\u003D(
    Color _param1,
    bool _param2,
    double _param3 = 1.0)
    : this()
  {
    this.\u0023\u003DzPsHNvsjqIq72 = _param1.A == (byte) 0;
    this.\u0023\u003Dzfzo3Zt0\u003D = Color.FromArgb((byte) ((double) _param1.A * _param3), _param1.R, _param1.G, _param1.B);
    this.\u0023\u003Dzy_U\u00246cJFiCuB = _param2;
  }

  public Color Color => this.\u0023\u003Dzfzo3Zt0\u003D;

  [SpecialName]
  public int \u0023\u003DzjOBmdfcoOy1e() => -1;

  [SpecialName]
  public bool \u0023\u003DzZTHbSX1_i1\u0024W() => this.\u0023\u003Dzy_U\u00246cJFiCuB;

  [SpecialName]
  public bool \u0023\u003Dz7zS5QbVF0tOL() => this.\u0023\u003DzPsHNvsjqIq72;

  public void Dispose()
  {
  }
}
