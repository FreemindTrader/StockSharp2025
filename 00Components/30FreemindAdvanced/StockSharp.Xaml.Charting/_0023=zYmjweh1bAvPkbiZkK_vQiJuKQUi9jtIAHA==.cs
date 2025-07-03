// Decompiled with JetBrains decompiler
// Type: #=zYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D : 
  \u0023\u003Dz5Gmvm1KtOlJYFOleRn5\u0024KYUiNQwyvdLt\u0024UF8gTY\u003D,
  IDisposable
{
  
  private readonly IRenderContext2D \u0023\u003DzVxwXLcXPtvCC;
  
  private readonly bool \u0023\u003DzCGVfeT7yJc5e;
  
  private readonly float \u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
  
  private readonly double \u0023\u003DzBGuos5a1\u0024vqwiW65Dw\u003D\u003D;
  
  private readonly Dictionary<(Color, float), \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J> \u0023\u003Dzin_8jbOXQl9a;
  
  private readonly Dictionary<Color, IBrush2D> \u0023\u003DzRedLM92UZkPp;
  
  private readonly Dictionary<Brush, IBrush2D> \u0023\u003Dz15ylege7nbS\u0024;
  
  private readonly double[] \u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D;

  public \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(
    IRenderContext2D _param1,
    bool _param2,
    float _param3,
    double _param4,
    double[] _param5 = null)
  {
    this.\u0023\u003DzVxwXLcXPtvCC = _param1;
    this.\u0023\u003DzCGVfeT7yJc5e = _param2;
    this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = _param3;
    this.\u0023\u003DzBGuos5a1\u0024vqwiW65Dw\u003D\u003D = _param4;
    this.\u0023\u003Dzin_8jbOXQl9a = new Dictionary<(Color, float), \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J>();
    this.\u0023\u003DzRedLM92UZkPp = new Dictionary<Color, IBrush2D>();
    this.\u0023\u003Dz15ylege7nbS\u0024 = new Dictionary<Brush, IBrush2D>();
    this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D = _param5;
  }

  public \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dzc8S9rSE\u003D(
    Color _param1,
    float? _param2 = null)
  {
    _param2.GetValueOrDefault();
    if (!_param2.HasValue)
      _param2 = new float?(this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D);
    (Color, float) key = (_param1, _param2.Value);
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J;
    if (this.\u0023\u003Dzin_8jbOXQl9a.TryGetValue(key, out rhwYsZxA33iRu6Id7J))
      return rhwYsZxA33iRu6Id7J;
    rhwYsZxA33iRu6Id7J = this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003DzL3In9ls\u003D(_param1, this.\u0023\u003DzCGVfeT7yJc5e, (float) ((double) _param2 ?? (double) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D), this.\u0023\u003DzBGuos5a1\u0024vqwiW65Dw\u003D\u003D, this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D, PenLineCap.Round);
    this.\u0023\u003Dzin_8jbOXQl9a.Add(key, rhwYsZxA33iRu6Id7J);
    return rhwYsZxA33iRu6Id7J;
  }

  public IBrush2D \u0023\u003DzNryPIU0\u003D(
    Color _param1)
  {
    IBrush2D xrgcdFbSdWgN9GcT8_1;
    if (this.\u0023\u003DzRedLM92UZkPp.TryGetValue(_param1, out xrgcdFbSdWgN9GcT8_1))
      return xrgcdFbSdWgN9GcT8_1;
    IBrush2D xrgcdFbSdWgN9GcT8_2 = this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dze8WyDhI\u003D(_param1, this.\u0023\u003DzBGuos5a1\u0024vqwiW65Dw\u003D\u003D, new bool?());
    this.\u0023\u003DzRedLM92UZkPp.Add(_param1, xrgcdFbSdWgN9GcT8_2);
    return xrgcdFbSdWgN9GcT8_2;
  }

  public IBrush2D \u0023\u003DzNryPIU0\u003D(
    Brush _param1)
  {
    IBrush2D xrgcdFbSdWgN9GcT8_1;
    if (this.\u0023\u003Dz15ylege7nbS\u0024.TryGetValue(_param1, out xrgcdFbSdWgN9GcT8_1))
      return xrgcdFbSdWgN9GcT8_1;
    IBrush2D xrgcdFbSdWgN9GcT8_2 = this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dze8WyDhI\u003D(_param1, 1.0, \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerPrimitive);
    this.\u0023\u003Dz15ylege7nbS\u0024.Add(_param1, xrgcdFbSdWgN9GcT8_2);
    return xrgcdFbSdWgN9GcT8_2;
  }

  public void Dispose()
  {
    foreach (IDisposable disposable in this.\u0023\u003Dzin_8jbOXQl9a.Values)
      disposable.Dispose();
    foreach (IDisposable disposable in this.\u0023\u003DzRedLM92UZkPp.Values)
      disposable.Dispose();
    foreach (IDisposable disposable in this.\u0023\u003Dz15ylege7nbS\u0024.Values)
      disposable.Dispose();
    this.\u0023\u003Dzin_8jbOXQl9a.Clear();
    this.\u0023\u003DzRedLM92UZkPp.Clear();
  }
}
