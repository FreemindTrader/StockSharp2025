// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.FileApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using File = StockSharp.Web.DomainModel.File;

namespace StockSharp.Web.Api.Client
{
    internal class FileApiClient : BaseApiEntityClient<File>, IFileService, IBaseEntityService<File>, IVideoService
    {
        public FileApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public FileApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<string> IFileService.StartUploadAsync(
          File file,
          Compressions? compression,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "StartUploadAsync" ), cancellationToken, new object [2]
            {
         file,
         compression
            } );
        }

        Task IFileService.UploadAsync(
          string operationId,
          byte [ ] bodyPart,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "UploadAsync" ), cancellationToken, new object [2]
            {
         operationId,
         bodyPart
            } );
        }

        Task<File> IFileService.FinishUploadAsync(
          string operationId,
          bool isCancel,
          CancellationToken cancellationToken )
        {
            return this.Post<File>( RestBaseApiClient.GetCurrentMethod( "FinishUploadAsync" ), cancellationToken, new object [2]
            {
         operationId,
         isCancel
            } );
        }

        Task IFileService.ClearTempsAsync(
          bool? uploads,
          bool? downloads,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ClearTempsAsync" ), cancellationToken, new object [2]
            {
         uploads,
         downloads
            } );
        }

        Task IFileService.RemoveTempAsync(
          string operationId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "RemoveTempAsync" ), cancellationToken, new object [1]
            {
         operationId
            } );
        }

        Task<File> IFileService.GetTempAsync(
          string operationId,
          CancellationToken cancellationToken )
        {
            return this.Get<File>( RestBaseApiClient.GetCurrentMethod( "GetTempAsync" ), cancellationToken, new object [1]
            {
         operationId
            } );
        }

        Task<string> IFileService.StartDownloadAsync(
          long? fileId,
          long? bodyId,
          bool? isSnapshot,
          Compressions? compression,
          int? width,
          int? height,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "StartDownloadAsync" ), cancellationToken, new object [6]
            {
         fileId,
         bodyId,
         isSnapshot,
         compression,
         width,
         height
            } );
        }

        Task<byte [ ]> IFileService.GetPartAsync(
          string operationId,
          long skip,
          long? count,
          CancellationToken cancellationToken )
        {
            return this.Get<byte [ ]>( RestBaseApiClient.GetCurrentMethod( "GetPartAsync" ), cancellationToken, new object [3]
            {
         operationId,
         skip,
         count
            } );
        }

        Task<byte [ ]> IFileService.GetBodyAsync(
          long fileId,
          long skip,
          long? count,
          Compressions? compression,
          int? width,
          int? height,
          CancellationToken cancellationToken )
        {
            return this.Get<byte [ ]>( RestBaseApiClient.GetCurrentMethod( "GetBodyAsync" ), cancellationToken, new object [6]
            {
         fileId,
         skip,
         count,
         compression,
         width,
         height
            } );
        }

        Task<byte [ ]> IFileService.GetVersionBodyAsync(
          long bodyId,
          bool? isSnapshot,
          long skip,
          long? count,
          Compressions? compression,
          int? width,
          int? height,
          CancellationToken cancellationToken )
        {
            return this.Get<byte [ ]>( RestBaseApiClient.GetCurrentMethod( "GetVersionBodyAsync" ), cancellationToken, new object [7]
            {
         bodyId,
         isSnapshot,
         skip,
         count,
         compression,
         width,
         height
            } );
        }

        Task<FileBody> IFileService.GetFileBodyAsync(
          long bodyId,
          CancellationToken cancellationToken )
        {
            return this.Get<FileBody>( RestBaseApiClient.GetCurrentMethod( "GetFileBodyAsync" ), cancellationToken, new object [1]
            {
         bodyId
            } );
        }

        Task<File> IFileService.GetFileByBodyAsync(
          long bodyId,
          CancellationToken cancellationToken )
        {
            return this.Get<File>( RestBaseApiClient.GetCurrentMethod( "GetFileByBodyAsync" ), cancellationToken, new object [1]
            {
         bodyId
            } );
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<File>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [18]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         clientId,
         groupId,
         messageId,
         draftId,
         emailId,
         tempUploads,
         tempDownloads,
         cloud,
         like,
         likeCompare
            } );
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<FileBody>>( RestBaseApiClient.GetCurrentMethod( "FindVersionsAsync" ), cancellationToken, new object [10]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         fileId,
         groupId
            } );
        }

        Task<BaseEntitySet<NugetSpecification>> IFileService.GetNugetPackagesAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<NugetSpecification>>( RestBaseApiClient.GetCurrentMethod( "GetNugetPackagesAsync" ), cancellationToken, new object [6]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount
            } );
        }

        Task<FileBody> IFileService.AddNugetPackageAsync(
          byte [ ] package,
          CancellationToken cancellationToken )
        {
            return this.Post<FileBody>( RestBaseApiClient.GetCurrentMethod( "AddNugetPackageAsync" ), cancellationToken, new object [1]
            {
         package
            } );
        }

        

        Task IFileService.AddGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddGroupAsync" ), cancellationToken, new object [2]
            {
         fileId,
         groupId
            } );
        }

        Task<bool> IFileService.RemoveGroupAsync(
          long fileId,
          long groupId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveGroupAsync" ), cancellationToken, new object [2]
            {
         fileId,
         groupId
            } );
        }

        Task<int> IFileService.ClearLogsAsync(
          int daysFrom,
          int daysTo,
          int maxCount,
          CancellationToken cancellationToken )
        {
            return this.Post<int>( RestBaseApiClient.GetCurrentMethod( "ClearLogsAsync" ), cancellationToken, new object [3]
            {
         daysFrom,
         daysTo,
         maxCount
            } );
        }

        Task IVideoService.UpdateInfoAsync(
          long clientId,
          long fileId,
          string path,
          long length,
          CancellationToken cancellationToken )
        {
            return this.Put( RestBaseApiClient.GetCurrentMethod( "UpdateInfoAsync" ), cancellationToken, new object [4]
            {
         clientId,
         fileId,
         path,
         length
            } );
        }

        
        Task<(string path, long length)> IVideoService.GetInfoAsync(
          long fileId,
          CancellationToken cancellationToken )
        {
            return this.Get<ValueTuple<string, long>>( RestBaseApiClient.GetCurrentMethod( "GetInfoAsync" ), cancellationToken, new object [1]
            {
         fileId
            } );
        }

        Task<File> IFileService.GetAvatarAsync( long clientId, CancellationToken cancellationToken )
        {
            return this.Get<File>( RestBaseApiClient.GetCurrentMethod( "GetAvatarAsync" ), cancellationToken, new object [1]
            {
                clientId
            } );
        }        
    }
}
