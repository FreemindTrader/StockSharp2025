using DevExpress.Xpf.PropertyGrid;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;

namespace fx.Charting.Ultrachart
{
    public class IndicatorUISettingsObject : ChartSettingsObjectBase< IChartElement >
    {
        public IndicatorUISettingsObject( IChartElement element ) : base( element )
        {
            CategoriesMode = CategoriesShowMode.Hidden;
            Orig.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
        }

        protected override PropertyDescriptor[ ] OnGetProperties( IChartElement element )
        {
            PooledList< PropertyDescriptor > propertyDescriptorList = new PooledList< PropertyDescriptor >( );

            if( element is IndicatorUI indicatorElement )
            {
                IIndicator indicator = element.Chart?.GetIndicator( indicatorElement );

                if( indicator != null )
                {
                    propertyDescriptorList.Add( IndicatorChartSettings.GetPropertyDescriptor( indicator.Name, this, indicator ) );
                }
            }

            var myStyle = new PdSelector( ( e, p ) =>
            {
                BrowsableAttribute browse = p.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );

                if ( ( browse != null ? ( browse.Browsable ? 1 : 0 ) : 1 ) != 0 )
                {
                    Attribute0 attr = p.Attributes.OfType<Attribute0>( ).FirstOrDefault( );
                    if ( ( attr != null ? ( !attr.GetAttributeValue( ) ? 1 : 0 ) : 1 ) != 0 )
                    {
                        if ( e.ParentElement == null )
                        {
                            return !( p.Name == "IsVisible" );
                        }
                        return true;
                    }
                }

                return false;
            } );

            var browsableAndVisible = new PdSelector( ( e, p ) =>
            {
                if ( e.ParentElement != null )
                {
                    return false;
                }

                if ( p.Name == "IsVisible" )
                {
                    return true;
                }

                var browse = p.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );

                if ( ( browse != null ? ( browse.Browsable ? 1 : 0 ) : 1 ) == 0 )
                {
                    return false;
                }
                var attribute0 = p.Attributes.OfType<Attribute0>( ).FirstOrDefault( );

                if ( attribute0 == null )
                {
                    return false;
                }
                return attribute0.GetAttributeValue( );
            } );

            propertyDescriptorList.Add( ChartElementChartSetting.Create( "LocalizedStrings.Str1946", this, element, myStyle ));
            propertyDescriptorList.Add( ChartElementChartSetting.Create( "LocalizedStrings.Str1946", this, element, browsableAndVisible ) );

            return propertyDescriptorList.ToArray( );
        }

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            NotifyChanged( e.PropertyName );
        }        
    }
}
