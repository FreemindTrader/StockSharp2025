using SciChart.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    internal sealed class BuySellConverter : IMultiValueConverter
    {
        public object Convert( object[ ] _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            if ( _param1.Length >= 2 )
            {
                object obj1 = _param1[0];
                if ( obj1 is double )
                {
                    double num1 = (double) obj1;
                    object obj2 = _param1[1];
                    if ( obj2 is double )
                    {
                        double num2 = (double) obj2;
                        return num2 > 0.0 ? num2 : num1;
                    }
                }
            }
            return Binding.DoNothing;
        }

        object[ ] IMultiValueConverter.ConvertBack(
          object _param1,
          Type[ ] _param2,
          object _param3,
          CultureInfo _param4 )
        {
            throw new NotSupportedException( );
        }
    }
}
