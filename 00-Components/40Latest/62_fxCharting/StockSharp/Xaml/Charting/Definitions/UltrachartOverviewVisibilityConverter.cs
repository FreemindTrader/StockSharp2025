using SciChart.Charting.Visuals;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    public sealed class UltrachartOverviewVisibilityConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] values,
                                             Type targetType,
                                             object parameter,
                                             CultureInfo culture )
        {                        
            if( values[ 1 ] is SciChartSurface )
            {
                bool? isVisible = values[ 0 ] as bool?;
                
                if( isVisible.GetValueOrDefault( ) & isVisible.HasValue )
                {
                    return(  Visibility.Visible );                    
                }
            }
            
            return Visibility.Collapsed;
        }

        object[ ] IMultiValueConverter.ConvertBack( object value,
                                                    Type[ ] targetTypes,
                                                    object parameter,
                                                    CultureInfo culture )
        {
            throw new NotSupportedException( );
        }
    }
}
