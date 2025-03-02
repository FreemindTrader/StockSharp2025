// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IVideoService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IVideoService
    {
        Task UpdateInfoAsync(
          long clientId,
          long fileId,
          string path,
          long length,
          CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(string path, long length)> GetInfoAsync( long fileId, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
