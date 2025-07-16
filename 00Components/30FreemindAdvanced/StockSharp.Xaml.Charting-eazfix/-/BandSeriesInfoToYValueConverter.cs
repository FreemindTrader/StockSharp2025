using System;
using System.Globalization;
using System.Windows.Data;
using SciChart.Charting.Model.ChartData;

#nullable disable
namespace SciChart.Charting;

internal sealed class BandSeriesInfoToYValueConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        BandSeriesInfo k3zbmiw1OaRAdtq7psDwa = _param1 as BandSeriesInfo;
        string str1 = k3zbmiw1OaRAdtq7psDwa.FormattedYValue;
        string str2;
        string str3;
        if ( k3zbmiw1OaRAdtq7psDwa.YValue.CompareTo( ( object ) k3zbmiw1OaRAdtq7psDwa.Y1Value ) >= 0 )
        {
            str2 = k3zbmiw1OaRAdtq7psDwa.FormattedYValue;
            str3 = k3zbmiw1OaRAdtq7psDwa.FormattedY1Value;
        }
        else
        {
            str2 = k3zbmiw1OaRAdtq7psDwa.FormattedY1Value;
            str3 = k3zbmiw1OaRAdtq7psDwa.FormattedYValue;
        }
        if ( _param3 != null )
        {
            switch ( _param3.ToString().ToUpperInvariant() )
            {
                case "1":
                    str1 = k3zbmiw1OaRAdtq7psDwa.FormattedY1Value;
                    break;
                case "GREATER":
                    str1 = str2;
                    break;
                case "LOWER":
                    str1 = str3;
                    break;
            }
        }
        return ( object ) str1;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
