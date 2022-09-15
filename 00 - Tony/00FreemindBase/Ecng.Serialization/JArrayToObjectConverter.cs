// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JArrayToObjectConverter
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Ecng.Serialization
{
  public class JArrayToObjectConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return true;
    }

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      if (existingValue == null)
        existingValue = Activator.CreateInstance(objectType);
      JArray jarray = JArray.Load(reader);
      FieldInfo[] fields = objectType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < fields.Length; ++index)
      {
        FieldInfo fieldInfo = fields[index];
        JToken jtoken = jarray[index];
        fieldInfo.SetValue(existingValue, jtoken.ToObject(fieldInfo.FieldType));
      }
      return existingValue;
    }

    public override bool CanWrite
    {
      get
      {
        return false;
      }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotSupportedException();
    }
  }
}
