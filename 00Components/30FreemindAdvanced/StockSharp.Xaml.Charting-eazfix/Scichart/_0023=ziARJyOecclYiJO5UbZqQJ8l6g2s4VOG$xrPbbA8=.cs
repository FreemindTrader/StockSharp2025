// Decompiled with JetBrains decompiler
// Type: #=ziARJyOecclYiJO5UbZqQJ8l6g2s4VOG$xrPbbA8=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
internal static class \u0023\u003DziARJyOecclYiJO5UbZqQJ8l6g2s4VOG\u0024xrPbbA8\u003D
{
  internal static void \u0023\u003DzmFyFyI4\u003D(
    this UIElementCollection _param0,
    Func<UIElement, bool> _param1)
  {
    for (int index = _param0.Count - 1; index >= 0; --index)
    {
      if (_param1(_param0[index]))
        _param0.RemoveAt(index);
    }
  }

  internal static UIElement \u0023\u003DzANkihmf319MM(
    this UIElementCollection _param0,
    Func<UIElement, bool> _param1)
  {
    for (int index = 0; index < _param0.Count; ++index)
    {
      if (_param1(_param0[index]))
        return _param0[index];
    }
    return (UIElement) null;
  }
}
