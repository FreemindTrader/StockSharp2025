using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;

using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Localization;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using static StockSharp.Xaml.OrderGrid;




namespace StockSharp.Xaml
{
    class OrderStateConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            var state = (OrderStates)values[0];
            var order = (Order)values[1];

            switch ( state )
            {
                case OrderStates.None:
                    return "--";
                case OrderStates.Pending:
                    return LocalizedStrings.Str538;
                case OrderStates.Failed:
                    return LocalizedStrings.Str152;
                case OrderStates.Active:
                    return LocalizedStrings.Str238;
                case OrderStates.Done:
                    return order.IsMatched() ? LocalizedStrings.Str1328 : LocalizedStrings.Str1329;
                default:
                    throw new ArgumentOutOfRangeException( nameof( values ), state, LocalizedStrings.Str1597Params.Put( order ) );
            }
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }


    

// ns2.Class412


    internal sealed class OrderStateColorConverter : IMultiValueConverter
    {
        private Brush brush_0;

        private Brush brush_1;

        private Brush brush_2;

        private Brush brush_3;

        private Brush brush_4;

        private Brush brush_5;

        private Brush brush_6;

        private Brush brush_7;

        private Brush brush_8;

        private Brush brush_9;

        private Brush brush_10;

        private Brush brush_11;

        public Brush NoneBrushDark
        {
            get
            {
                return brush_0;
            }
            set
            {
                brush_0 = value;
            }
        }

        public Brush NoneBrushLight
        {
            get
            {
                return brush_1;
            }
            set
            {
                brush_1 = value;
            }
        }

        public Brush CancelBrushDark
        {
            get
            {
                return brush_2;
            }
            set
            {
                brush_2 = value;
            }
        }

        public Brush CancelBrushLight
        {
            get
            {
                return brush_3;
            }
            set
            {
                brush_3 = value;
            }
        }

        public Brush PendingBrushDark
        {
            get
            {
                return brush_4;
            }
            set
            {
                brush_4 = value;
            }
        }

        public Brush PendingBrushLight
        {
            get
            {
                return brush_5;
            }
            set
            {
                brush_5 = value;
            }
        }

        public Brush ActiveBrushDark
        {
            get
            {
                return brush_6;
            }
            set
            {
                brush_6 = value;
            }
        }

        public Brush ActiveBrushLight
        {
            get
            {
                return brush_7;
            }
            set
            {
                brush_7 = value;
            }
        }

        public Brush FailedBrushDark
        {
            get
            {
                return brush_8;
            }
            set
            {
                brush_8 = value;
            }
        }

        public Brush FailedBrushLight
        {
            get
            {
                return brush_9;
            }
            set
            {
                brush_9 = value;
            }
        }

        public Brush MatchedBrushDark
        {
            get
            {
                return brush_10;
            }
            set
            {
                brush_10 = value;
            }
        }

        public Brush MatchedBrushLight
        {
            get
            {
                return brush_11;
            }
            set
            {
                brush_11 = value;
            }
        }

        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            object obj = values[0];
            if ( obj != null && obj != DependencyProperty.UnsetValue )
            {
                bool IsDark = ApplicationThemeHelper.ApplicationThemeName.IsDark();

                switch ( ( OrderEnum ) obj )
                {
                    default:
                        throw new ArgumentOutOfRangeException();
                    case OrderEnum.None:
                        if ( !IsDark )
                        {
                            return NoneBrushLight;
                        }
                        return NoneBrushDark;

                    case OrderEnum.Pending:
                        if ( !IsDark )
                        {
                            return PendingBrushLight;
                        }
                        return PendingBrushDark;

                    case OrderEnum.Failed:
                        if ( !IsDark )
                        {
                            return FailedBrushLight;
                        }
                        return FailedBrushDark;

                    case OrderEnum.Active:
                        if ( !IsDark )
                        {
                            return ActiveBrushLight;
                        }
                        return ActiveBrushDark;
                    case OrderEnum.Cancelled:
                        if ( !IsDark )
                        {
                            return CancelBrushLight;
                        }
                        return CancelBrushDark;

                    case OrderEnum.Matched:
                        if ( !IsDark )
                        {
                            return MatchedBrushLight;
                        }
                        return MatchedBrushDark;
                }
            }
            return Binding.DoNothing;
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }



    internal sealed class OrderTimeInForceConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {            
            //IL_0040: Expected I4, but got Unknown
            if ( values[ 0 ] != DependencyProperty.UnsetValue && values[ 1 ] != DependencyProperty.UnsetValue )
            {
                TimeInForce? tif = (TimeInForce?)values[0];
                DateTimeOffset? expiryDate = (DateTimeOffset?)values[1];
                if ( tif.HasValue )
                {
                    TimeInForce valueOrDefault = tif.GetValueOrDefault();
                    switch ( valueOrDefault )
                    {
                        default:
                            throw new ArgumentOutOfRangeException();
                        case TimeInForce.MatchOrCancel:
                            return "FOK";

                        case TimeInForce.CancelBalance:
                            return "IOC";

                        case TimeInForce.PutInQueue:
                            break;
                    }
                }
                if ( !expiryDate.HasValue )
                {
                    return "GTC";
                }
                if ( TraderHelper.IsToday( expiryDate.Value ) )
                {
                    return "GTD";
                }
                return expiryDate.Value.LocalDateTime.ToString( "d" );
            }
            return Binding.DoNothing;
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }


    //class OrderTimeInForceConverter : IMultiValueConverter
    //{
    //    object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
    //    {
    //        var tif        = (TimeInForce?)values[0];
    //        var expiryDate = (DateTimeOffset?)values[1];

    //        switch ( tif )
    //        {
    //            case null:
    //            case TimeInForce.PutInQueue:
    //                {
    //                    if ( expiryDate == null || expiryDate.Value.IsGtc() )
    //                        return "GTC";
    //                    else if ( expiryDate.Value.IsToday() )
    //                        return "GTD";
    //                    else
    //                        return expiryDate.Value.LocalDateTime.ToString( "d" );
    //                }
    //            case TimeInForce.MatchOrCancel:
    //                return "FOK";
    //            case TimeInForce.CancelBalance:
    //                return "IOC";
    //            default:
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }

    //    object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
    //    {
    //        throw new NotSupportedException();
    //    }
    //}

    //class OrderConditionConverter : IValueConverter
    //{
    //    object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
    //    {
    //        return OrderGrid.FormatCondition( ( OrderCondition ) value );
    //    }

    //    object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    //    {
    //        throw new NotSupportedException();
    //    }
    //}

    internal sealed class OrderConditionConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( value as string )?.Replace( Environment.NewLine, ", " );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }

}
