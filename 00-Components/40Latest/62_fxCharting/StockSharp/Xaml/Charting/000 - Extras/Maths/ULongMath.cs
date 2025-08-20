using System;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;

#nullable disable
public sealed class ULongMath : 
  IMath<ulong>
{
  public ulong MinValue => 0;

  public ulong MaxValue => ulong.MaxValue;

  public ulong ZeroValue => 0;

  public ulong Max(ulong _param1, ulong _param2)
  {
    return _param1 <= _param2 ? _param2 : _param1;
  }

  public ulong Min(ulong _param1, ulong _param2)
  {
    return _param1 >= _param2 ? _param2 : _param1;
  }

  public ulong MinGreaterThan(ulong _param1, ulong _param2, ulong _param3)
  {
    ulong num = this.Min(_param2, _param3);
    ulong koqZw = this.Max(_param2, _param3);
    return num.CompareTo(_param1) <= 0 ? koqZw : num;
  }

  public bool IsNaN(ulong _param1) => false;

  public ulong Subtract(ulong _param1, ulong _param2) => _param1 - _param2;

  public ulong Abs(ulong _param1) => _param1;

  public double ToDouble(ulong _param1) => (double) _param1;

  public ulong Mult(ulong _param1, ulong _param2) => _param1 * _param2;

  public ulong Mult(ulong _param1, double _param2)
  {
    return (ulong) ((double) _param1 * _param2);
  }

  public ulong Add(ulong _param1, ulong _param2) => _param1 + _param2;

  public ulong Inc(ref ulong _param1) => ++_param1;

  public ulong Dec(ref ulong _param1) => --_param1;

    public ulong Div(ulong lhs, ulong rhs)
    {
        throw new NotImplementedException();
    }

    public ulong FromDouble(double dValue)
    {
        throw new NotImplementedException();
    }
}
