using System;
using System.Collections.Generic;
using System.Linq;
namespace fx.Xaml.Charting
{
    public class TimeframeDataSegment : IPoint
    {
        private readonly UltraList<TimeframeDataSegment.PriceLevel> _levels = new UltraList<TimeframeDataSegment.PriceLevel>(4);

        private double _minPrice;

        private double _maxPrice;

        public IEnumerable<double> AllPrices
        {
            get
            {
                return
                    from l in _levels
                    select l.Price;
            }
        }

        public int Index
        {
            get;
        }

        private bool IsEmtpy
        {
            get
            {
                return _levels.Count == 0;
            }
        }

        public int MaxDigits
        {
            get;
            private set;
        }

        public double MaxPrice
        {
            get
            {
                return _maxPrice;
            }
        }

        public long MaxValue
        {
            get;
            private set;
        }

        public double MinPrice
        {
            get
            {
                return _minPrice;
            }
        }

        public double PriceStep
        {
            get;
        }

        public DateTime Time
        {
            get;
        }

        public IEnumerable<TimeframeDataSegment.PriceLevel> Values
        {
            get
            {
                return _levels;
            }
        }

        public double X
        {
            get
            {
                return ( double ) Time.Ticks;
            }
        }

        public double Y
        {
            get
            {
                if ( IsEmtpy )
                {
                    return double.NaN;
                }
                return ( _maxPrice + _minPrice ) / 2;
            }
        }

        public TimeframeDataSegment( DateTime time, double priceStep, int index )
        {
            if ( time.Second != 0 || time.Millisecond != 0 )
            {
                throw new InvalidOperationException( "invalid time" );
            }
            Time = time;
            PriceStep = priceStep;
            Index = index;
            _minPrice = double.MaxValue;
            _maxPrice = double.MinValue;
        }

        public void AddPoint( double price, long volume )
        {
            if ( volume == 0 )
            {
                return;
            }
            price = price.NormalizePrice( PriceStep );
            TimeframeDataSegment.PriceLevel priceLevel = GetPriceLevel(price);
            priceLevel.AddValue( volume );
            if ( priceLevel.Value > MaxValue )
            {
                MaxValue = priceLevel.Value;
                MaxDigits = priceLevel.Digits;
            }
        }

        private TimeframeDataSegment.PriceLevel GetPriceLevel( double normPrice )
        {
            if ( normPrice < MinPrice )
            {
                _minPrice = normPrice;
            }
            if ( normPrice > MaxPrice )
            {
                _maxPrice = normPrice;
            }
            int num = 1 + (int)Math.Round((MaxPrice - MinPrice) / PriceStep);
            bool flag = _levels.EnsureMinSize(num);
            TimeframeDataSegment.PriceLevel[] itemsArray = _levels.ItemsArray;
            if ( num == 1 )
            {
                TimeframeDataSegment.PriceLevel priceLevel = itemsArray[0];
                if ( priceLevel == null )
                {
                    TimeframeDataSegment.PriceLevel priceLevel1 = new TimeframeDataSegment.PriceLevel(normPrice);
                    TimeframeDataSegment.PriceLevel priceLevel2 = priceLevel1;
                    itemsArray[ 0 ] = priceLevel1;
                    priceLevel = priceLevel2;
                }
                return priceLevel;
            }
            TimeframeDataSegment.PriceLevel priceLevel3 = itemsArray[0];
            if ( priceLevel3 == null )
            {
                string str = string.Format("ERROR: GetPriceLevel({0}): first item is null for time={1}", normPrice, Time);
                UltrachartDebugLogger.Instance.WriteLine( str, new object[ 0 ] );
                throw new InvalidOperationException( str );
            }
            int num1 = (int)Math.Round((normPrice - priceLevel3.Price) / PriceStep);
            if ( num1 >= 0 )
            {
                if ( flag )
                {
                    for ( int i = 0 ; i < _levels.Count ; i++ )
                    {
                        if ( itemsArray[ i ] == null )
                        {
                            itemsArray[ i ] = new TimeframeDataSegment.PriceLevel( ( MinPrice + ( double ) i * PriceStep ).NormalizePrice( PriceStep ) );
                        }
                    }
                }
                return itemsArray[ num1 ];
            }
            num1 = -num1;
            for ( int j = num - 1 ; j >= 0 ; j-- )
            {
                double num2 = (MinPrice + (double)j * PriceStep).NormalizePrice(PriceStep);
                itemsArray[ j ] = ( j >= num1 ? itemsArray[ j - num1 ] ?? new TimeframeDataSegment.PriceLevel( num2 ) : new TimeframeDataSegment.PriceLevel( num2 ) );
            }
            return itemsArray[ 0 ];
        }

