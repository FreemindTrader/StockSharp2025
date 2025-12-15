using Ecng.Collections;
using Ecng.Common;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockSharp.Xaml.Charting;

#nullable disable
internal sealed class TransactionDataSegment : IPoint
{
    private readonly SciList<ChartDrawData.sTrade> _allTrades = new SciList<ChartDrawData.sTrade>(1);
    private double maxValue = double.NaN;
    private double minValue = double.NaN;
    private readonly DateTime _transTimeframe;

    public TransactionDataSegment( DateTime _param1 )
    {
        _transTimeframe = _param1;
    }

    public bool NoTransacations => _allTrades.Count == 0;

    public IEnumerable<ChartDrawData.sTrade> AllTransactions => _allTrades;
    public DateTime Time => _transTimeframe;


    public double MaxValue( )
    {
        UpdateMinMax( );
        return maxValue;
    }

    public double MinValue( )
    {
        UpdateMinMax( );
        return minValue;
    }

    [SpecialName]
    public double X => ( double ) Time.Ticks;

    [SpecialName]
    public double Y => NoTransacations ? double.NaN : ( minValue + maxValue ) / 2.0;


    private void UpdateMinMax( )
    {
        if ( !MathHelper.IsNaN( maxValue ) )
            return;
        double min = double.MinValue;
        double max = double.MaxValue;
        using ( SciList<ChartDrawData.sTrade>.Enumerator interator = _allTrades.GetEnumerator( ) )
        {
            while ( interator.MoveNext( ) )
            {
                ChartDrawData.sTrade current = interator.Current;
                if ( current.Price < max )
                    max = current.Price;
                if ( current.Price > min )
                    min = current.Price;
            }
        }
        maxValue = max;
        minValue = min;
    }

    private void Reset( )
    {
        maxValue = minValue = double.NaN;
    }

    public void AddOrUpdate( ChartDrawData.sTrade myTrade )
    {        
        int index = CollectionHelper.IndexOf( _allTrades, s => s.GetTransactionString( ) == myTrade.GetTransactionString( ));
        if ( index < 0 )
            _allTrades.Add( myTrade );
        else
            _allTrades[index] = myTrade;
        Reset( );
    }

    public ChartDrawData.sTrade GetClosestTrade( double price, double maxDiff )
    {
        if ( maxDiff < 0.0 )
            throw new ArgumentException( "maxDiff must be non-negative" );
        if ( _allTrades.Count == 0 )
            return ( ChartDrawData.sTrade ) null;
        UpdateMinMax( );
        double max = double.MaxValue;
        ChartDrawData.sTrade desiredTrade = (ChartDrawData.sTrade) null;
        using ( SciList<ChartDrawData.sTrade>.Enumerator myTrades = _allTrades.GetEnumerator( ) )
        {
            while ( myTrades.MoveNext( ) )
            {
                ChartDrawData.sTrade current = myTrades.Current;
                double diff = Math.Abs(current.Price - price);
                if ( diff < max )
                {
                    max = diff;
                    desiredTrade = current;
                }
            }
        }
        return max > maxDiff ? ( ChartDrawData.sTrade ) null : desiredTrade;
    }

    public static void MinMax( IEnumerable<TransactionDataSegment> dsList, out double minimum, out double maximum )
    {
        minimum = double.MaxValue;
        maximum = double.MinValue;

        foreach ( var ds in dsList.Where(  p => p != null ) )
        {
            if ( ds.MaxValue( ) < minimum )
                minimum = ds.MaxValue( );
            if ( ds.MinValue( ) > maximum )
                maximum = ds.MinValue( );
        }
    }        
}
