// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogPreviewButtonItem
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Logging;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class LogPreviewButtonItem : BarButtonItem, ILogListener, IPersistable, IDisposable, IStudioControl, IComponentConnector
    {
        public static readonly DependencyProperty HasErrorsProperty = DependencyProperty.Register( nameof( HasErrors ), typeof( bool ), typeof( LogPreviewButtonItem ), new PropertyMetadata( false ) );
        private bool _logControlOpened;
        

        private bool HasErrors
        {
            get
            {
                return ( bool )GetValue( HasErrorsProperty );
            }
            set
            {
                SetValue( HasErrorsProperty, value );
            }
        }

        public LogPreviewButtonItem()
        {
            InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            StudioServicesRegistry.CommandService.Register<ControlOpenedCommand>( this, false, cmd =>
                {
                    _logControlOpened = cmd.Control.GetType() == typeof( LogManagerPanel );
                    if ( !_logControlOpened )
                        return;
                    GuiDispatcher.GlobalDispatcher.AddAction( () => HasErrors = false );
                }, null );
        }

        private void LogPreviewButtonItem_OnItemClick( object sender, ItemClickEventArgs e )
        {
            new OpenWindowCommand( typeof( LogManagerPanel ).GUID.ToString(), typeof( LogManagerPanel ), true ).SyncProcess( this );
            HasErrors = false;
        }

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            if ( _logControlOpened )
                return;
            foreach ( LogMessage message in messages )
            {
                if ( message.Level == LogLevels.Error )
                {
                    _logControlOpened = true;
                    GuiDispatcher.GlobalDispatcher.AddAction( () => HasErrors = true );
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

        string IStudioControl.Title
        {
            get
            {
                return null;
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

        Uri IStudioControl.Icon
        {
            get
            {
                return null;
            }
        }

        string IStudioControl.Key { get; set; }

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
