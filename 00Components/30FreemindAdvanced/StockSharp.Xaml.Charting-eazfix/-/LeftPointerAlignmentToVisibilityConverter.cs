// Decompiled with JetBrains decompiler
// Type: -.LeftPointerAlignmentToVisibilityConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class LeftPointerAlignmentToVisibilityConverter : IValueConverter
{
  
  private bool \u0023\u003Dz7GPhXtbDen9tX_jAMvwBl2E\u003D;

  
  public bool IsLeftPointer
  {
    get => this.\u0023\u003Dz7GPhXtbDen9tX_jAMvwBl2E\u003D;
    set => this.\u0023\u003Dz7GPhXtbDen9tX_jAMvwBl2E\u003D = value;
  }

  public bool \u0023\u003Dz0T_we1eshhoI() => this.\u0023\u003Dz7GPhXtbDen9tX_jAMvwBl2E\u003D;

  public void \u0023\u003DzB\u00245R8EsdcS2I(bool _param1)
  {
    this.\u0023\u003Dz7GPhXtbDen9tX_jAMvwBl2E\u003D = _param1;
  }

  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    AxisAlignment demydmpA2K68QEjd = (AxisAlignment) _param1;
    return (object) (Visibility) (!this.\u0023\u003Dz0T_we1eshhoI() ? (demydmpA2K68QEjd == AxisAlignment.Left || demydmpA2K68QEjd == AxisAlignment.Top ? 0 : 2) : (demydmpA2K68QEjd == AxisAlignment.Left || demydmpA2K68QEjd == AxisAlignment.Top ? 2 : 0));
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
