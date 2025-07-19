////using DevExpress.Xpf.PropertyGrid;
////using Ecng.Common;
////using Ecng.Xaml;
////using StockSharp.Charting;
////using SciChart.Charting.Model.DataSeries;
////using SciChart.Charting.Numerics.CoordinateCalculators;
////using SciChart.Charting.Visuals;
////using SciChart.Charting.Visuals.Axes;
////using Ecng.Xaml.Converters;
////using StockSharp.Localization;
////using StockSharp.Xaml.Charting;
////using StockSharp.Xaml.PropertyGrid;
////using System;
////using System.Collections.Generic; 
////using fx.Collections;
////using System.ComponentModel;
////using System.Globalization;
////using System.Windows;
////using System.Windows.Controls;
////using System.Windows.Data;
////using System.Windows.Input;
////using SciChart.Data.Model;
////using SciChart.Charting.Visuals.Axes.LabelProviders;
////using SciChart.Charting.Common.Helpers;
////using SciChart.Core.Framework;
////using SciChart.Charting.Numerics.TickProviders;
////using SciChart.Charting.ChartModifiers;
////using StockSharp.Charting;

////internal static class ExtensionHelper
////{
////    public static AxisAlignment ToAxisAlignment( this ChartAxisAlignment axisAlignment )
////    {
////        switch ( axisAlignment )
////        {
////            case ChartAxisAlignment.Default:
////                return AxisAlignment.Default;

////            case ChartAxisAlignment.Right:
////                return AxisAlignment.Right;

////            case ChartAxisAlignment.Left:
////                return AxisAlignment.Left;

////            case ChartAxisAlignment.Top:
////                return AxisAlignment.Top;

////            case ChartAxisAlignment.Bottom:
////                return AxisAlignment.Bottom;

////            default:
////                throw new ArgumentOutOfRangeException( "value" );
////        }
////    }

////    //public static AxisBase InitAndSetBinding( this ChartAxis axis, VisibleRangeDpo visibleRangeProp, ICommand removeAxisCommand, ICommand resetAxisTimeZoneCommand, StockSharp.Xaml.Charting.IChart theChart )
////    //{
////    //    AxisBase newAxis = null;

////    //    DoubleRange myDoubleRange;

////    //    switch ( axis.AxisType )
////    //    {
////    //        case ChartAxisType.DateTime:
////    //        {
////    //            newAxis = new DateTimeAxis( );
////    //        }            
////    //        break;

////    //        case ChartAxisType.CategoryDateTime:
////    //        {                
////    //            newAxis               = new CategoryDateTimeAxis( );
////    //            newAxis.LabelProvider = new CategoryDateTimeAxisLabelProvider( theChart, axis );
////    //            newAxis.TickProvider  = new CategoryDateTimeAxisTickProvider( );

////    //            newAxis.SetBindings( CategoryDateTimeAxis.SubDayTextFormattingProperty, axis, "SubDayTextFormatting", BindingMode.TwoWay, null, null );
////    //        }            
////    //        break;

////    //        case ChartAxisType.Numeric:
////    //        {
////    //            bool hasAxis          = axis.ChartArea.XAxises.Contains( axis );
////    //            myDoubleRange         = new DoubleRange( 0.0, hasAxis ? 0.0 : 0.05 );

////    //            var numericAxis       = new NumericAxis( );
////    //            numericAxis.GrowBy    = myDoubleRange;
////    //            newAxis               = numericAxis;                

////    //            newAxis.SetBindings( AxisCore.FlipCoordinatesProperty,                  axis, "FlipCoordinates", BindingMode.TwoWay, null, null );
////    //            newAxis.SetBindings( AxisCore.CursorTextFormattingProperty,             axis, "TextFormatting", BindingMode.TwoWay, null, null );

