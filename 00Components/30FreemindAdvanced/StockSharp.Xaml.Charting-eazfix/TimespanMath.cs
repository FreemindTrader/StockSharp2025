using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;

#nullable disable
internal sealed class TimespanMath :
  IMath<TimeSpan>
{
    
    public TimeSpan MinValue => TimeSpan.MinValue;

    
    public TimeSpan MaxValue => TimeSpan.MaxValue;

    
    public TimeSpan ZeroValue => TimeSpan.Zero;

    public TimeSpan Max( TimeSpan _param1, TimeSpan _param2 )
    {
        return _param1.Ticks <= _param2.Ticks ? _param2 : _param1;
    }

    public TimeSpan Min( TimeSpan _param1, TimeSpan _param2 )
    {
        return _param1.Ticks >= _param2.Ticks ? _param2 : _param1;
    }

    public TimeSpan MinGreaterThan( TimeSpan _param1, TimeSpan _param2, TimeSpan _param3 )
    {
        TimeSpan timeSpan = this.Min(_param2, _param3);
        TimeSpan koqZw = this.Max(_param2, _param3);
        return timeSpan.CompareTo( _param1 ) <= 0 ? koqZw : timeSpan;
    }

    public bool IsNaN( TimeSpan _param1 ) => false;

    public TimeSpan Subtract( TimeSpan _param1, TimeSpan _param2 )
    {
        return _param1 - _param2;
    }

    public TimeSpan Abs( TimeSpan _param1 ) => _param1;

    public double ToDouble( TimeSpan _param1 ) => ( double ) _param1.Ticks;

    public TimeSpan Mult( TimeSpan _param1, TimeSpan _param2 )
    {
        return new TimeSpan( _param1.Ticks * _param2.Ticks );
    }

    public TimeSpan Mult( TimeSpan _param1, double _param2 )
    {
        return new TimeSpan( ( long ) ( ( double ) _param1.Ticks * _param2 ) );
    }

    public TimeSpan Add( TimeSpan _param1, TimeSpan _param2 )
    {
        return new TimeSpan( _param1.Ticks + _param2.Ticks );
    }

    public TimeSpan Inc( ref TimeSpan _param1 )
    {
        return new TimeSpan( _param1.Ticks + 1L );
    }

    public TimeSpan Dec( ref TimeSpan _param1 )
    {
        return new TimeSpan( _param1.Ticks - 1L );
    }
}
