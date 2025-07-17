// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Helpers.XmlSerialization.SerializationHelper`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
namespace fx.Xaml.Charting
{
    internal abstract class SerializationHelper<T> where T : IXmlSerializable
    {
        protected string[] BaseProperties = new string[0];
        protected Dictionary<Type, string[]> AddittionalPropertiesDictionary;

        public virtual void SerializeProperties( T element, XmlWriter writer )
        {
            foreach ( string baseProperty in this.BaseProperties )
                writer.SerializeProperty( ( object ) element, baseProperty );
            if ( AddittionalPropertiesDictionary == null )
                return;
            foreach ( string propertyName in this.AddittionalPropertiesDictionary.Where<KeyValuePair<Type, string[ ]>>( ( Func<KeyValuePair<Type, string[ ]>, bool> ) ( x => x.Key.IsInstanceOfType( ( object ) element ) ) ).SelectMany<KeyValuePair<Type, string[ ]>, string>( ( Func<KeyValuePair<Type, string[ ]>, IEnumerable<string>> ) ( x => ( IEnumerable<string> ) x.Value ) ) )
                writer.SerializeProperty( ( object ) element, propertyName );
        }

        public virtual void DeserializeProperties( T element, XmlReader reader )
        {
            foreach ( string baseProperty in this.BaseProperties )
                reader.DeserilizeProperty( ( object ) element, baseProperty );
            if ( AddittionalPropertiesDictionary == null )
                return;
            foreach ( string propertyName in this.AddittionalPropertiesDictionary.Where<KeyValuePair<Type, string[ ]>>( ( Func<KeyValuePair<Type, string[ ]>, bool> ) ( x => x.Key.IsInstanceOfType( ( object ) element ) ) ).SelectMany<KeyValuePair<Type, string[ ]>, string>( ( Func<KeyValuePair<Type, string[ ]>, IEnumerable<string>> ) ( x => ( IEnumerable<string> ) x.Value ) ) )
                reader.DeserilizeProperty( ( object ) element, propertyName );
        }

        public void SerializeCollection( IEnumerable<T> collection, XmlWriter writer )
        {
            foreach ( T obj in collection )
            {
                Type type = obj.GetType();
                writer.WriteStartElement( type.Name );
                writer.WriteAttributeString( "Type", type.ToTypeString() );
                obj.WriteXml( writer );
                writer.WriteEndElement();
            }
        }

        public IEnumerable<T> DeserializeCollection( XmlReader reader )
        {
            if ( reader.MoveToContent() == XmlNodeType.Element && !reader.IsEmptyElement && reader.Read() )
            {
                while ( reader.MoveToContent() == XmlNodeType.Element )
                {
                    T instance = (T) Activator.CreateInstance(Type.GetType(reader["Type"]));
                    instance.ReadXml( reader );
                    yield return instance;
                    reader.Read();
                }
            }
        }
    }
}
