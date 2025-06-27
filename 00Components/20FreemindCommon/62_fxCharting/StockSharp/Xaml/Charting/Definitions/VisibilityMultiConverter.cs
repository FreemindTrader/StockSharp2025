using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

internal sealed class VisibilityMultiConverter : IMultiValueConverter
{
    private bool _value = true;

    public bool Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    object IMultiValueConverter.Convert(
      object[ ] values,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        return values.Cast<bool>( ).Any( b => b == Value );
    }

    object[ ] IMultiValueConverter.ConvertBack(
      object value,
      Type[ ] targetTypes,
      object parameter,
      CultureInfo culture )
    {
        throw new NotImplementedException( );
    }    
}
