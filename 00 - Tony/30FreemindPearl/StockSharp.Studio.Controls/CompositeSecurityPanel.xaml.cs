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

        public static readonly DependencyProperty SecurityCodeProperty = DependencyProperty.Register( nameof( SecurityCode ), typeof( string ), typeof( CompositeSecurityPanel ), new PropertyMetadata( string.Empty ) );
        public static readonly DependencyProperty BoardProperty = DependencyProperty.Register( nameof( Board ), typeof( ExchangeBoard ), typeof( CompositeSecurityPanel ), new PropertyMetadata( ExchangeBoard.Associated ) );
        public static readonly DependencyProperty CanEditProperty = DependencyProperty.Register( nameof( CanEdit ), typeof( bool ), typeof( CompositeSecurityPanel ), new PropertyMetadata( true ) );
        public static readonly DependencyProperty IsStartedProperty = DependencyProperty.Register(
            nameof( IsStarted ),
            typeof( bool ),
            typeof( CompositeSecurityPanel ),
            new PropertyMetadata( false, new PropertyChangedCallback( IsStartedPropertyChanged ) ) );

        public static readonly DependencyProperty DrawSourcesProperty = DependencyProperty.Register( nameof( DrawSources ), typeof( bool ), typeof( CompositeSecurityPanel ), new PropertyMetadata( true ) );
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
                return ( IMarketDataDrive )GetValue( MarketDataDriveProperty );
            }
            set
            {
                SetValue( MarketDataDriveProperty, value );
            }
        }

        public DateTime DateFrom
        {
            get
            {
                return ( DateTime )GetValue( DateFromProperty );
            }
            set
            {
                SetValue( DateFromProperty, value );
            }
        }

        public DateTime DateTo
        {
            get
            {
                return ( DateTime )GetValue( DateToProperty );
            }
            set
            {
                SetValue( DateToProperty, value );
            }
        }

        public string SecurityCode
        {
            get
            {
                return ( string )GetValue( SecurityCodeProperty );
            }
            set
            {
                SetValue( SecurityCodeProperty, value );
            }
        }

        public ExchangeBoard Board
        {
            get
            {
                return ( ExchangeBoard )GetValue( BoardProperty );
            }
            set
            {
                SetValue( BoardProperty, value );
            }
        }

        public bool CanEdit
        {
            get
            {
                return ( bool )GetValue( CanEditProperty );
            }
            set
            {
                SetValue( CanEditProperty, value );
            }
        }

        private static void IsStartedPropertyChanged( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        { ( ( CompositeSecurityPanel )sender )._isStarted = ( bool )args.NewValue; }

        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
            set
            {
                SetValue( IsStartedProperty, value );
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
                return ( bool )GetValue( DrawSourcesProperty );
            }
            set
            {
                SetValue( DrawSourcesProperty, value );
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
                return _security;
            }
            set
            {
                Security security = value;
                if ( security == null )
                    throw new ArgumentNullException( nameof( value ) );
                _security = security;
                _canSave = true;
                CanEdit = true;
                if ( !_security.Id.IsEmpty() )
                {
                    SecurityId securityId = _security.Id.ToSecurityId( null );
                    SecurityCode = securityId.SecurityCode;
                    Board = ExchangeInfoProvider.GetOrCreateBoard( securityId.BoardCode, null );
                    CanEdit = false;
                    Title = _security.Id;
                }
                else
                    Title = LocalizedStrings.Str3217;
                if ( !OnSecurityChanged( _security ) )
                    return;
                _canSave = false;
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
            InitializeComponent();
            InputBorder.SetBindings( IsEnabledProperty, this, nameof( IsStarted ), BindingMode.OneWay, new InverseBooleanConverter(), null );
            MarketDataDrive = Cache.DefaultDrive;
            DateFrom = DateTime.Today.AddDays( -180.0 );
            DateTo = DateTime.Today;
            _timer = new ResettableTimer( TimeSpan.FromSeconds( 30.0 ), "Composite" );
            _timer.Elapsed += canProcess =>
              {
                  GuiDispatcher.GlobalDispatcher.AddAction( () => IsStarted = false );
                  ChartPanel.IsAutoRange = false;
              };
            _drawTimer = new ResettableTimer( TimeSpan.FromSeconds( 2.0 ), "Composite 2" );
            _drawTimer.Elapsed += new Action<Func<bool>>( DrawTimerOnElapsed );
            ChartPanel.SubscribeCandleElement += new Action<IChartCandleElement, CandleSeries>( OnChartPanelSubscribeCandleElement );
            ChartPanel.SubscribeIndicatorElement += new Action<IChartIndicatorElement, CandleSeries, IIndicator>( OnChartPanelSubscribeIndicatorElement );
            ChartPanel.UnSubscribeElement += new Action<IChartElement>( OnChartPanelUnSubscribeElement );
            ChartPanel.IsInteracted = true;
            ChartPanel.MinimumRange = 200;
            ChartPanel.FillIndicators();
            SecurityPicker.SetColumnVisibility( "Id", Visibility.Visible );
            SecurityPicker.SetColumnVisibility( "Code", Visibility.Collapsed );
            _candleManager = Connector;
            _candleManager.Processing += new Action<CandleSeries, Candle>( ProcessCandle );
            _layoutManager = new LayoutManager( DockingManager, null );
            _layoutManager.Changed += RaiseChangedCommand;
            WhenLoaded( new Action( OnLoaded ) );
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
            _canSave = errorText == null;
            HasError = errorText != null;
            InputBorder.BorderBrush = HasError ? Brushes.Red : ( Brush )null;
            ToolTip toolTip = ( ToolTip )InputBorder.ToolTip;
            ( ( TextBlock )toolTip.Content ).Text = errorText;
            toolTip.Placement = PlacementMode.Bottom;
            toolTip.PlacementTarget = InputBorder;
            toolTip.IsOpen = HasError;
        }

        protected string Validate( IEnumerable<Security> innerSecurities, Security parent = null )
        {
            foreach ( Security innerSecurity in innerSecurities )
            {
                if ( innerSecurity == Security )
                {
                    string str;
                    if ( parent == null )
                        str = LocalizedStrings.Str3219Params.Put( Security.Id );
                    else
                        str = LocalizedStrings.Str3218Params.Put( Security.Id, parent.Id );
                    return str;
                }
                BasketSecurity security = innerSecurity as BasketSecurity;
                if ( security != null )
                    return Validate( security.GetInnerSecurities( SecurityProvider ), innerSecurity );
            }
            return null;
        }

        private void OnLoaded()
        {
            SecurityPicker.SecurityProvider = SecurityProvider;
            _mainArea = ChartPanel.Areas.FirstOrDefault();
            if ( _mainArea == null )
            {
                _mainArea = ChartPanel.CreateArea();
                _mainArea.Title = LocalizedStrings.Panel + " 1";
                ChartPanel.AddArea( _mainArea );
            }
            _candleElement = _mainArea.Elements.OfType<IChartCandleElement>().FirstOrDefault();
            if ( _candleElement == null )
                ChartPanel.AddElement( _mainArea, _candleElement = ChartPanel.CreateCandleElement(), CreateSeries() );
            _mainArea.Elements.OfType<IChartIndicatorElement>().Where( e => _indicators.TryGetValue( e ) is CandlePartIndicator ).ForEach( e => _sourceElements.Add( ( ( CandleSeries )ChartPanel.GetSource( e ) ).Security, e ) );
            _isLoaded = true;
            if ( HasError )
                return;
            StartSeries();
        }

        private void OnChartPanelSubscribeCandleElement(
          IChartCandleElement element,
          CandleSeries candleSeries )
        {
            AddElement( element, candleSeries );
        }

        private void OnChartPanelSubscribeIndicatorElement(
          IChartIndicatorElement element,
          CandleSeries candleSeries,
          IIndicator indicator )
        {
            ChartPanel.SetSource( element, candleSeries );
            _indicators.Add( element, indicator );
            AddElement( element, candleSeries );
        }

        private void OnChartPanelUnSubscribeElement( IChartElement element )
        {
            if ( !_isLoaded )
                return;
            CandleSeries source = ( CandleSeries )ChartPanel.GetSource( element );
            if ( source == null || !( element is IChartCandleElement ) )
                return;
            _candleManager.Stop( source );
        }

        private void AddElement( IChartElement element, CandleSeries candleSeries )
        {
            if ( !_isLoaded || candleSeries == null )
                return;
            _changedElements.Add( element );
            _skipElements.Add( element );
            _drawTimer.Activate();
        }

        public override void Load( SettingsStorage storage )
        {
            DateFrom = storage.GetValue( "DateFrom", DateFrom );
            DateTo = storage.GetValue( "DateTo", DateTo );
            MarketDataDrive = Cache.GetDrive( storage.GetValue<string>( "MarketDataDrive", null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "ChartPanel", null );
            if ( storage1 != null )
                ChartPanel.Load( storage1 );
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "SecurityPicker", null );
            if ( storage2 != null )
                SecurityPicker.Load( storage2 );
            string str = storage.GetValue<string>( "Layout", null );
            if ( str.IsEmpty() )
                return;
            _layoutManager.LoadLayout( str );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( "DateFrom", DateFrom );
            storage.SetValue( "DateTo", DateTo );
            if ( MarketDataDrive != null )
                storage.SetValue( "MarketDataDrive", MarketDataDrive.Path );
            storage.SetValue( "ChartPanel", ChartPanel.Save() );
            storage.SetValue( "SecurityPicker", SecurityPicker.Save() );
            storage.SetValue( "Layout", _layoutManager.SaveLayout() );
        }

        public override void Dispose()
        {
            if ( IsStarted )
                StopSeries();
            _drawTimer.Flush();
            _timer.Flush();
            base.Dispose();
        }

        private Security CreateSecurity()
        {
            Security instance = SecurityType.CreateInstance<Security>();
            string secCode = SecurityCode.IsEmpty() ? DefaultSecurityCode : SecurityCode;
            instance.Id = _idGenerator.GenerateId( secCode, Board );
            instance.Code = secCode;
            instance.Name = secCode;
            instance.Board = Board;
            UpdateSecurity( instance );
            return instance;
        }

        private CandleSeries CreateSeries()
        {
            return new CandleSeries( typeof( TimeFrameCandle ), CreateSecurity(), TimeSpan.FromMinutes( 5.0 ) ) { From = new DateTimeOffset?( ( DateTimeOffset )DateFrom ), To = new DateTimeOffset?( ( DateTimeOffset )DateTo ) };
        }

        private void AddIndicator( IChartIndicatorElement element )
        {
            CandleSeries source = ( CandleSeries )ChartPanel.GetSource( element );
            if ( source == null || _sourceElements.ContainsKey( source.Security ) )
                return;
            IChartDrawData data = ChartPanel.CreateData();
            lock ( _syncRoot )
            {
                foreach ( TimeFrameCandle timeFrameCandle in _candleManager.GetCandles<TimeFrameCandle>( source ).Take( _candlesCount ) )
                    data.Group( timeFrameCandle.OpenTime ).Add( element, CreateIndicatorValue( element, timeFrameCandle ) );
                _skipElements.Remove( element );
            }
            ChartPanel.Reset( new IChartIndicatorElement[1]
            {
        element
            } );
            ChartPanel.Draw( data );
        }

        private void ProcessCandle( CandleSeries series, Candle candle )
        {
            if ( !_isStarted )
                this.GuiAsync( () => IsStarted = true );
            _timer.Activate();
            ++_candlesCount;
            if ( _candlesCount % 100 == 0 )
                Thread.Sleep( 200 );
            CandleSeries source = ( CandleSeries )ChartPanel.GetSource( _candleElement );
            if ( series == source )
            {
                IChartDrawData data = ChartPanel.CreateData();
                IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( candle.OpenTime );
                lock ( _syncRoot )
                {
                    foreach ( IChartElement chartElement in ChartPanel.Elements.Where( e => ChartPanel.GetSource( e ) == series ) )
                    {
                        if ( !_skipElements.Contains( chartElement ) )
                        {
                            IChartCandleElement element1 = chartElement as IChartCandleElement;
                            if ( element1 == null )
                            {
                                IChartIndicatorElement element2 = chartElement as IChartIndicatorElement;
                                if ( element2 != null )
                                    chartDrawDataItem.Add( element2, CreateIndicatorValue( element2, candle ) );
                            }
                            else
                                chartDrawDataItem.Add( element1, candle );
                        }
                    }
                }
                ChartPanel.Draw( data );
                if ( !( series.Security is ContinuousSecurity ) )
                    return;
                ProcessContinuousSourceElements( candle );
            }
            else
                ProcessIndexSourceElements( candle );
        }

        private void ProcessIndexSourceElements( Candle candle )
        {
            IChartIndicatorElement element = _sourceElements.TryGetValue( candle.Security );
            if ( element == null )
                return;
            IChartDrawData data = ChartPanel.CreateData();
            data.Group( candle.OpenTime ).Add( element, CreateIndicatorValue( element, candle ) );
            ChartPanel.Draw( data );
        }

        private void ProcessContinuousSourceElements( Candle candle )
        {
            IChartDrawData data = ChartPanel.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( candle.OpenTime );
            foreach ( IChartIndicatorElement element in _sourceElements.Select( sourceElement => sourceElement.Value ) )
                chartDrawDataItem.Add( element, CreateIndicatorValue( element, candle ) );
            ChartPanel.Draw( data );
        }

        private IIndicatorValue CreateIndicatorValue(
          IChartIndicatorElement element,
          Candle candle )
        {
            IIndicator indicator = _indicators.TryGetValue( element );
            if ( indicator == null )
                throw new InvalidOperationException( LocalizedStrings.IndicatorNotFound.Put( element ) );
            return indicator.Process( candle );
        }

        private void DrawTimerOnElapsed( Func<bool> canProcess )
        {
            try
            {
                RaiseChangedCommand();
                IChartElement[ ] source = _changedElements.CopyAndClear();
                IChartCandleElement chartCandleElement = source.OfType<IChartCandleElement>().FirstOrDefault();
                if ( chartCandleElement == null )
                {
                    foreach ( IChartIndicatorElement element in source.OfType<IChartIndicatorElement>() )
                        AddIndicator( element );
                }
                else
                {
                    _candlesCount = 0;
                    ChartPanel.IsAutoRange = true;
                    GuiDispatcher.GlobalDispatcher.AddAction( () => IsStarted = true );
                    _skipElements.Clear();
                    _candleManager.Start( ( CandleSeries )ChartPanel.GetSource( chartCandleElement ) );
                }
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
            }
        }

        private void Save( bool showErrorIfNoCode )
        {
            if ( SecurityCode.IsEmpty() || Board == null )
            {
                if ( !showErrorIfNoCode )
                    return;
                int num = ( int )new MessageBoxBuilder().Owner( this ).Caption( LocalizedStrings.Str3220 ).Text( LocalizedStrings.Str3221 ).Warning().Show();
            }
            else
            {
                string id = _idGenerator.GenerateId( SecurityCode, Board );
                ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
                Security security1 = securityStorage.LookupById( id );
                if ( security1 != null && security1.GetType() != SecurityType )
                {
                    int num = ( int )new MessageBoxBuilder().Owner( this ).Caption( LocalizedStrings.Str3222 ).Text( LocalizedStrings.Str3223Params.Put( security1.Id, security1.Type ) ).Error().Show();
                }
                else
                {
                    if ( security1 != null )
                    {
                        if ( security1 != Security )
                        {
                            if ( new MessageBoxBuilder().Owner( this ).Caption( LocalizedStrings.Str3222 ).Text( LocalizedStrings.Str3224Params.Put( security1.Id ) ).Question().YesNo().Show() != MessageBoxResult.Yes )
                                return;
                        }
                        UpdateSecurity( security1 );
                        securityStorage.Save( security1, true );
                        Security = security1;
                    }
                    else
                    {
                        Security security2 = Security;
                        security2.Id = id;
                        security2.Code = SecurityCode;
                        security2.Board = Board;
                        UpdateSecurity( security2 );
                        securityStorage.Save( security2, true );
                        SecurityPicker.Securities.Add( security2 );
                        ChartPanel.Elements.Select( e => ChartPanel.GetSource( e ) ).OfType<CandleSeries>().ForEach( series =>
                                  {
                                      series.Security.Id = id;
                                      series.Security.Code = SecurityCode;
                                      series.Security.Board = Board;
                                  } );
                        CanEdit = false;
                        Title = id;
                        RaiseChangedCommand();
                    }
                    _canSave = false;
                }
            }
        }

        private void StopSeries()
        {
            IChartCandleElement chartCandleElement = ChartPanel.Elements.OfType<IChartCandleElement>().FirstOrDefault();
            if ( chartCandleElement != null )
                _candleManager.Stop( ( CandleSeries )ChartPanel.GetSource( chartCandleElement ) );
            _timer.Cancel();
        }

        private void ExecutedSaveSecurityCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Save( true );
        }

        private void CanExecuteSaveSecurityCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _canSave && !HasError;
        }

        private void ExecutedDrawSecurityCommand( object sender, ExecutedRoutedEventArgs e )
        {
            if ( !IsStarted )
            {
                Save( false );
                StartSeries();
            }
            else
                StopSeries();
        }

        private void StartSeries()
        {
            CandleSeries mainSeries = ( CandleSeries )ChartPanel.GetSource( _candleElement );
            if ( _candleElement != null && _candleManager.Series.Any( s => s == mainSeries ) )
                _candleManager.Stop( mainSeries );
            ChartPanel.Reset( ChartPanel.Elements );
            RemoveSourceElements();
            mainSeries.Security = CreateSecurity();
            mainSeries.From = new DateTimeOffset?( ( DateTimeOffset )DateFrom );
            mainSeries.To = new DateTimeOffset?( ( DateTimeOffset )DateTo );
            if ( DrawSources )
            {
                Security security1 = mainSeries.Security;
                IndexSecurity security2 = security1 as IndexSecurity;
                if ( security2 == null )
                {
                    ContinuousSecurity security3 = security1 as ContinuousSecurity;
                    if ( security3 != null )
                        CreateSourceElements( security3 );
                }
                else
                    CreateSourceElements( security2 );
            }
            IsStarted = true;
            AddElement( _candleElement, mainSeries );
            _drawTimer.Flush();
        }

        private void RemoveSourceElements()
        {
            IChartArea mainArea = _mainArea;
            mainArea.Elements.RemoveWhere( el =>
               {
                   IChartIndicatorElement indicatorElement = el as IChartIndicatorElement;
                   if ( indicatorElement == null )
                       return false;
                   CandleSeries source = ( CandleSeries )ChartPanel.GetSource( indicatorElement );
                   if ( !_sourceElements.ContainsKey( source.Security ) )
                       return false;
                   _sourceElements.Remove( source.Security );
                   return true;
               } );
            mainArea.YAxises.RemoveWhere( a => a.Id.StartsWith( "SA_" ) );
        }

        private void CreateSourceElements( IndexSecurity security )
        {
            IChartArea mainArea = _mainArea;
            int index = 1;
            foreach ( Security innerSecurity in security.GetInnerSecurities( SecurityProvider ) )
            {
                string str = "SA_" + index++.ToString();
                CandleSeries candleSeries = new CandleSeries( typeof( TimeFrameCandle ), innerSecurity, TimeSpan.FromMinutes( 5.0 ) );
                IChartIndicatorElement indicatorElement = ChartPanel.CreateIndicatorElement();
                indicatorElement.FullTitle = innerSecurity.Id;
                indicatorElement.Color = _colors[index];
                indicatorElement.YAxisId = str;
                indicatorElement.StrokeThickness = 1;
                IChartAxis axis = ChartPanel.CreateAxis();
                axis.Id = str;
                axis.AutoRange = true;
                axis.AxisType = ChartAxisType.Numeric;
                mainArea.YAxises.Add( axis );
                CandlePartIndicator candlePartIndicator = new CandlePartIndicator();
                _sourceElements.Add( innerSecurity, indicatorElement );
                ChartPanel.AddElement( mainArea, indicatorElement, candleSeries, candlePartIndicator );
            }
        }

        private void CreateSourceElements( ContinuousSecurity security )
        {
            IChartArea mainArea = _mainArea;
            int num = 1;
            foreach ( Security innerSecurity in security.GetInnerSecurities( SecurityProvider ) )
            {
                CandleSeries candleSeries = new CandleSeries( typeof( TimeFrameCandle ), innerSecurity, TimeSpan.FromMinutes( 5.0 ) );
                IChartIndicatorElement indicatorElement = ChartPanel.CreateIndicatorElement();
                indicatorElement.FullTitle = innerSecurity.Id;
                indicatorElement.Color = _colors[num++];
                indicatorElement.StrokeThickness = 1;
                CandlePartIndicator candlePartIndicator = new CandlePartIndicator();
                _sourceElements.Add( innerSecurity, indicatorElement );
                ChartPanel.AddElement( mainArea, indicatorElement, candleSeries, candlePartIndicator );
            }
        }

        private void CanExecuteDrawSecurityCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !HasError;
        }

        private void SecurityPicker_OnSecurityDoubleClick( Security security )
        {
            if ( IsStarted )
                return;
            InsertSecurity( security );
        }

        private void SecurityPicker_OnMouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( IsStarted )
                return;
            Security selectedSecurity = SecurityPicker.SelectedSecurity;
            if ( selectedSecurity == null )
                return;
            int num = ( int )DragDrop.DoDragDrop( SecurityPicker, selectedSecurity, DragDropEffects.Copy );
        }



        public class CandlePartIndicator : BaseIndicator
        {
            private readonly Security _security;

            public CandlePartIndicator()
            {
            }

            public CandlePartIndicator( Security security )
            {
                _security = security;
            }

            protected override IIndicatorValue OnProcess( IIndicatorValue input )
            {
                Candle candle = input.GetValue<Candle>();
                IsFormed = _security == null || candle.Security == _security;
                return new CandleIndicatorValue( this, candle, c => c.ClosePrice );
            }
        }
    }
}
