using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Wintellect.PowerCollections;

namespace StockSharp.Xaml
{
    public partial class SecurityPicker : UserControl, IPersistable, IComponentConnector
    {
        public static readonly DependencyProperty SelectionModeProperty           = DependencyProperty.Register( nameof( SelectionMode )          , typeof( MultiSelectMode ), typeof( SecurityPicker ), new PropertyMetadata( MultiSelectMode.Row, new PropertyChangedCallback( OnSelectionModePropertyChanged ) ) );
        public static readonly DependencyProperty ShowCommonStatColumnsProperty   = DependencyProperty.Register( nameof( ShowCommonStatColumns )  , typeof( bool )           , typeof( SecurityPicker ), new PropertyMetadata( false, new PropertyChangedCallback( ShowCommonStatColumnsPropertyChanged ) ) );        
        public static readonly DependencyProperty ShowCommonOptionColumnsProperty = DependencyProperty.Register( nameof( ShowCommonOptionColumns ), typeof( bool )           , typeof( SecurityPicker ), new PropertyMetadata( false, new PropertyChangedCallback( ShowCommonOptionColumnsPropertyChanged ) ) );
        public static readonly DependencyProperty TitleProperty                   = DependencyProperty.Register( nameof( Title )                  , typeof( string )         , typeof( SecurityPicker ), new PropertyMetadata( string.Empty, new PropertyChangedCallback( OnTitleChanged ) ) );

        
        

        private static readonly HashSet<string> _commonStatColumns = new HashSet<string>( ) 
                                                                                            { 
                                                                                                AppendDot( new string[ 1 ] { "PriceStep" } ), 
                                                                                                AppendDot( new string[ 1 ] { "VolumeStep" } ), 
                                                                                                AppendDot( new string[ 2 ] { "BestBid", "Price" } ), 
                                                                                                AppendDot( new string[ 2 ] { "BestBid", "Volume" } ), 
                                                                                                AppendDot( new string[ 2 ] { "BestAsk", "Price" } ), 
                                                                                                AppendDot( new string[ 2 ] { "BestAsk", "Volume" } ), 
                                                                                                AppendDot( new string[ 2 ] { "LastTrade", "Price" } ), 
                                                                                                AppendDot( new string[ 2 ] { "LastTrade", "Volume" } ), 
                                                                                                AppendDot( new string[ 3 ] { "LastTrade", "Time", "TimeOfDay" } ), 
                                                                                                AppendDot( new string[ 3 ] { "LastTrade", "Time", "Date" } ) 
                                                                                            };

        private static readonly HashSet<string> _commonOptionColumns = new HashSet<string>( ) 
                                                                                            { 
                                                                                                AppendDot( new string[ 1 ] { "Strike" } ), 
                                                                                                AppendDot( new string[ 1 ] { "OptionType" } ), 
                                                                                                AppendDot( new string[ 1 ] { "TheorPrice" } ), 
                                                                                                AppendDot( new string[ 1 ] { "ImpliedVolatility" } ), 
                                                                                                AppendDot( new string[ 1 ] { "HistoricalVolatility" } ) 
                                                                                            };

        private readonly SynchronizedList<Tuple<bool, NotifyCollectionChangedAction, IEnumerable<Security>>> _tonySyncList = new CachedSynchronizedList<Tuple<bool, NotifyCollectionChangedAction, IEnumerable<Security>>>( );
        private string                                   _securityFilter = string.Empty;
        private string                                   _title          = string.Empty;
        private readonly CollectionSecurityProvider      _securities     = new CollectionSecurityProvider( );
        private readonly CachedSynchronizedSet<Security> _excludeSecurities;
        private string                                   _prevFilter;
        private SecurityTypes?                           _prevType;
        private bool                                     _ownProvider;
        private bool                                     _checkFiltered;
        private SecurityTypes?                           _selectedType;
        private ISecurityProvider                        _securityProvider;

