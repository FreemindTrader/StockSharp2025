// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.BaseApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

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
using Ecng.Common;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Reflection;
using Newtonsoft.Json;

#nullable disable
namespace StockSharp.Web.Api.Client;

public abstract class BaseApiClient : RestBaseApiClient
{
    private readonly BaseApiClient.LogSource _source;
    private ILogReceiver _logs;

    private static MediaTypeFormatter CreateFormatter()
    {
        JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
        formatter.SerializerSettings.NullValueHandling = (NullValueHandling)1;
        formatter.SerializerSettings.ReferenceLoopHandling = (ReferenceLoopHandling)1;
        return (MediaTypeFormatter)formatter;
    }

    private BaseApiClient(Uri baseAddress, HttpMessageInvoker http)
      : base(http, BaseApiClient.CreateFormatter(), BaseApiClient.CreateFormatter())
    {
        this._source = new BaseApiClient.LogSource(this);
        this.BaseAddress = new Uri(baseAddress, StringHelper.Remove(((object)this).GetType().Name, "ApiClient", true).ToLowerInvariant() + "/");
        this.RetryPolicy.ReadMaxCount = 5;
    }

    protected BaseApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : this(baseAddress, http)
    {
        this.AddAuthBearer(StringHelper.UnSecure(token));
    }

    protected BaseApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : this(baseAddress, http)
    {
        this.AddAuth(AuthenticationSchemes.Basic, StringHelper.Base64(StringHelper.UTF8($"{login}:{StringHelper.UnSecure(password)}")));
    }

    internal ILogReceiver Logs
    {
        get => this._logs;
        set
        {
            this._logs = value;
            this.Tracing = value != null;
        }
    }

    protected override void TraceCall(HttpMethod method, Uri uri, TimeSpan elapsed)
    {
        ILogReceiver logs = this.Logs;
        if (logs == null || LogLevels.Debug < logs.LogLevel)
            return;
        logs.AddLog(new LogMessage((ILogSource)this._source, DateTimeOffset.Now, (LogLevels)2, "{0} {1} ({2})", new object[3]
        {
         method,
         uri,
         elapsed
        }));
    }

    protected Task<TResult> Get<TResult>(
      string methodName,
      CancellationToken cancellationToken,
      object[] args)
    {
        return this.GetAsync<TResult>(methodName, cancellationToken, args);
    }

    protected Task Post(string methodName, CancellationToken cancellationToken, object[] args)
    {
        return (Task)this.PostAsync<VoidType>(methodName, cancellationToken, args);
    }

    protected Task<TResult> Post<TResult>(
      string methodName,
      CancellationToken cancellationToken,
      object[] args)
    {
        return this.PostAsync<TResult>(methodName, cancellationToken, args);
    }

    protected Task Put(string methodName, CancellationToken cancellationToken, object[] args)
    {
        return (Task)this.PutAsync<VoidType>(methodName, cancellationToken, args);
    }

    protected Task<TResult> Put<TResult>(
      string methodName,
      CancellationToken cancellationToken,
      object[] args)
    {
        return this.PutAsync<TResult>(methodName, cancellationToken, args);
    }

    protected Task Delete(string methodName, CancellationToken cancellationToken, object[] args)
    {
        return (Task)this.DeleteAsync<VoidType>(methodName, cancellationToken, args);
    }

    protected Task<TResult> Delete<TResult>(
      string methodName,
      CancellationToken cancellationToken,
      object[] args)
    {
        return this.DeleteAsync<TResult>(methodName, cancellationToken, args);
    }

    protected override async Task ValidateResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            await base.ValidateResponseAsync(response, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            HttpRequestException requestException = ex;
            IEnumerable<string> values;
            if (!response.Headers.TryGetValues("SS-EI-64", out values))
                throw;
            try
            {
                requestException = NetworkHelper.CreateHttpRequestException(response.StatusCode, StringHelper.JoinNL(values.Select<string, string>((Func<string, string>)(s => StringHelper.UTF8(StringHelper.Base64(s))))));
            }
            catch
            {
            }
            throw requestException;
        }
    }

    protected override object TryFormat(object arg, MethodInfo callerMethod, HttpMethod method)
    {
        return arg is DateTime ? (object)Converter.To<long>(arg) : base.TryFormat(arg, callerMethod, method);
    }

    private class LogSource(BaseApiClient client) : ILogSource, IDisposable
    {
        private readonly BaseApiClient _client = client ?? throw new ArgumentNullException(nameof(client));

        Guid ILogSource.Id => throw new NotSupportedException();

        DateTimeOffset ILogSource.CurrentTime => DateTimeOffset.UtcNow;

        bool ILogSource.IsRoot => false;

        string ILogSource.Name
        {
            get => ((object)this._client).GetType().Name;
            set => throw new NotSupportedException();
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
