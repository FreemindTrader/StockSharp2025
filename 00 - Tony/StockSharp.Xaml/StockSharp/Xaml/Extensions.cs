using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using Ecng.Backup;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace StockSharp.Xaml
{
    /// <summary>Extension class.</summary>
    public static class Extensions
    {
        /// <summary>Load settings.</summary>
        /// <param name="window">Window instance.</param>
        /// <param name="storage">Settings storage.</param>
        public static void LoadWindowSettings( this Window window, SettingsStorage storage )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            window.Top = ( double )storage.GetValue<double>( "Top", window.Top );
            window.Left = ( double )storage.GetValue<double>( "Left", window.Left );
            window.Width = ( double )storage.GetValue<double>( "Width", window.Width );
            window.Height = ( double )storage.GetValue<double>( "Height", window.Height );
            window.WindowState = ( WindowState )storage.GetValue<WindowState>( "WindowState", window.WindowState );
        }

        /// <summary>Save settings.</summary>
        /// <param name="window">Window instance.</param>
        /// <param name="storage">Settings storage.</param>
        public static void SaveWindowSettings( this Window window, SettingsStorage storage )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            storage.SetValue<double>( "Top", window.Top );
            storage.SetValue<double>( "Left", window.Left );
            storage.SetValue<double>( "Width", window.Width );
            storage.SetValue<double>( "Height", window.Height );
            storage.SetValue<WindowState>( "WindowState", window.WindowState );
        }

        /// <summary>Save DevExpress control settings.</summary>
        /// <param name="obj">Control.</param>
        /// <returns>Settings.</returns>
        public static string SaveDevExpressControl( this DependencyObject obj )
        {
            if ( obj == null )
                throw new ArgumentNullException( nameof( obj ) );

            using ( MemoryStream memoryStream = new MemoryStream() )
            {
                DXSerializer.Serialize( obj, ( Stream )memoryStream, "Designer", ( DXOptionsLayout )null );

                return StringHelper.UTF8( ( byte[ ] )Converter.To<byte[ ]>( ( object )memoryStream ) );
            }
        }

        /// <summary>Load DevExpress control settings.</summary>
        /// <param name="obj">Control.</param>
        /// <param name="settings">Settings.</param>
        public static void LoadDevExpressControl( this DependencyObject obj, string settings )
        {
            if ( obj == null )
                throw new ArgumentNullException( nameof( obj ) );
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );

            using ( MemoryStream memoryStream = new MemoryStream( Encoding.UTF8.GetBytes( settings ) ) )
                DXSerializer.Deserialize( obj, ( Stream )memoryStream, "Designer", ( DXOptionsLayout )null );
        }

        private sealed class SomeLambdaClass337
        {
            public string _string;
            public DependencyObject _depObject;

            internal void Method0298()
            {
                this._string.TryOpenLink( this._depObject );
            }
        }

        /// <summary>Try to open the specified link.</summary>
        /// <param name="url">Link.</param>
        /// <param name="owner">UI thread owner.</param>
        public static void TryOpenLink( this string url, DependencyObject owner )
        {
            Dispatcher dispatcher = GuiDispatcher.GlobalDispatcher.Dispatcher;

            if ( !dispatcher.CheckAccess() )
            {
                dispatcher.GuiAsync( () => url.TryOpenLink( owner ) );
            }
            else
            {
                if ( IOHelper.OpenLink( url, false ) )
                    return;
                url.CopyToClipboard<string>();
                int num = ( int )new MessageBoxBuilder().Error().Text( StringHelper.Put( LocalizedStrings.CannotOpenLink, url ) ).Owner( owner ).Show();
            }
        }

        internal static ICollection<Tuple<DateTime, Decimal>> AveragePriceByCount( this ICollection<Tuple<DateTime, Decimal>> timeToPriceCollection, int count )
        {
            if ( timeToPriceCollection == null )
            {
                throw new ArgumentNullException( "source" );
            }

            if ( count >= timeToPriceCollection.Count )
            {
                return timeToPriceCollection;
            }

            var tupleList = new List<Tuple<DateTime, Decimal>>();

            int combinedTotal = timeToPriceCollection.Count / count;
            Decimal tickSum = new Decimal();
            Decimal priceSum = new Decimal();
            int counter = 0;

            foreach ( var tuple in timeToPriceCollection )
            {
                tickSum += ( Decimal )tuple.Item1.Ticks;
                priceSum += tuple.Item2;
                ++counter;

                if ( counter >= combinedTotal )
                {
                    tupleList.Add( Tuple.Create<DateTime, Decimal>( new DateTime( ( long )( tickSum / ( Decimal )counter ) ), priceSum / ( Decimal )counter ) );
                    tickSum = new Decimal();
                    priceSum = new Decimal();
                    counter = 0;
                }
            }
            if ( counter > 0 )
                tupleList.Add( Tuple.Create<DateTime, Decimal>( new DateTime( ( long )( tickSum / ( Decimal )counter ) ), priceSum / ( Decimal )counter ) );
            return ( ICollection<Tuple<DateTime, Decimal>> )tupleList;
        }

        /// <summary>Save layout state.</summary>
        /// <param name="manager">Represents a container for dock and layout items.</param>
        /// <returns>Layout encoded as a string.</returns>
        public static string SaveLayout( this DockLayoutManager manager )
        {
            if ( manager == null )
                throw new ArgumentNullException( nameof( manager ) );

            using ( MemoryStream memoryStream = new MemoryStream() )
            {
                manager.SaveLayoutToStream( ( Stream )memoryStream );
                return StringHelper.UTF8( ( byte[ ] )Converter.To<byte[ ]>( ( object )memoryStream ) );
            }
        }

        /// <summary>Restore layout state.</summary>
        /// <param name="manager">Represents a container for dock and layout items.</param>
        /// <param name="layout">Layout encoded as a string.</param>
        public static void LoadLayout( this DockLayoutManager manager, string layout )
        {
            if ( manager == null )
                throw new ArgumentNullException( nameof( manager ) );

            if ( StringHelper.IsEmpty( layout ) )
                return;
            using ( MemoryStream memoryStream = new MemoryStream( StringHelper.UTF8( layout ) ) )
                manager.RestoreLayoutFromStream( ( Stream )memoryStream );
        }

        /// <summary>
        /// Get icon for <see cref="P:StockSharp.Algo.Strategies.Strategy.ProcessState" />.
        /// </summary>
        /// <param name="strategy">Strategy.</param>
        /// <returns>Icon name.</returns>
        /// 
        public static string GetStrategyProcessStateIconName( this Strategy strategy )
        {
            if ( strategy == null )
                throw new ArgumentNullException( nameof( strategy ) );


            ProcessStates processState = strategy.ProcessState;
            string result;

            if ( processState != ProcessStates.Stopped )
            {
                if ( processState - ProcessStates.Stopping > 1 )
                {
                    throw new ArgumentOutOfRangeException( strategy.ProcessState.To<string>() );
                }

                result = "Start";
            }
            else
            {
                result = "Stop";
            }

            return result;
        }

        /// <summary>Generate price format for the specified security.</summary>
        /// <param name="security">Security.</param>
        /// <returns>Price format.</returns>
        public static string GetPriceTextFormat( this Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );

            if ( !security.PriceStep.HasValue && !security.Decimals.HasValue )
            {
                return "0.00";
            }

            Decimal? priceStep = security.PriceStep;

            string str = new string( '0', priceStep.HasValue ? MathHelper.GetCachedDecimals( priceStep.GetValueOrDefault() ) : security.Decimals.Value );
            return "0" + ( str.Length == 0 ? string.Empty : "." + str );
        }

        /// <summary>Generate volume format for the specified security.</summary>
        /// <param name="security">Security.</param>
        /// <returns>Volume format.</returns>
        public static string GetVolumeTextFormat( this Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            if ( !security.VolumeStep.HasValue )
                return "0";

            string str = new string( '0', MathHelper.GetCachedDecimals( security.VolumeStep.Value ) );
            return "0" + ( str.Length == 0 ? string.Empty : "." + str );
        }

        /// <summary>
        /// Configure connection using <see cref="T:StockSharp.Xaml.ConnectorWindow" />.
        /// </summary>
        /// <param name="connector">The connection.</param>
        /// <param name="owner">UI thread owner.</param>
        /// <returns>
        /// <see langword="true" /> if the specified connection was configured, otherwise, <see langword="false" />.</returns>
        public static bool Configure( this Connector connector, Window owner )
        {
            if ( connector == null )
                throw new ArgumentNullException( nameof( connector ) );
            return connector.Adapter.Configure( owner );
        }

        /// <summary>
        /// Configure connection using <see cref="T:StockSharp.Xaml.ConnectorWindow" />.
        /// </summary>
        /// <param name="adapter">The connection.</param>
        /// <param name="owner">UI thread owner.</param>
        /// <returns>
        /// <see langword="true" /> if the specified connection was configured, otherwise, <see langword="false" />.</returns>
        public static bool Configure( this BasketMessageAdapter adapter, Window owner )
        {
            bool autoConnect = false;
            SettingsStorage windowSettings = ( SettingsStorage )null;
            return adapter.Configure( owner, ref autoConnect, ref windowSettings );
        }

        /// <summary>Share file.</summary>
        /// <param name="owner">Owner.</param>
        /// <param name="isNew">Is file newly or updated.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="getBody">File body.</param>
        /// <param name="token">Cancelation token.</param>
        public static async Task ShareAsync( this DependencyObject owner, bool isNew, string fileName, Func<Stream> getBody, CancellationToken token )
        {
            IBackupService ibackupService;
            BackupEntry backupEntry;
            ILogReceiver ilogReceiver;
            try
            {
                if ( getBody == null )
                    throw new ArgumentNullException( "Function is Empty" );

                ibackupService = ( IBackupService )ConfigManager.TryGetService<IBackupService>();

                if ( ibackupService != null )
                {
                    BackupEntry backup = new BackupEntry();
                    backup.Name = ( fileName );
                    backupEntry = backup;
                    ilogReceiver = LogManager.Instance?.Application;

                    await ibackupService.UploadAsync( backupEntry, getBody(), p => { }, token );


                    LoggingHelper.AddInfoLog( ilogReceiver, "File Name = {0}, backupType = {1}", new object[2]
                    {
                        fileName,
                        ibackupService.GetType().Name
                    } );


                    if ( isNew )
                    {
                        if ( ibackupService.CanPublish )
                        {
                            var result = await ibackupService.PublishAsync( backupEntry, token );

                            LoggingHelper.AddInfoLog( ilogReceiver, "File Name = {0}", fileName );

                            if ( !StringHelper.IsEmpty( result ) )
                            {
                                result.CopyToClipboard<string>();
                                result.TryOpenLink( owner );
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                ibackupService = null;
                backupEntry = null;
                ilogReceiver = null;
                return;
            }

            ibackupService = null;
            backupEntry = null;
            ilogReceiver = null;

        }

        /// <summary>
        /// Configure connection using <see cref="T:StockSharp.Xaml.ConnectorWindow" />.
        /// </summary>
        /// <param name="adapter">The connection.</param>
        /// <param name="owner">UI thread owner.</param>
        /// <param name="autoConnect">Auto connect.</param>
        /// <param name="windowSettings">
        /// <see cref="T:StockSharp.Xaml.ConnectorWindow" /> settings.</param>
        /// <returns>
        /// <see langword="true" /> if the specified connection was configured, otherwise, <see langword="false" />.</returns>
        public static bool Configure( this BasketMessageAdapter adapter, Window owner, ref bool autoConnect, ref SettingsStorage windowSettings )
        {
            if ( adapter == null )
            {
                throw new ArgumentNullException( nameof( adapter ) );
            }

            if ( owner == null )
            {
                throw new ArgumentNullException( nameof( owner ) );
            }

            ConnectorWindow connectorWindow = new ConnectorWindow();
            if ( windowSettings != null )
            {
                connectorWindow.Load( windowSettings );
            }

            using ( IEnumerator<IMessageAdapter> enumerator = Extensions.GetAdapterProviders( adapter ).PossibleAdapters.GetEnumerator() )
            {
                while ( ( ( IEnumerator )enumerator ).MoveNext() )
                {
                    IMessageAdapter current = enumerator.Current;
                    Extensions.AddConnectorInfo( connectorWindow, current );
                }
            }
            connectorWindow.Adapter = ( BasketMessageAdapter )adapter.Clone();
            connectorWindow.AutoConnect = autoConnect;
            if ( !XamlHelper.ShowModal( ( Window )connectorWindow, owner ) )
            {
                windowSettings = PersistableHelper.Save( ( IPersistable )connectorWindow );
                return false;
            }
                  ( ( BaseLogSource )adapter ).Load( PersistableHelper.Save( ( IPersistable )connectorWindow.Adapter ) );
            autoConnect = connectorWindow.AutoConnect;
            windowSettings = PersistableHelper.Save( ( IPersistable )connectorWindow );
            return true;
        }

        private static void AddConnectorInfo( ConnectorWindow wnd, IMessageAdapter adapter )
        {
            if ( wnd == null )
                throw new ArgumentNullException( nameof( wnd ) );
            if ( adapter == null )
                throw new ArgumentNullException( nameof( adapter ) );

            wnd.ConnectorsInfo.Add( new ConnectorInfo( adapter ) );
        }

        private static IMessageAdapterProvider GetAdapterProviders( BasketMessageAdapter adapter )
        {
            return ServicesRegistry.TryAdapterProvider ?? ( IMessageAdapterProvider )new InMemoryMessageAdapterProvider( ( IEnumerable<IMessageAdapter> )adapter.InnerAdapters );
        }

        /// <summary>
        /// Initialize <paramref name="window" />.
        /// </summary>
        /// <param name="window">The window for the order creating.</param>
        /// <param name="connector">The class to create connections to trading systems.</param>
        /// <returns>The window for the order creating.</returns>
        public static OrderWindow Init( this OrderWindow window, Connector connector )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );

            OrderWindow orderWindow = window;

            if ( connector == null )
                throw new ArgumentNullException( nameof( connector ) );
            orderWindow.SecurityProvider = ( ISecurityProvider )connector;
            window.MarketDataProvider = ( IMarketDataProvider )connector;
            window.PortfolioAdapterProvider = connector.Adapter.PortfolioAdapterProvider;
            window.AdapterProvider = Extensions.GetAdapterProviders( connector.Adapter );
            window.Portfolios = new PortfolioDataSource( ( IPortfolioProvider )connector );
            return window;
        }



        /// <summary>Исключить инструмент "Все инструменты".</summary>
        /// <param name="picker">Визуальный компонент для поиска и выбора инструмента.</param>
        public static void ExcludeAllSecurity( this SecurityPicker picker )
        {
            if ( picker == null )
                throw new ArgumentNullException( nameof( picker ) );
            picker.ExcludeSecurities.Add( TraderHelper.AllSecurity );
        }

        /// <summary>
        /// Fill <see cref="T:StockSharp.Xaml.CredentialsWindow" /> with credentials information.
        /// </summary>
        /// <param name="window">
        /// <see cref="T:StockSharp.Xaml.CredentialsWindow" />.</param>
        /// <param name="credentials">
        /// <see cref="T:Ecng.ComponentModel.ServerCredentials" />.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.CredentialsWindow" />.</returns>
        public static CredentialsWindow FillWindow( this CredentialsWindow window, ServerCredentials credentials )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            if ( credentials == null )
                return window;
            window.Email = credentials.Email;
            window.Password = credentials.Password;
            window.AutoLogon = credentials.AutoLogon;
            return window;
        }

        /// <summary>
        /// Get filled <see cref="T:Ecng.ComponentModel.ServerCredentials" />.
        /// </summary>
        /// <param name="window">
        /// <see cref="T:StockSharp.Xaml.CredentialsWindow" />.</param>
        /// <returns>
        /// <see cref="T:Ecng.ComponentModel.ServerCredentials" />.</returns>
        public static ServerCredentials GetCredentials( this CredentialsWindow window )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            ServerCredentials serverCredentials = new ServerCredentials();
            serverCredentials.Email = ( window.Email );
            serverCredentials.Password = ( window.Password );
            serverCredentials.AutoLogon = ( window.AutoLogon );
            return serverCredentials;
        }
    }
}
