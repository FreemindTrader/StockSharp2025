// Decompiled with JetBrains decompiler
// Type: #=z11$2ZZHXfa65mwO6Nijb7f3v9RTWy9c8Wf3usYRgbGKk
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7f3v9RTWy9c8Wf3usYRgbGKk : 
  \u0023\u003DztYZOHWyeiGLm7MH\u0024MqDS9fJgMIjfkOcK7kdYTA2avPAE<float>
{
  [SpecialName]
  public float \u0023\u003DzNLkUKUcMn0yy() => float.MaxValue;

  [SpecialName]
  public float \u0023\u003Dzn4BdrXIKaI7E() => float.MinValue;

  [SpecialName]
  public float \u0023\u003DzsQvnxYLDT3j3() => 0.0f;

  public float \u0023\u003DzTOKoqZw\u003D(float _param1, float _param2)
  {
    return this.\u0023\u003DzeNpB9guo_tur(_param1) || !this.\u0023\u003DzeNpB9guo_tur(_param2) && (double) _param1 <= (double) _param2 ? _param2 : _param1;
  }

  private bool \u0023\u003DzutrFxOU\u003D(float _param1)
  {
    return !float.IsInfinity(_param1) && !float.IsNaN(_param1);
  }

  public float \u0023\u003DzRHWvkgM\u003D(float _param1, float _param2)
  {
    return this.\u0023\u003DzeNpB9guo_tur(_param1) || !this.\u0023\u003DzeNpB9guo_tur(_param2) && (double) _param1 >= (double) _param2 ? _param2 : _param1;
  }

  public float \u0023\u003DzBz1ADeDbIZML(float _param1, float _param2, float _param3)
  {
    float num = this.\u0023\u003DzRHWvkgM\u003D(_param2, _param3);
    float koqZw = this.\u0023\u003DzTOKoqZw\u003D(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool \u0023\u003DzeNpB9guo_tur(float _param1) => (double) _param1 != (double) _param1;

  public float \u0023\u003DzFXH4KOE\u003D(float _param1, float _param2) => _param1 - _param2;

  public float \u0023\u003DzyIGkTCg\u003D(float _param1) => Math.Abs(_param1);

  public double \u0023\u003Dzb9UCYbo\u003D(float _param1) => (double) _param1;

  public float \u0023\u003Dz9_a9j8HqDt0f(float _param1, float _param2) => _param1 * _param2;

  public float \u0023\u003Dz9_a9j8HqDt0f(float _param1, double _param2)
  {
    return _param1 * (float) _param2;
  }

  public float \u0023\u003Dz6wT8xpE\u003D(float _param1, float _param2) => _param1 + _param2;

  public float \u0023\u003DzS\u0024BuL6M\u003D(ref float _param1) => ++_param1;

  public float \u0023\u003DzpTVOY3k\u003D(ref float _param1) => --_param1;
}
