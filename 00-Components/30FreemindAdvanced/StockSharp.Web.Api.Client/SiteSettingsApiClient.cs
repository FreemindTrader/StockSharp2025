// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.SiteSettingsApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net;
using Ecng.Net.Captcha;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class SiteSettingsApiClient :
  BaseApiEntityClient<SiteSettings>,
  ISiteSettingsService,
  IBaseEntityService<SiteSettings>,
  ICaptchaService,
  ICaptchaValidator<bool>
{
    public SiteSettingsApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public SiteSettingsApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<SiteSettings>> ISiteSettingsService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<SiteSettings>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[8]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd
        });
    }

    Task ISiteSettingsService.ResetCacheAsync(CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ResetCacheAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<byte[]> ISiteSettingsService.SignAsync(byte[] data, CancellationToken cancellationToken)
    {
        return this.Post<byte[]>(RestBaseApiClient.GetCurrentMethod("SignAsync"), cancellationToken, new object[1]
        {
      (object) data
        });
    }

    Task<(string value1, string value2, string value3)> ISiteSettingsService.EncryptUrlAsync(
      string value1,
      string value2,
      string value3,
      CancellationToken cancellationToken)
    {
        return this.Post<(string, string, string)>(RestBaseApiClient.GetCurrentMethod("EncryptUrlAsync"), cancellationToken, new object[3]
        {
      (object) value1,
      (object) value2,
      (object) value3
        });
    }

    Task<(string value1, string value2, string value3)> ISiteSettingsService.DecryptUrlAsync(
      string value1,
      string value2,
      string value3,
      CancellationToken cancellationToken)
    {
        return this.Post<(string, string, string)>(RestBaseApiClient.GetCurrentMethod("DecryptUrlAsync"), cancellationToken, new object[3]
        {
      (object) value1,
      (object) value2,
      (object) value3
        });
    }

    Task<string> ISiteSettingsService.GetStatAsync(
      long skip,
      long? count,
      CancellationToken cancellationToken)
    {
        return this.Get<string>(RestBaseApiClient.GetCurrentMethod("GetStatAsync"), cancellationToken, new object[2]
        {
      (object) skip,
      (object) count
        });
    }

    Task<BaseEntitySet<SiteSettingsKeys>> ISiteSettingsService.FindKeysAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      string appName,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<SiteSettingsKeys>>(RestBaseApiClient.GetCurrentMethod("FindKeysAsync"), cancellationToken, new object[9]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) appName
        });
    }

    Task<SiteSettingsKeys> ISiteSettingsService.AddKeysAsync(
      SiteSettingsKeys keys,
      CancellationToken cancellationToken)
    {
        return this.Post<SiteSettingsKeys>(RestBaseApiClient.GetCurrentMethod("AddKeysAsync"), cancellationToken, new object[1]
        {
      (object) keys
        });
    }

    Task<bool> ISiteSettingsService.RemoveKeysAsync(long keysId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveKeysAsync"), cancellationToken, new object[1]
        {
      (object) keysId
        });
    }

    Task<(string name, string link)[]> ISiteSettingsService.GoogleSearchAsync(
      long skip,
      long? count,
      long domainId,
      string query,
      CancellationToken cancellationToken)
    {
        return this.Post<(string, string)[]>(RestBaseApiClient.GetCurrentMethod("GoogleSearchAsync"), cancellationToken, new object[4]
        {
      (object) skip,
      (object) count,
      (object) domainId,
      (object) query
        });
    }

    Task<float> ICaptchaService.GetScoreAsync(IPAddress address, CancellationToken cancellationToken)
    {
        return this.Get<float>(RestBaseApiClient.GetCurrentMethod("GetScoreAsync"), cancellationToken, new object[1]
        {
      (object) address
        });
    }

    Task<BaseEntitySet<(string address, DateTime time, float? score, string message)>> ICaptchaService.GetPendingAsync(
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<(string, DateTime, float?, string)>>(RestBaseApiClient.GetCurrentMethod("GetPendingAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<bool> ICaptchaValidator<bool>.ValidateAsync(
      string response,
      string address,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("ValidateAsync"), cancellationToken, new object[2]
        {
      (object) response,
      (object) address
        });
    }
}
