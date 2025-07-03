// Decompiled with JetBrains decompiler
// Type: #=zFDK4fEILkMRswIjIg1$y3N4WH8Xv4Js0cFdhBIo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;

#nullable disable
internal static class \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3N4WH8Xv4Js0cFdhBIo\u003D
{
  internal static Point \u0023\u003Dz2xcanLolC38g(this Rect _param0, Point _param1)
  {
    double right = _param0.Right;
    double left = _param0.Left;
    double top = _param0.Top;
    double bottom = _param0.Bottom;
    _param1.X = _param1.X > right ? right : _param1.X;
    _param1.X = _param1.X < left ? left : _param1.X;
    _param1.Y = _param1.Y > bottom ? bottom : _param1.Y;
    _param1.Y = _param1.Y < top ? top : _param1.Y;
    return _param1;
  }

  internal static Rect \u0023\u003DzZqkixZQ\u003D(this Rect _param0, double _param1)
  {
    return new Rect(_param0.X - _param1, _param0.Y - _param1, _param0.Width + 2.0 * _param1, _param0.Height + 2.0 * _param1);
  }
}
