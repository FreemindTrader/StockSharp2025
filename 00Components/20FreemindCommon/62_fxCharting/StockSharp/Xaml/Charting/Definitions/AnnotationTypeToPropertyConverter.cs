using DevExpress.Xpf.PropertyGrid;
using SciChart.Charting.Common;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Definitions;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections;
using System.Collections.Generic; 
using fx.Collections;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Data;
using System.Windows.Media;
using StockSharp.Charting;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    internal sealed class AnnotationTypeToPropertyConverter : IValueConverter
    {
        private static readonly PropertyGridEx _propertyGridEx = new PropertyGridEx( );
        private readonly PooledDictionary<ChartAnnotationTypes, PooledList<PropertyDefinition>> _annotationTypesToProperty = new PooledDictionary<ChartAnnotationTypes, PooledList<PropertyDefinition>>( );

        private PooledList<PropertyDefinition> GetPropertyDefinitionList( ChartAnnotationTypes annotationType )
        {
            PropertyDefinitionStruct definitionStruct;
            definitionStruct.PropertyDefinitionList = new PooledList<PropertyDefinition>( );
            AddPropertyDefinition( "Stroke", typeof( Brush ), ref definitionStruct );
            AddPropertyDefinition( "StrokeThickness", typeof( double ), ref definitionStruct );
            AddPropertyDefinition( "ShowLabel", null, ref definitionStruct );
            AddPropertyDefinition( "LabelPlacement", typeof( Enum ), ref definitionStruct );

            switch ( annotationType )
            {
                case ChartAnnotationTypes.TextAnnotation:
                AddPropertyDefinition( "Text", null, ref definitionStruct );
                AddPropertyDefinition( "Foreground", typeof( Brush ), ref definitionStruct );
                AddPropertyDefinition( "Background", typeof( Brush ), ref definitionStruct );
                AddPropertyDefinition( "BorderBrush", typeof( Brush ), ref definitionStruct );
                AddPropertyDefinition( "BorderThickness", null, ref definitionStruct );
                break;
                case ChartAnnotationTypes.BoxAnnotation:
                AddPropertyDefinition( "BorderBrush", typeof( Brush ), ref definitionStruct );
                AddPropertyDefinition( "BorderThickness", null, ref definitionStruct );
                AddPropertyDefinition( "Background", typeof( Brush ), ref definitionStruct );
                break;
                case ChartAnnotationTypes.HorizontalLineAnnotation:
                AddPropertyDefinition( "HorizontalAlignment", typeof( Enum ), ref definitionStruct );
                break;
                case ChartAnnotationTypes.VerticalLineAnnotation:
                AddPropertyDefinition( "VerticalAlignment", typeof( Enum ), ref definitionStruct );
                break;
            }
            return definitionStruct.PropertyDefinitionList;
        }

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            AnnotationBase annotation = value as AnnotationBase;

            if ( annotation == null )
            {
                return null;
            }

            var annotationType = AnnotationExtensionHelper.GetType( annotation );

            PooledList<PropertyDefinition> propertyDefinitionList;

            if ( !_annotationTypesToProperty.TryGetValue( annotationType, out propertyDefinitionList ) )
            {
                _annotationTypesToProperty[ annotationType ] = propertyDefinitionList = GetPropertyDefinitionList( annotationType );
            }

            return propertyDefinitionList;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException( );
        }

        internal static void AddPropertyDefinition( string propertyName, Type propertyType, ref PropertyDefinitionStruct definitionStruct )
        {
            var definition = new PropertyDefinition( );
            definition.Path = propertyName;
            definition.PostOnEditValueChanged = true;

            if ( propertyType != null )
            {
                if ( propertyType == typeof( double ) )
                {
                    propertyType = typeof( Decimal );
                }

                Maybe.Do( _propertyGridEx.PropertyDefinitions.OfType<PropertyDefinition>( ).FirstOrDefault( p => p.Type == propertyType ), d => definition.CellTemplate = d.CellTemplate );
            }
            definitionStruct.PropertyDefinitionList.Add( definition );
        }

        [StructLayout( LayoutKind.Auto )]
        public struct PropertyDefinitionStruct
        {
            public PooledList<PropertyDefinition> PropertyDefinitionList;
        }
    }
}
