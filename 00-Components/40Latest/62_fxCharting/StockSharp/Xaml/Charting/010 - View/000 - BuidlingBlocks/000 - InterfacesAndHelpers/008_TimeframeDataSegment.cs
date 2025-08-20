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
/// The more volume we have at a certain price level, the stronger the support or resistance that price range can be.
/// </summary>
/// <param name="datetime"></param>
/// <param name="count"></param>
public sealed class TimeframeDataSegment(DateTime datetime, int count) : IPoint
{
    public DateTime Time
    {
        get
        {
            return _dsTimeframe;
        }
    }

    public double MaxPrice
    {
        get
        {
            return _maxPrice;
        }

        set
        {
            _maxPrice = value;
        }
    }

    public double MinPrice
    {
        get
        {
            return _minPrice;
        }

        set
        {
            _minPrice = value;
        }
    }

    public int Count => _count;

    private readonly SyncObject _syncObject;
    private readonly Dictionary<double, CandlePriceLevel> _price2CandlePriceLevel;
    private readonly Dictionary<double, (KeyValuePair<double, CandlePriceLevel>[], SortedDictionary<double, CandlePriceLevel>, Decimal)> _complexCandlePriceLevel;
    private readonly int _count = count;
    private readonly DateTime _dsTimeframe = datetime;

    private double _maxPrice = double.MinValue;
    private double _minPrice = double.MaxValue;
    private double _lastPrice;
    private double? _firstPrice;



    


    public double? FirstPrice
    {
        get
        {
            return _firstPrice;
        }

        set
        {
            _firstPrice = value;
        }
    }


    public double LastPrice
    {
        get
        {
            return _lastPrice;
        }
        
        set
        {
            _lastPrice = value;
        }
    }

        
    public double X => (double)Time.Ticks;
    
    public double Y
    {
        get
        {
            return _price2CandlePriceLevel.Count <= 0 ? double.NaN : ( MaxPrice + MinPrice ) / 2.0;
        }

    }

    public void AddOrUpdatePriceLevel(double price, CandlePriceLevel priceLevel)
    {
        lock ( _syncObject )
        {
            if ( priceLevel.TotalVolume == 0M )
                return;
            CandlePriceLevel candlePriceLevel;
            if ( _price2CandlePriceLevel.TryGetValue(price, out candlePriceLevel) )
            {
                _price2CandlePriceLevel[price] = ( (CandlePriceLevel) candlePriceLevel ).Join(priceLevel);
            }
            else
            {
                _price2CandlePriceLevel.Add(price, priceLevel);
                UpdatePriceSegement(price);
            }
            ClearPriceLevels();
        }
    }

    public void SetPriceLevel(double price, CandlePriceLevel priceLvl)
    {
        lock ( _syncObject )
        {
            if ( priceLvl.TotalVolume == 0M )
                return;

            CandlePriceLevel candlePriceLevel;
            
            if ( _price2CandlePriceLevel.TryGetValue(price, out candlePriceLevel) )
            {
                if ( ( (CandlePriceLevel) candlePriceLevel ).TotalVolume == ( (CandlePriceLevel) priceLvl ).TotalVolume )
                    return;
                _price2CandlePriceLevel[price] = priceLvl;
            }
            else
                _price2CandlePriceLevel.Add(price, priceLvl);
            UpdatePriceSegement(price);
            ClearPriceLevels();
        }
    }

    private void ClearPriceLevels()
    {
        _complexCandlePriceLevel.Clear();
    }

    private void UpdatePriceSegement(double price)
    {
        if ( price < MinPrice )
            MinPrice = ( price );
        if ( price > MaxPrice )
            MaxPrice = ( price );

        double? first = FirstPrice;
        first.GetValueOrDefault();
        
        if ( !first.HasValue )
            FirstPrice = (new double?(price));
        LastPrice = price;
    }

    private static double GetQuotient(double number, double divisible)
    {
        return Math.Floor(number / divisible) * divisible;
    }

    public CandlePriceLevel GetCandlePriceLevel(double price, double priceStep)
    {
        if ( priceStep <= 0.0 )
            throw new ArgumentOutOfRangeException("priceStep", (object)priceStep, LocalizedStrings.InvalidValue);

        lock ( _syncObject )
        {                        
            if ( _complexCandlePriceLevel.TryGetValue(priceStep, out var tuple) )
            {
                return CollectionHelper.TryGetValue( tuple.Item2, GetQuotient(price, priceStep));
            }
                
            var quotient = GetQuotient(price, priceStep);
            var nextQuotient = quotient + priceStep;
            CandlePriceLevel pl = new CandlePriceLevel();
            pl.Price = (Decimal)price;
            
            
            foreach ( var priceLvls in _price2CandlePriceLevel.Where( l => l.Key >= quotient && l.Key < nextQuotient ) )
            {                
                pl = ( priceLvls.Value ).Join(pl);
            }

            return pl;
        }
    }

    public (KeyValuePair<double, CandlePriceLevel>[], Decimal) GetPriceLevelsAndVolume(double priceStep)
    {
        if ( priceStep <= 0.0 )
            throw new ArgumentOutOfRangeException("priceStep", (object)priceStep, LocalizedStrings.InvalidValue);
        
        lock ( _syncObject )
        {            
            if ( _complexCandlePriceLevel.TryGetValue(priceStep, out var tuple) )
                return (tuple.Item1, tuple.Item3);
            
            Decimal myVolume = 0M;

            var newPriceLevels = new SortedDictionary<double, CandlePriceLevel>();
            
            foreach ( (double myPrice, CandlePriceLevel level) in _price2CandlePriceLevel )
            {
                
                double myQuotient = GetQuotient(myPrice, priceStep);                

                if ( newPriceLevels.TryGetValue(myQuotient, out var candleLevels) )
                {
                    newPriceLevels[myQuotient] = candleLevels.Join(level);
                    myVolume = MathHelper.Max(myVolume, ( (CandlePriceLevel)candleLevels ).TotalVolume + ( (CandlePriceLevel)level ).TotalVolume);
                }
                else
                {
                    newPriceLevels[myQuotient] = level;
                    myVolume = MathHelper.Max(myVolume, ( (CandlePriceLevel) level ).TotalVolume);
                }
            }

            var priceLevelsArrary = newPriceLevels.ToArray<KeyValuePair<double, CandlePriceLevel>>();

            if ( _complexCandlePriceLevel.Count == 5 )
                _complexCandlePriceLevel.Clear();
            
            _complexCandlePriceLevel[priceStep] = (priceLevelsArrary, newPriceLevels, myVolume);
            
            return (priceLevelsArrary, myVolume);
        }
    }

    public static (double, double) GetRange(IEnumerable<TimeframeDataSegment> segments)
    {
        double minPrice = double.MaxValue;
        double maxPrice = double.MinValue;
        foreach ( TimeframeDataSegment segment in segments )
        {
            if ( segment.MinPrice < minPrice )
                minPrice = segment.MinPrice;
            if ( segment.MaxPrice > maxPrice )
                maxPrice = segment.MaxPrice;
        }
        return (minPrice, maxPrice);
    }    
}
