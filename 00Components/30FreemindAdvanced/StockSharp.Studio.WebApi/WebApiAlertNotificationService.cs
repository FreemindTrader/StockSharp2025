// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiAlertNotificationService
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B97A7121-FFB7-49F4-8E30-FC5C471868BC
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.xml

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Logging;
using StockSharp.Alerts;
using StockSharp.Localization;
using StockSharp.Web.Api.Interfaces;

#nullable enable
namespace StockSharp.Studio.WebApi;

/// <summary>
/// <see cref="T:StockSharp.Alerts.IAlertNotificationService" /> version uses StockSharp WebAPI.
/// </summary>
public class WebApiAlertNotificationService :
  BaseLogReceiver,
  IAlertNotificationService,
  ILogSource,
  IDisposable
{
    async ValueTask IAlertNotificationService.NotifyAsync(
      AlertNotifications type,
      long? externalId,
      LogLevels logLevel,
#nullable disable
      string caption,
      string message,
      DateTimeOffset time,
      CancellationToken cancellationToken)
    {
        WebApiAlertNotificationService notificationService = this;
        if (!externalId.HasValue)
            notificationService.LogError(LocalizedStrings.ExternalIdIsNotSet, Array.Empty<object>());
        else if (WebApiServicesRegistry.Offline)
        {
            notificationService.LogWarning(LocalizedStrings.OfflineWarning, Array.Empty<object>());
        }
        else
        {
            IClientSocialService service = WebApiServicesRegistry.GetService<IClientSocialService>();
            string text = $"{logLevel} caption: {caption}\r\ntime: {time.ToLocalTime()}\r\nmessage: {message}";
            long[] socialIds = new long[1] { externalId.Value };
            long[] attachIds = Array.Empty<long>();
            CancellationToken cancellationToken1 = cancellationToken;
            long? clientId = new long?();
            DateTime? schedule = new DateTime?();
            CancellationToken cancellationToken2 = cancellationToken1;
            (long, bool, string, string)[] valueTupleArray = await service.SendAsync(text, socialIds, attachIds, clientId, schedule, cancellationToken2);
        }
    }
}
