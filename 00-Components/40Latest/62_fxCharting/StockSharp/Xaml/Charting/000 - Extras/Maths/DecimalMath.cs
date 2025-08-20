using System;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;

#nullable disable
public sealed class DecimalMath :
  IMath<Decimal>
{

    public Decimal MinValue => Decimal.MinValue;


    public Decimal MaxValue => Decimal.MaxValue;


    public Decimal ZeroValue => 0M;

    public Decimal Max( Decimal _param1, Decimal _param2 )
    {
        return !( _param1 > _param2 ) ? _param2 : _param1;
    }

    public Decimal Min( Decimal _param1, Decimal _param2 )
    {
        return !( _param1 < _param2 ) ? _param2 : _param1;
    }

    public Decimal MinGreaterThan( Decimal _param1, Decimal _param2, Decimal _param3 )
    {
        Decimal num = this.Min(_param2, _param3);
        Decimal koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( Decimal _param1 ) => false;

    public Decimal Subtract( Decimal _param1, Decimal _param2 ) => _param1 - _param2;

    public Decimal Abs( Decimal _param1 ) => Math.Abs( _param1 );

    public double ToDouble( Decimal _param1 ) => ( double ) _param1;

    public Decimal Mult( Decimal _param1, Decimal _param2 ) => _param1 * _param2;

    public Decimal Mult( Decimal _param1, double _param2 )
    {
        return _param1 * ( Decimal ) _param2;
    }

    public Decimal Add( Decimal _param1, Decimal _param2 ) => _param1 + _param2;

    public Decimal Inc( ref Decimal _param1 ) => ++_param1;

    public Decimal Dec( ref Decimal _param1 ) => --_param1;

    public decimal Div(decimal lhs, decimal rhs)
    {
        throw new NotImplementedException();
    }

    public decimal FromDouble(double dValue)
    {
        throw new NotImplementedException();
    }
}
