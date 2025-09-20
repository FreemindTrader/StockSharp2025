using Ecng.Collections;
using SciChart.Data.Model;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// This is the dependency object to hold the visible range for the viewport.
/// 
/// For the X-axis, we have the following properties.
///     1) CategoryDateTimeRange    - the visible range for X-axis in the index form.
///     2) DateTimeRange            - visible range for X-axis in the DateTime Format
///     
/// For the Y-axis, we have the following property.
///     1) NumericRange             - visible range for Y-axis in the numeric form.
///     
/// </summary>
public sealed class VisibleRangeDpo : DependencyObject
{

    private static readonly Dictionary<(string, string, object), VisibleRangeDpo> _rangePropertyMap = new Dictionary<(string, string, object), VisibleRangeDpo>();

    public static readonly DependencyProperty CategoryDateTimeRangeProperty = DependencyProperty.Register(nameof(CategoryDateTimeRange), typeof(IndexRange),  typeof(VisibleRangeDpo), new PropertyMetadata(new PropertyChangedCallback(OnCategoryDateTimeRangePropertyChanged)));
    public static readonly DependencyProperty NumericRangeProperty          = DependencyProperty.Register(nameof(NumericRange),          typeof(DoubleRange), typeof(VisibleRangeDpo), new PropertyMetadata(new PropertyChangedCallback(OnNumericRangePropertyChanged)));    
    public static readonly DependencyProperty DateTimeRangeProperty         = DependencyProperty.Register(nameof(DateTimeRange),         typeof(DateRange),   typeof(VisibleRangeDpo), new PropertyMetadata(new PropertyChangedCallback(OnDateTimeRangePropertyChanged)));

    private IndexRange  _categoryDateTimeRange;
    private DoubleRange _numericRange;
    private static void OnCategoryDateTimeRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( VisibleRangeDpo ) d )._categoryDateTimeRange = ( IndexRange ) e.NewValue;
    }


    private static void OnNumericRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( VisibleRangeDpo ) d )._numericRange = ( DoubleRange ) e.NewValue;
    }


    private static void OnDateTimeRangePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        ( ( VisibleRangeDpo ) d )._dateRange = ( DateRange ) e.NewValue;
    }

    private DateRange _dateRange;

    private readonly ChartAxisType _chartAxisType;

    public VisibleRangeDpo( ChartAxisType axisType, int? maxBars )
    {
        _chartAxisType = axisType;
        InitRangeDepProperty( maxBars );
    }

    /// <summary>
    /// This is the visible range for X-axis in the index form.
    /// 
    /// For the current viewport, we will see the bars from index -5 to index (maxBars).
    /// </summary>
    public IndexRange CategoryDateTimeRange
    {
        get => _categoryDateTimeRange;
        set
        {
            SetValue( CategoryDateTimeRangeProperty, value );
        }
    }

    /// <summary>
    /// This is the visible range for Y-axis in the numeric form.
    /// </summary>
    public DoubleRange NumericRange
    {
        get => _numericRange;
        set
        {
            SetValue( NumericRangeProperty, value );
        }
    }

    /// <summary>
    /// This is the visible range for X-axis in the DateTime Format.
    /// 
    /// For the current viewport, we will see the bars starting from first bar time to last bar time.
    /// </summary>
    public DateRange DateTimeRange
    {
        get => _dateRange;
        set
        {
            SetValue( DateTimeRangeProperty, value );
        }
    }

    public ChartAxisType GetAxisType()
    {
        return _chartAxisType;
    }

    private void InitRangeDepProperty( int? range )
    {
        if ( !range.HasValue || range.GetValueOrDefault() <= 0 )
            range = new int?( 50 );

        CategoryDateTimeRange = new IndexRange( -5, range.Value );
        NumericRange          = new DoubleRange();
    }

    public static void InitRangeDepProperty( object viewModel )
    {
        var properties = _rangePropertyMap.Where(p => p.Key.Item3 == viewModel);

        foreach ( var property in properties )
        {
            property.Value.InitRangeDepProperty( VisibleRangeDpo.GetMinimumRange( viewModel ) );
        }
    }


    public static VisibleRangeDpo AddRangeProperty( object obj, string propertyName, string extra, ChartAxisType type )
    {
        return CollectionHelper.SafeAdd( _rangePropertyMap, (propertyName, extra, obj), p => new VisibleRangeDpo( type, VisibleRangeDpo.GetMinimumRange( obj ) ) );
    }

    private static int? GetMinimumRange( object drawObject )
    {
        switch ( drawObject )
        {
            case ScichartSurfaceMVVM drawing:
                return new int?( drawing.MinimumRange );

            case ChartViewModel vm:
                return new int?( vm.MinimumRange );

            default:
                return new int?();
        }
    }
}