// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.ISerializer
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
  public interface ISerializer
  {
    Type Type { get; }

    Serializer<T> GetSerializer<T>();

    ISerializer GetSerializer(Type entityType);

    string FileExtension { get; }

    ValueTask SerializeAsync(
      object graph,
      Stream stream,
      CancellationToken cancellationToken);

    ValueTask<object> DeserializeAsync(Stream stream, CancellationToken cancellationToken);
  }
}
