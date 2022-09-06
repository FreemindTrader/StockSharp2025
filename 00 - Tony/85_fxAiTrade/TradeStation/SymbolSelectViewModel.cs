using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Definitions;
using System.Collections.ObjectModel;
using DevExpress.Utils.Svg;
using System.Windows.Controls;
using Ecng.Configuration;
using StockSharp.Studio.Core.Commands;
using StockSharp.BusinessEntities;
using StockSharp.Algo;
using Ecng.Collections;
using Ecng.Common;
using StockSharp.Messages;
using Ecng.Xaml;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using fx.Common;
using Ecng.Serialization;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using DevExpress.Xpf.Bars;
using fx.Charting;
using StockSharp.Xaml;
using fx.Collections;
using fx.Algorithm;
using fx.Bars;
using Ecng.ComponentModel;

namespace FreemindAITrade.ViewModels
{
    [POCOViewModel]
    public class SymbolSelectViewModel : IPersistable
    {
        private readonly CachedSynchronizedSet<Security>            _excludeSecurities = new CachedSynchronizedSet<Security>( );
        private readonly ThreadSafeObservableCollection< Security > _filterSecurities;        
        private readonly CollectionSecurityProvider                 _securities        = new CollectionSecurityProvider( );
        private ISecurityProvider                                   _securityProvider;        
        private string                                              _prevFilter        = "";
        private string                                              _securityFilter    = string.Empty;
        private SecurityTypes?                                      _prevType          = null;
        private SecurityTypes?                                      _selectedType;
        private SymbolForGrid                                       _selectedItem;

        private string _selectedSecurity = null;

        public virtual ChartPanelOrderSettings OrderSettings { get; set; }

        public virtual PortfolioPickerWindowViewModel portfolioVM { get; set; }

        public SymbolSelectViewModel()
        {
            SymbolForGridCollection = new ObservableCollection<SymbolForGrid>( );            

            var securities          = new ObservableCollectionEx<Security>( );

            _filterSecurities       = new ThreadSafeObservableCollection<Security>( securities ) { MaxCount = 1000 };

            Sec01                   = false;
            Min01                   = true;
            Min04                   = false;
            Min05                   = true;
            Min15                   = true;
            Min30                   = true;
            Hrs01                   = true;
            Hrs02                   = true;
            Hrs03                   = true;
            Hrs04                   = true;
            Hrs06                   = true;
            Hrs08                   = true;
            Daily                   = true;
            Weekly                  = true;
            Monthly                 = true;
            ToSelectAll             = false;
            IsBarIntegrityCheck     = false;
            ShowAllTimeFrameCharts  = false;
           
            //SBar.Inspect(typeof(SBar));

            var symboexSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(SymbolEx));

            var taSignalSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(SBar));

            OrderSettings = new ChartPanelOrderSettings( );