        public SecurityPicker( )
        {
            InitializeComponent( );

            GuiDispatcher.GlobalDispatcher.AddPeriodicalAction( new Action( SecurityPickerPeriodicalAction ) );
            
            _excludeSecurities                    = new CachedSynchronizedSet<Security>( );
            _excludeSecurities.AddedRange        += AddSecurities;
            _excludeSecurities.RemovedRange      += RemoveSecurities;
            _excludeSecurities.Cleared           += ClearSecurities;

            SecurityProvider                      = null;
            SecuritiesCtrl.SelectionMode          = MultiSelectMode.Row;

            UpdateCounter( );
            
            SecurityTypeCtrl.NullItem.Description = LocalizedStrings.Str1569;
        }

        private void AddSecurities( IEnumerable<Security> securities )
        {
            SecurityProviderOnSecuritiesChanged( true, NotifyCollectionChangedAction.Add, securities );
        }

        private void RemoveSecurities( IEnumerable<Security> securities )
        {
            SecurityProviderOnSecuritiesChanged( true, NotifyCollectionChangedAction.Remove, securities );
        }

        private void ClearSecurities( )
        {
            SecurityProviderOnSecuritiesChanged( true, NotifyCollectionChangedAction.Reset, null );
        }

        private static void OnSelectionModePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( SecurityPicker )d ).SecuritiesCtrl.SelectionMode = ( MultiSelectMode )e.NewValue;
        }

        private static void OnTitleChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var child = d.FindLogicalChild<SecurityPicker>( );

            string newValue               = ( string )e.NewValue;
            child.SecurityFilterCtrl.Text = newValue;
            child._title                  = newValue;
        }

        public MultiSelectMode SelectionMode
        {
            get
            {
                return ( MultiSelectMode )GetValue( SelectionModeProperty );
            }
            set
            {
                SetValue( SelectionModeProperty, value );
            }
        }        

        private static void OnCommonStatColumnsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e, HashSet<string> commonStatColumns )
        {         
            SecurityPicker securityPicker = ( SecurityPicker )d;
            
            var isVisibile = ( bool )e.NewValue;
            
            securityPicker.SecuritiesCtrl.Columns.Where( c => _commonStatColumns.Contains( c.FieldName ) ).ForEach( c => c.Visible = isVisibile );
        }

        private static string AppendDot( string[ ] parameters )
        {
            return ( ( IEnumerable<string> )parameters ).Join( "." );
        }

        private static void ShowCommonStatColumnsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            OnCommonStatColumnsChanged( d, e, _commonStatColumns );
        }

        public bool ShowCommonStatColumns
        {
            get
            {
                return ( bool )GetValue( ShowCommonStatColumnsProperty );
            }
            set
            {
                SetValue( ShowCommonStatColumnsProperty, value );
            }
        }

        private static void ShowCommonOptionColumnsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            OnCommonStatColumnsChanged( d, e, _commonOptionColumns );
        }

        public bool ShowCommonOptionColumns
        {
            get
            {
                return ( bool )GetValue( ShowCommonOptionColumnsProperty );
            }
            set
            {
                SetValue( ShowCommonOptionColumnsProperty, value );
            }
        }

        public event Action<Security> SecurityDoubleClick;

        public event Action<Security> SecuritySelected;

        public event Action GridChanged;

        public Security SelectedSecurity
        {
            get
            {
                return SecuritiesCtrl.SelectedSecurity;
            }
            set
            {
                SecuritiesCtrl.SelectedSecurity = value;
            }
        }

        public IList<Security> SelectedSecurities
        {
            get
            {
                return SecuritiesCtrl.SelectedSecurities;
            }
        }

        public IListEx<Security> FilteredSecurities
        {
            get
            {
                return SecuritiesCtrl.Securities;
            }
        }

        public SecurityGrid UnderlyingGrid
        {
            get
            {
                return SecuritiesCtrl;
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
                SecurityTypeCtrl.SelectedType = value;
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
                SecurityFilterCtrl.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetValue( TitleProperty, value );
            }
        }

        public SynchronizedList<Security> Securities
        {
            get
            {
                return _securities;
            }
        }

        public ISet<Security> ExcludeSecurities
        {
            get
            {
                return _excludeSecurities;
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

                    if ( _ownProvider )
                    {
                        _securityProvider.Dispose( );
                    }
                }

                if ( value == null )
                {
                    value = new FilterableSecurityProvider( _securities, false );
                    _ownProvider = true;
                }
                else
                {
                    _ownProvider = false;
                }

                _securityProvider          = value;
                _securityProvider.Added   += OnAddNewSecurities;
                _securityProvider.Removed += OnRemoveSecurities;
                _securityProvider.Cleared += OnClearSecurities;

                FilterSecurities( false );
            }
        }

        private void OnAddNewSecurities( IEnumerable<Security> securities )
        {
            SecurityProviderOnSecuritiesChanged( false, NotifyCollectionChangedAction.Add, securities );
        }

        private void OnRemoveSecurities( IEnumerable<Security> securities )
        {
            SecurityProviderOnSecuritiesChanged( false, NotifyCollectionChangedAction.Remove, securities );
        }

        private void OnClearSecurities( )
        {
            SecurityProviderOnSecuritiesChanged( false, NotifyCollectionChangedAction.Reset, null );
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return SecuritiesCtrl.MarketDataProvider;
            }
            set
            {
                SecuritiesCtrl.MarketDataProvider = value;
            }
        }

        public IExtendedInfoStorageItem ExtendedInfoStorage
        {
            get
            {
                return SecuritiesCtrl.ExtendedInfoStorage;
            }
            set
            {
                SecuritiesCtrl.ExtendedInfoStorage = value;
            }
        }

        public IPriceChartDataProvider PriceChartDataProvider
        {
            get
            {
                return SecuritiesCtrl.PriceChartDataProvider;
            }
            set
            {
                SecuritiesCtrl.PriceChartDataProvider = value;
            }
        }        


        public void SetColumnVisibility( string name, Visibility visibility )
        {            
            SecuritiesCtrl.Columns.Where( c => c.FieldName.CompareIgnoreCase( name ) ).ForEach( c => c.Visible = visibility == Visibility.Visible );
        }

        public void AddContextMenuItem( Control menuItem )
        {
            if ( menuItem == null )
            {
                throw new ArgumentNullException( nameof( menuItem ) );
            }

            SecuritiesCtrl.ContextMenu.Items.Add( menuItem );
        }

        private void SecurityProviderOnSecuritiesChanged( bool exclude, NotifyCollectionChangedAction changedAction, IEnumerable<Security> securities )
        {
            if ( !CheckAccess( ) )
            {
                _prevFilter = null;
                _prevType   = new SecurityTypes?( );

                lock ( _tonySyncList.SyncRoot )
                {
                    _tonySyncList.Add( Tuple.Create( exclude, changedAction, securities ) );
                }
            }
            else
            {
                SecuritiesCtrl.BeginDataUpdate( );

                try
                {
                    InnerSecurityProviderOnSecuritiesChanged( exclude, changedAction, securities );
                }
                finally
                {
                    SecuritiesCtrl.EndDataUpdate( );
                    UpdateCounter( );
                }
            }
        }

        private void InnerSecurityProviderOnSecuritiesChanged( bool exclude, NotifyCollectionChangedAction changeAction, IEnumerable<Security> securities )
        {
            switch ( changeAction )
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if ( exclude )
                    {
                        FilterSecurities( false );
                        break;
                    }
                    securities = securities.Where( new Func<Security, bool>( CheckCondition ) );

                    if ( _checkFiltered )
                    {
                        securities = securities.Where( s => !FilteredSecurities.Contains( s ) ).ToArray( );
                    }

                    FilteredSecurities.AddRange( securities );
                }                    
                break;

                case NotifyCollectionChangedAction.Remove:
                {
                    if ( exclude )
                    {
                        FilterSecurities( true );
                        break;
                    }
                    FilteredSecurities.RemoveRange( securities );
                }
                break;

                case NotifyCollectionChangedAction.Reset:
                {
                    if ( exclude )
                    {
                        FilterSecurities( false );
                        break;
                    }
                    FilteredSecurities.Clear( );
                }
                break;

                default:
                    throw new ArgumentOutOfRangeException( "action" );
            }
        }

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
        

        private void FilterSecurities( bool bool_3 )
        {
            SecuritiesCtrl.BeginDataUpdate( );
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
                int? indexOfAt    = filter?.LastIndexOf( '@' );
                
                if ( indexOfAt.GetValueOrDefault( ) > 0 & indexOfAt.HasValue )
                {
                    string oldFilter = filter;
                    filter           = oldFilter.Substring( 0, indexOfAt.Value );
                    prevFilter       = oldFilter.Substring( indexOfAt.Value + 1 );
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
            }
            finally
            {
                SecuritiesCtrl.EndDataUpdate( );
                UpdateCounter( );
            }
        }
        

        private void UpdateCounter( )
        {
            int totalCount = FilteredSecurities.Count;
            int showCount  = SecurityProvider.Count - _excludeSecurities.Count;

            SecurityFilterCtrl.Text = totalCount == showCount ? string.Empty : string.Format( "{0}/{1}", totalCount, showCount );
        }

        private void SecuritiesCtrl_ItemDoubleClick( object sender, ItemDoubleClickEventArgs e )
        {
            Security security = SecuritiesCtrl.CurrentSecurity;
            if ( security == null )
            {
                return;
            }

            Action<Security> action0 = SecurityDoubleClick;
            if ( action0 == null )
            {
                return;
            }

            action0( security );
        }

        private void SecurityTypeCtrl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            _prevType      = _selectedType;
            _selectedType  = SecurityTypeCtrl.SelectedType;
            FilterSecurities( false );
            _prevType      = _selectedType;
            _checkFiltered = true;

            Action changedAction = GridChanged;
            
            if ( changedAction == null )
            {
                return;
            }

            changedAction( );
        }

        private void SecurityFilterCtrl_TextChanged( object sender, TextChangedEventArgs e )
        {
            _prevFilter     = _securityFilter;
            _securityFilter = SecurityFilterCtrl.Text;
            FilterSecurities( false );
            _prevFilter     = _securityFilter;
            _checkFiltered  = true;

            Action changedAction = GridChanged;
            if ( changedAction == null )
            {
                return;
            }

            changedAction( );
        }

        private void SecuritiesCtrl_SelectionChanged( object sender, GridSelectionChangedEventArgs e )
        {
            if ( e.Action == CollectionChangeAction.Refresh )
            {
                return;
            }

            Action<Security> action1 = SecuritySelected;
            if ( action1 == null )
            {
                return;
            }

            action1( SelectedSecurity );
        }

        private void SecuritiesCtrl_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            Action<Security> action1 = SecuritySelected;
            if ( action1 == null )
            {
                return;
            }

            action1( SelectedSecurity );
        }

        private void SecuritiesCtrl_LayoutChanged( )
        {
            Action action2 = GridChanged;
            if ( action2 == null )
            {
                return;
            }

            action2( );
        }

        public void Load( SettingsStorage storage )
        {
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "GridSettings", null );
            if ( storage1 != null )
            {
                SecuritiesCtrl.Load( storage1 );
            }

            SecurityFilter = storage.GetValue<string>( "SecurityFilter", null );
            SelectedType = ( storage.GetValue<string>( "SelectedType", null ) ?? storage.GetValue<string>( "SecurityType", null ) ).To<SecurityTypes?>( );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "GridSettings", SecuritiesCtrl.Save( ) );
            storage.SetValue( "SecurityFilter", SecurityFilter );
            storage.SetValue( "SelectedType", SelectedType.To<string>( ) );
        }



        private void SecurityPickerPeriodicalAction( )
        {
            Tuple<bool, NotifyCollectionChangedAction, IEnumerable<Security>>[ ] tupleArray;
            lock ( _tonySyncList.SyncRoot )
            {
                if ( _tonySyncList.Count == 0 )
                {
                    return;
                }

                tupleArray = _tonySyncList.CopyAndClear( );
            }
            SecuritiesCtrl.BeginDataUpdate( );
            try
            {
                foreach ( Tuple<bool, NotifyCollectionChangedAction, IEnumerable<Security>> tuple in tupleArray )
                {
                    InnerSecurityProviderOnSecuritiesChanged( tuple.Item1, tuple.Item2, tuple.Item3 );
                }
            }
            finally
            {
                SecuritiesCtrl.EndDataUpdate( );
                _checkFiltered = false;
                UpdateCounter( );
            }
        }

        

        

        

       

       
        

        
        
    }
}
