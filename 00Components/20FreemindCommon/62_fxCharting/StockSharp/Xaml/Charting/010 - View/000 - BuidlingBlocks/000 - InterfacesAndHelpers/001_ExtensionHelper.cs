using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Numerics.TickProviders;
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
public static class AxisBaseHelper
{
    private sealed class SomeWheireosoe
    {
        public IChartAxis _IChartAxis_098;
        public AxisBase _axisBase_032;
        public PropertyGridEx _PropertyGridEx_032;
        public ContentControl ___ContentControl;
        public ICommand _____ICommand;

        public void SomePUbliciVoidFunction(

          object? _param1,
          CancelEventArgs _param2 )
        {
            if ( this._PropertyGridEx_032 == null )
                this._PropertyGridEx_032 = this.___ContentControl.FindVisualChild<PropertyGridEx>();
            if ( this._PropertyGridEx_032 != null )
            {
                this._PropertyGridEx_032.SelectedObject = ( object ) null;
                this._PropertyGridEx_032.SelectedObject = ( object ) this._IChartAxis_098;
            }
      ( ( DelegateCommand<ChartAxis> ) this._____ICommand )?.RaiseCanExecuteChanged();
        }
    }

    public sealed class fxCategoryDateTimeAxisLabelProvider : TradeChartAxisLabelProvider
    {
        private readonly IChartEx _chart;
        private readonly IChartAxis _ichartAxis;
        private CategoryDateTimeAxis _CategoryDateTimeAxis;
        private string _string01;
        private string _string02;

        public fxCategoryDateTimeAxisLabelProvider()
        {
        }

        public fxCategoryDateTimeAxisLabelProvider( IChartEx theChart, IChartAxis axis )
        {
            this._chart = theChart ?? throw new ArgumentNullException( "chart" );
            this._ichartAxis = axis ?? throw new ArgumentNullException( "axis" );
            ( ( INotifyPropertyChanged ) theChart ).PropertyChanged += new PropertyChangedEventHandler( this.OnChartPropertyChanged );
            axis.PropertyChanged += new PropertyChangedEventHandler( this.OnAxisPropertyChanged );
        }

        private TimeZoneInfo GetTimeZone()
        {
            TimeZoneInfo timeZone = this._ichartAxis?.TimeZone;
            if ( timeZone != null )
                return timeZone;
            // ISSUE: explicit non-virtual call
            return !( this._chart is Chart zF58l4g ) ? ( TimeZoneInfo ) null : ( zF58l4g.TimeZone );
        }

        public override void Init( IAxisCore _param1 )
        {
            this._CategoryDateTimeAxis = ( CategoryDateTimeAxis ) _param1;
            base.Init( _param1 );
        }

        public override void OnBeginAxisDraw()
        {
            this.InitString();
            if ( ( ( AxisBase ) this.ParentAxis ).TickProvider is AxisBaseHelper.CategoryDateTimeAxisTickProvider tickProvider )
                tickProvider.SyncParentParams();
            base.OnBeginAxisDraw();
        }

        private void InitString()
        {
            this._string01 = $"{{0:{this._CategoryDateTimeAxis.SubDayTextFormatting}}}";
            this._string02 = $"{{0:{this._CategoryDateTimeAxis.TextFormatting}}}";
        }

        public DateTime GetDateTime( IComparable _param1 )
        {
            if ( _param1 == null )
                return DateTime.MinValue;
            DateTime dateTime = (DateTime) _param1;
            return dateTime == DateTime.MinValue || this._ichartAxis == null || this._chart == null ? dateTime : TimeHelper.To( dateTime, TimeZoneInfo.Utc, this.GetTimeZone() ?? TimeZoneInfo.Local );
        }

        public override string FormatCursorLabel( IComparable _param1 )
        {
            DateTime dateTime = this.GetDateTime( _param1 );
            if ( dateTime == DateTime.MinValue )
                return string.Empty;
            TimeSpan? baseUtcOffset = this._ichartAxis?.TimeZone?.BaseUtcOffset;
            if ( baseUtcOffset.HasValue )
                return $"{dateTime:G} UTC{( baseUtcOffset.Value < TimeSpan.Zero ? "-" : "+" ) + baseUtcOffset.Value.ToString( "hh\\:mm" )}";
            return $"{dateTime:G}";
        }

