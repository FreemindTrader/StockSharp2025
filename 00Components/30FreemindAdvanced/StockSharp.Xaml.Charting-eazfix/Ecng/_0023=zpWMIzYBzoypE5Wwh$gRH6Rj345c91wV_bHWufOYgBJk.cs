// Decompiled with JetBrains decompiler
// Type: #=zpWMIzYBzoypE5Wwh$gRH6Rj345c91wV_bHWufOYgBJkEIa$gxVFiFtn1t6a1SKrdXMUKfjlv0yjch$1C5KjERZk=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6Rj345c91wV_bHWufOYgBJkEIa\u0024gxVFiFtn1t6a1SKrdXMUKfjlv0yjch\u00241C5KjERZk\u003D : 
  ISpanInterpolator
{
  private ITransform \u0023\u003DzA6knWDzOHW8u;
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D \u0023\u003Dz_qJgG_H4iKXd;
  private \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D \u0023\u003DzCmfG6yExbxuh;

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6Rj345c91wV_bHWufOYgBJkEIa\u0024gxVFiFtn1t6a1SKrdXMUKfjlv0yjch\u00241C5KjERZk\u003D()
  {
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6Rj345c91wV_bHWufOYgBJkEIa\u0024gxVFiFtn1t6a1SKrdXMUKfjlv0yjch\u00241C5KjERZk\u003D(
    ITransform _param1)
  {
    this.\u0023\u003DzA6knWDzOHW8u = _param1;
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6Rj345c91wV_bHWufOYgBJkEIa\u0024gxVFiFtn1t6a1SKrdXMUKfjlv0yjch\u00241C5KjERZk\u003D(
    ITransform _param1,
    double _param2,
    double _param3,
    int _param4)
  {
    this.\u0023\u003DzA6knWDzOHW8u = _param1;
    this.\u0023\u003DzoLjFgpI\u003D(_param2, _param3, _param4);
  }

  public ITransform \u0023\u003DzRLWOoTmSHt4EDNlIQQ\u003D\u003D()
  {
    return this.\u0023\u003DzA6knWDzOHW8u;
  }

  public void \u0023\u003DzRLWOoTmSHt4EDNlIQQ\u003D\u003D(
    ITransform _param1)
  {
    this.\u0023\u003DzA6knWDzOHW8u = _param1;
  }

  public void \u0023\u003DzsdUKR\u0024YKO9R\u0024(out int _param1, out int _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzoLjFgpI\u003D(double _param1, double _param2, int _param3)
  {
    double num1 = _param1;
    double num2 = _param2;
    this.\u0023\u003DzA6knWDzOHW8u.\u0023\u003DzhA5n1D0\u003D(ref num1, ref num2);
    int num3 = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(num1 * 256.0);
    int num4 = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(num2 * 256.0);
    double num5 = _param1 + (double) _param3;
    double num6 = _param2;
    this.\u0023\u003DzA6knWDzOHW8u.\u0023\u003DzhA5n1D0\u003D(ref num5, ref num6);
    int num7 = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(num5 * 256.0);
    int num8 = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(num6 * 256.0);
    this.\u0023\u003Dz_qJgG_H4iKXd = new \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(num3, num7, _param3);
    this.\u0023\u003DzCmfG6yExbxuh = new \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(num4, num8, _param3);
  }

  public void \u0023\u003DzSOOEB2keHYTJkN1g7T7GOos\u003D(
    double _param1,
    double _param2,
    int _param3)
  {
    this.\u0023\u003DzA6knWDzOHW8u.\u0023\u003DzhA5n1D0\u003D(ref _param1, ref _param2);
    this.\u0023\u003Dz_qJgG_H4iKXd = new \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(this.\u0023\u003Dz_qJgG_H4iKXd.\u0023\u003Dzi8jDI4I\u003D(), agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param1 * 256.0), _param3);
    this.\u0023\u003DzCmfG6yExbxuh = new \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(this.\u0023\u003DzCmfG6yExbxuh.\u0023\u003Dzi8jDI4I\u003D(), agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param2 * 256.0), _param3);
  }

  public void \u0023\u003DzXiQrjbw\u003D()
  {
    this.\u0023\u003Dz_qJgG_H4iKXd.\u0023\u003DzXiQrjbw\u003D();
    this.\u0023\u003DzCmfG6yExbxuh.\u0023\u003DzXiQrjbw\u003D();
  }

  public void \u0023\u003Dz95kJcrQe23xpIQcxtQ\u003D\u003D(out int _param1, out int _param2)
  {
    _param1 = this.\u0023\u003Dz_qJgG_H4iKXd.\u0023\u003Dzi8jDI4I\u003D();
    _param2 = this.\u0023\u003DzCmfG6yExbxuh.\u0023\u003Dzi8jDI4I\u003D();
  }

  private enum \u0023\u003DzSnrYaSZuHuU7fhAKk8nNb1o\u003D
  {
  }
}
