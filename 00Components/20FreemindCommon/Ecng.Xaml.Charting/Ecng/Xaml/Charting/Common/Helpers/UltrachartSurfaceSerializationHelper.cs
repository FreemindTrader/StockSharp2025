// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Helpers.UltrachartSurfaceSerializationHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Ecng.Xaml.Charting.ChartModifiers;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Common.Helpers.XmlSerialization;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Annotations;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.Common.Helpers
{
    internal class UltrachartSurfaceSerializationHelper : SerializationHelper<UltrachartSurface>
    {
        private static UltrachartSurfaceSerializationHelper _instance;

        internal static UltrachartSurfaceSerializationHelper Instance
        {
            get
            {
                return UltrachartSurfaceSerializationHelper._instance ?? ( UltrachartSurfaceSerializationHelper._instance = new UltrachartSurfaceSerializationHelper() );
            }
        }

        private UltrachartSurfaceSerializationHelper()
        {
            this.BaseProperties = new string[ 5 ]
            {
        "MaxFrameRate",
        "ClipOverlayAnnotations",
        "ClipUnderlayAnnotations",
        "ClipModifierSurface",
        "ChartTitle"
            };
        }

        public override void SerializeProperties( UltrachartSurface element, XmlWriter writer )
        {
            base.SerializeProperties( element, writer );
            string theme = ThemeManager.GetTheme((DependencyObject) element);
            if ( theme != null )
            {
                writer.WriteAttributeString( "Theme", theme );
            }

            string renderSurfaceType = RenderSurfaceBase.GetRenderSurfaceType((UIElement) element);
            if ( renderSurfaceType != null )
            {
                writer.WriteAttributeString( "RenderSurfaceType", renderSurfaceType );
            }

            UltrachartSurfaceSerializationHelper.SerilalizeRenderableSeries( ( IEnumerable<IRenderableSeries> ) element.RenderableSeries, writer );
            UltrachartSurfaceSerializationHelper.SerializeElement<AxisCollection>( element.XAxes, "XAxes", writer );
            UltrachartSurfaceSerializationHelper.SerializeElement<AxisCollection>( element.YAxes, "YAxes", writer );
            UltrachartSurfaceSerializationHelper.SerializeElement<AnnotationCollection>( element.Annotations, "Annotations", writer );
            UltrachartSurfaceSerializationHelper.SerializeElement<ChartModifierBase>( ( ChartModifierBase ) element.ChartModifier, "ChartModifier", writer );
        }

        public override void DeserializeProperties( UltrachartSurface element, XmlReader reader )
        {
            base.DeserializeProperties( element, reader );
            string str1 = reader["Theme"];
            if ( str1 != null )
            {
                ThemeManager.SetTheme( ( DependencyObject ) element, str1 );
            }

            string str2 = reader["RenderSurfaceType"];
            if ( str2 != null )
            {
                RenderSurfaceBase.SetRenderSurfaceType( ( UIElement ) element, str2 );
            }

            if ( !reader.Read() )
            {
                return;
            }

            while ( reader.MoveToContent() == XmlNodeType.Element )
            {
                string localName = reader.LocalName;
                if ( localName == "RenderableSeries" )
                {
                    ObservableCollection<IRenderableSeries> instance = (ObservableCollection<IRenderableSeries>) Activator.CreateInstance(typeof (ObservableCollection<IRenderableSeries>));
                    IEnumerable<IRenderableSeries> values = RenderableSeriesSerializationHelper.Instance.DeserializeCollection(reader);
                    instance.AddRange<IRenderableSeries>( values );
                    element.RenderableSeries = instance;
                }
                else
                {
                    IXmlSerializable instance = (IXmlSerializable) Activator.CreateInstance(Type.GetType(reader["Type"]));
                    instance.ReadXml( reader );
                    element.GetType().GetProperty( localName ).SetValue( ( object ) element, ( object ) instance, ( object[ ] ) null );
                }
                reader.Read();
            }
        }

        private static void SerializeElement<T>( T element, string elementName, XmlWriter writer ) where T : IXmlSerializable
        {
            if ( ( object ) element == null )
            {
                return;
            }

            writer.WriteStartElement( elementName );
            writer.WriteAttributeString( "Type", element.GetType().ToTypeString() );
            element.WriteXml( writer );
            writer.WriteEndElement();
        }

        private static void SerilalizeRenderableSeries( IEnumerable<IRenderableSeries> renderableSeries, XmlWriter writer )
        {
            if ( renderableSeries == null )
            {
                return;
            }

            writer.WriteStartElement( "RenderableSeries" );
            RenderableSeriesSerializationHelper.Instance.SerializeCollection( renderableSeries, writer );
            writer.WriteEndElement();
        }
    }
}