        public long GetValueByPrice( double price )
        {
            if ( _levels.Count == 0 )
            {
                return ( long ) 0;
            }
            price = price.NormalizePrice( PriceStep );
            if ( price < MinPrice || price > MaxPrice )
            {
                return ( long ) 0;
            }
            TimeframeDataSegment.PriceLevel[] itemsArray = _levels.ItemsArray;
            int num = (int)Math.Round((price - itemsArray[0].Price) / PriceStep);
            if ( num < 0 || num >= _levels.Count )
            {
                return ( long ) 0;
            }
            TimeframeDataSegment.PriceLevel priceLevel = itemsArray[num];
            if ( priceLevel != null )
            {
                return priceLevel.Value;
            }
            return ( long ) 0;
        }

        public static void MinMax( IEnumerable<TimeframeDataSegment> segments, out double minPrice, out double maxPrice )
        {
            int num;
            TimeframeDataSegment.MinMax( segments, out minPrice, out maxPrice, out num );
        }

        public static void MinMax( IEnumerable<TimeframeDataSegment> segments, out double minPrice, out double maxPrice, out int numCellsY )
        {
            minPrice = double.MaxValue;
            maxPrice = double.MinValue;
            numCellsY = 0;
            double priceStep = 0;
            foreach ( TimeframeDataSegment segment in segments )
            {
                if ( segment.MinPrice < minPrice )
                {
                    minPrice = segment.MinPrice;
                }
                if ( segment.MaxPrice > maxPrice )
                {
                    maxPrice = segment.MaxPrice;
                }
                priceStep = segment.PriceStep;
            }
            if ( priceStep > 0 )
            {
                numCellsY = 1 + ( int ) Math.Round( ( maxPrice - minPrice ) / priceStep );
            }
        }

        public void UpdatePoint( double price, long volume )
        {
            price = price.NormalizePrice( PriceStep );
            TimeframeDataSegment.PriceLevel priceLevel = GetPriceLevel(price);
            long value = priceLevel.Value;
            priceLevel.UpdateValue( volume );
            if ( priceLevel.Value > value && priceLevel.Value > MaxValue )
            {
                MaxValue = volume;
                MaxDigits = priceLevel.Digits;
                return;
            }
            if ( priceLevel.Value < value )
            {
                MaxValue = _levels.Max<TimeframeDataSegment.PriceLevel>( ( TimeframeDataSegment.PriceLevel l ) => l.Value );
                MaxDigits = MaxValue.NumDigitsInPositiveNumber();
            }
        }

        public class PriceLevel
        {
            public int Digits
            {
                get;
                private set;
            }

            public double Price
            {
                get;
            }

            public long Value
            {
                get;
                private set;
            }

            public PriceLevel( double price )
            {
                Price = price;
            }

            public void AddValue( long val )
            {
                Value = Value + val;
                Digits = Value.NumDigitsInPositiveNumber();
            }

            public void UpdateValue( long val )
            {
                Value = val;
                Digits = Value.NumDigitsInPositiveNumber();
            }
        }
    }
}
