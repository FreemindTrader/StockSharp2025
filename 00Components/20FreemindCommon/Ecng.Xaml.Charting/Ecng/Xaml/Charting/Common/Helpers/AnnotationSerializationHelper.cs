// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Helpers.AnnotationSerializationHelper
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Xml;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers.XmlSerialization;
using StockSharp.Xaml.Charting.Visuals.Annotations;

namespace StockSharp.Xaml.Charting.Common.Helpers
{
    internal sealed class AnnotationSerializationHelper : SerializationHelper<IAnnotation>
    {
        private static AnnotationSerializationHelper _instance;

        internal static AnnotationSerializationHelper Instance
        {
            get
            {
                return AnnotationSerializationHelper._instance ?? ( AnnotationSerializationHelper._instance = new AnnotationSerializationHelper() );
            }
        }

        private AnnotationSerializationHelper()
        {
            this.BaseProperties = new string[ 14 ]
            {
        "IsEditable",
        "IsHidden",
        "IsSelected",
        "XAxisId",
        "YAxisId",
        "CoordinateMode",
        "Background",
        "BorderBrush",
        "BorderThickness",
        "Foreground",
        "FontSize",
        "FontWeight",
        "FontFamily",
        "FontStyle"
            };
            this.AddittionalPropertiesDictionary = new Dictionary<Type, string[ ]>()
      {
        {
          typeof (TextAnnotation),
          new string[2]{ "Text", "TextAlignment" }
        },
        {
          typeof (LineAnnotationBase),
          new string[2]{ "Stroke", "StrokeThickness" }
        },
        {
          typeof (LineArrowAnnotation),
          new string[2]{ "HeadWidth", "HeadLength" }
        },
        {
          typeof (LineAnnotationWithLabelsBase),
          new string[2]{ "ShowLabel", "LabelPlacement" }
        },
        {
          typeof (HorizontalLineAnnotation),
          new string[1]{ "HorizontalAlignment" }
        },
        {
          typeof (VerticalLineAnnotation),
          new string[2]{ "LabelsOrientation", "VerticalAlignment" }
        }
      };
        }

        public override void DeserializeProperties( IAnnotation element, XmlReader reader )
        {
            base.DeserializeProperties( element, reader );
            string typeName1 = reader["XType"];
            if ( typeName1 != null )
            {
                Type type = Type.GetType(typeName1);
                element.X1 = ( IComparable ) reader.GetValue( "X1", type );
                element.X2 = ( IComparable ) reader.GetValue( "X2", type );
            }
            string typeName2 = reader["YType"];
            if ( typeName2 == null )
                return;
            Type type1 = Type.GetType(typeName2);
            element.Y1 = ( IComparable ) reader.GetValue( "Y1", type1 );
            element.Y2 = ( IComparable ) reader.GetValue( "Y2", type1 );
        }

        public override void SerializeProperties( IAnnotation element, XmlWriter writer )
        {
            base.SerializeProperties( element, writer );
            if ( element.X1 != null )
            {
                Type type = element.X1.GetType();
                writer.WriteAttributeString( "XType", type.ToTypeString() );
                writer.SerializeProperty( ( object ) element, "X1" );
                writer.SerializeProperty( ( object ) element, "X2" );
            }
            if ( element.Y1 == null )
                return;
            Type type1 = element.Y1.GetType();
            writer.WriteAttributeString( "YType", type1.ToTypeString() );
            writer.SerializeProperty( ( object ) element, "Y1" );
            writer.SerializeProperty( ( object ) element, "Y2" );
        }
    }
}
