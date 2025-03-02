// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.BaseApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public abstract class BaseApiClient : RestBaseApiClient
    {
        private readonly BaseApiClient.LogSource _source;
        private ILogReceiver _logs;

        private static MediaTypeFormatter CreateFormatter()
        {
            JsonMediaTypeFormatter mediaTypeFormatter = new JsonMediaTypeFormatter();
            mediaTypeFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; 
            mediaTypeFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
            return ( MediaTypeFormatter ) mediaTypeFormatter;
        }

        private BaseApiClient( Uri baseAddress, HttpMessageInvoker http ) : base( http, BaseApiClient.CreateFormatter(), BaseApiClient.CreateFormatter() )
        {            
            this._source = new BaseApiClient.LogSource( this );
            this.BaseAddress = ( new Uri( baseAddress, StringHelper.Remove( ( ( object ) this ).GetType().Name, "ApiClient", true ).ToLowerInvariant() + "/" ) );
            this.RetryPolicy.ReadMaxCount = 5;
        }

        protected BaseApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : this( baseAddress, http )
        {
            this.AddAuthBearer( StringHelper.UnSecure( token ) );
        }

        protected BaseApiClient( Uri baseAddress, HttpMessageInvoker http, string login, SecureString password )
          : this( baseAddress, http )
        {
            this.AddAuth( AuthenticationSchemes.Basic, StringHelper.Base64( StringHelper.UTF8( login + ":" + StringHelper.UnSecure( password ) ) ) );
        }

        internal ILogReceiver Logs
        {
            get
            {
                return this._logs;
            }
            set
            {
                this._logs = value;
                this.Tracing = value != null;
            }
        }

        protected virtual void TraceCall( HttpMethod method, Uri uri, TimeSpan elapsed )
        {
            ILogReceiver logs = this.Logs;
            if ( logs == null || LogLevels.Debug < logs.LogLevel )
                return;
            logs.AddLog( new LogMessage( ( ILogSource ) this._source, DateTimeOffset.Now, ( LogLevels ) 2, "{0} {1} ({2})", new object [3]
            {
         method,
         uri,
         elapsed
            } ) );
        }

        protected Task<TResult> Get<TResult>(
          string methodName,
          CancellationToken cancellationToken,
          object [ ] args )
        {
            return this.GetAsync<TResult>( methodName, cancellationToken, args );
        }

        protected Task Post( string methodName, CancellationToken cancellationToken, object [ ] args )
        {
            return ( Task ) this.PostAsync<VoidType>( methodName, cancellationToken, args );
        }

        protected Task<TResult> Post<TResult>(
          string methodName,
          CancellationToken cancellationToken,
          object [ ] args )
        {
            return this.PostAsync<TResult>( methodName, cancellationToken, args );
        }

        protected Task Put( string methodName, CancellationToken cancellationToken, object [ ] args )
        {
            return ( Task ) this.PutAsync<VoidType>( methodName, cancellationToken, args );
        }

        protected Task<TResult> Put<TResult>(
          string methodName,
          CancellationToken cancellationToken,
          object [ ] args )
        {
            return this.PutAsync<TResult>( methodName, cancellationToken, args );
        }

        protected Task Delete(
          string methodName,
          CancellationToken cancellationToken,
          object [ ] args )
        {
            return ( Task ) this.DeleteAsync<VoidType>( methodName, cancellationToken, args );
        }

        protected Task<TResult> Delete<TResult>(
          string methodName,
          CancellationToken cancellationToken,
          object [ ] args )
        {
            return this.DeleteAsync<TResult>( methodName, cancellationToken, args );
        }

        protected virtual async Task ValidateResponseAsync(
          HttpResponseMessage response,
          CancellationToken cancellationToken )
        {
            try
            {
                await base.ValidateResponseAsync( response, cancellationToken );
            }
            catch ( HttpRequestException ex )
            {
                HttpRequestException requestException = ex;
                IEnumerable<string> values;
                if ( !response.Headers.TryGetValues( "SS-EI-64", out values ) )
                {
                    throw;
                }
                else
                {
                    try
                    {
                        requestException = NetworkHelper.CreateHttpRequestException( response.StatusCode, StringHelper.JoinNL( values.Select<string, string>( ( Func<string, string> ) ( s => StringHelper.UTF8( StringHelper.Base64( s ) ) ) ) ) );
                    }
                    catch
                    {
                    }
                    throw requestException;
                }
            }
        }

        protected virtual object TryFormat( object arg, MethodInfo callerMethod, HttpMethod method )
        {
            if ( arg is DateTime )
                return ( object ) ( long ) Converter.To<long>( arg );
            return base.TryFormat( arg, callerMethod, method );
        }

        private class LogSource : ILogSource, IDisposable
        {
            private readonly BaseApiClient _client;

            public LogSource( BaseApiClient client )
            {
                BaseApiClient baseApiClient = client;
                if ( baseApiClient == null )
                    throw new ArgumentNullException( nameof( client ) );
                this._client = baseApiClient;                
            }

            Guid ILogSource.Id
            {
                get
                {
                    throw new NotSupportedException();
                }
            }

            DateTimeOffset ILogSource.CurrentTime
            {
                get
                {
                    return DateTimeOffset.UtcNow;
                }
            }

            bool ILogSource.IsRoot
            {
                get
                {
                    return false;
                }
            }

            string ILogSource.Name
            {
                get
                {
                    return ( ( object ) this._client ).GetType().Name;
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            ILogSource ILogSource.Parent { get; set; }

            LogLevels ILogSource.LogLevel { get; set; }

            event Action<ILogSource> ILogSource.ParentRemoved
            {
                add
                {
                }
                remove
                {
                }
            }

            event Action<LogMessage> ILogSource.Log
            {
                add
                {
                }
                remove
                {
                }
            }

            void IDisposable.Dispose()
            {
            }
        }
    }
}
