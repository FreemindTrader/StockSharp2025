
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using StockSharp.Algo;
using StockSharp.Studio.Community.Offline;
using StockSharp.Web.Api.Client;
using StockSharp.Web.Api.Interfaces;
using System;

namespace StockSharp.Studio.Community
{
    /// <summary>Services registry.</summary>
    public static class CommunityServicesRegistry
    {
        /// <summary>
        /// </summary>
        public static bool Offline { get; set; }

        /// <summary>
        /// </summary>
        public static IApiServiceProvider ApiServiceProvider
        {
            get
            {
                return ConfigManager.GetService<IApiServiceProvider>();
            }
        }

        /// <summary>Get required service instance.</summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <returns>Required service instance.</returns>
        public static TService GetService<TService>()
        {
            ServerCredentials credentials;
            if (!ServicesRegistry.CredentialsProvider.TryLoad(out credentials))
                throw new InvalidOperationException("TryLoadCredentials == false");
            if (CommunityServicesRegistry.Offline)
            {
                if (typeof(TService) == typeof(IFileService))
                    return new FileClient().To<TService>();
                if (typeof(TService) == typeof(IFileShareService))
                    return new FileShareClient().To<TService>();
                if (typeof(TService) == typeof(ILicenseService))
                    return new LicenseClient().To<TService>();
                if (typeof(TService) == typeof(IMessageService))
                    return new MessageClient().To<TService>();
                if (typeof(TService) == typeof(IProductService))
                    return new ProductClient().To<TService>();
                if (typeof(TService) == typeof(IProductFeedbackService))
                    return new ProductFeedbackClient().To<TService>();
                if (typeof(TService) == typeof(IProductBugReportService))
                    return new ProductBugReportClient().To<TService>();
                if (typeof(TService) == typeof(ISessionService))
                    return new SessionClient().To<TService>();
                if (typeof(TService) == typeof(IClientService))
                    return new ProfileClient().To<TService>();
                throw new ArgumentOutOfRangeException(typeof(TService).Name);
            }
            IApiServiceProvider apiServiceProvider = CommunityServicesRegistry.ApiServiceProvider;
            return (!credentials.Token.IsEmpty() ? apiServiceProvider.GetService<TService>(credentials.Token) : apiServiceProvider.GetService<TService>(credentials.Email, credentials.Password)).TrySetAsUser<TService>(true);
        }
    }
}
