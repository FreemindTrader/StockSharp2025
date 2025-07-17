// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.CoordinateCalculators.CategoryCoordinateCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Numerics.CoordinateCalculators
{
    internal sealed class CategoryCoordinateCalculator : CoordinateCalculatorBase, ICategoryCoordinateCalculator, ICoordinateCalculator<double>
    {
        private readonly double _barTimeFrame;
        private readonly IndexRange _visibleRange;
        private readonly double _pixelsPerBar;
        private readonly double _viewportDimension;

        public bool IsBaseXValuesSorted
        {
            get; private set;
        }

        protected IList BaseXValues
        {
            get; private set;
        }

        public CategoryCoordinateCalculator( double barTimeFrame, double pixelsPerBar, double viewportDimension, IPointSeries categoryPointSeries, IndexRange visibleRange, bool isHorizontal, IList baseXValues, bool isBaseXValuesSorted )
        {
            BaseXValues = baseXValues;
            IsBaseXValuesSorted = isBaseXValuesSorted;
            IsXAxisCalculator = true;
            IsHorizontalAxisCalculator = isHorizontal;
            IsCategoryAxisCalculator = true;
            Guard.Assert( ( IComparable ) barTimeFrame, nameof( barTimeFrame ) ).IsGreaterThan( ( IComparable ) 0.0 );
            Guard.NotNull( ( object ) categoryPointSeries, nameof( categoryPointSeries ) );
            _barTimeFrame = barTimeFrame;
            _visibleRange = visibleRange;
            _pixelsPerBar = pixelsPerBar;
            _viewportDimension = viewportDimension;
        }

        public override sealed double GetCoordinate( DateTime dataValue )
        {
            return GetCoordinate( ( double ) dataValue.Ticks );
        }

        public override sealed double GetCoordinate( double dataValue )
        {
            return _viewportDimension * ( dataValue.RoundOff() - ( double ) _visibleRange.Min ) / ( double ) ( _visibleRange.Max - _visibleRange.Min ) + CoordinatesOffset;
        }

        public override sealed double GetDataValue( double coordinate )
        {
            return ( ( double ) ( _visibleRange.Max - _visibleRange.Min ) * ( coordinate - CoordinatesOffset ) / _viewportDimension + ( double ) _visibleRange.Min ).RoundOff();
        }

        public override DoubleRange TranslateBy( double pixels, DoubleRange inputRange )
        {
            int num = (int) (-pixels / _pixelsPerBar);
            return new DoubleRange( inputRange.Min + ( double ) num, inputRange.Max + ( double ) num );
        }

        public DateTime TransformIndexToData( int index )
        {
            if ( index == int.MinValue )
                return DateTime.MinValue;
            int count = BaseXValues.Count;
            IComparable c = (IComparable) DateTime.MinValue;
            if ( !BaseXValues.IsNullOrEmptyList() )
                c = index >= 0 ? ( index < count ? TransformIndexToDataInternal( index ) : ( IComparable ) ( ( double ) ( index - count + 1 ) * _barTimeFrame + ( double ) TransformIndexToDataInternal( count - 1 ).ToDateTime().Ticks ) ) : ( IComparable ) ( ( double ) index * _barTimeFrame + ( double ) TransformIndexToDataInternal( 0 ).ToDateTime().Ticks );
            return c.ToDateTime();
        }

        public int TransformDataToIndex( IComparable dataValue )
        {
            return TransformDataToIndex( dataValue.ToDateTime() );
        }

        public int TransformDataToIndex( DateTime dataValue )
        {
            return TransformDataToIndex( dataValue, SearchMode.Nearest );
        }

        public int TransformDataToIndex( DateTime dataValue, SearchMode mode )
        {
            int count = BaseXValues.Count;
            int num = TransformDataToIndexInternal((IComparable) dataValue, mode);
            if ( !BaseXValues.IsNullOrEmptyList() )
            {
                DateTime dateTime1 = ((IComparable) BaseXValues[count - 1]).ToDateTime();
                DateTime dateTime2 = ((IComparable) BaseXValues[0]).ToDateTime();
                if ( dataValue > dateTime1 )
                    num = ( int ) ( ( double ) ( dataValue.Ticks - dateTime1.Ticks ) / _barTimeFrame + ( double ) count - 1.0 );
                else if ( dataValue < dateTime2 )
                    num = ( int ) ( ( double ) ( dataValue.Ticks - dateTime2.Ticks ) / _barTimeFrame );
            }
            return num;
        }

        private IComparable TransformIndexToDataInternal( int index )
        {
            index = NumberUtil.Constrain( index, 0, BaseXValues.Count - 1 );
            return ( IComparable ) BaseXValues[ index ];
        }

        private int TransformDataToIndexInternal( IComparable dataValue, SearchMode mode )
        {
            if ( !BaseXValues.IsNullOrEmptyList() )
                dataValue = ComparableUtil.FromDouble( dataValue.ToDouble(), BaseXValues[ 0 ].GetType() );
            return BaseXValues.FindIndex( IsBaseXValuesSorted, dataValue, mode );
        }
    }
}
