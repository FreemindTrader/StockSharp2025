using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Numerics.TickProviders;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.Axes.LabelProviders;
using SciChart.Data.Model;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

using IChart = StockSharp.Charting.IChart;

#nullable enable
public static class AxisBaseHelper2025
{    
    public sealed class fxCategoryDateTimeAxisLabelProvider : TradeChartAxisLabelProvider
    {
        private readonly IChart? _chart ;
        private readonly IChartAxis? _iAxis;
        private CategoryDateTimeAxis? _CategoryDateTimeAxis = null;
        private string _subDayTextFormatting = string.Empty;
        private string _textFormatting = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public fxCategoryDateTimeAxisLabelProvider()
        {
        }

        /// <summary>
        /// This is the class that will draw the x-axis of the chart.
        /// </summary>
        /// <param name="theChart"></param>
        /// <param name="axis"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public fxCategoryDateTimeAxisLabelProvider( IChart theChart, IChartAxis axis )
        {
            _chart = theChart ?? throw new ArgumentNullException( "chart" );
            _iAxis = axis ?? throw new ArgumentNullException( "axis" );

            ( ( INotifyPropertyChanged ) theChart ).PropertyChanged += new PropertyChangedEventHandler( OnChartPropertyChanged );
            
            axis.PropertyChanged += new PropertyChangedEventHandler( OnAxisPropertyChanged );
        }

        /// <summary>
        /// Get the time zone from the axis or chart
        /// </summary>
        /// <returns></returns>
        private TimeZoneInfo? GetTimeZone()
        {
            TimeZoneInfo? timeZone = _iAxis?.TimeZone;
            
            if ( timeZone != null )
                return timeZone;
            
            return !( _chart is Chart theChart ) ? null : ( theChart.TimeZone );
        }

        /// <summary>
        /// This init function is used to initialize the X-Axis of value type DateTime.-
        /// </summary>
        /// <param name="axis"></param>
        public override void Init( IAxisCore axis )
        {
            _CategoryDateTimeAxis = ( CategoryDateTimeAxis ) axis;
            base.Init( axis );
        }

        public override void OnBeginAxisDraw()
        {
            SetupFormatting();
            if (  ParentAxis.TickProvider is CategoryDateTimeAxisTickProvider tickProvider )
                tickProvider.SyncParentParams();
            base.OnBeginAxisDraw();
        }

        private void SetupFormatting()
        {
            _subDayTextFormatting = $"{{0:{_CategoryDateTimeAxis?.SubDayTextFormatting}}}";
            _textFormatting       = $"{{0:{_CategoryDateTimeAxis?.TextFormatting}}}";
        }

        public DateTime GetDateTime( IComparable dt )
        {
            if ( dt == null )
                return DateTime.MinValue;
            DateTime dateTime = (DateTime) dt;
            return dateTime == DateTime.MinValue || _iAxis == null || _chart == null ? dateTime : TimeHelper.To( dateTime, TimeZoneInfo.Utc, GetTimeZone() ?? TimeZoneInfo.Local );
        }

        public override string FormatCursorLabel( IComparable cLabel )
        {
            DateTime dateTime = GetDateTime( cLabel );
            if ( dateTime == DateTime.MinValue )
                return string.Empty;

            TimeSpan? baseUtcOffset = _iAxis?.TimeZone?.BaseUtcOffset;
            
            if ( baseUtcOffset.HasValue )
                return $"{dateTime:G} UTC{( baseUtcOffset.Value < TimeSpan.Zero ? "-" : "+" ) + baseUtcOffset.Value.ToString( "hh\\:mm" )}";

            return $"{dateTime:G}";
        }

        public override string FormatLabel( IComparable label )
        {
            if ( _CategoryDateTimeAxis == null )
                return base.FormatLabel( label );

            if ( _textFormatting == null || _subDayTextFormatting == null )
                SetupFormatting();

            return StringHelper.Put( ( ParentAxis?.TickProvider is CategoryDateTimeAxisTickProvider tickProvider ? ( tickProvider.DiffAtDate( label ) ? 1 : 0 ) : 0 ) != 0 ? _textFormatting : _subDayTextFormatting, GetDateTime( label ) );
        }

        private void OnChartPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
        {
            if ( !( _param2.PropertyName == "TimeZone" ) )
                return;

            IAxis pA = (IAxis) ParentAxis;

            pA.InvalidateElement();
        }

