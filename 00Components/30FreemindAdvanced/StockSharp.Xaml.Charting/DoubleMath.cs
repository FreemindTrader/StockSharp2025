using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;

#nullable disable
internal sealed class DoubleMath :
  IMath<double>
{

    public double MaxValue => double.MaxValue;


    public double MinValue => double.MinValue;


    public double ZeroValue => 0.0;

    public double Max( double _param1, double _param2 )
    {
        return _param1.IsNaN() || !_param2.IsNaN() && _param1 <= _param2 ? _param2 : _param1;
    }

    public double Min( double _param1, double _param2 )
    {
        return _param1.IsNaN() || !_param2.IsNaN() && _param1 >= _param2 ? _param2 : _param1;
    }

    public double MinGreaterThan( double _param1, double _param2, double _param3 )
    {
        double num = this.Min(_param2, _param3);
        double koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( double _param1 ) => _param1.IsNaN();

    public double Subtract( double _param1, double _param2 ) => _param1 - _param2;

    public double Abs( double _param1 ) => Math.Abs( _param1 );

    public double ToDouble( double _param1 ) => _param1;

    public double Mult( double _param1, double _param2 ) => _param1 * _param2;

    public double Add( double _param1, double _param2 ) => _param1 + _param2;

    public double Inc( ref double _param1 ) => ++_param1;

    public double Dec( ref double _param1 ) => --_param1;
}