        public override string FormatLabel( IComparable _param1 )
        {
            if ( this._CategoryDateTimeAxis == null )
                return base.FormatLabel( _param1 );
            if ( this._string02 == null || this._string01 == null )
                this.InitString();
            return StringHelper.Put( ( this.ParentAxis?.TickProvider is AxisBaseHelper.CategoryDateTimeAxisTickProvider tickProvider ? ( tickProvider.DiffAtDate( _param1 ) ? 1 : 0 ) : 0 ) != 0 ? this._string02 : this._string01, new object[ 1 ]
            {
        (object) this.GetDateTime(_param1)
            } );
        }

        private void OnChartPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
        {
            if ( !( _param2.PropertyName == "TimeZone" ) )
                return;

            IAxis pA = (IAxis) this.ParentAxis;

            pA.InvalidateElement();
        }

        private void OnAxisPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
        {
            if ( !( _param2.PropertyName == "TimeZone" ) )
                return;

            IAxis pA = (IAxis) this.ParentAxis;

            pA.InvalidateElement();
        }
    }

    //private sealed class PrivateSealedClass0392
    //{
    //    public DoubleRange _doubleRange;
    //    public AxisBaseHelper.SomeWheireosoe _someHelperClassMereber;

    //    public void SomeMethod0324(
    //      object _param1,
    //      MouseButtonEventArgs _param2 )
    //    {
    //        _param2.Handled = true;
    //        this._someHelperClassMereber._IChartAxis_098.AutoRange = true;
    //        this._someHelperClassMereber._axisBase_032.SetValue( AxisBase.\u0023\u003Dz3kyPJRWoiKq0, ( object ) this._doubleRange );
    //        this._someHelperClassMereber._axisBase_032.ParentSurface?.InvalidateElement();
    //    }
    //}

