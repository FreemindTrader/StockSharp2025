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
///
/// </summary>
public sealed class VisibleRangeDpo : DependencyObject
{
    private static readonly Dictionary<(string, string, object), VisibleRangeDpo> _rangePropertyMap = new Dictionary<(string, string, object), VisibleRangeDpo>();

    public static readonly DependencyProperty CategoryDateTimeRangeProperty 
        = DependencyProperty.Register(
                                        nameof(CategoryDateTimeRange),
                                        typeof(IndexRange),
                                        typeof(VisibleRangeDpo),
                                        new PropertyMetadata(new PropertyChangedCallback(OnCategoryDateTimeRangePropertyChanged)));

    private IndexRange  _categoryDateTimeRange;

    public static readonly DependencyProperty NumericRangeProperty 
        = DependencyProperty.Register(
                                        nameof(NumericRange),
                                        typeof(DoubleRange),
                                        typeof(VisibleRangeDpo),
                                        new PropertyMetadata(new PropertyChangedCallback(OnNumericRangePropertyChanged)));

    private DoubleRange _numericRange;

    public static readonly DependencyProperty DateTimeRangeProperty = DependencyProperty.Register(
        nameof(DateTimeRange),
        typeof(DateRange),
        typeof(VisibleRangeDpo),
        new PropertyMetadata(new PropertyChangedCallback(OnDateTimeRangePropertyChanged)));

    private static void OnCategoryDateTimeRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((VisibleRangeDpo) d)._categoryDateTimeRange = (IndexRange) e.NewValue;
    }


    private static void OnNumericRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((VisibleRangeDpo) d)._numericRange = (DoubleRange) e.NewValue;
    }


    private static void OnDateTimeRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((VisibleRangeDpo) d)._dateRange = (DateRange) e.NewValue;
    }

    private DateRange _dateRange;

    private readonly ChartAxisType _chartAxisType;

    public VisibleRangeDpo(ChartAxisType _param1, int? _param2)
    {
        this._chartAxisType = _param1;
        this.InitRangeDepProperty(_param2);
    }

    public IndexRange CategoryDateTimeRange
    {
        get => this._categoryDateTimeRange;
        set
        {
            this.SetValue(CategoryDateTimeRangeProperty, (object) value);
        }
    }

    public DoubleRange NumericRange
    {
        get => this._numericRange;
        set
        {
            this.SetValue(NumericRangeProperty, (object) value);
        }
    }

    public DateRange DateTimeRange
    {
        get => this._dateRange;
        set
        {
            this.SetValue(DateTimeRangeProperty, (object) value);
        }
    }

    public ChartAxisType GetAxisType()
    {
        return this._chartAxisType;
    }

    private void InitRangeDepProperty(int? range)
    {
        if(!range.HasValue || range.GetValueOrDefault() <= 0)
            range = new int?(50);
        this.CategoryDateTimeRange = new IndexRange(-5, range.Value);
        this.NumericRange = new DoubleRange();
    }

    public static void InitRangeDepProperty(object viewModel)
    {
        var properties = _rangePropertyMap.Where(p => p.Key.Item3 == viewModel);

        foreach(var property in properties)
        {
            property.Value.InitRangeDepProperty(VisibleRangeDpo.GetMinimumRange(viewModel));
        }
    }


    public static VisibleRangeDpo AddRangeProperty(object obj, string propertyName, string extra, ChartAxisType type)
    {
        return CollectionHelper.SafeAdd(
            _rangePropertyMap,
            (propertyName, extra, obj),
            p => new VisibleRangeDpo(type, VisibleRangeDpo.GetMinimumRange(obj)));
    }

    private static int? GetMinimumRange(object _param0)
    {
        switch(_param0)
        {
            case ScichartSurfaceMVVM drawing:
                return new int?(drawing.MinimumRange);
            case ChartViewModel vm:
                return new int?(vm.MinimumRange);
            default:
                return new int?();
        }
    }
}