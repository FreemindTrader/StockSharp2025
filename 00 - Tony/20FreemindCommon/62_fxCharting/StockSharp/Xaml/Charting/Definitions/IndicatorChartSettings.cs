using Ecng.ComponentModel;
using SciChart.Charting.Common;
using MoreLinq;
using StockSharp.Algo.Indicators;
using fx.Charting.Ultrachart;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using fx.Charting.Definitions;

internal sealed class IndicatorChartSettings : ChartSettingsObjectBase<IIndicator>
{
    public IndicatorChartSettings( IIndicator indicator )
      : base( indicator )
    {
        Orig.Reseted += OnReseted;
    }

    public static PropertyDescriptor GetPropertyDescriptor( string name, object settingObject, IIndicator indicator )
    {
        return new IndicatorProxyDescriptor( name, settingObject, indicator );
    }

    private void OnReseted( )
    {
    }

    protected override PropertyDescriptor[ ] OnGetProperties( IIndicator element )
    {
        return element.With
        (
            i =>
            {
                return TypeDescriptor.GetProperties( i, false ).OfType<PropertyDescriptor>( ).Where
                        (
                            pd =>
                            {
                                if ( pd.Name == "Name" )
                                {
                                    return false;
                                }

                                var attri = pd.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );
                                if ( attri == null )
                                {
                                    return true;
                                }

                                return attri.Browsable;
                            }
                        ).Select
                        (
                            pd =>
                            {
                                var ind = pd.GetValue( i ) as IIndicator;

                                if ( ind == null )
                                {
                                    return pd;
                                }

                                return GetPropertyDescriptor( Extensions.GetDisplayName( pd, ind.Name ), i, ind );
                            }
                        ).ToArray( );
            }
        );
    }

    private sealed class IndicatorProxyDescriptor : ProxyDescriptor
    {
        public IndicatorProxyDescriptor( string name, object settingObject, IIndicator indicator )
          : base( name, settingObject, indicator, MoreEnumerable.Append( TypeDescriptor.GetAttributes( indicator, false ).Cast<Attribute>( ), new TypeConverterAttribute( typeof( ExpandableObjectConverter ) ) ), null )
        {
        }

        protected override ChartSettingsObjectBase<IIndicator> CreateWrapper( IIndicator obj, PdSelector selector = null )
        {
            return new IndicatorChartSettings( obj );
        }
    }
}
