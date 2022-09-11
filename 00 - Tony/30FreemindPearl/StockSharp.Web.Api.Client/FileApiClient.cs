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
            return Post<string>( GetCurrentMethod( "StartUploadAsync"), cancellationToken, file, compression );
        }

        Task IFileService.UploadAsync(
          string operationId,
          byte[] bodyPart,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "UploadAsync"), cancellationToken, operationId, bodyPart );
        }

        Task<File> IFileService.FinishUploadAsync(
          string operationId,
          bool isCancel,
          CancellationToken cancellationToken)
        {
            return Post<File>( GetCurrentMethod( "FinishUploadAsync"), cancellationToken, operationId, isCancel );
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
            return Get<BaseEntitySet<File>>( GetCurrentMethod( "GetTempsAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, uploads, downloads );
        }

        Task IFileService.ClearTempsAsync(
          bool uploads,
          bool downloads,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ClearTempsAsync"), cancellationToken, uploads, downloads );
        }

        Task IFileService.RemoveTempAsync(
          string operationId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RemoveTempAsync"), cancellationToken, operationId );
        }

        Task<File> IFileService.GetTempAsync(
          string operationId,
          CancellationToken cancellationToken)
        {
            return Get<File>( GetCurrentMethod( "GetTempAsync"), cancellationToken, operationId );
        }

        Task<byte[]> IFileService.GetBodyAsync(
          long fileId,
          long skip,
          long? count,
          Compressions? compression,
          bool isSnapshot,
          CancellationToken cancellationToken)
        {
            return Get<byte[]>( GetCurrentMethod( "GetBodyAsync"), cancellationToken, fileId, skip, count, compression, isSnapshot );
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
            return Get<BaseEntitySet<File>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, groupId, nestedGroups, includeHistory, messageId, draftId, emailId, emailTemplateOnly, like, likeCompare );
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
            return Get<BaseEntitySet<File>>( GetCurrentMethod( "GetHistoryAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, fileId );
        }

        Task<BaseEntitySet<(File, byte[] nuspec, string[] frameworks)>> IFileService.GetNugetPackagesAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ValueTuple<File, byte[], string[]>>>( GetCurrentMethod( "GetNugetPackagesAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc );
        }

        Task<File> IFileService.AddNugetPackageAsync(
          byte[] package,
          CancellationToken cancellationToken)
        {
            return Post<File>( GetCurrentMethod( "AddNugetPackageAsync"), cancellationToken, package );
        }

        Task<File> IFileService.GetAvatarAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return Get<File>( GetCurrentMethod( "GetAvatarAsync"), cancellationToken, clientId );
        }

        Task IFileService.AddGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddGroupAsync"), cancellationToken, fileId, groupId );
        }

        Task<bool> IFileService.RemoveGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveGroupAsync"), cancellationToken, fileId, groupId );
        }

        Task IVideoService.UpdateInfoAsync(
          string clientToken,
          long fileId,
          string path,
          long length,
          CancellationToken cancellationToken)
        {
            return Put( GetCurrentMethod( "UpdateInfoAsync"), cancellationToken, clientToken, fileId, path, length );
        }

        

        Task<(string clientToken, long fileId)[]> IVideoService.GetPendingRequestsAsync(
          CancellationToken cancellationToken)
        {
            return Get<ValueTuple<string, long>[]>( GetCurrentMethod( "GetPendingRequestsAsync"), cancellationToken);
        }

        Task<(string path, long length)> IVideoService.GetInfoAsync(long fileId, CancellationToken cancellationToken)
        {
            return Get<ValueTuple<string, long>>( GetCurrentMethod( "GetInfoAsync"), cancellationToken, fileId );
        }
    }
}
