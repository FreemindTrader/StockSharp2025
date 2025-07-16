// Decompiled with JetBrains decompiler
// Type: #=zRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_9VHGTxXzwywvTCxLE07GZsg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class Ellipse : 
  IVertexSource
{
  public double \u0023\u003Dzu3VuSd0\u003D;
  public double \u0023\u003DzzFs4Jmc\u003D;
  public double \u0023\u003DzIE9S2pQt9vrh;
  public double \u0023\u003DzcfwtWFDezFnt;
  private double \u0023\u003Dz9IbNApzCydlB;
  private int \u0023\u003DzTToRp9n429_k;
  private int \u0023\u003DzWiFSa2\u0024o8viR;
  private bool \u0023\u003DzZRnb1mfJfJVT;

  public Ellipse()
  {
    this.\u0023\u003Dzu3VuSd0\u003D = 0.0;
    this.\u0023\u003DzzFs4Jmc\u003D = 0.0;
    this.\u0023\u003DzIE9S2pQt9vrh = 1.0;
    this.\u0023\u003DzcfwtWFDezFnt = 1.0;
    this.\u0023\u003Dz9IbNApzCydlB = 1.0;
    this.\u0023\u003DzTToRp9n429_k = 4;
    this.\u0023\u003DzWiFSa2\u0024o8viR = 0;
    this.\u0023\u003DzZRnb1mfJfJVT = false;
  }

  public Ellipse(
    Vector2 _param1,
    double _param2)
    : this(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd, _param2, _param2)
  {
  }

  public Ellipse(
    Vector2 _param1,
    double _param2,
    double _param3,
    int _param4 = 0,
    bool _param5 = false)
    : this(_param1.dje_z3GBAX47U_ejd, _param1.dje_zLPL6EZPA_ejd, _param2, _param3, _param4, _param5)
  {
  }

  public Ellipse(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    int _param5 = 0,
    bool _param6 = false)
  {
    this.\u0023\u003Dzu3VuSd0\u003D = _param1;
    this.\u0023\u003DzzFs4Jmc\u003D = _param2;
    this.\u0023\u003DzIE9S2pQt9vrh = _param3;
    this.\u0023\u003DzcfwtWFDezFnt = _param4;
    this.\u0023\u003Dz9IbNApzCydlB = 1.0;
    this.\u0023\u003DzTToRp9n429_k = _param5;
    this.\u0023\u003DzWiFSa2\u0024o8viR = 0;
    this.\u0023\u003DzZRnb1mfJfJVT = _param6;
    if (this.\u0023\u003DzTToRp9n429_k != 0)
      return;
    this.\u0023\u003DzgZPyrKEmb\u0024lS2eI6EA\u003D\u003D();
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003Dz0gbwL\u00244\u003D(_param1, _param2, _param3, _param4, 0, false);
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    int _param5)
  {
    this.\u0023\u003Dz0gbwL\u00244\u003D(_param1, _param2, _param3, _param4, _param5, false);
  }

  public void \u0023\u003Dz0gbwL\u00244\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    int _param5,
    bool _param6)
  {
    this.\u0023\u003Dzu3VuSd0\u003D = _param1;
    this.\u0023\u003DzzFs4Jmc\u003D = _param2;
    this.\u0023\u003DzIE9S2pQt9vrh = _param3;
    this.\u0023\u003DzcfwtWFDezFnt = _param4;
    this.\u0023\u003DzTToRp9n429_k = _param5;
    this.\u0023\u003DzWiFSa2\u0024o8viR = 0;
    this.\u0023\u003DzZRnb1mfJfJVT = _param6;
    if (this.\u0023\u003DzTToRp9n429_k != 0)
      return;
    this.\u0023\u003DzgZPyrKEmb\u0024lS2eI6EA\u003D\u003D();
  }

  public void \u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT(double _param1)
  {
    this.\u0023\u003Dz9IbNApzCydlB = _param1;
    this.\u0023\u003DzgZPyrKEmb\u0024lS2eI6EA\u003D\u003D();
  }

  public void \u0023\u003DzVawdK5C5Lyf_(int _param1) => this.\u0023\u003DzWiFSa2\u0024o8viR = 0;

  public Path.\u0023\u003Dz9kUnn38\u003D \u0023\u003DzxfekdAs1X3YM(
    out double _param1,
    out double _param2)
  {
    _param1 = 0.0;
    _param2 = 0.0;
    if (this.\u0023\u003DzWiFSa2\u0024o8viR == this.\u0023\u003DzTToRp9n429_k)
    {
      ++this.\u0023\u003DzWiFSa2\u0024o8viR;
      return (Path.\u0023\u003Dz9kUnn38\u003D) 95;
    }
    if (this.\u0023\u003DzWiFSa2\u0024o8viR > this.\u0023\u003DzTToRp9n429_k)
      return (Path.\u0023\u003Dz9kUnn38\u003D) 0;
    double num = (double) this.\u0023\u003DzWiFSa2\u0024o8viR / (double) this.\u0023\u003DzTToRp9n429_k * 2.0 * Math.PI;
    if (this.\u0023\u003DzZRnb1mfJfJVT)
      num = 2.0 * Math.PI - num;
    _param1 = this.\u0023\u003Dzu3VuSd0\u003D + Math.Cos(num) * this.\u0023\u003DzIE9S2pQt9vrh;
    _param2 = this.\u0023\u003DzzFs4Jmc\u003D + Math.Sin(num) * this.\u0023\u003DzcfwtWFDezFnt;
    ++this.\u0023\u003DzWiFSa2\u0024o8viR;
    return this.\u0023\u003DzWiFSa2\u0024o8viR != 1 ? (Path.\u0023\u003Dz9kUnn38\u003D) 2 : (Path.\u0023\u003Dz9kUnn38\u003D) 1;
  }

  private void \u0023\u003DzgZPyrKEmb\u0024lS2eI6EA\u003D\u003D()
  {
    double num = (Math.Abs(this.\u0023\u003DzIE9S2pQt9vrh) + Math.Abs(this.\u0023\u003DzcfwtWFDezFnt)) / 2.0;
    this.\u0023\u003DzTToRp9n429_k = (int) Math.Round(2.0 * Math.PI / (Math.Acos(num / (num + 0.125 / this.\u0023\u003Dz9IbNApzCydlB)) * 2.0));
  }
}
