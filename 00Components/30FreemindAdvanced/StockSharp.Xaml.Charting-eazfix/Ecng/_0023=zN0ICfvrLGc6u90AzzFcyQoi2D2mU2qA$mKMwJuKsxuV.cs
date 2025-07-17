// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQoi2D2mU2qA$mKMwJuKsxuVlAOF_qEOCvYTfPE7CbzyQZw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class RoundedRect : 
  IVertexSource
{
  private RectangleDouble \u0023\u003DzXAIVgfY\u003D;
  private Vector2 \u0023\u003Dzw9UCUHkwOR2n;
  private Vector2 \u0023\u003Dz\u00240tutq\u0024KQH0o;
  private Vector2 \u0023\u003DzXjJvl0XncRte;
  private Vector2 \u0023\u003DzGbB75PmSkUJk;
  private int \u0023\u003DzvwpBT1s\u003D;
  private \u0023\u003DzQN2Zes8h9tElvYmX48o49LUqCmhVH6715qLutxVwykZRNhszysZjrqoQNirgMq_Duw\u003D\u003D \u0023\u003DzkyfhG2FmoVbJ = new \u0023\u003DzQN2Zes8h9tElvYmX48o49LUqCmhVH6715qLutxVwykZRNhszysZjrqoQNirgMq_Duw\u003D\u003D();

  public RoundedRect(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5)
  {
    this.\u0023\u003DzXAIVgfY\u003D = new RectangleDouble(_param1, _param2, _param3, _param4);
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd = _param5;
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd = _param5;
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd = _param5;
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd = _param5;
    this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd = _param5;
    this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd = _param5;
    this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd = _param5;
    this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd = _param5;
    if (_param1 > _param3)
    {
      this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzP4R7yU0\u003D = _param3;
      this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003Dzp55dtus\u003D = _param1;
    }
    if (_param2 <= _param4)
      return;
    this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzRNV_Dpk\u003D = _param4;
    this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzSzOWcj8\u003D = _param2;
  }

  public RoundedRect(
    RectangleDouble _param1,
    double _param2)
    : this(_param1.\u0023\u003DzP4R7yU0\u003D, _param1.\u0023\u003DzRNV_Dpk\u003D, _param1.\u0023\u003Dzp55dtus\u003D, _param1.\u0023\u003DzSzOWcj8\u003D, _param2)
  {
  }

  public RoundedRect(
    RectangleInt _param1,
    double _param2)
    : this((double) _param1.\u0023\u003DzP4R7yU0\u003D, (double) _param1.\u0023\u003DzRNV_Dpk\u003D, (double) _param1.\u0023\u003Dzp55dtus\u003D, (double) _param1.\u0023\u003DzSzOWcj8\u003D, _param2)
  {
  }

  public void \u0023\u003DzOIONaT0\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003DzXAIVgfY\u003D = new RectangleDouble(_param1, _param2, _param3, _param4);
    if (_param1 > _param3)
    {
      this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzP4R7yU0\u003D = _param3;
      this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003Dzp55dtus\u003D = _param1;
    }
    if (_param2 <= _param4)
      return;
    this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzRNV_Dpk\u003D = _param4;
    this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzSzOWcj8\u003D = _param2;
  }

  public void radius(double _param1)
  {
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd = this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd = this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd = this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd = this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd = this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd = this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd = this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd = _param1;
  }

  public void radius(double _param1, double _param2)
  {
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd = this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd = this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd = this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd = _param1;
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd = this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd = this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd = this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd = _param2;
  }

  public void radius(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003Dzw9UCUHkwOR2n = new Vector2(_param1, _param1);
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o = new Vector2(_param2, _param2);
    this.\u0023\u003DzXjJvl0XncRte = new Vector2(_param3, _param3);
    this.\u0023\u003DzGbB75PmSkUJk = new Vector2(_param4, _param4);
  }

  public void radius(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6,
    double _param7,
    double _param8)
  {
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd = _param1;
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd = _param2;
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd = _param3;
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd = _param4;
    this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd = _param5;
    this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd = _param6;
    this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd = _param7;
    this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd = _param8;
  }

  public void \u0023\u003DzSMJ53fQvuDbW11WY\u0024g\u003D\u003D()
  {
    double num1 = Math.Abs(this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzSzOWcj8\u003D - this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzRNV_Dpk\u003D);
    double num2 = Math.Abs(this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003Dzp55dtus\u003D - this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzP4R7yU0\u003D);
    double num3 = 1.0;
    double num4 = num1 / (this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd + this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd);
    if (num4 < num3)
      num3 = num4;
    double num5 = num1 / (this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd + this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd);
    if (num5 < num3)
      num3 = num5;
    double num6 = num2 / (this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd + this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd);
    if (num6 < num3)
      num3 = num6;
    double num7 = num2 / (this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd + this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd);
    if (num7 < num3)
      num3 = num7;
    if (num3 >= 1.0)
      return;
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd *= num3;
    this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd *= num3;
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd *= num3;
    this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd *= num3;
    this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd *= num3;
    this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd *= num3;
    this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd *= num3;
    this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd *= num3;
  }

  public void \u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT(double _param1)
  {
    this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT(_param1);
  }

  public double \u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT()
  {
    return this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003Dz5TglSMBX9ZRLd_7HBMzyncJUn7cT();
  }

  public void \u0023\u003DzVawdK5C5Lyf_(int _param1) => this.\u0023\u003DzvwpBT1s\u003D = 0;

  public Path.\u0023\u003Dz9kUnn38\u003D \u0023\u003DzxfekdAs1X3YM(
    out double _param1,
    out double _param2)
  {
    _param1 = 0.0;
    _param2 = 0.0;
    Path.\u0023\u003Dz9kUnn38\u003D z9kUnn38_1 = (Path.\u0023\u003Dz9kUnn38\u003D) 0;
    switch (this.\u0023\u003DzvwpBT1s\u003D)
    {
      case 0:
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003Dz0gbwL\u00244\u003D(this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzP4R7yU0\u003D + this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd, this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzRNV_Dpk\u003D + this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd, this.\u0023\u003Dzw9UCUHkwOR2n.dje_z3GBAX47U_ejd, this.\u0023\u003Dzw9UCUHkwOR2n.dje_zLPL6EZPA_ejd, Math.PI, 3.0 * Math.PI / 2.0);
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzVawdK5C5Lyf_(0);
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 1;
      case 1:
        Path.\u0023\u003Dz9kUnn38\u003D z9kUnn38_2 = this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzxfekdAs1X3YM(out _param1, out _param2);
        if (!Path.\u0023\u003DzVHztYKNVoUMf(z9kUnn38_2))
          return z9kUnn38_2;
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 2;
      case 2:
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003Dz0gbwL\u00244\u003D(this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003Dzp55dtus\u003D - this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd, this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzRNV_Dpk\u003D + this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd, this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_z3GBAX47U_ejd, this.\u0023\u003Dz\u00240tutq\u0024KQH0o.dje_zLPL6EZPA_ejd, 3.0 * Math.PI / 2.0, 0.0);
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzVawdK5C5Lyf_(0);
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 3;
      case 3:
        if (!Path.\u0023\u003DzVHztYKNVoUMf(this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzxfekdAs1X3YM(out _param1, out _param2)))
          return (Path.\u0023\u003Dz9kUnn38\u003D) 2;
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 4;
      case 4:
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003Dz0gbwL\u00244\u003D(this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003Dzp55dtus\u003D - this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd, this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzSzOWcj8\u003D - this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd, this.\u0023\u003DzXjJvl0XncRte.dje_z3GBAX47U_ejd, this.\u0023\u003DzXjJvl0XncRte.dje_zLPL6EZPA_ejd, 0.0, Math.PI / 2.0);
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzVawdK5C5Lyf_(0);
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 5;
      case 5:
        if (!Path.\u0023\u003DzVHztYKNVoUMf(this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzxfekdAs1X3YM(out _param1, out _param2)))
          return (Path.\u0023\u003Dz9kUnn38\u003D) 2;
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 6;
      case 6:
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003Dz0gbwL\u00244\u003D(this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzP4R7yU0\u003D + this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd, this.\u0023\u003DzXAIVgfY\u003D.\u0023\u003DzSzOWcj8\u003D - this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd, this.\u0023\u003DzGbB75PmSkUJk.dje_z3GBAX47U_ejd, this.\u0023\u003DzGbB75PmSkUJk.dje_zLPL6EZPA_ejd, Math.PI / 2.0, Math.PI);
        this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzVawdK5C5Lyf_(0);
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 7;
      case 7:
        if (!Path.\u0023\u003DzVHztYKNVoUMf(this.\u0023\u003DzkyfhG2FmoVbJ.\u0023\u003DzxfekdAs1X3YM(out _param1, out _param2)))
          return (Path.\u0023\u003Dz9kUnn38\u003D) 2;
        ++this.\u0023\u003DzvwpBT1s\u003D;
        goto case 8;
      case 8:
        z9kUnn38_1 = (Path.\u0023\u003Dz9kUnn38\u003D) 95;
        ++this.\u0023\u003DzvwpBT1s\u003D;
        break;
    }
    return z9kUnn38_1;
  }
}
