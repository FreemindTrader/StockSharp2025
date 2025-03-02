// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ClientApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using Ecng.Net.Sms;
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
    internal class ClientApiClient : BaseApiEntityClient<StockSharp.Web.DomainModel.Client>, IClientService, IBaseEntityService<StockSharp.Web.DomainModel.Client>, ISmsService
    {
        public ClientApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ClientApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task IClientService.PingAsync( CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "PingAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<bool?> IClientService.FindByEmailAsync(
          string email,
          CancellationToken cancellationToken )
        {
            return this.Get<bool?>( RestBaseApiClient.GetCurrentMethod( "FindByEmailAsync" ), cancellationToken, new object [1]
            {
         email
            } );
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.GetCurrentAsync(
          bool? includedOrders,
          string salt,
          CancellationToken cancellationToken )
        {
            return this.Get<StockSharp.Web.DomainModel.Client>( RestBaseApiClient.GetCurrentMethod( "GetCurrentAsync" ), cancellationToken, new object [2]
            {
         includedOrders,
         salt
            } );
        }

        Task IClientService.ValidateActivationTokenAsync(
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ValidateActivationTokenAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.AddWithPasswordAsync(
          StockSharp.Web.DomainModel.Client entity,
          string password,
          string passwordQuestion,
          string passwordAnswer,
          CancellationToken cancellationToken )
        {
            return this.Post<StockSharp.Web.DomainModel.Client>( RestBaseApiClient.GetCurrentMethod( "AddWithPasswordAsync" ), cancellationToken, new object [4]
            {
         entity,
         password,
         passwordQuestion,
         passwordAnswer
            } );
        }

        Task<BaseEntitySet<StockSharp.Web.DomainModel.Client>> IClientService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? awardId,
          long? roleId,
          long? groupId,
          long? licenseFeatureId,
          long? fileGroupId,
          bool? isApproved,
          bool? isLockedOut,
          bool? isSelfDeleted,
          bool? isAllowStatistics,
          EmailPreferences? emailPreferences,
          bool? isGroups,
          long? productId,
          long? domainId,
          long? referralId,
          bool? hasBalance,
          bool? isOnline,
          bool? isTags,
          string tagName,
          long? topicSubscription,
          long? topicFavorite,
          long? menuId,
          long? socialId,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<StockSharp.Web.DomainModel.Client>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [32]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         awardId,
         roleId,
         groupId,
         licenseFeatureId,
         fileGroupId,
         isApproved,
         isLockedOut,
         isSelfDeleted,
         isAllowStatistics,
         emailPreferences,
         isGroups,
         productId,
         domainId,
         referralId,
         hasBalance,
         isOnline,
         isTags,
         tagName,
         topicSubscription,
         topicFavorite,
         menuId,
         socialId,
         like,
         likeCompare
            } );
        }

        
        Task<(bool isAdmin, bool canPublish, bool isProductManager)> IClientService.GetNugetPermissionsAsync(
          CancellationToken cancellationToken )
        {
            return this.Get<ValueTuple<bool, bool, bool>>( RestBaseApiClient.GetCurrentMethod( "GetNugetPermissionsAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task IClientService.ChangePasswordAsync(
          string newPassword,
          long? clientId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ChangePasswordAsync" ), cancellationToken, new object [2]
            {
         newPassword,
         clientId
            } );
        }

        Task IClientService.UpdateProfileAsync(
          StockSharp.Web.DomainModel.Client changes,
          CancellationToken cancellationToken )
        {
            return this.Put( RestBaseApiClient.GetCurrentMethod( "UpdateProfileAsync" ), cancellationToken, new object [1]
            {
         changes
            } );
        }

        Task IClientService.UpdateReferralAsync(
          long referralId,
          CancellationToken cancellationToken )
        {
            return this.Put( RestBaseApiClient.GetCurrentMethod( "UpdateReferralAsync" ), cancellationToken, new object [1]
            {
         referralId
            } );
        }

        Task IClientService.BlockAsync(
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "BlockAsync" ), cancellationToken, new object [1]
            {
         clientId
            } );
        }

        Task IClientService.UpdateEmailPreferencesAsync(
          EmailPreferences preferences,
          CancellationToken cancellationToken )
        {
            return this.Put( RestBaseApiClient.GetCurrentMethod( "UpdateEmailPreferencesAsync" ), cancellationToken, new object [1]
            {
         preferences
            } );
        }

        Task<long [ ]> IClientService.GetRolesIdsAsync(
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Get<long [ ]>( RestBaseApiClient.GetCurrentMethod( "GetRolesIdsAsync" ), cancellationToken, new object [1]
            {
         clientId
            } );
        }

        Task IClientService.ReactivateAsync(
          long clientId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ReactivateAsync" ), cancellationToken, new object [1]
            {
         clientId
            } );
        }

        Task<ClientRole> IClientService.AddRoleAsync(
          long clientId,
          long roleId,
          long? oneAppId,
          DateTime? till,
          CancellationToken cancellationToken )
        {
            return this.Post<ClientRole>( RestBaseApiClient.GetCurrentMethod( "AddRoleAsync" ), cancellationToken, new object [4]
            {
         clientId,
         roleId,
         oneAppId,
         till
            } );
        }

        Task<bool> IClientService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveRoleAsync" ), cancellationToken, new object [1]
            {
         roleId
            } );
        }

        Task IClientService.AddAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddAwardAsync" ), cancellationToken, new object [2]
            {
         clientId,
         awardId
            } );
        }

        Task<bool> IClientService.RemoveAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveAwardAsync" ), cancellationToken, new object [2]
            {
         clientId,
         awardId
            } );
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.SetTelegramIdAsync(
          string authToken,
          long telegramId,
          CancellationToken cancellationToken )
        {
            return this.Post<StockSharp.Web.DomainModel.Client>( RestBaseApiClient.GetCurrentMethod( "SetTelegramIdAsync" ), cancellationToken, new object [2]
            {
         authToken,
         telegramId
            } );
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.TryGetByTelegramIdAsync(
          long telegramId,
          CancellationToken cancellationToken )
        {
            return this.Get<StockSharp.Web.DomainModel.Client>( RestBaseApiClient.GetCurrentMethod( "TryGetByTelegramIdAsync" ), cancellationToken, new object [1]
            {
         telegramId
            } );
        }

        Task IClientService.SendTelegramBotAsync(
          long clientId,
          string text,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "SendTelegramBotAsync" ), cancellationToken, new object [2]
            {
         clientId,
         text
            } );
        }

        Task IClientService.MakeTempSessionAsync(
          string salt,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "MakeTempSessionAsync" ), cancellationToken, new object [1]
            {
         salt
            } );
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.TryGetByEmailTokenAsync(
          string emailToken,
          CancellationToken cancellationToken )
        {
            return this.Get<StockSharp.Web.DomainModel.Client>( RestBaseApiClient.GetCurrentMethod( "TryGetByEmailTokenAsync" ), cancellationToken, new object [1]
            {
         emailToken
            } );
        }

        Task<string> ISmsService.SendAsync(
          string phone,
          string message,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "SendAsync" ), cancellationToken, new object [2]
            {
         phone,
         message
            } );
        }
    }
}