////    //            if ( !hasAxis )
////    //            {
////    //                newAxis.MouseDoubleClick += ( s, e ) =>
////    //                                                            {
////    //                                                                e.Handled = true;
////    //                                                                axis.AutoRange = true;
////    //                                                                newAxis.SetValue( AxisCore.GrowByProperty, myDoubleRange );
////    //                                                                ( newAxis.ParentSurface )?.InvalidateElement( );
////    //                                                            };
////    //                break;
////    //            }
////    //        }            
////    //        break;

////    //        default:
////    //        throw new ArgumentOutOfRangeException( );
////    //    }

////    //    newAxis.Tag = axis;
////    //    newAxis.SetBindings( AxisCore.IdProperty,                 axis, "Id"                , BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.AutoRangeProperty,          axis, "AutoRange"         , BindingMode.TwoWay, new AutoRangeConverter( ), null );
////    //    newAxis.SetBindings( AxisCore.DrawLabelsProperty,         axis, "DrawLabels"        , BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.DrawMajorGridLinesProperty, axis, "DrawMajorGridLines", BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.DrawMajorTicksProperty,     axis, "DrawMajorTicks"    , BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.DrawMajorBandsProperty,     axis, "DrawMajorBands"    , BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.DrawMinorGridLinesProperty, axis, "DrawMinorGridLines", BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.DrawMinorTicksProperty,     axis, "DrawMinorTicks"    , BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisCore.TextFormattingProperty,     axis, "TextFormatting"    , BindingMode.TwoWay, null, null );
////    //    newAxis.SetBindings( AxisBase.AxisAlignmentProperty,      axis, "AxisAlignment"     , BindingMode.OneWay, new AxisAlignmentConverter( ), null );
////    //    newAxis.SetBindings( UIElement.VisibilityProperty,        axis, "IsVisible"         , BindingMode.TwoWay, new BoolToVisibilityConverter( ), null );

////    //    string rangeDepPropertyName;
////    //    if ( visibleRangeProp.GetAxisType( ) != ChartAxisType.CategoryDateTime )
////    //    {
////    //        if ( visibleRangeProp.GetAxisType( ) != ChartAxisType.Numeric )
////    //        {
////    //            if ( visibleRangeProp.GetAxisType( ) != ChartAxisType.DateTime )
////    //            {
////    //                throw new NotSupportedException( "unsupported range type" );
////    //            }

////    //            rangeDepPropertyName = "DateTimeRange";
////    //        }
////    //        else
////    //        {
////    //            rangeDepPropertyName = "NumericRange";
////    //        }
////    //    }
////    //    else
////    //    {
////    //        rangeDepPropertyName = "CategoryDateTimeRange";
////    //    }        

////    //    newAxis.SetBindings( AxisCore.VisibleRangeProperty, visibleRangeProp, rangeDepPropertyName, BindingMode.TwoWay, null, null );
////    //    var templ = new ControlTemplate( );

////    //    templ.VisualTree = new FrameworkElementFactory( typeof( PropertyGridEx ) );
////    //    templ.VisualTree.SetValue( FrameworkElement.WidthProperty, 300.0 );
////    //    templ.VisualTree.SetValue( FrameworkElement.HeightProperty, 400.0 );
////    //    templ.VisualTree.SetValue( PropertyGridControl.ShowCategoriesProperty, CategoriesShowMode.Hidden );
////    //    templ.VisualTree.SetValue( PropertyGridControl.ShowMenuButtonInRowsProperty, false );
////    //    templ.VisualTree.SetValue( PropertyGridControl.ShowToolPanelProperty, false );
////    //    templ.VisualTree.SetValue( PropertyGridControl.ShowSearchBoxProperty, false );
////    //    templ.VisualTree.SetValue( PropertyGridControl.SelectedObjectProperty, axis );

////    //    var axisMenu       = new ContextMenu( );

////    //    var headerItem     = new MenuItem( );
////    //    headerItem.Header  = "LocalizedStrings.Str1507";

////    //    var pGridItem      = new MenuItem( );

////    //    pGridItem.Template = templ;

////    //    headerItem.Items.Add( pGridItem );
////    //    axisMenu.Items.Add( headerItem );

