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
            Content = _logControl;
            _listener = new GuiLogListener( _logControl );
            WhenLoaded( () => new AddLogListenerCommand( _listener ).SyncProcess( this ) );
        }

        public override void Load( SettingsStorage storage )
        {
            _logControl.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            _logControl.Save( storage );
        }

        public override void Dispose()
        {
            new RemoveLogListenerCommand( _listener ).Process( this, false );
            base.Dispose();
        }
    }
}
