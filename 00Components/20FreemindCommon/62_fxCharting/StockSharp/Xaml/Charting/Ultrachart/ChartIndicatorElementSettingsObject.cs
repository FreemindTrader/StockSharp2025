using DevExpress.Xpf.PropertyGrid;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.ComponentModel;
using System.Linq;
using Ecng.Common;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using StockSharp.Charting;

namespace StockSharp.Xaml.Charting.Ultrachart;

/// <summary>
/// Proxy object to edit chart indicator element in property grid.
/// </summary>
public class ChartIndicatorElementSettingsObject : ChartSettingsObjectBase<
#nullable disable
IChartElement>
{
    /// <summary>Create instance.</summary>
    /// <param name="element">Element.</param>
    public ChartIndicatorElementSettingsObject( IChartElement element )
      : base( element )
    {
        this.CategoriesMode = CategoriesShowMode.Hidden;
        this.Orig.PropertyChanged += new PropertyChangedEventHandler( this.OnPropertyChanged );
    }

    /// <inheritdoc />
    protected override PropertyDescriptor[ ] OnGetProperties( IChartElement element )
    {
        List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
        if ( element is IChartIndicatorElement elem )
        {
            var chart = elem.CheckOnNull("elem").ChartArea?.Chart;

            // TONYFIX
            throw new NotImplementedException();
            //IIndicator indicator = chart.TryGetIndicator();
            //if ( indicator != null )
            //    propertyDescriptorList.Add( IndicatorChartSettings.GetPropertyDescriptor(indicator.Name, ( object ) this, indicator) );
        }
        propertyDescriptorList.Add(ChartComponentChartSettings.Create( LocalizedStrings.Style, ( object ) this, ( IChartComponent ) element, new Func<IChartComponent, PropertyDescriptor, bool>( ChartIndicatorElementSettingsObject.StaticBool01 )) );
        propertyDescriptorList.Add(ChartComponentChartSettings.Create( LocalizedStrings.Common, ( object ) this, ( IChartComponent ) element, new Func<IChartComponent, PropertyDescriptor, bool>( ChartIndicatorElementSettingsObject.StaticBool02 )) );
        return propertyDescriptorList.ToArray();
    }

    private void OnPropertyChanged(
#nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2 )
    {
        this.NotifyChanged( _param2.PropertyName );
    }

    internal static bool StaticBool01(
#nullable disable
        IChartComponent _param0,
      PropertyDescriptor _param1 )
    {
        BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
        if ( ( browsableAttribute != null ? ( browsableAttribute.Browsable ? 1 : 0 ) : 1 ) != 0 )
        {
      Attribute0 myAttr = _param1.Attributes.OfType <Attribute0> ().FirstOrDefault <Attribute0> ();
            if ( ( myAttr != null ? ( !myAttr.GetAttributeValue() ? 1 : 0) : 1) != 0)
        return _param0.ParentElement != null || !( _param1.Name == "IsVisible" );
        }
        return false;
    }

    internal static bool StaticBool02(
        IChartComponent _param0,
      PropertyDescriptor _param1 )
    {
        if ( _param0.ParentElement != null )
            return false;
        if ( _param1.Name == "IsVisible" )
      return true;
        BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
        if ( ( browsableAttribute != null ? ( browsableAttribute.Browsable ? 1 : 0 ) : 1 ) == 0 )
            return false;
    Attribute0 myAttr = _param1.Attributes.OfType <Attribute0> ().FirstOrDefault <Attribute0> ();
        return myAttr != null && myAttr.GetAttributeValue();
    }
}
