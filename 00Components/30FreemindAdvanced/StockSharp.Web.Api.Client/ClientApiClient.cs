// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ClientApiClient
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
using Ecng.Net.Sms;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class ClientApiClient :
  BaseApiEntityClient<StockSharp.Web.DomainModel.Client>,
  IClientService,
  IBaseEntityService<StockSharp.Web.DomainModel.Client>,
  ISmsService
{
    public ClientApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ClientApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task IClientService.PingAsync(CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("PingAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<bool?> IClientService.FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return this.Get<bool?>(RestBaseApiClient.GetCurrentMethod("FindByEmailAsync"), cancellationToken, new object[1]
        {
      (object) email
        });
    }

    Task<StockSharp.Web.DomainModel.Client> IClientService.GetCurrentAsync(
      bool? includedOrders,
      string salt,
      CancellationToken cancellationToken)
    {
        return this.Get<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("GetCurrentAsync"), cancellationToken, new object[2]
        {
      (object) includedOrders,
      (object) salt
        });
    }

    Task IClientService.ValidateActivationTokenAsync(CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ValidateActivationTokenAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<StockSharp.Web.DomainModel.Client> IClientService.AddWithPasswordAsync(
      StockSharp.Web.DomainModel.Client entity,
      string password,
      string passwordQuestion,
      string passwordAnswer,
      CancellationToken cancellationToken)
    {
        return this.Post<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("AddWithPasswordAsync"), cancellationToken, new object[4]
        {
      (object) entity,
      (object) password,
      (object) passwordQuestion,
      (object) passwordAnswer
        });
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
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<StockSharp.Web.DomainModel.Client>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[32 /*0x20*/]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) awardId,
      (object) roleId,
      (object) groupId,
      (object) licenseFeatureId,
      (object) fileGroupId,
      (object) isApproved,
      (object) isLockedOut,
      (object) isSelfDeleted,
      (object) isAllowStatistics,
      (object) emailPreferences,
      (object) isGroups,
      (object) productId,
      (object) domainId,
      (object) referralId,
      (object) hasBalance,
      (object) isOnline,
      (object) isTags,
      (object) tagName,
      (object) topicSubscription,
      (object) topicFavorite,
      (object) menuId,
      (object) socialId,
      (object) like,
      (object) likeCompare
        });
    }

    Task<(bool isAdmin, bool canPublish, bool isProductManager)> IClientService.GetNugetPermissionsAsync(
      CancellationToken cancellationToken)
    {
        return this.Get<(bool, bool, bool)>(RestBaseApiClient.GetCurrentMethod("GetNugetPermissionsAsync"), cancellationToken, Array.Empty<object>());
    }

    Task IClientService.ChangePasswordAsync(
      string newPassword,
      long? clientId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ChangePasswordAsync"), cancellationToken, new object[2]
        {
      (object) newPassword,
      (object) clientId
        });
    }

    Task IClientService.UpdateProfileAsync(StockSharp.Web.DomainModel.Client changes, CancellationToken cancellationToken)
    {
        return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateProfileAsync"), cancellationToken, new object[1]
        {
      (object) changes
        });
    }

    Task IClientService.UpdateReferralAsync(long referralId, CancellationToken cancellationToken)
    {
        return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateReferralAsync"), cancellationToken, new object[1]
        {
      (object) referralId
        });
    }

    Task IClientService.BlockAsync(long clientId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("BlockAsync"), cancellationToken, new object[1]
        {
      (object) clientId
        });
    }

    Task IClientService.UpdateEmailPreferencesAsync(
      EmailPreferences preferences,
      CancellationToken cancellationToken)
    {
        return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateEmailPreferencesAsync"), cancellationToken, new object[1]
        {
      (object) preferences
        });
    }

    Task<long[]> IClientService.GetRolesIdsAsync(long clientId, CancellationToken cancellationToken)
    {
        return this.Get<long[]>(RestBaseApiClient.GetCurrentMethod("GetRolesIdsAsync"), cancellationToken, new object[1]
        {
      (object) clientId
        });
    }

    Task IClientService.ReactivateAsync(long clientId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ReactivateAsync"), cancellationToken, new object[1]
        {
      (object) clientId
        });
    }

    Task<ClientRole> IClientService.AddRoleAsync(
      long clientId,
      long roleId,
      long? oneAppId,
      DateTime? till,
      CancellationToken cancellationToken)
    {
        return this.Post<ClientRole>(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, new object[4]
        {
      (object) clientId,
      (object) roleId,
      (object) oneAppId,
      (object) till
        });
    }

    Task<bool> IClientService.RemoveRoleAsync(long roleId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, new object[1]
        {
      (object) roleId
        });
    }

    Task IClientService.AddAwardAsync(
      long clientId,
      long awardId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddAwardAsync"), cancellationToken, new object[2]
        {
      (object) clientId,
      (object) awardId
        });
    }

    Task<bool> IClientService.RemoveAwardAsync(
      long clientId,
      long awardId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveAwardAsync"), cancellationToken, new object[2]
        {
      (object) clientId,
      (object) awardId
        });
    }

    Task<StockSharp.Web.DomainModel.Client> IClientService.SetTelegramIdAsync(
      string authToken,
      long telegramId,
      CancellationToken cancellationToken)
    {
        return this.Post<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("SetTelegramIdAsync"), cancellationToken, new object[2]
        {
      (object) authToken,
      (object) telegramId
        });
    }

    Task<StockSharp.Web.DomainModel.Client> IClientService.TryGetByTelegramIdAsync(
      long telegramId,
      CancellationToken cancellationToken)
    {
        return this.Get<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("TryGetByTelegramIdAsync"), cancellationToken, new object[1]
        {
      (object) telegramId
        });
    }

    Task IClientService.SendTelegramBotAsync(
      long clientId,
      string text,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("SendTelegramBotAsync"), cancellationToken, new object[2]
        {
      (object) clientId,
      (object) text
        });
    }

    Task IClientService.MakeTempSessionAsync(string salt, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("MakeTempSessionAsync"), cancellationToken, new object[1]
        {
      (object) salt
        });
    }

    Task<StockSharp.Web.DomainModel.Client> IClientService.TryGetByEmailTokenAsync(
      string emailToken,
      CancellationToken cancellationToken)
    {
        return this.Get<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("TryGetByEmailTokenAsync"), cancellationToken, new object[1]
        {
      (object) emailToken
        });
    }

    Task<string> ISmsService.SendAsync(
      string phone,
      string message,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("SendAsync"), cancellationToken, new object[2]
        {
      (object) phone,
      (object) message
        });
    }
}
