// Decompiled with JetBrains decompiler
// Type: -.dje_zHRCSWK4ZNLJHREKZYGF3XQ6G39RRTFP4GZDJWS9Z6DQ5AUA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zHRCSWK4ZNLJHREKZYGF3XQ6G39RRTFP4GZDJWS9Z6DQ5AUA_ejd : IValueConverter
{
  
  private TextBlock \u0023\u003Dza54w02Q\u003D = new TextBlock();

  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D dataContext = !(_param1 is TextBlock textBlock) || textBlock.DataContext == null ? (\u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D) null : textBlock.DataContext as \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D;
    Thickness thickness = new Thickness(0.0, 0.0, 0.0, 0.0);
    if (dataContext != null && dataContext.HasExponent)
    {
      double num = double.Parse((string) _param3, (IFormatProvider) CultureInfo.InvariantCulture);
      this.\u0023\u003Dza54w02Q\u003D.FontSize = textBlock.FontSize;
      this.\u0023\u003Dza54w02Q\u003D.\u0023\u003DzI0WdlDcUgrX_();
      thickness.Top = this.\u0023\u003Dza54w02Q\u003D.ActualHeight * num;
    }
    return (object) thickness;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
