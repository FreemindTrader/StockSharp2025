using System;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;

#nullable disable
public sealed class SByteMath :
  IMath<sbyte>
{

    public sbyte MinValue => sbyte.MinValue;


    public sbyte MaxValue => sbyte.MaxValue;


    public sbyte ZeroValue => 0;

    public sbyte Max( sbyte _param1, sbyte _param2 )
    {
        return ( int ) _param1 <= ( int ) _param2 ? _param2 : _param1;
    }

    public sbyte Min( sbyte _param1, sbyte _param2 )
    {
        return ( int ) _param1 >= ( int ) _param2 ? _param2 : _param1;
    }

    public sbyte MinGreaterThan( sbyte _param1, sbyte _param2, sbyte _param3 )
    {
        sbyte num = this.Min(_param2, _param3);
        sbyte koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( sbyte _param1 ) => false;

    public sbyte Subtract( sbyte _param1, sbyte _param2 )
    {
        return ( sbyte ) ( ( int ) _param1 - ( int ) _param2 );
    }

    public sbyte Abs( sbyte _param1 ) => Math.Abs( _param1 );

    public double ToDouble( sbyte _param1 ) => ( double ) _param1;

    public sbyte Mult( sbyte _param1, sbyte _param2 )
    {
        return ( sbyte ) ( ( int ) _param1 * ( int ) _param2 );
    }

    public sbyte Mult( sbyte _param1, double _param2 )
    {
        return ( sbyte ) ( ( double ) _param1 * _param2 );
    }

    public sbyte Add( sbyte _param1, sbyte _param2 )
    {
        return ( sbyte ) ( ( int ) _param1 + ( int ) _param2 );
    }

    public sbyte Inc( ref sbyte _param1 ) => ++_param1;

    public sbyte Dec( ref sbyte _param1 ) => --_param1;

    public sbyte Div(sbyte lhs, sbyte rhs)
    {
        throw new NotImplementedException();
    }

    public sbyte FromDouble(double dValue)
    {
        throw new NotImplementedException();
    }
}