////    //    if ( removeAxisCommand != null )
////    //    {            
////    //        MenuItem removeAx         = new MenuItem( );
////    //        removeAx.Header           = "LocalizedStrings.Str2060";
////    //        removeAx.Command          = removeAxisCommand;
////    //        removeAx.CommandParameter = axis;
////    //        removeAx.CommandTarget    = newAxis;
////    //        axisMenu.Items.Add( removeAx );
////    //    }

////    //    if ( axis.AxisType == ChartAxisType.CategoryDateTime && resetAxisTimeZoneCommand != null )
////    //    {            
////    //        MenuItem resetTZ          = new MenuItem( );
////    //        resetTZ.Header            = LocalizedStrings.ResetTimeZone;
////    //        resetTZ.Command           = resetAxisTimeZoneCommand;
////    //        resetTZ.CommandParameter  = axis;
////    //        resetTZ.CommandTarget     = newAxis;

////    //        axisMenu.Items.Add( resetTZ );
////    //    }

////    //    newAxis.ContextMenu = axisMenu;
////    //    //newAxis.ContextMenuOpening += ( s, e ) => ( ( DelegateCommand<ChartAxis> )removeAxisCommand )?.RaiseCanExecuteChanged( );
////    //    return newAxis;
////    //}



////    public sealed class CategoryDateTimeAxisLabelProvider : TradeChartAxisLabelProvider
////    {
////        private readonly StockSharp.Charting.IChart      _chart;
////        private readonly ChartAxis      _chartAxis;
////        private CategoryDateTimeAxis _dateTimeAxis;
////        private string               _subDayFormat;
////        private string               _textFormat;

////        public CategoryDateTimeAxisLabelProvider( )
////        {

////        }

////        public CategoryDateTimeAxisLabelProvider( StockSharp.Charting.IChart chart, ChartAxis axis )
////        {
////            if ( chart == null )
////            {
////                throw new ArgumentNullException( "chart" );
////            }

////            _chart = chart;

////            if ( axis == null )
////            {
////                throw new ArgumentNullException( "axis" );
////            }

////            _chartAxis = axis;

////            ( ( INotifyPropertyChanged )chart ).PropertyChanged += OnChartPropertyChanged;
////            axis.PropertyChanged                                += OnAxisPropertyChanged;
////        }

////        #region Customization mainly overwrite these two methods.
////        public override string FormatCursorLabel( IComparable myDateTime )
////        {
////            DateTime dateTime = GetDateTime( myDateTime );

////            if ( dateTime == DateTime.MinValue )
////            {
////                return string.Empty;
////            }

////            TimeSpan? baseUtcOffset = _chartAxis?.TimeZone?.BaseUtcOffset;
////            if ( baseUtcOffset.HasValue )
////            {
////                return string.Format( "{0:G} UTC{1}", dateTime, ( ( baseUtcOffset.Value < TimeSpan.Zero ? "-" : "+" ) + baseUtcOffset.Value.ToString( "hh\\:mm" ) ) );
////            }

////            return string.Format( "{0:G}", dateTime );
////        }

////        public override string FormatLabel( IComparable myDateTime )
////        {
////            if ( _dateTimeAxis == null )
////            {
////                return base.FormatLabel( myDateTime );
////            }

////            if ( _textFormat == null || _subDayFormat == null )
////            {
////                SetupFormating( );
////            }

////            CategoryDateTimeAxisTickProvider tickProvider = ParentAxis?.TickProvider as CategoryDateTimeAxisTickProvider;
////            return StringHelper.Put( ( tickProvider != null ? ( tickProvider.DiffAtDate( myDateTime ) ? 1 : 0 ) : 0 ) != 0 ? _textFormat : _subDayFormat, new object[ 1 ] { GetDateTime( myDateTime ) } );
////        }
////        #endregion

////        private TimeZoneInfo GetAxisTimeZone( )
////        {
////            TimeZoneInfo timeZoneInfo;

