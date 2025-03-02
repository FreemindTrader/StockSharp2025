// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.LicenseApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class LicenseApiClient : BaseApiEntityClient<License>, ILicenseService, IBaseEntityService<License>
    {
        public LicenseApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public LicenseApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<License>> ILicenseService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? clientId,
          long? featureId,
          string platformId,
          string hardwareId,
          DateTime? expirationDateMin,
          DateTime? expirationDateMax,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<License>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [16]
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
         featureId,
         platformId,
         hardwareId,
         expirationDateMin,
         expirationDateMax,
         like,
         likeCompare
            } );
        }

        Task ILicenseService.SendLicenseByEmailAsync(
          long licenseId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "SendLicenseByEmailAsync" ), cancellationToken, new object [1]
            {
         licenseId
            } );
        }

        Task<LicenseFeatureEx> ILicenseService.AddFeatureAsync(
          LicenseFeatureEx feature,
          CancellationToken cancellationToken )
        {
            return this.Post<LicenseFeatureEx>( RestBaseApiClient.GetCurrentMethod( "AddFeatureAsync" ), cancellationToken, new object [1]
            {
         feature
            } );
        }

        Task<bool> ILicenseService.RemoveFeatureAsync(
          long featureId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveFeatureAsync" ), cancellationToken, new object [1]
            {
         featureId
            } );
        }
    }
}
