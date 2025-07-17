// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Extensions.XElementExtensions
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Xml.Linq;

namespace StockSharp.Xaml.Charting.Common.Extensions
{
    internal static class XElementExtensions
    {
        internal static string GetOptionalAttributeValue( this XElement element, string attrName )
        {
            return element.Attribute( ( XName ) attrName )?.Value;
        }

        internal static string GetRequiredAttributeValue( this XElement element, string attributeName )
        {
            XAttribute xattribute = element.Attribute((XName) attributeName);
            if ( xattribute == null )
                throw new InvalidOperationException( string.Format( "Expected attribute <{0}/>", ( object ) attributeName ) );
            return xattribute.Value;
        }

        internal static string GetRequiredElementValue( this XElement element, string elementName )
        {
            XElement xelement = element.Element((XName) elementName);
            if ( xelement == null )
                throw new InvalidOperationException( string.Format( "Expected element <{0}/>", ( object ) elementName ) );
            return xelement.Value;
        }

        internal static string GetOptionalElementValue( this XElement element, string elementName )
        {
            return element.Element( ( XName ) elementName )?.Value;
        }
    }
}
