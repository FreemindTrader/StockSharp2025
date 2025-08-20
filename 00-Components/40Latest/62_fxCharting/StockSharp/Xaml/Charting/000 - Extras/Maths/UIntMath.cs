using System;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;


#nullable disable
public sealed class UIntMath : IMath<uint>
{

    public uint MinValue => 0;


    public uint MaxValue => uint.MaxValue;


    public uint ZeroValue => 0;

    public uint Max( uint _param1, uint _param2 )
    {
        return _param1 <= _param2 ? _param2 : _param1;
    }

    public uint Min( uint _param1, uint _param2 )
    {
        return _param1 >= _param2 ? _param2 : _param1;
    }

    public uint MinGreaterThan( uint _param1, uint _param2, uint _param3 )
    {
        uint num = this.Min(_param2, _param3);
        uint koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( uint _param1 ) => false;

    public uint Subtract( uint _param1, uint _param2 ) => _param1 - _param2;

    public uint Abs( uint _param1 ) => _param1;

    public double ToDouble( uint _param1 ) => ( double ) _param1;

    public uint Mult( uint _param1, uint _param2 ) => _param1 * _param2;

    public uint Mult( uint _param1, double _param2 )
    {
        return ( uint ) ( ( double ) _param1 * _param2 );
    }

    public uint Add( uint _param1, uint _param2 ) => _param1 + _param2;

    public uint Inc( ref uint _param1 ) => ++_param1;

    public uint Dec( ref uint _param1 ) => --_param1;

    public uint Div(uint lhs, uint rhs)
    {
        throw new NotImplementedException();
    }

    public uint FromDouble(double dValue)
    {
        throw new NotImplementedException();
    }
}
