
using Ecng.Net.Sms;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class ProfileClient : BaseOfflineClient<Client>, IClientService, IBaseEntityService<Client>, IBaseService, ISmsService
    {
        Task<Client> IClientService.AddWithPasswordAsync(
          Client client,
          string password,
          string passwordQuestion,
          string passwordAnswer,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.BlockAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.ChangePasswordAsync(
          string newPassword,
          long? clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.EmailAsSpamAsync(
          string email,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.EmailBouncedAsync(
          string email,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.EmailDeliveredAsync(
          string email,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<Client>> IClientService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? awardId,
          long? roleId,
          long? groupId,
          long? licenseFeatureId,
          long? fileGroupId,
          bool? isApproved,
          bool? isGroups,
          long? productId,
          long? domainId,
          long? referralId,
          bool? hasBalance,
          bool? isOnline,
          bool? isTopTags,
          string tagName,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<BaseEntitySet<Client>>(new BaseEntitySet<Client>());
        }

        Task<Client> IClientService.FindByEmailAsync(
          string email,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<Client> IClientService.GetCurrentAsync(
          bool? includedOrders,
          CancellationToken cancellationToken)
        {
            return Task.FromResult<Client>(new Client() { DisplayName = "Offline" });
        }

        Task<(bool isAdmin, bool canPublish, bool isProductManager)> IClientService.GetNugetPermissionsAsync(
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IClientService.IsInRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return (Task<bool>)null;
        }

        Task<string[]> IClientService.GetRolesAsync(
          long? clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.ReactivateAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.ResetNugetAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.SetRoleAsync(
          long clientId,
          long roleId,
          long? oneAppId,
          DateTime? till,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IClientService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.AddAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IClientService.RemoveAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.AddLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> IClientService.RemoveLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.AddTagAsync(
          string tagName,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.SelfDeleteAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<string> ISmsService.SendAsync(
          string phone,
          string message,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.SubscribeAsync(
          bool enable,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.UpdateProfileAsync(
          Client changes,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.ValidateActivationTokenAsync(
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.ResetPasswordAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IClientService.UpdateReferralAsync(
          long referralId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}
