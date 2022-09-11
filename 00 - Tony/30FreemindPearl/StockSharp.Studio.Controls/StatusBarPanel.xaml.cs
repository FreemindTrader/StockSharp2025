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
        public static readonly DependencyProperty LastLogInfoProperty = DependencyProperty.Register( nameof( LastLogInfo ), typeof( LogInfo ), typeof( StatusBarPanel ), new PropertyMetadata( new LogInfo() ) );
        private readonly SubscriptionManager _subscriptionManager;
        private int _uniqueBugReports;
        private const int _maxUniqueBugReports = 10;
        
        public string Status
        {
            get
            {
                return ( string )GetValue( StatusProperty );
            }
            set
            {
                SetValue( StatusProperty, value );
            }
        }

        public LogInfo LastLogInfo
        {
            get
            {
                return ( LogInfo )GetValue( LastLogInfoProperty );
            }
            set
            {
                SetValue( LastLogInfoProperty, value );
            }
        }

        public StatusBarPanel()
        {
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            if ( this.IsDesignMode() )
                return;
            CommandService.Register<EntityCommand<News>>( this, true, cmd => AdvertisePanel.AddNews( cmd.Entities.Where( n => n.IsStockSharp() ) ), null );
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.News, null ) );
        }

        public override void Dispose( CloseReason reason )
        {
            CommandService.UnRegister<EntityCommand<News>>( this );
            _subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public void ResetLogsImages()
        {
            LastLogInfo.LastWarnVisible = Visibility.Collapsed;
            LastLogInfo.LastErrorVisible = Visibility.Collapsed;
            LastLogInfo.LastLogMessage = string.Empty;
        }

        private void CopyToBufferCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !LastLogInfo.LastLogMessage.IsEmpty();
        }

        private void CopyToBufferCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            LastLogInfo.LastLogMessage.CopyToClipboard();
        }

        private void LogDirectoryCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void LogDirectoryCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            ResetLogsImages();
            string logsDir = BaseUserConfig<StudioUserConfig>.Instance.LogConfig.LogsDir;
            if ( !Directory.Exists( logsDir ) )
                return;
            logsDir.TryOpenLink( this );
        }

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            foreach ( LogMessage message1 in messages )
            {
                LogMessage message = message1;
                if ( ( message.Level == LogLevels.Warning || message.Level == LogLevels.Error ) && ( !( message.Source is HistoryEmulationConnector ) && !( message.Source is HistoryMessageAdapter ) ) && ( !( message.Source is EmulationMessageAdapter ) && !( message.Source is MarketEmulator ) ) )
                {
                    if ( message.Level == LogLevels.Error && _uniqueBugReports < 10 )
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
                                ProductBugReport bugReport = AsyncContext.Run( () =>
                                   {
                                       IProductBugReportService bugReportService = reportSvc;
                                       ProductBugReport entity = new ProductBugReport();
                                       entity.Product = new Product()
                                       {
                                           Id = Helper.ProductId
                                       };
                                       entity.Version = Paths.InstalledVersion;
                                       entity.SystemInfo = Extensions.GetSystemInfo();
                                       entity.Message = new Web.DomainModel.Message()
                                       {
                                           Body = message.Message
                                       };
                                       CancellationToken cancellationToken = new CancellationToken();
                                       return bugReportService.TryProposeAsync( entity, cancellationToken );
                                   } );
                                if ( bugReport != null )
                                {
                                    ++_uniqueBugReports;
                                    string zip = Extensions.PrepareLogsFile( TimeSpan.FromDays( 1.0 ), ex => { } );
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
                    GuiDispatcher.GlobalDispatcher.AddAction( () =>
                       {
                           LastLogInfo.LastLogMessage = string.Format( "{0:HH:mm:ss}  {1}", message.Time, message.Message );
                           if ( message.Level == LogLevels.Warning )
                           {
                               LastLogInfo.LastWarnVisible = Visibility.Visible;
                               LastLogInfo.LastErrorVisible = Visibility.Collapsed;
                           }
                           else
                           {
                               LastLogInfo.LastWarnVisible = Visibility.Collapsed;
                               LastLogInfo.LastErrorVisible = Visibility.Visible;
                           }
                       } );
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
                    return _lastErrorVisible;
                }
                set
                {
                    if ( _lastErrorVisible == value )
                        return;
                    _lastErrorVisible = value;
                    NotifyChanged( nameof( LastErrorVisible ) );
                }
            }

            public Visibility LastWarnVisible
            {
                get
                {
                    return _lastWarnVisible;
                }
                set
                {
                    if ( _lastWarnVisible == value )
                        return;
                    _lastWarnVisible = value;
                    NotifyChanged( nameof( LastWarnVisible ) );
                }
            }

            public string LastLogMessage
            {
                get
                {
                    return _lastLogMessage;
                }
                set
                {
                    if ( _lastLogMessage == value )
                        return;
                    _lastLogMessage = value;
                    NotifyChanged( nameof( LastLogMessage ) );
                }
            }
        }
    }
}
