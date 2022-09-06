using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.Runtime.InteropServices;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3237" )]
    [Guid( "703384E8-0629-4D77-8771-A92B690AAD5A" )]
    [VectorIcon( "Logs" )]
    public class LogControlPanel : BaseStudioControl
    {
        private readonly LogControl _logControl = new LogControl();
        private readonly GuiLogListener _listener;

        public LogControlPanel()
        {
            this.Content = ( object )this._logControl;
            this._listener = new GuiLogListener( ( ILogListener )this._logControl );
            this.WhenLoaded( ( Action )( () => new AddLogListenerCommand( ( ILogListener )this._listener ).SyncProcess( ( object )this ) ) );
        }

        public override void Load( SettingsStorage storage )
        {
            this._logControl.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            this._logControl.Save( storage );
        }

        public override void Dispose()
        {
            new RemoveLogListenerCommand( ( ILogListener )this._listener ).Process( ( object )this, false );
            base.Dispose();
        }
    }
}
