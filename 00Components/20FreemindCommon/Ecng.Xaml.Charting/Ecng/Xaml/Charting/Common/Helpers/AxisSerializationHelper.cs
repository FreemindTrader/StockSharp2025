// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Helpers.AxisSerializationHelper
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Xml;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers.XmlSerialization;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.Common.Helpers
{
    internal sealed class AxisSerializationHelper : SerializationHelper<AxisBase>
    {
        private static AxisSerializationHelper _instance;

        internal static AxisSerializationHelper Instance
        {
            get
            {
                return AxisSerializationHelper._instance ?? ( AxisSerializationHelper._instance = new AxisSerializationHelper() );
            }
        }

        private AxisSerializationHelper()
        {
            this.BaseProperties = new string[ 16 ]
            {
        "AutoRange",
        "AutoTicks",
        "AxisAlignment",
        "DrawMajorBands",
        "DrawMajorTicks",
        "DrawLabels",
        "DrawMinorTicks",
        "DrawMajorGridLines",
        "DrawMinorGridLines",
        "AxisTitle",
        "VisibleRange",
        "Id",
        "GrowBy",
        "MinorsPerMajor",
        "MaxAutoTicks",
        "FlipCoordinates"
            };
        }

        public override void DeserializeProperties( AxisBase element, XmlReader reader )
        {
            base.DeserializeProperties( element, reader );
            string typeName = reader["DeltaType"];
            if ( typeName == null )
                return;
            Type type = Type.GetType(typeName);
            element.MajorDelta = ( IComparable ) reader.GetValue( "MajorDelta", type );
            element.MinorDelta = ( IComparable ) reader.GetValue( "MinorDelta", type );
        }

        public override void SerializeProperties( AxisBase element, XmlWriter writer )
        {
            base.SerializeProperties( element, writer );
            AxisBase axisBase = element;
            if ( axisBase == null || axisBase.AutoTicks || ( element.MinorDelta == null || element.MajorDelta == null ) )
                return;
            Type type = element.MajorDelta.GetType();
            writer.WriteAttributeString( "DeltaType", type.ToTypeString() );
            writer.SerializeProperty( ( object ) element, "MinorDelta" );
            writer.SerializeProperty( ( object ) element, "MajorDelta" );
        }
    }
}
