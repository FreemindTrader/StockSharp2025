// Decompiled with JetBrains decompiler
// Type: #=zVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D
{
  private int \u0023\u003DzJki7qxUGZqU9;
  private int \u0023\u003DzbVyNpJQNjSEM;
  private int \u0023\u003Dz5vlwISZbXSFA;
  private int \u0023\u003DzW3f38yQ_qtoF;
  private int \u0023\u003DzFfSb8y0\u003D;

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D()
  {
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(
    int _param1,
    int _param2,
    int _param3)
  {
    this.\u0023\u003DzJki7qxUGZqU9 = _param3 <= 0 ? 1 : _param3;
    this.\u0023\u003DzbVyNpJQNjSEM = (_param2 - _param1) / this.\u0023\u003DzJki7qxUGZqU9;
    this.\u0023\u003Dz5vlwISZbXSFA = (_param2 - _param1) % this.\u0023\u003DzJki7qxUGZqU9;
    this.\u0023\u003DzW3f38yQ_qtoF = this.\u0023\u003Dz5vlwISZbXSFA;
    this.\u0023\u003DzFfSb8y0\u003D = _param1;
    if (this.\u0023\u003DzW3f38yQ_qtoF <= 0)
    {
      this.\u0023\u003DzW3f38yQ_qtoF += _param3;
      this.\u0023\u003Dz5vlwISZbXSFA += _param3;
      --this.\u0023\u003DzbVyNpJQNjSEM;
    }
    this.\u0023\u003DzW3f38yQ_qtoF -= _param3;
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    this.\u0023\u003DzJki7qxUGZqU9 = _param3 <= 0 ? 1 : _param3;
    this.\u0023\u003DzbVyNpJQNjSEM = (_param2 - _param1) / this.\u0023\u003DzJki7qxUGZqU9;
    this.\u0023\u003Dz5vlwISZbXSFA = (_param2 - _param1) % this.\u0023\u003DzJki7qxUGZqU9;
    this.\u0023\u003DzW3f38yQ_qtoF = this.\u0023\u003Dz5vlwISZbXSFA;
    this.\u0023\u003DzFfSb8y0\u003D = _param1;
    if (this.\u0023\u003DzW3f38yQ_qtoF > 0)
      return;
    this.\u0023\u003DzW3f38yQ_qtoF += _param3;
    this.\u0023\u003Dz5vlwISZbXSFA += _param3;
    --this.\u0023\u003DzbVyNpJQNjSEM;
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpY66I7KBgFOrZE6TjZYGqzX9MyREyvpL0V3tgpQVgHh60tZDRpKxNBxb0ofxOw\u003D\u003D(
    int _param1,
    int _param2)
  {
    this.\u0023\u003DzJki7qxUGZqU9 = _param2 <= 0 ? 1 : _param2;
    this.\u0023\u003DzbVyNpJQNjSEM = _param1 / this.\u0023\u003DzJki7qxUGZqU9;
    this.\u0023\u003Dz5vlwISZbXSFA = _param1 % this.\u0023\u003DzJki7qxUGZqU9;
    this.\u0023\u003DzW3f38yQ_qtoF = this.\u0023\u003Dz5vlwISZbXSFA;
    this.\u0023\u003DzFfSb8y0\u003D = 0;
    if (this.\u0023\u003DzW3f38yQ_qtoF > 0)
      return;
    this.\u0023\u003DzW3f38yQ_qtoF += _param2;
    this.\u0023\u003Dz5vlwISZbXSFA += _param2;
    --this.\u0023\u003DzbVyNpJQNjSEM;
  }

  public void \u0023\u003DzXiQrjbw\u003D()
  {
    this.\u0023\u003DzW3f38yQ_qtoF += this.\u0023\u003Dz5vlwISZbXSFA;
    this.\u0023\u003DzFfSb8y0\u003D += this.\u0023\u003DzbVyNpJQNjSEM;
    if (this.\u0023\u003DzW3f38yQ_qtoF <= 0)
      return;
    this.\u0023\u003DzW3f38yQ_qtoF -= this.\u0023\u003DzJki7qxUGZqU9;
    ++this.\u0023\u003DzFfSb8y0\u003D;
  }

  public void \u0023\u003DzocM1nio\u003D()
  {
    if (this.\u0023\u003DzW3f38yQ_qtoF <= this.\u0023\u003Dz5vlwISZbXSFA)
    {
      this.\u0023\u003DzW3f38yQ_qtoF += this.\u0023\u003DzJki7qxUGZqU9;
      --this.\u0023\u003DzFfSb8y0\u003D;
    }
    this.\u0023\u003DzW3f38yQ_qtoF -= this.\u0023\u003Dz5vlwISZbXSFA;
    this.\u0023\u003DzFfSb8y0\u003D -= this.\u0023\u003DzbVyNpJQNjSEM;
  }

  public void \u0023\u003Dz49sA\u0024Ar4vyK1()
  {
    this.\u0023\u003DzW3f38yQ_qtoF -= this.\u0023\u003DzJki7qxUGZqU9;
  }

  public void \u0023\u003Dz0n2T82CuCx68aCbY3Q\u003D\u003D()
  {
    this.\u0023\u003DzW3f38yQ_qtoF += this.\u0023\u003DzJki7qxUGZqU9;
  }

  public int \u0023\u003DzkMgbJfY\u003D() => this.\u0023\u003DzW3f38yQ_qtoF;

  public int \u0023\u003DzKCVo7D4\u003D() => this.\u0023\u003Dz5vlwISZbXSFA;

  public int \u0023\u003DzMWJFGIs\u003D() => this.\u0023\u003DzbVyNpJQNjSEM;

  public int \u0023\u003Dzi8jDI4I\u003D() => this.\u0023\u003DzFfSb8y0\u003D;

  private enum \u0023\u003Dzx5Dxc37kfIzn
  {
  }
}
