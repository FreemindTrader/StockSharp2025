using System;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;

#nullable disable
public sealed class DateTimeMath : IMath<DateTime>
{
    
    public DateTime MinValue => DateTime.MinValue;

    
    public DateTime MaxValue => DateTime.MaxValue;

    
    public DateTime ZeroValue => DateTime.MinValue;

    public DateTime Max( DateTime _param1, DateTime _param2 )
    {
        return _param1.Ticks <= _param2.Ticks ? _param2 : _param1;
    }

    public DateTime Min( DateTime _param1, DateTime _param2 )
    {
        return _param1.Ticks >= _param2.Ticks ? _param2 : _param1;
    }

    public DateTime MinGreaterThan( DateTime _param1, DateTime _param2, DateTime _param3 )
    {
        DateTime dateTime = this.Min(_param2, _param3);
        DateTime koqZw = this.Max(_param2, _param3);
        return dateTime.CompareTo( _param1 ) <= 0 ? koqZw : dateTime;
    }

    public bool IsNaN( DateTime _param1 ) => false;

    public DateTime Subtract( DateTime _param1, DateTime _param2 )
    {
        return new DateTime( _param1.Ticks - _param2.Ticks );
    }

    public DateTime Abs( DateTime _param1 ) => _param1;

    public double ToDouble( DateTime _param1 ) => ( double ) _param1.Ticks;

    public DateTime Mult( DateTime _param1, DateTime _param2 )
    {
        return new DateTime( _param1.Ticks * _param2.Ticks );
    }

    public DateTime Mult( DateTime _param1, double _param2 )
    {
        return new DateTime( ( long ) ( ( double ) _param1.Ticks * _param2 ) );
    }

    public DateTime Add( DateTime _param1, DateTime _param2 )
    {
        return new DateTime( _param1.Ticks + _param2.Ticks );
    }

    public DateTime Inc( ref DateTime _param1 )
    {
        return new DateTime( _param1.Ticks + 1L );
    }

    public DateTime Dec( ref DateTime _param1 )
    {
        return new DateTime( _param1.Ticks - 1L );
    }

    public DateTime Div(DateTime lhs, DateTime rhs)
    {
        throw new NotImplementedException();
    }

    public DateTime FromDouble(double dValue)
    {
        throw new NotImplementedException();
    }
}
