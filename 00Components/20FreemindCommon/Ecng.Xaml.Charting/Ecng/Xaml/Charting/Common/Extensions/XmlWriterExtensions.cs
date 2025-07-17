// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.XmlWriterExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace Ecng.Xaml.Charting
{
    internal static class XmlWriterExtensions
    {
        public static void SerializeProperty( this XmlWriter writer, object element, string propertyName )
        {
            object propertyValue = ((IEnumerable<PropertyInfo>) element.GetType().GetProperties()).First<PropertyInfo>((Func<PropertyInfo, bool>) (property => property.Name == propertyName)).GetValue(element, (object[]) null);
            if ( propertyValue == null )
            {
                return;
            }

            string valueString = XmlWriterExtensions.GetValueString(propertyValue);
            writer.WriteAttributeString( propertyName, valueString );
        }

        private static string GetValueString( object propertyValue )
        {
            string str;
            if ( propertyValue is Brush )
            {
                Color color = ((Brush) propertyValue).ExtractColor();
                str = string.Format( "{0:X},{1:X},{2:X},{3:X}", ( object ) color.A, ( object ) color.R, ( object ) color.G, ( object ) color.B );
            }
            else if ( propertyValue is Color )
            {
                Color color = (Color) propertyValue;
                str = string.Format( "{0:X},{1:X},{2:X},{3:X}", ( object ) color.A, ( object ) color.R, ( object ) color.G, ( object ) color.B );
            }
            else if ( propertyValue is IRange )
            {
                Type type = propertyValue.GetType();
                DoubleRange doubleRange = ((IRange) propertyValue).AsDoubleRange();
                str = string.Format( "{0},{1},{2}", ( object ) type.FullName, ( object ) doubleRange.Min, ( object ) doubleRange.Max );
            }
            else if ( propertyValue is Thickness )
            {
                Thickness thickness = (Thickness) propertyValue;
                str = string.Format( "{0},{1},{2},{3}", ( object ) thickness.Left, ( object ) thickness.Top, ( object ) thickness.Right, ( object ) thickness.Bottom );
            }
            else
            {
                str = propertyValue.ToString();
            }

            return str;
        }
    }
}
