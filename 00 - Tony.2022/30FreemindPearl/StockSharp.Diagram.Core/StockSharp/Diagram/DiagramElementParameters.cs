using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>Current values of diagram element parameters.</summary>
    [DisplayNameLoc( "Str3057" )]
    public sealed class DiagramElementParameters : ICustomTypeDescriptor
    {
        
        private readonly DiagramElement _diagramElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramElementParameters" />.
        /// </summary>
        /// <param name="element">The diagram element.</param>
        public DiagramElementParameters( DiagramElement element )
        {
            if ( element == null )
                throw new ArgumentNullException( "element == null" );

            _diagramElement = element;
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetAttributes();
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetClassName();
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetComponentName();
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetConverter();
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetDefaultEvent();
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetDefaultProperty();
        }

        object ICustomTypeDescriptor.GetEditor( Type _param1 )
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetEditor( _param1 );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetEvents();
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[ ] _param1 )
        {
            return ( ( ICustomTypeDescriptor )_diagramElement ).GetEvents( _param1 );
        }

        private PropertyDescriptorCollection GetProperties()
        {
            var first  = _diagramElement.Parameters.Select( x => new DiagramElementProperties( LocalizedStrings.Str225, x ) ).Cast<PropertyDescriptor>();
            var second = _diagramElement.InputSockets.Select( x => new DiagramElementParametersProperty( LocalizedStrings.Input, x ) );
            var third  = _diagramElement.OutputSockets.Select( x => new DiagramElementParametersProperty( LocalizedStrings.Output, x ) );
            
            return new PropertyDescriptorCollection( first.Concat( second ).Concat( third ).ToArray() );
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return GetProperties();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[ ] _param1 )
        {
            return GetProperties();
        }

        object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor _param1 )
        {
            return _diagramElement;
        }
    }
}
