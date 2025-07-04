using Ecng.ComponentModel;
using SciChart.Charting.Common;
using fx.Charting;
using fx.Charting.Definitions;
using fx.Charting.Ultrachart;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;

internal sealed class ChartElementChartSetting : ChartSettingsObjectBase<IChartElement>
{
    private readonly PdSelector _propertyDescSelector;

    public ChartElementChartSetting( IChartElement chartElement, PdSelector selector = null )
      : base( chartElement )
    {
        _propertyDescSelector = selector;
        Orig.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
    }

    public static PropertyDescriptor Create( string string_0, object obj, IChartElement element, PdSelector pdSelector_1 )
    {
        return new ProxyDescriptorEx( string_0, obj, element, pdSelector_1 );
    }

    protected override PropertyDescriptor[ ] OnGetProperties( IChartElement element )
    {
        var hashSet_0 = new PooledSet<string>( );

        Func<string, string> getCountString = ( s =>
        {
            string str = s;
            int count = 0;

            while ( !hashSet_0.Add( str ) )
            {
                str = s + ( ++count );
            }
                
            return str;
        } );

        var output = element.With( e =>
        {
            return TypeDescriptor.GetProperties( e, false ).OfType<PropertyDescriptor>( ).Where( propD =>
            {
                var mySelector = _propertyDescSelector;

                if ( mySelector != null )
                {
                    if ( ! mySelector( e, propD ) )
                    {
                        return false;
                    }                    
                }
                    
                IElementWithXYAxes elementWithAxis = e as IElementWithXYAxes;
                if ( elementWithAxis == null )
                    return true;
                return !elementWithAxis.AdditionalName( propD.Name );
            }
                                                                                                                    )
            .SelectMany
            (
                propD =>
                {
                    object prop = propD.GetValue( e );

                    if ( prop != null )
                    {
                        IChartElement chartElement = prop as IChartElement;

                        if ( chartElement == null )
                        {
                            IChartIndicatorPainter indicatorPainter = prop as IChartIndicatorPainter;

                            if ( indicatorPainter != null )
                            {

                                return TypeDescriptor.GetProperties( indicatorPainter, false ).OfType<PropertyDescriptor>( ).Where
                                                                                                                             ( pd =>
                                                                                                                             {
                                                                                                                                 var attri = pd.Attributes.OfType<BrowsableAttribute>( ).FirstOrDefault( );
                                                                                                                                 if ( attri == null )
                                                                                                                                     return true;
                                                                                                                                 return attri.Browsable;
                                                                                                                             }
                                                                                                                             ).Select
                                                                                                                             (
                                                                                                                                        pd =>
                                                                                                                                        {
                                                                                                                                            if ( !typeof( IChartElement ).IsAssignableFrom( pd.PropertyType ) )
                                                                                                                                                return pd;

                                                                                                                                            return Create( getCountString( pd.GetDisplayName( ) ), indicatorPainter, ( IChartElement )pd.GetValue( indicatorPainter ), _propertyDescSelector );
                                                                                                                                        }
                                                                                                                             );
                            }
                        }
                        else
                        {

                            PropertyDescriptor[ ] pdArrary = new PropertyDescriptor[ 1 ];
                            int index = 0;

                            IElementWithXYAxes elementWithAxis = chartElement as IElementWithXYAxes;
                            string name;

                            if ( elementWithAxis != null )
                            {
                                name = elementWithAxis.GetName( chartElement );
                            }
                            else
                            {
                                name =   null;
                            }

                            if ( name == null )
                            {
                                name = Extensions.GetDisplayName( propD,   null );
                            }

                            var propertyDescriptor = Create( getCountString( name ),   chartElement, chartElement, _propertyDescSelector );
                            pdArrary[ index ] = propertyDescriptor;
                            return   pdArrary;
                        }
                    }
                    return   new PropertyDescriptor[ 1 ] { propD };
                }
             ).ToArray( );

        } );


        return output;        
    }



    private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
    {
        NotifyChanged( e.PropertyName );
    }






    private sealed class ProxyDescriptorEx : ProxyDescriptor
    {
        public ProxyDescriptorEx( string string_0, object object_1, IChartElement element, PdSelector pdSelector_0 )
          : base( string_0, object_1, element, TypeDescriptor.GetAttributes( element, false ).Cast<Attribute>( ), pdSelector_0 )
        {
        }

        protected override ChartSettingsObjectBase<IChartElement> CreateWrapper( IChartElement obj, PdSelector selector = null )
        {
            return new ChartElementChartSetting( obj, selector );
        }
    }
}
