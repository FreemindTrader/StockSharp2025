// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.IAsyncPersistable
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using System.Threading;
using System.Threading.Tasks;

namespace Ecng.Serialization
{
  public interface IAsyncPersistable
  {
    Task LoadAsync(SettingsStorage storage, CancellationToken cancellationToken);

    Task SaveAsync(SettingsStorage storage, CancellationToken cancellationToken);
  }
}
