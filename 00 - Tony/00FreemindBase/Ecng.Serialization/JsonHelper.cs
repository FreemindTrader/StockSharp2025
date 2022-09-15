// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JsonHelper
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
  public static class JsonHelper
  {
    public static readonly Encoding UTF8NoBom = (Encoding) new UTF8Encoding(false);

    [Conditional("DEBUG")]
    public static void ChechExpectedToken(this JsonReader reader, JsonToken token)
    {
      if (reader.TokenType != token)
        throw new InvalidOperationException(string.Format("{0} != {1}", (object) reader.TokenType, (object) token));
    }

    public static async Task ReadWithCheckAsync(
      this JsonReader reader,
      CancellationToken cancellationToken)
    {
      if (!await reader.ReadAsync(cancellationToken))
        throw new InvalidOperationException("EOF");
    }

    public static T DeserializeObject<T>(this string content)
    {
      return (T) content.DeserializeObject(typeof (T));
    }

    public static T DeserializeObject<T>(this JToken token)
    {
      return (T) token.DeserializeObject(typeof (T));
    }

    public static object DeserializeObject(this string content, Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if (content.IsEmpty())
      {
        if (type.IsClass || type.IsNullable())
          return (object) null;
        throw new ArgumentNullException(nameof (content));
      }
      try
      {
        if (content == "null")
          return (object) null;
        return JsonConvert.DeserializeObject(content, type);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Can't convert " + content + " to type '" + type.Name + "'.", ex);
      }
    }

    public static object DeserializeObject(this JToken token, Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if (token == null)
      {
        if (type.IsClass || type.IsNullable())
          return (object) null;
        throw new ArgumentNullException(nameof (token));
      }
      try
      {
        if (token.Type == JTokenType.String && (string) token == "null")
          return (object) null;
        return token.ToObject(type);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(string.Format("Can't convert {0} to type '{1}'.", (object) token, (object) type.Name), ex);
      }
    }

    public static JsonSerializerSettings CreateJsonSerializerSettings()
    {
      return new JsonSerializerSettings()
      {
        FloatParseHandling = FloatParseHandling.Decimal,
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = (IContractResolver) new DefaultContractResolver()
        {
          NamingStrategy = (NamingStrategy) new SnakeCaseNamingStrategy()
        }
      };
    }

    [Obsolete]
    public static JsonWriter WriteProperty(
      this JsonWriter writer,
      string name,
      object value)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      writer.WritePropertyName(name);
      writer.WriteValue(value);
      return writer;
    }
  }
}
