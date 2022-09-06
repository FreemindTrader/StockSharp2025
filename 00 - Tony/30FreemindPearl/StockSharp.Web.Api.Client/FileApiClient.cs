// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.FileApiClient
// Assembly: StockSharp.Web.Api.Client, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9C29C3BA-4173-498F-98DB-80C2C449F660
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Client.dll

using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class FileApiClient : BaseApiEntityClient<File>, IFileService, IBaseEntityService<File>, IBaseService, IVideoService
    {
        public FileApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public FileApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<string> IFileService.StartUploadAsync(
          File file,
          Compressions? compression,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("StartUploadAsync"), cancellationToken, (object)file, (object)compression);
        }

        Task IFileService.UploadAsync(
          string operationId,
          byte[] bodyPart,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("UploadAsync"), cancellationToken, (object)operationId, (object)bodyPart);
        }

        Task<File> IFileService.FinishUploadAsync(
          string operationId,
          bool isCancel,
          CancellationToken cancellationToken)
        {
            return this.Post<File>(RestBaseApiClient.GetCurrentMethod("FinishUploadAsync"), cancellationToken, (object)operationId, (object)isCancel);
        }

        Task<BaseEntitySet<File>> IFileService.GetTempsAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool uploads,
          bool downloads,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<File>>(RestBaseApiClient.GetCurrentMethod("GetTempsAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)uploads, (object)downloads);
        }

        Task IFileService.ClearTempsAsync(
          bool uploads,
          bool downloads,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ClearTempsAsync"), cancellationToken, (object)uploads, (object)downloads);
        }

        Task IFileService.RemoveTempAsync(
          string operationId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RemoveTempAsync"), cancellationToken, (object)operationId);
        }

        Task<File> IFileService.GetTempAsync(
          string operationId,
          CancellationToken cancellationToken)
        {
            return this.Get<File>(RestBaseApiClient.GetCurrentMethod("GetTempAsync"), cancellationToken, (object)operationId);
        }

        Task<byte[]> IFileService.GetBodyAsync(
          long fileId,
          long skip,
          long? count,
          Compressions? compression,
          bool isSnapshot,
          CancellationToken cancellationToken)
        {
            return this.Get<byte[]>(RestBaseApiClient.GetCurrentMethod("GetBodyAsync"), cancellationToken, (object)fileId, (object)skip, (object)count, (object)compression, (object)isSnapshot);
        }

        Task<BaseEntitySet<File>> IFileService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? groupId,
          bool? nestedGroups,
          bool? includeHistory,
          long? messageId,
          long? draftId,
          long? emailId,
          bool? emailTemplateOnly,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<File>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)groupId, (object)nestedGroups, (object)includeHistory, (object)messageId, (object)draftId, (object)emailId, (object)emailTemplateOnly, (object)like, (object)likeCompare);
        }

        Task<BaseEntitySet<File>> IFileService.GetHistoryAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? fileId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<File>>(RestBaseApiClient.GetCurrentMethod("GetHistoryAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)fileId);
        }

        Task<BaseEntitySet<(File, byte[] nuspec, string[] frameworks)>> IFileService.GetNugetPackagesAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<ValueTuple<File, byte[], string[]>>>(RestBaseApiClient.GetCurrentMethod("GetNugetPackagesAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc);
        }

        Task<File> IFileService.AddNugetPackageAsync(
          byte[] package,
          CancellationToken cancellationToken)
        {
            return this.Post<File>(RestBaseApiClient.GetCurrentMethod("AddNugetPackageAsync"), cancellationToken, (object)package);
        }

        Task<File> IFileService.GetAvatarAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<File>(RestBaseApiClient.GetCurrentMethod("GetAvatarAsync"), cancellationToken, (object)clientId);
        }

        Task IFileService.AddGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddGroupAsync"), cancellationToken, (object)fileId, (object)groupId);
        }

        Task<bool> IFileService.RemoveGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveGroupAsync"), cancellationToken, (object)fileId, (object)groupId);
        }

        Task IVideoService.UpdateInfoAsync(
          string clientToken,
          long fileId,
          string path,
          long length,
          CancellationToken cancellationToken)
        {
            return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateInfoAsync"), cancellationToken, (object)clientToken, (object)fileId, (object)path, (object)length);
        }

        

        Task<(string clientToken, long fileId)[]> IVideoService.GetPendingRequestsAsync(
          CancellationToken cancellationToken)
        {
            return this.Get<ValueTuple<string, long>[]>(RestBaseApiClient.GetCurrentMethod("GetPendingRequestsAsync"), cancellationToken);
        }

        Task<(string path, long length)> IVideoService.GetInfoAsync(long fileId, CancellationToken cancellationToken)
        {
            return this.Get<ValueTuple<string, long>>(RestBaseApiClient.GetCurrentMethod("GetInfoAsync"), cancellationToken, (object)fileId);
        }
    }
}
