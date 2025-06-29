// Decompiled with JetBrains decompiler
// Type: #=zuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b$y
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b\u0024y
{
  public static readonly DependencyProperty \u0023\u003DzQUfZszk\u003D = DependencyProperty.RegisterAttached("", typeof (bool), typeof (\u0023\u003DzuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b\u0024y), new PropertyMetadata((object) false, new PropertyChangedCallback(\u0023\u003DzuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b\u0024y.\u0023\u003Dzsw9pqJ9tAkiO)));

  private static void \u0023\u003Dzsw9pqJ9tAkiO(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is Freezable freezable) || !true.Equals(_param1.NewValue) || !freezable.CanFreeze)
      return;
    freezable.Freeze();
  }

  public static void SetFreeze(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(\u0023\u003DzuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b\u0024y.\u0023\u003DzQUfZszk\u003D, (object) _param1);
  }

  public static bool GetFreeze(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(\u0023\u003DzuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b\u0024y.\u0023\u003DzQUfZszk\u003D);
  }
}
