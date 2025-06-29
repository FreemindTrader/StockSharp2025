// Decompiled with JetBrains decompiler
// Type: -.dje_zTLE44YDSQ94G2KUCWUAX47SNF45Y8CQLMAP44RY8_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zTLE44YDSQ94G2KUCWUAX47SNF45Y8CQLMAP44RY8_ejd : FrameworkElement
{
  
  public static readonly DependencyProperty \u0023\u003Dzb9NlFA1HMDOO = DependencyProperty.RegisterAttached(XXX.SSS(-539441220), typeof (dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd), typeof (dje_zTLE44YDSQ94G2KUCWUAX47SNF45Y8CQLMAP44RY8_ejd), new PropertyMetadata((object) dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd.All, new PropertyChangedCallback(dje_zTLE44YDSQ94G2KUCWUAX47SNF45Y8CQLMAP44RY8_ejd.\u0023\u003DzWE_UfM17p3U9)));

  public static void SetVisibleIn(
    DependencyObject _param0,
    dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd _param1)
  {
    _param0.SetValue(dje_zTLE44YDSQ94G2KUCWUAX47SNF45Y8CQLMAP44RY8_ejd.\u0023\u003Dzb9NlFA1HMDOO, (object) _param1);
  }

  public static dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd GetVisibleIn(
    DependencyObject _param0)
  {
    return (dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd) _param0.GetValue(dje_zTLE44YDSQ94G2KUCWUAX47SNF45Y8CQLMAP44RY8_ejd.\u0023\u003Dzb9NlFA1HMDOO);
  }

  private static void \u0023\u003DzWE_UfM17p3U9(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    Visibility visibility = (dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd) _param1.NewValue == dje_zVHECNFY3M2WCG49XYA6NSQB8Z75WSE6LTSJYB7WK_ejd.Silverlight ? Visibility.Collapsed : Visibility.Visible;
    (_param0 as UIElement).Visibility = visibility;
  }
}
