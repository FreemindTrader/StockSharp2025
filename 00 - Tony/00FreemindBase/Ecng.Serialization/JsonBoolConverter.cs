// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JsonBoolConverter
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Newtonsoft.Json;
using System;

namespace Ecng.Serialization
{
  public class JsonBoolConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue((bool) value ? 1 : 0);
    }

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      return (object) (reader.Value.ToString() == "1");
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof (bool);
    }
  }
}
