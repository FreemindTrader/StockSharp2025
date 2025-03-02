// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StatusBarPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.WebApi;
using StockSharp.Web.DomainModel;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    public partial class StatusBarPanel : BaseStudioControl, ILogListener, IPersistable, IDisposable
    {
        public static readonly RoutedCommand CopyToBufferCommand = new RoutedCommand();
        public static readonly RoutedCommand LogDirectoryCommand = new RoutedCommand();
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof (Status), typeof (string), typeof (StatusBarPanel));
        public static readonly DependencyProperty LastLogInfoProperty = DependencyProperty.Register(nameof (LastLogInfo), typeof (StatusBarPanel.LogInfo), typeof (StatusBarPanel), new PropertyMetadata((object) new StatusBarPanel.LogInfo()));
        public static readonly DependencyProperty LoginInfoProperty = DependencyProperty.Register(nameof (LoginInfo), typeof (string), typeof (StatusBarPanel));
        
        public string Status
        {
            get
            {
                return ( string ) this.GetValue( StatusBarPanel.StatusProperty );
            }
            set
            {
                this.SetValue( StatusBarPanel.StatusProperty,  value );
            }
        }

        public StatusBarPanel.LogInfo LastLogInfo
        {
            get
            {
                return ( StatusBarPanel.LogInfo ) this.GetValue( StatusBarPanel.LastLogInfoProperty );
            }
            set
            {
                this.SetValue( StatusBarPanel.LastLogInfoProperty,  value );
            }
        }

        public string LoginInfo
        {
            get
            {
                return ( string ) this.GetValue( StatusBarPanel.LoginInfoProperty );
            }
            set
            {
                this.SetValue( StatusBarPanel.LoginInfoProperty,  value );
            }
        }

        public StatusBarPanel()
        {
            this.InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            this.Register<EntityCommand<News>>(  this, true, ( Action<EntityCommand<News>> ) ( cmd =>
            {
                if ( !cmd.Entity.IsStockSharp() )
                    return;
                this.AdvertisePanel.AddNews( cmd.Entity );
            } ), ( Func<EntityCommand<News>, bool> ) null );
            WebApiHelper.ProfileChanged += new Action( this.RefreshLoginString );
        }

        public override void Dispose( CloseReason reason )
        {
            WebApiHelper.ProfileChanged -= new Action( this.RefreshLoginString );
            base.Dispose( reason );
        }

        public void ResetLogsImages()
        {
            this.LastLogInfo.LastWarnVisible = Visibility.Collapsed;
            this.LastLogInfo.LastErrorVisible = Visibility.Collapsed;
            this.LastLogInfo.LastLogMessage = string.Empty;
        }

        private void RefreshLoginString()
        {
            ( ( DispatcherObject ) this ).GuiAsync( ( Action ) ( () => this.LoginInfo = WebApiHelper.Profile?.DisplayName ) );
        }

        private void CopyToBufferCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !StringHelper.IsEmpty( this.LastLogInfo.LastLogMessage );
        }

        private void CopyToBufferCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.LastLogInfo.LastLogMessage.CopyToClipboard<string>();
        }

        private void LogDirectoryCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void LogDirectoryCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.ResetLogsImages();
            string logsDir = StudioUserConfig.Instance.LogConfig.LogsDir;
            if ( !Directory.Exists( logsDir ) )
                return;
            logsDir.TryOpenLink( ( DependencyObject ) this );
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
            using ( IEnumerator<LogMessage> enumerator = messages.GetEnumerator() )
            {
                while ( ( ( IEnumerator ) enumerator ).MoveNext() )
                {
                    LogMessage message = enumerator.Current;
                    if ( ( message.Level == LogLevels.Warning || message.Level == LogLevels.Error ) && ( !( message.Source is HistoryEmulationConnector ) && !( message.Source is HistoryMessageAdapter ) ) && ( !( message.Source is EmulationMessageAdapter ) && !( message.Source is MarketEmulator ) ) )
                    {
                        Task<ProductBugReport> task = message.TrySendBugReport(new CancellationToken());
                        if ( task != null )
                            LoggingHelper.ObserveErrorAndTrace( ( Task ) task );
                        GuiDispatcher.GlobalDispatcher.AddAction( ( Action ) ( () =>
                        {
                            StatusBarPanel.LogInfo lastLogInfo = this.LastLogInfo;
                            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
                            interpolatedStringHandler.AppendFormatted<DateTimeOffset>( message.Time, "HH:mm:ss" );
                            interpolatedStringHandler.AppendLiteral( "  " );
                            interpolatedStringHandler.AppendFormatted( message.Message );
                            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                            lastLogInfo.LastLogMessage = stringAndClear;
                            if ( message.Level == LogLevels.Warning )
                            {
                                this.LastLogInfo.LastWarnVisible = Visibility.Visible;
                                this.LastLogInfo.LastErrorVisible = Visibility.Collapsed;
                            }
                            else
                            {
                                this.LastLogInfo.LastWarnVisible = Visibility.Collapsed;
                                this.LastLogInfo.LastErrorVisible = Visibility.Visible;
                            }
                        } ) );
                    }
                }
            }
        }

        

        public class LogInfo : NotifiableObject
        {
            private Visibility _lastErrorVisible;
            private Visibility _lastWarnVisible;
            private string _lastLogMessage;

            public Visibility LastErrorVisible
            {
                get
                {
                    return this._lastErrorVisible;
                }
                set
                {
                    if ( this._lastErrorVisible == value )
                        return;
                    this._lastErrorVisible = value;
                    this.NotifyChanged( nameof( LastErrorVisible ) );
                }
            }

            public Visibility LastWarnVisible
            {
                get
                {
                    return this._lastWarnVisible;
                }
                set
                {
                    if ( this._lastWarnVisible == value )
                        return;
                    this._lastWarnVisible = value;
                    this.NotifyChanged( nameof( LastWarnVisible ) );
                }
            }

            public string LastLogMessage
            {
                get
                {
                    return this._lastLogMessage;
                }
                set
                {
                    if ( this._lastLogMessage == value )
                        return;
                    this._lastLogMessage = value;
                    this.NotifyChanged( nameof( LastLogMessage ) );
                }
            }

            public LogInfo()
            {
                
            }
        }
    }
}
