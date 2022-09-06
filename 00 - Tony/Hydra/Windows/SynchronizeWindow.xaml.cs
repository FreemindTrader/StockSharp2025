using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class SynchronizeWindow : ThemedWindow, ILogReceiver, ILogSource, IDisposable, IComponentConnector
    {
        private CancellationTokenSource _token;
        
        public SynchronizeWindow()
        {
            InitializeComponent();
        }

        public IMarketDataDrive Drive { get; set; }

        protected override void OnClosing( CancelEventArgs e )
        {
            if ( _token != null )
            {
                if ( Sync.IsEnabled && new MessageBoxBuilder().Text( LocalizedStrings.Str2928 ).Warning().YesNo().Owner( this ).Show() == MessageBoxResult.Yes )
                    StopSync();
                e.Cancel = true;
            }
            base.OnClosing( e );
        }

        private void StopSync()
        {
            Sync.IsEnabled = false;
            _token.Cancel();
        }

        private void Sync_Click( object sender, RoutedEventArgs e )
        {
            if ( _token != null )
            {
                StopSync();
            }
            else
            {
                Sync.Content = LocalizedStrings.Str2890;
                _token = new CancellationTokenSource();
                Task.Factory
                    .StartNew(
                            () =>
                            {
                                IMarketDataDrive[ ] marketDataDriveArray1;
                                if ( Drive != null )
                                {
                                    marketDataDriveArray1 = new IMarketDataDrive[1] { Drive };
                                }
                                else
                                {
                                    marketDataDriveArray1 = ServicesRegistry.DriveCache.Drives
                                        .OfType<LocalMarketDataDrive>()
                                        .ToArray();
                                }

                                IMarketDataDrive[ ] marketDataDriveArray2 = marketDataDriveArray1;
                                SecurityLookupMessage allCriteriaMessage = Messages.Extensions.LookupAllCriteriaMessage;
                                ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
                                IExchangeInfoProvider exchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider;
                                foreach ( IMarketDataDrive marketDataDrive in marketDataDriveArray2 )
                                {
                                }
                            },
                        _token.Token )
                    .ContinueWithExceptionHandling(
                           this,
                            res =>
                            {
                                Sync.Content = LocalizedStrings.Str2932;
                                Sync.IsEnabled = true;
                                Progress.Value = 0.0;
                                _token = null;
                            } );
            }
        }

        void IDisposable.Dispose()
        {
        }

        Guid ILogSource.Id { get; } = Guid.NewGuid();

        ILogSource ILogSource.Parent { get; set; }

        public event Action<ILogSource> ParentRemoved
        {
            add
            {
            }
            remove
            {
            }
        }

        LogLevels ILogSource.LogLevel { get; set; }

        DateTimeOffset ILogSource.CurrentTime
        {
            get
            {
                return TimeHelper.NowWithOffset;
            }
        }

        bool ILogSource.IsRoot
        {
            get
            {
                return false;
            }
        }

        event Action<LogMessage> ILogSource.Log
        {
            add
            {
            }
            remove
            {
            }
        }

        void ILogReceiver.AddLog( LogMessage message )
        {
            Logs.Messages.Add( message );
        }        
    }
}
