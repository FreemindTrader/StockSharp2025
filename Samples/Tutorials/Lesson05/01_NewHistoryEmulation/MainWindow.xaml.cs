#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: SampleHistoryTesting.SampleHistoryTestingPublic
File: MainWindow.xaml.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace _01_NewHistoryEmulationGithub
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Collections.Generic;

    using Ecng.Xaml;
    using Ecng.Common;
    using Ecng.Collections;

    using StockSharp.Algo;
    using StockSharp.Algo.Candles;
    using StockSharp.Algo.Commissions;
    using StockSharp.Algo.Storages;
    using StockSharp.Algo.Testing;
    using StockSharp.BusinessEntities;
    using StockSharp.Logging;
    using StockSharp.Messages;
    using StockSharp.Charting;
    using StockSharp.Xaml.Charting;
    using StockSharp.Localization;
    using StockSharp.Configuration;

    public partial class MainWindow
    {
        private string _pathHistory = @"U:\ForexData";

        private HistoryEmulationConnector _connector;

        private DateTime _startEmulationTime;

        private readonly InMemoryExchangeInfoProvider _exchangeInfoProvider = new();

        public MainWindow()
        {
            InitializeComponent();

            StartBtn.IsEnabled = true;


            SecId.Text = "SPX500@FXCM";

            From.EditValue = new DateTime( 2022, 10, 1 );
            To.EditValue = new DateTime( 2022, 10, 5 );

            TimeFrame.SelectedIndex = 0;
        }

        private void StartBtnClick( object sender, RoutedEventArgs e )
        {
            var id = SecId.Text.ToSecurityId();

            var timeFrame = TimeSpan.FromMinutes( TimeFrame.SelectedIndex == 0 ? 1 : 5 );

            var secCode = id.SecurityCode;
            var board = _exchangeInfoProvider.GetOrCreateBoard( id.BoardCode );

            // create test security
            var security = new Security
            {
                Id = SecId.Text, // sec id has the same name as folder with historical data
                Code = secCode,
                Board = board,
            };




            // storage to historical data
            var storageRegistry = new StorageRegistry
            {
                DefaultDrive = new LocalMarketDataDrive( _pathHistory )
            };

            var startTime = ( ( DateTime )From.EditValue ).UtcKind();
            var stopTime = ( ( DateTime )To.EditValue ).UtcKind();

            // (ru only) ОЛ необходимо загружать с 18.45 пред дня, чтобы стаканы строились правильно

            // set ProgressBar bounds
            CandlesProgress.Value = 0;
            CandlesProgress.Maximum = 100;



            var logManager = new LogManager();
            var fileLogListener = new FileLogListener( "sample.log" );
            logManager.Listeners.Add( fileLogListener );
            //logManager.Listeners.Add(new DebugLogListener());	// for track logs in output window in Vusial Studio (poor performance).

            var secId = security.ToSecurityId();

            SetIsEnabled( false, false, false );

            //foreach (var set in settings)



            var title = "Candle";

            ClearChart( CandlesChart, CandlesEquity, CandlesPosition );

            var level1Info = new Level1ChangeMessage
            {
                SecurityId = secId,
                ServerTime = startTime,
            }
            .TryAdd( Level1Fields.PriceStep, 0.01m )
            .TryAdd( Level1Fields.StepPrice, 0.01m )
            .TryAdd( Level1Fields.MinPrice, 0.01m )
            .TryAdd( Level1Fields.MaxPrice, 1000000m )
            .TryAdd( Level1Fields.MarginBuy, 10000m )
            .TryAdd( Level1Fields.MarginSell, 10000m );

            // test portfolio
            var portfolio = Portfolio.CreateSimulator();

            var secProvider = ( ISecurityProvider )new CollectionSecurityProvider( new[ ] { security } );

            // create backtesting connector
            var simConnector = new HistoryEmulationConnector(
                                                                        secProvider,
                                                                        new[ ] { portfolio }
                                                                   )
            {
                EmulationAdapter =
                {
                        Settings =
                        {
							// match order if historical price touched our limit order price. 
							// It is terned off, and price should go through limit order price level
							// (more "severe" test mode)
							MatchOnTouch = false,
                        }
                },



                HistoryMessageAdapter =
                {
                        StorageRegistry = storageRegistry,

                        OrderLogMarketDepthBuilders =
                        {
                            {
                                secId,
                                new OrderLogMarketDepthBuilder(secId)
                            }
                        }
                },

                // set market time freq as time frame
                MarketTimeChangedInterval = timeFrame,
            };

            ( ( ILogSource )simConnector ).LogLevel = DebugLogCheckBox.IsChecked == true ? LogLevels.Debug : LogLevels.Info;

            logManager.Sources.Add( simConnector );

            var series = new CandleSeries( typeof( TimeFrameCandle ), security, timeFrame )
            {
                BuildCandlesMode = MarketDataBuildModes.Load,
                BuildCandlesFrom2 = null,
            };

            // create strategy based on 80 5-min и 10 5-min
            var strategy = new SmaStrategy( series )
            {
                LongSma = { Length = 80 },
                ShortSma = { Length = 10 },
                Volume = 1,
                Portfolio = portfolio,
                Security = security,
                Connector = simConnector,
                LogLevel = DebugLogCheckBox.IsChecked == true ? LogLevels.Debug : LogLevels.Info,

                // by default interval is 1 min,
                // it is excessively for time range with several months
                UnrealizedPnLInterval = ( ( stopTime - startTime ).Ticks / 1000 ).To<TimeSpan>()
            };

            //var chart = ;

            CandlesChart.IsInteracted = false;
            strategy.SetChart( CandlesChart );

            logManager.Sources.Add( strategy );

            //if ( emulationInfo.CustomHistoryAdapter != null )
            //{
            //    simConnector.Adapter.InnerAdapters.Remove( simConnector.MarketDataAdapter );

            //    var emu = simConnector.EmulationAdapter.Emulator;
            //    simConnector.Adapter.InnerAdapters.Add( new EmulationMessageAdapter( emulationInfo.CustomHistoryAdapter( simConnector.TransactionIdGenerator ), new InMemoryMessageChannel( new MessageByLocalTimeQueue(), "History out", err => err.LogError() ), true, emu.SecurityProvider, emu.PortfolioProvider, emu.ExchangeInfoProvider ) );
            //}

            // set history range
            simConnector.HistoryMessageAdapter.StartDate = startTime;
            simConnector.HistoryMessageAdapter.StopDate = stopTime;

            simConnector.SecurityReceived += ( subscr, s ) =>
            {
                if ( s != security )
                    return;

                // fill level1 values
                simConnector.EmulationAdapter.SendInMessage( level1Info );

                // ------------------------------------------------------------------------------------------
                // 
                //+++ Step 2: Received Security, now start Strategy
                // 
                // ------------------------------------------------------------------------------------------

                // start strategy before emulation started
                strategy.Start();

                // start historical data loading when connection established successfully and all data subscribed
                simConnector.Start();
            };

            // fill parameters panel
            CandlesParameterGrid.Parameters.Clear();
            CandlesParameterGrid.Parameters.AddRange( strategy.StatisticManager.Parameters );

            var equity = CandlesEquity;

            var pnlCurve = equity.CreateCurve( LocalizedStrings.PnL + " " + "SMA", Colors.Green, Colors.Red, ChartIndicatorDrawStyles.Area );
            var unrealizedPnLCurve = equity.CreateCurve( LocalizedStrings.PnLUnreal + " " + "SMA", Colors.Black, ChartIndicatorDrawStyles.Line );
            var commissionCurve = equity.CreateCurve( LocalizedStrings.Str159 + " " + "SMA", Colors.Red, ChartIndicatorDrawStyles.DashedLine );

            strategy.PnLChanged += () =>
            {
                var data = equity.CreateData();

                data
                    .Group( strategy.CurrentTime )
                        .Add( pnlCurve, strategy.PnL - ( strategy.Commission ?? 0 ) )
                        .Add( unrealizedPnLCurve, strategy.PnLManager.UnrealizedPnL ?? 0 )
                        .Add( commissionCurve, strategy.Commission ?? 0 );

                equity.Draw( data );
            };

            var posItems = CandlesPosition.CreateCurve( "SMA", Colors.DarkBlue, ChartIndicatorDrawStyles.Line );

            strategy.PositionChanged += () =>
            {
                var data = CandlesPosition.CreateData();

                data
                    .Group( strategy.CurrentTime )
                        .Add( posItems, strategy.Position );

                CandlesPosition.Draw( data );
            };

            simConnector.ProgressChanged += steps => this.GuiAsync( () => CandlesProgress.Value = steps );

            simConnector.StateChanged += () =>
            {
                if ( simConnector.State == ChannelStates.Stopped )
                {
                    strategy.Stop();

                    SetIsChartEnabled( CandlesChart, false );

                    if ( _connector.State == ChannelStates.Stopped )
                    {
                        logManager.Dispose();
                        //_connector.Clear();

                        SetIsEnabled( true, false, false );
                    }

                    this.GuiAsync( () =>
                    {
                        if ( simConnector.IsFinished )
                        {
                            CandlesProgress.Value = CandlesProgress.Maximum;
                            MessageBox.Show( this, LocalizedStrings.Str3024.Put( DateTime.Now - _startEmulationTime ), title );
                        }
                        else
                            MessageBox.Show( this, LocalizedStrings.cancelled, title );
                    } );
                }
                else if ( simConnector.State == ChannelStates.Started )
                {
                    if ( _connector.State == ChannelStates.Started )
                        SetIsEnabled( false, true, true );

                    SetIsChartEnabled( CandlesChart, true );
                }
                else if ( simConnector.State == ChannelStates.Suspended )
                {
                    if ( _connector.State == ChannelStates.Suspended )
                        SetIsEnabled( true, false, true );
                }
            };

            MarketDepth.UpdateFormat( security );

            simConnector.NewMessage += message =>
            {
                if ( message is QuoteChangeMessage quoteMsg )
                    MarketDepth.UpdateDepth( quoteMsg );
            };

            _connector =  simConnector;

            CandlesProgress.Value = 0;


            _startEmulationTime = DateTime.Now;

            _connector.Connect();

            // 1 cent commission for trade
            _connector.SendInMessage( new CommissionRuleMessage
            {
                Rule = new CommissionPerTradeRule { Value = 0.01m }
            } );            
        }
    }
}