        private void OnAxisPropertyChanged( object? obj, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName != "TimeZone" )
                return;

            IAxis pA = (IAxis) ParentAxis;

            pA.InvalidateElement();
        }
    }    

    public static AxisBase InitAndSetBinding( this IChartAxis axis, ICommand removeAxisCommand, ICommand resetAxisTimeZoneCommand, IChart theChart )
    {
        AxisBase? newAxis = null;
        DoubleRange myDoubleRange;

        switch ( axis.AxisType )
        {
            case ChartAxisType.DateTime:
                newAxis = ( AxisBase ) new DateTimeAxis();
                break;

            case ChartAxisType.CategoryDateTime:
                {
                    newAxis = new CategoryDateTimeAxis();
                    newAxis.LabelProvider = new fxCategoryDateTimeAxisLabelProvider( theChart, axis );
                    newAxis.TickProvider = new CategoryDateTimeAxisTickProvider();

                    newAxis.SetBindings( CategoryDateTimeAxis.SubDayTextFormattingProperty, axis, "SubDayTextFormatting", BindingMode.TwoWay, null, null );
                }
                break;


            case ChartAxisType.Numeric:
                {
                    bool hasAxis = axis.ChartArea.XAxises.Contains( axis );
                    myDoubleRange = new DoubleRange( 0.0, hasAxis ? 0.0 : 0.05 );

                    var numericAxis       = new NumericAxis( );
                    numericAxis.GrowBy = myDoubleRange;
                    newAxis = numericAxis;

                    newAxis.SetBindings( AxisCore.FlipCoordinatesProperty, axis, "FlipCoordinates", BindingMode.TwoWay, null, null );
                    newAxis.SetBindings( AxisCore.CursorTextFormattingProperty, axis, "TextFormatting", BindingMode.TwoWay, null, null );

                    if ( !hasAxis )
                    {
                        newAxis.MouseDoubleClick += ( s, e ) =>
                        {
                            e.Handled = true;
                            axis.AutoRange = true;
                            newAxis.SetValue( AxisCore.GrowByProperty, myDoubleRange );
                            ( newAxis.ParentSurface )?.InvalidateElement();
                        };
                        break;
                    }
                }
                break;

            default:
                throw new ArgumentOutOfRangeException( "axis", axis.AxisType, LocalizedStrings.InvalidValue );
        }
        newAxis.Tag = axis; ;
        newAxis.SetBindings( AxisBase.IdProperty, axis, "Id" );
        newAxis.SetBindings( AxisBase.AutoRangeProperty, axis, "AutoRange", converter: ( IValueConverter ) new AutoRangeConverter() );
        newAxis.SetBindings( AxisBase.DrawLabelsProperty, axis, "DrawLabels" );
        newAxis.SetBindings( AxisBase.DrawMajorGridLinesProperty, axis, "DrawMajorGridLines" );
        newAxis.SetBindings( AxisBase.DrawMajorTicksProperty, axis, "DrawMajorTicks" );
        newAxis.SetBindings( AxisBase.DrawMajorBandsProperty, axis, "DrawMinorGridLines" );
        newAxis.SetBindings( AxisBase.DrawMinorGridLinesProperty, axis, "DrawMinorTicks" );
        newAxis.SetBindings( AxisBase.DrawMinorTicksProperty, axis, "TextFormatting" );
        newAxis.SetBindings( AxisBase.TextFormattingProperty, axis, "SwitchAxisLocation", converter: ( IValueConverter ) new AxisAlignmentConverter(  axis.ChartArea.XAxises.Contains( axis ) ) );
        newAxis.SetBindings( UIElement.VisibilityProperty, axis, "IsVisible", converter: ( IValueConverter ) new BoolToVisibilityConverter() );

        ControlTemplate template = new ControlTemplate();
        template.VisualTree = new FrameworkElementFactory( typeof( PropertyGridEx ) );
        
        template.VisualTree.SetValue( FrameworkElement.WidthProperty, ( object ) 300.0 );
        template.VisualTree.SetValue( FrameworkElement.HeightProperty, ( object ) 400.0 );
        template.VisualTree.SetValue( PropertyGridControl.ShowCategoriesProperty, ( object ) CategoriesShowMode.Hidden );
        template.VisualTree.SetValue( PropertyGridControl.ShowMenuButtonInRowsProperty, ( object ) false );
        template.VisualTree.SetValue( PropertyGridControl.ShowToolPanelProperty, ( object ) false );
        template.VisualTree.SetValue( PropertyGridControl.ShowSearchBoxProperty, ( object ) false );
        template.VisualTree.SetValue( PropertyGridControl.SelectedObjectProperty, axis );
        
        ContentControl contentControl = new ContentControl();
        contentControl.Template = template;        
        PopupMenu popupMenu1 = new PopupMenu();
        CommonBarItemCollection items1 = popupMenu1.Items;
        var bsButtonItem = new BarSplitButtonItem();
        bsButtonItem.Glyph = ThemedIconsExtension.GetImage( "Settings" );
        bsButtonItem.ActAsDropDown = true;
        bsButtonItem.Content = ( object ) LocalizedStrings.Properties;
        bsButtonItem.PopupControl = ( IPopupControl ) new PopupControlContainer()
        {
            Content = ( UIElement ) contentControl
        };
        items1.Add( ( IBarItem ) bsButtonItem );
        PopupMenu popupMenu2 = popupMenu1;
        if ( removeAxisCommand != null )
        {
            CommonBarItemCollection items2 = popupMenu2.Items;
            BarButtonItem barButtonItem = new BarButtonItem();
            barButtonItem.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem.Content = ( object ) LocalizedStrings.Delete;
            barButtonItem.Command = removeAxisCommand;
            barButtonItem.CommandParameter = axis;
            barButtonItem.CommandTarget = ( IInputElement ) newAxis;
            items2.Add( ( IBarItem ) barButtonItem );
        }
        if ( axis.AxisType == ChartAxisType.CategoryDateTime && resetAxisTimeZoneCommand != null )
        {
            CommonBarItemCollection items3 = popupMenu2.Items;
            BarButtonItem barButtonItem = new BarButtonItem();
            barButtonItem.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem.Content = ( object ) LocalizedStrings.ResetTimeZone;
            barButtonItem.Command = resetAxisTimeZoneCommand;
            barButtonItem.CommandParameter = axis;
            barButtonItem.CommandTarget = ( IInputElement ) newAxis;
            items3.Add( ( IBarItem ) barButtonItem );
        }

        PropertyGridEx grid = null;

        popupMenu2.Opening += (o, e) =>
        {
            if(grid == null)
            {
                grid = contentControl.FindVisualChild<PropertyGridEx>();
            }

            if(grid != null)
            {
                grid.SelectedObject = null;
                grid.SelectedObject = axis;
            }

            ((DelegateCommand<ChartAxis>) removeAxisCommand)?.RaiseCanExecuteChanged();
        };
        BarManager.SetDXContextMenu( ( UIElement ) newAxis, ( IPopupControl ) popupMenu2 );
        return newAxis;
    }

    


    private sealed class AxisAlignmentConverter( bool _param1 ) : IValueConverter
    {
        private readonly bool _someBoolean = _param1;

        public object Convert( object value, Type _param2, object _param3, CultureInfo _param4 )
        {
            return ( object ) ( ChartAxisAlignment ) ( ( value as bool? ).GetValueOrDefault() ? ( _someBoolean ? 2 : 1 ) : ( _someBoolean ? 3 : 0 ) );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var axisAlign = (ChartAxisAlignment) value;
            return ( object ) ( bool ) ( axisAlign == ChartAxisAlignment.Default ? false : ( _someBoolean ? ( axisAlign != ChartAxisAlignment.Bottom ? true : false ) : ( axisAlign != 0 ? true : false ) ) );
        }
    }

    private sealed class AutoRangeConverter : IValueConverter
    {
        public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            return ( object ) ( AutoRange ) ( ( bool ) _param1 ? 1 : 0 );
        }

        public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            return ( object ) ( bool ) ( !( _param1 is AutoRange qctsxU83FdjrbtjvEjd ) ? false : ( qctsxU83FdjrbtjvEjd == AutoRange.Always ? true : false ) );
        }
    }


    /// <summary>
    /// Tick provider for <see cref="CategoryDateTimeAxis"/>.
    /// </summary>
    public sealed class CategoryDateTimeAxisTickProvider : NumericTickProvider
    {
        private AxisParams _axisParams;
        private ICategoryCoordinateCalculator? _calc;

        public void SyncParentParams()
        {
            _axisParams = ( ( AxisBase ) ParentAxis ).GetAxisParams();
        }

        public override double[ ] GetMajorTicks( IAxisParams axisParams )
        {
            IndexRange?  visibleRange = axisParams.VisibleRange as IndexRange ;
            var majorDelta = (double) axisParams.MajorDelta;

            if ( ( visibleRange != null ? ( !visibleRange.IsDefined ? 1 : 0 ) : 1 ) != 0 )
                return ( double[ ] ) base.GetMajorTicks( axisParams );

            var surface = (SciChartSurface )((AxisBase) ParentAxis).ParentSurface;

            var rangeTicks = GetTicksWithinRange(visibleRange, majorDelta);
            var majorTicks = GetMajorTicks(axisParams);


            var ps = surface.RenderableSeries;
            var  indexR = axisParams.GetMaximumRange();
            return ( indexR != null ? ( !indexR.IsDefined ? 1 : 0 ) : 1 ) != 0 || ps == null || ps.Count == 0 ? majorTicks : rangeTicks ?? majorTicks;
        }

        private int GetDateTimeDifferent( DateTime dt1, DateTime dt2 )
        {
            AxisCore axis = ( AxisCore )ParentAxis;

            var provider = axis.LabelProvider as fxCategoryDateTimeAxisLabelProvider;

            if (  provider != null )
            {
                dt1 = provider.GetDateTime( ( IComparable ) dt1 );
                dt2 = provider.GetDateTime( ( IComparable ) dt2 );                               
            }

            if ( dt1.Date != dt2.Date )
                return 3;

            return dt1.Hour == dt2.Hour ? ( dt1.Second == 0 ? 1 : 0 ) : 2;
        }

        public bool DiffAtDate( IComparable myDateTime )
        {
            if ( _calc == null )
                return false;
            int index = _calc.TransformDataToIndex( myDateTime );
            DateTime dateTime = ( DateTime ) _calc.TransformIndexToData( index - 1 );
            return index == 0 || GetDateTimeDifferent( ( DateTime ) myDateTime, dateTime ) == 3;
        }

        private double[ ]? GetTicksWithinRange( IndexRange visibleRange, double majorDelta )
        {
            _calc = ( ( AxisBase ) ParentAxis ).GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
            if ( _calc == null )
            {
                return null;
            }

            List<double> majorTicks = new List<double>( );

            int min         = visibleRange.Min;
            int max         = visibleRange.Max;
            int visiableMin = min;
            int lastValue   = int.MinValue;
            int delta       = ( int )Math.Max( 1.0, Math.Round( majorDelta ) );
            int deltaAvg    = ( int )Math.Round( Math.Min( majorDelta / 2.0, 1000.0 ) );
            int range       = max - min + 1;
            int lowerBound;

            for ( int i = 0; visiableMin <= max && ++i <= range; visiableMin = Math.Max( lowerBound + Math.Max( 1, deltaAvg ), Math.Min( lowerBound + delta, max ) ) )
            {
                lowerBound = visiableMin;
                int diffDateHour = visiableMin == 0 ? 3 : GetDateTimeDifferent( ( DateTime ) _calc.TransformIndexToData( lowerBound ), ( DateTime )_calc.TransformIndexToData( lowerBound - 1 ) );

                for ( int j = 1; j <= deltaAvg && diffDateHour < 3; ++j )
                {
                    int iMinBackward = visiableMin - j;
                    if ( iMinBackward > lastValue && iMinBackward <= max )
                    {
                        int dtDiff = iMinBackward == 0 ? 3 : GetDateTimeDifferent( ( DateTime ) _calc.TransformIndexToData( iMinBackward ), ( DateTime )_calc.TransformIndexToData( iMinBackward - 1 ) );
                        if ( dtDiff > diffDateHour )
                        {
                            lowerBound = iMinBackward;
                            diffDateHour = dtDiff;
                            if ( dtDiff == 3 )
                            {
                                break;
                            }
                        }
                    }
                    int iMinForward = visiableMin + j;
                    if ( iMinForward <= max )
                    {
                        int dtDiff = iMinForward == 0 ? 3 : GetDateTimeDifferent( ( DateTime ) _calc.TransformIndexToData( iMinForward ), ( DateTime ) _calc.TransformIndexToData( iMinForward - 1 ) );
                        if ( dtDiff > diffDateHour )
                        {
                            lowerBound = iMinForward;
                            diffDateHour = dtDiff;
                        }
                    }
                }
                majorTicks.Add( lowerBound );
                lastValue = lowerBound;
            }
            return majorTicks.ToArray();
        }
    }




}
