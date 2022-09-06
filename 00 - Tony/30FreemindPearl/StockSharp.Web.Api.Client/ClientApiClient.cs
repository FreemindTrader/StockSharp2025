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
    public class ClientApiClient : BaseApiEntityClient<StockSharp.Web.DomainModel.Client>, IClientService, IBaseEntityService<StockSharp.Web.DomainModel.Client>, IBaseService, ISmsService
    {
        public ClientApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ClientApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.FindByEmailAsync(
          string email,
          CancellationToken cancellationToken)
        {
            return this.Get<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("FindByEmailAsync"), cancellationToken, (object)email);
        }

        Task<StockSharp.Web.DomainModel.Client> IClientService.GetCurrentAsync(
          bool? includedOrders,
          CancellationToken cancellationToken)
        {
            return this.Get<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("GetCurrentAsync"), cancellationToken, (object)includedOrders);
        }

        Task IClientService.ValidateActivationTokenAsync(
          CancellationToken cancellationToken)
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
            return this.Post<StockSharp.Web.DomainModel.Client>(RestBaseApiClient.GetCurrentMethod("AddWithPasswordAsync"), cancellationToken, (object)entity, (object)password, (object)passwordQuestion, (object)passwordAnswer);
        }

        Task<BaseEntitySet<StockSharp.Web.DomainModel.Client>> IClientService.FindAsync(
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
            return this.Get<BaseEntitySet<StockSharp.Web.DomainModel.Client>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)awardId, (object)roleId, (object)groupId, (object)licenseFeatureId, (object)fileGroupId, (object)isApproved, (object)isGroups, (object)productId, (object)domainId, (object)referralId, (object)hasBalance, (object)isOnline, (object)isTopTags, (object)tagName, (object)like, (object)likeCompare);
        }

        Task IClientService.EmailAsSpamAsync(
          string email,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("EmailAsSpamAsync"), cancellationToken, (object)email);
        }

        Task IClientService.EmailBouncedAsync(
          string email,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("EmailBouncedAsync"), cancellationToken, (object)email);
        }

        Task IClientService.EmailDeliveredAsync(
          string email,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("EmailDeliveredAsync"), cancellationToken, (object)email);
        }

        Task<(bool isAdmin, bool canPublish, bool isProductManager)> IClientService.GetNugetPermissionsAsync(
          CancellationToken cancellationToken)
        {
            return this.Get<ValueTuple<bool, bool, bool>>(RestBaseApiClient.GetCurrentMethod("GetNugetPermissionsAsync"), cancellationToken);
        }

        Task IClientService.ChangePasswordAsync(
          string newPassword,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ChangePasswordAsync"), cancellationToken, (object)newPassword, (object)clientId);
        }

        Task IClientService.ResetPasswordAsync(CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ResetPasswordAsync"), cancellationToken, Array.Empty<object>());
        }

        Task IClientService.UpdateProfileAsync(
          StockSharp.Web.DomainModel.Client changes,
          CancellationToken cancellationToken)
        {
            return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateProfileAsync"), cancellationToken, (object)changes);
        }

        Task IClientService.UpdateReferralAsync(
          long referralId,
          CancellationToken cancellationToken)
        {
            return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateReferralAsync"), cancellationToken, (object)referralId);
        }

        Task IClientService.BlockAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("BlockAsync"), cancellationToken, (object)clientId);
        }

        Task IClientService.SubscribeAsync(
          bool enable,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("SubscribeAsync"), cancellationToken, (object)enable);
        }

        Task IClientService.SelfDeleteAsync(CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("SelfDeleteAsync"), cancellationToken, Array.Empty<object>());
        }

        Task<bool> IClientService.IsInRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("IsInRoleAsync"), cancellationToken, (object)roleId);
        }

        Task<string[]> IClientService.GetRolesAsync(
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<string[]>(RestBaseApiClient.GetCurrentMethod("GetRolesAsync"), cancellationToken, (object)clientId);
        }

        Task IClientService.ReactivateAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ReactivateAsync"), cancellationToken, (object)clientId);
        }

        Task IClientService.ResetNugetAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ResetNugetAsync"), cancellationToken, (object)clientId);
        }

        Task IClientService.SetRoleAsync(
          long clientId,
          long roleId,
          long? oneAppId,
          DateTime? till,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("SetRoleAsync"), cancellationToken, (object)clientId, (object)roleId, (object)oneAppId, (object)till);
        }

        Task<bool> IClientService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, (object)roleId);
        }

        Task IClientService.AddAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddAwardAsync"), cancellationToken, (object)clientId, (object)awardId);
        }

        Task<bool> IClientService.RemoveAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveAwardAsync"), cancellationToken, (object)clientId, (object)awardId);
        }

        Task IClientService.AddLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddLicenseFeatureAsync"), cancellationToken, (object)clientId, (object)featureId);
        }

        Task<bool> IClientService.RemoveLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveLicenseFeatureAsync"), cancellationToken, (object)clientId, (object)featureId);
        }

        Task IClientService.AddTagAsync(
          string tagName,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddTagAsync"), cancellationToken, (object)tagName);
        }

        Task<string> ISmsService.SendAsync(
          string phone,
          string message,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("SendAsync"), cancellationToken, (object)phone, (object)message);
        }
    }
}
