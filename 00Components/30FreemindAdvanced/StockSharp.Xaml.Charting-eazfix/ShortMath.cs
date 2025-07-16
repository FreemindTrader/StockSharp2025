using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;

#nullable disable
public sealed class ShortMath :
  IMath<short>
{

    public short MinValue => short.MinValue;


    public short MaxValue => short.MaxValue;


    public short ZeroValue => 0;

    public short Max( short _param1, short _param2 )
    {
        return ( int ) _param1 <= ( int ) _param2 ? _param2 : _param1;
    }

    public short Min( short _param1, short _param2 )
    {
        return ( int ) _param1 >= ( int ) _param2 ? _param2 : _param1;
    }

    public short MinGreaterThan( short _param1, short _param2, short _param3 )
    {
        short num = this.Min(_param2, _param3);
        short koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( short _param1 ) => false;

    public short Subtract( short _param1, short _param2 )
    {
        return ( short ) ( ( int ) _param1 - ( int ) _param2 );
    }

    public short Abs( short _param1 ) => Math.Abs( _param1 );

    public double ToDouble( short _param1 ) => ( double ) _param1;

    public short Mult( short _param1, short _param2 )
    {
        return ( short ) ( ( int ) _param1 * ( int ) _param2 );
    }

    public short Mult( short _param1, double _param2 )
    {
        return ( short ) ( ( double ) _param1 * _param2 );
    }

    public short Add( short _param1, short _param2 )
    {
        return ( short ) ( ( int ) _param1 + ( int ) _param2 );
    }

    public short Inc( ref short _param1 ) => ++_param1;

    public short Dec( ref short _param1 ) => --_param1;
}
