// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IClientService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using Ecng.Net.Sms;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IClientService : IBaseEntityService<Client>, ISmsService
    {
        Task PingAsync( CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool?> FindByEmailAsync( string email, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<Client> GetCurrentAsync(
          bool? includedOrders = null,
          string salt = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task ValidateActivationTokenAsync( CancellationToken cancellationToken = default( CancellationToken ) );

        Task<Client> AddWithPasswordAsync(
          Client entity,
          string password,
          string passwordQuestion,
          string passwordAnswer,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<BaseEntitySet<Client>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          long? awardId = null,
          long? roleId = null,
          long? groupId = null,
          long? licenseFeatureId = null,
          long? fileGroupId = null,
          bool? isApproved = null,
          bool? isLockedOut = null,
          bool? isSelfDeleted = null,
          bool? isAllowStatistics = null,
          EmailPreferences? emailPreferences = null,
          bool? isGroups = null,
          long? productId = null,
          long? domainId = null,
          long? referralId = null,
          bool? hasBalance = null,
          bool? isOnline = null,
          bool? isTags = null,
          string tagName = null,
          long? topicSubscription = null,
          long? topicFavorite = null,
          long? menuId = null,
          long? socialId = null,
          string like = null,
          ComparisonOperator? likeCompare = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<(bool isAdmin, bool canPublish, bool isProductManager)> GetNugetPermissionsAsync( CancellationToken cancellationToken = default( CancellationToken ) );


        Task ChangePasswordAsync(
          string newPassword,
          long? clientId = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task UpdateProfileAsync( Client changes, CancellationToken cancellationToken = default( CancellationToken ) );

        Task UpdateReferralAsync( long referralId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task BlockAsync( long clientId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task UpdateEmailPreferencesAsync(
          EmailPreferences preferences,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<long [ ]> GetRolesIdsAsync( long clientId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task ReactivateAsync( long clientId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<ClientRole> AddRoleAsync(
          long clientId,
          long roleId,
          long? oneAppId = null,
          DateTime? till = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveRoleAsync( long roleId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task AddAwardAsync( long clientId, long awardId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<Client> SetTelegramIdAsync(
          string authToken,
          long telegramId,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<Client> TryGetByTelegramIdAsync(
          long telegramId,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task SendTelegramBotAsync( long clientId, string text, CancellationToken cancellationToken = default( CancellationToken ) );

        Task MakeTempSessionAsync( string salt = null, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<Client> TryGetByEmailTokenAsync(
          string emailToken,
          CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
