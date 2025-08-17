// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IVideoService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IVideoService
{
    Task UpdateInfoAsync(
      long clientId,
      long fileId,
      string path,
      long length,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<(string path, long length)> GetInfoAsync(long fileId, CancellationToken cancellationToken = default(CancellationToken));
}
