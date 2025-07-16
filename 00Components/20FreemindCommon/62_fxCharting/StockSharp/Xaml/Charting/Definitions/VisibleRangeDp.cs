using Ecng.Collections;
using StockSharp.Charting;
using MoreLinq;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Linq;
using System.Windows;
using SciChart.Data.Model;
using StockSharp.Charting;

public sealed class VisbleRangeDp : DependencyObject
{
    public static readonly DependencyProperty CategoryDateTimeRangeProperty = DependencyProperty.Register( nameof( CategoryDateTimeRange ), typeof( IndexRange ),  typeof( VisbleRangeDp ), new PropertyMetadata( new PropertyChangedCallback( OnCategoryDateTimeRangePropertyChanged ) ) );
    public static readonly DependencyProperty NumericRangeProperty          = DependencyProperty.Register( nameof( NumericRange )         , typeof( DoubleRange ), typeof( VisbleRangeDp ), new PropertyMetadata( new PropertyChangedCallback( OnNumericRangePropertyChanged ) ) );
    public static readonly DependencyProperty DateTimeRangeProperty         = DependencyProperty.Register( nameof( DateTimeRange )        , typeof( DateRange ),   typeof( VisbleRangeDp ), new PropertyMetadata( new PropertyChangedCallback( OnDateTimeRangePropertyChanged ) ) );



    private static readonly PooledDictionary<Tuple<string, object>, VisbleRangeDp> _rangePropertyMap = new PooledDictionary<Tuple<string, object>, VisbleRangeDp>( );

    private static void OnCategoryDateTimeRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( VisbleRangeDp )d )._categoryDateTimeRange = ( IndexRange )e.NewValue;
    }



    private static void OnNumericRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( VisbleRangeDp )d )._numericRange = ( DoubleRange )e.NewValue;
    }



    private static void OnDateTimeRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( VisbleRangeDp )d )._dateRange = ( DateRange )e.NewValue;
    }

    private IndexRange             _categoryDateTimeRange;
    private DoubleRange            _numericRange;
    private DateRange              _dateRange;
    private readonly ChartAxisType _chartAxisType;

    public VisbleRangeDp( ChartAxisType axisType )
    {
        _chartAxisType = axisType;
        Init( );
    }

    public IndexRange CategoryDateTimeRange
    {
        get
        {
            return _categoryDateTimeRange;
        }
        set
        {
            SetValue( CategoryDateTimeRangeProperty, value );
        }
    }

    public DoubleRange NumericRange
    {
        get
        {
            return _numericRange;
        }
        set
        {
            SetValue( NumericRangeProperty, value );
        }
    }

    public DateRange DateTimeRange
    {
        get
        {
            return _dateRange;
        }
        set
        {
            SetValue( DateTimeRangeProperty, value );
        }
    }

    public ChartAxisType GetAxisType( )
    {
        return _chartAxisType;
    }

    private void Init( )
    {
        CategoryDateTimeRange = new IndexRange( 0, 0 );
        NumericRange          = new DoubleRange( );
    }

    public static void InitRangeDepProperty( object viewModel )
    {
        var properties = _rangePropertyMap.Where( p => p.Key == viewModel );

        foreach ( var property in properties )
        {
            property.Value.Init( );
        }
    }

    public static VisbleRangeDp AddRangeProperty( object viewModel, string propertyName, ChartAxisType axisType )
    {
        return _rangePropertyMap.SafeAdd( Tuple.Create( propertyName, viewModel ), p => new VisbleRangeDp( axisType ) );
    }


}
