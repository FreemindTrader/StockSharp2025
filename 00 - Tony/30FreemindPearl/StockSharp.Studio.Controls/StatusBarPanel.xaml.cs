// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StatusBarPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using Nito.AsyncEx;
using StockSharp.Algo;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Community;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class StatusBarPanel : BaseStudioControl, ILogListener, IPersistable, IDisposable, IComponentConnector
    {
        public static readonly RoutedCommand CopyToBufferCommand = new RoutedCommand();
        public static readonly RoutedCommand LogDirectoryCommand = new RoutedCommand();
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register( nameof( Status ), typeof( string ), typeof( StatusBarPanel ) );
        public static readonly DependencyProperty LastLogInfoProperty = DependencyProperty.Register( nameof( LastLogInfo ), typeof( StatusBarPanel.LogInfo ), typeof( StatusBarPanel ), new PropertyMetadata( ( object )new StatusBarPanel.LogInfo() ) );
        private readonly SubscriptionManager _subscriptionManager;
        private int _uniqueBugReports;
        private const int _maxUniqueBugReports = 10;
        
        public string Status
        {
            get
            {
                return ( string )this.GetValue( StatusBarPanel.StatusProperty );
            }
            set
            {
                this.SetValue( StatusBarPanel.StatusProperty, ( object )value );
            }
        }

        public StatusBarPanel.LogInfo LastLogInfo
        {
            get
            {
                return ( StatusBarPanel.LogInfo )this.GetValue( StatusBarPanel.LastLogInfoProperty );
            }
            set
            {
                this.SetValue( StatusBarPanel.LastLogInfoProperty, ( object )value );
            }
        }

        public StatusBarPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            if ( this.IsDesignMode() )
                return;
            BaseStudioControl.CommandService.Register<EntityCommand<News>>( ( object )this, true, ( Action<EntityCommand<News>> )( cmd => this.AdvertisePanel.AddNews( cmd.Entities.Where<News>( ( Func<News, bool> )( n => n.IsStockSharp() ) ) ) ), ( Func<EntityCommand<News>, bool> )null );
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.News, ( Action<StockSharp.Algo.Subscription> )null ) ) );
        }

        public override void Dispose( CloseReason reason )
        {
            BaseStudioControl.CommandService.UnRegister<EntityCommand<News>>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public void ResetLogsImages()
        {
            this.LastLogInfo.LastWarnVisible = Visibility.Collapsed;
            this.LastLogInfo.LastErrorVisible = Visibility.Collapsed;
            this.LastLogInfo.LastLogMessage = string.Empty;
        }

        private void CopyToBufferCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !this.LastLogInfo.LastLogMessage.IsEmpty();
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
            string logsDir = BaseUserConfig<StudioUserConfig>.Instance.LogConfig.LogsDir;
            if ( !Directory.Exists( logsDir ) )
                return;
            logsDir.TryOpenLink( ( DependencyObject )this );
        }

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            foreach ( LogMessage message1 in messages )
            {
                LogMessage message = message1;
                if ( ( message.Level == LogLevels.Warning || message.Level == LogLevels.Error ) && ( !( message.Source is HistoryEmulationConnector ) && !( message.Source is HistoryMessageAdapter ) ) && ( !( message.Source is EmulationMessageAdapter ) && !( message.Source is MarketEmulator ) ) )
                {
                    if ( message.Level == LogLevels.Error && this._uniqueBugReports < 10 )
                    {
                        Client profile = Extensions.Profile;
                        int num;
                        if ( profile == null )
                        {
                            num = 0;
                        }
                        else
                        {
                            bool? isAllowStatistics = profile.IsAllowStatistics;
                            bool flag = true;
                            num = isAllowStatistics.GetValueOrDefault() == flag & isAllowStatistics.HasValue ? 1 : 0;
                        }
                        if ( num != 0 )
                        {
                            try
                            {
                                IProductBugReportService reportSvc = CommunityServicesRegistry.GetService<IProductBugReportService>();
                                ProductBugReport bugReport = AsyncContext.Run<ProductBugReport>( ( Func<Task<ProductBugReport>> )( () =>
                                   {
                                       IProductBugReportService bugReportService = reportSvc;
                                       ProductBugReport entity = new ProductBugReport();
                                       entity.Product = new Product()
                                       {
                                           Id = Helper.ProductId
                                       };
                                       entity.Version = Paths.InstalledVersion;
                                       entity.SystemInfo = Extensions.GetSystemInfo();
                                       entity.Message = new StockSharp.Web.DomainModel.Message()
                                       {
                                           Body = message.Message
                                       };
                                       CancellationToken cancellationToken = new CancellationToken();
                                       return bugReportService.TryProposeAsync( entity, cancellationToken );
                                   } ) );
                                if ( bugReport != null )
                                {
                                    ++this._uniqueBugReports;
                                    string zip = Extensions.PrepareLogsFile( TimeSpan.FromDays( 1.0 ), ( Action<Exception> )( ex => { } ) );
                                    try
                                    {
                                        using ( FileStream body = System.IO.File.OpenRead( zip ) )
                                        {
                                            //IFileService fileSvc = CommunityServicesRegistry.GetService<IFileService>();
                                            //AsyncContext.Run<StockSharp.Web.DomainModel.File>( ( Func<Task<StockSharp.Web.DomainModel.File>> )( () =>
                                            //   {
                                            //       IFileService service = fileSvc;
                                            //       StockSharp.Web.DomainModel.File file = new StockSharp.Web.DomainModel.File();
                                            //       file.Message = bugReport.Message;
                                            //       file.Name = Path.GetFileName( zip );
                                            //       FileStream fileStream = body;
                                            //       Compressions? compression = new Compressions?();
                                            //       CancellationToken cancellationToken = new CancellationToken();
                                            //       return service.UploadFullAsync( file, ( Stream )fileStream, 102400, ( Action<long> )null, compression, cancellationToken );
                                            //   } ) );
                                        }
                                    }
                                    finally
                                    {
                                        try
                                        {
                                            System.IO.File.Delete( zip );
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    GuiDispatcher.GlobalDispatcher.AddAction( ( Action )( () =>
                       {
                           this.LastLogInfo.LastLogMessage = string.Format( "{0:HH:mm:ss}  {1}", ( object )message.Time, ( object )message.Message );
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

        void IPersistable.Load( SettingsStorage storage )
        {
        }

        void IPersistable.Save( SettingsStorage storage )
        {
        }

        void IDisposable.Dispose()
        {
        }

        

        public class LogInfo : NotifiableObject
        {
            private Visibility _lastErrorVisible = Visibility.Collapsed;
            private Visibility _lastWarnVisible = Visibility.Collapsed;
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
        }
    }
}
