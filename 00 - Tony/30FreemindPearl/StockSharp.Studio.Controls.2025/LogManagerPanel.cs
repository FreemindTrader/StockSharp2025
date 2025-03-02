// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogManagerPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "Logs", Name = "Logs", ResourceType = typeof( LocalizedStrings ) )]
    [Guid( "F97DCB8B-2104-4DF3-B6C5-CBB2B8B3B704" )]
    [ToolWindow]
    [VectorIcon( "Logs" )]
    [Doc( "topics/designer/user_interface/logs.html" )]
    public class LogManagerPanel : BaseStudioControl
    {
        private readonly Monitor _monitor = new Monitor();
        private readonly GuiLogListener _listener;

        public LogManagerPanel()
        {
            this.Content =  this._monitor;
            this._listener = new GuiLogListener( ( ILogListener ) this._monitor );
            if ( this.IsDesignMode() )
                return;
            BaseStudioControl.LogManager.Listeners.Add(  this._listener );
            this._monitor.LayoutChanged += RaiseChangedCommand;
        }

        public bool ShowStrategies
        {
            get
            {
                return this._monitor.ShowStrategies;
            }
            set
            {
                this._monitor.ShowStrategies = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            this._monitor.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            this._monitor.Save( storage );
        }

        public override void Dispose( CloseReason reason )
        {
            this._monitor.LayoutChanged -= RaiseChangedCommand;
            BaseStudioControl.LogManager.Listeners.Remove( ( ILogListener ) this._listener );
            base.Dispose( reason );
        }
    }
}
