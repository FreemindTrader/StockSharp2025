using DevExpress.Xpf.PropertyGrid;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using Ecng.Common;

namespace fx.Charting.Ultrachart;

/// <summary>
/// Proxy object to edit chart indicator element in property grid.
/// </summary>
public class ChartIndicatorElementSettingsObject : ChartSettingsObjectBase<IChartElement>
{
    /// <summary>Create instance.</summary>
    /// <param name="element">Element.</param>
    public ChartIndicatorElementSettingsObject(IChartElement element) : base(element)
    {
        CategoriesMode = CategoriesShowMode.Hidden;
        Orig.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
    }

    protected override PropertyDescriptor[ ] OnGetProperties(IChartElement element)
    {
        // Tony 1:
        throw new NotImplementedException();
        //PooledList< PropertyDescriptor > propertyDescriptorList = new PooledList< PropertyDescriptor >( );

        //if( element is IndicatorUI indicatorElement )
        //{
        //    var myChart = element.CheckOnNull( nameof( element ) ).ChartArea?.Chart;

        //    IIndicator indicator = myChart?.GetIndicator( indicatorElement );

        //    if( indicator != null )
        //    {
        //        propertyDescriptorList.Add( IndicatorChartSettings.GetPropertyDescriptor( indicator.Name, this, indicator ) );
        //    }
        //}

        //var myStyle = new PdSelector( ( e, p ) =>
        //{
        //    BrowsableAttribute browse = p.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );

        //    if ( ( browse != null ? ( browse.Browsable ? 1 : 0 ) : 1 ) != 0 )
        //    {
        //        Attribute0 attr = p.Attributes.OfType<Attribute0>( ).FirstOrDefault( );
        //        if ( ( attr != null ? ( !attr.GetAttributeValue( ) ? 1 : 0 ) : 1 ) != 0 )
        //        {
        //            if ( e.ParentElement == null )
        //            {
        //                return !( p.Name == "IsVisible" );
        //            }
        //            return true;
        //        }
        //    }

        //    return false;
        //} );

        //var browsableAndVisible = new PdSelector( ( e, p ) =>
        //{
        //    if ( e.ParentElement != null )
        //    {
        //        return false;
        //    }

        //    if ( p.Name == "IsVisible" )
        //    {
        //        return true;
        //    }

        //    var browse = p.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );

        //    if ( ( browse != null ? ( browse.Browsable ? 1 : 0 ) : 1 ) == 0 )
        //    {
        //        return false;
        //    }
        //    var attribute0 = p.Attributes.OfType<Attribute0>( ).FirstOrDefault( );

        //    if ( attribute0 == null )
        //    {
        //        return false;
        //    }
        //    return attribute0.GetAttributeValue( );
        //} );

        //propertyDescriptorList.Add( ChartComponentChartSettings.Create( "LocalizedStrings.Str1946", this, element, myStyle ));
        //propertyDescriptorList.Add( ChartComponentChartSettings.Create( "LocalizedStrings.Str1946", this, element, browsableAndVisible ) );

        //return propertyDescriptorList.ToArray( );
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        NotifyChanged(e.PropertyName);
    }
}