    public static AxisBase InitAndSetBinding( this IChartAxis axis, ICommand removeAxisCommand, ICommand resetAxisTimeZoneCommand, IChartEx theChart )
    {
        AxisBase newAxis = null;
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
                    bool hasAxis          = axis.ChartArea.XAxises.Contains( axis );
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
        newAxis.SetBindings( AxisBase.AutoRangeProperty, axis, "AutoRange", converter: ( IValueConverter ) new AxisBaseHelper.AutoRangeConverter() );
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
        BarSplitButtonItem barSplitButtonItem = new BarSplitButtonItem();
        barSplitButtonItem.Glyph = ThemedIconsExtension.GetImage( "Settings" );
        barSplitButtonItem.ActAsDropDown = true;
        barSplitButtonItem.Content = ( object ) LocalizedStrings.Properties;
        barSplitButtonItem.PopupControl = ( IPopupControl ) new PopupControlContainer()
        {
            Content = ( UIElement ) contentControl
        };
        items1.Add( ( IBarItem ) barSplitButtonItem );
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
            return ( object ) ( ChartAxisAlignment ) ( ( value as bool? ).GetValueOrDefault() ? ( this._someBoolean ? 2 : 1 ) : ( this._someBoolean ? 3 : 0 ) );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var axisAlign = (ChartAxisAlignment) value;
            return ( object ) ( bool ) ( axisAlign == ChartAxisAlignment.Default ? false : ( this._someBoolean ? ( axisAlign != ChartAxisAlignment.Bottom ? true : false ) : ( axisAlign != 0 ? true : false ) ) );
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

    //public sealed class CategoryDateTimeAxisTickProvider :
    //  NumericTickProvider
    //{
    //    private AxisParams _axisParams;
    //    private ICategoryCoordinateCalculator _categoryCoordinateCalc;

    //    public void OnBeginAxisDraw()
    //    {
    //        this._axisParams = ( ( AxisBase ) this.ParentAxis ).GetAxisParams();
    //    }

    //    public override double[ ] GetMajorTicks( IAxisParams axisParams )
    //    {
    //        IndexRange  visibleRange = axisParams.VisibleRange as IndexRange ;
    //        double majorDelta = (double) axisParams.MajorDelta;
    //        if ( ( visibleRange != null ? ( !visibleRange.IsDefined ? 1 : 0 ) : 1 ) != 0 )
    //            return ( double[ ] )base.GetMajorTicks( axisParams );

    //        IPointSeries ps = this._axisParams.\u0023\u003DznUzlqIN9ReXL;
    //        IndexRange  indexR = this._axisParams.\u0023\u003DindexR;
    //        return ( indexR != null ? ( !indexR.IsDefined ? 1 : 0 ) : 1 ) != 0 || ps == null || ps.\u0023\u003DzlpVGw6E\u003D() == 0 ? base.GetMajorTicks( axisParams ) : this.GetTicksWithinRange( visibleRange, majorDelta ) ?? base.GetMajorTicks( axisParams );
    //    }

    //    private int GetDateTimeDifferent( DateTime _param1, DateTime _param2 )
    //    {
    //        _param1 = this.ParentAxis.LabelProvider is AxisBaseHelper.fxCategoryDateTimeAxisLabelProvider labelProvider1 ? labelProvider1.GetDateTime( ( IComparable ) _param1 ) : _param1;
    //        _param2 = this.ParentAxis.get_LabelProvider() is AxisBaseHelper.fxCategoryDateTimeAxisLabelProvider labelProvider2 ? labelProvider2.GetDateTime( ( IComparable ) _param2 ) : _param2;
    //        if ( _param1.Date != _param2.Date )
    //            return 3;
    //        return _param1.Hour == _param2.Hour ? ( _param1.Second == 0 ? 1 : 0 ) : 2;
    //    }

    //    public bool DiffAtDate( IComparable _param1 )
    //    {
    //        if ( this._categoryCoordinateCalc == null)
    //    return false;
    //        int num = this._categoryCoordinateCalc.\u0023\u003DzFk6sufr\u0024co4e( _param1 );
    //        DateTime dateTime = this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc( num - 1 );
    //        return num == 0 || this.GetDateTimeDifferent( ( DateTime ) _param1, dateTime ) == 3;
    //    }

    //    private double[ ] GetTicksWithinRange(
    //      IndexRange _param1,
    //      double _param2 )
    //    {
    //        this._categoryCoordinateCalc = ( ( AxisBase ) this.ParentAxis ).GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
    //        if ( this._categoryCoordinateCalc == null)
    //    return ( double[ ] ) null;
    //        List<double> doubleList = new List<double>();
    //        int min = _param1.Min;
    //        int max = _param1.Max;
    //        int num1 = min;
    //        int num2 = int.MinValue;
    //        int num3 = (int) Math.Max(1.0, Math.Round(_param2));
    //        int val2 = (int) Math.Round(Math.Min(_param2 / 2.0, 1000.0));
    //        int num4 = max - min + 1;
    //        int num5;
    //        for ( int index1 = 0 ; num1 <= max && ++index1 <= num4 ; num1 = Math.Max( num5 + Math.Max( 1, val2 ), Math.Min( num5 + num3, max ) ) )
    //        {
    //            num5 = num1;
    //            int num6 = num1 == 0 ? 3 : this.GetDateTimeDifferent( this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc( num5 ), this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc( num5 - 1 ) );
    //            for ( int index2 = 1 ; index2 <= val2 && num6 < 3 ; ++index2 )
    //            {
    //                int num7 = num1 - index2;
    //                if ( num7 > num2 && num7 <= max )
    //                {
    //                    int num8 = num7 == 0 ? 3 : this.GetDateTimeDifferent( this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc( num7 ), this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc(num7 - 1));
    //                    if ( num8 > num6 )
    //                    {
    //                        num5 = num7;
    //                        num6 = num8;
    //                        if ( num8 == 3 )
    //                            break;
    //                    }
    //                }
    //                int num9 = num1 + index2;
    //                if ( num9 <= max )
    //                {
    //                    int num10 = num9 == 0 ? 3 : this.GetDateTimeDifferent(this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc(num9), this._categoryCoordinateCalc.\u0023\u003DzWZQlXHuDrnKc(num9 - 1));
    //                    if ( num10 > num6 )
    //                    {
    //                        num5 = num9;
    //                        num6 = num10;
    //                    }
    //                }
    //            }
    //            doubleList.Add( ( double ) num5 );
    //            num2 = num5;
    //        }
    //        return doubleList.ToArray();
    //    }
    //}

    public sealed class CategoryDateTimeAxisTickProvider : NumericTickProvider
    {
        private AxisParams _axisParams;
        private ICategoryCoordinateCalculator _categoryCoordinateCalc;

        public void SyncParentParams()
        {
            _axisParams = ( ( AxisBase ) ParentAxis ).GetAxisParams();
        }

        public override IList<double> GetMajorTicks( IAxisParams axisParams )
        {
            var visibleRange = axisParams.VisibleRange as IndexRange;
            var majorDelta   = ( double )axisParams.MajorDelta;

            if ( ( visibleRange != null ? ( !visibleRange.IsDefined ? 1 : 0 ) : 1 ) != 0 )
            {
                return base.GetMajorTicks( axisParams );
            }

            IList<double> majorTicks = GetTicksWithinRange( visibleRange, majorDelta );

            if ( majorTicks == null )
            {
                majorTicks = base.GetMajorTicks( axisParams );
            }

            return majorTicks;

        }

        private int GetDateTimeDifferent( DateTime first, DateTime second )
        {
            //var labelProvider = ParentAxis.LabelProvider as ExtensionHelper.CategoryDateTimeAxisLabelProvider;
            //first             = labelProvider != null ? labelProvider.GetDateTime( first ) : first;            
            //second            = labelProvider != null ? labelProvider.GetDateTime( second ) : second;

            if ( first.Date != second.Date )
            {
                return 3;
            }

            if ( first.Hour != second.Hour )
            {
                return 2;
            }

            return first.Second != 0 ? 0 : 1;
        }

        public bool DiffAtDate( IComparable icomparable_0 )
        {
            if ( _categoryCoordinateCalc == null )
                return false;

            int index = _categoryCoordinateCalc.TransformDataToIndex( icomparable_0 );

            DateTime data = ( DateTime ) _categoryCoordinateCalc.TransformIndexToData( index - 1 );

            if ( index != 0 )
            {
                return GetDateTimeDifferent( ( DateTime ) icomparable_0, data ) == 3;
            }

            return true;
        }

        private double[ ] GetTicksWithinRange( IndexRange visibleRange, double majorDelta )
        {
            _categoryCoordinateCalc = ( ( AxisBase ) ParentAxis ).GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
            if ( _categoryCoordinateCalc == null )
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

            for ( int i = 0 ; visiableMin <= max && ++i <= range ; visiableMin = Math.Max( lowerBound + Math.Max( 1, deltaAvg ), Math.Min( lowerBound + delta, max ) ) )
            {
                lowerBound = visiableMin;
                int diffDateHour = visiableMin == 0 ? 3 : GetDateTimeDifferent( ( DateTime ) _categoryCoordinateCalc.TransformIndexToData( lowerBound ), ( DateTime )_categoryCoordinateCalc.TransformIndexToData( lowerBound - 1 ) );

                for ( int j = 1 ; j <= deltaAvg && diffDateHour < 3 ; ++j )
                {
                    int iMinBackward = visiableMin - j;
                    if ( iMinBackward > lastValue && iMinBackward <= max )
                    {
                        int dtDiff = iMinBackward == 0 ? 3 : GetDateTimeDifferent( ( DateTime ) _categoryCoordinateCalc.TransformIndexToData( iMinBackward ), ( DateTime )_categoryCoordinateCalc.TransformIndexToData( iMinBackward - 1 ) );
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
                        int dtDiff = iMinForward == 0 ? 3 : GetDateTimeDifferent( ( DateTime ) _categoryCoordinateCalc.TransformIndexToData( iMinForward ), ( DateTime ) _categoryCoordinateCalc.TransformIndexToData( iMinForward - 1 ) );
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
