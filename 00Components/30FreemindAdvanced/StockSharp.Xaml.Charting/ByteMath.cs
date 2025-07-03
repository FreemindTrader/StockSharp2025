using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;


#nullable disable
internal sealed class ByteMath :
  IMath<byte>
{

    public byte MinValue => 0;


    public byte MaxValue => byte.MaxValue;


    public byte ZeroValue => 0;

    public byte Max( byte _param1, byte _param2 )
    {
        return ( int ) _param1 <= ( int ) _param2 ? _param2 : _param1;
    }

    public byte Min( byte _param1, byte _param2 )
    {
        return ( int ) _param1 >= ( int ) _param2 ? _param2 : _param1;
    }

    public byte MinGreaterThan( byte _param1, byte _param2, byte _param3 )
    {
        byte num = this.Min(_param2, _param3);
        byte koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( byte _param1 ) => false;

    public byte Subtract( byte _param1, byte _param2 )
    {
        return ( byte ) ( ( uint ) _param1 - ( uint ) _param2 );
    }

    public byte Abs( byte _param1 ) => _param1;

    public double ToDouble( byte _param1 ) => ( double ) _param1;

    public byte Mult( byte _param1, byte _param2 )
    {
        return ( byte ) ( ( uint ) _param1 * ( uint ) _param2 );
    }

    public byte Mult( byte _param1, double _param2 )
    {
        return ( byte ) ( ( double ) _param1 * _param2 );
    }

    public byte Add( byte _param1, byte _param2 )
    {
        return ( byte ) ( ( uint ) _param1 + ( uint ) _param2 );
    }

    public byte Inc( ref byte _param1 ) => ++_param1;

    public byte Dec( ref byte _param1 ) => --_param1;
}
