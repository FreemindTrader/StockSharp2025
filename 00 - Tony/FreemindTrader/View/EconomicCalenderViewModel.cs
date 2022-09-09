using DevExpress.Mvvm;
using Ecng.ComponentModel;
using fx.Algorithm;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using HtmlAgilityPack;
using StockSharp.AlgoEx;
using StockSharp.BusinessEntities;
using StockSharp.Xaml;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using ViewModelBase = DevExpress.Mvvm.ViewModelBase;

#pragma warning disable 168


namespace FreemindTrader
{
    public class EconomicCalenderViewModel : ViewModelBase
    {
        private ThreadSafeDictionary<FxNewsEvent, string> _sentNewsEvents = new ThreadSafeDictionary<FxNewsEvent, string>();
        private string _xmlAddress = "https://sslecal2.forexprostools.com/";
        private ObservableCollectionEx<FxNewsEvent> _ecoCalendarItemSource;

        private string _security;
        private PooledList<FxNewsEvent> _weeklyNewEvents = new PooledList<FxNewsEvent>();
        private DateTime _lastDownloadTime = DateTime.MinValue;
        private Security _monitoringSymbol;
        FxEconomicCalendarBindingList _ecoCalendarBindingList;
        private readonly DispatcherTimer _mintimer;

        private WorkFlowStatus _downloadStatus = WorkFlowStatus.NotStarted;

        public EconomicCalenderViewModel()
        {
            Messenger.Default.Register<SelectSecurityMessage>( this, x => OnSelectSecurityMessage( x ) );
            //Messenger.Default.Register< EconomicCalenderDataSourceUpdateMessage >( this, x => OnEconomicCalenderDataSourceUpdateMessage( x ) );

            OnLoadedCommand = new DevExpress.Mvvm.DelegateCommand( OnLoaded );

            _mintimer = new DispatcherTimer();
        }

        //private void OnEconomicCalenderDataSourceUpdateMessage( EconomicCalenderDataSourceUpdateMessage x )
        //{
        //    _security = x.Symbol;
        //    _monitoringSymbol = new fxSymbol( _security );

        //    BindSymbolsNewsToList( _monitoringSymbol );
        //}

        public DevExpress.Mvvm.DelegateCommand OnLoadedCommand
        {
            get;
            private set;
        }

        public void OnLoaded()
        {
            if ( _downloadStatus == WorkFlowStatus.NotStarted || _downloadStatus == WorkFlowStatus.ErrorInWork )
            {
                Task detectTask = new Task(
                                            () =>
                                            {
                                                DownloadWeeklyNews();

                                            }, GeneralHelper.GlobalExitToken()
                                        );

                detectTask.Start();
            }

        }

        private void OnSelectSecurityMessage( SelectSecurityMessage x )
        {
            _security = x.Symbol.Code;

            _monitoringSymbol = x.Symbol;

            BindSymbolsNewsToList( _monitoringSymbol );
        }

        private void BindSymbolsNewsToList( Security monitoringSymbol )
        {
            PooledList<FxNewsEvent> temp = new PooledList<FxNewsEvent>();

            if ( _weeklyNewEvents.Count == 0 )
            {
                DownloadWeeklyNews();
            }

            if ( _weeklyNewEvents.Count > 0 )
            {
                DateTime currentTime = DateTime.UtcNow;

                foreach ( var fxNewsEvent in _weeklyNewEvents )
                {
                    if ( monitoringSymbol.IsRelatedNews( fxNewsEvent.Country ) )
                    {
                        TimeSpan difference = fxNewsEvent.EventTimeUTC - currentTime;

                        string differstr = string.Empty;

                        if ( difference.TotalMinutes < -5 )
                        {
                            continue;
                        }
                        else if ( difference.TotalMinutes < 0 )
                        {
                            differstr = string.Format( "{0} min passed", difference.Minutes );
                        }
                        else if ( difference.Days == 1 )
                        {
                            differstr = string.Format( "{0} day {1} hr {2} min", difference.Days, difference.Hours, difference.Minutes );
                        }
                        else if ( difference.Days > 1 )
                        {
                            differstr = string.Format( "{0} days {1} hr {2} min", difference.Days, difference.Hours, difference.Minutes );
                        }
                        else if ( difference.Days == 0 )
                        {
                            differstr = string.Format( "{0} hr {1} min", difference.Hours, difference.Minutes );
                        }


                        if ( fxNewsEvent.Impact == EventImpact.High )
                        {
                            if ( difference.TotalMinutes > 0 && difference.TotalMinutes <= 15 )
                            {
                                _sentNewsEvents[fxNewsEvent] = differstr;

                                Messenger.Default.Send( new ImportantNewsComingMessage( fxNewsEvent ) );

                            }
                        }

                        fxNewsEvent.CountDown = differstr;

                        temp.Add( fxNewsEvent );
                    }

                }
            }

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( monitoringSymbol );
            if ( aa == null )
                return;

            _ecoCalendarBindingList = aa.EconomicCalenderBindingList;

            _ecoCalendarBindingList.AddNews( temp );

            RaisePropertyChanged( nameof( EconomicCalenderItemSource ) );

            StartNewsTimeCountDown();
        }



