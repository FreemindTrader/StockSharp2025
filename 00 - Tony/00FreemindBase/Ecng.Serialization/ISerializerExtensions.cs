// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.ISerializerExtensions
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
  public static class ISerializerExtensions
  {
    public static void Serialize(this ISerializer serializer, object graph, string fileName)
    {
      File.WriteAllBytes(fileName, serializer.CheckOnNull<ISerializer>(nameof (serializer)).Serialize(graph));
    }

    public static byte[] Serialize(this ISerializer serializer, object graph)
    {
      MemoryStream memoryStream = new MemoryStream();
      serializer.CheckOnNull<ISerializer>(nameof (serializer)).Serialize(graph, (Stream) memoryStream);
      return memoryStream.To<byte[]>();
    }

    public static void Serialize(this ISerializer serializer, object graph, Stream stream)
    {
      ThreadingHelper.Run((Func<ValueTask>) (() => serializer.CheckOnNull<ISerializer>(nameof (serializer)).SerializeAsync(graph, stream, new CancellationToken())));
    }

    public static T Deserialize<T>(this ISerializer<T> serializer, string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        return serializer.CheckOnNull<ISerializer<T>>(nameof (serializer)).Deserialize<T>((Stream) fileStream);
    }

    public static T Deserialize<T>(this ISerializer<T> serializer, byte[] data)
    {
      MemoryStream memoryStream = new MemoryStream(data);
      return serializer.CheckOnNull<ISerializer<T>>(nameof (serializer)).Deserialize<T>((Stream) memoryStream);
    }

    public static T Deserialize<T>(this ISerializer<T> serializer, Stream stream)
    {
      return ThreadingHelper.Run<T>((Func<ValueTask<T>>) (() => serializer.CheckOnNull<ISerializer<T>>(nameof (serializer)).DeserializeAsync(stream, new CancellationToken())));
    }

    public static object Deserialize(this ISerializer serializer, byte[] data)
    {
      MemoryStream memoryStream = new MemoryStream(data);
      return serializer.CheckOnNull<ISerializer>(nameof (serializer)).Deserialize((Stream) memoryStream);
    }

    public static object Deserialize(this ISerializer serializer, Stream stream)
    {
      return ThreadingHelper.Run<object>((Func<ValueTask<object>>) (() => serializer.CheckOnNull<ISerializer>(nameof (serializer)).DeserializeAsync(stream, new CancellationToken())));
    }
  }
}
