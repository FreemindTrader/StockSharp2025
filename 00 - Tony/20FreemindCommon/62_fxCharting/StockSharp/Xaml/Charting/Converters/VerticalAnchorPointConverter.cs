using SciChart.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace fx.Charting
{
    public sealed class VerticalAnchorPointConverter : IValueConverter
    {
        private bool _center;

        public bool Center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
            }
        }        

        public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            return ( bool )_param1 ? VerticalAnchorPoint.Center : ( Center ? VerticalAnchorPoint.Top : VerticalAnchorPoint.Bottom );
        }

        object IValueConverter.ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            throw new NotSupportedException( );
        }
    }
}
