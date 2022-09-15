// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JsonDateTimeConverter
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Common;
using Newtonsoft.Json;
using System;

namespace Ecng.Serialization
{
  public class JsonDateTimeConverter : JsonConverter
  {
    private readonly bool _isSeconds;

    public JsonDateTimeConverter()
      : this(true)
    {
    }

    public JsonDateTimeConverter(bool isSeconds)
    {
      this._isSeconds = isSeconds;
    }

    public override bool CanConvert(Type objectType)
    {
      return typeof (DateTime) == objectType;
    }

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      try
      {
        double? nullable = reader.Value.To<double?>();
        if (!nullable.HasValue)
          return (object) null;
        return (object) this.Convert(nullable.Value);
      }
      catch (Exception ex)
      {
        throw new JsonReaderException(ex.Message, ex);
      }
    }

    protected virtual DateTime Convert(double value)
    {
      return value.FromUnix(this._isSeconds);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotSupportedException();
    }
  }
}
