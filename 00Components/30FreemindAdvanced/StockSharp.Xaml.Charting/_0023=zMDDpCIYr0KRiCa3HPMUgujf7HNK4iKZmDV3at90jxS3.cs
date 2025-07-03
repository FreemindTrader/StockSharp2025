// Decompiled with JetBrains decompiler
// Type: #=zMDDpCIYr0KRiCa3HPMUgujf7HNK4iKZmDV3at90jxS3JM0NsILRb5TM=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

#nullable disable
internal struct \u0023\u003DzMDDpCIYr0KRiCa3HPMUgujf7HNK4iKZmDV3at90jxS3JM0NsILRb5TM\u003D : 
  IBrush2D,
  IPathColor,
  IDisposable
{
  
  private Color \u0023\u003Dzfzo3Zt0\u003D;
  
  private int \u0023\u003DzY0hdWq2CWq89;
  
  private bool \u0023\u003Dzy_U\u00246cJFiCuB;
  
  private bool \u0023\u003DzPsHNvsjqIq72;

  internal \u0023\u003DzMDDpCIYr0KRiCa3HPMUgujf7HNK4iKZmDV3at90jxS3JM0NsILRb5TM\u003D(
    Color _param1,
    int _param2,
    bool _param3)
    : this()
  {
    this.\u0023\u003DzPsHNvsjqIq72 = _param1.A == (byte) 0;
    this.\u0023\u003Dzfzo3Zt0\u003D = _param1;
    this.\u0023\u003DzY0hdWq2CWq89 = _param2;
    this.\u0023\u003Dzy_U\u00246cJFiCuB = _param3;
  }

  public Color Color => this.\u0023\u003Dzfzo3Zt0\u003D;

  [SpecialName]
  public int ColorCode => this.\u0023\u003DzY0hdWq2CWq89;

  [SpecialName]
  public bool \u0023\u003DzZTHbSX1_i1\u0024W() => this.\u0023\u003Dzy_U\u00246cJFiCuB;

  [SpecialName]
  public bool IsTransparent => this.\u0023\u003DzPsHNvsjqIq72;

  public void Dispose()
  {
  }
}
