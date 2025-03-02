// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiAlertNotificationService
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 54E25E17-EECA-4F64-ACCA-A2705D24CD36
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.dll

using Ecng.Logging;
using StockSharp.Alerts;
using StockSharp.Localization;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.WebApi
{
    /// <summary>
    /// <see cref="T:StockSharp.Alerts.IAlertNotificationService" /> version uses StockSharp WebAPI.
    /// </summary>
    public class WebApiAlertNotificationService : BaseLogReceiver, IAlertNotificationService, ILogSource, IDisposable
    {
        async ValueTask IAlertNotificationService.NotifyAsync( AlertNotifications type, long? externalId, LogLevels logLevel, string caption, string message, DateTimeOffset time, CancellationToken cancellationToken )
        {
            WebApiAlertNotificationService notificationService = this;
            if ( !externalId.HasValue )
                LoggingHelper.AddErrorLog( ( ILogReceiver ) notificationService, LocalizedStrings.ExternalIdIsNotSet, Array.Empty<object>() );
            else if ( WebApiServicesRegistry.Offline )
            {
                LoggingHelper.AddWarningLog( ( ILogReceiver ) notificationService, LocalizedStrings.OfflineWarning, Array.Empty<object>() );
            }
            else
            {
                IClientSocialService service = WebApiServicesRegistry.GetService<IClientSocialService>();
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 4);
                interpolatedStringHandler.AppendFormatted<LogLevels>( logLevel );
                interpolatedStringHandler.AppendLiteral( " caption: " );
                interpolatedStringHandler.AppendFormatted( caption );
                interpolatedStringHandler.AppendLiteral( "\r\ntime: " );
                interpolatedStringHandler.AppendFormatted<DateTimeOffset>( time );
                interpolatedStringHandler.AppendLiteral( "\r\nmessage: " );
                interpolatedStringHandler.AppendFormatted( message );
                string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                long[] socialIds = new long[1]{ externalId.Value };
                long[] attachIds = Array.Empty<long>();
                CancellationToken cancellationToken1 = cancellationToken;
                long? clientId = new long?();
                DateTime? schedule = new DateTime?();
                CancellationToken cancellationToken2 = cancellationToken1;
                ValueTuple<long, bool, string, string>[] valueTupleArray = await service.SendAsync(stringAndClear, socialIds, attachIds, clientId, schedule, cancellationToken2);
            }
        }

        public WebApiAlertNotificationService()
        {
            
        }
    }
}
