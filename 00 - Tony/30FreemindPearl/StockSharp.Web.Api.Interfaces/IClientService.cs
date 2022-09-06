using Ecng.Net.Sms;
using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IClientService : IBaseEntityService<Client>, IBaseService, ISmsService
    {
        Task<Client> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        Task<Client> GetCurrentAsync(
          bool? includedOrders = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task ValidateActivationTokenAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<Client> AddWithPasswordAsync(
          Client entity,
          string password,
          string passwordQuestion,
          string passwordAnswer,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<Client>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? awardId = null,
          long? roleId = null,
          long? groupId = null,
          long? licenseFeatureId = null,
          long? fileGroupId = null,
          bool? isApproved = null,
          bool? isGroups = null,
          long? productId = null,
          long? domainId = null,
          long? referralId = null,
          bool? hasBalance = null,
          bool? isOnline = null,
          bool? isTopTags = null,
          string tagName = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task EmailAsSpamAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        Task EmailBouncedAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        Task EmailDeliveredAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
        
        Task< (bool isAdmin, bool canPublish, bool isProductManager)> GetNugetPermissionsAsync(
          CancellationToken cancellationToken = default(CancellationToken));

        Task ChangePasswordAsync(
          string newPassword,
          long? clientId = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task ResetPasswordAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateProfileAsync(Client changes, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateReferralAsync(long referralId, CancellationToken cancellationToken = default(CancellationToken));

        Task BlockAsync(long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task SubscribeAsync(bool enable, CancellationToken cancellationToken = default(CancellationToken));

        Task SelfDeleteAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> IsInRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<string[]> GetRolesAsync(long? clientId = null, CancellationToken cancellationToken = default(CancellationToken));

        Task ReactivateAsync(long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task ResetNugetAsync(long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task SetRoleAsync(
          long clientId,
          long roleId,
          long? oneAppId = null,
          DateTime? till = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task AddAwardAsync(long clientId, long awardId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task AddLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task AddTagAsync(string tagName, CancellationToken cancellationToken = default(CancellationToken));
    }
}
