using Ecng.Collections;
using Ecng.Common;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// For certain time period, we want to store the range of prices and the volume of trades at each price level.
/// 
/// TimeframeDataSegment indeed is kinda like a candle
///     1)  Candle is a collection of price over time period ( eg. 5 minutes ), It will have a max price, a min price, an open price and a close price and also volume
///         Candle is a collection of price information of the X-Axis
///         
///     2)  TimeframeDataSegment is a collection of price information <see cref="T:StockSharp.Messages.CandlePriceLevel"/> over a price range, It will have a max price, a min price, an open price and a close price and also volume
///         TimeframeDataSegment is a collection of price information of the Y-Axis
///         
/// The more volume we have at a certain price level, the stronger the support or resistance that price range can be.
/// </summary>
/// <param name="datetime"></param>
/// <param name="count"></param>

public sealed class TimeframeDataSegment( DateTime datetime, int count ) : IPoint
{
    public DateTime Time
    {
        get
        {
            return _dsTimeframe;
        }
    }

    public double HighPrice
    {
        get
        {
            return _highPrice;
        }

        set
        {
            _highPrice = value;
        }
    }

    public double LowPrice
    {
        get
        {
            return _lowPrice;
        }

        set
        {
            _lowPrice = value;
        }
    }

    public double? OpenPrice
    {
        get
        {
            return _openPrice;
        }

        set
        {
            _openPrice = value;
        }
    }


    public double ClosePrice
    {
        get
        {
            return _closePrice;
        }

        set
        {
            _closePrice = value;
        }
    }

    public int Count => _count;

    private readonly SyncObject _syncObject;
    private readonly Dictionary<double, CandlePriceLevel> _priceToCPL = new Dictionary<double, CandlePriceLevel>();
    private readonly Dictionary<double, (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal)> _priceStepToSortedPriceSegments = new Dictionary<double, (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, decimal)>();

    private readonly int _count            = count;
    private readonly DateTime _dsTimeframe = datetime;

    private double _highPrice              = double.MinValue;
    private double _lowPrice               = double.MaxValue;
    private double _closePrice;
    private double? _openPrice;


    public double X => ( double ) Time.Ticks;

    public double Y
    {
        get
        {
            return _priceToCPL.Count <= 0 ? double.NaN : ( HighPrice + LowPrice ) / 2.0;
        }

    }

    public void JoinCandlePriceLevel( double price, CandlePriceLevel cpl )
    {
        lock ( _syncObject )
        {
            if ( cpl.TotalVolume == 0M )
                return;

            if ( _priceToCPL.TryGetValue( price, out var _cpl ) )
            {
                _priceToCPL[price] = _cpl.Join( cpl );
            }
            else
            {
                _priceToCPL.Add( price, cpl );                
            }

            UpdatePriceSegement( price );
            ClearPriceLevels();
        }
    }

    public void SetCandlePriceLevel( double price, CandlePriceLevel cpl )
    {
        lock ( _syncObject )
        {
            if ( cpl.TotalVolume == 0M )
                return;            

            if ( _priceToCPL.TryGetValue( price, out var _cpl ) )
            {
                if ( _cpl.TotalVolume == cpl.TotalVolume )
                    return;

                _priceToCPL[price] = cpl;
            }
            else
                _priceToCPL.Add( price, cpl );

            UpdatePriceSegement( price );
            ClearPriceLevels();
        }
    }

    private void ClearPriceLevels()
    {
        _priceStepToSortedPriceSegments.Clear();
    }

    private void UpdatePriceSegement( double price )
    {
        if ( price < LowPrice )
            LowPrice = price;

        if ( price > HighPrice )
            HighPrice = price;

        double? openPrice = OpenPrice;
        openPrice.GetValueOrDefault();

        if ( !openPrice.HasValue )
            OpenPrice = price;

        ClosePrice = price;
    }

    private static double GetLowerBound( double price, double priceStep )
    {
        return Math.Floor( price / priceStep ) * priceStep;
    }

    public CandlePriceLevel GetCPLFromPriceStep( double price, double priceStep )
    {
        if ( priceStep <= 0.0 )
            throw new ArgumentOutOfRangeException( "priceStep", ( object ) priceStep, LocalizedStrings.InvalidValue );

        lock ( _syncObject )
        {
            if ( _priceStepToSortedPriceSegments.TryGetValue( priceStep, out var tuple ) )
            {
                return CollectionHelper.TryGetValue( tuple.Item2, GetLowerBound( price, priceStep ) );
            }

            var psLowerBound = GetLowerBound(price, priceStep);
            var psUpperBound = psLowerBound + priceStep;
            var cpl          = new CandlePriceLevel();
            cpl.Price        = ( Decimal ) price;


            foreach ( var _cpl in _priceToCPL.Where( l => l.Key >= psLowerBound && l.Key < psUpperBound ) )
            {
                cpl = ( _cpl.Value ).Join( cpl );
            }

            return cpl;
        }
    }

    public (KeyValuePair<double, CandlePriceLevel>[ ], Decimal) GetCPLVFromPriceStep( double priceStep )
    {
        if ( priceStep <= 0.0 )
            throw new ArgumentOutOfRangeException( "priceStep", ( object ) priceStep, LocalizedStrings.InvalidValue );

        lock ( _syncObject )
        {
            if ( _priceStepToSortedPriceSegments.TryGetValue( priceStep, out var tuple ) )
                return (tuple.Item1, tuple.Item3);

            Decimal totalVolume = 0M;

            var sortedPrice2CPLMap = new SortedDictionary<double, CandlePriceLevel>();

            foreach ( (double myPrice, CandlePriceLevel _cpl) in _priceToCPL )
            {

                double psLowerBound = GetLowerBound(myPrice, priceStep);

                if ( sortedPrice2CPLMap.TryGetValue( psLowerBound, out var cpl ) )
                {
                    sortedPrice2CPLMap[psLowerBound] = cpl.Join( _cpl );
                    totalVolume = MathHelper.Max( totalVolume,  cpl.TotalVolume +  _cpl.TotalVolume );
                }
                else
                {
                    sortedPrice2CPLMap[psLowerBound] = _cpl;
                    totalVolume = MathHelper.Max( totalVolume,  _cpl.TotalVolume );
                }
            }

            var sortedPrice2CPLArray = sortedPrice2CPLMap.ToArray<KeyValuePair<double, CandlePriceLevel>>();

            if ( _priceStepToSortedPriceSegments.Count == 5 )
                _priceStepToSortedPriceSegments.Clear();

            _priceStepToSortedPriceSegments[priceStep] = (sortedPrice2CPLArray, sortedPrice2CPLMap, totalVolume);

            return (sortedPrice2CPLArray, totalVolume);
        }
    }

    public static (double, double) MinMax( IEnumerable<TimeframeDataSegment> segments )
    {
        double minPrice = double.MaxValue;
        double maxPrice = double.MinValue;
        foreach ( TimeframeDataSegment segment in segments )
        {
            if ( segment.LowPrice < minPrice )
                minPrice = segment.LowPrice;
            if ( segment.HighPrice > maxPrice )
                maxPrice = segment.HighPrice;
        }
        return (minPrice, maxPrice);
    }
}
