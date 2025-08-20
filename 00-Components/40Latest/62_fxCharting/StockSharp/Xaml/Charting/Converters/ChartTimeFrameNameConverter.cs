using fx.Definitions;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    
    public class ChartTimeFrameNameConverter : IValueConverter
    {
        object IValueConverter.Convert( object _param1, Type _param2, object _param3, CultureInfo _param4)
        {
            if ( !( _param1 is TimeSpan ) )
            {
                return Binding.DoNothing;
            }
                
            TimeSpan timeSpan = (TimeSpan) _param1;

            var timeString = timeSpan.ToReadable( );
            
            return timeString;
        }

        object IValueConverter.ConvertBack(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4)
        {
            throw new NotSupportedException( );
        }
    }
}
