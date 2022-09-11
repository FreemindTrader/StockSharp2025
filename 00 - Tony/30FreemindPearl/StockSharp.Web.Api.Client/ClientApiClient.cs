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
    public class ClientApiClient : BaseApiEntityClient<DomainModel.Client>, IClientService, IBaseEntityService<DomainModel.Client>, IBaseService, ISmsService
    {
        public ClientApiClient(HttpMessageInvoker http, SecureString token) : base(http, token)
        {
        }

        public ClientApiClient(HttpMessageInvoker http, string login, SecureString password) : base(http, login, password)
        {
        }

        Task<DomainModel.Client> IClientService.FindByEmailAsync( string email, CancellationToken cancellationToken)
        {
            return Get<DomainModel.Client>( GetCurrentMethod( "FindByEmailAsync"), cancellationToken, email );
        }

        Task<DomainModel.Client> IClientService.GetCurrentAsync( bool? includedOrders, CancellationToken cancellationToken)
        {
            return Get<DomainModel.Client>( GetCurrentMethod( "GetCurrentAsync"), cancellationToken, includedOrders );
        }

        Task IClientService.ValidateActivationTokenAsync( CancellationToken cancellationToken )
        {
            return Post( GetCurrentMethod( "ValidateActivationTokenAsync"), cancellationToken, Array.Empty<object>());
        }

        Task<DomainModel.Client> IClientService.AddWithPasswordAsync( DomainModel.Client entity, string password, string passwordQuestion, string passwordAnswer, CancellationToken cancellationToken)
        {
            return Post<DomainModel.Client>( GetCurrentMethod( "AddWithPasswordAsync"), cancellationToken, entity, password, passwordQuestion, passwordAnswer );
        }

        Task<BaseEntitySet<DomainModel.Client>> IClientService.FindAsync(
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
            return Get<BaseEntitySet<DomainModel.Client>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, awardId, roleId, groupId, licenseFeatureId, fileGroupId, isApproved, isGroups, productId, domainId, referralId, hasBalance, isOnline, isTopTags, tagName, like, likeCompare );
        }

        Task IClientService.EmailAsSpamAsync( string email, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "EmailAsSpamAsync"), cancellationToken, email );
        }

        Task IClientService.EmailBouncedAsync( string email, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "EmailBouncedAsync"), cancellationToken, email );
        }

        Task IClientService.EmailDeliveredAsync( string email, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "EmailDeliveredAsync"), cancellationToken, email );
        }

        Task<(bool isAdmin, bool canPublish, bool isProductManager)> IClientService.GetNugetPermissionsAsync( CancellationToken cancellationToken)
        {
            return Get<ValueTuple<bool, bool, bool>>( GetCurrentMethod( "GetNugetPermissionsAsync"), cancellationToken);
        }

        Task IClientService.ChangePasswordAsync( string newPassword, long? clientId, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ChangePasswordAsync"), cancellationToken, newPassword, clientId );
        }

        Task IClientService.ResetPasswordAsync(CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ResetPasswordAsync"), cancellationToken, Array.Empty<object>());
        }

        Task IClientService.UpdateProfileAsync( DomainModel.Client changes, CancellationToken cancellationToken)
        {
            return Put( GetCurrentMethod( "UpdateProfileAsync"), cancellationToken, changes );
        }

        Task IClientService.UpdateReferralAsync( long referralId, CancellationToken cancellationToken)
        {
            return Put( GetCurrentMethod( "UpdateReferralAsync"), cancellationToken, referralId );
        }

        Task IClientService.BlockAsync( long clientId, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "BlockAsync"), cancellationToken, clientId );
        }

        Task IClientService.SubscribeAsync( bool enable, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "SubscribeAsync"), cancellationToken, enable );
        }

        Task IClientService.SelfDeleteAsync(CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "SelfDeleteAsync"), cancellationToken, Array.Empty<object>());
        }

        Task<bool> IClientService.IsInRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "IsInRoleAsync"), cancellationToken, roleId );
        }

        Task<string[]> IClientService.GetRolesAsync(
          long? clientId,
          CancellationToken cancellationToken)
        {
            return Get<string[]>( GetCurrentMethod( "GetRolesAsync"), cancellationToken, clientId );
        }

        Task IClientService.ReactivateAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ReactivateAsync"), cancellationToken, clientId );
        }

        Task IClientService.ResetNugetAsync(
          long clientId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ResetNugetAsync"), cancellationToken, clientId );
        }

        Task IClientService.SetRoleAsync(
          long clientId,
          long roleId,
          long? oneAppId,
          DateTime? till,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "SetRoleAsync"), cancellationToken, clientId, roleId, oneAppId, till );
        }

        Task<bool> IClientService.RemoveRoleAsync(
          long roleId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveRoleAsync"), cancellationToken, roleId );
        }

        Task IClientService.AddAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddAwardAsync"), cancellationToken, clientId, awardId );
        }

        Task<bool> IClientService.RemoveAwardAsync(
          long clientId,
          long awardId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveAwardAsync"), cancellationToken, clientId, awardId );
        }

        Task IClientService.AddLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddLicenseFeatureAsync"), cancellationToken, clientId, featureId );
        }

        Task<bool> IClientService.RemoveLicenseFeatureAsync(
          long clientId,
          long featureId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveLicenseFeatureAsync"), cancellationToken, clientId, featureId );
        }

        Task IClientService.AddTagAsync(
          string tagName,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddTagAsync"), cancellationToken, tagName );
        }

        Task<string> ISmsService.SendAsync(
          string phone,
          string message,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "SendAsync"), cancellationToken, phone, message );
        }
    }
}
