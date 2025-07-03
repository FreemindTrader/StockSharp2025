namespace SciChart.Data.Numerics.GenericMath;


#nullable disable
internal sealed class UShortMath :
  IMath<ushort>
{    
    public ushort MinValue => 0;

    public ushort MaxValue => ushort.MaxValue;

    public ushort ZeroValue => 0;

    public ushort Max( ushort _param1, ushort _param2 )
    {
        return ( int ) _param1 <= ( int ) _param2 ? _param2 : _param1;
    }

    public ushort Min( ushort _param1, ushort _param2 )
    {
        return ( int ) _param1 >= ( int ) _param2 ? _param2 : _param1;
    }

    public ushort MinGreaterThan( ushort _param1, ushort _param2, ushort _param3 )
    {
        ushort num = this.Min(_param2, _param3);
        ushort koqZw = this.Max(_param2, _param3);
        return num.CompareTo( _param1 ) <= 0 ? koqZw : num;
    }

    public bool IsNaN( ushort _param1 ) => false;

    public ushort Subtract( ushort _param1, ushort _param2 )
    {
        return ( ushort ) ( ( uint ) _param1 - ( uint ) _param2 );
    }

    public ushort Abs( ushort _param1 ) => _param1;

    public double ToDouble( ushort _param1 ) => ( double ) _param1;

    public ushort Mult( ushort _param1, ushort _param2 )
    {
        return ( ushort ) ( ( uint ) _param1 * ( uint ) _param2 );
    }

    public ushort Mult( ushort _param1, double _param2 )
    {
        return ( ushort ) ( ( double ) _param1 * _param2 );
    }

    public ushort Add( ushort _param1, ushort _param2 )
    {
        return ( ushort ) ( ( uint ) _param1 + ( uint ) _param2 );
    }

    public ushort Inc( ref ushort _param1 ) => ++_param1;

    public ushort Dec( ref ushort _param1 ) => --_param1;
}
