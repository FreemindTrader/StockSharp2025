using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;

#nullable disable
public sealed class LongMath :
  IMath<long>
{

    public long MaxValue => long.MaxValue;


    public long MinValue => long.MinValue;


    public long ZeroValue => 0;

    public long Max( long _param1, long _param2 )
    {
        return _param1 <= _param2 ? _param2 : _param1;
    }

    public long Min( long _param1, long _param2 )
    {
        return _param1 >= _param2 ? _param2 : _param1;
    }

    public long MinGreaterThan( long _param1, long _param2, long _param3 )
    {
        long num = this.Min(_param2, _param3);
        long koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( long _param1 ) => false;

    public long Subtract( long _param1, long _param2 ) => _param1 - _param2;

    public long Abs( long _param1 ) => Math.Abs( _param1 );

    public double ToDouble( long _param1 ) => ( double ) _param1;

    public long Mult( long _param1, long _param2 ) => _param1 * _param2;

    public long Mult( long _param1, double _param2 )
    {
        return ( long ) ( ( double ) _param1 * _param2 );
    }

    public long Add( long _param1, long _param2 ) => _param1 + _param2;

    public long Inc( ref long _param1 ) => ++_param1;

    public long Dec( ref long _param1 ) => --_param1;
}
