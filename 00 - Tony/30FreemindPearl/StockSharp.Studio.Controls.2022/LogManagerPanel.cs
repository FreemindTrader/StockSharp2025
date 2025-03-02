
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
    [DescriptionLoc( "Str3237", true )]
    [Guid( "F97DCB8B-2104-4DF3-B6C5-CBB2B8B3B704" )]
    [DockingWindowType( true )]
    [VectorIcon( "Logs" )]
    [Doc( "topics/Designer_Panel_Logs.html" )]
    public class LogManagerPanel : BaseStudioControl
    {
        private readonly Monitor _monitor = new Monitor();
        private readonly GuiLogListener _listener;

        public LogManagerPanel()
        {
            Content = _monitor;
            _listener = new GuiLogListener( _monitor );
            LogManager.Listeners.Add( _listener );
            _monitor.LayoutChanged += RaiseChangedCommand;
        }

        public bool ShowStrategies
        {
            get
            {
                return _monitor.ShowStrategies;
            }
            set
            {
                _monitor.ShowStrategies = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            _monitor.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            _monitor.Save( storage );
        }

        public override void Dispose()
        {
            _monitor.LayoutChanged -= RaiseChangedCommand;
            new RemoveLogListenerCommand( _listener ).Process( this, false );
            base.Dispose();
        }
    }
}
