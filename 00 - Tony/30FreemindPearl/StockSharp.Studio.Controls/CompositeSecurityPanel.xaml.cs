// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CompositeSecurityPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    public partial class CompositeSecurityPanel : BaseStudioControl, IComponentConnector
    {
        public static readonly RoutedCommand SaveSecurityCommand = new RoutedCommand();
        public static readonly RoutedCommand DrawSecurityCommand = new RoutedCommand();
        public static readonly DependencyProperty MarketDataDriveProperty = DependencyProperty.Register(
            nameof( MarketDataDrive ),
            typeof( IMarketDataDrive ),
            typeof( CompositeSecurityPanel ),
            new PropertyMetadata( new PropertyChangedCallback( OnMarketDataDrivePropertyChanged ) ) );
        public static readonly DependencyProperty DateFromProperty = DependencyProperty.Register(
            nameof( DateFrom ),
            typeof( DateTime ),
            typeof( CompositeSecurityPanel ),
            new PropertyMetadata( new PropertyChangedCallback( OnPropertyChanged ) ) );

        public static readonly DependencyProperty DateToProperty = DependencyProperty.Register(
            nameof( DateTo ),
            typeof( DateTime ),
            typeof( CompositeSecurityPanel ),
            new PropertyMetadata( new PropertyChangedCallback( OnPropertyChanged ) ) );

        public static readonly DependencyProperty SecurityCodeProperty = DependencyProperty.Register( nameof( SecurityCode ), typeof( string ), typeof( CompositeSecurityPanel ), new PropertyMetadata( ( object )string.Empty ) );
        public static readonly DependencyProperty BoardProperty = DependencyProperty.Register( nameof( Board ), typeof( ExchangeBoard ), typeof( CompositeSecurityPanel ), new PropertyMetadata( ( object )ExchangeBoard.Associated ) );
        public static readonly DependencyProperty CanEditProperty = DependencyProperty.Register( nameof( CanEdit ), typeof( bool ), typeof( CompositeSecurityPanel ), new PropertyMetadata( ( object )true ) );
        public static readonly DependencyProperty IsStartedProperty = DependencyProperty.Register(
            nameof( IsStarted ),
            typeof( bool ),
            typeof( CompositeSecurityPanel ),
            new PropertyMetadata( false, new PropertyChangedCallback( IsStartedPropertyChanged ) ) );

        public static readonly DependencyProperty DrawSourcesProperty = DependencyProperty.Register( nameof( DrawSources ), typeof( bool ), typeof( CompositeSecurityPanel ), new PropertyMetadata( ( object )true ) );
        private readonly System.Drawing.Color[ ] _colors = new System.Drawing.Color[10] { System.Drawing.Color.Black, System.Drawing.Color.Blue, System.Drawing.Color.BlueViolet, System.Drawing.Color.CadetBlue, System.Drawing.Color.Chocolate, System.Drawing.Color.DarkBlue, System.Drawing.Color.CornflowerBlue, System.Drawing.Color.ForestGreen, System.Drawing.Color.Indigo, System.Drawing.Color.Turquoise };
        private readonly SyncObject _syncRoot = new SyncObject();
        private readonly SecurityIdGenerator _idGenerator = new SecurityIdGenerator();
        private readonly SynchronizedSet<IChartElement> _changedElements = new SynchronizedSet<IChartElement>();
        private readonly SynchronizedSet<IChartElement> _skipElements = new SynchronizedSet<IChartElement>();
        private readonly Dictionary<Security, IChartIndicatorElement> _sourceElements = new Dictionary<Security, IChartIndicatorElement>();
        private readonly Dictionary<IChartIndicatorElement, IIndicator> _indicators = new Dictionary<IChartIndicatorElement, IIndicator>();
        private readonly ICandleManager _candleManager;
        private readonly ResettableTimer _timer;
        private readonly ResettableTimer _drawTimer;
        private readonly LayoutManager _layoutManager;
        private bool _isLoaded;
        private bool _canSave;
        private bool _isStarted;
        private int _candlesCount;
        private IChartArea _mainArea;
        private IChartCandleElement _candleElement;
        private Security _security;


        private static void OnMarketDataDrivePropertyChanged(
          DependencyObject sender,
          DependencyPropertyChangedEventArgs args )
        {
        }

        public IMarketDataDrive MarketDataDrive
        {
            get
            {
                return ( IMarketDataDrive )this.GetValue( CompositeSecurityPanel.MarketDataDriveProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.MarketDataDriveProperty, ( object )value );
            }
        }

        public DateTime DateFrom
        {
            get
            {
                return ( DateTime )this.GetValue( CompositeSecurityPanel.DateFromProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.DateFromProperty, ( object )value );
            }
        }

        public DateTime DateTo
        {
            get
            {
                return ( DateTime )this.GetValue( CompositeSecurityPanel.DateToProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.DateToProperty, ( object )value );
            }
        }

        public string SecurityCode
        {
            get
            {
                return ( string )this.GetValue( CompositeSecurityPanel.SecurityCodeProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.SecurityCodeProperty, ( object )value );
            }
        }

        public ExchangeBoard Board
        {
            get
            {
                return ( ExchangeBoard )this.GetValue( CompositeSecurityPanel.BoardProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.BoardProperty, ( object )value );
            }
        }

        public bool CanEdit
        {
            get
            {
                return ( bool )this.GetValue( CompositeSecurityPanel.CanEditProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.CanEditProperty, ( object )value );
            }
        }

        private static void IsStartedPropertyChanged( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        { ( ( CompositeSecurityPanel )sender )._isStarted = ( bool )args.NewValue; }

        public bool IsStarted
        {
            get
            {
                return this._isStarted;
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.IsStartedProperty, ( object )value );
            }
        }

        private static void OnPropertyChanged( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        {
            var owner = ( CompositeSecurityPanel )sender;

            owner.RaiseChangedCommand();
        }

        public bool DrawSources
        {
            get
            {
                return ( bool )this.GetValue( CompositeSecurityPanel.DrawSourcesProperty );
            }
            set
            {
                this.SetValue( CompositeSecurityPanel.DrawSourcesProperty, ( object )value );
            }
        }

        public bool HasError { get; private set; }

        public virtual Type SecurityType
        {
            get
            {
                return typeof( Security );
            }
        }

        public Security Security
        {
            get
            {
                return this._security;
            }
            set
            {
                Security security = value;
                if ( security == null )
                    throw new ArgumentNullException( nameof( value ) );
                this._security = security;
                this._canSave = true;
                this.CanEdit = true;
                if ( !this._security.Id.IsEmpty() )
                {
                    SecurityId securityId = this._security.Id.ToSecurityId( ( SecurityIdGenerator )null );
                    this.SecurityCode = securityId.SecurityCode;
                    this.Board = BaseStudioControl.ExchangeInfoProvider.GetOrCreateBoard( securityId.BoardCode, ( Func<string, ExchangeBoard> )null );
                    this.CanEdit = false;
                    this.Title = this._security.Id;
                }
                else
                    this.Title = LocalizedStrings.Str3217;
                if ( !this.OnSecurityChanged( this._security ) )
                    return;
                this._canSave = false;
            }
        }

        protected virtual string DefaultSecurityCode
        {
            get
            {
                return string.Empty;
            }
        }

        private static DriveCache Cache
        {
            get
            {
                return ServicesRegistry.DriveCache;
            }
        }

        public CompositeSecurityPanel()
        {
            this.InitializeComponent();
            this.InputBorder.SetBindings( UIElement.IsEnabledProperty, ( object )this, nameof( IsStarted ), BindingMode.OneWay, ( IValueConverter )new InverseBooleanConverter(), ( object )null );
            this.MarketDataDrive = CompositeSecurityPanel.Cache.DefaultDrive;
            this.DateFrom = DateTime.Today.AddDays( -180.0 );
            this.DateTo = DateTime.Today;
            this._timer = new ResettableTimer( TimeSpan.FromSeconds( 30.0 ), "Composite" );
            this._timer.Elapsed += ( Action<Func<bool>> )( canProcess =>
              {
                  GuiDispatcher.GlobalDispatcher.AddAction( ( Action )( () => this.IsStarted = false ) );
                  this.ChartPanel.IsAutoRange = false;
              } );
            this._drawTimer = new ResettableTimer( TimeSpan.FromSeconds( 2.0 ), "Composite 2" );
            this._drawTimer.Elapsed += new Action<Func<bool>>( this.DrawTimerOnElapsed );
            this.ChartPanel.SubscribeCandleElement += new Action<IChartCandleElement, CandleSeries>( this.OnChartPanelSubscribeCandleElement );
            this.ChartPanel.SubscribeIndicatorElement += new Action<IChartIndicatorElement, CandleSeries, IIndicator>( this.OnChartPanelSubscribeIndicatorElement );
            this.ChartPanel.UnSubscribeElement += new Action<IChartElement>( this.OnChartPanelUnSubscribeElement );
            this.ChartPanel.IsInteracted = true;
            this.ChartPanel.MinimumRange = 200;
            this.ChartPanel.FillIndicators();
            this.SecurityPicker.SetColumnVisibility( "Id", Visibility.Visible );
            this.SecurityPicker.SetColumnVisibility( "Code", Visibility.Collapsed );
            this._candleManager = ( ICandleManager )BaseStudioControl.Connector;
            this._candleManager.Processing += new Action<CandleSeries, Candle>( this.ProcessCandle );
            this._layoutManager = new LayoutManager( this.DockingManager, ( DocumentGroup )null );
            this._layoutManager.Changed += RaiseChangedCommand;
            this.WhenLoaded( new Action( this.OnLoaded ) );
        }

        protected virtual bool OnSecurityChanged( Security security )
        {
            return false;
        }

        protected virtual void UpdateSecurity( Security security )
        {
        }

        protected virtual void InsertSecurity( Security security )
        {
        }

        protected void ShowError( string errorText )
        {
            this._canSave = errorText == null;
            this.HasError = errorText != null;
            this.InputBorder.BorderBrush = this.HasError ? ( Brush )Brushes.Red : ( Brush )null;
            ToolTip toolTip = ( ToolTip )this.InputBorder.ToolTip;
            ( ( TextBlock )toolTip.Content ).Text = errorText;
            toolTip.Placement = PlacementMode.Bottom;
            toolTip.PlacementTarget = ( UIElement )this.InputBorder;
            toolTip.IsOpen = this.HasError;
        }

        protected string Validate( IEnumerable<Security> innerSecurities, Security parent = null )
        {
            foreach ( Security innerSecurity in innerSecurities )
            {
                if ( innerSecurity == this.Security )
                {
                    string str;
                    if ( parent == null )
                        str = LocalizedStrings.Str3219Params.Put( ( object )this.Security.Id );
                    else
                        str = LocalizedStrings.Str3218Params.Put( ( object )this.Security.Id, ( object )parent.Id );
                    return str;
                }
                BasketSecurity security = innerSecurity as BasketSecurity;
                if ( security != null )
                    return this.Validate( security.GetInnerSecurities( BaseStudioControl.SecurityProvider ), innerSecurity );
            }
            return ( string )null;
        }

        private void OnLoaded()
        {
            this.SecurityPicker.SecurityProvider = BaseStudioControl.SecurityProvider;
            this._mainArea = this.ChartPanel.Areas.FirstOrDefault<IChartArea>();
            if ( this._mainArea == null )
            {
                this._mainArea = this.ChartPanel.CreateArea();
                this._mainArea.Title = LocalizedStrings.Panel + " 1";
                this.ChartPanel.AddArea( this._mainArea );
            }
            this._candleElement = this._mainArea.Elements.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
            if ( this._candleElement == null )
                this.ChartPanel.AddElement( this._mainArea, this._candleElement = this.ChartPanel.CreateCandleElement(), this.CreateSeries() );
            this._mainArea.Elements.OfType<IChartIndicatorElement>().Where<IChartIndicatorElement>( ( Func<IChartIndicatorElement, bool> )( e => this._indicators.TryGetValue<IChartIndicatorElement, IIndicator>( e ) is CompositeSecurityPanel.CandlePartIndicator ) ).ForEach<IChartIndicatorElement>( ( Action<IChartIndicatorElement> )( e => this._sourceElements.Add( ( ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )e ) ).Security, e ) ) );
            this._isLoaded = true;
            if ( this.HasError )
                return;
            this.StartSeries();
        }

        private void OnChartPanelSubscribeCandleElement(
          IChartCandleElement element,
          CandleSeries candleSeries )
        {
            this.AddElement( ( IChartElement )element, candleSeries );
        }

        private void OnChartPanelSubscribeIndicatorElement(
          IChartIndicatorElement element,
          CandleSeries candleSeries,
          IIndicator indicator )
        {
            this.ChartPanel.SetSource( ( IChartElement )element, ( object )candleSeries );
            this._indicators.Add( element, indicator );
            this.AddElement( ( IChartElement )element, candleSeries );
        }

        private void OnChartPanelUnSubscribeElement( IChartElement element )
        {
            if ( !this._isLoaded )
                return;
            CandleSeries source = ( CandleSeries )this.ChartPanel.GetSource( element );
            if ( source == null || !( element is IChartCandleElement ) )
                return;
            this._candleManager.Stop( source );
        }

        private void AddElement( IChartElement element, CandleSeries candleSeries )
        {
            if ( !this._isLoaded || candleSeries == null )
                return;
            this._changedElements.Add( element );
            this._skipElements.Add( element );
            this._drawTimer.Activate();
        }

        public override void Load( SettingsStorage storage )
        {
            this.DateFrom = storage.GetValue<DateTime>( "DateFrom", this.DateFrom );
            this.DateTo = storage.GetValue<DateTime>( "DateTo", this.DateTo );
            this.MarketDataDrive = CompositeSecurityPanel.Cache.GetDrive( storage.GetValue<string>( "MarketDataDrive", ( string )null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "ChartPanel", ( SettingsStorage )null );
            if ( storage1 != null )
                this.ChartPanel.Load( storage1 );
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "SecurityPicker", ( SettingsStorage )null );
            if ( storage2 != null )
                this.SecurityPicker.Load( storage2 );
            string str = storage.GetValue<string>( "Layout", ( string )null );
            if ( str.IsEmpty() )
                return;
            this._layoutManager.LoadLayout( str );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue<DateTime>( "DateFrom", this.DateFrom );
            storage.SetValue<DateTime>( "DateTo", this.DateTo );
            if ( this.MarketDataDrive != null )
                storage.SetValue<string>( "MarketDataDrive", this.MarketDataDrive.Path );
            storage.SetValue<SettingsStorage>( "ChartPanel", this.ChartPanel.Save() );
            storage.SetValue<SettingsStorage>( "SecurityPicker", this.SecurityPicker.Save() );
            storage.SetValue<string>( "Layout", this._layoutManager.SaveLayout() );
        }

        public override void Dispose()
        {
            if ( this.IsStarted )
                this.StopSeries();
            this._drawTimer.Flush();
            this._timer.Flush();
            base.Dispose();
        }

        private Security CreateSecurity()
        {
            Security instance = this.SecurityType.CreateInstance<Security>();
            string secCode = this.SecurityCode.IsEmpty() ? this.DefaultSecurityCode : this.SecurityCode;
            instance.Id = this._idGenerator.GenerateId( secCode, this.Board );
            instance.Code = secCode;
            instance.Name = secCode;
            instance.Board = this.Board;
            this.UpdateSecurity( instance );
            return instance;
        }

        private CandleSeries CreateSeries()
        {
            return new CandleSeries( typeof( TimeFrameCandle ), this.CreateSecurity(), ( object )TimeSpan.FromMinutes( 5.0 ) ) { From = new DateTimeOffset?( ( DateTimeOffset )this.DateFrom ), To = new DateTimeOffset?( ( DateTimeOffset )this.DateTo ) };
        }

        private void AddIndicator( IChartIndicatorElement element )
        {
            CandleSeries source = ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )element );
            if ( source == null || this._sourceElements.ContainsKey( source.Security ) )
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            lock ( this._syncRoot )
            {
                foreach ( TimeFrameCandle timeFrameCandle in this._candleManager.GetCandles<TimeFrameCandle>( source ).Take<TimeFrameCandle>( this._candlesCount ) )
                    data.Group( timeFrameCandle.OpenTime ).Add( element, this.CreateIndicatorValue( element, ( Candle )timeFrameCandle ) );
                this._skipElements.Remove( ( IChartElement )element );
            }
            this.ChartPanel.Reset( ( IEnumerable<IChartElement> )new IChartIndicatorElement[1]
            {
        element
            } );
            this.ChartPanel.Draw( data );
        }

        private void ProcessCandle( CandleSeries series, Candle candle )
        {
            if ( !this._isStarted )
                ( ( DispatcherObject )this ).GuiAsync( ( Action )( () => this.IsStarted = true ) );
            this._timer.Activate();
            ++this._candlesCount;
            if ( this._candlesCount % 100 == 0 )
                Thread.Sleep( 200 );
            CandleSeries source = ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )this._candleElement );
            if ( series == source )
            {
                IChartDrawData data = this.ChartPanel.CreateData();
                IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( candle.OpenTime );
                lock ( this._syncRoot )
                {
                    foreach ( IChartElement chartElement in this.ChartPanel.Elements.Where<IChartElement>( ( Func<IChartElement, bool> )( e => this.ChartPanel.GetSource( e ) == series ) ) )
                    {
                        if ( !this._skipElements.Contains( chartElement ) )
                        {
                            IChartCandleElement element1 = chartElement as IChartCandleElement;
                            if ( element1 == null )
                            {
                                IChartIndicatorElement element2 = chartElement as IChartIndicatorElement;
                                if ( element2 != null )
                                    chartDrawDataItem.Add( element2, this.CreateIndicatorValue( element2, candle ) );
                            }
                            else
                                chartDrawDataItem.Add( element1, candle );
                        }
                    }
                }
                this.ChartPanel.Draw( data );
                if ( !( series.Security is ContinuousSecurity ) )
                    return;
                this.ProcessContinuousSourceElements( candle );
            }
            else
                this.ProcessIndexSourceElements( candle );
        }

        private void ProcessIndexSourceElements( Candle candle )
        {
            IChartIndicatorElement element = this._sourceElements.TryGetValue<Security, IChartIndicatorElement>( candle.Security );
            if ( element == null )
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            data.Group( candle.OpenTime ).Add( element, this.CreateIndicatorValue( element, candle ) );
            this.ChartPanel.Draw( data );
        }

        private void ProcessContinuousSourceElements( Candle candle )
        {
            IChartDrawData data = this.ChartPanel.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( candle.OpenTime );
            foreach ( IChartIndicatorElement element in this._sourceElements.Select<KeyValuePair<Security, IChartIndicatorElement>, IChartIndicatorElement>( ( Func<KeyValuePair<Security, IChartIndicatorElement>, IChartIndicatorElement> )( sourceElement => sourceElement.Value ) ) )
                chartDrawDataItem.Add( element, this.CreateIndicatorValue( element, candle ) );
            this.ChartPanel.Draw( data );
        }

        private IIndicatorValue CreateIndicatorValue(
          IChartIndicatorElement element,
          Candle candle )
        {
            IIndicator indicator = this._indicators.TryGetValue<IChartIndicatorElement, IIndicator>( element );
            if ( indicator == null )
                throw new InvalidOperationException( LocalizedStrings.IndicatorNotFound.Put( ( object )element ) );
            return indicator.Process( candle );
        }

        private void DrawTimerOnElapsed( Func<bool> canProcess )
        {
            try
            {
                this.RaiseChangedCommand();
                IChartElement[ ] source = this._changedElements.CopyAndClear<IChartElement>();
                IChartCandleElement chartCandleElement = source.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
                if ( chartCandleElement == null )
                {
                    foreach ( IChartIndicatorElement element in source.OfType<IChartIndicatorElement>() )
                        this.AddIndicator( element );
                }
                else
                {
                    this._candlesCount = 0;
                    this.ChartPanel.IsAutoRange = true;
                    GuiDispatcher.GlobalDispatcher.AddAction( ( Action )( () => this.IsStarted = true ) );
                    this._skipElements.Clear();
                    this._candleManager.Start( ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )chartCandleElement ) );
                }
            }
            catch ( Exception ex )
            {
                ex.LogError( ( string )null );
            }
        }

        private void Save( bool showErrorIfNoCode )
        {
            if ( this.SecurityCode.IsEmpty() || ( Equatable<ExchangeBoard> )this.Board == ( ExchangeBoard )null )
            {
                if ( !showErrorIfNoCode )
                    return;
                int num = ( int )new MessageBoxBuilder().Owner( ( DependencyObject )this ).Caption( LocalizedStrings.Str3220 ).Text( LocalizedStrings.Str3221 ).Warning().Show();
            }
            else
            {
                string id = this._idGenerator.GenerateId( this.SecurityCode, this.Board );
                ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
                Security security1 = securityStorage.LookupById( id );
                if ( security1 != null && security1.GetType() != this.SecurityType )
                {
                    int num = ( int )new MessageBoxBuilder().Owner( ( DependencyObject )this ).Caption( LocalizedStrings.Str3222 ).Text( LocalizedStrings.Str3223Params.Put( ( object )security1.Id, ( object )security1.Type ) ).Error().Show();
                }
                else
                {
                    if ( security1 != null )
                    {
                        if ( security1 != this.Security )
                        {
                            if ( new MessageBoxBuilder().Owner( ( DependencyObject )this ).Caption( LocalizedStrings.Str3222 ).Text( LocalizedStrings.Str3224Params.Put( ( object )security1.Id ) ).Question().YesNo().Show() != MessageBoxResult.Yes )
                                return;
                        }
                        this.UpdateSecurity( security1 );
                        securityStorage.Save( security1, true );
                        this.Security = security1;
                    }
                    else
                    {
                        Security security2 = this.Security;
                        security2.Id = id;
                        security2.Code = this.SecurityCode;
                        security2.Board = this.Board;
                        this.UpdateSecurity( security2 );
                        securityStorage.Save( security2, true );
                        this.SecurityPicker.Securities.Add( security2 );
                        this.ChartPanel.Elements.Select<IChartElement, object>( ( Func<IChartElement, object> )( e => this.ChartPanel.GetSource( e ) ) ).OfType<CandleSeries>().ForEach<CandleSeries>( ( Action<CandleSeries> )( series =>
                                  {
                                      series.Security.Id = id;
                                      series.Security.Code = this.SecurityCode;
                                      series.Security.Board = this.Board;
                                  } ) );
                        this.CanEdit = false;
                        this.Title = id;
                        this.RaiseChangedCommand();
                    }
                    this._canSave = false;
                }
            }
        }

        private void StopSeries()
        {
            IChartCandleElement chartCandleElement = this.ChartPanel.Elements.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
            if ( chartCandleElement != null )
                this._candleManager.Stop( ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )chartCandleElement ) );
            this._timer.Cancel();
        }

        private void ExecutedSaveSecurityCommand( object sender, ExecutedRoutedEventArgs e )
        {
            this.Save( true );
        }

        private void CanExecuteSaveSecurityCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this._canSave && !this.HasError;
        }

        private void ExecutedDrawSecurityCommand( object sender, ExecutedRoutedEventArgs e )
        {
            if ( !this.IsStarted )
            {
                this.Save( false );
                this.StartSeries();
            }
            else
                this.StopSeries();
        }

        private void StartSeries()
        {
            CandleSeries mainSeries = ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )this._candleElement );
            if ( this._candleElement != null && this._candleManager.Series.Any<CandleSeries>( ( Func<CandleSeries, bool> )( s => s == mainSeries ) ) )
                this._candleManager.Stop( mainSeries );
            this.ChartPanel.Reset( this.ChartPanel.Elements );
            this.RemoveSourceElements();
            mainSeries.Security = this.CreateSecurity();
            mainSeries.From = new DateTimeOffset?( ( DateTimeOffset )this.DateFrom );
            mainSeries.To = new DateTimeOffset?( ( DateTimeOffset )this.DateTo );
            if ( this.DrawSources )
            {
                Security security1 = mainSeries.Security;
                IndexSecurity security2 = security1 as IndexSecurity;
                if ( security2 == null )
                {
                    ContinuousSecurity security3 = security1 as ContinuousSecurity;
                    if ( security3 != null )
                        this.CreateSourceElements( security3 );
                }
                else
                    this.CreateSourceElements( security2 );
            }
            this.IsStarted = true;
            this.AddElement( ( IChartElement )this._candleElement, mainSeries );
            this._drawTimer.Flush();
        }

        private void RemoveSourceElements()
        {
            IChartArea mainArea = this._mainArea;
            mainArea.Elements.RemoveWhere<IChartElement>( ( Func<IChartElement, bool> )( el =>
               {
                   IChartIndicatorElement indicatorElement = el as IChartIndicatorElement;
                   if ( indicatorElement == null )
                       return false;
                   CandleSeries source = ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )indicatorElement );
                   if ( !this._sourceElements.ContainsKey( source.Security ) )
                       return false;
                   this._sourceElements.Remove( source.Security );
                   return true;
               } ) );
            mainArea.YAxises.RemoveWhere<IChartAxis>( ( Func<IChartAxis, bool> )( a => a.Id.StartsWith( "SA_" ) ) );
        }

        private void CreateSourceElements( IndexSecurity security )
        {
            IChartArea mainArea = this._mainArea;
            int index = 1;
            foreach ( Security innerSecurity in security.GetInnerSecurities( BaseStudioControl.SecurityProvider ) )
            {
                string str = "SA_" + index++.ToString();
                CandleSeries candleSeries = new CandleSeries( typeof( TimeFrameCandle ), innerSecurity, ( object )TimeSpan.FromMinutes( 5.0 ) );
                IChartIndicatorElement indicatorElement = this.ChartPanel.CreateIndicatorElement();
                indicatorElement.FullTitle = innerSecurity.Id;
                indicatorElement.Color = this._colors[index];
                indicatorElement.YAxisId = str;
                indicatorElement.StrokeThickness = 1;
                IChartAxis axis = this.ChartPanel.CreateAxis();
                axis.Id = str;
                axis.AutoRange = true;
                axis.AxisType = ChartAxisType.Numeric;
                mainArea.YAxises.Add( axis );
                CompositeSecurityPanel.CandlePartIndicator candlePartIndicator = new CompositeSecurityPanel.CandlePartIndicator();
                this._sourceElements.Add( innerSecurity, indicatorElement );
                this.ChartPanel.AddElement( mainArea, indicatorElement, candleSeries, ( IIndicator )candlePartIndicator );
            }
        }

        private void CreateSourceElements( ContinuousSecurity security )
        {
            IChartArea mainArea = this._mainArea;
            int num = 1;
            foreach ( Security innerSecurity in security.GetInnerSecurities( BaseStudioControl.SecurityProvider ) )
            {
                CandleSeries candleSeries = new CandleSeries( typeof( TimeFrameCandle ), innerSecurity, ( object )TimeSpan.FromMinutes( 5.0 ) );
                IChartIndicatorElement indicatorElement = this.ChartPanel.CreateIndicatorElement();
                indicatorElement.FullTitle = innerSecurity.Id;
                indicatorElement.Color = this._colors[num++];
                indicatorElement.StrokeThickness = 1;
                CompositeSecurityPanel.CandlePartIndicator candlePartIndicator = new CompositeSecurityPanel.CandlePartIndicator();
                this._sourceElements.Add( innerSecurity, indicatorElement );
                this.ChartPanel.AddElement( mainArea, indicatorElement, candleSeries, ( IIndicator )candlePartIndicator );
            }
        }

        private void CanExecuteDrawSecurityCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !this.HasError;
        }

        private void SecurityPicker_OnSecurityDoubleClick( Security security )
        {
            if ( this.IsStarted )
                return;
            this.InsertSecurity( security );
        }

        private void SecurityPicker_OnMouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( this.IsStarted )
                return;
            Security selectedSecurity = this.SecurityPicker.SelectedSecurity;
            if ( selectedSecurity == null )
                return;
            int num = ( int )DragDrop.DoDragDrop( ( DependencyObject )this.SecurityPicker, ( object )selectedSecurity, DragDropEffects.Copy );
        }



        public class CandlePartIndicator : BaseIndicator
        {
            private readonly Security _security;

            public CandlePartIndicator()
            {
            }

            public CandlePartIndicator( Security security )
            {
                this._security = security;
            }

            protected override IIndicatorValue OnProcess( IIndicatorValue input )
            {
                Candle candle = input.GetValue<Candle>();
                this.IsFormed = this._security == null || candle.Security == this._security;
                return ( IIndicatorValue )new CandleIndicatorValue( ( IIndicator )this, candle, ( Func<Candle, Decimal> )( c => c.ClosePrice ) );
            }
        }
    }
}
