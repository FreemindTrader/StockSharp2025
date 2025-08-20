using Ecng.ComponentModel;
using StockSharp.Algo.Indicators;
using StockSharp.Xaml.Charting.Ultrachart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


#nullable disable
internal sealed class IndicatorChartSettings : ChartSettingsObjectBase<IIndicator>
{
    public IndicatorChartSettings( IIndicator _param1 ) : base( _param1 )
    {
        Orig.Reseted += new Action( OnReseted );
    }

    private void OnReseted()
    {
    }

    public static PropertyDescriptor GetPropertyDescriptor( string name, object _param1, IIndicator indicator )
    {
        return new IndicatorProxyDescriptor( name, _param1, indicator );
    }

    private sealed class IndicatorProxyDescriptor( string name, object _param2, IIndicator ind ) 
        : ProxyDescriptor( name, _param2, ind, Enumerable.Append<Attribute>( Enumerable.Cast<Attribute>( TypeDescriptor.GetAttributes( ind, false ) ), ( Attribute ) new TypeConverterAttribute( typeof( ExpandableObjectConverter ) ) ) )
    {
        protected override ChartSettingsObjectBase<IIndicator> CreateWrapper( IIndicator indicator, Func<IIndicator, PropertyDescriptor, bool> _param2 = null )
        {
            return new IndicatorChartSettings( indicator );
        }
    }



    protected override PropertyDescriptor[ ] OnGetProperties( IIndicator ind )
    {
        if ( ind == null )
            return null;

        var pdList = new List<PropertyDescriptor>();
        pdList.AddRange( TypeDescriptor.GetProperties( ind, false ).OfType<PropertyDescriptor>().Where(p =>
                {
                    if ( !( p.Name != "Name" ) )
                        return false;

                    var att = p.Attributes.OfType<BrowsableAttribute>().FirstOrDefault();
                    return att == null || att.Browsable;

                } ).Select(s =>
                {
                    return !( s.GetValue( ind ) is IIndicator indicator ) ? s : IndicatorChartSettings.GetPropertyDescriptor( Extensions.GetDisplayName( s, indicator.Name ), ind, indicator );
                } ) );
        return pdList.ToArray();
    }    
}




//using Ecng.ComponentModel;
//using SciChart.Charting.Common;
//using MoreLinq;
//using StockSharp.Algo.Indicators;
//using StockSharp.Xaml.Charting.Ultrachart;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.ComponentModel;
//using System.Linq;
//using StockSharp.Xaml.Charting.Definitions;

//internal sealed class IndicatorChartSettings : ChartSettingsObjectBase<IIndicator>
//{
//    public IndicatorChartSettings( IIndicator indicator )
//      : base( indicator )
//    {
//        Orig.Reseted += OnReseted;
//    }

//    public static PropertyDescriptor GetPropertyDescriptor( string name, object settingObject, IIndicator indicator )
//    {
//        return new IndicatorProxyDescriptor( name, settingObject, indicator );
//    }

//    private void OnReseted( )
//    {
//    }

//    protected override PropertyDescriptor[ ] OnGetProperties( IIndicator element )
//    {
//        return element.With
//        (
//            i =>
//            {
//                return TypeDescriptor.GetProperties( i, false ).OfType<PropertyDescriptor>( ).Where
//                        (
//                            pd =>
//                            {
//                                if ( pd.Name == "Name" )
//                                {
//                                    return false;
//                                }

//                                var attri = pd.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );
//                                if ( attri == null )
//                                {
//                                    return true;
//                                }

//                                return attri.Browsable;
//                            }
//                        ).Select
//                        (
//                            pd =>
//                            {
//                                var ind = pd.GetValue( i ) as IIndicator;

//                                if ( ind == null )
//                                {
//                                    return pd;
//                                }

//                                return GetPropertyDescriptor( Extensions.GetDisplayName( pd, ind.Name ), i, ind );
//                            }
//                        ).ToArray( );
//            }
//        );
//    }

//    private sealed class IndicatorProxyDescriptor : ProxyDescriptor
//    {
//        public IndicatorProxyDescriptor( string name, object settingObject, IIndicator indicator )
//          : base( name, settingObject, indicator, MoreEnumerable.Append( TypeDescriptor.GetAttributes( indicator, false ).Cast<Attribute>( ), new TypeConverterAttribute( typeof( ExpandableObjectConverter ) ) ), null )
//        {
//        }

//        protected override ChartSettingsObjectBase<IIndicator> CreateWrapper( IIndicator obj, PdSelector selector = null )
//        {
//            return new IndicatorChartSettings( obj );
//        }
//    }
//}
