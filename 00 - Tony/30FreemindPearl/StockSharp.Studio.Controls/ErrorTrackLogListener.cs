
using Ecng.Serialization;
using StockSharp.Algo.Testing;
using StockSharp.Logging;
using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Controls
{
    public class ErrorTrackLogListener : ILogListener, IPersistable, IDisposable
    {
        public bool HasErrors { get; set; }

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            foreach ( LogMessage message in messages )
            {
                if ( message.Level == LogLevels.Error && !( message.Source is HistoryEmulationConnector ) && ( !( message.Source is HistoryMessageAdapter ) && !( message.Source is EmulationMessageAdapter ) ) && !( message.Source is MarketEmulator ) )
                {
                    HasErrors = true;
                    break;
                }
            }
        }

        void IPersistable.Load( SettingsStorage storage )
        {
        }

        void IPersistable.Save( SettingsStorage storage )
        {
        }

        void IDisposable.Dispose()
        {
        }
    }
}
