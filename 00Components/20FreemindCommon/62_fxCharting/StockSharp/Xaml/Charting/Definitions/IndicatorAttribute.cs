using StockSharp.Algo.Indicators;
using System;

namespace StockSharp.Xaml.Charting
{
    [AttributeUsage( AttributeTargets.Class )]
    public class IndicatorAttribute : Attribute
    {
        private readonly Type type_0;

        public IndicatorAttribute( Type type )
        {
            if ( type == null )
            {
                throw new ArgumentNullException( nameof( type ) );
            }
            if ( !typeof( IIndicator ).IsAssignableFrom( type ) )
            {
                throw new ArgumentException( nameof( type ) );
            }
            type_0 = type;
        }

        public Type Type
        {
            get
            {
                return type_0;
            }
        }
    }
}
