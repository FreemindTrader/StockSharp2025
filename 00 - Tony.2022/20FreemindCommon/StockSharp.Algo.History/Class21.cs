using StockSharp.Algo;
using StockSharp.Algo.History.Russian.Rts;
using StockSharp.BusinessEntities;
using System;

internal abstract class Class21
{
    private readonly RtsHistorySource rtsHistorySource_0;
    private string string_0;
    private ExchangeBoard exchangeBoard_0;
    private bool bool_0;

    protected Class21( RtsHistorySource rtsHistorySource_1 )
    {
        RtsHistorySource rtsHistorySource = rtsHistorySource_1;
        if( rtsHistorySource == null )
        {
            throw new ArgumentNullException( "source" );
        }

        this.rtsHistorySource_0 = rtsHistorySource;
    }

    protected RtsHistorySource method_0( )
    {
        return this.rtsHistorySource_0;
    }

    protected SecurityIdGenerator method_1( )
    {
        return this.method_0( ).SecurityIdGenerator;
    }

    protected TimeZoneInfo method_2( )
    {
        return this.method_0( ).TimeZone;
    }

    public string method_3( )
    {
        return this.string_0;
    }

    public void method_4( string string_1 )
    {
        string str = string_1;
        if( str == null )
        {
            throw new ArgumentNullException( "value" );
        }

        this.string_0 = str;
    }

    public ExchangeBoard method_5( )
    {
        return this.exchangeBoard_0;
    }

    public void method_6( ExchangeBoard exchangeBoard_1 )
    {
        this.exchangeBoard_0 = exchangeBoard_1;
    }

    public bool method_7( )
    {
        return this.bool_0;
    }

    public void method_8( bool bool_1 )
    {
        this.bool_0 = bool_1;
    }
}
