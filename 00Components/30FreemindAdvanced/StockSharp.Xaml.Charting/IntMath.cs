using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;

#nullable disable
internal sealed class IntMath :
  IMath<int>
{

    public int MaxValue => int.MaxValue;


    public int MinValue => int.MinValue;

    public int ZeroValue => 0;

    public int Max( int _param1, int _param2 )
    {
        return _param1 <= _param2 ? _param2 : _param1;
    }

    public int Min( int _param1, int _param2 )
    {
        return _param1 >= _param2 ? _param2 : _param1;
    }

    public int MinGreaterThan( int _param1, int _param2, int _param3 )
    {
        int num = this.Min(_param2, _param3);
        int koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( int _param1 ) => false;

    public int Subtract( int _param1, int _param2 ) => _param1 - _param2;

    public int Abs( int _param1 ) => Math.Abs( _param1 );

    public double ToDouble( int _param1 ) => ( double ) _param1;

    public int Mult( int _param1, int _param2 ) => _param1 * _param2;

    public int Mult( int _param1, double _param2 )
    {
        return ( int ) ( ( double ) _param1 * _param2 );
    }

    public int Add( int _param1, int _param2 ) => _param1 + _param2;

    public int Inc( ref int _param1 ) => ++_param1;

    public int Dec( ref int _param1 ) => --_param1;
}