////            if ( _chartAxis == null )
////            {
////                timeZoneInfo = null;
////            }
////            else
////            {
////                timeZoneInfo = _chartAxis.TimeZone;

////                if ( timeZoneInfo != null )
////                {
////                    return timeZoneInfo;
////                }
////            }
////            return ( _chart as Chart )?.TimeZone;
////        }

////        // Optional: called when the label provider is attached to the axis
////        public override void Init( IAxisCore parentAxis )
////        {
////            _dateTimeAxis = ( CategoryDateTimeAxis ) parentAxis;

////            base.Init( parentAxis );
////        }


////        // Optional: called when the Axis Begins each drawing pass
////        public override void OnBeginAxisDraw( )
////        {
////            SetupFormating( );

////            var provider = ( ( ( AxisBase )ParentAxis ).TickProvider as CategoryDateTimeAxisTickProvider );

////            if ( provider != null )
////            {
////                provider.SyncParentParams( );
////            }

////            base.OnBeginAxisDraw( );
////        }

////        private void SetupFormating( )
////        {
////            _subDayFormat = "{0:" + _dateTimeAxis.SubDayTextFormatting + "}";
////            _textFormat   = "{0:" + _dateTimeAxis.TextFormatting + "}";
////        }

////        public DateTime GetDateTime( IComparable myDateTime )
////        {
////            if ( myDateTime == null )
////            {
////                return DateTime.MinValue;
////            }

////            if ( myDateTime is DateTime )
////            {
////                DateTime dateTime = ( DateTime )myDateTime;
////                if ( !( dateTime == DateTime.MinValue ) && _chartAxis != null && _chart != null )
////                {
////                    return TimeHelper.To( dateTime, TimeZoneInfo.Utc, GetAxisTimeZone( ) ?? TimeZoneInfo.Local );
////                }

////                return dateTime;
////            }

////            return DateTime.MinValue;
////        }



////        private void OnChartPropertyChanged( object sender, PropertyChangedEventArgs e )
////        {
////            if ( !( e.PropertyName == "TimeZone" ) )
////            {
////                return;
////            }

////            ( ( IInvalidatableElement )ParentAxis ).InvalidateElement( );
////        }

////        private void OnAxisPropertyChanged( object sender, PropertyChangedEventArgs e )
////        {
////            if ( !( e.PropertyName == "TimeZone" ) )
////            {
////                return;
////            }

////            ( ( IInvalidatableElement )ParentAxis ).InvalidateElement( );
////        }
////    }

////    private sealed class AutoRangeConverter : IValueConverter
////    {
////        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
////        {
////            return ( AutoRange )( ( bool )value ? 1 : 0 );
////        }

////        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
////        {
////            object obj = value;
////            return ( !( obj is AutoRange ) ? false : ( ( AutoRange )obj == AutoRange.Always ? true : false ) );
////        }
////    }



////    private sealed class AxisAlignmentConverter : IValueConverter
////    {
////        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
////        {
////            return ( ( ChartAxisAlignment )value ).ToAxisAlignment( );
////        }

////        public object ConvertBack(
////          object value,
////          Type targetType,
////          object parameter,
////          CultureInfo culture )
////        {
////            throw new NotSupportedException( );
////        }
////    }



//public sealed class categorydatetimeaxistickprovider : numerictickprovider
//{
//    private axisparams _axisparams;
//    private icategorycoordinatecalculator _categorycoordinatecalc;

//    public void syncparentparams()
//    {
//        _axisparams = ( ( axisbase ) parentaxis ).getaxisparams();
//    }

//    public override ilist<double> getmajorticks( iaxisparams axisparams )
//    {
//        var visiblerange = axisparams.visiblerange as indexrange;
//        var majordelta   = ( double )axisparams.majordelta;

//        if ( ( visiblerange != null ? ( !visiblerange.isdefined ? 1 : 0 ) : 1 ) != 0 )
//        {
//            return base.getmajorticks( axisparams );
//        }

//        ilist<double> majorticks = gettickswithinrange( visiblerange, majordelta );

