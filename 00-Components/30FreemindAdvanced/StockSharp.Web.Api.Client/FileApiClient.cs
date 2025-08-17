// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.FileApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class FileApiClient :
  BaseApiEntityClient<File>,
  IFileService,
  IBaseEntityService<File>,
  IVideoService
{
    public FileApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public FileApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<string> IFileService.StartUploadAsync(
      File file,
      Compressions? compression,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("StartUploadAsync"), cancellationToken, new object[2]
        {
      (object) file,
      (object) compression
        });
    }

    Task IFileService.UploadAsync(
      string operationId,
      byte[] bodyPart,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("UploadAsync"), cancellationToken, new object[2]
        {
      (object) operationId,
      (object) bodyPart
        });
    }

    Task<File> IFileService.FinishUploadAsync(
      string operationId,
      bool isCancel,
      CancellationToken cancellationToken)
    {
        return this.Post<File>(RestBaseApiClient.GetCurrentMethod("FinishUploadAsync"), cancellationToken, new object[2]
        {
      (object) operationId,
      (object) isCancel
        });
    }

    Task IFileService.ClearTempsAsync(
      bool? uploads,
      bool? downloads,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ClearTempsAsync"), cancellationToken, new object[2]
        {
      (object) uploads,
      (object) downloads
        });
    }

    Task IFileService.RemoveTempAsync(string operationId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RemoveTempAsync"), cancellationToken, new object[1]
        {
      (object) operationId
        });
    }

    Task<File> IFileService.GetTempAsync(string operationId, CancellationToken cancellationToken)
    {
        return this.Get<File>(RestBaseApiClient.GetCurrentMethod("GetTempAsync"), cancellationToken, new object[1]
        {
      (object) operationId
        });
    }

    Task<string> IFileService.StartDownloadAsync(
      long? fileId,
      long? bodyId,
      bool? isSnapshot,
      Compressions? compression,
      int? width,
      int? height,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("StartDownloadAsync"), cancellationToken, new object[6]
        {
      (object) fileId,
      (object) bodyId,
      (object) isSnapshot,
      (object) compression,
      (object) width,
      (object) height
        });
    }

    Task<byte[]> IFileService.GetPartAsync(
      string operationId,
      long skip,
      long? count,
      CancellationToken cancellationToken)
    {
        return this.Get<byte[]>(RestBaseApiClient.GetCurrentMethod("GetPartAsync"), cancellationToken, new object[3]
        {
      (object) operationId,
      (object) skip,
      (object) count
        });
    }

    Task<byte[]> IFileService.GetBodyAsync(
      long fileId,
      long skip,
      long? count,
      Compressions? compression,
      int? width,
      int? height,
      CancellationToken cancellationToken)
    {
        return this.Get<byte[]>(RestBaseApiClient.GetCurrentMethod("GetBodyAsync"), cancellationToken, new object[6]
        {
      (object) fileId,
      (object) skip,
      (object) count,
      (object) compression,
      (object) width,
      (object) height
        });
    }

    Task<byte[]> IFileService.GetVersionBodyAsync(
      long bodyId,
      bool? isSnapshot,
      long skip,
      long? count,
      Compressions? compression,
      int? width,
      int? height,
      CancellationToken cancellationToken)
    {
        return this.Get<byte[]>(RestBaseApiClient.GetCurrentMethod("GetVersionBodyAsync"), cancellationToken, new object[7]
        {
      (object) bodyId,
      (object) isSnapshot,
      (object) skip,
      (object) count,
      (object) compression,
      (object) width,
      (object) height
        });
    }

    Task<FileBody> IFileService.GetFileBodyAsync(long bodyId, CancellationToken cancellationToken)
    {
        return this.Get<FileBody>(RestBaseApiClient.GetCurrentMethod("GetFileBodyAsync"), cancellationToken, new object[1]
        {
      (object) bodyId
        });
    }

    Task<File> IFileService.GetFileByBodyAsync(long bodyId, CancellationToken cancellationToken)
    {
        return this.Get<File>(RestBaseApiClient.GetCurrentMethod("GetFileByBodyAsync"), cancellationToken, new object[1]
        {
      (object) bodyId
        });
    }

    Task<BaseEntitySet<File>> IFileService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? groupId,
      long? messageId,
      long? draftId,
      long? emailId,
      bool? tempUploads,
      bool? tempDownloads,
      bool? cloud,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<File>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[18]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId,
      (object) groupId,
      (object) messageId,
      (object) draftId,
      (object) emailId,
      (object) tempUploads,
      (object) tempDownloads,
      (object) cloud,
      (object) like,
      (object) likeCompare
        });
    }

    Task<BaseEntitySet<FileBody>> IFileService.FindVersionsAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? fileId,
      long? groupId,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<FileBody>>(RestBaseApiClient.GetCurrentMethod("FindVersionsAsync"), cancellationToken, new object[10]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) fileId,
      (object) groupId
        });
    }

    Task<BaseEntitySet<NugetSpecification>> IFileService.GetNugetPackagesAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<NugetSpecification>>(RestBaseApiClient.GetCurrentMethod("GetNugetPackagesAsync"), cancellationToken, new object[6]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount
        });
    }

    Task<FileBody> IFileService.AddNugetPackageAsync(
      byte[] package,
      CancellationToken cancellationToken)
    {
        return this.Post<FileBody>(RestBaseApiClient.GetCurrentMethod("AddNugetPackageAsync"), cancellationToken, new object[1]
        {
      (object) package
        });
    }

    Task<File> IFileService.GetAvatarAsync(long clientId, CancellationToken cancellationToken)
    {
        return this.Get<File>(RestBaseApiClient.GetCurrentMethod("GetAvatarAsync"), cancellationToken, new object[1]
        {
      (object) clientId
        });
    }

    Task IFileService.AddGroupAsync(long fileId, long groupId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddGroupAsync"), cancellationToken, new object[2]
        {
      (object) fileId,
      (object) groupId
        });
    }

    Task<bool> IFileService.RemoveGroupAsync(
      long fileId,
      long groupId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveGroupAsync"), cancellationToken, new object[2]
        {
      (object) fileId,
      (object) groupId
        });
    }

    Task<int> IFileService.ClearLogsAsync(
      int daysFrom,
      int daysTo,
      int maxCount,
      CancellationToken cancellationToken)
    {
        return this.Post<int>(RestBaseApiClient.GetCurrentMethod("ClearLogsAsync"), cancellationToken, new object[3]
        {
      (object) daysFrom,
      (object) daysTo,
      (object) maxCount
        });
    }

    Task IVideoService.UpdateInfoAsync(
      long clientId,
      long fileId,
      string path,
      long length,
      CancellationToken cancellationToken)
    {
        return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateInfoAsync"), cancellationToken, new object[4]
        {
      (object) clientId,
      (object) fileId,
      (object) path,
      (object) length
        });
    }

    Task<(string path, long length)> IVideoService.GetInfoAsync(
      long fileId,
      CancellationToken cancellationToken)
    {
        return this.Get<(string, long)>(RestBaseApiClient.GetCurrentMethod("GetInfoAsync"), cancellationToken, new object[1]
        {
      (object) fileId
        });
    }
}
