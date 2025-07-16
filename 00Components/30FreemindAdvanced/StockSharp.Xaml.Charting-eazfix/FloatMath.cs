// Decompiled with JetBrains decompiler
// Type: #=z11$2ZZHXfa65mwO6Nijb7f3v9RTWy9c8Wf3usYRgbGKk
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
namespace SciChart.Data.Numerics.GenericMath;

#nullable disable
public sealed class FloatMath : 
  IMath<float>
{
  
  public float MaxValue => float.MaxValue;

  
  public float MinValue => float.MinValue;

  
  public float ZeroValue => 0.0f;

  public float Max(float _param1, float _param2)
  {
    return this.IsNaN(_param1) || !this.IsNaN(_param2) && (double) _param1 <= (double) _param2 ? _param2 : _param1;
  }

  private bool IsDefined(float _param1)
  {
    return !float.IsInfinity(_param1) && !float.IsNaN(_param1);
  }

  public float Min(float _param1, float _param2)
  {
    return this.IsNaN(_param1) || !this.IsNaN(_param2) && (double) _param1 >= (double) _param2 ? _param2 : _param1;
  }

  public float MinGreaterThan(float _param1, float _param2, float _param3)
  {
    float num = this.Min(_param2, _param3);
    float koqZw = this.Max(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool IsNaN(float _param1) => (double) _param1 != (double) _param1;

  public float Subtract(float _param1, float _param2) => _param1 - _param2;

  public float Abs(float _param1) => Math.Abs(_param1);

  public double ToDouble(float _param1) => (double) _param1;

  public float Mult(float _param1, float _param2) => _param1 * _param2;

  public float Mult(float _param1, double _param2)
  {
    return _param1 * (float) _param2;
  }

  public float Add(float _param1, float _param2) => _param1 + _param2;

  public float Inc(ref float _param1) => ++_param1;

  public float Dec(ref float _param1) => --_param1;
}