            if ( OrderSettings.Portfolio == null )
            {
                var PortfolioSource = ConfigManager.GetService< PortfolioDataSource >( );

                portfolioVM = PortfolioPickerWindowViewModel.Create( );

                portfolioVM.PortfolioDataSource = PortfolioSource;                
            }
        }

        public PooledList<TimeSpan> GetSelectedTimeFrames()
        {
            PooledList< TimeSpan > output = new PooledList< TimeSpan >( );

            if ( Sec01 )
            {
                output.Add( TimeSpan.FromSeconds( 1 ) );
            }

            if ( Min01 )
            {
                output.Add( TimeSpan.FromMinutes( 1 ) );
            }

            if ( Min04 )
            {
                output.Add( TimeSpan.FromMinutes( 4 ) );
            }

            if ( Min05 )
            {
                output.Add( TimeSpan.FromMinutes( 5 ) );
            }

            if ( Min15 )
            {
                output.Add( TimeSpan.FromMinutes( 15 ) );
            }

            if ( Min30 )
            {
                output.Add( TimeSpan.FromMinutes( 30 ) );
            }

            if ( Hrs01 )
            {
                output.Add( TimeSpan.FromHours( 1 ) );
            }

            if ( Hrs02 )
            {
                output.Add( TimeSpan.FromHours( 2 ) );
            }

            if ( Hrs03 )
            {
                output.Add( TimeSpan.FromHours( 3 ) );
            }

            if ( Hrs04 )
            {
                output.Add( TimeSpan.FromHours( 4 ) );
            }

            if ( Hrs06 )
            {
                output.Add( TimeSpan.FromHours( 6 ) );
            }

            if ( Hrs08 )
            {
                output.Add( TimeSpan.FromHours( 8 ) );
            }

            if ( Daily )
            {
                output.Add( TimeSpan.FromDays( 1 ) );
            }

            if ( Weekly )
            {
                output.Add( TimeSpan.FromDays( 7 ) );
            }

            if ( Monthly )
            {
                output.Add( TimeSpan.FromDays( 30 ) );
            }            

            return output;
        }

        public void SelectWaveButton()
        {
            Sec01   = false;
            Min01   = true;
            Min04   = false;
            Min05   = false;
            Min15   = false;
            Min30   = false;
            Hrs01   = true;
            Hrs02   = false;
            Hrs03   = false;
            Hrs04   = false;
            Hrs06   = false;
            Hrs08   = false;
            Daily   = true;
            Weekly  = true;
            Monthly = true;
        }

        public void SelectStandardButton( )
        {
            Sec01   = false;
            Min01   = true;
            Min04   = false;
            Min05   = true;
            Min15   = true;
            Min30   = true;
            Hrs01   = true;
            Hrs02   = true;
            Hrs03   = true;
            Hrs04   = true;
            Hrs06   = true;
            Hrs08   = true;
            Daily   = true;
            Weekly  = true;
            Monthly = true;
        }

        public void SelectMinButton( )
        {
            Sec01   = false;
            Min01   = true;
            Min04   = false;
            Min05   = false;
            Min15   = false;
            Min30   = false;
            Hrs01   = false;
            Hrs02   = false;
            Hrs03   = false;
            Hrs04   = false;
            Hrs06   = false;
            Hrs08   = false;
            Daily   = true;
            Weekly  = true;
            Monthly = true;
        }

        public void SelectMaxButton( )
        {
            Sec01   = true;
            Min01   = true;
            Min04   = true;
            Min05   = true;
            Min15   = true;
            Min30   = true;
            Hrs01   = true;
            Hrs02   = true;
            Hrs03   = true;
            Hrs04   = true;
            Hrs06   = true;
            Hrs08   = true;
            Daily   = true;
            Weekly  = true;
            Monthly = true;
        }

        public static SymbolSelectViewModel Create()
        {
            return ViewModelSource.Create( ( ) => new SymbolSelectViewModel( ) );
        }

        protected TradeStationViewModel ParentVM 
        { 
            get 
            { 
                return this.GetParentViewModel<TradeStationViewModel>( ); 
            } 
        }

        public virtual ICollection<SymbolForGrid> SymbolForGridCollection
        {
            get;
            set;
        }

        public virtual SymbolForGrid SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;

                _securities.Clear( );

                _securities.Add( _selectedItem.Security );

                IsEnable = _selectedItem != null;

                _selectedSecurity = _selectedItem.Security.Code;
            }
        }

        public string SelectedSecurity
        {
            get
            {
                return _selectedSecurity;
            }

            set
            {
                if( value == null )
                    return;

                _selectedSecurity = value;

                foreach ( var symbolForGrid in SymbolForGridCollection )
                {
                    if ( symbolForGrid.Security.Code == _selectedSecurity )
                    {
                        SelectedItem = symbolForGrid;
                        break;
                    }
                }
            }
        }

        public virtual bool ToSelectAll { get; set; }
        

        public void OnToSelectAllChanged( )
        {
            Sec01   = ToSelectAll;
            Min01   = ToSelectAll;
            Min04   = ToSelectAll;
            Min05   = ToSelectAll;
            Min15   = ToSelectAll;
            Min30   = ToSelectAll;
            Hrs01   = ToSelectAll;
            Hrs02   = ToSelectAll;
            Hrs03   = ToSelectAll;
            Hrs04   = ToSelectAll;
            Hrs06   = ToSelectAll;
            Hrs08   = ToSelectAll;
            Daily   = ToSelectAll;
            Weekly  = ToSelectAll;
            Monthly = ToSelectAll;
        }
        
        public virtual bool IsLiveTrading { get; set; }
        public virtual bool IsBackTesting { get; set; }
        public virtual bool IsBarIntegrityCheck { get; set; }
        public virtual bool ShowAllTimeFrameCharts { get; set; }
        public virtual bool Sec01 { get; set; }        
        public virtual bool Min01 { get; set; }
        public virtual bool Min04 { get; set; }
        public virtual bool Min05 { get; set; }
        public virtual bool Min15 { get; set; }
        public virtual bool Min30 { get; set; }
        public virtual bool Hrs01 { get; set; }
        public virtual bool Hrs02 { get; set; }
        public virtual bool Hrs03 { get; set; }
        public virtual bool Hrs04 { get; set; }
        public virtual bool Hrs06 { get; set; }
        public virtual bool Hrs08 { get; set; }
        public virtual bool Daily { get; set; }
        public virtual bool Weekly { get; set; }
        public virtual bool Monthly { get; set; }

        public virtual bool LoadAllDaily { get; set; }

        public virtual bool LoadAll01Min { get; set; }

        public virtual bool LoadAllHourly { get; set; }


        public virtual DateTime StartDate { get; set; } = new DateTime( 2020, 01, 01 );
        public virtual DateTime EndDate { get; set; } = DateTime.UtcNow;        


        public virtual bool IsEnable
        {
            get; set;

        }

        public void AskParentToCloseWindow()
        {
            ParentVM.CloseDocument( );
        }

        public Task OnLoaded( )
        {
            return Task.Factory.StartNew( PopulateAllSymbols );
        }

        private void PopulateAllSymbols( )
        {
            ConfigManager.GetService< IStudioCommandService >( ).Register< LookupSecuritiesResultCommand >( this, false, OnSecurityResults );        
        }

        private void OnSecurityResults( LookupSecuritiesResultCommand cmd )
        {
            foreach ( Security security in cmd.Securities )
            {
                //SecuritiesAll.Securities.TryAdd<Security>( security );
            }
        }

        public CollectionSecurityProvider Securities
        {
            get
            {
                return _securities;
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return _securityProvider;
            }

            set
            {
                if ( _securityProvider != null && _securityProvider == value )
                {
                    return;
                }

                if ( _securityProvider != null )
                {
                    _securityProvider.Added   -= OnAddNewSecurities;
                    _securityProvider.Removed -= OnRemoveSecurities;
                    _securityProvider.Cleared -= OnClearSecurities;

                    _securityProvider = null;
                }

                if ( value == null )
                {
                    value = new FilterableSecurityProvider( _securities );
                    
                }
                else
                {
                    
                }

                _securityProvider = value;
                _securityProvider.Added += OnAddNewSecurities;
                _securityProvider.Removed += OnRemoveSecurities;
                _securityProvider.Cleared += OnClearSecurities;

                FilterSecurities( false );
            }
        }

        public string SecurityFilter
        {
            get
            {
                return _securityFilter;
            }
            set
            {
                _securityFilter = value;
            }
        }

        public SecurityTypes? SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
            }
        }

        public IListEx<Security> FilteredSecurities
        {
            get
            {
                return _filterSecurities;
            }
        }

        private void FilterSecurities( bool bool_3 )
        {
            //SecuritiesCtrl.BeginDataUpdate( );
            try
            {
                string filter = SecurityFilter?.Trim( );

                if ( !bool_3 && !_prevFilter.IsEmpty( ) && ( filter != null && filter.StartsWithIgnoreCase( _prevFilter ) ) )
                {
                    if ( ( _prevType.GetValueOrDefault( ) == SelectedType.GetValueOrDefault( ) & _prevType.HasValue == SelectedType.HasValue || !_prevType.HasValue && SelectedType.HasValue ) && FilteredSecurities.Count < 500 )
                    {
                        FilteredSecurities.RemoveWhere( s => !CheckCondition( s ) );
                        return;
                    }
                }

                string prevFilter = null;
                int? indexOfAt = filter?.LastIndexOf( '@' );

                if ( indexOfAt.GetValueOrDefault( ) > 0 & indexOfAt.HasValue )
                {
                    string oldFilter = filter;
                    filter = oldFilter.Substring( 0, indexOfAt.Value );
                    prevFilter = oldFilter.Substring( indexOfAt.Value + 1 );
                }

                IEnumerable<Security> source = filter.IsEmpty( ) ? SecurityProvider.LookupAll( ) : SecurityProvider.LookupByCode( filter );

                if ( prevFilter != null )
                {
                    source = source.Where( s => s.Board.Code.StartsWithIgnoreCase( prevFilter ) );
                }

                Security[ ] array = source.Where( s =>
                                                        {
                                                            if ( _excludeSecurities.Contains( s ) )
                                                            {
                                                                return false;
                                                            }

                                                            if ( !_prevType.HasValue )
                                                            {
                                                                return true;
                                                            }

                                                            return s.Type.GetValueOrDefault( ) == _prevType.GetValueOrDefault( ) & s.Type.HasValue == _prevType.HasValue;
                                                        }
                                               ).ToArray( );

                if ( FilteredSecurities.SequenceEqual( array ) )
                {
                    return;
                }

                FilteredSecurities.Clear( );
                FilteredSecurities.AddRange( array );

                SymbolForGridCollection.Clear( );

                foreach ( Security security in array )
                {
                    var secWithout = security.Code.Replace( @"/", "" );

                    SymbolForGrid newEntry = new SymbolForGrid( security.Type.ToString(), security.Code, SymbolImageHelper.GetImageSourece( security ), security );

                    SymbolForGridCollection.Add( newEntry );
                }


                BaseUserConfig<StudioUserConfig>.Instance.TryLoadSettings( "SymbolSelectView", new Action<SettingsStorage>( Load ) );
            }
            finally
            {
                //SecuritiesCtrl.EndDataUpdate( );
                //UpdateCounter( );
            }
        }

        //public void LoadStream( )
        //{
        //    var assemblyName = System.Reflection.Assembly.GetEntryAssembly( ).GetName( );
        //    var myAssembly = Assembly.Load( ( string ) assemblyName.Name );

        //    //var mainfest = myAssembly + "." + ( ( string )this.Path ).Replace( '/', '.' );

        //    var names = System.Reflection.Assembly.GetExecutingAssembly( ).GetManifestResourceNames( );

        //    //using ( Stream manifestResourceStream = myAssembly.GetManifestResourceStream( mainfest ) )
        //    //{
        //    //    //MyInnerDict = ( ResourceDictionary )XamlReader.Load( manifestResourceStream );
        //    //}

        //}

        private bool CheckCondition( Security sec )
        {
            string securityFilter = SecurityFilter;
            SecurityTypes? selectedType = SelectedType;

            if ( !_excludeSecurities.Contains( sec ) )
            {
                if ( selectedType.HasValue )
                {
                    if ( !( sec.Type.GetValueOrDefault( ) == selectedType.GetValueOrDefault( ) & sec.Type.HasValue ) )
                    {
                        return false;
                    }
                }
                if ( !securityFilter.IsEmpty( ) && ( sec.Code.IsEmpty( ) || !sec.Code.ContainsIgnoreCase( securityFilter ) ) && ( ( sec.Name.IsEmpty( ) || !sec.Name.ContainsIgnoreCase( securityFilter ) ) && ( sec.ShortName.IsEmpty( ) || !sec.ShortName.ContainsIgnoreCase( securityFilter ) ) ) )
                {
                    return sec.Id.ContainsIgnoreCase( securityFilter );
                }

                return true;
            }

            return false;
        }

        private void OnAddNewSecurities( IEnumerable<Security> securities )
        {
            //SecurityProviderOnSecuritiesChanged( false, NotifyCollectionChangedAction.Add, securities );
        }

        private void OnRemoveSecurities( IEnumerable<Security> securities )
        {
            //SecurityProviderOnSecuritiesChanged( false, NotifyCollectionChangedAction.Remove, securities );
        }

        private void OnClearSecurities( )
        {
            //SecurityProviderOnSecuritiesChanged( false, NotifyCollectionChangedAction.Reset, null );
        }

        public void Load( SettingsStorage storage )
        {            
            Sec01                  = storage.GetValue<bool>( nameof( Sec01 ) );
            Min01                  = storage.GetValue<bool>( nameof( Min01 ) );
            Min04                  = storage.GetValue<bool>( nameof( Min04 ) );
            Min05                  = storage.GetValue<bool>( nameof( Min05 ) );
            Min15                  = storage.GetValue<bool>( nameof( Min15 ) );
            Min30                  = storage.GetValue<bool>( nameof( Min30 ) );
            Hrs01                  = storage.GetValue<bool>( nameof( Hrs01 ) );
            Hrs02                  = storage.GetValue<bool>( nameof( Hrs02 ) );
            Hrs03                  = storage.GetValue<bool>( nameof( Hrs03 ) );
            Hrs04                  = storage.GetValue<bool>( nameof( Hrs04 ) );
            Hrs06                  = storage.GetValue<bool>( nameof( Hrs06 ) );
            Hrs08                  = storage.GetValue<bool>( nameof( Hrs08 ) );
            Daily                  = storage.GetValue<bool>( nameof( Daily ) );
            Weekly                 = storage.GetValue<bool>( nameof( Weekly ) );
            Monthly                = storage.GetValue<bool>( nameof( Monthly ) );
            IsBarIntegrityCheck    = storage.GetValue<bool>( nameof( IsBarIntegrityCheck ) );
            ShowAllTimeFrameCharts = storage.GetValue<bool>( nameof( ShowAllTimeFrameCharts ) );
            SelectedSecurity       = storage.GetValue<string>( nameof( SelectedSecurity ) );
            TradingMode            = storage.GetValue<TradingMode>( nameof( TradingMode ) );
            
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( nameof( Sec01 ), Sec01 );
            storage.SetValue( nameof( Min01 ), Min01 );
            storage.SetValue( nameof( Min04 ), Min04 );
            storage.SetValue( nameof( Min05 ), Min05 );
            storage.SetValue( nameof( Min15 ), Min15 );
            storage.SetValue( nameof( Min30 ), Min30 );
            storage.SetValue( nameof( Hrs01 ), Hrs01 );
            storage.SetValue( nameof( Hrs02 ), Hrs02 );
            storage.SetValue( nameof( Hrs03 ), Hrs03 );
            storage.SetValue( nameof( Hrs04 ), Hrs04 );
            storage.SetValue( nameof( Hrs06 ), Hrs06 );
            storage.SetValue( nameof( Hrs08 ), Hrs08 );
            storage.SetValue( nameof( Daily ), Daily );
            storage.SetValue( nameof( Weekly ), Weekly );
            storage.SetValue( nameof( Monthly ), Monthly );
            storage.SetValue( nameof( IsBarIntegrityCheck ), IsBarIntegrityCheck );
            storage.SetValue( nameof( ShowAllTimeFrameCharts ), ShowAllTimeFrameCharts );
            storage.SetValue( nameof( SelectedSecurity ), SelectedSecurity );
            storage.SetValue( nameof( TradingMode ), TradingMode );
        }


        public TradingMode TradingMode
        {
            get
            {
                return _tradingMode;
            }

            set
            {                
                _tradingMode = value;    
                
                if ( _tradingMode == TradingMode.BACKTESTING )
                {
                    IsBackTesting = true;
                }
                else if ( _tradingMode == TradingMode.LIVETRADING )
                {
                    IsLiveTrading = true;
                }
            }
        }

        private TradingMode _tradingMode = TradingMode.LIVETRADING;

        [ Command( false )]
        public void OnCheckChanged( ItemClickEventArgs args )
        {
            ItemClickEventArgs myevent = args;

            if ( args.Source is BarCheckItem )
            {
                var checkItem = ( BarCheckItem )args.Source;

                if ( checkItem.IsChecked.Value == false )
                {
                    return;
                }
            }

            _tradingMode = ( TradingMode )Enum.Parse( typeof( TradingMode ), ( string )myevent.Item.Tag );            
        }


        [Command( false )]
        public void OnIntegrityCheckChanged( ItemClickEventArgs args )
        {
            ItemClickEventArgs myevent = args;

            if ( args.Source is BarCheckItem )
            {
                var checkItem = ( BarCheckItem )args.Source;

                if ( checkItem.IsChecked.Value == false )
                {
                    return;
                }
            }

            _tradingMode = ( TradingMode ) Enum.Parse( typeof( TradingMode ), ( string ) myevent.Item.Tag );
        }
    }

    public class SymbolPeriod : BindableBase
    {        
        public TimeSpan SymbolTimeSpan 
        {
            get { return GetValue<TimeSpan>( ); }
            set
            {
                SetValue( value );
            }
        }        

        public string ForexPeriod
        {
            get { return GetValue<string>( ); }
            set
            {
                SetValue( value );
            }
        }        

        public SymbolPeriod( TimeSpan time )
        {
            SymbolTimeSpan = time;
            ForexPeriod    = time.ToReadable( );
        }
    }

    public class SymbolForGrid : BindableBase
    {
        private Security _security;

        public ISymbol SymbolObject
        {
            get { return GetValue<ISymbol>( ); }
            set
            {
                SetValue( value );
            }
        }

        public string SymbolGroup
        {
            get { return GetValue<string>( ); }
            set
            {
                SetValue( value );
            }
        }

        public string SymbolName
        {
            get { return GetValue<string>( ); }
            set
            {
                SetValue( value );
            }
        }

        public BitmapImage SymbolImage
        {
            get { return GetValue<BitmapImage>( ); }
            set
            {
                SetValue( value );
            }
        }

        public Security Security
        {
            get { return _security; }
            set
            {
                _security = value;
            }
        }

        public SymbolForGrid( string group, string name, BitmapImage image, Security security )
        {
            SymbolGroup = group;
            SymbolName  = name;
            SymbolImage = image;
            Security    = security;
        }

        public SymbolForGrid( ISymbol symbol )
        {
            SymbolObject = symbol;

            if ( symbol.SymbolGroup == fx.Definitions.SymbolGroup.NA )
            {
                symbol.FixSymbol( );
            }

            SymbolGroup = symbol.SymbolGroup.ToString( );

            SymbolName = symbol.SymbolString;
        }
    }
}
