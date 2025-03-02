// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogPreviewButtonItem
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    public partial class LogPreviewButtonItem : BarButtonItem, ILogListener, IPersistable, IDisposable, IStudioControl
    {
        public static readonly DependencyProperty HasErrorsProperty = DependencyProperty.Register(nameof (HasErrors), typeof (bool), typeof (LogPreviewButtonItem), new PropertyMetadata((object) false));
        private bool _logControlOpened;
        

        private bool HasErrors
        {
            get
            {
                return ( bool ) this.GetValue( LogPreviewButtonItem.HasErrorsProperty );
            }
            set
            {
                this.SetValue( LogPreviewButtonItem.HasErrorsProperty,  value );
            }
        }

        public LogPreviewButtonItem()
        {
            this.InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            StudioServicesRegistry.CommandService.Register<ControlOpenedCommand>(  this, false, ( Action<ControlOpenedCommand> ) ( cmd =>
            {
                this._logControlOpened = cmd.Control.GetType() == typeof( LogManagerPanel );
                if ( !this._logControlOpened )
                    return;
                GuiDispatcher.GlobalDispatcher.AddAction( ( Action ) ( () => this.HasErrors = false ) );
            } ), ( Func<ControlOpenedCommand, bool> ) null );
        }

        private void LogPreviewButtonItem_OnItemClick( object sender, ItemClickEventArgs e )
        {
            new OpenWindowCommand( typeof( LogManagerPanel ), true ).SyncProcess(  this );
            this.HasErrors = false;
        }

        bool ILogListener.CanSave
        {
            get
            {
                return false;
            }
        }

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            if ( this._logControlOpened )
                return;
            using ( IEnumerator<LogMessage> enumerator = messages.GetEnumerator() )
            {
                while ( ( ( IEnumerator ) enumerator ).MoveNext() )
                {
                    if ( enumerator.Current.Level == LogLevels.Error )
                    {
                        this._logControlOpened = true;
                        GuiDispatcher.GlobalDispatcher.AddAction( ( Action ) ( () => this.HasErrors = true ) );
                    }
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
            GC.SuppressFinalize(  this );
        }

        string IStudioControl.Title
        {
            get
            {
                return ( string ) null;
            }
            set
            {
            }
        }

        void IStudioControl.FirstTimeInit()
        {
        }

        void IStudioControl.SendCommand( IStudioCommand command )
        {
        }

        ImageSource IStudioControl.Icon
        {
            get
            {
                return ( ImageSource ) null;
            }
        }

        string IStudioControl.Key { get; set; }

        string IStudioControl.DocUrl
        {
            get
            {
                return ( string ) null;
            }
        }

        bool IStudioControl.SaveWithLayout
        {
            get
            {
                return false;
            }
        }

        bool IStudioControl.IsTitleEditable
        {
            get
            {
                return false;
            }
        }        
    }
}
