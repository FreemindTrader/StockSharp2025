
using Ecng.Common;
using Ecng.Net;
using Ecng.Reflection;
using Newtonsoft.Json;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public abstract class BaseApiClient : RestBaseApiClient, IBaseService
    {
        public static Uri DefaultBaseAddress = "https://api.stocksharp.com/v1/".To<Uri>();

        private static MediaTypeFormatter CreateFormatter()
        {
            JsonMediaTypeFormatter mediaTypeFormatter = new JsonMediaTypeFormatter();
            mediaTypeFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            return mediaTypeFormatter;
        }

        private BaseApiClient(HttpMessageInvoker http) : base(http, CreateFormatter(), CreateFormatter())
        {
            BaseAddress = new Uri(DefaultBaseAddress, GetType().Name.Remove("ApiClient", true).ToLowerInvariant() + "/");
        }

        protected BaseApiClient(HttpMessageInvoker http, SecureString token) : this(http)
        {
            PerRequestHeaders.Add("SS-AUTH", token.UnSecure());
        }

        protected BaseApiClient(HttpMessageInvoker client, string login, SecureString password) : this(client)
        {
            PerRequestHeaders.Add("SS-LGN", login);
            PerRequestHeaders.Add("SS-PWD-64", password.UnSecure().UTF8().Base64());
        }

        ContextContainer IBaseService.ContextContainer
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        protected Task<TResult> Get<TResult>( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return GetAsync<TResult>(methodName, cancellationToken, args);
        }

        protected Task Post( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return PostAsync<VoidType>(methodName, cancellationToken, args);
        }

        protected Task<TResult> Post<TResult>( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return PostAsync<TResult>(methodName, cancellationToken, args);
        }

        protected Task Put( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return PutAsync<VoidType>(methodName, cancellationToken, args);
        }

        protected Task<TResult> Put<TResult>( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return PutAsync<TResult>(methodName, cancellationToken, args);
        }

        protected Task Delete( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return DeleteAsync<VoidType>(methodName, cancellationToken, args);
        }

        protected Task<TResult> Delete<TResult>( string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return DeleteAsync<TResult>(methodName, cancellationToken, args);
        }

        protected override async Task ValidateResponseAsync( HttpResponseMessage response, CancellationToken cancellationToken)
        {
            try
            {
                await base.ValidateResponseAsync(response, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                if (!response.RequestMessage.Headers.Contains("SS-EI"))
                    throw;
                try
                {
                    ex = new HttpRequestException(string.Format("{0} ({1}): {2}", (int)response.StatusCode, response.StatusCode, (await response.Content.ReadAsAsync<ErrorInfo>(cancellationToken)).Error));
                }
                catch
                {
                }
                throw ex;
            }
        }

        protected object TryFormat(object arg, HttpMethod method)
        {
            if (method == HttpMethod.Get && arg is DateTime)
                return ((DateTime)arg).To<long>();

            return arg;
        }
    }
}