        public ObservableCollectionEx<FxNewsEvent> EconomicCalenderItemSource
        {

            get
            {
                if ( _security != null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );
                    if ( aa == null )
                        return null;

                    _ecoCalendarItemSource = aa.EconomicCalenderItemSource;

                    return _ecoCalendarItemSource;
                }

                return null;
            }
        }

        public void StartNewsTimeCountDown()
        {
            DateTime nowTime = DateTime.UtcNow;

            if ( nowTime.Second % 60 != 0 )
            {
                DateTime nextMinute = new DateTime( nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute, 0 ).AddMinutes( 1 );

                TimeSpan diffTimeSpan = nextMinute - nowTime;

                if ( diffTimeSpan.TotalMilliseconds > 0 )
                {
                    _mintimer.Interval = diffTimeSpan;
                }
                else
                {
                    _mintimer.Interval = TimeSpan.FromMinutes( 1 );
                }


                _mintimer.Tick += _mintimer_Initialization;
            }

            _mintimer.Start();

        }

        private void _mintimer_Initialization( object sender, EventArgs e )
        {

            _mintimer.Stop();
            _mintimer.Tick -= _mintimer_Initialization;

            _mintimer.Interval = TimeSpan.FromMinutes( 1 );
            _mintimer.Tick += _mintimer_Tick;

            _mintimer.Start();

            _mintimer_Tick( sender, e );
        }

        private void _mintimer_Tick( object sender, EventArgs e )
        {
            _ecoCalendarBindingList.UpdateCountDown( _monitoringSymbol );
        }

        private bool _isUpdating = false;

        public void DownloadWeeklyNews()
        {
            _downloadStatus = WorkFlowStatus.StartWork;

            if ( _isUpdating == true ) return;

            var setting = BaseApplication.ProxySettings;

            _isUpdating = true;
            var events = new PooledList<FxNewsEvent>();

            try
            {
                WebClient wc = new WebClient();

                if ( setting.UseProxy )
                {
                    var proxyAddress = "http://" + setting.Address.ToString();
                    var uri = new Uri( proxyAddress );
                    var wp = new WebProxy( uri );

                    wc.Proxy = wp;
                }

                var xmlFile = wc.DownloadString( _xmlAddress );
                var doc = new HtmlDocument();
                doc.LoadHtml( xmlFile );

                var eventTables = doc.DocumentNode.SelectNodes( "//table[@id='ecEventsTable']//tr" );

                foreach ( HtmlNode row in eventTables )
                {
                    if ( row.Id.Contains( "eventRowId" ) )
                    {
                        var eventTimeStamp = row.Attributes["event_timestamp"]?.Value;

                        if ( eventTimeStamp != null )
                        {
                            Debug.WriteLine( eventTimeStamp );

                            var economyHtml = row.SelectSingleNode( "td[@class='flagCur']" )?.InnerText;

                            string economy = Regex.Replace( economyHtml, @"<[^>]+>|&nbsp;", string.Empty ).Trim();

                            var impact = row.SelectSingleNode( "td[@class='sentiment']" )?.Attributes["title"]?.Value;
                            var name = row.SelectSingleNode( "td[@class='left event']" )?.InnerText;
                            var actual = row.SelectSingleNode( "td[contains(@class, 'act')]" )?.InnerText;
                            var forecastHtml = row.SelectSingleNode( "td[@class='fore']" )?.InnerText;
                            string forecast = Regex.Replace( forecastHtml, @"<[^>]+>|&nbsp;", string.Empty ).Trim();
                            var previousHtml = row.SelectSingleNode( "td[contains(@id, 'eventPrevious')]" )?.InnerText;
                            string previous = Regex.Replace( previousHtml, @"<[^>]+>|&nbsp;", string.Empty ).Trim();

                            EventImpact impactValue = EventImpact.Unknown;


                            if ( !string.IsNullOrEmpty( impact ) )
                            {
                                if ( impact.Contains( "Low" ) )
                                {
                                    impactValue = EventImpact.Low;
                                }
                                else if ( impact.Contains( "Moderate" ) )
                                {
                                    impactValue = EventImpact.Medium;
                                }
                                else if ( impact.Contains( "High" ) )
                                {
                                    impactValue = EventImpact.High;
                                }
                            }


                            DateTimeFormatInfo info = new DateTimeFormatInfo();                            // Perform custom date time format and fix (month-day-year).
                            info.LongTimePattern = "MM-dd-yyyy";
                            DateTime dateTimeValue = DateTime.Parse( eventTimeStamp, info );

                            var newsEvent = new FxNewsEvent( name,
                                                            economy,
                                                            string.IsNullOrEmpty( eventTimeStamp ) == false,
                                                            dateTimeValue,
string.Empty,
                                                            impactValue,
                                                            forecast,
                                                            previous
                                                       );

                            if ( !events.Exists( x => x == newsEvent ) )
                            {
                                events.Add( newsEvent );
                            }



                        }


                    }
                }//END TR
            }
            catch
            {
                _downloadStatus = WorkFlowStatus.ErrorInWork;
            }


            lock ( this )
            {
                _weeklyNewEvents.Clear();
                _weeklyNewEvents.AddRange( events );
            }

            _lastDownloadTime = DateTime.UtcNow;
            _isUpdating = false;
            _downloadStatus = WorkFlowStatus.DoneWork;

        }
    }
}
