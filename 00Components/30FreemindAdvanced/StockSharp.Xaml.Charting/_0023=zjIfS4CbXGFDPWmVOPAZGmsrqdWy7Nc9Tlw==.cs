// Decompiled with JetBrains decompiler
// Type: #=zjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;

#nullable disable
internal sealed class \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D : DependencyObject
{
  
  private static Dictionary<string, List<ToggleButton>> \u0023\u003Dz4zWJLHVc016J = new Dictionary<string, List<ToggleButton>>();
  
  public static readonly DependencyProperty \u0023\u003DzXrmaHuc\u003D = DependencyProperty.RegisterAttached("", typeof (string), typeof (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzJbpy7\u00247ugE8z)));

  public static void SetGroupName(ToggleButton _param0, string _param1)
  {
    _param0.SetValue(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzXrmaHuc\u003D, (object) _param1);
  }

  public static string GetGroupName(ToggleButton _param0)
  {
    return _param0.GetValue(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzXrmaHuc\u003D).ToString();
  }

  private static void \u0023\u003DzJbpy7\u00247ugE8z(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is ToggleButton toggleButton))
      return;
    string str1 = _param1.NewValue.ToString();
    string str2 = _param1.OldValue.ToString();
    if (string.IsNullOrEmpty(str1))
    {
      \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzQNgJJH8Qzdgn(str1, toggleButton);
    }
    else
    {
      if (!(str1 != str2))
        return;
      if (!string.IsNullOrEmpty(str2))
        \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzQNgJJH8Qzdgn(str2, toggleButton);
      \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzJqDM9ufvcza7(toggleButton, _param1.NewValue.ToString());
    }
  }

  private static void \u0023\u003DzQNgJJH8Qzdgn(string _param0, ToggleButton _param1)
  {
    List<ToggleButton> toggleButtonList;
    if (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dz4zWJLHVc016J.TryGetValue(_param0, out toggleButtonList))
    {
      toggleButtonList.Remove(_param1);
      if (toggleButtonList.Count == 0)
        \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dz4zWJLHVc016J.Remove(_param0);
    }
    _param1.Click -= \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D ?? (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzLjEhfuERfrg_));
    _param1.Checked -= \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D ?? (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzLjEhfuERfrg_));
    _param1.Unloaded -= \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzMXfvpi5sJ2USDoqFIQ\u003D\u003D ?? (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzMXfvpi5sJ2USDoqFIQ\u003D\u003D = new RoutedEventHandler(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzADgo0e84n7YE));
  }

  private static void \u0023\u003DzJqDM9ufvcza7(ToggleButton _param0, string _param1)
  {
    if (!\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dz4zWJLHVc016J.TryGetValue(_param1, out List<ToggleButton> _))
    {
      List<ToggleButton> toggleButtonList = new List<ToggleButton>();
      \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dz4zWJLHVc016J.Add(_param1, toggleButtonList);
    }
    \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dz4zWJLHVc016J[_param1].Add(_param0);
    _param0.Click += \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D ?? (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzLjEhfuERfrg_));
    _param0.Checked += \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D ?? (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzLjEhfuERfrg_));
    _param0.Unloaded += \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzMXfvpi5sJ2USDoqFIQ\u003D\u003D ?? (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzMXfvpi5sJ2USDoqFIQ\u003D\u003D = new RoutedEventHandler(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzADgo0e84n7YE));
  }

  private static void \u0023\u003DzADgo0e84n7YE(object _param0, RoutedEventArgs _param1)
  {
    ToggleButton toggleButton = (ToggleButton) _param0;
    \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003DzQNgJJH8Qzdgn(\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.GetGroupName(toggleButton), toggleButton);
  }

  private static void \u0023\u003DzLjEhfuERfrg_(object _param0, RoutedEventArgs _param1)
  {
    ToggleButton originalSource = _param1.OriginalSource as ToggleButton;
    foreach (ToggleButton toggleButton in \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.\u0023\u003Dz4zWJLHVc016J[\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmsrqdWy7Nc9Tlw\u003D\u003D.GetGroupName(originalSource)])
      toggleButton.IsChecked = new bool?(toggleButton == originalSource);
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static RoutedEventHandler \u0023\u003DzD2PBJuOS\u0024ThI_8ILfw\u003D\u003D;
    public static RoutedEventHandler \u0023\u003DzMXfvpi5sJ2USDoqFIQ\u003D\u003D;
  }
}