//        if ( majorticks == null )
//        {
//            majorticks = base.getmajorticks( axisparams );
//        }

//        return majorticks;

//    }

//    private int getdatetimedifferent( datetime first, datetime second )
//    {
//        //var labelprovider = parentaxis.labelprovider as extensionhelper.categorydatetimeaxislabelprovider;
//        //first             = labelprovider != null ? labelprovider.getdatetime( first ) : first;            
//        //second            = labelprovider != null ? labelprovider.getdatetime( second ) : second;

//        if ( first.date != second.date )
//        {
//            return 3;
//        }

//        if ( first.hour != second.hour )
//        {
//            return 2;
//        }

//        return first.second != 0 ? 0 : 1;
//    }

//    public bool diffatdate( icomparable icomparable_0 )
//    {
//        if ( _categorycoordinatecalc == null )
//            return false;

//        int index = _categorycoordinatecalc.transformdatatoindex( icomparable_0 );

//        datetime data = ( datetime ) _categorycoordinatecalc.transformindextodata( index - 1 );

//        if ( index != 0 )
//        {
//            return getdatetimedifferent( ( datetime ) icomparable_0, data ) == 3;
//        }

//        return true;
//    }

//    private double[ ] gettickswithinrange( indexrange visiblerange, double majordelta )
//    {
//        _categorycoordinatecalc = ( ( axisbase ) parentaxis ).getcurrentcoordinatecalculator() as icategorycoordinatecalculator;
//        if ( _categorycoordinatecalc == null )
//        {
//            return null;
//        }

//        pooledlist<double> majorticks = new pooledlist<double>( );

//        int min         = visiblerange.min;
//        int max         = visiblerange.max;
//        int visiablemin = min;
//        int lastvalue   = int.minvalue;
//        int delta       = ( int )math.max( 1.0, math.round( majordelta ) );
//        int deltaavg    = ( int )math.round( math.min( majordelta / 2.0, 1000.0 ) );
//        int range       = max - min + 1;
//        int lowerbound;

//        for ( int i = 0 ; visiablemin <= max && ++i <= range ; visiablemin = math.max( lowerbound + math.max( 1, deltaavg ), math.min( lowerbound + delta, max ) ) )
//        {
//            lowerbound = visiablemin;
//            int diffdatehour = visiablemin == 0 ? 3 : getdatetimedifferent( ( datetime ) _categorycoordinatecalc.transformindextodata( lowerbound ), ( datetime )_categorycoordinatecalc.transformindextodata( lowerbound - 1 ) );

//            for ( int j = 1 ; j <= deltaavg && diffdatehour < 3 ; ++j )
//            {
//                int iminbackward = visiablemin - j;
//                if ( iminbackward > lastvalue && iminbackward <= max )
//                {
//                    int dtdiff = iminbackward == 0 ? 3 : getdatetimedifferent( ( datetime ) _categorycoordinatecalc.transformindextodata( iminbackward ), ( datetime )_categorycoordinatecalc.transformindextodata( iminbackward - 1 ) );
//                    if ( dtdiff > diffdatehour )
//                    {
//                        lowerbound = iminbackward;
//                        diffdatehour = dtdiff;
//                        if ( dtdiff == 3 )
//                        {
//                            break;
//                        }
//                    }
//                }
//                int iminforward = visiablemin + j;
//                if ( iminforward <= max )
//                {
//                    int dtdiff = iminforward == 0 ? 3 : getdatetimedifferent( ( datetime ) _categorycoordinatecalc.transformindextodata( iminforward ), ( datetime ) _categorycoordinatecalc.transformindextodata( iminforward - 1 ) );
//                    if ( dtdiff > diffdatehour )
//                    {
//                        lowerbound = iminforward;
//                        diffdatehour = dtdiff;
//                    }
//                }
//            }
//            majorticks.add( lowerbound );
//            lastvalue = lowerbound;
//        }
//        return majorticks.toarray();
//    }
//}
////}
