using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class DoubleArrayToDoubleCollectionConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        IEnumerable<double> doubles = _param1 as IEnumerable<double>;
        DoubleCollection doubleCollection = new DoubleCollection();
        if ( doubles != null )
        {
            foreach ( double num in doubles )
                doubleCollection.Add( num );
        }
        return ( object ) doubleCollection;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
