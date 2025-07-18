// Decompiled with JetBrains decompiler
// Type: #=zJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;

#nullable disable
public struct \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb
{
  
  public int \u0023\u003DzwP120vA\u003D;
  
  public int \u0023\u003Dzi8jDI4I\u003D;
  
  public int \u0023\u003Dzu6bzeD\u0024Y6N55;
  
  public int _chartArea_093;
  
  public int \u0023\u003DzLw_1RqQ\u003D;
  
  public int \u0023\u003DzTgDnxCg\u003D;

  public void \u0023\u003DzIT\u0024DfT8\u003D()
  {
    this.\u0023\u003DzwP120vA\u003D = int.MaxValue;
    this.\u0023\u003Dzi8jDI4I\u003D = int.MaxValue;
    this.\u0023\u003Dzu6bzeD\u0024Y6N55 = 0;
    this._chartArea_093 = 0;
    this.\u0023\u003DzLw_1RqQ\u003D = -1;
    this.\u0023\u003DzTgDnxCg\u003D = -1;
  }

  public void \u0023\u003DzBYt8raY\u003D(
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb _param1)
  {
    this.\u0023\u003DzwP120vA\u003D = _param1.\u0023\u003DzwP120vA\u003D;
    this.\u0023\u003Dzi8jDI4I\u003D = _param1.\u0023\u003Dzi8jDI4I\u003D;
    this.\u0023\u003Dzu6bzeD\u0024Y6N55 = _param1.\u0023\u003Dzu6bzeD\u0024Y6N55;
    this._chartArea_093 = _param1._chartArea_093;
    this.\u0023\u003DzLw_1RqQ\u003D = _param1.\u0023\u003DzLw_1RqQ\u003D;
    this.\u0023\u003DzTgDnxCg\u003D = _param1.\u0023\u003DzTgDnxCg\u003D;
  }

  public void \u0023\u003DzgQgmhv4\u003D(
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb _param1)
  {
    this.\u0023\u003DzLw_1RqQ\u003D = _param1.\u0023\u003DzLw_1RqQ\u003D;
    this.\u0023\u003DzTgDnxCg\u003D = _param1.\u0023\u003DzTgDnxCg\u003D;
  }

  public bool \u0023\u003DzVMp0AwLhFwUR(
    int _param1,
    int _param2,
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb _param3)
  {
    return (_param1 - this.\u0023\u003DzwP120vA\u003D | _param2 - this.\u0023\u003Dzi8jDI4I\u003D | this.\u0023\u003DzLw_1RqQ\u003D - _param3.\u0023\u003DzLw_1RqQ\u003D | this.\u0023\u003DzTgDnxCg\u003D - _param3.\u0023\u003DzTgDnxCg\u003D) != 0;
  }
}
